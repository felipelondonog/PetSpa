using System.ComponentModel.DataAnnotations;

namespace PetSpaAPI.DAL.Entities
{
    public class Appointment
    {
        [Required]
        public Guid Id { get; set; }

        [Display(Name = "Fecha")]
        [Required]
        public DateTime Date { get; set; }

        [Display(Name = "Empleado")]
        public Employee Employee { get; set; }

        [Display(Name = "CC Empleado")]
        public int EmployeeCc { get; set; }

        [Display(Name = "Servicio")]
        public Service Service { get; set; }

        [Display(Name = "ID Servicio")]
        public int ServiceId { get; set; }

        [Display(Name = "Mascota")]
        public Pet Pet { get; set; }

        [Display(Name = "ID Mascota")]
        public int PetId { get; set; }
    }
}
