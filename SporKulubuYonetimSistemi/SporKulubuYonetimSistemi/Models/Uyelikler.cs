using System;
using System.Collections.Generic;

namespace SporKulubuYonetimSistemi.Models;

public partial class Uyelikler
{
    public int UyelikId { get; set; }

    public int KullaniciId { get; set; }

    public int PlanId { get; set; }

    public DateOnly BaslangicTarihi { get; set; }

    public DateOnly? BitisTarihi { get; set; }

    public int KalanGirisHakki { get; set; }

    public decimal Bakiye { get; set; }

    public virtual Kullanicilar Kullanici { get; set; } = null!;

    public virtual ICollection<Odemeler> Odemelers { get; set; } = new List<Odemeler>();

    public virtual UyelikPlanlari Plan { get; set; } = null!;
}
