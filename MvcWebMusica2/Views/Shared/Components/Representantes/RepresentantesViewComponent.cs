using Microsoft.AspNetCore.Mvc;
using MvcWebMusica2.Services.Repositorio;
using MvcWebMusica2.Services.Specification;
using MvcWebMusica2.Models;

namespace MvcWebMusica2.Views.Shared.Components.Representantes
{
    public class RepresentantesViewComponent(IGenericRepositorio<Grupos> coleccion) : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(IGrupoSpecification especificacion)
        {
            var items = await coleccion.DameTodos();
            var itemsFiltrados = items.Where(especificacion.IsValid);
            return View(itemsFiltrados);
        }
    }
}
