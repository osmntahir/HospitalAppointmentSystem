using System;
using System.Collections.Generic;

namespace HospitalAppointmentSystem.Models
{
    public partial class CalismaGun
    {
        public CalismaGun()
        {
            Randevus = new HashSet<Randevu>();
            Saatlers = new HashSet<Saatler>();
        }

        public int CalismaGunId { get; set; }
        public int? DoktorId { get; set; }
        public DateTime Gun { get; set; }
        public TimeSpan? BaslangicSaati { get; set; }
        public TimeSpan? BitisSaati { get; set; }

        public virtual Doktor? Doktor { get; set; }
        public virtual ICollection<Randevu> Randevus { get; set; }
        public virtual ICollection<Saatler> Saatlers { get; set; }
    }
}
