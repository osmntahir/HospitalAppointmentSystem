using System;
using System.Collections.Generic;

namespace HospitalAppointmentSystem.Models
{
    public partial class CalismaGun
    {
        public int CalismaGunId { get; set; }
        public int? DoktorId { get; set; }
        public DateTime Gun { get; set; }
        public TimeSpan? BaslangicSaati { get; set; }
        public TimeSpan? BitisSaati { get; set; }
        public string? SaatDilimleri { get; set; }

        public virtual Doktor? Doktor { get; set; }
    }
}
