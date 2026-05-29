using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MoldeMVC_Core.Data;
using MoldeMVC_Core.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System.IO;
using System.Linq;

public class MedicosController : Controller
{
    private readonly VerisMongoContext _context;
    private readonly IWebHostEnvironment _env;

    public MedicosController(VerisMongoContext context, IWebHostEnvironment env)
    {
        _context = context;
        _env = env;
    }

    private async Task PopularSelectListsAsync(string? especialidadId = null)
    {
        var especialidades = await _context.Especialidades
            .Find(_ => true)
            .SortBy(e => e.Descripcion)
            .ToListAsync();

        ViewBag.EspecialidadId = new SelectList(especialidades, nameof(Especialidades.Id), nameof(Especialidades.Descripcion), especialidadId);
    }

    private async Task<string> GuardarFotoAsync(IFormFile? fotoFile, string subcarpeta, string? fotoActual = null)
    {
        if (fotoFile == null || fotoFile.Length == 0)
        {
            return fotoActual ?? string.Empty;
        }

        var extensionesPermitidas = new[] { ".jpg", ".jpeg", ".png", ".gif" };
        var extension = Path.GetExtension(fotoFile.FileName).ToLowerInvariant();
        if (!extensionesPermitidas.Contains(extension))
        {
            throw new InvalidOperationException("Solo se permiten imágenes JPG, PNG o GIF.");
        }

        if (fotoFile.Length > 2 * 1024 * 1024)
        {
            throw new InvalidOperationException("La imagen no debe superar 2 MB.");
        }

        var nombreArchivo = $"{Guid.NewGuid()}{extension}";
        var carpeta = Path.Combine(_env.WebRootPath, "imaganes", subcarpeta);
        Directory.CreateDirectory(carpeta);

        var rutaCompleta = Path.Combine(carpeta, nombreArchivo);
        using (var stream = new FileStream(rutaCompleta, FileMode.Create))
        {
            await fotoFile.CopyToAsync(stream);
        }

        if (!string.IsNullOrEmpty(fotoActual))
        {
            var rutaAnterior = Path.Combine(carpeta, fotoActual);
            if (System.IO.File.Exists(rutaAnterior))
            {
                System.IO.File.Delete(rutaAnterior);
            }
        }

        return nombreArchivo;
    }

    // GET: MEDICOSS
    public async Task<IActionResult> Index()
    {
        var especialidades = await _context.Especialidades
            .Find(_ => true)
            .ToListAsync();

        var medicos = await _context.Medicos
            .Find(Builders<Medicos>.Filter.Empty)
            .SortBy(m => m.Nombre)
            .ToListAsync();

        medicos.ForEach(m =>
            m.EspecialidadNombre = especialidades.FirstOrDefault(e => e.Id == m.EspecialidadId)?.Descripcion ?? "-");

        return View(medicos);
    }

    // GET: MEDICOSS/Details/5
    public async Task<IActionResult> Details(string id)
    {
        if (string.IsNullOrEmpty(id) || !ObjectId.TryParse(id, out _))
        {
            return NotFound();
        }

        var medico = await _context.Medicos
            .Find(m => m.Id == id)
            .FirstOrDefaultAsync();

        if (medico == null)
        {
            return NotFound();
        }

        var especialidad = await _context.Especialidades
            .Find(e => e.Id == medico.EspecialidadId)
            .FirstOrDefaultAsync();

        medico.EspecialidadNombre = especialidad?.Descripcion ?? "-";

        return View(medico);
    }

    // GET: MEDICOSS/Create
    public async Task<IActionResult> Create()
    {
        await PopularSelectListsAsync();
        return View();
    }

    // POST: MEDICOSS/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Medicos medicos)
    {
        medicos.Id = ObjectId.GenerateNewId().ToString();

        if (!ModelState.IsValid)
        {
            await PopularSelectListsAsync(medicos.EspecialidadId);
            return View(medicos);
        }

        try
        {
            medicos.Foto = await GuardarFotoAsync(medicos.FotoFile, "medicos");
            await _context.Medicos.InsertOneAsync(medicos);
            return RedirectToAction(nameof(Index));
        }
        catch (InvalidOperationException ex)
        {
            ModelState.AddModelError(nameof(Medicos.FotoFile), ex.Message);
        }
        catch (MongoWriteException ex)
        {
            ModelState.AddModelError(string.Empty, "Error al guardar en MongoDB: " + ex.Message);
        }
        catch (Exception ex)
        {
            ModelState.AddModelError(string.Empty, "Error inesperado: " + ex.Message);
        }

        await PopularSelectListsAsync(medicos.EspecialidadId);
        return View(medicos);
    }

    // GET: MEDICOSS/Edit/5
    public async Task<IActionResult> Edit(string id)
    {
        if (string.IsNullOrEmpty(id) || !ObjectId.TryParse(id, out _))
        {
            return NotFound();
        }

        var medico = await _context.Medicos
            .Find(m => m.Id == id)
            .FirstOrDefaultAsync();

        if (medico == null)
        {
            return NotFound();
        }

        await PopularSelectListsAsync(medico.EspecialidadId);
        return View(medico);
    }

    // POST: MEDICOSS/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(string id, Medicos medicos)
    {
        if (string.IsNullOrEmpty(id) || !ObjectId.TryParse(id, out _))
        {
            return NotFound();
        }

        if (id != medicos.Id)
        {
            return NotFound();
        }

        var medicoActual = await _context.Medicos
            .Find(m => m.Id == id)
            .FirstOrDefaultAsync();

        if (medicoActual == null)
        {
            return NotFound();
        }

        medicos.Foto = medicoActual.Foto;

        if (!ModelState.IsValid)
        {
            await PopularSelectListsAsync(medicos.EspecialidadId);
            return View(medicos);
        }

        try
        {
            medicos.Foto = await GuardarFotoAsync(medicos.FotoFile, "medicos", medicoActual.Foto);

            var resultado = await _context.Medicos
                .ReplaceOneAsync(m => m.Id == id, medicos);

            if (resultado.MatchedCount == 0)
            {
                return NotFound();
            }

            return RedirectToAction(nameof(Index));
        }
        catch (InvalidOperationException ex)
        {
            ModelState.AddModelError(nameof(Medicos.FotoFile), ex.Message);
            medicos.Foto = medicoActual.Foto;
        }
        catch (MongoWriteException ex)
        {
            ModelState.AddModelError(string.Empty, "Error al actualizar en MongoDB: " + ex.Message);
            medicos.Foto = medicoActual.Foto;
        }
        catch (Exception ex)
        {
            ModelState.AddModelError(string.Empty, "Error inesperado: " + ex.Message);
            medicos.Foto = medicoActual.Foto;
        }

        await PopularSelectListsAsync(medicos.EspecialidadId);
        return View(medicos);
    }

    // GET: MEDICOSS/Delete/5
    public async Task<IActionResult> Delete(string id)
    {
        if (string.IsNullOrEmpty(id) || !ObjectId.TryParse(id, out _))
        {
            return NotFound();
        }

        var medico = await _context.Medicos
            .Find(m => m.Id == id)
            .FirstOrDefaultAsync();

        if (medico == null)
        {
            return NotFound();
        }

        var especialidad = await _context.Especialidades
            .Find(e => e.Id == medico.EspecialidadId)
            .FirstOrDefaultAsync();

        medico.EspecialidadNombre = especialidad?.Descripcion ?? "-";

        return View(medico);
    }

    // POST: MEDICOSS/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(string id)
    {
        if (string.IsNullOrEmpty(id) || !ObjectId.TryParse(id, out _))
        {
            return NotFound();
        }

        var medico = await _context.Medicos
            .Find(m => m.Id == id)
            .FirstOrDefaultAsync();

        if (medico == null)
        {
            return NotFound();
        }

        var resultado = await _context.Medicos
            .DeleteOneAsync(m => m.Id == id);

        if (resultado.DeletedCount == 0)
        {
            return NotFound();
        }

        if (!string.IsNullOrWhiteSpace(medico.Foto))
        {
            var rutaFoto = Path.Combine(_env.WebRootPath, "imaganes", "medicos", medico.Foto);
            if (System.IO.File.Exists(rutaFoto))
            {
                System.IO.File.Delete(rutaFoto);
            }
        }

        return RedirectToAction(nameof(Index));
    }
}
