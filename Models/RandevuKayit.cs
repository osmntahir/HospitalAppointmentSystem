using System;
using System.ComponentModel.DataAnnotations;

namespace HospitalAppointmentSystem.Models
{
    public class RandevuKayit
    {
        [Required(ErrorMessage = "Please select a polyclinic.")]
        public int PoliklinikId { get; set; }

        [Required(ErrorMessage = "Please select a doctor.")]
        public int DoktorId { get; set; }

        [Required(ErrorMessage = "Please select a working day.")]
        public int CalismaGunID { get; set; }

        [Required(ErrorMessage = "Please select a time slot.")]
        public int SaatDilimi { get; set; }

        public string Aciklama { get; set; }
    }
}
