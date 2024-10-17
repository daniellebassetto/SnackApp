using Microsoft.AspNetCore.Mvc;
using SnackApp.Models;
using SnackApp.ViewModels;

namespace SnackApp.Components;

public class CarrinhoCompraResumo(CarrinhoCompra carrinhoCompra) : ViewComponent
{
    private readonly CarrinhoCompra _carrinhoCompra = carrinhoCompra;

    public IViewComponentResult Invoke()
    {
        _carrinhoCompra.Itens = _carrinhoCompra.GetCarrinhoCompraItens();

        var carrinhoCompraViewModel = new CarrinhoCompraViewModel
        {
            CarrinhoCompra = _carrinhoCompra,
            Total = _carrinhoCompra.GetCarrinhoCompraTotal()
        };

        return View(carrinhoCompraViewModel);
    }
}