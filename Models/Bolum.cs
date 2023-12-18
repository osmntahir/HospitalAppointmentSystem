using System;
using System.Collections.Generic;

namespace HospitalAppointmentSystem.Models
{
    public partial class Bolum
    {
        public Bolum()
        {
            Polikliniks = new HashSet<Poliklinik>();
        }

        public int BolumId { get; set; }
        public string BolumAdi { get; set; } = null!;
        public string? Aciklama { get; set; }

        public virtual ICollection<Poliklinik> Polikliniks { get; set; }
    }
}
