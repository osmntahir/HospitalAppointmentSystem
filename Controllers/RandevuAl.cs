using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HospitalAppointmentSystem.Models;
using System.Security.Claims;

public class RandevuAlController : Controller
{
    private readonly hospitalContext _context;

    public RandevuAlController(hospitalContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        // Poliklinikleri dropdown list için hazırla
        ViewBag.Poliklinikler = new SelectList(_context.Polikliniks, "PoliklinikId", "Adi");

        return View();
    }

    [HttpPost]
    public IActionResult GetDoktorlar(int poliklinikId)
    {
        try
        {
            // Seçilen poliklinikteki doktorları getir
            var doktorlar = _context.Doktors
                .Where(d => d.PoliklinikId == poliklinikId)
                .Select(d => new { DoktorId = d.DoktorId, AdSoyad = d.Adi + " " + d.Soyadi })
                .ToList();

            // JSON formatında doktorları geri döndür
            return Json(doktorlar);
        }
        catch (Exception ex)
        {
            // Hata durumunda bir mesaj döndür
            return Json(new { success = false, message = "Doktorlar getirilirken bir hata oluştu.", error = ex.Message });
        }
    }


    // Diğer action'lar ve kodlar buraya eklenecek...

    [HttpPost]
    public IActionResult GetCalismaGunleri(int doktorId)
    {
        // Seçilen doktorun çalışma günlerini getir
        var calismaGunleri = _context.CalismaGuns
            .Where(c => c.DoktorId == doktorId)
            .Select(c => new { c.CalismaGunId, c.Gun, c.BaslangicSaati, c.BitisSaati })
            .ToList();

        // JSON formatında çalışma günlerini geri döndür
        return Json(calismaGunleri);
    }

    [HttpPost]
    public async Task<IActionResult> RandevuOlustur(int calismaGunId, [FromBody] RandevuKayit randevuKayit)
    {
        try
        {
            // Seçilen çalışma gününe ait randevu oluşturma işlemleri

            // 1. Önce seçilen çalışma gününü bul
            var calismaGun = await _context.CalismaGuns.FindAsync(calismaGunId);
            if (calismaGun == null)
            {
                return Json(new { success = false, message = "Çalışma günü bulunamadı." });
            }

            // 2. Randevu oluştur
            var randevu = new Randevu
            {
                PoliklinikId = randevuKayit.PoliklinikId,
                DoktorId = randevuKayit.DoktorId,
                KullaniciId = GetLoggedInUserId(), // Get the user ID here
                RandevuTarihiSaat = calismaGun.Gun.AddHours(randevuKayit.SaatDilimi),
                Aciklama = randevuKayit.Aciklama
            };

            // 3. Oluşturulan randevuyu ekleyip kaydet
            _context.Randevus.Add(randevu);
            await _context.SaveChangesAsync();

            return Json(new { success = true, message = "Randevu oluşturuldu." });
        }
        catch (Exception ex)
        {
            // Hata durumunda bir mesaj döndür
            return Json(new { success = false, message = "Randevu oluşturulurken bir hata oluştu.", error = ex.Message });
        }
    }

    private int GetLoggedInUserId()
    {
        // Retrieve the user ID from claims
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

        if (userIdClaim != null && int.TryParse(userIdClaim.Value, out int userId))
        {
            return userId;
        }

        // Return a default value or handle the case where the user ID is not available
        return 0;
    }


}
