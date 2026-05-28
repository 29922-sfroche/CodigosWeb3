
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoldeMVC_Core.Models;
using MoldeMVC_Core.Data;

public class EspecialidadesController : Controller
{
    private readonly VerisMongoContext _context;

    public EspecialidadesController(VerisMongoContext context)
    {
        _context = context;
    }

    //// GET: ESPECIALIDADESS
    //public async Task<IActionResult> Index()    
    //{
    //    return View(await _context.Especialidades.ToListAsync());
    //}

    //// GET: ESPECIALIDADESS/Details/5
    //public async Task<IActionResult> Details(string? _id)
    //{
    //    if (_id == null)
    //    {
    //        return NotFound();
    //    }

    //    var especialidades = await _context.Especialidades
    //        .FirstOrDefaultAsync(m => m._id == _id);
    //    if (especialidades == null)
    //    {
    //        return NotFound();
    //    }

    //    return View(especialidades);
    //}

    //// GET: ESPECIALIDADESS/Create
    //public IActionResult Create()
    //{
    //    return View();
    //}

    //// POST: ESPECIALIDADESS/Create
    //// To protect from overposting attacks, enable the specific properties you want to bind to.
    //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    //[HttpPost]
    //[ValidateAntiForgeryToken]
    //public async Task<IActionResult> Create([Bind("_id,descripcion,dias,franjaHI,franjaHF")] Especialidades especialidades)
    //{
    //    if (ModelState.IsValid)
    //    {
    //        _context.Add(especialidades);
    //        await _context.SaveChangesAsync();
    //        return RedirectToAction(nameof(Index));
    //    }
    //    return View(especialidades);
    //}

    //// GET: ESPECIALIDADESS/Edit/5
    //public async Task<IActionResult> Edit(string? _id)
    //{
    //    if (_id == null)
    //    {
    //        return NotFound();
    //    }

    //    var especialidades = await _context.Especialidades.FindAsync(_id);
    //    if (especialidades == null)
    //    {
    //        return NotFound();
    //    }
    //    return View(especialidades);
    //}

    //// POST: ESPECIALIDADESS/Edit/5
    //// To protect from overposting attacks, enable the specific properties you want to bind to.
    //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    //[HttpPost]
    //[ValidateAntiForgeryToken]
    //public async Task<IActionResult> Edit(string? _id, [Bind("_id,descripcion,dias,franjaHI,franjaHF")] Especialidades especialidades)
    //{
    //    if (_id != especialidades._id)
    //    {
    //        return NotFound();
    //    }

    //    if (ModelState.IsValid)
    //    {
    //        try
    //        {
    //            _context.Update(especialidades);
    //            await _context.SaveChangesAsync();
    //        }
    //        catch (DbUpdateConcurrencyException)
    //        {
    //            if (!EspecialidadesExists(especialidades._id))
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
    //    return View(especialidades);
    //}

    //// GET: ESPECIALIDADESS/Delete/5
    //public async Task<IActionResult> Delete(string? _id)
    //{
    //    if (_id == null)
    //    {
    //        return NotFound();
    //    }

    //    var especialidades = await _context.Especialidades
    //        .FirstOrDefaultAsync(m => m._id == _id);
    //    if (especialidades == null)
    //    {
    //        return NotFound();
    //    }

    //    return View(especialidades);
    //}

    //// POST: ESPECIALIDADESS/Delete/5
    //[HttpPost, ActionName("Delete")]
    //[ValidateAntiForgeryToken]
    //public async Task<IActionResult> DeleteConfirmed(string? _id)
    //{
    //    var especialidades = await _context.Especialidades.FindAsync(_id);
    //    if (especialidades != null)
    //    {
    //        _context.Especialidades.Remove(especialidades);
    //    }

    //    await _context.SaveChangesAsync();
    //    return RedirectToAction(nameof(Index));
    //}

    //private bool EspecialidadesExists(string? _id)
    //{
    //    return _context.Especialidades.Any(e => e._id == _id);
    //}
}
