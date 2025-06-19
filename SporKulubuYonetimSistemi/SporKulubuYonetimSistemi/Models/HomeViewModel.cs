using System.Collections.Generic;

namespace SporKulubuYonetimSistemi.Models
{
    public class HomeViewModel
    {
        public List<Duyurular> Duyurular { get; set; } = new();
        public List<UyelikPlanlari> Planlar { get; set; } = new();
        public List<Branslar> Branslar { get; set; } = new();
    }
}
