using System;
using System.Collections.Generic;

namespace SporKulubuYonetimSistemi.Models;

public partial class Kullanicilar
{
    public int KullaniciId { get; set; }

    public string AdSoyad { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Sifre { get; set; } = null!;

    public int RolId { get; set; }

    public virtual ICollection<Duyurular> Duyurulars { get; set; } = new List<Duyurular>();

    public virtual ICollection<GirisKayitlari> GirisKayitlaris { get; set; } = new List<GirisKayitlari>();

    public virtual ICollection<Kartlar> Kartlars { get; set; } = new List<Kartlar>();

    public virtual ICollection<KullaniciBran> KullaniciBrans { get; set; } = new List<KullaniciBran>();

    public virtual ICollection<Odemeler> Odemelers { get; set; } = new List<Odemeler>();

    public virtual Roller Rol { get; set; } = null!;

    public virtual ICollection<Uyelikler> Uyeliklers { get; set; } = new List<Uyelikler>();
}
