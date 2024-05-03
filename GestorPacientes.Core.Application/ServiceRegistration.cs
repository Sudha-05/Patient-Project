using GestorPacientes.Core.Application.Interfaces.Services;
using GestorPacientes.Core.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace GestorPacientes.Core.Application
{
    public static class ServiceRegistration
    {
        public static void AddApplicationLayer(this IServiceCollection services)
        {
            #region Services
            services.AddTransient<IDoctorService, DoctorService>();
            services.AddTransient<IPatientService, PatientService>();
            services.AddTransient<ILaboratoryTestService, LaboratoryTestService>();
            services.AddTransient<IAppointmentService, AppointmentService>();
            services.AddTransient<ILaboratoryResultService, LaboratoryResultService>();
            services.AddTransient<IUserService, UserService>();
            #endregion
        }
    }
}
