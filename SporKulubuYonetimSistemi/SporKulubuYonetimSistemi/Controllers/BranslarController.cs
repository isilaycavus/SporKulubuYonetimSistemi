using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SporKulubuYonetimSistemi.Models;
using System.Linq;

namespace SporKulubuYonetimSistemi.Controllers
{
    public class BranslarController : Controller
    {
        private readonly SporKulubuContext _ctx;
        public BranslarController(SporKulubuContext ctx) => _ctx = ctx;

        // /Branslar
        public IActionResult Index()
        {
            var branslar = _ctx.Branslars
                               .OrderBy(b => b.BransAdi)
                               .ToList();
            return View(branslar);
        }

        // /Branslar/Detay/5
        public IActionResult Detay(int id)
        {
            var brans = _ctx.Branslars.FirstOrDefault(b => b.BransId == id);
            if (brans is null) return NotFound();

            // “Antrenör” rolüne sahip ve bu branşa atanmış kullanıcılar
            var antrenorler = _ctx.KullaniciBrans
                                  .Where(kb => kb.BransId == id &&
                                               kb.Kullanici.Rol.RolAdi == "Antrenör")
                                  .Include(kb => kb.Kullanici)
                                  .Select(kb => kb.Kullanici)
                                  .Distinct()
                                  .ToList();

            var vm = new BransDetayViewModel
            {
                Brans = brans,
                Antrenorler = antrenorler
            };
            return View(vm);
        }
    }
}
