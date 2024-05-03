using GestorPacientes.Core.Application.ViewModels.LaboratoryTests;
using System.ComponentModel.DataAnnotations;

namespace GestorPacientes.Core.Application.ViewModels.LaboratoryResult
{
    public class SaveLaboratoryResultViewModel
    {
        public int Id { get; set; }
        [Required]
        public int AppointmentId { get; set; }
        public string? Resultado { get; set; }
        public bool IsCompleted { get; set; } = false;
        public int? LaboratoryTestId { get; set; }
        public List<int>? LaboratoryTestIds { get; set; }
        public List<LaboratoryTestViewModel>? LaboratoryTests { get; set; }

    }
}
