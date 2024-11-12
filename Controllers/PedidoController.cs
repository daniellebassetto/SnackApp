using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SnackApp.Models;
using SnackApp.Repositories.Interfaces;

namespace SnackApp.Controllers;

public class PedidoController(IPedidoRepository pedidoRepository, CarrinhoCompra carrinhoCompra) : Controller
{
    private readonly IPedidoRepository _pedidoRepository = pedidoRepository;
    private readonly CarrinhoCompra _carrinhoCompra = carrinhoCompra;

    [Authorize]
    [HttpGet]
    public IActionResult Checkout()
    {
        return View();
    }

    [Authorize]
    [HttpPost]
    public IActionResult Checkout(Pedido pedido)
    {
        int totalItens = 0;
        decimal precoTotal = 0.0m;

        List<CarrinhoCompraItem> items = _carrinhoCompra.GetCarrinhoCompraItens();
        _carrinhoCompra.Itens = items;

        if (_carrinhoCompra.Itens.Count == 0)
            ModelState.AddModelError("", "Seu carrinho está vazio, inclua um lanche...");

        foreach (var item in items)
        {
            totalItens += item.Quantidade;
            precoTotal += item.Lanche.Preco * item.Quantidade;
        }

        pedido.TotalItens = totalItens;
        pedido.Total = precoTotal;

        if (ModelState.IsValid)
        {
            _pedidoRepository.Criar(pedido);
            ViewBag.CheckoutCompletoMensagem = "Obrigado pelo seu pedido :)";
            ViewBag.TotalPedido = _carrinhoCompra.GetCarrinhoCompraTotal();
            _carrinhoCompra.LimparCarrinho();
            return View("~/Views/Pedido/CheckoutCompleto.cshtml", pedido);
        }

        return View(pedido);
    }
}
