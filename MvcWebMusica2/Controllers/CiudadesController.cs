using System.Data;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MvcWebMusica2.Models;
using MvcWebMusica2.Services.Repositorio;

namespace MvcWebMusica2.Controllers
{
    public class CiudadesController(
        IGenericRepositorio<Ciudades> repositorioCiudades,
        IGenericRepositorio<Paises> repositorioPaises
        ) : Controller
    {
        private readonly string _nombre = "Nombre";
        private readonly string _paisesId = "PaisesID";

        // GET: Ciudades
        public async Task<IActionResult> Index()
        {
            var listaCiudades = await repositorioCiudades.DameTodos();
            foreach (var ciudad in listaCiudades)
            {
                ciudad.Paises = await repositorioPaises.DameUno(ciudad.PaisesID);
            }
            return View(listaCiudades);
        }

        // GET: Ciudades/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ciudad = await repositorioCiudades.DameUno(id);

            if (ciudad == null)
            {
                return NotFound();
            }

            ciudad.Paises = await repositorioPaises.DameUno(ciudad.PaisesID);

            return View(ciudad);
        }

        // GET: Ciudades/Create
        public async Task<IActionResult> CreateAsync()
        {
            ViewData[_paisesId] = new SelectList(await repositorioPaises.DameTodos(), "Id", _nombre);
            return View();
        }

        // POST: Ciudades/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,PaisesID")] Ciudades ciudades)
        {
            if (ModelState.IsValid)
            {
                await repositorioCiudades.Agregar(ciudades);
                return RedirectToAction(nameof(Index));
            }
            ViewData[_paisesId] = new SelectList(await repositorioPaises.DameTodos(), "Id", _nombre, ciudades.PaisesID);
            return View(ciudades);
        }

        // GET: Ciudades/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ciudad = await repositorioCiudades.DameUno(id);
            if (ciudad == null)
            {
                return NotFound();
            }
            ViewData[_paisesId] = new SelectList(await repositorioPaises.DameTodos(), "Id", _nombre, ciudad.PaisesID);
            return View(ciudad);
        }

        // POST: Ciudades/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,PaisesID")] Ciudades ciudades)
        {
            if (id != ciudades.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await repositorioCiudades.Modificar(id, ciudades);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await CiudadesExists(ciudades.Id))
                    {
                        return NotFound();
                    }

                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData[_paisesId] = new SelectList(await repositorioPaises.DameTodos(), "Id", _nombre, ciudades.PaisesID);
            return View(ciudades);
        }

        // GET: Ciudades/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ciudad = await repositorioCiudades.DameUno(id);

            if (ciudad == null)
            {
                return NotFound();
            }

            ciudad.Paises = await repositorioPaises.DameUno(ciudad.PaisesID);

            return View(ciudad);
        }

        // POST: Ciudades/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ciudad = await repositorioCiudades.DameUno(id);
            if (ciudad != null)
            {
                await repositorioCiudades.Borrar(id);
            }

            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> CiudadesExists(int id)
        {
            var lista = await repositorioCiudades.DameTodos();
            return lista.Exists(e => e.Id == id);
        }

        [HttpGet]
        public async Task<FileResult> DescargarExcel()
        {
            var ciudades = await repositorioCiudades.DameTodos();
            foreach (var ciudad in ciudades)
            {
                ciudad.Paises = await repositorioPaises.DameUno(ciudad.PaisesID);
            }
            var nombreArchivo = "Ciudades.xlsx";
            return GenerarExcel(nombreArchivo, ciudades);
        }

        private FileContentResult GenerarExcel(string nombreArchivo, IEnumerable<Ciudades> ciudades)
        {
            DataTable dataTable = new("Ciudades");
            dataTable.Columns.AddRange([
                new("Nombre"),
                new("Paises")
            ]);

            foreach (var ciudad in ciudades)
            {
                dataTable.Rows.Add(
                    ciudad.Nombre,
                    ciudad.Paises?.Nombre);
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
