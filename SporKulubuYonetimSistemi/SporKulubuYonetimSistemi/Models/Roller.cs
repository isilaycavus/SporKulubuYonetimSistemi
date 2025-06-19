using System;
using System.Collections.Generic;

namespace SporKulubuYonetimSistemi.Models;

public partial class Roller
{
    public int RolId { get; set; }

    public string RolAdi { get; set; } = null!;

    public virtual ICollection<Kullanicilar> Kullanicilars { get; set; } = new List<Kullanicilar>();
}
