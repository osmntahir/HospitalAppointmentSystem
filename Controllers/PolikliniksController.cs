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
    public class PolikliniksController : Controller
    {
        private readonly hospitalContext _context;
        private readonly DoktorsController _doktorController;

        public PolikliniksController(hospitalContext context, DoktorsController doktorController)
        {
            _context = context;
            _doktorController = doktorController;
        }

        // GET: Polikliniks
        public async Task<IActionResult> Index()
        {
            var hospitalContext = _context.Polikliniks.Include(p => p.AnaBilimDali);
            return View(await hospitalContext.ToListAsync());
        }

        // GET: Polikliniks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Polikliniks == null)
            {
                return NotFound();
            }

            var poliklinik = await _context.Polikliniks
                .Include(p => p.AnaBilimDali)
                .FirstOrDefaultAsync(m => m.PoliklinikId == id);
            if (poliklinik == null)
            {
                return NotFound();
            }

            return View(poliklinik);
        }

        // GET: Polikliniks/Create
        public IActionResult Create()
        {
            ViewData["AnaBilimDaliId"] = new SelectList(_context.AnaBilimDalis, "AnaBilimDaliId", "AnaBilimDaliId");
            return View();
        }

        // POST: Polikliniks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PoliklinikId,AnaBilimDaliId,Adi")] Poliklinik poliklinik)
        {
            if (ModelState.IsValid)
            {
                _context.Add(poliklinik);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AnaBilimDaliId"] = new SelectList(_context.AnaBilimDalis, "AnaBilimDaliId", "AnaBilimDaliId", poliklinik.AnaBilimDaliId);
            return View(poliklinik);
        }

        // GET: Polikliniks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Polikliniks == null)
            {
                return NotFound();
            }

            var poliklinik = await _context.Polikliniks.FindAsync(id);
            if (poliklinik == null)
            {
                return NotFound();
            }
            ViewData["AnaBilimDaliId"] = new SelectList(_context.AnaBilimDalis, "AnaBilimDaliId", "AnaBilimDaliId", poliklinik.AnaBilimDaliId);
            return View(poliklinik);
        }

        // POST: Polikliniks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PoliklinikId,AnaBilimDaliId,Adi")] Poliklinik poliklinik)
        {
            if (id != poliklinik.PoliklinikId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(poliklinik);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PoliklinikExists(poliklinik.PoliklinikId))
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
            ViewData["AnaBilimDaliId"] = new SelectList(_context.AnaBilimDalis, "AnaBilimDaliId", "AnaBilimDaliId", poliklinik.AnaBilimDaliId);
            return View(poliklinik);
        }

        // GET: Polikliniks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Polikliniks == null)
            {
                return NotFound();
            }

            var poliklinik = await _context.Polikliniks
                .Include(p => p.AnaBilimDali)
                .FirstOrDefaultAsync(m => m.PoliklinikId == id);
            if (poliklinik == null)
            {
                return NotFound();
            }

            return View(poliklinik);
        }

        // POST: Polikliniks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Polikliniks == null)
            {
                return Problem("Entity set 'hospitalContext.Polikliniks' is null.");
            }

            var poliklinik = await _context.Polikliniks.FindAsync(id);

            if (poliklinik == null)
            {
                return NotFound();
            }

            // İlgili doktorları bul ve sil
            var doktorlar = _context.Doktors.Where(d => d.PoliklinikId == id).ToList();

            foreach (var doktor in doktorlar)
            {
                // İlgili doktoru ve ona bağlı verileri sil
                await _doktorController.DeleteConfirmed(doktor.DoktorId);
            }
            _context.Polikliniks.Remove(poliklinik);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        private bool PoliklinikExists(int id)
        {
          return (_context.Polikliniks?.Any(e => e.PoliklinikId == id)).GetValueOrDefault();
        }
    }
}
