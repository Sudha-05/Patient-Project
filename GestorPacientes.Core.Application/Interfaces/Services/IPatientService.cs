using GestorPacientes.Core.Application.ViewModels.Patients;

namespace GestorPacientes.Core.Application.Interfaces.Services
{
    public interface IPatientService : IGenericService<SavePatientViewModel, PatientViewModel>
    {

    }
}
