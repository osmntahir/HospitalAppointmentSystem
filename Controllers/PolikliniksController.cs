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

        public PolikliniksController(hospitalContext context)
        {
            _context = context;
        }

        // GET: Polikliniks
        public async Task<IActionResult> Index()
        {
            var hospitalContext = _context.Polikliniks.Include(p => p.Bolum);
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
                .Include(p => p.Bolum)
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
            ViewData["BolumId"] = new SelectList(_context.Bolums, "BolumId", "BolumId");
            return View();
        }

        // POST: Polikliniks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PoliklinikId,BolumId,PoliklinikAdi")] Poliklinik poliklinik)
        {
            if (ModelState.IsValid)
            {
                _context.Add(poliklinik);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BolumId"] = new SelectList(_context.Bolums, "BolumId", "BolumId", poliklinik.BolumId);
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
            ViewData["BolumId"] = new SelectList(_context.Bolums, "BolumId", "BolumId", poliklinik.BolumId);
            return View(poliklinik);
        }

        // POST: Polikliniks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PoliklinikId,BolumId,PoliklinikAdi")] Poliklinik poliklinik)
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
            ViewData["BolumId"] = new SelectList(_context.Bolums, "BolumId", "BolumId", poliklinik.BolumId);
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
                .Include(p => p.Bolum)
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
                return Problem("Entity set 'hospitalContext.Polikliniks'  is null.");
            }
            var poliklinik = await _context.Polikliniks.FindAsync(id);
            if (poliklinik != null)
            {
                _context.Polikliniks.Remove(poliklinik);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PoliklinikExists(int id)
        {
          return (_context.Polikliniks?.Any(e => e.PoliklinikId == id)).GetValueOrDefault();
        }
    }
}
