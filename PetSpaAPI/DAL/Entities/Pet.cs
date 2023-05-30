using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata.Ecma335;

namespace PetSpaAPI.DAL.Entities
{
    public class Pet
    {
        [Required]
        public int Id { get; set; }

        [Display(Name = "Nombre")]
        [MaxLength(50, ErrorMessage = "El campo {0} debe tener {1} caracteres.")]
        [Required]
        public string Name { get; set; }

        [Display(Name = "Fecha de nacimiento")]
        public DateTime? BirthDate { get; set; }

        [Display(Name = "Peso(kg)")]
        public double? Weight { get; set; }

        [Display(Name = "Detalles")]
        [MaxLength(200, ErrorMessage = "El campo {0} debe tener {1} caracteres.")]
        public string? Details { get; set; }

        [Display(Name = "Propietario")]
        public Client Client { get; set; }

        [Display(Name = "CC propietario")]
        public int ClientCc { get; set; }

        [Display(Name = "Raza")]
        public Breed Breed { get; set; }

        [Display(Name = "Id raza")]
        public int BreedId { get; set; }
    }
}
