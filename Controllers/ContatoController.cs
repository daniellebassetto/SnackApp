using Microsoft.AspNetCore.Mvc;

namespace SnackApp.Controllers;

public class ContatoController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}