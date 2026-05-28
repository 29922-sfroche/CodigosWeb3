
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoldeMVC_Core.Models;
using MoldeMVC_Core.Data;

public class MedicamentosController : Controller
{
    private readonly VerisMongoContext _context;

    public MedicamentosController(VerisMongoContext context)
    {
        _context = context;
    }

    //// GET: MEDICAMENTOSS
    //public async Task<IActionResult> Index()    
    //{
    //    return View(await _context.Medicamentos.ToListAsync());
    //}

    //// GET: MEDICAMENTOSS/Details/5
    //public async Task<IActionResult> Details(string? _id)
    //{
    //    if (_id == null)
    //    {
    //        return NotFound();
    //    }

    //    var medicamentos = await _context.Medicamentos
    //        .FirstOrDefaultAsync(m => m._id == _id);
    //    if (medicamentos == null)
    //    {
    //        return NotFound();
    //    }

    //    return View(medicamentos);
    //}

    //// GET: MEDICAMENTOSS/Create
    //public IActionResult Create()
    //{
    //    return View();
    //}

    //// POST: MEDICAMENTOSS/Create
    //// To protect from overposting attacks, enable the specific properties you want to bind to.
    //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    //[HttpPost]
    //[ValidateAntiForgeryToken]
    //public async Task<IActionResult> Create([Bind("_id,nombre,tipo")] Medicamentos medicamentos)
    //{
    //    if (ModelState.IsValid)
    //    {
    //        _context.Add(medicamentos);
    //        await _context.SaveChangesAsync();
    //        return RedirectToAction(nameof(Index));
    //    }
    //    return View(medicamentos);
    //}

    //// GET: MEDICAMENTOSS/Edit/5
    //public async Task<IActionResult> Edit(string? _id)
    //{
    //    if (_id == null)
    //    {
    //        return NotFound();
    //    }

    //    var medicamentos = await _context.Medicamentos.FindAsync(_id);
    //    if (medicamentos == null)
    //    {
    //        return NotFound();
    //    }
    //    return View(medicamentos);
    //}

    //// POST: MEDICAMENTOSS/Edit/5
    //// To protect from overposting attacks, enable the specific properties you want to bind to.
    //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    //[HttpPost]
    //[ValidateAntiForgeryToken]
    //public async Task<IActionResult> Edit(string? _id, [Bind("_id,nombre,tipo")] Medicamentos medicamentos)
    //{
    //    if (_id != medicamentos._id)
    //    {
    //        return NotFound();
    //    }

    //    if (ModelState.IsValid)
    //    {
    //        try
    //        {
    //            _context.Update(medicamentos);
    //            await _context.SaveChangesAsync();
    //        }
    //        catch (DbUpdateConcurrencyException)
    //        {
    //            if (!MedicamentosExists(medicamentos._id))
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
    //    return View(medicamentos);
    //}

    //// GET: MEDICAMENTOSS/Delete/5
    //public async Task<IActionResult> Delete(string? _id)
    //{
    //    if (_id == null)
    //    {
    //        return NotFound();
    //    }

    //    var medicamentos = await _context.Medicamentos
    //        .FirstOrDefaultAsync(m => m._id == _id);
    //    if (medicamentos == null)
    //    {
    //        return NotFound();
    //    }

    //    return View(medicamentos);
    //}

    //// POST: MEDICAMENTOSS/Delete/5
    //[HttpPost, ActionName("Delete")]
    //[ValidateAntiForgeryToken]
    //public async Task<IActionResult> DeleteConfirmed(string? _id)
    //{
    //    var medicamentos = await _context.Medicamentos.FindAsync(_id);
    //    if (medicamentos != null)
    //    {
    //        _context.Medicamentos.Remove(medicamentos);
    //    }

    //    await _context.SaveChangesAsync();
    //    return RedirectToAction(nameof(Index));
    //}

    //private bool MedicamentosExists(string? _id)
    //{
    //    return _context.Medicamentos.Any(e => e._id == _id);
    //}
}
