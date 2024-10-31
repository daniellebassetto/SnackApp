using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SnackApp.Models;

namespace SnackApp.Context;

public class SnackAppContext(DbContextOptions<SnackAppContext> options) : IdentityDbContext<IdentityUser>(options)
{
    public DbSet<Categoria> Categorias { get; set; }
    public DbSet<Lanche> Lanches { get; set; }
    public DbSet<CarrinhoCompraItem> CarrinhoCompraItens { get; set; }
    public DbSet<Pedido> Pedidos { get; set; }
    public DbSet<PedidoItem> PedidoItens { get; set; }
}