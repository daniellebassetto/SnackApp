using Microsoft.AspNetCore.Mvc;
using SnackApp.Repositories.Interfaces;

namespace SnackApp.Components;

public class CategoriaMenu(ICategoriaRepository categoriaRepository) : ViewComponent
{
    private readonly ICategoriaRepository _categoriaRepository = categoriaRepository;

    public IViewComponentResult Invoke()
    {
        var categorias = _categoriaRepository.Categorias.OrderBy(c => c.Nome);
        return View(categorias);
    }
}