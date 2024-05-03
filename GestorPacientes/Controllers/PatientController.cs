using GestorPacientes.Core.Application.Interfaces.Services;
using GestorPacientes.Core.Application.ViewModels.Patients;
using GestorPacientes.Core.Application.ViewModels.Users;
using GestorPacientes.Core.Application.Helpers;
using GestorPacientes.Middlewares;
using Microsoft.AspNetCore.Mvc;
using GestorPacientes.Core.Application.Enums;

namespace GestorPacientes.Controllers
{
    public class PatientController : Controller
    {
        private readonly IPatientService _patientService;
        private readonly ValidateUserSession _validateUserSession;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserViewModel? userViewModel;

        public PatientController(IPatientService patientService, ValidateUserSession validateUserSession,
            IHttpContextAccessor httpContextAccessor)
        {
            this._patientService = patientService;
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
            var list = await _patientService.GetAllViewModel();
            return View(list);
        }

        public IActionResult Create()
        {
            if (!_validateUserSession.HasUser() || userViewModel.TypeUserId != Roles.Assistant)
            {
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }
            SavePatientViewModel vm = new();
            return View("SavePatient", vm);
        }

        [HttpPost]
        public async Task<IActionResult> Create(SavePatientViewModel vm)
        {
            if (!_validateUserSession.HasUser() || userViewModel.TypeUserId != Roles.Assistant)
            {
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }
            if (!ModelState.IsValid)
            {
                return View("SavePatient", vm);
            }

            SavePatientViewModel patientVm = await _patientService.Add(vm);

            if (patientVm.Id != 0 && patientVm != null)
            {
                patientVm.ImageUrl = UploadFile(vm.File, patientVm.Id);
                await _patientService.Update(patientVm);
            }

            return RedirectToRoute(new { controller = "Patient", action = "Index" });
        }

        public async Task<IActionResult> Edit(int id)
        {
            if (!_validateUserSession.HasUser() || userViewModel.TypeUserId != Roles.Assistant)
            {
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }
            var patient = await _patientService.GetByIdSaveViewModel(id);
            return View("SavePatient", patient);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(SavePatientViewModel vm)
        {
            if (!_validateUserSession.HasUser() || userViewModel.TypeUserId != Roles.Assistant)
            {
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }
            if (!ModelState.IsValid)
            {
                return View("SavePatient", vm);
            }

            SavePatientViewModel patientVm = await _patientService.GetByIdSaveViewModel(vm.Id);
            vm.ImageUrl = UploadFile(vm.File, vm.Id, true, patientVm.ImageUrl);

            await _patientService.Update(vm);
            return RedirectToRoute(new { controller = "Patient", action = "Index" });
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (!_validateUserSession.HasUser() || userViewModel.TypeUserId != Roles.Assistant)
            {
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }
            var patient = await _patientService.GetByIdSaveViewModel(id);
            return View("Delete", patient);
        }

        [HttpPost]
        public async Task<IActionResult> DeletePatient(int id)
        {
            if (!_validateUserSession.HasUser() || userViewModel.TypeUserId != Roles.Assistant)
            {
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }
            await _patientService.Delete(id);

            //get directory path
            string basePath = $"/Images/Patients/{id}";
            string path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot{basePath}");

            if (Directory.Exists(path))
            {
                DirectoryInfo directoryInfo = new DirectoryInfo(path);
                foreach (FileInfo file in directoryInfo.GetFiles())
                {
                    file.Delete();
                }

                foreach (DirectoryInfo folder in directoryInfo.GetDirectories())
                {
                    folder.Delete(true);
                }

                Directory.Delete(path);
            }

            return RedirectToRoute(new { controller = "Patient", action = "Index" });
        }

        private string UploadFile(IFormFile file, int id, bool isEditMode = false, string imagePath = "")
        {
            if (isEditMode && file == null)
            {
                return imagePath;
            }

            //get directory path
            string basePath = $"/Images/Patients/{id}";
            string path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot{basePath}");

            //create folder if no exist
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            //get file path
            Guid guid = Guid.NewGuid();
            FileInfo fileInfo = new(file.FileName);
            string filename = guid + fileInfo.Extension;

            string fileNameWithPath = Path.Combine(path, filename);

            using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            if (isEditMode)
            {
                string[] oldImagePart = imagePath.Split("/");
                string oldImageName = oldImagePart[^1];
                string completeImageOldPath = Path.Combine(path, oldImageName);

                if (System.IO.File.Exists(completeImageOldPath))
                {
                    System.IO.File.Delete(completeImageOldPath);
                }
            }

            return $"{basePath}/{filename}";
        }
    }
}
