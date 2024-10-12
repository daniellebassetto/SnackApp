using Microsoft.AspNetCore.Mvc;
using SnackApp.Repositories.Interfaces;
using SnackApp.ViewModels;

namespace SnackApp.Controllers;

public class LancheController(ILancheRepository lancheRepository) : Controller
{
    private readonly ILancheRepository _lancheRepository = lancheRepository;

    public IActionResult List()
    {
        var lancheListViewModel = new LancheListViewModel();
        var lanches = _lancheRepository.Lanches;
        lancheListViewModel.Lanches = lanches;
        lancheListViewModel.CategoriaAtual = "Categoria atual";
        
        return View(lanches);
    }
}