using GestorPacientes.Core.Application.ViewModels.LaboratoryResult;

namespace GestorPacientes.Core.Application.Interfaces.Services
{
    public interface ILaboratoryResultService : IGenericService<SaveLaboratoryResultViewModel, LaboratoryResultViewModel>
    {
        Task<List<LaboratoryResultViewModel>> GetAllViewModelWithFilter(FilterLabResultViewModel filter);
    }
}
