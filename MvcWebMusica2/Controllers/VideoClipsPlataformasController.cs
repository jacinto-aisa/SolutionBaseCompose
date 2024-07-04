using System.Data;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MvcWebMusica2.Models;
using MvcWebMusica2.Services.Repositorio;

namespace MvcWebMusica2.Controllers
{
    public class VideoClipsPlataformasController(
        IGenericRepositorio<VideoClipsPlataformas> repositorioVideoClipsPlataformas,
        IGenericRepositorio<Plataformas> repositorioPlataformas,
        IGenericRepositorio<VideoClips> repositorioVideoClips,
        IGenericRepositorio<Canciones> repositorioCanciones
        ) : Controller
    {
        // GET: VideoClipsPlataformas
        public async Task<IActionResult> Index()
        {
            var listaVideoClipsPlataformas = await repositorioVideoClipsPlataformas.DameTodos();
            foreach (var videoClipPlataformas in listaVideoClipsPlataformas)
            {
                videoClipPlataformas.Plataformas = await repositorioPlataformas.DameUno(videoClipPlataformas.PlataformasId);
                videoClipPlataformas.VideoClips = await repositorioVideoClips.DameUno(videoClipPlataformas.VideoClipsId);
                videoClipPlataformas.VideoClips!.Canciones = await repositorioCanciones.DameUno(videoClipPlataformas.VideoClips.CancionesId);
            }
            return View(listaVideoClipsPlataformas);
        }

        // GET: VideoClipsPlataformas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var videoClipsPlataformas = await repositorioVideoClipsPlataformas.DameUno(id);

            if (videoClipsPlataformas == null)
            {
                return NotFound();
            }

            videoClipsPlataformas.Plataformas = await repositorioPlataformas.DameUno(videoClipsPlataformas.PlataformasId);
            videoClipsPlataformas.VideoClips = await repositorioVideoClips.DameUno(videoClipsPlataformas.VideoClipsId);
            videoClipsPlataformas.VideoClips!.Canciones = await repositorioCanciones.DameUno(videoClipsPlataformas.VideoClips.CancionesId);

            return View(videoClipsPlataformas);
        }

        // GET: VideoClipsPlataformas/Create
        public async Task<IActionResult> CreateAsync()
        {

            var listaVideoClips = await repositorioVideoClips.DameTodos();
            foreach (var item in listaVideoClips)
            {
                item.Canciones = await repositorioCanciones.DameUno(item.CancionesId);
            }

            ViewData["PlataformasId"] = new SelectList(await repositorioPlataformas.DameTodos(), "Id", "Nombre");
            ViewData["VideoClipsId"] = new SelectList(listaVideoClips, "Id", "Canciones.Titulo");
            return View();
        }

        // POST: VideoClipsPlataformas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PlataformasId,VideoClipsId,url")] VideoClipsPlataformas videoClipsPlataformas)
        {
            if (ModelState.IsValid)
            {
                await repositorioVideoClipsPlataformas.Agregar(videoClipsPlataformas);
                return RedirectToAction(nameof(Index));
            }

            var listaVideoClips = await repositorioVideoClips.DameTodos();
            foreach (var item in listaVideoClips)
            {
                item.Canciones = await repositorioCanciones.DameUno(item.CancionesId);
            }
            ViewData["PlataformasId"] = new SelectList(await repositorioPlataformas.DameTodos(), "Id", "Nombre", videoClipsPlataformas.PlataformasId);
            ViewData["VideoClipsId"] = new SelectList(listaVideoClips, "Id", "Canciones.Titulo", videoClipsPlataformas.VideoClipsId);
            return View(videoClipsPlataformas);
        }

        // GET: VideoClipsPlataformas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var videoClipsPlataformas = await repositorioVideoClipsPlataformas.DameUno(id);
            if (videoClipsPlataformas == null)
            {
                return NotFound();
            }

            var listaVideoClips = await repositorioVideoClips.DameTodos();
            foreach (var item in listaVideoClips)
            {
                item.Canciones = await repositorioCanciones.DameUno(item.CancionesId);
            }
            ViewData["PlataformasId"] = new SelectList(await repositorioPlataformas.DameTodos(), "Id", "Nombre", videoClipsPlataformas.PlataformasId);
            ViewData["VideoClipsId"] = new SelectList(listaVideoClips, "Id", "Canciones.Titulo", videoClipsPlataformas.VideoClipsId);
            
            return View(videoClipsPlataformas);
        }

        // POST: VideoClipsPlataformas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PlataformasId,VideoClipsId,url")] VideoClipsPlataformas videoClipsPlataformas)
        {
            if (id != videoClipsPlataformas.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await repositorioVideoClipsPlataformas.Modificar(id, videoClipsPlataformas);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await VideoClipsPlataformasExists(videoClipsPlataformas.Id))
                    {
                        return NotFound();
                    }

                    throw;
                }
                return RedirectToAction(nameof(Index));
            }

            var listaVideoClips = await repositorioVideoClips.DameTodos();
            foreach (var item in listaVideoClips)
            {
                item.Canciones = await repositorioCanciones.DameUno(item.CancionesId);
            }
            ViewData["PlataformasId"] = new SelectList(await repositorioPlataformas.DameTodos(), "Id", "Nombre", videoClipsPlataformas.PlataformasId);
            ViewData["VideoClipsId"] = new SelectList(listaVideoClips, "Id", "Canciones.Titulo", videoClipsPlataformas.VideoClipsId);

            return View(videoClipsPlataformas);
        }

        // GET: VideoClipsPlataformas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var videoClipsPlataformas = await repositorioVideoClipsPlataformas.DameUno(id);

            if (videoClipsPlataformas == null)
            {
                return NotFound();
            }

            videoClipsPlataformas.Plataformas = await repositorioPlataformas.DameUno(videoClipsPlataformas.PlataformasId);
            videoClipsPlataformas.VideoClips = await repositorioVideoClips.DameUno(videoClipsPlataformas.VideoClipsId);
            videoClipsPlataformas.VideoClips!.Canciones = await repositorioCanciones.DameUno(videoClipsPlataformas.VideoClips.CancionesId);

            return View(videoClipsPlataformas);
        }

        // POST: VideoClipsPlataformas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var album = await repositorioVideoClipsPlataformas.DameUno(id);
            if (album != null)
            {
                await repositorioVideoClipsPlataformas.Borrar(id);
            }

            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> VideoClipsPlataformasExists(int id)
        {
            var lista = await repositorioVideoClipsPlataformas.DameTodos();
            return lista.Exists(e => e.Id == id);
        }

        [HttpGet]
        public async Task<FileResult> DescargarExcel()
        {
            var videoClipsPlataformas = await repositorioVideoClipsPlataformas.DameTodos();
            foreach (var videoClipPlataforma in videoClipsPlataformas)
            {
                videoClipPlataforma.Plataformas = await repositorioPlataformas.DameUno(videoClipPlataforma.PlataformasId);
                videoClipPlataforma.VideoClips = await repositorioVideoClips.DameUno(videoClipPlataforma.VideoClipsId);
            }
            var nombreArchivo = "VideoclipsPlataformas.xlsx";
            return GenerarExcel(nombreArchivo, videoClipsPlataformas);
        }

        private FileContentResult GenerarExcel(string nombreArchivo, IEnumerable<VideoClipsPlataformas> videoClipsPlataformas)
        {
            DataTable dataTable = new("VideoClipsPlataformas");
            dataTable.Columns.AddRange([
                new("Url"),
                new("Plataformas"),
                new("VideClips")
            ]);

            foreach (var videoClipPlataformas in videoClipsPlataformas)
            {
                dataTable.Rows.Add(
                    videoClipPlataformas.url,
                    videoClipPlataformas.Plataformas?.Nombre,
                    videoClipPlataformas.VideoClips?.Id);
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
