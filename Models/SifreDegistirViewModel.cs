namespace HospitalAppointmentSystem.Models
{
    using System.ComponentModel.DataAnnotations;

    public class SifreDegistirViewModel
    {
        [Required(ErrorMessage = "Eski şifre gereklidir.")]
        public string EskiSifre { get; set; }

        [Required(ErrorMessage = "Yeni şifre gereklidir.")]
        public string YeniSifre { get; set; }

        [Compare("YeniSifre", ErrorMessage = "Yeni şifreler uyuşmuyor.")]
        public string YeniSifreTekrar { get; set; }
    }
}
