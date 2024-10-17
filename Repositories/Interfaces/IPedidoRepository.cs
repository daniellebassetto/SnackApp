using SnackApp.Models;

namespace SnackApp.Repositories.Interfaces;

public interface IPedidoRepository
{
    void Criar(Pedido pedido);
}