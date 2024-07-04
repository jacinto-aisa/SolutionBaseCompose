using Microsoft.AspNetCore.Mvc;
using MvcWebMusica2.Models;
using MvcWebMusica2.Services.Repositorio;
using MvcWebMusica2.Services.Specification;

namespace MvcWebMusica2.Views.Shared.Components.Giras
{
    public class GirasViewComponent(IGenericRepositorio<Conciertos> coleccion,IGenericRepositorio<Ciudades> colCiudad) : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(int giraId)
        {
            var items = await coleccion.DameTodos();
            IConciertoSpecification especificacion = new GiraSpecification(giraId);
            foreach (var concierto in items)
            {
                var ciudadEncontrada = await colCiudad.DameUno(concierto.CiudadesId);
                if (ciudadEncontrada != null)
                    concierto.Direccion = ciudadEncontrada.Nombre;
            }
            
            var itemsFiltrados = items.AsParallel().Where(especificacion.IsValid);


            return View(itemsFiltrados);
        }
    }
}
