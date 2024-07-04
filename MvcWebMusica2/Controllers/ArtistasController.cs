using System.Data;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MvcWebMusica2.Models;
using MvcWebMusica2.Services.Repositorio;

namespace MvcWebMusica2.Controllers
{
    public class ArtistasController(
        IGenericRepositorio<Artistas> repositorioArtistas,
        IGenericRepositorio<Ciudades> repositorioCiudades,
        IGenericRepositorio<Generos> repositorioGeneros,
        IGenericRepositorio<Grupos> repositorioGrupos)
        : Controller
    {
        private readonly string _nombre = "Nombre";

        // GET: Artistas
        public async Task<IActionResult> Index()
        {
            var listaArtistas = await repositorioArtistas.DameTodos();
            foreach (var artista in listaArtistas)
            {
                artista.Ciudades = await repositorioCiudades.DameUno(artista.CiudadesId);
                artista.Generos = await repositorioGeneros.DameUno(artista.GenerosId);
                artista.Grupos = await repositorioGrupos.DameUno(artista.GruposId);

            }
            return View(listaArtistas);
        }

        // GET: Aristas y Funciones
        public async Task<IActionResult> ArtistasYFunciones()
        {
            var listaArtistas = await repositorioArtistas.DameTodos();
            return View(listaArtistas);
        }

        // GET: Artistas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var artista = await repositorioArtistas.DameUno(id);
            if (artista == null)
            {
                return NotFound();
            }

            artista.Ciudades = await repositorioCiudades.DameUno(artista.CiudadesId);
            artista.Generos = await repositorioGeneros.DameUno(artista.GenerosId);
            artista.Grupos = await repositorioGrupos.DameUno(artista.GruposId);

            return View(artista);
        }

        // GET: Artistas/Create
        public async Task<IActionResult> Create()
        {
            ViewData["CiudadesId"] = new SelectList(await repositorioCiudades.DameTodos(), "Id", _nombre);
            ViewData["GenerosId"] = new SelectList(await repositorioGeneros.DameTodos(), "Id", _nombre);
            ViewData["GruposId"] = new SelectList(await repositorioGrupos.DameTodos(), "Id", _nombre);
            return View();
        }

        // POST: Artistas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,GenerosId,FechaDeNacimiento,CiudadesId,GruposId")] Artistas artista)
        {
            if (ModelState.IsValid)
            {
                await repositorioArtistas.Agregar(artista);
                return RedirectToAction(nameof(Index));
            }
            ViewData["CiudadesId"] = new SelectList(await repositorioCiudades.DameTodos(), "Id", _nombre, artista.CiudadesId);
            ViewData["GenerosId"] = new SelectList(await repositorioGeneros.DameTodos(), "Id", _nombre, artista.GenerosId);
            ViewData["GruposId"] = new SelectList(await repositorioGrupos.DameTodos(), "Id", _nombre, artista.GruposId);
            return View(artista);
        }

        // GET: Artistas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var artista = await repositorioArtistas.DameUno(id);
            
            if (artista == null)
            {
                return NotFound();
            }

            ViewData["CiudadesId"] = new SelectList(await repositorioCiudades.DameTodos(), "Id", _nombre, artista.CiudadesId);
            ViewData["GenerosId"] = new SelectList(await repositorioGeneros.DameTodos(), "Id", _nombre, artista.GenerosId);
            ViewData["GruposId"] = new SelectList(await repositorioGrupos.DameTodos(), "Id", _nombre, artista.GruposId);
            return View(artista);
        }

        // POST: Artistas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,GenerosId,FechaDeNacimiento,CiudadesId,GruposId")] Artistas artista)
        {
            if (id != artista.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await repositorioArtistas.Modificar(id, artista);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await ArtistasExists(artista.Id))
                    {
                        return NotFound();
                    }

                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CiudadesId"] = new SelectList(await repositorioCiudades.DameTodos(), "Id", _nombre, artista.CiudadesId);
            ViewData["GenerosId"] = new SelectList(await repositorioGeneros.DameTodos(), "Id", _nombre, artista.GenerosId);
            ViewData["GruposId"] = new SelectList(await repositorioGrupos.DameTodos(), "Id", _nombre, artista.GruposId);
            return View(artista);
        }

        // GET: Artistas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var artista = await repositorioArtistas.DameUno(id);
            if (artista == null)
            {
                return NotFound();
            }

            artista.Ciudades = await repositorioCiudades.DameUno(artista.CiudadesId);
            artista.Generos = await repositorioGeneros.DameUno(artista.GenerosId);
            artista.Grupos = await repositorioGrupos.DameUno(artista.GruposId);

            return View(artista);
        }

        // POST: Artistas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var artista = await repositorioArtistas.DameUno(id);
            if (artista != null)
            {
                await repositorioArtistas.Borrar(id);
            }

            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> ArtistasExists(int id)
        {
            var lista = await repositorioArtistas.DameTodos();
            return lista.Exists(e => e.Id == id);
        }

        [HttpGet]
        public async Task<FileResult> DescargarExcel()
        {
            var artistas = await repositorioArtistas.DameTodos();
            foreach (var artista in artistas)
            {
                artista.Ciudades = await repositorioCiudades.DameUno(artista.CiudadesId);
                artista.Generos = await repositorioGeneros.DameUno(artista.GenerosId);
                artista.Grupos = await repositorioGrupos.DameUno(artista.GruposId);
            }
            var nombreArchivo = "Artistas.xlsx";
            return GenerarExcel(nombreArchivo, artistas);
        }

        private FileContentResult GenerarExcel(string nombreArchivo, IEnumerable<Artistas> artistas)
        {
            DataTable dataTable = new("Artistas");
            dataTable.Columns.AddRange([
                new("Nombre"),
                new("FechaDeNacimiento"),
                new("Ciudades"),
                new("Generos"),
                new("Grupos")
            ]);

            foreach (var artista in artistas)
            {
                dataTable.Rows.Add(
                    artista.Nombre,
                    artista.FechaDeNacimiento,
                    artista.Ciudades?.Nombre,
                    artista.Generos?.Nombre,
                    artista.Grupos?.Nombre
                    );
            }

            using XLWorkbook wb = new();
            wb.Worksheets.Add(dataTable);

            using MemoryStream stream = new();
            wb.SaveAs(stream);
            return File(stream.ToArray(),
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                nombreArchivo);
        }
    }
}
