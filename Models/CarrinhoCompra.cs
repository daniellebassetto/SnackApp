using Microsoft.EntityFrameworkCore;
using SnackApp.Context;

namespace SnackApp.Models;

public class CarrinhoCompra(SnackAppContext snackAppContext)
{
    private readonly SnackAppContext _snackAppContext = snackAppContext;

    public string Id { get; set; }
    public List<CarrinhoCompraItem> CarrinhoCompraItens { get; set; }

    public static CarrinhoCompra GetCarrinho(IServiceProvider serviceProvider)
    {
        ISession session = serviceProvider.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;

        var context = serviceProvider.GetService<SnackAppContext>();

        string id = session.GetString("CarrinhoId") ?? Guid.NewGuid().ToString();

        session.SetString("CarrinhoId", id);

        return new CarrinhoCompra(context) { Id = id };
    }

    public void AdicionarItem(Lanche lanche)
    {
        var carrinhoCompraItem = _snackAppContext.CarrinhoCompraItens.SingleOrDefault(s => s.Lanche.Id == lanche.Id && s.CarrinhoCompraId == Id);

        if (carrinhoCompraItem == null)
        {
            carrinhoCompraItem = new CarrinhoCompraItem
            {
                CarrinhoCompraId = Id,
                Lanche = lanche,
                Quantidade = 1
            };

            _snackAppContext.CarrinhoCompraItens.Add(carrinhoCompraItem);
        }
        else
            carrinhoCompraItem.Quantidade++;

        _snackAppContext.SaveChanges();
    }

    public int RemoverItem(Lanche lanche)
    {
        var carrinhoCompraItem = _snackAppContext.CarrinhoCompraItens.SingleOrDefault(s => s.Lanche.Id == lanche.Id && s.CarrinhoCompraId == Id);

        var quantidadeLocal = 0;

        if (carrinhoCompraItem != null)
        {
            if (carrinhoCompraItem.Quantidade > 1)
            {
                carrinhoCompraItem.Quantidade--;
                quantidadeLocal = carrinhoCompraItem.Quantidade;
            }
            else
                _snackAppContext.CarrinhoCompraItens.Remove(carrinhoCompraItem);
        }

        _snackAppContext.SaveChanges();
        return quantidadeLocal;
    }

    public List<CarrinhoCompraItem> GetCarrinhoCompraItens()
    {
        return [.. _snackAppContext.CarrinhoCompraItens.Where(c => c.CarrinhoCompraId == Id).Include(s => s.Lanche)];
    }

    public void LimparCarrinho()
    {
        var carrinhoItens = _snackAppContext.CarrinhoCompraItens.Where(carrinho => carrinho.CarrinhoCompraId == Id);

        _snackAppContext.CarrinhoCompraItens.RemoveRange(carrinhoItens);
        _snackAppContext.SaveChanges();
    }

    public decimal GetCarrinhoCompraTotal()
    {
        return _snackAppContext.CarrinhoCompraItens.Where(c => c.CarrinhoCompraId == Id).Select(c => c.Lanche.Preco * c.Quantidade).Sum();
    }
}