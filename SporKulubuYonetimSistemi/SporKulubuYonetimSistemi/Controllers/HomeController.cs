using Microsoft.AspNetCore.Mvc;
using SporKulubuYonetimSistemi.Models;
using System.Linq;
using System.Globalization;

namespace SporKulubuYonetimSistemi.Controllers
{
    public class HomeController : Controller
    {
        private readonly SporKulubuContext _context;

        public HomeController(SporKulubuContext context) => _context = context;

        public IActionResult Index()
        {
            var vm = new HomeViewModel
            {
                Duyurular = _context.Duyurulars
                                    .OrderByDescending(d => d.Tarih)
                                    .Take(5)
                                    .ToList(),

                Planlar = _context.UyelikPlanlaris.ToList(),

                Branslar = _context.Branslars
                                    .OrderBy(b => b.BransAdi)
                                    .ToList()
            };

            return View(vm);
        }
    }
}
