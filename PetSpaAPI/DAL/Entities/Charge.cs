using System.ComponentModel.DataAnnotations;

namespace PetSpaAPI.DAL.Entities
{
    public class Charge
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Cargo")]
        [MaxLength(50, ErrorMessage = "El campo {0} debe tener máximo {1} caracteres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Name { get; set; }

        [Display(Name = "Descripción")]
        [MaxLength(200, ErrorMessage = "El campo {0} debe tener máximo {1} caracteres.")]
        public string? Description { get; set; }
    }
}