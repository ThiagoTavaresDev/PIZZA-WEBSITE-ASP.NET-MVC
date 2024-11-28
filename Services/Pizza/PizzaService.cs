using Microsoft.EntityFrameworkCore;
using PizzaProjeto.Data;
using PizzaProjeto.DTO;
using PizzaProjeto.Models;

namespace PizzaProjeto.Services.Pizza;

public class PizzaService : IPizzaInterface
{
   private readonly AppDbContext _context;
   private readonly string _sistema;

   public PizzaService(AppDbContext context, IWebHostEnvironment sistema)
   {
      _context = context;
      _sistema = sistema.WebRootPath;
   }

   public string GeraCaminhoArquivo(IFormFile foto)
   {
      var codigoUnico = Guid.NewGuid().ToString();
      var nomeCaminhoImagem = foto.FileName.Replace(" ", "").ToLower() + codigoUnico + ".png";
      var caminhoParaSalvarImagens = _sistema + "\\imagem\\";

      if (!Directory.Exists(caminhoParaSalvarImagens))
      {
         Directory.CreateDirectory(caminhoParaSalvarImagens);
      }

      using (var stream = File.Create(caminhoParaSalvarImagens + nomeCaminhoImagem))
      {
         foto.CopyToAsync(stream).Wait();
      }

      return nomeCaminhoImagem;
   }

   public async Task<PizzaModel> criarPizza(PizzaCriacaoDTO pizzaCriacaoDto, IFormFile foto)
   {
      try
      {
         var nomeCaminhoImagem = GeraCaminhoArquivo(foto);
         var pizza = new PizzaModel
         {
            Sabor = pizzaCriacaoDto.Sabor,
            Descricao = pizzaCriacaoDto.Descricao,
            Valor = pizzaCriacaoDto.Valor,
            Capa = nomeCaminhoImagem
         };
         _context.Add(pizza);
         await _context.SaveChangesAsync();
         return pizza;
      }
      catch (Exception e)
      {
         throw new Exception(e.Message);
      }
   }

   public async Task<List<PizzaModel>> GetPizzas()
   {
      try
      {
         return await _context.Pizzas.ToListAsync();
      }
      catch (Exception e)
      {
         throw new Exception(e.Message);
      }
   }

   public async Task<PizzaModel> GetPizzaById(int id)
   {
      try
      {
         return await _context.Pizzas.FirstOrDefaultAsync(x => x.Id == id);
      }
      catch (Exception e)
      {
         throw new Exception(e.Message);
      }
   }

   public async Task<PizzaModel> EditarPizza(PizzaModel pizza, IFormFile foto)
   {
      try
      {
         var pizzaDb = await _context.Pizzas.AsNoTracking().FirstOrDefaultAsync(x => x.Id == pizza.Id);

         var nomeCaminhoImagem = "";

         if (foto != null)
         {
            string caminhoCapaExistente = _sistema + "\\imagem\\" + pizzaDb.Capa;

            if (File.Exists(caminhoCapaExistente))
            {
               File.Delete(caminhoCapaExistente);
            }
            nomeCaminhoImagem = GeraCaminhoArquivo(foto);
         }

         pizzaDb.Sabor = pizza.Sabor;
         pizzaDb.Descricao = pizza.Descricao;
         pizzaDb.Valor = pizza.Valor;
         if (nomeCaminhoImagem != "")
         {
            pizza.Capa = nomeCaminhoImagem;
         }
         else
         {
            pizza.Capa = pizza.Capa;
         }
         _context.Update(pizzaDb);
         await _context.SaveChangesAsync();

         return pizza;
      }
      catch (Exception e)
      {
         throw new Exception(e.Message);
      }
   }

   public async Task<PizzaModel> Remover(int id)
   {
      try
      {
         var pizza = await _context.Pizzas.FirstOrDefaultAsync(x => x.Id == id);

         _context.Remove(pizza);
        await _context.SaveChangesAsync();

         return pizza;
      }
      catch(Exception e)
      {
         throw new Exception(e.Message);
      }
   }

   public async Task<List<PizzaModel>> GetPizzasFiltro(string? pesquisar)
   {
      try
      {
         var pizzas = await _context.Pizzas.Where(x => x.Sabor.Contains(pesquisar)).ToListAsync();
         return pizzas;
      }
      catch (Exception e)
      {
         throw new Exception(e.Message);
      }
   }
}