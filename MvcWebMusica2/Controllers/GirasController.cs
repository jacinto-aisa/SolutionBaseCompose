using System.Data;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MvcWebMusica2.Models;
using MvcWebMusica2.Services.Repositorio;

namespace MvcWebMusica2.Controllers
{
    public class GirasController(
        IGenericRepositorio<Giras> repositorioGiras,
        IGenericRepositorio<Grupos> repositorioGrupos)
        : Controller
    {
        private readonly string _nombre = "Nombre";

        // GET: Giras

        private async Task<List<Giras>> DameListaDeGiras()
        {
            var listaGiras = await repositorioGiras.DameTodos();
            foreach (var giras in listaGiras)
            {
                giras.Grupos = await repositorioGrupos.DameUno(giras.GruposId);
            }

            return listaGiras;
        }

        public async Task<IActionResult> Index()
        {
            return View(await DameListaDeGiras());
        }

        
        // GET: Informacion de las Giras

        public async Task<IActionResult> InfoGiras()
        {
            return View(await DameListaDeGiras());
        }

        // GET: Giras/Details/5

        private async Task<Giras?> DameGira(int? id)
        {
            if (id == null)
            {
                return null;
            }

            var giras = await repositorioGiras.DameUno(id);
            if (giras == null)
            {
                return null;
            }

            giras.Grupos = await repositorioGrupos.DameUno(giras.GruposId);

            return giras;
        }
        public async Task<IActionResult> Details(int? id)
        {
            return View(await DameGira(id));
        }

        // GET: Giras/Create
        public async Task<IActionResult> Create()
        {
            ViewData["GruposId"] = new SelectList(await repositorioGrupos.DameTodos(), "Id", _nombre);
            return View();
        }

        // POST: Giras/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,GruposId,FechaInicio,FechaFin")] Giras giras)
        {
            if (ModelState.IsValid)
            {
                await repositorioGiras.Agregar(giras);
                return RedirectToAction(nameof(Index));
            }
            ViewData["GruposId"] = new SelectList(await repositorioGrupos.DameTodos(), "Id", _nombre, giras.GruposId);
            return View(giras);
        }

        // GET: Giras/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var giras = await repositorioGiras.DameUno(id);
            if (giras == null)
            {
                return NotFound();
            }
            ViewData["GruposId"] = new SelectList(await repositorioGrupos.DameTodos(), "Id", _nombre, giras.GruposId);
            return View(giras);
        }

        // POST: Giras/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,GruposId,FechaInicio,FechaFin")] Giras giras)
        {
            if (id != giras.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await repositorioGiras.Modificar(id, giras);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await GirasExists(giras.Id))
                    {
                        return NotFound();
                    }

                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["GruposId"] = new SelectList(await repositorioGrupos.DameTodos(), "Id", _nombre, giras.GruposId);
            return View(giras);
        }

        // GET: Giras/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            return View(await DameGira(id));
        }

        // POST: Giras/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var giras = await repositorioGiras.DameUno(id);
            if (giras != null)
            {
                await repositorioGiras.Borrar(id);
            }

            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> GirasExists(int id)
        {
            var lista = await repositorioGiras.DameTodos();
            return lista.Exists(e => e.Id == id);
        }

        [HttpGet]
        public async Task<FileResult> DescargarExcel()
        {
            var giras = await repositorioGiras.DameTodos();
            foreach (var gira in giras)
            {
                gira.Grupos = await repositorioGrupos.DameUno(gira.GruposId);
            }
            var nombreArchivo = "Giras.xlsx";
            return GenerarExcel(nombreArchivo, giras);
        }

        private FileContentResult GenerarExcel(string nombreArchivo, IEnumerable<Giras> giras)
        {
            DataTable dataTable = new("Giras");
            dataTable.Columns.AddRange([
                new("Nombre"),
                new("FechaInicio"),
                new("FechaFin"),
                new("Grupos")
            ]);

            foreach (var gira in giras)
            {
                dataTable.Rows.Add(
                    gira.Nombre,
                    gira.FechaInicio,
                    gira.FechaFin,
                    gira.Grupos?.Nombre);
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
