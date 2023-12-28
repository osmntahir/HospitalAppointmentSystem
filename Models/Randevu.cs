using System;
using System.Collections.Generic;

namespace HospitalAppointmentSystem.Models
{
    public partial class Randevu
    {
        public int RandevuId { get; set; }
        public int? PoliklinikId { get; set; }
        public int? DoktorId { get; set; }
        public int? KullaniciId { get; set; }
        public int? CalismaGunId { get; set; }
        public int? SaatId { get; set; }
        public string? Aciklama { get; set; }

        public virtual CalismaGun? CalismaGun { get; set; }
        public virtual Doktor? Doktor { get; set; }
        public virtual Kullanici? Kullanici { get; set; }
        public virtual Poliklinik? Poliklinik { get; set; }
        public virtual Saatler? Saat { get; set; }
    }
}
