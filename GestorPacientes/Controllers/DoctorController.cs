using GestorPacientes.Core.Application.Interfaces.Services;
using GestorPacientes.Core.Application.ViewModels.Doctors;
using GestorPacientes.Core.Application.ViewModels.Users;
using GestorPacientes.Core.Application.Helpers;
using Microsoft.AspNetCore.Mvc;
using GestorPacientes.Middlewares;
using GestorPacientes.Core.Application.Enums;

namespace GestorPacientes.Controllers
{
    public class DoctorController : Controller
    {
        private readonly IDoctorService _doctorService;
        private readonly ValidateUserSession _validateUserSession;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserViewModel userViewModel;

        public DoctorController(IDoctorService doctorService, ValidateUserSession validateUserSession,
            IHttpContextAccessor httpContextAccessor)
        {
            this._doctorService = doctorService;
            this._validateUserSession = validateUserSession;
            this._httpContextAccessor = httpContextAccessor;
            this.userViewModel = _httpContextAccessor.HttpContext.Session.Get<UserViewModel>("user");

        }

        public async Task<IActionResult> Index()
        {
            if (!_validateUserSession.HasUser() || userViewModel.TypeUserId != Roles.Admin)
            {
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }
            var list = await _doctorService.GetAllViewModel(); 
            return View(list);
        }

        public IActionResult Create()
        {
            if (!_validateUserSession.HasUser() || userViewModel.TypeUserId != Roles.Admin)
            {
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }
            SaveDoctorViewModel vm = new();
            return View("SaveDoctor", vm);
        }

        [HttpPost]
        public async Task<IActionResult> Create(SaveDoctorViewModel vm)
        {
            if (!_validateUserSession.HasUser() || userViewModel.TypeUserId != Roles.Admin)
            {
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }
            if (!ModelState.IsValid)
            {
                return View("SaveDoctor" ,vm);
            }

            SaveDoctorViewModel doctorVm = await _doctorService.Add(vm);

            if (doctorVm.Id != 0 && doctorVm != null)
            {
                doctorVm.ImageUrl = UploadFile(vm.File, doctorVm.Id);
                await _doctorService.Update(doctorVm);
            }

            return RedirectToRoute(new { controller = "Doctor", action = "Index" });
        }

        public async Task<IActionResult> Edit(int id)
        {
            if (!_validateUserSession.HasUser() || userViewModel.TypeUserId != Roles.Admin)
            {
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }
            var doctor = await _doctorService.GetByIdSaveViewModel(id);
            return View("SaveDoctor", doctor);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(SaveDoctorViewModel vm)
        {
            if (!_validateUserSession.HasUser() || userViewModel.TypeUserId != Roles.Admin)
            {
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }
            if (!ModelState.IsValid)
            {
                return View("SaveDoctor", vm);
            }

            SaveDoctorViewModel doctorVm = await _doctorService.GetByIdSaveViewModel(vm.Id);
            vm.ImageUrl = UploadFile(vm.File, vm.Id, true, doctorVm.ImageUrl);

            await _doctorService.Update(vm);
            return RedirectToRoute(new {controller = "Doctor", action = "Index"});
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (!_validateUserSession.HasUser() || userViewModel.TypeUserId != Roles.Admin)
            {
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }
            var doctor = await _doctorService.GetByIdSaveViewModel(id);
            return View("Delete", doctor);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteDoctor(int id)
        {
            if (!_validateUserSession.HasUser() || userViewModel.TypeUserId != Roles.Admin)
            {
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }
            await _doctorService.Delete(id);

            //get directory path
            string basePath = $"/Images/Doctors/{id}";
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

            return RedirectToRoute(new {Controller = "Doctor", action = "Index"});
        }

        private string UploadFile(IFormFile file, int id, bool isEditMode = false, string imagePath = "")
        {
            if (isEditMode && file == null)
            {
                return imagePath;
            }

            //get directory path
            string basePath = $"/Images/Doctors/{id}";
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

            using(var stream = new FileStream(fileNameWithPath, FileMode.Create))
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
