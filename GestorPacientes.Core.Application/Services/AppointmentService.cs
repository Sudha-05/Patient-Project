using GestorPacientes.Core.Application.Enums;
using GestorPacientes.Core.Application.Interfaces.Repositories;
using GestorPacientes.Core.Application.Interfaces.Services;
using GestorPacientes.Core.Application.ViewModels.Appointment;
using GestorPacientes.Core.Application.ViewModels.LaboratoryResult;
using GestorPacientes.Core.Domain.Entities;
using System.Linq;

namespace GestorPacientes.Core.Application.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IAppointmentRepository _appointmentRepository;

        public AppointmentService(IAppointmentRepository appointmentRepository)
        {
            this._appointmentRepository = appointmentRepository;
        }

        public async Task<SaveAppointmentViewModel> Add(SaveAppointmentViewModel vm)
        {
            Appointment appointment = new();
            appointment.Id = vm.Id;
            appointment.PatientId = vm.PatientId;
            appointment.DoctorId = vm.DoctorId;
            appointment.Day = vm.Day;
            appointment.Time = vm.Time;
            appointment.Reason = vm.Reason;
            appointment.status = (int)vm.status;

            appointment = await _appointmentRepository.AddAsync(appointment);

            SaveAppointmentViewModel appointmentVm = new();
            appointmentVm.Id = appointment.Id;
            appointmentVm.PatientId = appointment.PatientId;
            appointmentVm.DoctorId = appointment.DoctorId;
            appointmentVm.Day = appointment.Day;
            appointmentVm.Time = appointment.Time;
            appointmentVm.Reason = appointment.Reason;
            appointmentVm.status = (Status)appointment.status;

            return appointmentVm;
        }

        public async Task Delete(int id)
        {
            var appointment = await _appointmentRepository.GetByIdAsync(id);

            if (appointment != null)
            {
                await _appointmentRepository.DeleteAsync(appointment);
            }
            else
            {
                throw new Exception();
            }
        }

        public async Task<List<AppointmentViewModel>> GetAllViewModel()
        {
            var list = await _appointmentRepository.GetAllWithIncludeAsync(new List<string> { "Patient", "Doctor", "LaboratoryResults" });
            return list.Select(a => new AppointmentViewModel
            {
                Id = a.Id,
                PatientId = a.PatientId,
                PatientName = $"{a.Patient.FirstName} {a.Patient.LastName}",
                DoctorId = a.DoctorId,
                DoctorName = $"{a.Doctor.FirstName} {a.Doctor.LastName}",
                Day = a.Day,
                Time = a.Time,
                Reason = a.Reason,
                status = (Status)a.status
            }).ToList();
        }

        public async Task<SaveAppointmentViewModel> GetByIdSaveViewModel(int id)
        {
            var appointment = await _appointmentRepository.GetByIdAsync(id);

            SaveAppointmentViewModel vm = new();
            vm.Id = appointment.Id;
            vm.PatientId = appointment.PatientId;
            vm.DoctorId = appointment.DoctorId;
            vm.Day = appointment.Day;
            vm.Time = appointment.Time;
            vm.Reason = appointment.Reason;
            vm.status = (Status)appointment.status;

            return vm;
        }

        public async Task Update(SaveAppointmentViewModel vm)
        {
            Appointment appointment = await _appointmentRepository.GetByIdWithIncludeAsync(vm.Id, new List<string> { "LaboratoryResults" });
            appointment.Id = vm.Id;
            appointment.PatientId = vm.PatientId;
            appointment.DoctorId = vm.DoctorId;
            appointment.Day = vm.Day;
            appointment.Time = vm.Time;
            appointment.Reason = vm.Reason;
            appointment.status = (int)vm.status;

            //if (appointment.LaboratoryResults != null && appointment.LaboratoryResults.Count > 0)
            //{
            //    if (appointment.LaboratoryResults.Any(lr => !lr.IsCompleted))
            //    {
            //        appointment.status = (int)Status.PendingResults;
            //    }
            //    else
            //    {
            //        appointment.status = (int)Status.Completed;
            //    }
            //}

            await _appointmentRepository.UpdateAsync(appointment);

        }
    }
}
