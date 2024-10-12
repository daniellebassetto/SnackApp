using SnackApp.Context;
using SnackApp.Models;
using SnackApp.Repositories.Interfaces;

namespace SnackApp.Repositories;

public class CategoriaRepository(SnackAppContext context) : ICategoriaRepository
{
    private readonly SnackAppContext _context = context;

    public IEnumerable<Categoria> Categorias => _context.Categorias;
}