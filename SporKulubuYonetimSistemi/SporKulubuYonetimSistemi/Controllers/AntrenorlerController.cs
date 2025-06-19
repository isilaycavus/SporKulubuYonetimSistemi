using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SporKulubuYonetimSistemi.Models;
using System.Linq;

namespace SporKulubuYonetimSistemi.Controllers
{
    public class AntrenorlerController : Controller
    {
        private readonly SporKulubuContext _context;
        public AntrenorlerController(SporKulubuContext context) => _context = context;

        public IActionResult Index()
        {
            var antrenorler = _context.Kullanicilars
                .Include(k => k.Rol)
                .Include(k => k.KullaniciBrans)              // ✔️ DÜZELTİLDİ
                    .ThenInclude(kb => kb.Brans)
                .Where(k => k.Rol.RolAdi == "Antrenör")
                .Select(k => new AntrenorViewModel
                {
                    AdSoyad = k.AdSoyad,
                    Email = k.Email,
                    Branslar = k.KullaniciBrans               // ✔️ DÜZELTİLDİ
                                   .Select(kb => kb.Brans.BransAdi)
                                   .ToList()
                })
                .ToList();

            return View(antrenorler);
        }
    }
}
