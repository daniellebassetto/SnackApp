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

    public IActionResult Search(string search)
    {
        IEnumerable<Lanche> lanches;
        string categoriaAtual = string.Empty;

        if (string.IsNullOrEmpty(search))
        {
            lanches = _lancheRepository.Lanches.OrderBy(l => l.Id);
            categoriaAtual = "Todos os lanches";
        }
        else
        {
            lanches = _lancheRepository.Lanches.Where(p => p.Nome.ToLower().Contains(search.ToLower()));

            if (lanches.Any())
                categoriaAtual = "Lanches";
            else
                categoriaAtual = "Nenhum lanche foi encontrado";
        }

        return View("~/Views/Lanche/List.cshtml", new LancheListViewModel
        {
            Lanches = lanches,
            CategoriaAtual = categoriaAtual
        });
    }
}