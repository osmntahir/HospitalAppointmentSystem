using HospitalAppointmentSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace HospitalAppointmentSystem.Controllers
{
    public class ProfileController : Controller
    {
        private readonly hospitalContext _context;

        public ProfileController(hospitalContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            // Kullanıcının bilgilerini çek
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var kullanici = _context.Kullanicis.FirstOrDefault(x => x.KullaniciId == userId);

            // Kullanıcının aldığı randevuları çek
            var randevular = _context.Randevus
                .Include(r => r.CalismaGun)
                .Include(r => r.Doktor)
                .Include(r => r.Poliklinik)
                .Include(r => r.Saat)
                .Where(r => r.KullaniciId == userId)
                .ToList();

            // ViewModel oluştur
            var viewModel = new ProfileViewModel
            {
                Kullanici = kullanici,
                Randevular = randevular
            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult IptalEt(int randevuId)
        {
            var randevu = _context.Randevus
                .Include(r => r.Saat)
                .FirstOrDefault(r => r.RandevuId == randevuId);

            if (randevu != null)
            {
                // Randevuyu iptal etme işlemleri
                _context.Randevus.Remove(randevu);
                _context.SaveChanges();

                // İptal edilen randevunun saat dilimini tekrar seçilebilir yapma
                randevu.Saat.Secilebilir = true;
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }

}
