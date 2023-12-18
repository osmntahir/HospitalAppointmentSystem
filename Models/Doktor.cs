using System;
using System.Collections.Generic;

namespace HospitalAppointmentSystem.Models
{
    public partial class Doktor
    {
        public int DoktorId { get; set; }
        public string DoktorAdi { get; set; } = null!;
        public string Soyadi { get; set; } = null!;
        public string? Brans { get; set; }
        public int? PoliklinikId { get; set; }
        public string? Telefon { get; set; }
        public string? Mail { get; set; }
        public string? Adres { get; set; }
        public decimal? Maas { get; set; }
        public string? Cinsiyet { get; set; }

        public virtual Poliklinik? Poliklinik { get; set; }
    }
}
