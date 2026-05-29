using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MoldeMVC_Core.Data;
using MoldeMVC_Core.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Globalization;
using System.Linq;

public class EspecialidadesController : Controller
{
    private readonly VerisMongoContext _context;

    public EspecialidadesController(VerisMongoContext context)
    {
        _context = context;
    }

    private static IEnumerable<SelectListItem> ObtenerHoras()
    {
        var cultura = CultureInfo.GetCultureInfo("en-US");
        return Enumerable.Range(8, 14).Select(hora =>
        {
            var horaBase = DateTime.Today.AddHours(hora);
            return new SelectListItem
            {
                Value = horaBase.ToString("HH:mm:ss", CultureInfo.InvariantCulture),
                Text = horaBase.ToString("h:mm tt", cultura)
            };
        });
    }

    private void PopularHoras(string? franjaHI = null, string? franjaHF = null)
    {
        ViewBag.Horas = new SelectList(ObtenerHoras(), "Value", "Text", franjaHI);
        ViewBag.HorasFin = new SelectList(ObtenerHoras(), "Value", "Text", franjaHF);
    }

    private static string NormalizarHora(string? hora)
    {
        if (string.IsNullOrWhiteSpace(hora))
        {
            return string.Empty;
        }

        var formatos = new[] { "HH:mm:ss", "HH:mm", "h:mm tt", "hh:mm tt" };
        if (DateTime.TryParseExact(hora, formatos, CultureInfo.GetCultureInfo("en-US"), DateTimeStyles.None, out var fecha))
        {
            return fecha.ToString("HH:mm:ss", CultureInfo.InvariantCulture);
        }

        if (TimeSpan.TryParse(hora, out var tiempo))
        {
            return tiempo.ToString(@"hh\:mm\:ss");
        }

        return hora;
    }

    // GET: ESPECIALIDADESS
    public async Task<IActionResult> Index()
    {
        var especialidades = await _context.Especialidades
            .Find(Builders<Especialidades>.Filter.Empty)
            .SortBy(e => e.Descripcion)
            .ToListAsync();

        return View(especialidades);
    }

    // GET: ESPECIALIDADESS/Details/5
    public async Task<IActionResult> Details(string id)
    {
        if (string.IsNullOrEmpty(id) || !ObjectId.TryParse(id, out _))
        {
            return NotFound();
        }

        var especialidad = await _context.Especialidades
            .Find(e => e.Id == id)
            .FirstOrDefaultAsync();

        if (especialidad == null)
        {
            return NotFound();
        }

        return View(especialidad);
    }

    // GET: ESPECIALIDADESS/Create
    public IActionResult Create()
    {
        PopularHoras();
        return View();
    }

    // POST: ESPECIALIDADESS/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Especialidades especialidades)
    {
        especialidades.Id = ObjectId.GenerateNewId().ToString();
        especialidades.FranjaHI = NormalizarHora(especialidades.FranjaHI);
        especialidades.FranjaHF = NormalizarHora(especialidades.FranjaHF);

        if (!ModelState.IsValid)
        {
            PopularHoras(especialidades.FranjaHI, especialidades.FranjaHF);
            return View(especialidades);
        }

        try
        {
            var existeEspecialidad = await _context.Especialidades
                .Find(e => e.Descripcion == especialidades.Descripcion)
                .AnyAsync();

            if (existeEspecialidad)
            {
                ModelState.AddModelError(nameof(Especialidades.Descripcion), "Ya existe una especialidad con esa descripción.");
                PopularHoras(especialidades.FranjaHI, especialidades.FranjaHF);
                return View(especialidades);
            }

            await _context.Especialidades.InsertOneAsync(especialidades);
            return RedirectToAction(nameof(Index));
        }
        catch (MongoWriteException ex)
        {
            ModelState.AddModelError(string.Empty, $"Ocurrió un error al crear la especialidad: {ex.Message}");
            PopularHoras(especialidades.FranjaHI, especialidades.FranjaHF);
            return View(especialidades);
        }
        catch (Exception ex)
        {
            ModelState.AddModelError(string.Empty, $"Ocurrió un error inesperado: {ex.Message}");
            PopularHoras(especialidades.FranjaHI, especialidades.FranjaHF);
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

        var especialidad = await _context.Especialidades
            .Find(e => e.Id == id)
            .FirstOrDefaultAsync();

        if (especialidad == null)
        {
            return NotFound();
        }

        PopularHoras(especialidad.FranjaHI, especialidad.FranjaHF);
        return View(especialidad);
    }

    // POST: ESPECIALIDADESS/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(string id, Especialidades especialidades)
    {
        if (string.IsNullOrEmpty(id) || !ObjectId.TryParse(id, out _))
        {
            return NotFound();
        }

        if (id != especialidades.Id)
        {
            return NotFound();
        }

        especialidades.FranjaHI = NormalizarHora(especialidades.FranjaHI);
        especialidades.FranjaHF = NormalizarHora(especialidades.FranjaHF);

        if (!ModelState.IsValid)
        {
            PopularHoras(especialidades.FranjaHI, especialidades.FranjaHF);
            return View(especialidades);
        }

        try
        {
            var existeEspecialidad = await _context.Especialidades
                .Find(e => e.Descripcion == especialidades.Descripcion && e.Id != id)
                .AnyAsync();

            if (existeEspecialidad)
            {
                ModelState.AddModelError(nameof(Especialidades.Descripcion), "Ya existe una especialidad con esa descripción.");
                PopularHoras(especialidades.FranjaHI, especialidades.FranjaHF);
                return View(especialidades);
            }

            var resultado = await _context.Especialidades
                .ReplaceOneAsync(e => e.Id == id, especialidades);

            if (resultado.MatchedCount == 0)
            {
                return NotFound();
            }

            return RedirectToAction(nameof(Index));
        }
        catch (MongoWriteException ex)
        {
            ModelState.AddModelError(string.Empty, $"Ocurrió un error al actualizar la especialidad: {ex.Message}");
            PopularHoras(especialidades.FranjaHI, especialidades.FranjaHF);
            return View(especialidades);
        }
        catch (Exception ex)
        {
            ModelState.AddModelError(string.Empty, $"Ocurrió un error inesperado: {ex.Message}");
            PopularHoras(especialidades.FranjaHI, especialidades.FranjaHF);
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

        var especialidad = await _context.Especialidades
            .Find(e => e.Id == id)
            .FirstOrDefaultAsync();

        if (especialidad == null)
        {
            return NotFound();
        }

        return View(especialidad);
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

        var resultado = await _context.Especialidades
            .DeleteOneAsync(e => e.Id == id);

        if (resultado.DeletedCount == 0)
        {
            return NotFound();
        }

        return RedirectToAction(nameof(Index));
    }
}
