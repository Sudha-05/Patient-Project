using System.ComponentModel.DataAnnotations;

namespace GestorPacientes.Core.Application.ViewModels.LaboratoryTests
{
    public class SaveLaboratoryTestViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string Name { get; set; }
    }
}
