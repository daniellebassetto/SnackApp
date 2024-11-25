using SnackApp.Areas.Admin.Services;
using Microsoft.AspNetCore.Mvc;

namespace SnackApp.Areas.Admin.Controllers;

[Area("Admin")]
public class AdminRelatorioVendaController(RelatorioVendaService service) : Controller
{
    private readonly RelatorioVendaService _service = service;

    public IActionResult Index()
    {
        return View();
    }

    public async Task<IActionResult> RelatorioVendasSimples(DateTime? minDate,
        DateTime? maxDate)
    {
        if (!minDate.HasValue)
            minDate = new DateTime(DateTime.Now.Year,1, 1);

        if (!maxDate.HasValue)
            maxDate = DateTime.Now;

        ViewData["minDate"] = minDate.Value.ToString("yyyy-MM-dd");
        ViewData["maxDate"] = maxDate.Value.ToString("yyyy-MM-dd");

        var result = await _service.FindByDateAsync(minDate, maxDate);
        return View(result);
    }
}