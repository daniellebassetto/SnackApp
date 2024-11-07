using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SnackApp.Models;
using SnackApp.Repositories.Interfaces;
using SnackApp.ViewModels;

namespace SnackApp.Controllers;

public class CarrinhoCompraController(ILancheRepository lancheRepository, CarrinhoCompra carrinhoCompra) : Controller
{
    private readonly ILancheRepository _lancheRepository = lancheRepository;
    private readonly CarrinhoCompra _carrinhoCompra = carrinhoCompra;

    public IActionResult Index()
    {
        _carrinhoCompra.Itens = _carrinhoCompra.GetCarrinhoCompraItens();
        
        var carrinhoCompraViewModel = new CarrinhoCompraViewModel
        {
            CarrinhoCompra = _carrinhoCompra,
            Total = _carrinhoCompra.GetCarrinhoCompraTotal()
        };

        return View(carrinhoCompraViewModel);
    }

    [Authorize]
    public IActionResult AdicionarItem(int lancheId)
    {
        var lancheSelecionado = _lancheRepository.Lanches.FirstOrDefault(l => l.Id == lancheId);

        if (lancheSelecionado != null)
            _carrinhoCompra.AdicionarItem(lancheSelecionado);

        return RedirectToAction("Index");
    }

    [Authorize]
    public IActionResult RemoverItem(int lancheId)
    {
        var lancheSelecionado = _lancheRepository.Lanches.FirstOrDefault(l => l.Id == lancheId);

        if (lancheSelecionado != null)
            _carrinhoCompra.RemoverItem(lancheSelecionado);

        return RedirectToAction("Index");
    }
}