using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using PizzaProjeto.DTO;
using PizzaProjeto.Models;
using PizzaProjeto.Services.Pizza;

namespace PizzaProjeto.Controllers;

public class PizzaController : Controller
{
    private readonly IPizzaInterface _pizzaInterface;
    public PizzaController(IPizzaInterface pizzaInterface)
    {
        _pizzaInterface = pizzaInterface;
    }
    public  async Task<IActionResult> Index()
    {
        var pizzas = await _pizzaInterface.GetPizzas();
        return View(pizzas);
    }

    public IActionResult Cadastrar()
    {
        return View();
    }

    public async Task<IActionResult> Detalhes(int id)
    {
        var pizzas = await _pizzaInterface.GetPizzaById(id);
        return View(pizzas);
    }

    public async Task<IActionResult> Editar(int id)
    {
        var pizza = await _pizzaInterface.GetPizzaById(id);
        return View(pizza);
    }
    
    [HttpPost]
    public async Task<IActionResult> Cadastrar(PizzaCriacaoDTO pizzaCriacaoDto, IFormFile foto)
    {
        if (ModelState.IsValid)
        {
            var pizza = await _pizzaInterface.criarPizza(pizzaCriacaoDto,foto);
            return RedirectToAction("Index", "Pizza");
        }
        else
        {
            return View(pizzaCriacaoDto);
        }
    }

    public async Task<IActionResult> Remover(int id)
    {
        var pizza = await _pizzaInterface.Remover(id);
        
        return RedirectToAction("Index", "Pizza");
    }
    
    [HttpPost]
    public async Task<IActionResult> Editar(PizzaModel pizzaModel, IFormFile foto)
    {
        if (ModelState.IsValid)
        {
            var pizza = await _pizzaInterface.EditarPizza(pizzaModel, foto);
            return RedirectToAction("Index", "Pizza");
        }
        else
        {
            return View(pizzaModel);
        }
        
    }
}