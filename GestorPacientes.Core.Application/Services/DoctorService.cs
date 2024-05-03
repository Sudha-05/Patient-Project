using GestorPacientes.Core.Application.Interfaces.Repositories;
using GestorPacientes.Core.Application.Interfaces.Services;
using GestorPacientes.Core.Application.ViewModels.Doctors;
using GestorPacientes.Core.Domain.Entities;

namespace GestorPacientes.Core.Application.Services
{
    public class DoctorService : IDoctorService
    {
        private readonly IDoctorRepository _doctorRepository;

        public DoctorService(IDoctorRepository doctorRepository)
        {
            this._doctorRepository = doctorRepository;
        }

        public async Task<SaveDoctorViewModel> Add(SaveDoctorViewModel vm)
        {
            Doctor doctor = new();
            doctor.FirstName = vm.FirstName;
            doctor.LastName = vm.LastName;
            doctor.Email = vm.Email;
            doctor.Phone = vm.Phone;
            doctor.IdNumber = vm.IdNumber;
            doctor.ImagePath = vm.ImageUrl;

            doctor = await _doctorRepository.AddAsync(doctor);

            SaveDoctorViewModel doctorVm = new();
            doctorVm.Id = doctor.Id;
            doctorVm.FirstName = doctor.FirstName;
            doctorVm.LastName = doctor.LastName;
            doctorVm.Email = doctor.Email;
            doctorVm.Phone = doctor.Phone;
            doctorVm.IdNumber = doctor.IdNumber;
            doctorVm.ImageUrl = doctor.ImagePath;

            return doctorVm;
        }

        public async Task Update(SaveDoctorViewModel vm)
        {
            Doctor doctor = await _doctorRepository.GetByIdAsync(vm.Id);

            doctor.Id = vm.Id;
            doctor.FirstName = vm.FirstName;
            doctor.LastName = vm.LastName;
            doctor.Email = vm.Email;
            doctor.Phone = vm.Phone;
            doctor.IdNumber = vm.IdNumber;
            doctor.ImagePath = vm.ImageUrl;

            await _doctorRepository.UpdateAsync(doctor);
        }

        public async Task Delete(int id)
        {
            var doctor = await _doctorRepository.GetByIdAsync(id);

            if (doctor != null)
            {
                await _doctorRepository.DeleteAsync(doctor);
            }
            else
            {
                throw new Exception();
            }
        }

        public async Task<List<DoctorViewModel>> GetAllViewModel()
        {
            var list = await _doctorRepository.GetAllAsync();
            return list.Select(d => new DoctorViewModel
            {
                Id = d.Id,
                FirstName = d.FirstName,
                LastName = d.LastName,
                Email = d.Email,
                Phone = d.Phone,
                IdNumber = d.IdNumber,
                ImageUrl = d.ImagePath
            }).ToList();
        }

        public async Task<SaveDoctorViewModel> GetByIdSaveViewModel(int id)
        {
            var doctor = await _doctorRepository.GetByIdAsync(id);

            SaveDoctorViewModel vm = new();
            vm.Id = doctor.Id;
            vm.FirstName = doctor.FirstName;
            vm.LastName = doctor.LastName;
            vm.Email = doctor.Email;
            vm.Phone = doctor.Phone;
            vm.IdNumber = doctor.IdNumber;
            vm.ImageUrl = doctor.ImagePath;

            return vm;
        }
    }
}
