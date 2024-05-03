using GestorPacientes.Core.Application.Enums;

namespace GestorPacientes.Core.Application.ViewModels.Appointment
{
    public class AppointmentViewModel
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public string PatientName { get; set; }
        public int DoctorId { get; set; }
        public string DoctorName { get; set; }
        public DateTime Day { get; set; }
        public TimeSpan Time { get; set; }
        public string Reason { get; set; }
        public Status status { get; set; }
    }
}
