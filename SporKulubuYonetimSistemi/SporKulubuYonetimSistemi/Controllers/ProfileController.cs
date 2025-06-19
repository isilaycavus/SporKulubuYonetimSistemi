using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SporKulubuYonetimSistemi.Models;
using System.Linq;
using System.Security.Claims;

namespace SporKulubuYonetimSistemi.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly SporKulubuContext _ctx;
        public ProfileController(SporKulubuContext ctx) => _ctx = ctx;

        public IActionResult Index()
        {
            // — Kullanıcı kimliği —
            var idStr = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!int.TryParse(idStr, out int userId))
                return RedirectToAction("Login", "Account");

            // — Kullanıcı temel bilgileri + branş listesi —
            var kullanici = _ctx.Kullanicilars
                                .Include(k => k.KullaniciBrans)
                                    .ThenInclude(kb => kb.Brans)
                                .FirstOrDefault(k => k.KullaniciId == userId);

            if (kullanici is null) return NotFound();

            // — Son (veya aktif) üyelik planını çek —
            var uyelik = _ctx.Uyeliklers
                             .Include(u => u.Plan)
                             .Where(u => u.KullaniciId == userId)
                             .OrderByDescending(u => u.BaslangicTarihi)   // en yeni
                             .FirstOrDefault();                           // yoksa null

            var vm = new ProfileViewModel
            {
                AdSoyad = kullanici.AdSoyad,
                Email = kullanici.Email,
                Branslar = kullanici.KullaniciBrans
                                       .Select(kb => kb.Brans.BransAdi)
                                       .ToList(),

                PlanAdi = uyelik?.Plan?.PlanAdi,
                Baslangic = uyelik?.BaslangicTarihi,
                KalanHakki = uyelik?.KalanGirisHakki,
                Bakiye = uyelik?.Bakiye
            };

            return View(vm);
        }
    }
}
