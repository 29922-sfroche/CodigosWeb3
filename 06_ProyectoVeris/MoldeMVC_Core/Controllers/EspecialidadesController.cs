
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoldeMVC_Core.Data;
using MoldeMVC_Core.Models;
using MongoDB.Bson;
using MongoDB.Driver;

public class EspecialidadesController : Controller
{
    private readonly VerisMongoContext _context;

    public EspecialidadesController(VerisMongoContext context)
    {
        _context = context;
    }

    // GET: ESPECIALIDADESS
    public async Task<IActionResult> Index()
    {
        var especialidades = await _context.Especialidades.Find(Builders<Especialidades>.Filter.Empty).ToListAsync();

        return View(especialidades);
    }

    // GET: ESPECIALIDADESS/Details/5
    public async Task<IActionResult> Details(string id)
    {
        if (string.IsNullOrEmpty(id) || !ObjectId.TryParse(id, out _))
        {
            return NotFound();
        }

        var especialidades = await _context.Especialidades.Find(e => e._id == id).FirstOrDefaultAsync();

        if (especialidades == null)
        {
            return NotFound();
        }

        return View(especialidades);
    }

    // GET: ESPECIALIDADESS/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: ESPECIALIDADESS/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("descripcion,dias,franjaHI,franjaHF")] Especialidades especialidades)
    {

        especialidades._id = ObjectId.GenerateNewId().ToString();

        ModelState.Remove("_id"); // Elimina la validación para _id, ya que se genera automáticamente


        if (ModelState.IsValid)
        {
            return View(especialidades);
        }

        try
        {
            var existeConsulta = await _context.Especialidades.Find(e => e.descripcion == especialidades.descripcion).FirstOrDefaultAsync();

            if (existeConsulta != null)
            {
                ModelState.AddModelError("descripcion", "Ya existe una especialidad con esa descripción.");
                return View(especialidades);
            }

            await _context.Especialidades.InsertOneAsync(especialidades);

            return RedirectToAction(nameof(Index));

        }
        catch (MongoWriteException ex)
        {
            ModelState.AddModelError(string.Empty, $"Ocurrió un error al crear la especialidad: {ex.Message}");
            return View(especialidades);
        }
        catch (Exception ex)
        {
            ModelState.AddModelError(string.Empty, $"Ocurrió un error inesperado: {ex.Message}");
            return View(especialidades);
        }
    }

    // GET: ESPECIALIDADESS/Edit/5
    public async Task<IActionResult> Edit(string id)
    {
        if (string.IsNullOrEmpty(id) || !ObjectId.TryParse(id, out _))
        {
            return NotFound();
        }

        var especialidades = await _context.Especialidades.Find(e => e._id == id).FirstOrDefaultAsync();

        if (especialidades == null)
        {
            return NotFound();
        }
        return View(especialidades);
    }

    // POST: ESPECIALIDADESS/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(string id, [Bind("descripcion,dias,franjaHI,franjaHF")] Especialidades especialidades)
    {

        if (string.IsNullOrEmpty(id) || !ObjectId.TryParse(id, out _))
        {
            return NotFound();
        }

        if (id != especialidades._id)
        {
            return NotFound();
        }

        ModelState.Remove("_id"); // Elimina la validación para _id, ya que no se está editando

        if (!ModelState.IsValid)
        {
            return View(especialidades);
        }

        try
        {
            var existeConsulta = await _context.Especialidades.Find(e => e.descripcion == especialidades.descripcion && e._id != id).FirstOrDefaultAsync();

            if (existeConsulta != null)
            {
                ModelState.AddModelError("descripcion", "Ya existe una especialidad con esa descripción.");
                return View(especialidades);
            }

            var updateResult = await _context.Especialidades.ReplaceOneAsync(e => e._id == id, especialidades);
            if (updateResult.MatchedCount == 0)
            {
                return NotFound();
            }
            return RedirectToAction(nameof(Index));
        }
        catch (MongoWriteException ex)
        {
            ModelState.AddModelError(string.Empty, $"Ocurrió un error al actualizar la especialidad: {ex.Message}");
            return View(especialidades);
        }
        catch (Exception ex)
        {
            ModelState.AddModelError(string.Empty, $"Ocurrió un error inesperado: {ex.Message}");
            return View(especialidades);
        }
    }

    // GET: ESPECIALIDADESS/Delete/5
    public async Task<IActionResult> Delete(string id)
    {
        if (string.IsNullOrEmpty(id) || !ObjectId.TryParse(id, out _))
        {
            return NotFound();
        }

        var especialidades = await _context.Especialidades.Find(e => e._id == id).FirstOrDefaultAsync();

        if (especialidades == null)
        {
            return NotFound();
        }

        return View(especialidades);
    }

    // POST: ESPECIALIDADESS/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(string id)
    {
        if (string.IsNullOrEmpty(id) || !ObjectId.TryParse(id, out _))
        {
            return NotFound();
        }

        var especialidades = await _context.Especialidades.DeleteOneAsync(e => e._id == id);

        if (especialidades.DeletedCount == 0)
        {
            return NotFound();
        }

        return RedirectToAction(nameof(Index));

    }

    //private bool EspecialidadesExists(string? _id)
    //{
    //    return _context.Especialidades.Any(e => e._id == _id);
    //}

}
