using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MoldeMVC_Core.Data;
using MoldeMVC_Core.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Globalization;
using System.Linq;

public class ConsultasController : Controller
{
    private readonly VerisMongoContext _context;

    public ConsultasController(VerisMongoContext context)
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

    private void PopularHoras(string? hi = null, string? hf = null)
    {
        ViewBag.Horas = new SelectList(ObtenerHoras(), "Value", "Text", hi);
        ViewBag.HorasFin = new SelectList(ObtenerHoras(), "Value", "Text", hf);
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

    private static bool EsHoraFinPosterior(string hi, string hf)
    {
        return TimeSpan.TryParse(hi, out var horaInicio)
            && TimeSpan.TryParse(hf, out var horaFin)
            && horaFin > horaInicio;
    }

    private async Task PopularSelectListsAsync(string? medicoId = null, string? pacienteId = null)
    {
        var medicos = await _context.Medicos
            .Find(_ => true)
            .SortBy(m => m.Nombre)
            .ToListAsync();

        var pacientes = await _context.Pacientes
            .Find(_ => true)
            .SortBy(p => p.Nombre)
            .ToListAsync();

        ViewBag.MedicoId = new SelectList(medicos, nameof(Medicos.Id), nameof(Medicos.Nombre), medicoId);
        ViewBag.PacienteId = new SelectList(pacientes, nameof(Pacientes.Id), nameof(Pacientes.Nombre), pacienteId);
        PopularHoras();
    }

    // GET: CONSULTASS
    public async Task<IActionResult> Index()
    {
        var medicos = await _context.Medicos.Find(_ => true).ToListAsync();
        var pacientes = await _context.Pacientes.Find(_ => true).ToListAsync();

        var consultas = await _context.Consultas
            .Find(Builders<Consultas>.Filter.Empty)
            .SortByDescending(c => c.FechaConsulta)
            .ToListAsync();

        consultas.ForEach(c =>
        {
            c.MedicoNombre = medicos.FirstOrDefault(m => m.Id == c.MedicoId)?.Nombre ?? "-";
            c.PacienteNombre = pacientes.FirstOrDefault(p => p.Id == c.PacienteId)?.Nombre ?? "-";
        });

        return View(consultas);
    }

    // GET: CONSULTASS/Details/5
    public async Task<IActionResult> Details(string id)
    {
        if (string.IsNullOrEmpty(id) || !ObjectId.TryParse(id, out _))
        {
            return NotFound();
        }

        var consulta = await _context.Consultas
            .Find(c => c.Id == id)
            .FirstOrDefaultAsync();

        if (consulta == null)
        {
            return NotFound();
        }

        var medico = await _context.Medicos.Find(m => m.Id == consulta.MedicoId).FirstOrDefaultAsync();
        var paciente = await _context.Pacientes.Find(p => p.Id == consulta.PacienteId).FirstOrDefaultAsync();
        consulta.MedicoNombre = medico?.Nombre ?? "-";
        consulta.PacienteNombre = paciente?.Nombre ?? "-";

        return View(consulta);
    }

    // GET: CONSULTASS/Create
    public async Task<IActionResult> Create()
    {
        await PopularSelectListsAsync();
        return View();
    }

    // POST: CONSULTASS/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Consultas consultas)
    {
        consultas.Id = ObjectId.GenerateNewId().ToString();
        consultas.Hi = NormalizarHora(consultas.Hi);
        consultas.Hf = NormalizarHora(consultas.Hf);

        consultas.FechaConsulta = DateTime.SpecifyKind(consultas.FechaConsulta.Date, DateTimeKind.Utc);

        if (!string.IsNullOrEmpty(consultas.Hi) && !string.IsNullOrEmpty(consultas.Hf) && !EsHoraFinPosterior(consultas.Hi, consultas.Hf))
        {
            ModelState.AddModelError(nameof(Consultas.Hf), "La hora de fin debe ser posterior a la hora de inicio.");
        }

        if (!ModelState.IsValid)
        {
            await PopularSelectListsAsync(consultas.MedicoId, consultas.PacienteId);
            return View(consultas);
        }

        try
        {
            await _context.Consultas.InsertOneAsync(consultas);
            return RedirectToAction(nameof(Index));
        }
        catch (MongoWriteException ex)
        {
            ModelState.AddModelError(string.Empty, $"Error al escribir en la base de datos: {ex.Message}");
        }
        catch (Exception ex)
        {
            ModelState.AddModelError(string.Empty, $"Error al crear la consulta: {ex.Message}");
        }

        await PopularSelectListsAsync(consultas.MedicoId, consultas.PacienteId);
        return View(consultas);
    }

    // GET: CONSULTASS/Edit/5
    public async Task<IActionResult> Edit(string id)
    {
        if (string.IsNullOrEmpty(id) || !ObjectId.TryParse(id, out _))
        {
            return NotFound();
        }

        var consulta = await _context.Consultas
            .Find(c => c.Id == id)
            .FirstOrDefaultAsync();

        if (consulta == null)
        {
            return NotFound();
        }

        await PopularSelectListsAsync(consulta.MedicoId, consulta.PacienteId);
        return View(consulta);
    }

    // POST: CONSULTASS/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(string id, Consultas consultas)
    {
        if (string.IsNullOrEmpty(id) || !ObjectId.TryParse(id, out _))
        {
            return NotFound();
        }

        if (id != consultas.Id)
        {
            return NotFound();
        }

        consultas.Hi = NormalizarHora(consultas.Hi);
        consultas.Hf = NormalizarHora(consultas.Hf);

        consultas.FechaConsulta = DateTime.SpecifyKind(consultas.FechaConsulta.Date, DateTimeKind.Utc);

        if (!string.IsNullOrEmpty(consultas.Hi) && !string.IsNullOrEmpty(consultas.Hf) && !EsHoraFinPosterior(consultas.Hi, consultas.Hf))
        {
            ModelState.AddModelError(nameof(Consultas.Hf), "La hora de fin debe ser posterior a la hora de inicio.");
        }

        if (!ModelState.IsValid)
        {
            await PopularSelectListsAsync(consultas.MedicoId, consultas.PacienteId);
            return View(consultas);
        }

        try
        {
            var resultado = await _context.Consultas
                .ReplaceOneAsync(c => c.Id == id, consultas);

            if (resultado.MatchedCount == 0)
            {
                return NotFound();
            }

            return RedirectToAction(nameof(Index));
        }
        catch (MongoWriteException ex)
        {
            ModelState.AddModelError(string.Empty, $"Error al escribir en la base de datos: {ex.Message}");
        }
        catch (Exception ex)
        {
            ModelState.AddModelError(string.Empty, $"Error al editar la consulta: {ex.Message}");
        }

        await PopularSelectListsAsync(consultas.MedicoId, consultas.PacienteId);
        return View(consultas);
    }

    // GET: CONSULTASS/Delete/5
    public async Task<IActionResult> Delete(string id)
    {
        if (string.IsNullOrEmpty(id) || !ObjectId.TryParse(id, out _))
        {
            return NotFound();
        }

        var consulta = await _context.Consultas
            .Find(c => c.Id == id)
            .FirstOrDefaultAsync();

        if (consulta == null)
        {
            return NotFound();
        }

        var medico = await _context.Medicos.Find(m => m.Id == consulta.MedicoId).FirstOrDefaultAsync();
        var paciente = await _context.Pacientes.Find(p => p.Id == consulta.PacienteId).FirstOrDefaultAsync();
        consulta.MedicoNombre = medico?.Nombre ?? "-";
        consulta.PacienteNombre = paciente?.Nombre ?? "-";

        return View(consulta);
    }

    // POST: CONSULTASS/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(string id)
    {
        if (string.IsNullOrEmpty(id) || !ObjectId.TryParse(id, out _))
        {
            return NotFound();
        }

        var resultado = await _context.Consultas
            .DeleteOneAsync(c => c.Id == id);

        if (resultado.DeletedCount == 0)
        {
            return NotFound();
        }

        return RedirectToAction(nameof(Index));
    }
}
