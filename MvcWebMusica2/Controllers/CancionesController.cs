using System.Data;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MvcWebMusica2.Models;
using MvcWebMusica2.Services.Repositorio;

namespace MvcWebMusica2.Controllers
{
    public class CancionesController(
        IGenericRepositorio<Canciones> repositorioCanciones,
        IGenericRepositorio<Albumes> repositorioAlbumes
        )
        : Controller
    {
        // GET: Canciones
        public async Task<IActionResult> Index()
        {
            var listaCanciones = await repositorioCanciones.DameTodos();
            foreach (var cancion in listaCanciones)
            {
                cancion.Albumes = await repositorioAlbumes.DameUno(cancion.AlbumesId);
            }
            return View(listaCanciones);
        }

        // GET: Canciones/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cancion = await repositorioCanciones.DameUno(id);
            
            if (cancion == null)
            {
                return NotFound();
            }

            cancion.Albumes = await repositorioAlbumes.DameUno(cancion.AlbumesId);

            return View(cancion);
        }

        // GET: Canciones/Create
        public async Task<IActionResult> Create()
        {
            ViewData["AlbumesId"] = new SelectList(await repositorioAlbumes.DameTodos(), "Id", "Nombre");
            return View();
        }

        // POST: Canciones/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Titulo,Duracion,AlbumesId,Single")] Canciones cancion)
        {
            if (ModelState.IsValid)
            {
                await repositorioCanciones.Agregar(cancion);
                return RedirectToAction(nameof(Index));
            }
            ViewData["AlbumesId"] = new SelectList(await repositorioAlbumes.DameTodos(), "Id", "Nombre", cancion.AlbumesId);
            return View(cancion);
        }

        // GET: Canciones/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cancion = await repositorioCanciones.DameUno(id);
            if (cancion == null)
            {
                return NotFound();
            }
            ViewData["AlbumesId"] = new SelectList(await repositorioAlbumes.DameTodos(), "Id", "Nombre", cancion.AlbumesId);
            return View(cancion);
        }

        // POST: Canciones/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Titulo,Duracion,AlbumesId,Single")] Canciones cancion)
        {
            if (id != cancion.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await repositorioCanciones.Modificar(id, cancion);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await CancionesExists(cancion.Id))
                    {
                        return NotFound();
                    }

                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["AlbumesId"] = new SelectList(await repositorioAlbumes.DameTodos(), "Id", "Nombre", cancion.AlbumesId);
            return View(cancion);
        }

        // GET: Canciones/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cancion = await repositorioCanciones.DameUno(id);
            if (cancion == null)
            {
                return NotFound();
            }

            return View(cancion);
        }

        // POST: Canciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cancion = await repositorioCanciones.DameUno(id);
            if (cancion != null)
            {
                await repositorioCanciones.Borrar(id);
            }

            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> CancionesExists(int id)
        {
            var lista = await repositorioCanciones.DameTodos();
            return lista.Exists(e => e.Id == id);
        }

        [HttpGet]
        public async Task<FileResult> DescargarExcel()
        {
            var canciones = await repositorioCanciones.DameTodos();
            foreach (var cancion in canciones)
            {
                cancion.Albumes = await repositorioAlbumes.DameUno(cancion.AlbumesId);
            }
            var nombreArchivo = "Canciones.xlsx";
            return GenerarExcel(nombreArchivo, canciones);
        }

        private FileContentResult GenerarExcel(string nombreArchivo, IEnumerable<Canciones> canciones)
        {
            DataTable dataTable = new("Canciones");
            dataTable.Columns.AddRange([
                new("Titulo"),
                new("Duracion"),
                new("Single"),
                new("Albumes")
            ]);

            foreach (var cancion in canciones)
            {
                dataTable.Rows.Add(
                    cancion.Titulo,
                    cancion.Duracion,
                    cancion.Single,
                    cancion.Albumes?.Nombre);
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
