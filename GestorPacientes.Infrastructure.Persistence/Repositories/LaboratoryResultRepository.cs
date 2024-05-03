using GestorPacientes.Core.Application.Interfaces.Repositories;
using GestorPacientes.Core.Domain.Entities;
using GestorPacientes.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace GestorPacientes.Infrastructure.Persistence.Repositories
{
    public class LaboratoryResultRepository : GenericRepository<LaboratoryResult>, ILaboratoryResultRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public LaboratoryResultRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            this._dbContext = dbContext;
        }

        //public override async Task<List<LaboratoryResult>> GetAllWithIncludeAsync(List<string> properties)
        //{
        //    var query = _dbContext.Set<LaboratoryResult>().AsQueryable();

        //    foreach (string property in properties)
        //    {
        //        if (property == "Appointment")
        //        {
        //            query = query.Include(lr => lr.Appointment)
        //                .ThenInclude(a => a.Patient);
        //        }
        //        else
        //        {
        //            query = query.Include(property);
        //        }
        //    }

        //    return await query.ToListAsync();
        //}

    }
}
