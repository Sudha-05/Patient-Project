using GestorPacientes.Core.Domain.Entities;

namespace GestorPacientes.Core.Application.Interfaces.Repositories
{
    public interface IAppointmentRepository : IGenericRepository<Appointment>
    {
        Task<Appointment?> GetByIdWithIncludeAsync(int id, List<string> properties);
    }
}
