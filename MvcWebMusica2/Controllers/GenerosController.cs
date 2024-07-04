using System.Data;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MvcWebMusica2.Models;
using MvcWebMusica2.Services.Repositorio;

namespace MvcWebMusica2.Controllers
{
    public class GenerosController(IGenericRepositorio<Generos> repositorioGeneros) : Controller
    {
        // GET: Generos
        public async Task<IActionResult> Index()
        {
            return View(await repositorioGeneros.DameTodos());
        }

        // GET: Generos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var generos = await repositorioGeneros.DameUno(id);
            if (generos == null)
            {
                return NotFound();
            }

            return View(generos);
        }

        // GET: Generos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Generos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre")] Generos generos)
        {
            if (ModelState.IsValid)
            {
                await repositorioGeneros.Agregar(generos);
                return RedirectToAction(nameof(Index));
            }
            return View(generos);
        }

        // GET: Generos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var generos = await repositorioGeneros.DameUno(id);
            if (generos == null)
            {
                return NotFound();
            }
            return View(generos);
        }

        // POST: Generos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre")] Generos generos)
        {
            if (id != generos.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await repositorioGeneros.Modificar(id, generos);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await GenerosExists(generos.Id))
                    {
                        return NotFound();
                    }

                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(generos);
        }

        // GET: Generos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var generos = await repositorioGeneros.DameUno(id);
            if (generos == null)
            {
                return NotFound();
            }

            return View(generos);
        }

        // POST: Generos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var generos = await repositorioGeneros.DameUno(id);
            if (generos != null)
            {
                await repositorioGeneros.Borrar(id);
            }

            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> GenerosExists(int id)
        {
            var lista = await repositorioGeneros.DameTodos();
            return lista.Exists(e => e.Id == id);
        }

        [HttpGet]
        public async Task<FileResult> DescargarExcel()
        {
            var generos = await repositorioGeneros.DameTodos();
            var nombreArchivo = "Generos.xlsx";
            return GenerarExcel(nombreArchivo, generos);
        }

        private FileContentResult GenerarExcel(string nombreArchivo, IEnumerable<Generos> generos)
        {
            DataTable dataTable = new("Generos");
            dataTable.Columns.AddRange([
                new("Nombre")
            ]);

            foreach (var genero in generos)
            {
                dataTable.Rows.Add(
                    genero.Nombre);
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
