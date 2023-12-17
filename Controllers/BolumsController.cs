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
    public class BolumsController : Controller
    {
        private readonly hospitalContext _context;

        public BolumsController(hospitalContext context)
        {
            _context = context;
        }

        // GET: Bolums
        public async Task<IActionResult> Index()
        {
              return _context.Bolums != null ? 
                          View(await _context.Bolums.ToListAsync()) :
                          Problem("Entity set 'hospitalContext.Bolums'  is null.");
        }

        // GET: Bolums/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Bolums == null)
            {
                return NotFound();
            }

            var bolum = await _context.Bolums
                .FirstOrDefaultAsync(m => m.BolumId == id);
            if (bolum == null)
            {
                return NotFound();
            }

            return View(bolum);
        }

        // GET: Bolums/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Bolums/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BolumId,BolumAdi,Aciklama")] Bolum bolum)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bolum);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(bolum);
        }

        // GET: Bolums/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Bolums == null)
            {
                return NotFound();
            }

            var bolum = await _context.Bolums.FindAsync(id);
            if (bolum == null)
            {
                return NotFound();
            }
            return View(bolum);
        }

        // POST: Bolums/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BolumId,BolumAdi,Aciklama")] Bolum bolum)
        {
            if (id != bolum.BolumId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bolum);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BolumExists(bolum.BolumId))
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
            return View(bolum);
        }

        // GET: Bolums/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Bolums == null)
            {
                return NotFound();
            }

            var bolum = await _context.Bolums
                .FirstOrDefaultAsync(m => m.BolumId == id);
            if (bolum == null)
            {
                return NotFound();
            }

            return View(bolum);
        }

        // POST: Bolums/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Bolums == null)
            {
                return Problem("Entity set 'hospitalContext.Bolums'  is null.");
            }
            var bolum = await _context.Bolums.FindAsync(id);
            if (bolum != null)
            {
                _context.Bolums.Remove(bolum);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BolumExists(int id)
        {
          return (_context.Bolums?.Any(e => e.BolumId == id)).GetValueOrDefault();
        }
    }
}
