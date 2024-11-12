using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SnackApp.Context;
using SnackApp.Models;

namespace SnackApp.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class AdminLancheController(SnackAppContext context) : Controller
{
    private readonly SnackAppContext _context = context;

    public async Task<IActionResult> Index()
    {
        var snackAppContext = _context.Lanches.Include(l => l.Categoria);
        return View(await snackAppContext.ToListAsync());
    }

    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
            return NotFound();

        var lanche = await _context.Lanches.Include(l => l.Categoria).FirstOrDefaultAsync(m => m.Id == id);
        if (lanche == null)
            return NotFound();

        return View(lanche);
    }

    public IActionResult Create()
    {
        ViewData["CategoriaId"] = new SelectList(_context.Categorias, "Id", "Descricao");
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Nome,DescricaoCurta,DescricaoDetalhada,Preco,ImagemUrl,ImagemThumbnailUrl,LanchePreferido,EmEstoque,CategoriaId")] Lanche lanche)
    {
        if (ModelState.IsValid)
        {
            _context.Add(lanche);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        ViewData["CategoriaId"] = new SelectList(_context.Categorias, "Id", "Descricao", lanche.CategoriaId);
        return View(lanche);
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
            return NotFound();

        var lanche = await _context.Lanches.FindAsync(id);
        if (lanche == null)
            return NotFound();
        ViewData["CategoriaId"] = new SelectList(_context.Categorias, "Id", "Descricao", lanche.CategoriaId);
        return View(lanche);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,DescricaoCurta,DescricaoDetalhada,Preco,ImagemUrl,ImagemThumbnailUrl,LanchePreferido,EmEstoque,CategoriaId")] Lanche lanche)
    {
        if (id != lanche.Id)
            return NotFound();

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(lanche);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LancheExists(lanche.Id))
                    return NotFound();
                else
                    throw;
            }
            return RedirectToAction(nameof(Index));
        }
        ViewData["CategoriaId"] = new SelectList(_context.Categorias, "Id", "Descricao", lanche.CategoriaId);
        return View(lanche);
    }

    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
            return NotFound();

        var lanche = await _context.Lanches.Include(l => l.Categoria).FirstOrDefaultAsync(m => m.Id == id);
        if (lanche == null)
            return NotFound();

        return View(lanche);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var lanche = await _context.Lanches.FindAsync(id);
        if (lanche != null)
            _context.Lanches.Remove(lanche);

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool LancheExists(int id)
    {
        return _context.Lanches.Any(e => e.Id == id);
    }
}