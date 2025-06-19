using System.ComponentModel.DataAnnotations;

namespace SporKulubuYonetimSistemi.Models
{
    public class RegisterViewModel
    {
        [Required]
        public string AdSoyad { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Sifre { get; set; }

        [Required]
        [Compare("Sifre", ErrorMessage = "Şifreler uyuşmuyor.")]
        [DataType(DataType.Password)]
        public string SifreTekrar { get; set; }
    }
}
