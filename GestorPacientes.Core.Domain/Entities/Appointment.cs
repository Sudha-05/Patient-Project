using GestorPacientes.Core.Domain.Common;

namespace GestorPacientes.Core.Domain.Entities
{
    public class Appointment : AuditableBaseEntity
    {
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        public DateTime Day { get; set; }
        public TimeSpan Time { get; set; }
        public string Reason { get; set; }
        public int status { get; set; }

        //navigation properties
        public Patient? Patient { get; set; }
        public Doctor? Doctor { get; set; }
        public ICollection<LaboratoryResult>? LaboratoryResults { get; set; }
    }
}
