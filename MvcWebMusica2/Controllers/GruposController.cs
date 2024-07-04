
﻿using System.Data;
using ClosedXML.Excel;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MvcWebMusica2.Models;
using MvcWebMusica2.Services.Repositorio;

namespace MvcWebMusica2.Controllers
{
    public class GruposController(
        IGenericRepositorio<Grupos> repositorioGrupos,
        IGenericRepositorio<Ciudades> repositorioCiudades,
        IGenericRepositorio<Generos> repositorioGeneros,
        IGenericRepositorio<Representantes> repositorioRepresentantes) 
        : Controller

    {
        // GET: Grupos
        public async Task<IActionResult> Index()
        {
            var listaGrupos = await repositorioGrupos.DameTodos();
            foreach (var grupo in listaGrupos)
            {
                grupo.Ciudades = await repositorioCiudades.DameUno(grupo.CiudadesId);
                grupo.Generos = await repositorioGeneros.DameUno(grupo.GenerosId);
                grupo.Representantes = await repositorioRepresentantes.DameUno(grupo.RepresentantesId);

            }
            return View(listaGrupos);

        }

        public async Task<IActionResult> ArtistasYGrupos()
        {

            var listaGrupos = await repositorioGrupos.DameTodos();
            return View(listaGrupos);

        }


        // GET: Grupos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var grupo = await repositorioGrupos.DameUno(id);

            if (grupo == null)

            {
                return NotFound();
            }

            grupo.Ciudades = await repositorioCiudades.DameUno(grupo.CiudadesId);
            grupo.Generos = await repositorioGeneros.DameUno(grupo.GenerosId);
            grupo.Representantes = await repositorioRepresentantes.DameUno(grupo.RepresentantesId);

            return View(grupo);
        }

        // GET: Grupos/Create
        public async Task<IActionResult> Create()
        {
            ViewData["CiudadesId"] = new SelectList(await repositorioCiudades.DameTodos(), "Id", "Nombre");
            ViewData["GenerosId"] = new SelectList(await repositorioGeneros.DameTodos(), "Id", "Nombre");
            ViewData["RepresentantesId"] = new SelectList(await repositorioRepresentantes.DameTodos(), "Id", "NombreCompleto");
            return View();
        }

        // POST: Grupos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,grupo,FechaCreacion,CiudadesId,RepresentantesId,GenerosId")] Grupos grupos)
        {
            if (ModelState.IsValid)
            {
                await repositorioGrupos.Agregar(grupos);

                return RedirectToAction(nameof(Index));
            }
            ViewData["CiudadesId"] = new SelectList(await repositorioCiudades.DameTodos(), "Id", "Nombre");
            ViewData["GenerosId"] = new SelectList(await repositorioGeneros.DameTodos(), "Id", "Nombre");
            ViewData["RepresentantesId"] = new SelectList(await repositorioRepresentantes.DameTodos(), "Id", "NombreCompleto");

            return View(grupos);
        }

        // GET: Grupos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }


            var grupo = await repositorioGrupos.DameUno(id);
            if (grupo == null)
            {
                return NotFound();
            }
            ViewData["CiudadesId"] = new SelectList(await repositorioCiudades.DameTodos(), "Id", "Nombre", grupo.CiudadesId);
            ViewData["GenerosId"] = new SelectList(await repositorioGeneros.DameTodos(), "Id", "Nombre", grupo.GenerosId);
            ViewData["RepresentantesId"] = new SelectList(await repositorioRepresentantes.DameTodos(), "Id", "NombreCompleto", grupo.RepresentantesId);
            return View(grupo);

        }

        // POST: Grupos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,grupo,FechaCreacion,CiudadesId,RepresentantesId,GenerosId")] Grupos grupos)
        {
            if (id != grupos.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await repositorioGrupos.Modificar(id, grupos);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await GruposExists(grupos.Id))
                    {
                        return NotFound();
                    }
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }

            ViewData["CiudadesId"] = new SelectList(await repositorioCiudades.DameTodos(), "Id", "Nombre", grupos.CiudadesId);
            ViewData["GenerosId"] = new SelectList(await repositorioGeneros.DameTodos(), "Id", "Nombre", grupos.GenerosId);
            ViewData["RepresentantesId"] = new SelectList(await repositorioRepresentantes.DameTodos(), "Id", "NombreCompleto", grupos.RepresentantesId);

            return View(grupos);
        }

        // GET: Grupos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }


            var grupo = await repositorioGrupos.DameUno(id);

            if (grupo == null)

            {
                return NotFound();
            }

            grupo.Ciudades = await repositorioCiudades.DameUno(grupo.CiudadesId);
            grupo.Generos = await repositorioGeneros.DameUno(grupo.GenerosId);
            grupo.Representantes = await repositorioRepresentantes.DameUno(grupo.RepresentantesId);

            return View(grupo);
        }

        // POST: Grupos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var grupos = await repositorioGrupos.DameUno(id);
            if (grupos != null)
            {
                await repositorioGrupos.Borrar(id);

            }

            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> GruposExists(int id)
        {

            var lista = await repositorioGrupos.DameTodos();
            return lista.Exists(e => e.Id == id);
        }

        [HttpGet]
        public async Task<FileResult> DescargarExcel()
        {
            var grupos = await repositorioGrupos.DameTodos();
            foreach (var grupo in grupos)
            {
                grupo.Ciudades = await repositorioCiudades.DameUno(grupo.CiudadesId);
                grupo.Generos = await repositorioGeneros.DameUno(grupo.GenerosId);
                grupo.Representantes = await repositorioRepresentantes.DameUno(grupo.RepresentantesId);
            }
            var nombreArchivo = "Grupos.xlsx";
            return GenerarExcel(nombreArchivo, grupos);
        }

        private FileContentResult GenerarExcel(string nombreArchivo, IEnumerable<Grupos> grupos)
        {
            DataTable dataTable = new("Grupos");
            dataTable.Columns.AddRange([
                new("Nombre"),
                new("FechaCreacion"),
                new("Ciudades"),
                new("Géneros"),
                new("Representantes")
            ]);

            foreach (var grupo in grupos)
            {
                dataTable.Rows.Add(
                    grupo.Nombre,
                    grupo.FechaCreacion,
                    grupo.Ciudades?.Nombre,
                    grupo.Generos?.Nombre,
                    grupo.Representantes?.NombreCompleto);
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
