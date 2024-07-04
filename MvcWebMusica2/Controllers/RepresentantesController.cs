using System.Data;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MvcWebMusica2.Models;
using MvcWebMusica2.Services.Repositorio;

namespace MvcWebMusica2.Controllers
{
    public class RepresentantesController(
        IGenericRepositorio<Representantes> repositorioRepresentantes,
        IGenericRepositorio<Ciudades> repositorioCiudades) 
        : Controller
    {
        private readonly string _nombre = "Nombre";
        private readonly string _ciudadesId = "CiudadesId";
        
        // GET: Representantes
        public async Task<IActionResult> Index()
        {
            var listaRepresentantes = await repositorioRepresentantes.DameTodos();
            foreach (var representante in listaRepresentantes)
            {
                representante.Ciudades = await repositorioCiudades.DameUno(representante.CiudadesID);
            }
            return View(listaRepresentantes);
        }

        // GET: Representantes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var representantes = await repositorioRepresentantes.DameUno(id);
            if (representantes == null)
            {
                return NotFound();
            }

            representantes.Ciudades = await repositorioCiudades.DameUno(representantes.CiudadesID);

            return View(representantes);
        }

        // GET: Representantes/Create
        public async Task<IActionResult> Create()
        {
            ViewData[_ciudadesId] = new SelectList(await repositorioCiudades.DameTodos(), "Id", _nombre);
            return View();
        }

        // POST: Representantes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NombreCompleto,FechaNacimiento,Identificacion,mail,Telefono,CiudadesID")] Representantes representantes)
        {
            if (ModelState.IsValid)
            {
                await repositorioRepresentantes.Agregar(representantes);
                return RedirectToAction(nameof(Index));
            }
            ViewData[_ciudadesId] = new SelectList(await repositorioCiudades.DameTodos(), "Id", _nombre, representantes.CiudadesID);
            return View(representantes);
        }

        // GET: Representantes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var representantes = await repositorioRepresentantes.DameUno(id);
            if (representantes == null)
            {
                return NotFound();
            }
            ViewData[_ciudadesId] = new SelectList(await repositorioCiudades.DameTodos(), "Id", _nombre, representantes.CiudadesID);
            return View(representantes);
        }

        // POST: Representantes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NombreCompleto,FechaNacimiento,Identificacion,mail,Telefono,CiudadesID")] Representantes representantes)
        {
            if (id != representantes.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await repositorioRepresentantes.Modificar(id, representantes);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await RepresentantesExists(representantes.Id))
                    {
                        return NotFound();
                    }

                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData[_ciudadesId] = new SelectList(await repositorioCiudades.DameTodos(), "Id", _nombre, representantes.CiudadesID);
            return View(representantes);
        }

        // GET: Representantes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var representantes = await repositorioRepresentantes.DameUno(id);
            if (representantes == null)
            {
                return NotFound();
            }

            representantes.Ciudades = await repositorioCiudades.DameUno(representantes.CiudadesID);
            return View(representantes);
        }

        // POST: Representantes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var representantes = await repositorioRepresentantes.DameUno(id);
            if (representantes != null)
            {
                await repositorioRepresentantes.Borrar(id);
            }

            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> RepresentantesExists(int id)
        {
            var lista = await repositorioRepresentantes.DameTodos();
            return lista.Exists(e => e.Id == id);
        }

        [HttpGet]
        public async Task<FileResult> DescargarExcel()
        {
            var representantes = await repositorioRepresentantes.DameTodos();
            foreach (var representante in representantes)
            {
                representante.Ciudades = await repositorioCiudades.DameUno(representante.CiudadesID);
            }
            var nombreArchivo = "Representantes.xlsx";
            return GenerarExcel(nombreArchivo, representantes);
        }

        private FileContentResult GenerarExcel(string nombreArchivo, IEnumerable<Representantes> representantes)
        {
            DataTable dataTable = new("Representantes");
            dataTable.Columns.AddRange([
                new("NombreCompleto"),
                new("FechaNacimiento"),
                new("Identificacion"),
                new("Mail"),
                new("Telefono"),
                new("Ciudades")
            ]);

            foreach (var representante in representantes)
            {
                dataTable.Rows.Add(
                    representante.NombreCompleto,
                    representante.FechaNacimiento,
                    representante.Identificacion,
                    representante.mail,
                    representante.Telefono,
                    representante.Ciudades?.Nombre);
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
