
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoldeMVC_Core.Data;
using MoldeMVC_Core.Models;
using MongoDB.Bson;
using MongoDB.Driver;

public class MedicosController : Controller
{
    private readonly VerisMongoContext _context;

    public MedicosController(VerisMongoContext context)
    {
        _context = context;
    }

    // GET: MEDICOSS
    public async Task<IActionResult> Index()
    {
        var medicos = await _context.Medicos
            .Find(Builders<Medicos>.Filter.Empty)
                .ToListAsync();

        return View(medicos);
    }

    // GET: MEDICOSS/Details/5
    public async Task<IActionResult> Details(string id)
    {
        if (string.IsNullOrEmpty(id) || !ObjectId.TryParse(id, out _))
        {
            return NotFound();
        }

        var medicos = await _context.Medicos
            .Find(m => m._id == id)
            .FirstOrDefaultAsync();

        if (medicos == null)
        {
            return NotFound();
        }

        return View(medicos);
    }

    // GET: MEDICOSS/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: MEDICOSS/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("nombre,especialidadId,foto")] Medicos medicos)
    {
        medicos._id = ObjectId.GenerateNewId().ToString();

        ModelState.Remove("_id");

        if (!ModelState.IsValid)
        {
            return View(medicos);
        }
        // Insertar en la BD
        try
        {
            await _context.Medicos.InsertOneAsync(medicos);
            return RedirectToAction(nameof(Index));
        }
        catch (MongoWriteException ex)
        {
            ModelState.AddModelError("", "Error al guardar en MongoDB: " + ex.Message);
            return View(medicos);
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", "Error inesperado: " + ex.Message);
            return View(medicos);

        }
    }

    // GET: MEDICOSS/Edit/5
    public async Task<IActionResult> Edit(string id)
    {
        if (string.IsNullOrEmpty(id) || !ObjectId.TryParse(id, out _))
        {
            return NotFound();
        }

        var medicos = await _context.Medicos
            .Find(m => m._id == id)
            .FirstOrDefaultAsync();

        if (medicos == null)
        {
            return NotFound();
        }
        return View(medicos);
    }

    // POST: MEDICOSS/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(string id, [Bind("nombre,especialidadId,foto")] Medicos medicos)
    {
        if (string.IsNullOrEmpty(id) || !ObjectId.TryParse(id, out _))
        {
            return NotFound();
        }

        if (id != medicos._id)
        {
            return NotFound();
        }

        ModelState.Remove("_id");

        if (!ModelState.IsValid)
        {
            return View(medicos);
        }

        try
        {
            // Validar que no exista otro médico con el mismo nombre (excluyendo el actual)
            var nombreUsadoPorOtro = await _context.Medicos
                .Find(m => m.nombre == medicos.nombre && m._id != medicos._id)
                .AnyAsync();

            if (nombreUsadoPorOtro)
            {
                ModelState.AddModelError("nombre", "Ya existe otro médico registrado con este nombre.");
                return View(medicos);
            }

            var resultado = await _context.Medicos
                .ReplaceOneAsync(m => m._id == id, medicos);
            if (resultado.MatchedCount == 0)
            {
                return NotFound();
            }

            return RedirectToAction(nameof(Index));
        }
        catch (MongoWriteException ex)
        {
            ModelState.AddModelError("", "Error al actualizar en MongoDB: " + ex.Message);
            return View(medicos);
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", "Error inesperado: " + ex.Message);
            return View(medicos);
        }

    }

    // GET: MEDICOSS/Delete/5
    public async Task<IActionResult> Delete(string id)
    {
        if (string.IsNullOrEmpty(id) || !ObjectId.TryParse(id, out _))
        {
            return NotFound();
        }

        var medicos = await _context.Medicos
            .Find(m => m._id == id)
            .FirstOrDefaultAsync();

        if (medicos == null)
        {
            return NotFound();
        }

        return View(medicos);
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

        var resultado = await _context.Medicos
            .DeleteOneAsync(m => m._id == id);

        if (resultado.DeletedCount == 0)
        {
            return NotFound();
        }
        return RedirectToAction(nameof(Index));
    }

}
