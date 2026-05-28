using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MoldeMVC_Core.Data;
using MoldeMVC_Core.Models;

namespace MoldeMVC_Core.Controllers
{
    public class MedicosController : Controller
    {
        private readonly VerisMongoContext _context;

        public MedicosController(VerisMongoContext context)
        {
            _context = context;
        }

        //// GET: Medicos
        //public async Task<IActionResult> Index()
        //{
        //    return View(await _context.Medicos.ToListAsync());
        //}

        //// GET: Medicos/Details/5
        //public async Task<IActionResult> Details(string id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var medicos = await _context.Medicos
        //        .FirstOrDefaultAsync(m => m._id == id);
        //    if (medicos == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(medicos);
        //}

        //// GET: Medicos/Create
        //public IActionResult Create()
        //{
        //    return View();
        //}

        //// POST: Medicos/Create
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("_id,nombre,especialidad,foto")] Medicos medicos)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(medicos);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(medicos);
        //}

        //// GET: Medicos/Edit/5
        //public async Task<IActionResult> Edit(string id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var medicos = await _context.Medicos.FindAsync(id);
        //    if (medicos == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(medicos);
        //}

        //// POST: Medicos/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(string id, [Bind("_id,nombre,especialidad,foto")] Medicos medicos)
        //{
        //    if (id != medicos._id)
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

        //// GET: Medicos/Delete/5
        //public async Task<IActionResult> Delete(string id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var medicos = await _context.Medicos
        //        .FirstOrDefaultAsync(m => m._id == id);
        //    if (medicos == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(medicos);
        //}

        //// POST: Medicos/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(string id)
        //{
        //    var medicos = await _context.Medicos.FindAsync(id);
        //    if (medicos != null)
        //    {
        //        _context.Medicos.Remove(medicos);
        //    }

        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool MedicosExists(string id)
        //{
        //    return _context.Medicos.Any(e => e._id == id);
        //}
    }
}
