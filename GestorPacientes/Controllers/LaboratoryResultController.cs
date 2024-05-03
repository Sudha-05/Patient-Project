using GestorPacientes.Core.Application.Interfaces.Services;
using GestorPacientes.Core.Application.ViewModels.LaboratoryResult;
using GestorPacientes.Core.Application.Helpers;
using Microsoft.AspNetCore.Mvc;
using GestorPacientes.Core.Application.ViewModels.Users;
using GestorPacientes.Middlewares;
using GestorPacientes.Core.Application.Enums;

namespace GestorPacientes.Controllers
{
    public class LaboratoryResultController : Controller
    {
        private readonly ILaboratoryResultService _labResultService;
        private readonly ILaboratoryTestService _labTestService;
        private readonly ValidateUserSession _validateUserSession;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserViewModel? userViewModel;

        public LaboratoryResultController(ILaboratoryResultService labResultService, ILaboratoryTestService labTestService,
            ValidateUserSession validateUserSession, IHttpContextAccessor httpContextAccessor)
        {
            this._labResultService = labResultService;
            this._labTestService = labTestService;
            this._validateUserSession = validateUserSession;
            this._httpContextAccessor = httpContextAccessor;
            this.userViewModel = _httpContextAccessor.HttpContext.Session.Get<UserViewModel>("user");
        }

        public async Task<IActionResult> Index(FilterLabResultViewModel vm)
        {
            if (!_validateUserSession.HasUser() || userViewModel.TypeUserId != Roles.Assistant)
            {
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }
            var list = await _labResultService.GetAllViewModelWithFilter(vm);
            return View(list);
        }

        public async Task<IActionResult> ReportResult(int id)
        {
            if (!_validateUserSession.HasUser() || userViewModel.TypeUserId != Roles.Assistant)
            {
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }
            var labResult = await _labResultService.GetByIdSaveViewModel(id);
            return View("ReportResult", labResult);
        }

        [HttpPost]
        public async Task<IActionResult> ReportResult(SaveLaboratoryResultViewModel vm)
        {
            if (!_validateUserSession.HasUser() || userViewModel.TypeUserId != Roles.Assistant)
            {
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }
            if (!ModelState.IsValid)
            {
                return View("ReportResult", vm);
            }

            vm.IsCompleted = true;

            await _labResultService.Update(vm);
            return RedirectToRoute(new { controller = "LaboratoryResult", action = "Index" });
        }
    }
}
