using Microsoft.EntityFrameworkCore;
using SnackApp.Context;
using SnackApp.Models;
using SnackApp.Repositories.Interfaces;

namespace SnackApp.Repositories;

public class LancheRepository(SnackAppContext context) : ILancheRepository
{
    private readonly SnackAppContext _context = context;

    public IEnumerable<Lanche> Lanches => _context.Lanches.Include(x => x.Categoria);

    public IEnumerable<Lanche> LanchesPreferidos => _context.Lanches.Where(x => x.LanchePreferido).Include(x => x.Categoria);

    public Lanche GetById(int id) => _context.Lanches.FirstOrDefault(x => x.Id == id);
}