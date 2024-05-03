using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace GestorPacientes.Core.Application.ViewModels.Doctors
{
    public class SaveDoctorViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string FirstName { get; set; }
        [Display(Name = "Apellido")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string LastName { get; set; }
        [EmailAddress]
        [Display(Name = "Correo electronico")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string Email { get; set; }
        [Phone]
        [Display(Name = "Telefono")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string Phone { get; set; }
        [Display(Name = "Cedula")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string IdNumber { get; set; }
        public string? ImageUrl { get; set; }
        [DataType(DataType.Upload)]
        [Display(Name = "Foto del medico")]
        public IFormFile? File { get; set; }
    }
}
