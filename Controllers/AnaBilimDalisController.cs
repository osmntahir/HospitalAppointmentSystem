using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HospitalAppointmentSystem.Models;

namespace HospitalAppointmentSystem.Controllers
{
    public class AnaBilimDalisController : Controller
    {
        private readonly hospitalContext _context;

        public AnaBilimDalisController(hospitalContext context)
        {
            _context = context;
        }

        // GET: AnaBilimDalis
        public async Task<IActionResult> Index()
        {
              return _context.AnaBilimDalis != null ? 
                          View(await _context.AnaBilimDalis.ToListAsync()) :
                          Problem("Entity set 'hospitalContext.AnaBilimDalis'  is null.");
        }

        // GET: AnaBilimDalis/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.AnaBilimDalis == null)
            {
                return NotFound();
            }

            var anaBilimDali = await _context.AnaBilimDalis
                .FirstOrDefaultAsync(m => m.AnaBilimDaliId == id);
            if (anaBilimDali == null)
            {
                return NotFound();
            }

            return View(anaBilimDali);
        }

        // GET: AnaBilimDalis/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AnaBilimDalis/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AnaBilimDaliId,Adi")] AnaBilimDali anaBilimDali)
        {
            if (ModelState.IsValid)
            {
                _context.Add(anaBilimDali);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(anaBilimDali);
        }

        // GET: AnaBilimDalis/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.AnaBilimDalis == null)
            {
                return NotFound();
            }

            var anaBilimDali = await _context.AnaBilimDalis.FindAsync(id);
            if (anaBilimDali == null)
            {
                return NotFound();
            }
            return View(anaBilimDali);
        }

        // POST: AnaBilimDalis/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AnaBilimDaliId,Adi")] AnaBilimDali anaBilimDali)
        {
            if (id != anaBilimDali.AnaBilimDaliId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(anaBilimDali);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AnaBilimDaliExists(anaBilimDali.AnaBilimDaliId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(anaBilimDali);
        }

        // GET: AnaBilimDalis/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.AnaBilimDalis == null)
            {
                return NotFound();
            }

            var anaBilimDali = await _context.AnaBilimDalis
                .FirstOrDefaultAsync(m => m.AnaBilimDaliId == id);
            if (anaBilimDali == null)
            {
                return NotFound();
            }

            return View(anaBilimDali);
        }

        // POST: AnaBilimDalis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.AnaBilimDalis == null)
            {
                return Problem("Entity set 'hospitalContext.AnaBilimDalis'  is null.");
            }
            var anaBilimDali = await _context.AnaBilimDalis.FindAsync(id);
            if (anaBilimDali != null)
            {
                _context.AnaBilimDalis.Remove(anaBilimDali);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AnaBilimDaliExists(int id)
        {
          return (_context.AnaBilimDalis?.Any(e => e.AnaBilimDaliId == id)).GetValueOrDefault();
        }
    }
}
