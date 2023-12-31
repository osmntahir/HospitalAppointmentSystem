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
    public class CalismaGunsController : Controller
    {
        private readonly hospitalContext _context;

        public CalismaGunsController(hospitalContext context)
        {
            _context = context;
        }

        // GET: CalismaGuns
        public async Task<IActionResult> Index()
        {
            var hospitalContext = _context.CalismaGuns.Include(c => c.Doktor);
            return View(await hospitalContext.ToListAsync());
        }

        // GET: CalismaGuns/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.CalismaGuns == null)
            {
                return NotFound();
            }

            var calismaGun = await _context.CalismaGuns
                .Include(c => c.Doktor)
                .FirstOrDefaultAsync(m => m.CalismaGunId == id);
            if (calismaGun == null)
            {
                return NotFound();
            }

            return View(calismaGun);
        }

        // GET: CalismaGuns/Create
        public IActionResult Create()
        {
            ViewData["DoktorId"] = new SelectList(_context.Doktors, "DoktorId", "DoktorId");
            return View();
        }

        // POST: CalismaGuns/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CalismaGunId,DoktorId,Gun,BaslangicSaati,BitisSaati")] CalismaGun calismaGun)
        {
            if (ModelState.IsValid)
            {
                _context.Add(calismaGun);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DoktorId"] = new SelectList(_context.Doktors, "DoktorId", "DoktorId", calismaGun.DoktorId);
            return View(calismaGun);
        }

        // GET: CalismaGuns/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.CalismaGuns == null)
            {
                return NotFound();
            }

            var calismaGun = await _context.CalismaGuns.FindAsync(id);
            if (calismaGun == null)
            {
                return NotFound();
            }
            ViewData["DoktorId"] = new SelectList(_context.Doktors, "DoktorId", "DoktorId", calismaGun.DoktorId);
            return View(calismaGun);
        }

        // POST: CalismaGuns/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CalismaGunId,DoktorId,Gun,BaslangicSaati,BitisSaati")] CalismaGun calismaGun)
        {
            if (id != calismaGun.CalismaGunId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(calismaGun);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CalismaGunExists(calismaGun.CalismaGunId))
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
            ViewData["DoktorId"] = new SelectList(_context.Doktors, "DoktorId", "DoktorId", calismaGun.DoktorId);
            return View(calismaGun);
        }

        // GET: CalismaGuns/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.CalismaGuns == null)
            {
                return NotFound();
            }

            var calismaGun = await _context.CalismaGuns
                .Include(c => c.Doktor)
                .FirstOrDefaultAsync(m => m.CalismaGunId == id);
            if (calismaGun == null)
            {
                return NotFound();
            }

            return View(calismaGun);
        }

        // POST: CalismaGuns/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.CalismaGuns == null)
            {
                return Problem("Entity set 'hospitalContext.CalismaGuns'  is null.");
            }
            var calismaGun = await _context.CalismaGuns.FindAsync(id);
            if (calismaGun != null)
            {
                _context.CalismaGuns.Remove(calismaGun);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CalismaGunExists(int id)
        {
          return (_context.CalismaGuns?.Any(e => e.CalismaGunId == id)).GetValueOrDefault();
        }
    }
}
