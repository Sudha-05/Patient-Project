using GestorPacientes.Core.Application.Interfaces.Repositories;
using GestorPacientes.Core.Domain.Entities;
using GestorPacientes.Infrastructure.Persistence.Contexts;

namespace GestorPacientes.Infrastructure.Persistence.Repositories
{
    public class LaboratoryTestRepository : GenericRepository<LaboratoryTest>, ILaboratoryTestRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public LaboratoryTestRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            this._dbContext = dbContext;
        }
    }
}
