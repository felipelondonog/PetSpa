using System.ComponentModel.DataAnnotations;

namespace PetSpaAPI.DAL.Entities
{
    public class Species
    {
        [Required]
        public int Id { get; set; }

        [Display(Name = "Especie")]
        [MaxLength(50, ErrorMessage = "El campo {0} debe tener {1} caracteres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Name { get; set; }
    }
}
