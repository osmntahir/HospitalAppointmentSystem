using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace HospitalAppointmentSystem.Models
{
    public partial class Kullanici
    {
        public Kullanici()
        {
            Randevus = new HashSet<Randevu>();
        }

        public int KullaniciId { get; set; }
        public string Email { get; set; } = null!;
        public string Sifre { get; set; } = null!;
        public string Adi { get; set; } = null!;
        public string Soyadi { get; set; } = null!;
        public string Cinsiyet { get; set; } = null!;
        public DateTime? DogumTarihi { get; set; }
        public string? TelefonNumarasi { get; set; }
        public string KullaniciRole { get; set; } = null!;

        public virtual ICollection<Randevu> Randevus { get; set; }
    }
}
