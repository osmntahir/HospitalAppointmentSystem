using HospitalAppointmentSystem.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

[Authorize(Roles = "U")]
public class RandevuController : Controller
{

    private readonly ILogger<RandevuController> _logger;
    private readonly hospitalContext _context;

    public RandevuController(ILogger<RandevuController> logger, hospitalContext context)
    {
        _logger = logger;
        _context = context;
        _context.Database.EnsureCreated();
    }

    public IActionResult Index()
    {
        var poliklinikler = _context.Polikliniks.ToList();
        var doktorlar = new List<Doktor>();

        poliklinikler.Add(new Poliklinik
        {
            PoliklinikId = 0,
            Adi = "--Poliklinik Seçin--"
        });

        doktorlar.Add(new Doktor
        {
            DoktorId = 0,
            Adi = "--Doktor Seçin--",
            Soyadi = ""
        });

        ViewData["PoliklinikData"] = new SelectList(poliklinikler.OrderBy(p => p.PoliklinikId), "PoliklinikId", "Adi");
        if (doktorlar != null && doktorlar.Any())
        {
            ViewData["DoktorData"] = new SelectList(doktorlar, "DoktorId", "Adi");
        }
        else
        {
            ViewData["DoktorData"] = new SelectList(new List<SelectListItem>()); // Boş bir doktor listesi
        }


        string host = $"{Request.Scheme}://{Request.Host}{Request.PathBase}/";
        ViewData["BaseUrl"] = host;

        return View();
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
           .Select(s => new { value = s.SaatId, text = s.SaatDilimi.ToString() })
            .ToList();

        return Json(saatler);
    }
    public IActionResult Bilgilendirme()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("RandevuId,PoliklinikId,DoktorId,KullaniciId,CalismaGunId,SaatId,Aciklama")] Randevu randevu)
    {
        if (ModelState.IsValid)
        {
            if (User.Identity.IsAuthenticated)
            {
                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

                if (userIdClaim != null)
                {
                    var userId = userIdClaim.Value;

                    // Kullanıcının daha önce aldığı randevuları kontrol et
                    var kullanici = await _context.Kullanicis
                        .Include(k => k.Randevus)
                        .FirstOrDefaultAsync(k => k.KullaniciId == int.Parse(userId));

                    if (kullanici != null)
                    {
                        // Kullanıcının aynı poliklinikten bir randevusu var mı kontrol et
                        if (kullanici.Randevus.Any(r => r.PoliklinikId == randevu.PoliklinikId))
                        {
                            // Kullanıcının aynı poliklinikten tekrar randevu almak istemesi durumunda bilgilendirme sayfasına yönlendir
                            return RedirectToAction("Bilgilendirme");
                        }
                        else
                        {
                            // Kullanıcının daha önce bir randevusu yok, yeni randevu oluşturabilir.
                            randevu.KullaniciId = int.Parse(userId);
                            _context.Add(randevu);

                            // Seçilen saat dilimini bul
                            var selectedSaat = _context.Saatlers.FirstOrDefault(s => s.SaatId == randevu.SaatId);

                            if (selectedSaat != null)
                            {
                                // Seçilen saat dilimini artık seçilemez yap
                                selectedSaat.Secilebilir = false;
                            }

                            await _context.SaveChangesAsync();
                            return RedirectToAction(nameof(Index));
                        }
                    }
                }
            }
        }
        // ModelState geçersizse, tekrar seçimleri yüklememiz gerekiyor
        var poliklinikler = _context.Polikliniks.ToList();
        var doktorlar = _context.Doktors.Where(d => d.PoliklinikId == randevu.PoliklinikId).ToList();

        poliklinikler.Add(new Poliklinik
        {
            PoliklinikId = 0,
            Adi = "--Poliklinik Seçin--"
        });

        doktorlar.Add(new Doktor
        {
            DoktorId = 0,
            Adi = "--Doktor Seçin--",
            Soyadi = ""
        });

        ViewData["PoliklinikData"] = new SelectList(poliklinikler.OrderBy(p => p.PoliklinikId), "PoliklinikId", "Adi", randevu.PoliklinikId);
        ViewData["DoktorData"] = new SelectList(doktorlar.OrderBy(d => d.DoktorId), "DoktorId", "FullName", randevu.DoktorId);
        ViewData["CalismaGunData"] = new SelectList(_context.CalismaGuns.Where(c => c.DoktorId == randevu.DoktorId).OrderBy(c => c.CalismaGunId), "CalismaGunId", "Gun", randevu.CalismaGunId);
        ViewData["SaatData"] = new SelectList(_context.Saatlers
            .Where(s => s.CalismaGunId == randevu.CalismaGunId && (s.Secilebilir == true || !s.Secilebilir.HasValue))
            .OrderBy(s => s.SaatId)
            .Select(s => new SelectListItem { Value = s.SaatId.ToString(), Text = s.SaatDilimi.ToString("HH:mm") }), "Value", "Text", randevu.SaatId);

        return View(randevu);
    }
}
