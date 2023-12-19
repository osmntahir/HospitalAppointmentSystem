﻿using System;
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
        public int? AnaBilimDaliId { get; set; }
        public string Adi { get; set; } = null!;

        public virtual AnaBilimDali? AnaBilimDali { get; set; }
        public virtual ICollection<Doktor> Doktors { get; set; }
    }
}
