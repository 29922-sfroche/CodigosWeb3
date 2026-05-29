using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MoldeMVC_Core.Data;
using MoldeMVC_Core.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Linq;

public class RecetasController : Controller
{
    private readonly VerisMongoContext _context;

    public RecetasController(VerisMongoContext context)
    {
        _context = context;
    }

    private async Task PopularSelectListsAsync(string? consultaId = null, string? medicamentoId = null)
    {
        var consultas = await _context.Consultas
            .Find(_ => true)
            .SortByDescending(c => c.FechaConsulta)
            .ToListAsync();

        var medicamentos = await _context.Medicamentos
            .Find(_ => true)
            .SortBy(m => m.Nombre)
            .ToListAsync();

        var consultasSelect = consultas.Select(c => new
        {
            c.Id,
            Descripcion = $"{c.FechaConsulta:dd/MM/yyyy} – {c.Diagnostico}"
        });

        ViewBag.ConsultaId = new SelectList(consultasSelect, "Id", "Descripcion", consultaId);
        ViewBag.MedicamentoId = new SelectList(medicamentos, nameof(Medicamentos.Id), nameof(Medicamentos.Nombre), medicamentoId);
    }

    // GET: RECETASS
    public async Task<IActionResult> Index()
    {
        var consultas = await _context.Consultas.Find(_ => true).ToListAsync();
        var medicamentos = await _context.Medicamentos.Find(_ => true).ToListAsync();

        var recetas = await _context.Recetas
            .Find(Builders<Recetas>.Filter.Empty)
            .ToListAsync();

        recetas.ForEach(r =>
        {
            var consulta = consultas.FirstOrDefault(c => c.Id == r.ConsultaId);
            var medicamento = medicamentos.FirstOrDefault(m => m.Id == r.MedicamentoId);
            r.ConsultaDescripcion = consulta == null
                ? "-"
                : $"{consulta.FechaConsulta:dd/MM/yyyy} – {consulta.Diagnostico}";
            r.MedicamentoNombre = medicamento?.Nombre ?? "-";
        });

        return View(recetas);
    }

    // GET: RECETASS/Details/5
    public async Task<IActionResult> Details(string id)
    {
        if (string.IsNullOrEmpty(id) || !ObjectId.TryParse(id, out _))
        {
            return NotFound();
        }

        var receta = await _context.Recetas
            .Find(r => r.Id == id)
            .FirstOrDefaultAsync();

        if (receta == null)
        {
            return NotFound();
        }

        var consulta = await _context.Consultas.Find(c => c.Id == receta.ConsultaId).FirstOrDefaultAsync();
        var medicamento = await _context.Medicamentos.Find(m => m.Id == receta.MedicamentoId).FirstOrDefaultAsync();
        receta.ConsultaDescripcion = consulta == null
            ? "-"
            : $"{consulta.FechaConsulta:dd/MM/yyyy} – {consulta.Diagnostico}";
        receta.MedicamentoNombre = medicamento?.Nombre ?? "-";

        return View(receta);
    }

    // GET: RECETASS/Create
    public async Task<IActionResult> Create()
    {
        await PopularSelectListsAsync();
        return View();
    }

    // POST: RECETASS/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Recetas recetas)
    {
        recetas.Id = ObjectId.GenerateNewId().ToString();

        if (recetas.Cantidad < 1)
        {
            ModelState.AddModelError(nameof(Recetas.Cantidad), "La cantidad debe ser al menos 1.");
        }

        if (!ModelState.IsValid)
        {
            await PopularSelectListsAsync(recetas.ConsultaId, recetas.MedicamentoId);
            return View(recetas);
        }

        try
        {
            await _context.Recetas.InsertOneAsync(recetas);
            return RedirectToAction(nameof(Index));
        }
        catch (MongoWriteException ex)
        {
            ModelState.AddModelError(string.Empty, "Error al guardar en MongoDB: " + ex.Message);
        }
        catch (Exception ex)
        {
            ModelState.AddModelError(string.Empty, "Error inesperado: " + ex.Message);
        }

        await PopularSelectListsAsync(recetas.ConsultaId, recetas.MedicamentoId);
        return View(recetas);
    }

    // GET: RECETASS/Edit/5
    public async Task<IActionResult> Edit(string id)
    {
        if (string.IsNullOrEmpty(id) || !ObjectId.TryParse(id, out _))
        {
            return NotFound();
        }

        var receta = await _context.Recetas
            .Find(r => r.Id == id)
            .FirstOrDefaultAsync();

        if (receta == null)
        {
            return NotFound();
        }

        await PopularSelectListsAsync(receta.ConsultaId, receta.MedicamentoId);
        return View(receta);
    }

    // POST: RECETASS/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(string id, Recetas recetas)
    {
        if (string.IsNullOrEmpty(id) || !ObjectId.TryParse(id, out _))
        {
            return NotFound();
        }

        if (id != recetas.Id)
        {
            return NotFound();
        }

        if (recetas.Cantidad < 1)
        {
            ModelState.AddModelError(nameof(Recetas.Cantidad), "La cantidad debe ser al menos 1.");
        }

        if (!ModelState.IsValid)
        {
            await PopularSelectListsAsync(recetas.ConsultaId, recetas.MedicamentoId);
            return View(recetas);
        }

        try
        {
            var resultado = await _context.Recetas
                .ReplaceOneAsync(p => p.Id == id, recetas);

            if (resultado.MatchedCount == 0)
            {
                return NotFound();
            }

            return RedirectToAction(nameof(Index));
        }
        catch (MongoWriteException ex)
        {
            ModelState.AddModelError(string.Empty, "Error al actualizar en MongoDB: " + ex.Message);
        }
        catch (Exception ex)
        {
            ModelState.AddModelError(string.Empty, "Error inesperado: " + ex.Message);
        }

        await PopularSelectListsAsync(recetas.ConsultaId, recetas.MedicamentoId);
        return View(recetas);
    }

    // GET: RECETASS/Delete/5
    public async Task<IActionResult> Delete(string id)
    {
        if (string.IsNullOrEmpty(id) || !ObjectId.TryParse(id, out _))
        {
            return NotFound();
        }

        var receta = await _context.Recetas
            .Find(r => r.Id == id)
            .FirstOrDefaultAsync();

        if (receta == null)
        {
            return NotFound();
        }

        var consulta = await _context.Consultas.Find(c => c.Id == receta.ConsultaId).FirstOrDefaultAsync();
        var medicamento = await _context.Medicamentos.Find(m => m.Id == receta.MedicamentoId).FirstOrDefaultAsync();
        receta.ConsultaDescripcion = consulta == null
            ? "-"
            : $"{consulta.FechaConsulta:dd/MM/yyyy} – {consulta.Diagnostico}";
        receta.MedicamentoNombre = medicamento?.Nombre ?? "-";

        return View(receta);
    }

    // POST: RECETASS/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(string id)
    {
        if (string.IsNullOrEmpty(id) || !ObjectId.TryParse(id, out _))
        {
            return NotFound();
        }

        var resultado = await _context.Recetas
            .DeleteOneAsync(r => r.Id == id);

        if (resultado.DeletedCount == 0)
        {
            return NotFound();
        }

        return RedirectToAction(nameof(Index));
    }
}
