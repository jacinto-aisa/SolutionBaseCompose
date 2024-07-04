using System.Data;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MvcWebMusica2.Models;
using MvcWebMusica2.Services.Repositorio;

namespace MvcWebMusica2.Controllers
{
    public class ConciertosController(
        IGenericRepositorio<Conciertos> repositorioConciertos,
        IGenericRepositorio<Ciudades> repositorioCiudades,
        IGenericRepositorio<Giras> repositorioGiras
        ) : Controller
    {
        private readonly string _nombre = "Nombre";
        private readonly string _ciudadesId = "CiudadesId";
        private readonly string _girasId = "GirasId";

        // GET: Conciertos
        public async Task<IActionResult> Index()
        {
            var listaConciertos = await repositorioConciertos.DameTodos();
            foreach (var concierto in listaConciertos)
            {
                concierto.Ciudades = await repositorioCiudades.DameUno(concierto.CiudadesId);
                concierto.Giras = await repositorioGiras.DameUno(concierto.GirasId);
            }
            return View(listaConciertos);
        }

        // GET: Conciertos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var concierto = await repositorioConciertos.DameUno(id);

            if (concierto == null)
            {
                return NotFound();
            }

            concierto.Ciudades = await repositorioCiudades.DameUno(concierto.CiudadesId);
            concierto.Giras = await repositorioGiras.DameUno(concierto.GirasId);

            return View(concierto);
        }

        // GET: Conciertos/Create
        public async Task<IActionResult> CreateAsync()
        {
            ViewData[_ciudadesId] = new SelectList(await repositorioCiudades.DameTodos(), "Id", _nombre);
            ViewData[_girasId] = new SelectList(await repositorioGiras.DameTodos(), "Id", _nombre);
            return View();
        }

        // POST: Conciertos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,GirasId,Fecha,CiudadesId,Direccion")] Conciertos conciertos)
        {
            if (ModelState.IsValid)
            {
                await repositorioConciertos.Agregar(conciertos);
                return RedirectToAction(nameof(Index));
            }
            ViewData[_ciudadesId] = new SelectList(await repositorioCiudades.DameTodos(), "Id", _nombre, conciertos.CiudadesId);
            ViewData[_girasId] = new SelectList(await repositorioGiras.DameTodos(), "Id", _nombre, conciertos.GirasId);
            return View(conciertos);
        }

        // GET: Conciertos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var concierto = await repositorioConciertos.DameUno(id);
            if (concierto == null)
            {
                return NotFound();
            }
            ViewData[_ciudadesId] = new SelectList(await repositorioCiudades.DameTodos(), "Id", _nombre, concierto.CiudadesId);
            ViewData[_girasId] = new SelectList(await repositorioGiras.DameTodos(), "Id", _nombre, concierto.GirasId);
            return View(concierto);
        }

        // POST: Conciertos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,GirasId,Fecha,CiudadesId,Direccion")] Conciertos conciertos)
        {
            if (id != conciertos.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await repositorioConciertos.Modificar(id, conciertos);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await ConciertosExists(conciertos.Id))
                    {
                        return NotFound();
                    }

                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData[_ciudadesId] = new SelectList(await repositorioCiudades.DameTodos(), "Id", _nombre, conciertos.CiudadesId);
            ViewData[_girasId] = new SelectList(await repositorioGiras.DameTodos(), "Id", _nombre, conciertos.GirasId);
            return View(conciertos);
        }

        // GET: Conciertos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var concierto = await repositorioConciertos.DameUno(id);

            if (concierto == null)
            {
                return NotFound();
            }

            concierto.Ciudades = await repositorioCiudades.DameUno(concierto.CiudadesId);
            concierto.Giras = await repositorioGiras.DameUno(concierto.GirasId);

            return View(concierto);
        }

        // POST: Conciertos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var concierto = await repositorioConciertos.DameUno(id);
            if (concierto != null)
            {
                await repositorioConciertos.Borrar(id);
            }

            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> ConciertosExists(int id)
        {
            var lista = await repositorioConciertos.DameTodos();
            return lista.Exists(e => e.Id == id);
        }

        [HttpGet]
        public async Task<FileResult> DescargarExcel()
        {
            var conciertos = await repositorioConciertos.DameTodos();
            foreach (var concierto in conciertos)
            {
                concierto.Ciudades = await repositorioCiudades.DameUno(concierto.CiudadesId);
                concierto.Giras = await repositorioGiras.DameUno(concierto.GirasId);
            }
            var nombreArchivo = "Conciertos.xlsx";
            return GenerarExcel(nombreArchivo, conciertos);
        }

        private FileContentResult GenerarExcel(string nombreArchivo, IEnumerable<Conciertos> conciertos)
        {
            DataTable dataTable = new("Conciertos");
            dataTable.Columns.AddRange([
                new("Fecha"),
                new("Direccion"),
                new("Ciudades"),
                new("Giras")
            ]);

            foreach (var concierto in conciertos)
            {
                dataTable.Rows.Add(
                    concierto.Fecha,
                    concierto.Direccion,
                    concierto.Ciudades?.Nombre,
                    concierto.Giras?.Nombre);
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
