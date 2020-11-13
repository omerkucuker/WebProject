using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebProject.Models;

namespace WebProject.Controllers
{
    public class LoginController : Controller
    {
        Context c = new Context();


        [HttpGet]
        public IActionResult GirisYap()  //httpget neden kondu bu yokken hata veriyordu metodu silince veya http ekleyince düzeldi
        {
            return View();
        }

        public async Task <IActionResult> GirisYap(Admin a)
        {
            var bilgiler = c.Admins.FirstOrDefault(x => x.Kullanici == a.Kullanici &&
            x.Sifre == a.Sifre);
            if (bilgiler != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, a.Kullanici)
                };

                var useridentity = new ClaimsIdentity(claims, "Login");
                ClaimsPrincipal principal = new ClaimsPrincipal(useridentity);
                await HttpContext.SignInAsync(principal);

                return RedirectToAction("Index", "Personelim");
            }

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> CikisYap()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction( "GirisYap", "Login");
        }

    }
}
