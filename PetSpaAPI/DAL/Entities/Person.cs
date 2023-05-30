using System.ComponentModel.DataAnnotations;

namespace PetSpaAPI.DAL.Entities
{
    public class Person
    {
        [Key]
        public int Cc { get; set; }

        [Display(Name = "Nombre")]
        [MaxLength(100, ErrorMessage = "El campo {0} debe tener máximo {1} caracteres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Name { get; set; }

        [Display(Name = "Teléfono")]
        public int? Phone { get; set; }

        [Display(Name = "E-mail")]
        [MaxLength(100, ErrorMessage = "El campo {0} debe tener máximo {1} caracteres.")]
        public string? Email { get; set; }

    }
}
