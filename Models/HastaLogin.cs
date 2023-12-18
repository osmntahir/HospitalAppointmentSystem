using System;
using System.Collections.Generic;

namespace HospitalAppointmentSystem.Models
{
    public partial class HastaLogin
    {
        public int HastaId { get; set; }
        public string UserName { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;

        public virtual Hastum Hasta { get; set; } = null!;
    }
}
