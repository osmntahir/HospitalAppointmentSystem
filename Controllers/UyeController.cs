using HospitalAppointmentSystem.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HospitalAppointmentSystem.Controllers
{
    public class UyeController : Controller
    {
        hospitalContext db;
        public UyeController(hospitalContext context) {
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
        public async Task<IActionResult> GirisYap(Kullanici k , string ReturnUrl)
        {
          //  hospitalContext db = new hospitalContext();
            var kullanici = db.Kullanicis.FirstOrDefault(x=>x.Email == k.Email &&
            x.Sifre==k.Sifre);

            if (kullanici != null) // boyle bir kullanici var
            {
                var talepler = new List<Claim>()
                {
                    new Claim(ClaimTypes.Name,k.Email.ToString()),
                    new Claim(ClaimTypes.Role, kullanici.KullaniciRole), // Add role information
                };
                ClaimsIdentity kimlik = new ClaimsIdentity(talepler, "Login");
                ClaimsPrincipal kural = new ClaimsPrincipal(kimlik);
                await HttpContext.SignInAsync(kural);
                if (!String.IsNullOrEmpty(ReturnUrl))
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
            await HttpContext.SignOutAsync
                (CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("GirisYap", "Uye");
        }
    }
}
