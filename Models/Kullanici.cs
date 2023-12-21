using System;
using System.Collections.Generic;

namespace HospitalAppointmentSystem.Models
{
    public partial class Kullanici
    {
        public int KullaniciId { get; set; }
        public string Email { get; set; } = null!;
        public string Sifre { get; set; } = null!;
        public string Adi { get; set; } = null!;
        public string Soyadi { get; set; } = null!;
        public string Cinsiyet { get; set; } = null!;
        public DateTime? DogumTarihi { get; set; }
        public string? TelefonNumarasi { get; set; }
        public string? KullaniciRol { get; set; }
    }
}
