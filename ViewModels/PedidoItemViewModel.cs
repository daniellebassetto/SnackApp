using SnackApp.Models;

namespace SnackApp.ViewModels;

public class PedidoItemViewModel
{
    public Pedido Pedido { get; set; }
    public IEnumerable<PedidoItem> Itens { get; set; }
}