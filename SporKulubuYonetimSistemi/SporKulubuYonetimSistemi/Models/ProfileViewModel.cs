namespace SporKulubuYonetimSistemi.Models
{
    public class ProfileViewModel
    {
        public string AdSoyad { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        public List<string> Branslar { get; set; } = new();   // kullanıcının kayıtlı olduğu branşlar

        // Son (veya aktif) üyelik bilgisi
        public string? PlanAdi { get; set; }
        public DateOnly? Baslangic { get; set; }
        public int? KalanHakki { get; set; }
        public decimal? Bakiye { get; set; }
    }


    public class BransUyelikBilgisi
    {
        public string BransAdi { get; set; } = string.Empty;
        public string PlanAdi { get; set; } = string.Empty;
        public DateOnly Baslangic { get; set; }
        public int KalanHakki { get; set; }
        public decimal Bakiye { get; set; }
    }

}
