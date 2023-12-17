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
        public int? BolumId { get; set; }
        public int? PoliklinikId { get; set; }
        public string DoktorAdi { get; set; } = null!;
        public string? Telefon { get; set; }

        public virtual Bolum? Bolum { get; set; }
        public virtual Poliklinik? Poliklinik { get; set; }
        public virtual ICollection<CalismaGun> CalismaGuns { get; set; }
        public virtual ICollection<Randevu> Randevus { get; set; }
    }
}
