using Microsoft.AspNetCore.Mvc;
using SnackApp.Models;
using SnackApp.Repositories.Interfaces;
using SnackApp.ViewModels;
using System.Globalization;

namespace SnackApp.Controllers;

public class LancheController(ILancheRepository lancheRepository) : Controller
{
    private readonly ILancheRepository _lancheRepository = lancheRepository;

    public IActionResult List(string categoria)
    {
        IEnumerable<Lanche> lanches;

        if (string.IsNullOrEmpty(categoria))
        {
            lanches = _lancheRepository.Lanches.OrderBy(l => l.Id);
            categoria = "Todos os lanches";
        }
        else
            lanches = _lancheRepository.Lanches.Where(l => l.Categoria.Nome.Equals(categoria, StringComparison.OrdinalIgnoreCase)).OrderBy(l => l.Nome);

        TextInfo textInfo = new CultureInfo("pt-BR", false).TextInfo;
        var lancheListViewModel = new LancheListViewModel
        {
            Lanches = lanches,
            CategoriaAtual = textInfo.ToTitleCase(categoria.ToLower())
        };

        return View(lancheListViewModel);
    }

    public IActionResult Details(int id)
    {
        var lanche = _lancheRepository.GetById(id);
        return View(lanche);
    }
}