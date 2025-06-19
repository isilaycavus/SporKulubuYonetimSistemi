using System;
using System.Collections.Generic;

namespace SporKulubuYonetimSistemi.Models;

public partial class Branslar
{
    public int BransId { get; set; }

    public string BransAdi { get; set; } = null!;

    public virtual ICollection<KullaniciBran> KullaniciBrans { get; set; } = new List<KullaniciBran>();
}
