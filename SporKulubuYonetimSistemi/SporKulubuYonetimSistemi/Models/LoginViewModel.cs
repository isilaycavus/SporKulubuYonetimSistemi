using System.ComponentModel.DataAnnotations;
namespace SporKulubuYonetimSistemi.Models
{
    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Sifre { get; set; }
    }
}
