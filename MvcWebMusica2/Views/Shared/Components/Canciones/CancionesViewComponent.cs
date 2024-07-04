using Microsoft.AspNetCore.Mvc;
using MvcWebMusica2.Services.Repositorio;
using MvcWebMusica2.Services.Specification;

namespace MvcWebMusica2.Views.Shared.Components.Canciones
{
    public class CancionesViewComponent(IGenericRepositorio<Models.Canciones> repositorioCanciones) : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(ICancionSpecification especificacion)
        {
            var listaCanciones = await repositorioCanciones.DameTodos();
            var cancionesFiltradas = listaCanciones.Where(especificacion.IsValid);
            ViewData["CabeceraTabla"] = false;
            return View(cancionesFiltradas);
        }
    }
}
