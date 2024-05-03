using GestorPacientes.Core.Application.Interfaces.Repositories;
using GestorPacientes.Core.Application.Interfaces.Services;
using GestorPacientes.Core.Application.ViewModels.Patients;
using GestorPacientes.Core.Domain.Entities;

namespace GestorPacientes.Core.Application.Services
{
    public class PatientService : IPatientService
    {
        private readonly IPatientRepository _patientRepository;

        public PatientService(IPatientRepository patientRepository)
        {
            this._patientRepository = patientRepository;
        }

        public async Task<SavePatientViewModel> Add(SavePatientViewModel vm)
        {
            Patient patient = new();
            patient.FirstName = vm.FirstName;
            patient.LastName = vm.LastName;
            patient.Phone = vm.Phone;
            patient.Address = vm.Address;
            patient.IdNumber = vm.IdNumber;
            patient.DateBirth = vm.DateBirth;
            patient.IsSmoker = vm.IsSmoker;
            patient.HasAllergies = vm.HasAllergies;
            patient.ImagePath = vm.ImageUrl;

            patient = await _patientRepository.AddAsync(patient);

            SavePatientViewModel patientVm = new();
            patientVm.Id = patient.Id;
            patientVm.FirstName = patient.FirstName;
            patientVm.LastName = patient.LastName;
            patientVm.Phone = patient.Phone;
            patientVm.Address = patient.Address;
            patientVm.IdNumber = patient.IdNumber;
            patientVm.DateBirth = patient.DateBirth;
            patientVm.IsSmoker = patient.IsSmoker;
            patientVm.HasAllergies = patient.HasAllergies;
            patientVm.ImageUrl = patient.ImagePath;

            return patientVm;
        }

        public async Task Update(SavePatientViewModel vm)
        {
            Patient patient = await _patientRepository.GetByIdAsync(vm.Id);

            patient.Id = vm.Id;
            patient.FirstName = vm.FirstName;
            patient.LastName = vm.LastName;
            patient.Phone = vm.Phone;
            patient.Address = vm.Address;
            patient.IdNumber = vm.IdNumber;
            patient.DateBirth = vm.DateBirth;
            patient.IsSmoker = vm.IsSmoker;
            patient.HasAllergies = vm.HasAllergies;
            patient.ImagePath = vm.ImageUrl;

            await _patientRepository.UpdateAsync(patient);
        }

        public async Task Delete(int id)
        {
            var patient = await _patientRepository.GetByIdAsync(id);
            await _patientRepository.DeleteAsync(patient);
        }

        public async Task<SavePatientViewModel> GetByIdSaveViewModel(int id)
        {
            var patient = await _patientRepository.GetByIdAsync(id);

            SavePatientViewModel vm = new();
            vm.Id = patient.Id;
            vm.FirstName = patient.FirstName;
            vm.LastName = patient.LastName;
            vm.Phone = patient.Phone;
            vm.Address = patient.Address;
            vm.IdNumber = patient.IdNumber;
            vm.DateBirth = patient.DateBirth;
            vm.IsSmoker = patient.IsSmoker;
            vm.HasAllergies = patient.HasAllergies;
            vm.ImageUrl = patient.ImagePath;

            return vm;
        }

        public async Task<List<PatientViewModel>> GetAllViewModel()
        {
            var list = await _patientRepository.GetAllAsync();
            return list.Select(p => new PatientViewModel
            {
                Id = p.Id,
                FirstName = p.FirstName,
                LastName = p.LastName,
                Phone = p.Phone,
                Address = p.Address,
                IdNumber = p.IdNumber,
                DateBirth = p.DateBirth,
                IsSmoker = p.IsSmoker,
                HasAllergies = p.HasAllergies,
                ImageUrl = p.ImagePath
            }).ToList();
        }
    }

}
