using System;
using System.Collections.Generic;

namespace SporKulubuYonetimSistemi.Models;

public partial class Kartlar
{
    public int KartId { get; set; }

    public int KullaniciId { get; set; }

    public string KartNumarasi { get; set; } = null!;

    public DateTime? KayitTarihi { get; set; }

    public virtual Kullanicilar Kullanici { get; set; } = null!;

    public virtual ICollection<Odemeler> Odemelers { get; set; } = new List<Odemeler>();
}
