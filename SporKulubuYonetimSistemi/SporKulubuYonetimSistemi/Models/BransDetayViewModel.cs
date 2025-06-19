// Models/BransDetayViewModel.cs
namespace SporKulubuYonetimSistemi.Models
{
    public class BransDetayViewModel
    {
        public Branslar Brans { get; set; } = null!;
        public List<Kullanicilar> Antrenorler { get; set; } = new();
    }
}
