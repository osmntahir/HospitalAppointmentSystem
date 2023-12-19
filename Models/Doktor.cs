using System;
using System.Collections.Generic;

namespace HospitalAppointmentSystem.Models
{
    public partial class Doktor
    {
        public Doktor()
        {
            CalismaGuns = new HashSet<CalismaGun>();
            Randevus = new HashSet<Randevu>();
        }

        public int DoktorId { get; set; }
        public string Adi { get; set; } = null!;
        public string Soyadi { get; set; } = null!;
        public string? Brans { get; set; }
        public int? PoliklinikId { get; set; }
        public string? Telefon { get; set; }
        public string? Mail { get; set; }
        public string? Adres { get; set; }
        public decimal? Maas { get; set; }
        public string? Cinsiyet { get; set; }

        public virtual Poliklinik? Poliklinik { get; set; }
        public virtual ICollection<CalismaGun> CalismaGuns { get; set; }
        public virtual ICollection<Randevu> Randevus { get; set; }
    }
}
