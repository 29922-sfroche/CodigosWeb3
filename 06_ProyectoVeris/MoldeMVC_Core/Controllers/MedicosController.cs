
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoldeMVC_Core.Data;

public class MedicosController : Controller
{
    private readonly VerisMongoContext _context;

    public MedicosController(VerisMongoContext context)
    {
        _context = context;
    }

    //// GET: MEDICOSS
    //public async Task<IActionResult> Index()    
    //{
    //    return View(await _context.Medicos.ToListAsync());
    //}

    //// GET: MEDICOSS/Details/5
    //public async Task<IActionResult> Details(string? _id)
    //{
    //    if (_id == null)
    //    {
    //        return NotFound();
    //    }

    //    var medicos = await _context.Medicos
    //        .FirstOrDefaultAsync(m => m._id == _id);
    //    if (medicos == null)
    //    {
    //        return NotFound();
    //    }

    //    return View(medicos);
    //}

    //// GET: MEDICOSS/Create
    //public IActionResult Create()
    //{
    //    return View();
    //}

    //// POST: MEDICOSS/Create
    //// To protect from overposting attacks, enable the specific properties you want to bind to.
    //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    //[HttpPost]
    //[ValidateAntiForgeryToken]
    //public async Task<IActionResult> Create([Bind("_id,nombre,especialidadId,foto")] Medicos medicos)
    //{
    //    if (ModelState.IsValid)
    //    {
    //        _context.Add(medicos);
    //        await _context.SaveChangesAsync();
    //        return RedirectToAction(nameof(Index));
    //    }
    //    return View(medicos);
    //}

    //// GET: MEDICOSS/Edit/5
    //public async Task<IActionResult> Edit(string? _id)
    //{
    //    if (_id == null)
    //    {
    //        return NotFound();
    //    }

    //    var medicos = await _context.Medicos.FindAsync(_id);
    //    if (medicos == null)
    //    {
    //        return NotFound();
    //    }
    //    return View(medicos);
    //}

    //// POST: MEDICOSS/Edit/5
    //// To protect from overposting attacks, enable the specific properties you want to bind to.
    //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    //[HttpPost]
    //[ValidateAntiForgeryToken]
    //public async Task<IActionResult> Edit(string? _id, [Bind("_id,nombre,especialidadId,foto")] Medicos medicos)
    //{
    //    if (_id != medicos._id)
    //    {
    //        return NotFound();
    //    }

    //    if (ModelState.IsValid)
    //    {
    //        try
    //        {
    //            _context.Update(medicos);
    //            await _context.SaveChangesAsync();
    //        }
    //        catch (DbUpdateConcurrencyException)
    //        {
    //            if (!MedicosExists(medicos._id))
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
    //    return View(medicos);
    //}

    //// GET: MEDICOSS/Delete/5
    //public async Task<IActionResult> Delete(string? _id)
    //{
    //    if (_id == null)
    //    {
    //        return NotFound();
    //    }

    //    var medicos = await _context.Medicos
    //        .FirstOrDefaultAsync(m => m._id == _id);
    //    if (medicos == null)
    //    {
    //        return NotFound();
    //    }

    //    return View(medicos);
    //}

    //// POST: MEDICOSS/Delete/5
    //[HttpPost, ActionName("Delete")]
    //[ValidateAntiForgeryToken]
    //public async Task<IActionResult> DeleteConfirmed(string? _id)
    //{
    //    var medicos = await _context.Medicos.FindAsync(_id);
    //    if (medicos != null)
    //    {
    //        _context.Medicos.Remove(medicos);
    //    }

    //    await _context.SaveChangesAsync();
    //    return RedirectToAction(nameof(Index));
    //}

}
