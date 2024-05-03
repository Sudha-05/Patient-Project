using GestorPacientes.Core.Application.ViewModels.Users;

namespace GestorPacientes.Core.Application.Interfaces.Services
{
    public interface IUserService : IGenericService<SaveUserViewModel, UserViewModel>
    {
        Task<UserViewModel> Login(LoginViewModel loginVm);
    }
}
