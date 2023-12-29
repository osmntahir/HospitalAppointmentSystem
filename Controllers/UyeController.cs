using HospitalAppointmentSystem.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
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
            return RedirectToAction("GirisYap", "Uye");
        }
    }
}
