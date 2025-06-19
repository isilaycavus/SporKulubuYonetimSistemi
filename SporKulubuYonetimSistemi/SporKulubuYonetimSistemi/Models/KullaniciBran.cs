using System;
using System.Collections.Generic;

namespace SporKulubuYonetimSistemi.Models;

public partial class KullaniciBran
{
    public int Id { get; set; }

    public int KullaniciId { get; set; }

    public int BransId { get; set; }

    public virtual Branslar Brans { get; set; } = null!;

    public virtual Kullanicilar Kullanici { get; set; } = null!;
}
