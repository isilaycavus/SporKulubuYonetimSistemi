namespace SporKulubuYonetimSistemi.Models
{
    public class BuyViewModel
    {
        public int PlanId { get; set; }
        public string PlanAdi { get; set; } = string.Empty;
        public decimal Ucret { get; set; }

        public int BransId { get; set; }
        public string BransAdi { get; set; } = string.Empty;
    }
}
