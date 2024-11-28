using PizzaProjeto.DTO;
using PizzaProjeto.Migrations;
using PizzaProjeto.Models;

namespace PizzaProjeto.Services.Pizza;

public interface IPizzaInterface
{
    
    Task<PizzaModel> criarPizza(PizzaCriacaoDTO pizzaCriacaoDto, IFormFile foto);
    Task<List<PizzaModel>> GetPizzas();
    
    Task<PizzaModel> GetPizzaById(int id);

    Task<PizzaModel> EditarPizza(PizzaModel pizza, IFormFile foto);
    
    Task<PizzaModel> Remover(int id);

    Task<List<PizzaModel>> GetPizzasFiltro(string? pesquisar);
}