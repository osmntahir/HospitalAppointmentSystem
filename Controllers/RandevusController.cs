﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HospitalAppointmentSystem.Models;

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
        public async Task<IActionResult> Index()
        {
            var hospitalContext = _context.Randevus.Include(r => r.CalismaGun).Include(r => r.Doktor).Include(r => r.Kullanici).Include(r => r.Poliklinik).Include(r => r.Saat);
            return View(await hospitalContext.ToListAsync());
        }

        // GET: Randevus/Details/5
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
      
        public IActionResult GetDoktorList(int poliklinikId)
        {
            var doktorlar = _context.Doktors
                .Where(d => d.PoliklinikId == poliklinikId)
                .Select(d => new { value = d.DoktorId, text = d.Adi + " " + d.Soyadi })
                .ToList();

            return Json(doktorlar);
        }


        public IActionResult GetCalismaGunList(int doktorId)
        {
            var calismaGunler = _context.CalismaGuns
                .Where(c => c.DoktorId == doktorId)
                .Select(c => new { value = c.CalismaGunId, text = c.Gun.ToString("yyyy-MM-dd") })
                .ToList();

            return Json(calismaGunler);
        }

        public IActionResult GetSaatList(int calismaGunId)
        {
            var saatler = _context.Saatlers
                .Where(s => s.CalismaGunId == calismaGunId && (s.Secilebilir == true || !s.Secilebilir.HasValue))
                .Select(s => new { value = s.SaatId, text = s.SaatDilimi.ToString("HH:mm") })
                .ToList();

            return Json(saatler);
        }

        // Create fonksiyonları
        public IActionResult Create()
        {
            ViewBag.PoliklinikId = new SelectList(_context.Polikliniks, "PoliklinikId", "Adi");
            ViewBag.DoktorId = new SelectList(new List<SelectListItem>(), "Value", "Text"); // Boş bir doktor listesiyle başla
            ViewBag.CalismaGunId = new SelectList(new List<SelectListItem>(), "Value", "Text"); // Boş bir çalışma günü listesiyle başla
            ViewBag.SaatId = new SelectList(new List<SelectListItem>(), "Value", "Text"); // Boş bir saat listesiyle başla

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RandevuId,PoliklinikId,DoktorId,KullaniciId,CalismaGunId,SaatId,Aciklama")] Randevu randevu)
        {
            if (ModelState.IsValid)
            {
                _context.Add(randevu);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // ModelState geçersizse, tekrar seçimleri yüklememiz gerekiyor
            ViewBag.PoliklinikId = new SelectList(_context.Polikliniks, "PoliklinikId", "Adi", randevu.PoliklinikId);
            ViewBag.DoktorId = new SelectList(_context.Doktors.Where(d => d.PoliklinikId == randevu.PoliklinikId), "DoktorId", "Adi", randevu.DoktorId);
            ViewBag.CalismaGunId = new SelectList(_context.CalismaGuns.Where(c => c.DoktorId == randevu.DoktorId), "CalismaGunId", "Gun", randevu.CalismaGunId);
            ViewBag.SaatId = new SelectList(_context.Saatlers
                .Where(s => s.CalismaGunId == randevu.CalismaGunId && (s.Secilebilir == true || !s.Secilebilir.HasValue))
                .Select(s => new SelectListItem { Value = s.SaatId.ToString(), Text = s.SaatDilimi.ToString("HH:mm") }), "Value", "Text", randevu.SaatId);

            return View(randevu);
        }



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
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Randevus == null)
            {
                return Problem("Entity set 'hospitalContext.Randevus'  is null.");
            }
            var randevu = await _context.Randevus.FindAsync(id);
            if (randevu != null)
            {
                _context.Randevus.Remove(randevu);
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
