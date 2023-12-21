using System;
using System.Collections.Generic;

namespace HospitalAppointmentSystem.Models
{
    public partial class HastaLogin
    {
        public int KullaniciId { get; set; }
        public string UserName { get; set; } = null!;
        public string Password { get; set; } = null!;

        public virtual Kullanici Hasta { get; set; } = null!;
    }
}
