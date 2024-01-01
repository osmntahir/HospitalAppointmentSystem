using HospitalAppointmentSystem.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace HospitalAppointmentSystem.Controllers
{
    public class UyeController : Controller
    {
        private readonly hospitalContext db;

        public UyeController(hospitalContext context)
        {
            db = context;
        }

        public IActionResult SifreDegistir()
        {
            return View();
        }

       [HttpPost]
public IActionResult SifreDegistir(SifreDegistirViewModel model)
{
    if (ModelState.IsValid)
    {
                var userEmail = HttpContext.User.FindFirstValue(ClaimTypes.Email);
                var kullanici = db.Kullanicis.FirstOrDefault(x => x.Email == userEmail);


                if (kullanici != null && kullanici.Sifre == model.EskiSifre)
        {
            // Eski şifre doğrulandı, yeni şifreyi kaydet
            kullanici.Sifre = model.YeniSifre;
            db.SaveChanges();

            return RedirectToAction("Index", "Home");
        }
        else
        {
            ModelState.AddModelError(string.Empty, "Eski şifreniz yanlış.");
        }
    }

    return View(model);
}



        public IActionResult GirisYap()
        {
            ClaimsPrincipal claimUser = HttpContext.User;

            if (claimUser.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GirisYap(Kullanici k, string ReturnUrl)
        {
            var kullanici = db.Kullanicis.FirstOrDefault(x => x.Email == k.Email && x.Sifre == k.Sifre);

            if (kullanici != null)
            {
                var claims = new List<Claim>
        {
           new Claim(ClaimTypes.NameIdentifier, kullanici.KullaniciId.ToString()), // Include user ID in claims
            new Claim(ClaimTypes.Email, k.Email),
            new Claim(ClaimTypes.Role, kullanici.KullaniciRole),
        };

                ClaimsIdentity identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                ClaimsPrincipal principal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync(principal);

                if (!string.IsNullOrEmpty(ReturnUrl))
                {
                    return Redirect(ReturnUrl);
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }

            return View();
        }


        public async Task<IActionResult> CikisYap()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
    }
}
