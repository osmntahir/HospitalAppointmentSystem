using System;
using System.Collections.Generic;

namespace HospitalAppointmentSystem.Models
{
    public partial class Poliklinik
    {
        public Poliklinik()
        {
            Doktors = new HashSet<Doktor>();
        }

        public int PoliklinikId { get; set; }
        public int? BolumId { get; set; }
        public string PoliklinikAdi { get; set; } = null!;

        public virtual Bolum? Bolum { get; set; }
        public virtual ICollection<Doktor> Doktors { get; set; }
    }
}
