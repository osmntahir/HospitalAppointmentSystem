using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HospitalAppointmentSystem.Models;
using Microsoft.AspNetCore.Authorization;

namespace HospitalAppointmentSystem.Controllers
{
    public class RandevusController : Controller
    {
        private readonly hospitalContext _context;

        public RandevusController(hospitalContext context)
        {
            _context = context;
        }

        // GET: Randevus
        [Authorize(Roles = "A")]
        public async Task<IActionResult> Index()
        {
            var hospitalContext = _context.Randevus.Include(r => r.CalismaGun).Include(r => r.Doktor).Include(r => r.Kullanici).Include(r => r.Poliklinik).Include(r => r.Saat);
            return View(await hospitalContext.ToListAsync());
        }

        // GET: Randevus/Details/5
        [Authorize(Roles = "A")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Randevus == null)
            {
                return NotFound();
            }

            var randevu = await _context.Randevus
                .Include(r => r.CalismaGun)
                .Include(r => r.Doktor)
                .Include(r => r.Kullanici)
                .Include(r => r.Poliklinik)
                .Include(r => r.Saat)
                .FirstOrDefaultAsync(m => m.RandevuId == id);
            if (randevu == null)
            {
                return NotFound();
            }

            return View(randevu);
        }
        [Authorize(Roles = "A")]
        // GET: Randevus/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Randevus == null)
            {
                return NotFound();
            }

            var randevu = await _context.Randevus.FindAsync(id);
            if (randevu == null)
            {
                return NotFound();
            }
            ViewData["CalismaGunId"] = new SelectList(_context.CalismaGuns, "CalismaGunId", "CalismaGunId", randevu.CalismaGunId);
            ViewData["DoktorId"] = new SelectList(_context.Doktors, "DoktorId", "DoktorId", randevu.DoktorId);
            ViewData["KullaniciId"] = new SelectList(_context.Kullanicis, "KullaniciId", "KullaniciId", randevu.KullaniciId);
            ViewData["PoliklinikId"] = new SelectList(_context.Polikliniks, "PoliklinikId", "PoliklinikId", randevu.PoliklinikId);
            ViewData["SaatId"] = new SelectList(_context.Saatlers, "SaatId", "SaatId", randevu.SaatId);
            return View(randevu);
        }

        // POST: Randevus/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "A")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RandevuId,PoliklinikId,DoktorId,KullaniciId,CalismaGunId,SaatId,Aciklama")] Randevu randevu)
        {
            if (id != randevu.RandevuId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(randevu);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RandevuExists(randevu.RandevuId))
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
            ViewData["CalismaGunId"] = new SelectList(_context.CalismaGuns, "CalismaGunId", "CalismaGunId", randevu.CalismaGunId);
            ViewData["DoktorId"] = new SelectList(_context.Doktors, "DoktorId", "DoktorId", randevu.DoktorId);
            ViewData["KullaniciId"] = new SelectList(_context.Kullanicis, "KullaniciId", "KullaniciId", randevu.KullaniciId);
            ViewData["PoliklinikId"] = new SelectList(_context.Polikliniks, "PoliklinikId", "PoliklinikId", randevu.PoliklinikId);
            ViewData["SaatId"] = new SelectList(_context.Saatlers, "SaatId", "SaatId", randevu.SaatId);
            return View(randevu);
        }

        // GET: Randevus/Delete/5
        [Authorize(Roles = "A")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Randevus == null)
            {
                return NotFound();
            }

            var randevu = await _context.Randevus
                .Include(r => r.CalismaGun)
                .Include(r => r.Doktor)
                .Include(r => r.Kullanici)
                .Include(r => r.Poliklinik)
                .Include(r => r.Saat)
                .FirstOrDefaultAsync(m => m.RandevuId == id);
            if (randevu == null)
            {
                return NotFound();
            }

            return View(randevu);
        }

        // POST: Randevus/Delete/5
        [Authorize(Roles = "A")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Randevus == null)
            {
                return Problem("Entity set 'hospitalContext.Randevus' is null.");
            }

            var randevu = await _context.Randevus.FindAsync(id);
            if (randevu != null)
            {
                // SaatID'yi al
                // SaatID'yi al
                int? saatIdNullable = randevu.SaatId;

                // Eğer saatIdNullable null değilse, intValue değişkenine atama yap
                int intValue = saatIdNullable ?? 0;


                _context.Randevus.Remove(randevu);

                // İlgili saat dilimini tekrar "secilebilir" yap
                var saat = await _context.Saatlers.FindAsync(saatIdNullable);
                if (saat != null)
                {
                    saat.Secilebilir = true;
                }
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RandevuExists(int id)
        {
          return (_context.Randevus?.Any(e => e.RandevuId == id)).GetValueOrDefault();
        }
    }
}
