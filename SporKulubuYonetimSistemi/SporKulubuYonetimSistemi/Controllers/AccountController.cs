using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SporKulubuYonetimSistemi.Models;
using System.Security.Claims;
using System.Linq;

namespace SporKulubuYonetimSistemi.Controllers
{
    public class AccountController : Controller
    {
        private readonly SporKulubuContext _context;
        public AccountController(SporKulubuContext context) => _context = context;

        /* --------------------  GET: Login  -------------------- */
        [HttpGet]
        public IActionResult Login() => View();

        /* --------------------  POST: Login  ------------------- */
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var kullanici = _context.Set<Kullanicilar>()
                .Include(k => k.Uyeliklers).ThenInclude(u => u.Plan)
                .Include(k => k.KullaniciBrans).ThenInclude(kb => kb.Brans)
                .Include(k => k.Rol)
                .FirstOrDefault(k => k.Email == model.Email && k.Sifre == model.Sifre);

            if (kullanici == null)
            {
                ViewBag.Hata = "Geçersiz e-posta veya şifre.";
                return View(model);
            }

            /* aktif üyelik + branş listesi */
            var today = DateOnly.FromDateTime(DateTime.Today);
            var aktifUyelik = kullanici.Uyeliklers
                               .FirstOrDefault(u => u.BitisTarihi == null || u.BitisTarihi >= today);

            string planAdi = aktifUyelik?.Plan?.PlanAdi ?? "Plan seçilmedi";
            string branslar = string.Join(", ", kullanici.KullaniciBrans.Select(b => b.Brans.BransAdi));

            /* claim listesi */
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name           , kullanici.AdSoyad),
                new Claim(ClaimTypes.Email          , kullanici.Email),
                new Claim(ClaimTypes.Role           , kullanici.Rol.RolAdi),
                new Claim(ClaimTypes.NameIdentifier , kullanici.KullaniciId.ToString()), // ↔️
                new Claim("Plan"                    , planAdi),
                new Claim("Brans"                   , branslar)
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
            return RedirectToAction("Index", "Home");
        }

        /* --------------------  GET: Register  ----------------- */
        [HttpGet]
        public IActionResult Register() => View();

        /* --------------------  POST: Register  ---------------- */
        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            if (_context.Set<Kullanicilar>().Any(k => k.Email == model.Email))
            {
                ViewBag.Hata = "Bu e-posta zaten kayıtlı.";
                return View(model);
            }

            int uyeRolId = _context.Set<Roller>()
                                   .First(r => r.RolAdi == "Üye").RolId;

            var yeni = new Kullanicilar
            {
                AdSoyad = model.AdSoyad,
                Email = model.Email,
                Sifre = model.Sifre,
                RolId = uyeRolId
            };
            _context.Add(yeni);
            _context.SaveChanges();

            return RedirectToAction("Login");
        }

        /* --------------------  Logout  ------------------------ */
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
