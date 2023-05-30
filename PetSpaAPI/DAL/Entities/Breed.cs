using System.ComponentModel.DataAnnotations;

namespace PetSpaAPI.DAL.Entities
{
    public class Breed
    {
        [Required]
        public int Id { get; set; }

        [Display(Name = "Raza")]
        [MaxLength(50, ErrorMessage = "El campo {0} debe tener {1} caracteres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Name { get; set; }

        [Display(Name = "Especie")]
        public Species Species { get; set; }

        [Display(Name = "Id Especie")]
        public int SpeciesId { get; set; }
    }
}
