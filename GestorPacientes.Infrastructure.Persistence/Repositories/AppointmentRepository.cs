using GestorPacientes.Core.Application.Interfaces.Repositories;
using GestorPacientes.Core.Domain.Entities;
using GestorPacientes.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace GestorPacientes.Infrastructure.Persistence.Repositories
{
    public class AppointmentRepository : GenericRepository<Appointment>, IAppointmentRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public AppointmentRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task<Appointment?> GetByIdWithIncludeAsync(int id, List<string> properties)
        {
            var query = _dbContext.Set<Appointment>().AsQueryable();

            foreach (string property in properties)
            {
                    query = query.Include(property);
            }

            return await query.FirstOrDefaultAsync(a => a.Id == id);
        }
    }
}
