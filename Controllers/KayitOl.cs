// KayitOlController.cs
using Microsoft.AspNetCore.Mvc;
using HospitalAppointmentSystem.Models;
using System;

public class KayitOlController : Controller
{
    private readonly hospitalContext _context;

    public KayitOlController(hospitalContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }

    // POST: KayitOl
    [HttpPost]

    public IActionResult Index(Kullanici viewModel)
    {
        if (ModelState.IsValid)
        {
            try
            {
                // Email kontrolü yapılması
                var existingUser = _context.Kullanicis.FirstOrDefault(u => u.Email == viewModel.Email);
                if (existingUser != null)
                {
                    ModelState.AddModelError("Email", "Bu e-posta adresi zaten kullanılmakta.");
                    return View(viewModel);
                }

                // ViewModel'den Kullanici nesnesine veri aktarımı
                var k = new Kullanici
                {
                    Adi = viewModel.Adi,
                    Soyadi = viewModel.Soyadi,
                    Email = viewModel.Email,
                    Sifre = viewModel.Sifre,
                    DogumTarihi = viewModel.DogumTarihi,
                    TelefonNumarasi = viewModel.TelefonNumarasi,
                    Cinsiyet = viewModel.Cinsiyet,
                    KullaniciRole = "U"
                };

                // Kullanıcıyı veritabanına ekle
                _context.Kullanicis.Add(k);
                _context.SaveChanges();

                // Başarılı kayıt olduktan sonra yapılacak işlemler
                // Örneğin, giriş sayfasına yönlendirme yapılabilir.
                return RedirectToAction("Login", "Account");
            }
            catch (Exception ex)
            {
                // Veritabanına ekleme sırasında bir hata olursa burada ele alabilirsiniz.
                ModelState.AddModelError("", "Kayıt sırasında bir hata oluştu. Lütfen tekrar deneyin.");
                return View(viewModel);
            }
        }

        // ModelState.IsValid false ise buraya düşer
        return View(viewModel);
    }
}
