using Microsoft.AspNetCore.Mvc;
using SnackApp.Areas.Admin.Services;

namespace SnackApp.Areas.Admin.Controllers;

[Area("Admin")]
public class AdminGraficoController(GraficoVendasService graficoVendas) : Controller
{
    private readonly GraficoVendasService _graficoVendas = graficoVendas ?? throw
            new ArgumentNullException(nameof(graficoVendas));

    public JsonResult VendasLanches(int dias)
    {
        var lanchesVendasTotais = _graficoVendas.GetVendasLanches(dias);
        return Json(lanchesVendasTotais);
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }

    [HttpGet]
    public IActionResult VendasMensal()
    {
        return View();
    }

    [HttpGet]
    public IActionResult VendasSemanal()
    {
        return View();
    }
}
