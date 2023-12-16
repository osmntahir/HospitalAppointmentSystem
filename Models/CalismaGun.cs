using System;
using System.Collections.Generic;

namespace HospitalAppointmentSystem.Models
{
    public partial class CalismaGun
    {
        public int CalismaGunId { get; set; }
        public int? DoktorId { get; set; }
        public int HaftaninGunuDeger { get; set; }
        public TimeSpan BaslangicSaati { get; set; }
        public TimeSpan BitisSaati { get; set; }

        public virtual Doktor? Doktor { get; set; }
    }
}
