using System;
using System.Collections.Generic;

namespace HospitalAppointmentSystem.Models
{
    public partial class Admin
    {
        public int AdminId { get; set; }
        public string UserName { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
    }
}
