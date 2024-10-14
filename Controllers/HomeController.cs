using Microsoft.AspNetCore.Mvc;
using SnackApp.Models;
using SnackApp.Repositories.Interfaces;
using SnackApp.ViewModels;
using System.Diagnostics;

namespace SnackApp.Controllers;

public class HomeController(ILancheRepository lancheRepository) : Controller
{
    private readonly ILancheRepository _lancheRepository = lancheRepository;

    public IActionResult Index()
    {
        var homeViewModel = new HomeViewModel
        {
            LanchesPreferidos = _lancheRepository.LanchesPreferidos
        };

        return View(homeViewModel);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}