using Microsoft.EntityFrameworkCore;
using SnackApp.Models;

namespace SnackApp.Context;

public class SnackAppContext : DbContext
{
    public SnackAppContext(DbContextOptions<SnackAppContext> options) : base(options)
    {
        
    }

    public DbSet<Categoria> Categorias { get; set; }
    public DbSet<Lanche> Lanches { get; set; }
}