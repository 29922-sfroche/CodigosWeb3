using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoldeMVC_Core.Data;
using MoldeMVC_Core.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Linq;

public class MedicamentosController : Controller
{
    private readonly VerisMongoContext _context;

    public MedicamentosController(VerisMongoContext context)
    {
        _context = context;
    }

    // GET: MEDICAMENTOSS
    public async Task<IActionResult> Index()
    {
        var medicamentos = await _context.Medicamentos
            .Find(Builders<Medicamentos>.Filter.Empty)
            .SortBy(m => m.Nombre)
            .ToListAsync();

        return View(medicamentos);
    }

    // GET: MEDICAMENTOSS/Details/5
    public async Task<IActionResult> Details(string id)
    {
        if (string.IsNullOrEmpty(id) || !ObjectId.TryParse(id, out _))
        {
            return NotFound();
        }

        var medicamento = await _context.Medicamentos
            .Find(m => m.Id == id)
            .FirstOrDefaultAsync();

        if (medicamento == null)
        {
            return NotFound();
        }

        return View(medicamento);
    }

    // GET: MEDICAMENTOSS/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: MEDICAMENTOSS/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Medicamentos medicamentos)
    {
        medicamentos.Id = ObjectId.GenerateNewId().ToString();

        if (!ModelState.IsValid)
        {
            return View(medicamentos);
        }

        try
        {
            var existeMedicamento = await _context.Medicamentos
                .Find(p => p.Nombre == medicamentos.Nombre && p.Tipo == medicamentos.Tipo)
                .AnyAsync();

            if (existeMedicamento)
            {
                ModelState.AddModelError(nameof(Medicamentos.Nombre), "Ya existe un medicamento registrado con este nombre y tipo.");
                return View(medicamentos);
            }

            await _context.Medicamentos.InsertOneAsync(medicamentos);
            return RedirectToAction(nameof(Index));
        }
        catch (MongoWriteException ex)
        {
            ModelState.AddModelError(string.Empty, "Error al guardar en MongoDB: " + ex.Message);
            return View(medicamentos);
        }
        catch (Exception ex)
        {
            ModelState.AddModelError(string.Empty, "Error inesperado: " + ex.Message);
            return View(medicamentos);
        }
    }

    // GET: MEDICAMENTOSS/Edit/5
    public async Task<IActionResult> Edit(string id)
    {
        if (string.IsNullOrEmpty(id) || !ObjectId.TryParse(id, out _))
        {
            return NotFound();
        }

        var medicamento = await _context.Medicamentos
            .Find(m => m.Id == id)
            .FirstOrDefaultAsync();

        if (medicamento == null)
        {
            return NotFound();
        }

        return View(medicamento);
    }

    // POST: MEDICAMENTOSS/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(string id, Medicamentos medicamentos)
    {
        if (string.IsNullOrEmpty(id) || !ObjectId.TryParse(id, out _))
        {
            return NotFound();
        }

        if (id != medicamentos.Id)
        {
            return NotFound();
        }

        if (!ModelState.IsValid)
        {
            return View(medicamentos);
        }

        try
        {
            var existeMedicamento = await _context.Medicamentos
                .Find(p => p.Nombre == medicamentos.Nombre && p.Tipo == medicamentos.Tipo && p.Id != id)
                .AnyAsync();

            if (existeMedicamento)
            {
                ModelState.AddModelError(nameof(Medicamentos.Nombre), "Ya existe un medicamento registrado con este nombre y tipo.");
                return View(medicamentos);
            }

            var resultado = await _context.Medicamentos
                .ReplaceOneAsync(p => p.Id == id, medicamentos);

            if (resultado.MatchedCount == 0)
            {
                return NotFound();
            }

            return RedirectToAction(nameof(Index));
        }
        catch (MongoWriteException ex)
        {
            ModelState.AddModelError(string.Empty, "Error al actualizar en MongoDB: " + ex.Message);
            return View(medicamentos);
        }
        catch (Exception ex)
        {
            ModelState.AddModelError(string.Empty, "Error inesperado: " + ex.Message);
            return View(medicamentos);
        }
    }

    // GET: MEDICAMENTOSS/Delete/5
    public async Task<IActionResult> Delete(string id)
    {
        if (string.IsNullOrEmpty(id) || !ObjectId.TryParse(id, out _))
        {
            return NotFound();
        }

        var medicamento = await _context.Medicamentos
            .Find(m => m.Id == id)
            .FirstOrDefaultAsync();

        if (medicamento == null)
        {
            return NotFound();
        }

        return View(medicamento);
    }

    // POST: MEDICAMENTOSS/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(string id)
    {
        if (string.IsNullOrEmpty(id) || !ObjectId.TryParse(id, out _))
        {
            return NotFound();
        }

        var resultado = await _context.Medicamentos
            .DeleteOneAsync(p => p.Id == id);

        if (resultado.DeletedCount == 0)
        {
            return NotFound();
        }

        return RedirectToAction(nameof(Index));
    }
}
