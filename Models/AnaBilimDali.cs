using System;
using System.Collections.Generic;

namespace HospitalAppointmentSystem.Models
{
    public partial class AnaBilimDali
    {
        public AnaBilimDali()
        {
            Polikliniks = new HashSet<Poliklinik>();
        }

        public int AnaBilimDaliId { get; set; }
        public string Adi { get; set; } = null!;

        public virtual ICollection<Poliklinik> Polikliniks { get; set; }
    }
}
