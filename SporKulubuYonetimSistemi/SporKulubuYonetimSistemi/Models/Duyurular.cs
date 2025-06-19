using System;
using System.Collections.Generic;

namespace SporKulubuYonetimSistemi.Models;

public partial class Duyurular
{
    public int DuyuruId { get; set; }

    public string Baslik { get; set; } = null!;

    public string Icerik { get; set; } = null!;

    public DateTime? Tarih { get; set; }

    public int? KullaniciId { get; set; }

    public virtual Kullanicilar? Kullanici { get; set; }
}
