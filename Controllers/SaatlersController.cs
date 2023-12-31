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
    public class SaatlersController : Controller
    {
        private readonly hospitalContext _context;

        public SaatlersController(hospitalContext context)
        {
            _context = context;
        }

        // GET: Saatlers
        public async Task<IActionResult> Index()
        {
            var hospitalContext = _context.Saatlers.Include(s => s.CalismaGun);
            return View(await hospitalContext.ToListAsync());
        }

        // GET: Saatlers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Saatlers == null)
            {
                return NotFound();
            }

            var saatler = await _context.Saatlers
                .Include(s => s.CalismaGun)
                .FirstOrDefaultAsync(m => m.SaatId == id);
            if (saatler == null)
            {
                return NotFound();
            }

            return View(saatler);
        }

        // GET: Saatlers/Create
        public IActionResult Create()
        {
            ViewData["CalismaGunId"] = new SelectList(_context.CalismaGuns, "CalismaGunId", "CalismaGunId");
            return View();
        }

        // POST: Saatlers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SaatId,CalismaGunId,SaatDilimi,Secilebilir")] Saatler saatler)
        {
            if (ModelState.IsValid)
            {
                _context.Add(saatler);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CalismaGunId"] = new SelectList(_context.CalismaGuns, "CalismaGunId", "CalismaGunId", saatler.CalismaGunId);
            return View(saatler);
        }

        // GET: Saatlers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Saatlers == null)
            {
                return NotFound();
            }

            var saatler = await _context.Saatlers.FindAsync(id);
            if (saatler == null)
            {
                return NotFound();
            }
            ViewData["CalismaGunId"] = new SelectList(_context.CalismaGuns, "CalismaGunId", "CalismaGunId", saatler.CalismaGunId);
            return View(saatler);
        }

        // POST: Saatlers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SaatId,CalismaGunId,SaatDilimi,Secilebilir")] Saatler saatler)
        {
            if (id != saatler.SaatId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(saatler);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SaatlerExists(saatler.SaatId))
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
            ViewData["CalismaGunId"] = new SelectList(_context.CalismaGuns, "CalismaGunId", "CalismaGunId", saatler.CalismaGunId);
            return View(saatler);
        }

        // GET: Saatlers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Saatlers == null)
            {
                return NotFound();
            }

            var saatler = await _context.Saatlers
                .Include(s => s.CalismaGun)
                .FirstOrDefaultAsync(m => m.SaatId == id);
            if (saatler == null)
            {
                return NotFound();
            }

            return View(saatler);
        }

        // POST: Saatlers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Saatlers == null)
            {
                return Problem("Entity set 'hospitalContext.Saatlers'  is null.");
            }
            var saatler = await _context.Saatlers.FindAsync(id);
            if (saatler != null)
            {
                _context.Saatlers.Remove(saatler);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SaatlerExists(int id)
        {
          return (_context.Saatlers?.Any(e => e.SaatId == id)).GetValueOrDefault();
        }
    }
}
