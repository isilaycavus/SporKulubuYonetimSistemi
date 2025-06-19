using System;
using System.Collections.Generic;

namespace SporKulubuYonetimSistemi.Models;

public partial class UyelikPlanlari
{
    public int PlanId { get; set; }

    public string PlanAdi { get; set; } = null!;

    public decimal GirisUcreti { get; set; }

    public int ToplamGirisHakki { get; set; }

    public virtual ICollection<Uyelikler> Uyeliklers { get; set; } = new List<Uyelikler>();
}
