using Microsoft.AspNetCore.Mvc;

namespace SporKulubuYonetimSistemi.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
