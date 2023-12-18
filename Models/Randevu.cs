using System;
using System.Collections.Generic;

namespace HospitalAppointmentSystem.Models
{
    public partial class Randevu
    {
        public int RandevuId { get; set; }
        public int? DoktorId { get; set; }
        public int? HastaId { get; set; }
        public DateTime RandevuTarihiSaat { get; set; }
        public string? Aciklama { get; set; }

        public virtual Hastum? Hasta { get; set; }
    }
}
