using SnackApp.Context;
using SnackApp.Models;
using SnackApp.Repositories.Interfaces;

namespace SnackApp.Repositories;

public class PedidoRepository(SnackAppContext context) : IPedidoRepository
{
    private readonly SnackAppContext _context = context;

    public void Criar(Pedido pedido)
    {
        pedido.Data = DateTime.Now;
        _context.Pedidos.Add(pedido);
        _context.SaveChanges();

        var carrinhoCompraItens = _context.CarrinhoCompraItens;
        foreach (var item in carrinhoCompraItens)
        {
            var pedidoItem = new PedidoItem
            {
                PedidoId = pedido.Id,
                LancheId = item.Lanche.Id,
                Quantidade = item.Quantidade,
                Preco = item.Lanche.Preco
            };
            _context.PedidoItens.Add(pedidoItem);
        }
        _context.SaveChanges();
    }
}