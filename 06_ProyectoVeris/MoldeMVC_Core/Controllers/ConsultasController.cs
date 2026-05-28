using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MoldeMVC_Core.Models;
using MoldeMVC_Core.Data;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class ConsultasController : Controller
{
    private readonly VerisMongoContext _context;

    public ConsultasController(VerisMongoContext context)
    {
        _context = context;
    }

    // GET: CONSULTASS
    public async Task<IActionResult> Index()
    {
        var consultas = await _context.Consultas.Find(Builders<Consultas>.Filter.Empty).ToListAsync();

        return View(consultas);
    }

    // GET: CONSULTASS/Details/5
    public async Task<IActionResult> Details(string id)
    {
        if (string.IsNullOrEmpty(id) || !ObjectId.TryParse(id, out _))
        {
            return NotFound();
        }

        var consultas = await _context.Consultas
            .Find(c => c._id == id)
            .FirstOrDefaultAsync();

        if (consultas == null)
        {
            return NotFound();
        }

        return View(consultas);
    }

    // GET: CONSULTASS/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: CONSULTASS/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("medicoId,pacienteId,fechaConsulta,hi,hf,diagnostico")] Consultas consultas)
    {

        consultas._id = ObjectId.GenerateNewId().ToString();

        ModelState.Remove("_id"); // Elimina la validación para _id, ya que se genera automáticamente

        if (ModelState.IsValid)
        {
            return View(consultas);
        }

        try
        {
            var existeConsulta = await _context.Consultas.Find(c => c._id == consultas._id).FirstOrDefaultAsync();

            if (existeConsulta != null)
            {
                ModelState.AddModelError(string.Empty, "Ya existe una consulta con el mismo ID.");
                return View(consultas);
            }

            await _context.Consultas.InsertOneAsync(consultas);

            return RedirectToAction(nameof(Index));

        }
        catch (MongoWriteException ex)
        {
            ModelState.AddModelError(string.Empty, $"Error al escribir en la base de datos: {ex.Message}");
            return View(consultas);
        }
        catch (Exception ex)
        {
            ModelState.AddModelError(string.Empty, $"Error al crear la consulta: {ex.Message}");
            return View(consultas);
        }

    }

    // GET: CONSULTASS/Edit/5
    public async Task<IActionResult> Edit(string id)
    {
        if (string.IsNullOrEmpty(id) || !ObjectId.TryParse(id, out _))
        {
            return NotFound();
        }

        var consultas = await _context.Consultas.Find(c => c._id == id).FirstOrDefaultAsync();

        if (consultas == null)
        {
            return NotFound();
        }
        return View(consultas);
    }

    // POST: CONSULTASS/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(string id, [Bind("_id,medicoId,pacienteId,fechaConsulta,hi,hf,diagnostico")] Consultas consultas)
    {
        if (String.IsNullOrEmpty(id) || !ObjectId.TryParse(id, out _))
        {
            return NotFound();
        }

        if(id != consultas._id)
        {
            return NotFound();
        }
        
        ModelState.Remove("_id"); // Elimina la validación para _id, ya que no se debe modificar

        if (!ModelState.IsValid)
        {
            return View(consultas);
        }

        try
        {
            var filtro = Builders<Consultas>.Filter.Eq(c => c._id, id);
            var resultado = await _context.Consultas.ReplaceOneAsync(filtro, consultas);
            if (resultado.MatchedCount == 0)
            {
                return NotFound();
            }
            return RedirectToAction(nameof(Index));
        }
        catch (MongoWriteException ex)
        {
            ModelState.AddModelError(string.Empty, $"Error al escribir en la base de datos: {ex.Message}");
            return View(consultas);
        }
        catch (Exception ex)
        {
            ModelState.AddModelError(string.Empty, $"Error al editar la consulta: {ex.Message}");
            return View(consultas);

        }
        

    }
         
    // GET: CONSULTASS/Delete/5
    public async Task<IActionResult> Delete(string id)
    {
        if (string.IsNullOrEmpty(id) || !ObjectId.TryParse(id, out _))
        {
            return NotFound();
        }

        var consultas = await _context.Consultas
            .Find(c => c._id == id)
            .FirstOrDefaultAsync();

        if (consultas == null)
        {
            return NotFound();
        }

        return View(consultas);
    }

    // POST: CONSULTASS/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(string id)
    {
        
        if(String.IsNullOrEmpty(id) || !ObjectId.TryParse(id, out _))
        {
            return NotFound();
        }

        var resultado = await _context.Consultas.DeleteOneAsync(c => c._id == id);
    
        if (resultado.DeletedCount == 0)
        {
            return NotFound();
        }

        return RedirectToAction(nameof(Index));

    }

    //private bool ConsultasExists(string? _id)
    //{
    //    return _context.Consultas.Any(e => e._id == _id);
    //}


}
