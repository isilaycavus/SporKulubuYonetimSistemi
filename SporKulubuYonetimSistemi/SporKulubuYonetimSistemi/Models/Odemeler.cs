using System;
using System.Collections.Generic;

namespace SporKulubuYonetimSistemi.Models;

public partial class Odemeler
{
    public int OdemeId { get; set; }

    public int KullaniciId { get; set; }

    public int? UyelikId { get; set; }

    public decimal Tutar { get; set; }

    public DateTime? OdemeTarihi { get; set; }

    public int? KartId { get; set; }

    public virtual Kartlar? Kart { get; set; }

    public virtual Kullanicilar Kullanici { get; set; } = null!;

    public virtual Uyelikler? Uyelik { get; set; }
}
