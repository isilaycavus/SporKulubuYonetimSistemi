using System;
using System.Collections.Generic;

namespace SporKulubuYonetimSistemi.Models;

public partial class GirisKayitlari
{
    public int KayitId { get; set; }

    public int KullaniciId { get; set; }

    public DateTime? GirisTarihi { get; set; }

    public virtual Kullanicilar Kullanici { get; set; } = null!;
}
