using System.ComponentModel.DataAnnotations;

namespace PetSpaAPI.DAL.Entities
{
    public class Employee : Person
    {
        [Display(Name = "Cargo")]
        public Charge Charge { get; set; }

        [Display(Name = "Id Cargo")]
        public int ChargeId { get; set; }

        [Display(Name = "Cargo")]
        [MaxLength(50, ErrorMessage = "El campo {0} debe tener máximo {1} caracteres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public DateTime BirthDate { get; set; }
    }
}
