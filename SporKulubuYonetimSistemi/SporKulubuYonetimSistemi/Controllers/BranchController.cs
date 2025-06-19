using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SporKulubuYonetimSistemi.Models;
using System.Security.Claims;

[Authorize]
public class BranchController : Controller
{
    private readonly SporKulubuContext _ctx;
    public BranchController(SporKulubuContext ctx) => _ctx = ctx;

    /* ==========  BRANŞ SEÇ  ========== */
    [HttpPost]
    public IActionResult Select(int bransId)
    {
        int kullaniciId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

        // Zaten seçili mi?
        bool mevcut = _ctx.KullaniciBrans
                          .Any(kb => kb.KullaniciId == kullaniciId && kb.BransId == bransId);
        if (mevcut)
        {
            TempData["Hata"] = "Bu branşı zaten seçtiniz.";
            return RedirectToAction("Index", "Home");
        }

        // Branş var mı?
        var brans = _ctx.Branslars.FirstOrDefault(b => b.BransId == bransId);
        if (brans is null) return NotFound();

        _ctx.KullaniciBrans.Add(new KullaniciBran
        {
            KullaniciId = kullaniciId,
            BransId = bransId
        });
        _ctx.SaveChanges();

        TempData["BransSecildi"] = brans.BransAdi;
        return RedirectToAction("Index", "Home");
    }

    /* ==========  BRANŞ İPTAL  ========== */
    [HttpPost]
    public IActionResult Remove(int bransId)
    {
        int kullaniciId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

        var rel = _ctx.KullaniciBrans
                      .Include(kb => kb.Brans)
                      .FirstOrDefault(kb => kb.KullaniciId == kullaniciId && kb.BransId == bransId);

        if (rel is null)
        {
            TempData["Hata"] = "Seçili olmayan branş iptal edilemez.";
            return RedirectToAction("Index", "Home");
        }

        string ad = rel.Brans.BransAdi;
        _ctx.KullaniciBrans.Remove(rel);
        _ctx.SaveChanges();

        TempData["BransSilindi"] = ad;
        return RedirectToAction("Index", "Home");
    }
}
