
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoldeMVC_Core.Data;
using MoldeMVC_Core.Models;
using MongoDB.Bson;
using MongoDB.Driver;

public class RecetasController : Controller
{
    private readonly VerisMongoContext _context;

    public RecetasController(VerisMongoContext context)
    {
        _context = context;
    }

    // GET: RECETASS
    public async Task<IActionResult> Index()
    {
        var recetas = await _context.Recetas
               .Find(Builders<Recetas>.Filter.Empty)
               .ToListAsync();

        return View(recetas);
    }

    // GET: RECETASS/Details/5
    public async Task<IActionResult> Details(string id)
    {
        if (string.IsNullOrEmpty(id) || !ObjectId.TryParse(id, out _))
        {
            return NotFound();
        }

        var recetas = await _context.Recetas
            .Find(r => r._id == id)
            .FirstOrDefaultAsync();

        if (recetas == null)
        {
            return NotFound();
        }

        return View(recetas);
    }

    // GET: RECETASS/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: RECETASS/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("consultaId,medicamentoId,cantidad")] Recetas recetas)
    {
        recetas._id = ObjectId.GenerateNewId().ToString();

        ModelState.Remove("_id");

        if (!ModelState.IsValid)
        {
            return View(recetas);
        }

        try
        {

            await _context.Recetas.InsertOneAsync(recetas);

            return RedirectToAction(nameof(Index));
        }
        catch (MongoWriteException ex)
        {
            ModelState.AddModelError("", "Error al guardar en MongoDB: " + ex.Message);
            return View(recetas);
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", "Error inesperado: " + ex.Message);
            return View(recetas);
        }

    }

    // GET: RECETASS/Edit/5
    public async Task<IActionResult> Edit(string id)
    {
        if (string.IsNullOrEmpty(id) || !ObjectId.TryParse(id, out _))
        {
            return NotFound();
        }

        var recetas = await _context.Recetas
            .Find(r => r._id == id)
            .FirstOrDefaultAsync();

        if (recetas == null)
        {
            return NotFound();
        }

        return View(recetas);
    }

    // POST: RECETASS/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(string id, [Bind("consultaId,medicamentoId,cantidad")] Recetas recetas)
    {
        if (string.IsNullOrEmpty(id) || !ObjectId.TryParse(id, out _))
        {
            return NotFound();
        }

        if (id != recetas._id)
        {
            return NotFound();
        }

        ModelState.Remove("_id");

        if (!ModelState.IsValid)
        {
            return View(recetas);
        }

        try
        {
            var resultado = await _context.Recetas
                .ReplaceOneAsync(p => p._id == id, recetas);

            if (resultado.MatchedCount == 0)
            {
                return NotFound();
            }

            return RedirectToAction(nameof(Index));
        }
        catch (MongoWriteException ex)
        {
            ModelState.AddModelError("", "Error al actualizar en MongoDB: " + ex.Message);
            return View(recetas);
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", "Error inesperado: " + ex.Message);
            return View(recetas);
        }
    }

    // GET: RECETASS/Delete/5
    public async Task<IActionResult> Delete(string id)
    {
        if (string.IsNullOrEmpty(id) || !ObjectId.TryParse(id, out _))
        {
            return NotFound();
        }

        var recetas = await _context.Recetas
            .Find(r => r._id == id)
            .FirstOrDefaultAsync();

        if (recetas == null)
        {
            return NotFound();
        }
        return View(recetas);
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
            .DeleteOneAsync(r=> r._id == id);

        if (resultado.DeletedCount == 0)
        {
            return NotFound();
        }

        return RedirectToAction(nameof(Index));

    }

}
