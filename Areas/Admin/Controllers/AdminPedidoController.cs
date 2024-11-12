using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SnackApp.Context;
using SnackApp.Models;

namespace SnackApp.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class AdminPedidoController(SnackAppContext context) : Controller
{
    private readonly SnackAppContext _context = context;

    public async Task<IActionResult> Index()
    {
        return View(await _context.Pedidos.ToListAsync());
    }

    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
            return NotFound();

        var pedido = await _context.Pedidos.FirstOrDefaultAsync(m => m.Id == id);
        if (pedido == null)
            return NotFound();

        return View(pedido);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Nome,Sobrenome,Endereco,Complemento,Cep,Estado,Cidade,Telefone,Email,Data,DataEntrega")] Pedido pedido)
    {
        if (ModelState.IsValid)
        {
            _context.Add(pedido);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(pedido);
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
            return NotFound();

        var pedido = await _context.Pedidos.FindAsync(id);

        if (pedido == null)
            return NotFound();

        return View(pedido);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Sobrenome,Endereco,Complemento,Cep,Estado,Cidade,Telefone,Email,Data,DataEntrega")] Pedido pedido)
    {
        if (id != pedido.Id)
            return NotFound();

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(pedido);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PedidoExists(pedido.Id))
                    return NotFound();
                else
                    throw;
            }
            return RedirectToAction(nameof(Index));
        }
        return View(pedido);
    }

    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
            return NotFound();

        var pedido = await _context.Pedidos.FirstOrDefaultAsync(m => m.Id == id);
        if (pedido == null)
            return NotFound();

        return View(pedido);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var pedido = await _context.Pedidos.FindAsync(id);
        if (pedido != null)
            _context.Pedidos.Remove(pedido);

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool PedidoExists(int id)
    {
        return _context.Pedidos.Any(e => e.Id == id);
    }
}