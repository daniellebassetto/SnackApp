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
        _carrinhoCompra.CarrinhoCompraItens = _carrinhoCompra.GetCarrinhoCompraItens();
        
        var carrinhoCompraViewModel = new CarrinhoCompraViewModel
        {
            CarrinhoCompra = _carrinhoCompra,
            Total = _carrinhoCompra.GetCarrinhoCompraTotal()
        };

        return View(carrinhoCompraViewModel);
    }

    public IActionResult AdicionarAoCarrinho(int lancheId)
    {
        var lancheSelecionado = _lancheRepository.Lanches.FirstOrDefault(l => l.Id == lancheId);

        if (lancheSelecionado != null)
            _carrinhoCompra.AdicionarAoCarrinho(lancheSelecionado);

        return RedirectToAction("Index");
    }

    public IActionResult RemoverDoCarrinho(int lancheId)
    {
        var lancheSelecionado = _lancheRepository.Lanches.FirstOrDefault(l => l.Id == lancheId);

        if (lancheSelecionado != null)
            _carrinhoCompra.RemoverDoCarrinho(lancheSelecionado);

        return RedirectToAction("Index");
    }
}