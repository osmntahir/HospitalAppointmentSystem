﻿using System;
using System.Collections.Generic;

namespace HospitalAppointmentSystem.Models
{
    public partial class Saatler
    {
        public Saatler()
        {
            Randevus = new HashSet<Randevu>();
        }

        public int SaatId { get; set; }
        public int CalismaGunId { get; set; }
        public TimeSpan SaatDilimi { get; set; }
        public bool? Secilebilir { get; set; }

        public virtual CalismaGun CalismaGun { get; set; } = null!;
        public virtual ICollection<Randevu> Randevus { get; set; }
    }
}
