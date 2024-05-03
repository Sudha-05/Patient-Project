using GestorPacientes.Core.Application.Helpers;
using GestorPacientes.Core.Application.ViewModels.Users;

namespace GestorPacientes.Middlewares
{
    public class ValidateUserSession
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ValidateUserSession(IHttpContextAccessor httpContextAccessor)
        {
            this._httpContextAccessor = httpContextAccessor;
        }

        public bool HasUser()
        {
            UserViewModel userViewModel = _httpContextAccessor.HttpContext.Session.Get<UserViewModel>("user");
            if (userViewModel == null)
            {
                return false;
            }

            return true;
        }
    }
}
