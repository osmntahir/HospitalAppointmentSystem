﻿using System;
using System.Collections.Generic;

namespace HospitalAppointmentSystem.Models
{
    public partial class Hastum
    {
        public Hastum()
        {
            Randevus = new HashSet<Randevu>();
        }

        public int HastaId { get; set; }
        public string Adi { get; set; } = null!;
        public string Soyadi { get; set; } = null!;
        public string Cinsiyet { get; set; } = null!;
        public DateTime DogumTarihi { get; set; }
        public string TelefonNumarasi { get; set; } = null!;
        public string? Email { get; set; }

        public virtual HastaLogin? HastaLogin { get; set; }
        public virtual ICollection<Randevu> Randevus { get; set; }
    }
}
