
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoldeMVC_Core.Models;
using MoldeMVC_Core.Data;

public class ConsultasController : Controller
{
    private readonly VerisMongoContext _context;

    public ConsultasController(VerisMongoContext context)
    {
        _context = context;
    }

    //// GET: CONSULTASS
    //public async Task<IActionResult> Index()    
    //{
    //    return View(await _context.Consultas.ToListAsync());
    //}

    //// GET: CONSULTASS/Details/5
    //public async Task<IActionResult> Details(string? _id)
    //{
    //    if (_id == null)
    //    {
    //        return NotFound();
    //    }

    //    var consultas = await _context.Consultas
    //        .FirstOrDefaultAsync(m => m._id == _id);
    //    if (consultas == null)
    //    {
    //        return NotFound();
    //    }

    //    return View(consultas);
    //}

    //// GET: CONSULTASS/Create
    //public IActionResult Create()
    //{
    //    return View();
    //}

    //// POST: CONSULTASS/Create
    //// To protect from overposting attacks, enable the specific properties you want to bind to.
    //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    //[HttpPost]
    //[ValidateAntiForgeryToken]
    //public async Task<IActionResult> Create([Bind("_id,medicoId,pacienteId,fechaConsulta,hi,hf,diagnostico")] Consultas consultas)
    //{
    //    if (ModelState.IsValid)
    //    {
    //        _context.Add(consultas);
    //        await _context.SaveChangesAsync();
    //        return RedirectToAction(nameof(Index));
    //    }
    //    return View(consultas);
    //}

    //// GET: CONSULTASS/Edit/5
    //public async Task<IActionResult> Edit(string? _id)
    //{
    //    if (_id == null)
    //    {
    //        return NotFound();
    //    }

    //    var consultas = await _context.Consultas.FindAsync(_id);
    //    if (consultas == null)
    //    {
    //        return NotFound();
    //    }
    //    return View(consultas);
    //}

    //// POST: CONSULTASS/Edit/5
    //// To protect from overposting attacks, enable the specific properties you want to bind to.
    //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    //[HttpPost]
    //[ValidateAntiForgeryToken]
    //public async Task<IActionResult> Edit(string? _id, [Bind("_id,medicoId,pacienteId,fechaConsulta,hi,hf,diagnostico")] Consultas consultas)
    //{
    //    if (_id != consultas._id)
    //    {
    //        return NotFound();
    //    }

    //    if (ModelState.IsValid)
    //    {
    //        try
    //        {
    //            _context.Update(consultas);
    //            await _context.SaveChangesAsync();
    //        }
    //        catch (DbUpdateConcurrencyException)
    //        {
    //            if (!ConsultasExists(consultas._id))
    //            {
    //                return NotFound();
    //            }
    //            else
    //            {
    //                throw;
    //            }
    //        }
    //        return RedirectToAction(nameof(Index));
    //    }
    //    return View(consultas);
    //}

    //// GET: CONSULTASS/Delete/5
    //public async Task<IActionResult> Delete(string? _id)
    //{
    //    if (_id == null)
    //    {
    //        return NotFound();
    //    }

    //    var consultas = await _context.Consultas
    //        .FirstOrDefaultAsync(m => m._id == _id);
    //    if (consultas == null)
    //    {
    //        return NotFound();
    //    }

    //    return View(consultas);
    //}

    //// POST: CONSULTASS/Delete/5
    //[HttpPost, ActionName("Delete")]
    //[ValidateAntiForgeryToken]
    //public async Task<IActionResult> DeleteConfirmed(string? _id)
    //{
    //    var consultas = await _context.Consultas.FindAsync(_id);
    //    if (consultas != null)
    //    {
    //        _context.Consultas.Remove(consultas);
    //    }

    //    await _context.SaveChangesAsync();
    //    return RedirectToAction(nameof(Index));
    //}

    //private bool ConsultasExists(string? _id)
    //{
    //    return _context.Consultas.Any(e => e._id == _id);
    //}
}
