using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SporKulubuYonetimSistemi.Models;
using System;
using System.Linq;
using System.Security.Claims;

namespace SporKulubuYonetimSistemi.Controllers
{
    [Authorize]   // → giriş yapılmamışsa otomatik /Account/Login’e yönlendirir
    public class MembershipController : Controller
    {
        private readonly SporKulubuContext _ctx;
        public MembershipController(SporKulubuContext ctx) => _ctx = ctx;

        /*──────────────────────────────────────────────────────────────
         * GET: /Membership/Buy?planId=1&branchId=4
         *  - Plan + Branş bilgilerini kullanıcıya onaylatır
         *──────────────────────────────────────────────────────────────*/
        public IActionResult Buy(int planId, int branchId)
        {
            var plan = _ctx.UyelikPlanlaris.Find(planId);
            var brans = _ctx.Branslars.Find(branchId);

            if (plan == null || brans == null)
                return NotFound();

            var vm = new BuyViewModel
            {
                PlanId = plan.PlanId,
                PlanAdi = plan.PlanAdi,
                Ucret = plan.GirisUcreti,
                BransId = brans.BransId,
                BransAdi = brans.BransAdi
            };
            return View(vm);   // Views/Membership/Buy.cshtml
        }

        /*──────────────────────────────────────────────────────────────
         * POST: /Membership/Buy
         *  - Üyelik kaydı + gerekirse branş eşlemesi oluşturur
         *──────────────────────────────────────────────────────────────*/
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult BuyConfirm(BuyViewModel model)
        {
            /* 1. Oturumdaki kullanıcı kimliği */
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null) return Challenge();        // cookie yok → Login

            if (!int.TryParse(userIdClaim.Value, out int userId))
                return BadRequest("Kullanıcı kimliği okunamadı.");

            /* 2. Plan & branş doğrulaması */
            var plan = _ctx.UyelikPlanlaris.Find(model.PlanId);
            var brans = _ctx.Branslars.Find(model.BransId);
            if (plan == null || brans == null) return NotFound();

            /* 3. Uyelik kaydı */
            var uyelik = new Uyelikler
            {
                KullaniciId = userId,
                PlanId = plan.PlanId,
                BaslangicTarihi = DateOnly.FromDateTime(DateTime.Today),
                // BitisTarihi: null → sınırsız veya ayrı hesaplanacak
                KalanGirisHakki = plan.ToplamGirisHakki,
                Bakiye = plan.GirisUcreti
            };
            _ctx.Uyeliklers.Add(uyelik);

            /* 4. Kullanıcı–branş eşlemesi yoksa ekle */
            bool zatenVar = _ctx.KullaniciBrans
                .Any(kb => kb.KullaniciId == userId && kb.BransId == model.BransId);

            if (!zatenVar)
            {
                _ctx.KullaniciBrans.Add(new KullaniciBran
                {
                    KullaniciId = userId,
                    BransId = model.BransId
                });
            }

            _ctx.SaveChanges();

            TempData["Mesaj"] = "Üyelik başarıyla oluşturuldu!";
            return RedirectToAction("Index", "Home");
        }
    }
}
