
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoldeMVC_Core.Data;
using MoldeMVC_Core.Models;
using MongoDB.Bson;
using MongoDB.Driver;

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

        var medicamentos = await _context.Medicamentos
            .Find(m => m._id == id)
            .FirstOrDefaultAsync();

        if (medicamentos == null)
        {
            return NotFound();
        }
        return View(medicamentos);
    }

    // GET: MEDICAMENTOSS/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: MEDICAMENTOSS/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("nombre,tipo")] Medicamentos medicamentos)
    {
        medicamentos._id = ObjectId.GenerateNewId().ToString();

        ModelState.Remove("_id");

        if (!ModelState.IsValid)
        {
            return View(medicamentos);
        }

        try
        {
            // Validar medicamento, que no exista otro con el mismo nombre y tipo

            var existeMedicamento = await _context.Medicamentos
                .Find(p => p.nombre == medicamentos.nombre && p.tipo == medicamentos.tipo)
                .AnyAsync();

            if (existeMedicamento)
            {
                ModelState.AddModelError("nombre", "Ya existe un medicamento registrado con este nombre y tipo.");
                return View(medicamentos);
            }

            await _context.Medicamentos.InsertOneAsync(medicamentos);
            return RedirectToAction(nameof(Index));
        }
        catch (MongoWriteException ex)
        {
            ModelState.AddModelError("", "Error al guardar en MongoDB: " + ex.Message);
            return View(medicamentos);
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", "Error inesperado: " + ex.Message);
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

        var medicamentos = await _context.Medicamentos
            .Find(m => m._id == id)
            .FirstOrDefaultAsync();

        if (medicamentos == null)
        {
            return NotFound();
        }

        return View(medicamentos);
    }

    // POST: MEDICAMENTOSS/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(string id, [Bind("nombre,tipo")] Medicamentos medicamentos)
    {
        if (string.IsNullOrEmpty(id) || !ObjectId.TryParse(id, out _))
        {
            return NotFound();
        }

        if (id != medicamentos._id)
        {
            return NotFound();
        }

        ModelState.Remove("_id");

        if (!ModelState.IsValid)
        {
            return View(medicamentos);
        }

        try
        {

            // Validar medicamento, que no exista otro con el mismo nombre y tipo

            var existeMedicamento = await _context.Medicamentos
                .Find(p => p.nombre == medicamentos.nombre && p.tipo == medicamentos.tipo)
                .AnyAsync();

            if (existeMedicamento)
            {
                ModelState.AddModelError("nombre", "Ya existe un medicamento registrado con este nombre y tipo.");
                return View(medicamentos);
            }


            var resultado = await _context.Medicamentos
                .ReplaceOneAsync(p => p._id == id, medicamentos);

            if (resultado.MatchedCount == 0)
            {
                return NotFound();
            }

            return RedirectToAction(nameof(Index));
        }
        catch (MongoWriteException ex)
        {
            ModelState.AddModelError("", "Error al actualizar en MongoDB: " + ex.Message);
            return View(medicamentos);
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", "Error inesperado: " + ex.Message);
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

        var medicamentos = await _context.Medicamentos
            .Find(m => m._id == id)
            .FirstOrDefaultAsync();

        if (medicamentos == null)
        {
            return NotFound();
        }

        return View(medicamentos);
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
            .DeleteOneAsync(p => p._id == id);

        if (resultado.DeletedCount == 0)
        {
            return NotFound();
        }

        return RedirectToAction(nameof(Index));
    }

}
