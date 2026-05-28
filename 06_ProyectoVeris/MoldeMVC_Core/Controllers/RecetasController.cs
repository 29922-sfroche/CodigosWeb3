
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoldeMVC_Core.Models;
using MoldeMVC_Core.Data;

public class RecetasController : Controller
{
    private readonly VerisMongoContext _context;

    public RecetasController(VerisMongoContext context)
    {
        _context = context;
    }

    //// GET: RECETASS
    //public async Task<IActionResult> Index()    
    //{
    //    return View(await _context.Recetas.ToListAsync());
    //}

    //// GET: RECETASS/Details/5
    //public async Task<IActionResult> Details(string? _id)
    //{
    //    if (_id == null)
    //    {
    //        return NotFound();
    //    }

    //    var recetas = await _context.Recetas
    //        .FirstOrDefaultAsync(m => m._id == _id);
    //    if (recetas == null)
    //    {
    //        return NotFound();
    //    }

    //    return View(recetas);
    //}

    //// GET: RECETASS/Create
    //public IActionResult Create()
    //{
    //    return View();
    //}

    //// POST: RECETASS/Create
    //// To protect from overposting attacks, enable the specific properties you want to bind to.
    //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    //[HttpPost]
    //[ValidateAntiForgeryToken]
    //public async Task<IActionResult> Create([Bind("_id,consultaId,medicamentoId,cantidad")] Recetas recetas)
    //{
    //    if (ModelState.IsValid)
    //    {
    //        _context.Add(recetas);
    //        await _context.SaveChangesAsync();
    //        return RedirectToAction(nameof(Index));
    //    }
    //    return View(recetas);
    //}

    //// GET: RECETASS/Edit/5
    //public async Task<IActionResult> Edit(string? _id)
    //{
    //    if (_id == null)
    //    {
    //        return NotFound();
    //    }

    //    var recetas = await _context.Recetas.FindAsync(_id);
    //    if (recetas == null)
    //    {
    //        return NotFound();
    //    }
    //    return View(recetas);
    //}

    //// POST: RECETASS/Edit/5
    //// To protect from overposting attacks, enable the specific properties you want to bind to.
    //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    //[HttpPost]
    //[ValidateAntiForgeryToken]
    //public async Task<IActionResult> Edit(string? _id, [Bind("_id,consultaId,medicamentoId,cantidad")] Recetas recetas)
    //{
    //    if (_id != recetas._id)
    //    {
    //        return NotFound();
    //    }

    //    if (ModelState.IsValid)
    //    {
    //        try
    //        {
    //            _context.Update(recetas);
    //            await _context.SaveChangesAsync();
    //        }
    //        catch (DbUpdateConcurrencyException)
    //        {
    //            if (!RecetasExists(recetas._id))
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
    //    return View(recetas);
    //}

    //// GET: RECETASS/Delete/5
    //public async Task<IActionResult> Delete(string? _id)
    //{
    //    if (_id == null)
    //    {
    //        return NotFound();
    //    }

    //    var recetas = await _context.Recetas
    //        .FirstOrDefaultAsync(m => m._id == _id);
    //    if (recetas == null)
    //    {
    //        return NotFound();
    //    }

    //    return View(recetas);
    //}

    //// POST: RECETASS/Delete/5
    //[HttpPost, ActionName("Delete")]
    //[ValidateAntiForgeryToken]
    //public async Task<IActionResult> DeleteConfirmed(string? _id)
    //{
    //    var recetas = await _context.Recetas.FindAsync(_id);
    //    if (recetas != null)
    //    {
    //        _context.Recetas.Remove(recetas);
    //    }

    //    await _context.SaveChangesAsync();
    //    return RedirectToAction(nameof(Index));
    //}

    //private bool RecetasExists(string? _id)
    //{
    //    return _context.Recetas.Any(e => e._id == _id);
    //}
}
