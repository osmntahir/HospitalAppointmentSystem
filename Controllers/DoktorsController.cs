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
    public class DoktorsController : Controller
    {
        private readonly hospitalContext _context;

        public DoktorsController(hospitalContext context)
        {
            _context = context;
        }

        // GET: Doktors
        public async Task<IActionResult> Index()
        {
            var hospitalContext = _context.Doktors.Include(d => d.Poliklinik);
            return View(await hospitalContext.ToListAsync());
        }

        // GET: Doktors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Doktors == null)
            {
                return NotFound();
            }

            var doktor = await _context.Doktors
                .Include(d => d.Poliklinik)
                .FirstOrDefaultAsync(m => m.DoktorId == id);
            if (doktor == null)
            {
                return NotFound();
            }

            return View(doktor);
        }

        // GET: Doktors/Create
        public IActionResult Create()
        {
            ViewData["PoliklinikId"] = new SelectList(_context.Polikliniks, "PoliklinikId", "PoliklinikId");
            return View();
        }

        // POST: Doktors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DoktorId,Adi,Soyadi,Brans,PoliklinikId,Telefon,Mail,Adres,Maas,Cinsiyet")] Doktor doktor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(doktor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PoliklinikId"] = new SelectList(_context.Polikliniks, "PoliklinikId", "PoliklinikId", doktor.PoliklinikId);
            return View(doktor);
        }

        // GET: Doktors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Doktors == null)
            {
                return NotFound();
            }

            var doktor = await _context.Doktors.FindAsync(id);
            if (doktor == null)
            {
                return NotFound();
            }
            ViewData["PoliklinikId"] = new SelectList(_context.Polikliniks, "PoliklinikId", "PoliklinikId", doktor.PoliklinikId);
            return View(doktor);
        }

        // POST: Doktors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DoktorId,Adi,Soyadi,Brans,PoliklinikId,Telefon,Mail,Adres,Maas,Cinsiyet")] Doktor doktor)
        {
            if (id != doktor.DoktorId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(doktor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DoktorExists(doktor.DoktorId))
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
            ViewData["PoliklinikId"] = new SelectList(_context.Polikliniks, "PoliklinikId", "PoliklinikId", doktor.PoliklinikId);
            return View(doktor);
        }

        // GET: Doktors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Doktors == null)
            {
                return NotFound();
            }

            var doktor = await _context.Doktors
                .Include(d => d.Poliklinik)
                .FirstOrDefaultAsync(m => m.DoktorId == id);
            if (doktor == null)
            {
                return NotFound();
            }

            return View(doktor);
        }

        // POST: Doktors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Doktors == null)
            {
                return Problem("Entity set 'hospitalContext.Doktors'  is null.");
            }
            var doktor = await _context.Doktors.FindAsync(id);
            if (doktor != null)
            {
                _context.Doktors.Remove(doktor);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DoktorExists(int id)
        {
          return (_context.Doktors?.Any(e => e.DoktorId == id)).GetValueOrDefault();
        }
    }
}
