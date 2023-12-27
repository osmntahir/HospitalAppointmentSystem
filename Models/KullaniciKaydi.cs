using System;
using System.Collections.Generic;

namespace HospitalAppointmentSystem.Models
{
    public partial class KullaniciKaydi
    {

        public string Email { get; set; } = null!;
        public string Sifre { get; set; } = null!;
        public string SifreOnayla { get; set; } = null!;
        public string Adi { get; set; } = null!;
        public string Soyadi { get; set; } = null!;
        public string Cinsiyet { get; set; } = null!;
        public DateTime? DogumTarihi { get; set; }
        public string? TelefonNumarasi { get; set; }
      

      
    }
}
