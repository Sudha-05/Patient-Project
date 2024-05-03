using GestorPacientes.Core.Application.Interfaces.Repositories;
using GestorPacientes.Core.Application.Interfaces.Services;
using GestorPacientes.Core.Application.ViewModels.LaboratoryResult;
using GestorPacientes.Core.Domain.Entities;

namespace GestorPacientes.Core.Application.Services
{
    public class LaboratoryResultService : ILaboratoryResultService
    {
        private readonly ILaboratoryResultRepository _labResultRepository;

        public LaboratoryResultService(ILaboratoryResultRepository labResultRepository)
        {
            this._labResultRepository = labResultRepository;
        }

        public async Task<SaveLaboratoryResultViewModel> Add(SaveLaboratoryResultViewModel vm)
        {
            if (vm.LaboratoryTestIds.Count > 1)
            {
                foreach (var labTestId in vm.LaboratoryTestIds)
                {
                    LaboratoryResult labResult = new()
                    {
                        AppointmentId = vm.AppointmentId,
                        LaboratoryTestId = labTestId,
                        Resultado = vm.Resultado,
                        IsCompleted = vm.IsCompleted
                    };

                    labResult = await _labResultRepository.AddAsync(labResult);
                }

                return null;
            }

            LaboratoryResult labResultModel = new();
            labResultModel.Id = vm.Id;
            labResultModel.AppointmentId = vm.AppointmentId;
            labResultModel.LaboratoryTestId = vm.LaboratoryTestIds.FirstOrDefault();
            labResultModel.Resultado = vm.Resultado;
            labResultModel.IsCompleted = vm.IsCompleted;

            await _labResultRepository.AddAsync(labResultModel);

            return null;

        }

        public async Task Update(SaveLaboratoryResultViewModel vm)
        {
            LaboratoryResult labResult = await _labResultRepository.GetByIdAsync(vm.Id);
            labResult.Id = vm.Id;
            labResult.AppointmentId = vm.AppointmentId;
            labResult.Resultado = vm.Resultado;
            labResult.IsCompleted = vm.IsCompleted;

            await _labResultRepository.UpdateAsync(labResult);

        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<SaveLaboratoryResultViewModel> GetByIdSaveViewModel(int id)
        {
            var labResult = await _labResultRepository.GetByIdAsync(id);

            SaveLaboratoryResultViewModel vm = new();
            vm.Id = labResult.Id;
            vm.AppointmentId = labResult.AppointmentId;
            vm.LaboratoryTestId = labResult.LaboratoryTestId;
            vm.Resultado = labResult.Resultado;
            vm.IsCompleted = labResult.IsCompleted;

            return vm;
        }

        public async Task<List<LaboratoryResultViewModel>> GetAllViewModel()
        {
            var list = await _labResultRepository.GetAllWithIncludeAsync(new List<string> { "Appointment", "LaboratoryTest", "Appointment.Patient" });
            return list
                .Where(labVm => !labVm.IsCompleted)
                .Select(labVm => new LaboratoryResultViewModel
            {
                Id = labVm.Id,
                AppointmentId = labVm.AppointmentId,
                PatientName = $"{labVm.Appointment.Patient.FirstName} {labVm.Appointment.Patient.LastName}",
                PatientIdNumber = labVm.Appointment.Patient.IdNumber,
                LaboratoryTestId = labVm.LaboratoryTestId,
                LaboratoryTestName = labVm.LaboratoryTest.Name
            }).ToList();
        }

        public async Task<List<LaboratoryResultViewModel>> GetAllViewModelWithFilter(FilterLabResultViewModel filter)
        {
            var list = await _labResultRepository.GetAllWithIncludeAsync(new List<string> { "Appointment", "LaboratoryTest", "Appointment.Patient" });
            var listViewModel = list
                .Select(labVm => new LaboratoryResultViewModel
                {
                    Id = labVm.Id,
                    AppointmentId = labVm.AppointmentId,
                    PatientName = $"{labVm.Appointment.Patient.FirstName} {labVm.Appointment.Patient.LastName}",
                    PatientIdNumber = labVm.Appointment.Patient.IdNumber,
                    LaboratoryTestId = labVm.LaboratoryTestId,
                    LaboratoryTestName = labVm.LaboratoryTest.Name,
                    IsCompleted = labVm.IsCompleted
                }).ToList();

            if (filter.LabSearch != null)
            {
                string searchValue = filter.LabSearch.ToUpper();
                listViewModel = listViewModel.Where(labVm => labVm.PatientIdNumber.ToUpper().Contains(searchValue)).ToList();
            }

            if (filter.AppointmentId != 0) 
            {
                int appointmentId = filter.AppointmentId;
                listViewModel = listViewModel.Where(labVm => labVm.AppointmentId == appointmentId).ToList();
            }

            return listViewModel;
        }


    }
}
