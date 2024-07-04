using System.Data;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MvcWebMusica2.Models;
using MvcWebMusica2.Services.Repositorio;

namespace MvcWebMusica2.Controllers
{
    public class VideoClipsController(
        IGenericRepositorio<VideoClips> repositorioVideoClips,
        IGenericRepositorio<Canciones> repositorioCanciones
        ) : Controller
    {
        private readonly string _cancionesId = "CancionesId";
        private readonly string _titulo = "Titulo";

        // GET: VideoClips
        public async Task<IActionResult> Index()
        {
            var listaVideoClips = await repositorioVideoClips.DameTodos();
            foreach (var videoClip in listaVideoClips)
            {
                videoClip.Canciones = await repositorioCanciones.DameUno(videoClip.CancionesId);
            }
            return View(listaVideoClips);
        }

        // GET: VideoClips/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var videoClips = await repositorioVideoClips.DameUno(id);

            if (videoClips == null)
            {
                return NotFound();
            }

            videoClips.Canciones = await repositorioCanciones.DameUno(videoClips.CancionesId);

            return View(videoClips);
        }

        // GET: VideoClips/Create
        public async Task<IActionResult> CreateAsync()
        {
            ViewData[_cancionesId] = new SelectList(await repositorioCanciones.DameTodos(), "Id", _titulo);
            return View();
        }

        // POST: VideoClips/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CancionesId,Fecha")] VideoClips videoClips)
        {
            if (ModelState.IsValid)
            {
                await repositorioVideoClips.Agregar(videoClips);
                return RedirectToAction(nameof(Index));
            }
            ViewData[_cancionesId] = new SelectList(await repositorioCanciones.DameTodos(), "Id", _titulo, videoClips.CancionesId);
            return View(videoClips);
        }

        // GET: VideoClips/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var videoClips = await repositorioVideoClips.DameUno(id);
            if (videoClips == null)
            {
                return NotFound();
            }
            ViewData[_cancionesId] = new SelectList(await repositorioCanciones.DameTodos(), "Id", _titulo, videoClips.CancionesId);
            return View(videoClips);
        }

        // POST: VideoClips/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CancionesId,Fecha")] VideoClips videoClips)
        {
            if (id != videoClips.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await repositorioVideoClips.Modificar(id, videoClips);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await VideoClipsExists(videoClips.Id))
                    {
                        return NotFound();
                    }

                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData[_cancionesId] = new SelectList(await repositorioCanciones.DameTodos(), "Id", _titulo, videoClips.CancionesId);
            return View(videoClips);
        }

        // GET: VideoClips/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var videoClips = await repositorioVideoClips.DameUno(id);
            if (videoClips == null)
            {
                return NotFound();
            }

            videoClips.Canciones = await repositorioCanciones.DameUno(videoClips.CancionesId);

            return View(videoClips);
        }

        // POST: VideoClips/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var videoClips = await repositorioVideoClips.DameUno(id);
            if (videoClips != null)
            {
                await repositorioVideoClips.Borrar(id);
            }

            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> VideoClipsExists(int id)
        {
            var lista = await repositorioVideoClips.DameTodos();
            return lista.Exists(e => e.Id == id);
        }

        [HttpGet]
        public async Task<FileResult> DescargarExcel()
        {
            var videoClips = await repositorioVideoClips.DameTodos();
            foreach (var videoClip in videoClips)
            {
                videoClip.Canciones = await repositorioCanciones.DameUno(videoClip.CancionesId);
            }
            var nombreArchivo = "Videoclips.xlsx";
            return GenerarExcel(nombreArchivo, videoClips);
        }

        private FileContentResult GenerarExcel(string nombreArchivo, IEnumerable<VideoClips> videoClips)
        {
            DataTable dataTable = new("VideoClips");
            dataTable.Columns.AddRange([
                new("Fecha"),
                new("Canciones")
            ]);

            foreach (var videoClip in videoClips)
            {
                dataTable.Rows.Add(
                    videoClip.Fecha,
                    videoClip.Canciones?.Titulo);
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
