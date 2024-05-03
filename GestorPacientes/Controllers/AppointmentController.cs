using GestorPacientes.Core.Application.Enums;
using GestorPacientes.Core.Application.Interfaces.Services;
using GestorPacientes.Core.Application.ViewModels.Appointment;
using GestorPacientes.Core.Application.ViewModels.LaboratoryResult;
using GestorPacientes.Core.Application.Helpers;
using GestorPacientes.Middlewares;
using Microsoft.AspNetCore.Mvc;
using GestorPacientes.Core.Application.ViewModels.Users;
using Microsoft.AspNetCore.Http;

namespace GestorPacientes.Controllers
{
    public class AppointmentController : Controller
    {
        private readonly IAppointmentService _appointmentService;
        private readonly IPatientService _patientService;
        private readonly IDoctorService _doctorService;
        private readonly ILaboratoryResultService _labResultService;
        private readonly ILaboratoryTestService _labTestService;
        private readonly ValidateUserSession _validateUserSession;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserViewModel? userViewModel;

        public AppointmentController(IAppointmentService appointmentService, IPatientService patientService,
            IDoctorService doctorService, ILaboratoryResultService labResultService, ILaboratoryTestService labTestService,
            ValidateUserSession validateUserSession, IHttpContextAccessor httpContextAccessor)
        {
            this._appointmentService = appointmentService;
            this._patientService = patientService;
            this._doctorService = doctorService;
            this._labResultService = labResultService;
            this._labTestService = labTestService;
            this._validateUserSession = validateUserSession;
            this._httpContextAccessor = httpContextAccessor;
            this.userViewModel = _httpContextAccessor.HttpContext.Session.Get<UserViewModel>("user");

        }

        public async Task<IActionResult> Index()
        {
            if (!_validateUserSession.HasUser() || userViewModel.TypeUserId != Roles.Assistant)
            {
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }

            var list = await _appointmentService.GetAllViewModel();
            return View(list);
        }

        public async Task<IActionResult> Create()
        {
            if (!_validateUserSession.HasUser() || userViewModel.TypeUserId != Roles.Assistant)
            {
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }

            var listPatients = await _patientService.GetAllViewModel();
            var listDoctors = await _doctorService.GetAllViewModel();
            var model = new SaveAppointmentViewModel()
            {
                Patients = listPatients,
                Doctors = listDoctors
            };
            return View("SaveAppointment", model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(SaveAppointmentViewModel vm)
        {
            if (!_validateUserSession.HasUser() || userViewModel.TypeUserId != Roles.Assistant)
            {
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }

            if (!ModelState.IsValid)
            {
                vm.Patients = await _patientService.GetAllViewModel();
                vm.Doctors = await _doctorService.GetAllViewModel();
                return View("SaveAppointment", vm);
            }

            await _appointmentService.Add(vm);
            return RedirectToRoute(new { controller = "Appointment", action = "Index" });
        }

        public async Task<IActionResult> Consult(int appointmentid)
        {
            if (!_validateUserSession.HasUser() || userViewModel.TypeUserId != Roles.Assistant)
            {
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }

            SaveLaboratoryResultViewModel model = new();
            model.AppointmentId = appointmentid;
            model.LaboratoryTests = await _labTestService.GetAllViewModel();
            return View("ConsultPatient", model);
        }

        [HttpPost]
        public async Task<IActionResult> Consult(SaveLaboratoryResultViewModel vm)
        {
            if (!_validateUserSession.HasUser() || userViewModel.TypeUserId != Roles.Assistant)
            {
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }

            if (!ModelState.IsValid)
            {
                vm.LaboratoryTests = await _labTestService.GetAllViewModel();
                return View("ConsultPatient", vm);

            }

            await _labResultService.Add(vm);

            var appointment = await _appointmentService.GetByIdSaveViewModel(vm.AppointmentId);
            appointment.status = Status.PendingResults;

            await _appointmentService.Update(appointment);

            return RedirectToRoute(new { controller = "Appointment", action = "Index" });
        }

        public async Task<IActionResult> CheckResults(FilterLabResultViewModel filterLabResult, string? status)
        {
            if (!_validateUserSession.HasUser() || userViewModel.TypeUserId != Roles.Assistant)
            {
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }

            var list = await _labResultService.GetAllViewModelWithFilter(filterLabResult);

            if (status != null)
            {
                ViewBag.Status = status;
            }
            return View("LaboratoryResults", list);
        }

        public async Task<IActionResult> CompleteAppointment(int id)
        {
            if (!_validateUserSession.HasUser() || userViewModel.TypeUserId != Roles.Assistant)
            {
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }

            var appointment = await _appointmentService.GetByIdSaveViewModel(id);
            appointment.status = Status.Completed;

            await _appointmentService.Update(appointment);

            return RedirectToRoute(new { controller = "Appointment", action = "Index" });
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (!_validateUserSession.HasUser() || userViewModel.TypeUserId != Roles.Assistant)
            {
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }

            var appointment = await _appointmentService.GetByIdSaveViewModel(id);
            return View(appointment);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteAppointment(int id)
        {
            if (!_validateUserSession.HasUser() || userViewModel.TypeUserId != Roles.Assistant)
            {
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }

            await _appointmentService.Delete(id);
            return RedirectToRoute(new { controller = "Appointment", action = "Index" });
        }
    }
}
