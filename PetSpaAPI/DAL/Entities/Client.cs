using System.ComponentModel.DataAnnotations;

namespace PetSpaAPI.DAL.Entities
{
    public class Client : Person
    {
        [Display(Name = "Dirección")]
        [MaxLength(200, ErrorMessage = "El campo {0} debe tener máximo {1} caracteres.")]
        public string? Address { get; set; }
    }
}
