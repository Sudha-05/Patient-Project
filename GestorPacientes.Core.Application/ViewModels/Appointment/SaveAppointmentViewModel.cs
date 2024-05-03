using GestorPacientes.Core.Application.Enums;
using GestorPacientes.Core.Application.ViewModels.Doctors;
using GestorPacientes.Core.Application.ViewModels.Patients;
using System.ComponentModel.DataAnnotations;

namespace GestorPacientes.Core.Application.ViewModels.Appointment
{
    public class SaveAppointmentViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Paciente")]
        [Range(1, maximum: int.MaxValue, ErrorMessage = "Debe seleccionar un {0}")]
        public int PatientId { get; set; }
        public List<PatientViewModel>? Patients { get; set; }
        [Display(Name = "Doctor")]
        [Range(1, maximum: int.MaxValue, ErrorMessage = "Debe seleccionar un {0}")]
        public int DoctorId { get; set; }
        public List<DoctorViewModel>? Doctors { get; set; }
        [Display(Name = "Dia")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public DateTime Day { get; set; }
        [Display(Name = "Hora")]
        [DataType(DataType.Time)]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public TimeSpan Time { get; set; }
        [Display(Name = "Causa de la cita")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string Reason { get; set; }
        public Status status { get; set; } = Status.PendingConsultation;
    }
}
