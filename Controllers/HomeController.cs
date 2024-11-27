using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace PizzaProjeto.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

}