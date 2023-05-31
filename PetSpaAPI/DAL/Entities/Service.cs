using System.ComponentModel.DataAnnotations;
using System.Data.SqlTypes;

namespace PetSpaAPI.DAL.Entities
{
    public class Service
    {
        [Required]
        public int Id { get; set; }

        [Display(Name = "Servicio")]
        [MaxLength(100, ErrorMessage = "El campo {0} debe tener máximo {1} caracteres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Name { get; set; }

        [Display(Name = "Duración (minutos)")]
        public int? Duration { get; set; }

        [Display(Name = "Costo")]
        public double? Cost { get; set; }
    }
}
