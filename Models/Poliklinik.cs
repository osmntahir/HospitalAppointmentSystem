using System;
using System.Collections.Generic;

namespace HospitalAppointmentSystem.Models
{
    public partial class Poliklinik
    {
        public Poliklinik()
        {
            Doktors = new HashSet<Doktor>();
            Randevus = new HashSet<Randevu>();
        }

        public int PoliklinikId { get; set; }
        public string Adi { get; set; } = null!;

        public virtual ICollection<Doktor> Doktors { get; set; }
        public virtual ICollection<Randevu> Randevus { get; set; }
    }
}
