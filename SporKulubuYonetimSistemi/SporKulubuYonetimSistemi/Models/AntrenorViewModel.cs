using System.Collections.Generic;

namespace SporKulubuYonetimSistemi.Models
{
    public class AntrenorViewModel
    {
        public string AdSoyad { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public List<string> Branslar { get; set; } = new();
    }
}
