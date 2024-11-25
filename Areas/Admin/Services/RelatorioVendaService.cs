using SnackApp.Context;
using SnackApp.Models;
using Microsoft.EntityFrameworkCore;

namespace SnackApp.Areas.Admin.Services;

public class RelatorioVendaService(SnackAppContext _context)
{
    private readonly SnackAppContext context = _context;

    public async Task<List<Pedido>> FindByDateAsync(DateTime? minDate, DateTime? maxDate)
    {
        var resultado = from obj in context.Pedidos select obj;

        if (minDate.HasValue)
            resultado = resultado.Where(x => x.Data >= minDate.Value);

        if (maxDate.HasValue)
            resultado = resultado.Where(x => x.Data <= maxDate.Value);


        return await resultado.Include(l => l.Itens).ThenInclude(l => l.Lanche).OrderByDescending(x => x.Data).ToListAsync();
    }
}