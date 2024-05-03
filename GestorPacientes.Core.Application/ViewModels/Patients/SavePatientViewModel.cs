using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.Timers;

namespace GestorPacientes.Core.Application.ViewModels.Patients
{
    public class SavePatientViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string FirstName { get; set; }
        [Display(Name = "Apellido")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string LastName { get; set; }
        [Display(Name = "Telefono")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string Phone { get; set; }
        [Display(Name = "Dirección")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string Address { get; set; }
        [Display(Name = "Cedula")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string IdNumber { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Fecha de nacimiento")]
        public DateTime DateBirth { get; set; }
        [Display(Name = "Es fumador?")]
        public bool IsSmoker { get; set; } = false;
        [Display(Name = "Tiene alergias?")]
        public bool HasAllergies { get; set; } = false;
        public string? ImageUrl { get; set; }
        [DataType(DataType.Upload)]
        [Display(Name = "Foto del paciente")]
        public IFormFile? File { get; set; }
    }
}
