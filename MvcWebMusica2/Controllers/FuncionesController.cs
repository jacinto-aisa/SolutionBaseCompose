using System.Data;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MvcWebMusica2.Models;
using MvcWebMusica2.Services.Repositorio;

namespace MvcWebMusica2.Controllers
{
    public class FuncionesController(
        IGenericRepositorio<Funciones> repositorioFunciones
        ) : Controller
    {
        // GET: Funciones
        public async Task<IActionResult> Index()
        {
            var listaFunciones = await repositorioFunciones.DameTodos();
            return View(listaFunciones);
        }

        // GET: Funciones/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var funciones = await repositorioFunciones.DameUno(id);

            if (funciones == null)
            {
                return NotFound();
            }

            return View(funciones);
        }

        // GET: Funciones/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Funciones/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre")] Funciones funciones)
        {
            if (ModelState.IsValid)
            {
                await repositorioFunciones.Agregar(funciones);
                return RedirectToAction(nameof(Index));
            }

            return View(funciones);
        }

        // GET: Funciones/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var funciones = await repositorioFunciones.DameUno(id);
            if (funciones == null)
            {
                return NotFound();
            }

            return View(funciones);
        }

        // POST: Funciones/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre")] Funciones funciones)
        {
            if (id != funciones.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await repositorioFunciones.Modificar(id, funciones);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await FuncionesExists(funciones.Id))
                    {
                        return NotFound();
                    }

                    throw;
                }
                return RedirectToAction(nameof(Index));
            }

            return View(funciones);
        }

        // GET: Funciones/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var funciones = await repositorioFunciones.DameUno(id);

            if (funciones == null)
            {
                return NotFound();
            }

            return View(funciones);
        }

        // POST: Funciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var funciones = await repositorioFunciones.DameUno(id);
            if (funciones != null)
            {
                await repositorioFunciones.Borrar(id);
            }

            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> FuncionesExists(int id)
        {
            var lista = await repositorioFunciones.DameTodos();
            return lista.Exists(e => e.Id == id);
        }

        [HttpGet]
        public async Task<FileResult> DescargarExcel()
        {
            var funciones = await repositorioFunciones.DameTodos();
            var nombreArchivo = "Funciones.xlsx";
            return GenerarExcel(nombreArchivo, funciones);
        }

        private FileContentResult GenerarExcel(string nombreArchivo, IEnumerable<Funciones> funciones)
        {
            DataTable dataTable = new("Funciones");
            dataTable.Columns.AddRange([
                new("Nombre")
            ]);

            foreach (var funcion in funciones)
            {
                dataTable.Rows.Add(
                    funcion.Nombre);
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
