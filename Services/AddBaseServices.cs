using Application.Interfaces;
using Infraestructure.Repositories;

namespace CleanCodeArchitecture.Services
{
    public static class AddBaseServices
    {
        public static IServiceCollection AddAppBaseServices(this IServiceCollection _services)
        {
            _services.AddTransient(typeof(IUnitOfWork<>), typeof(UnitOfWork<>));
            _services.AddHttpContextAccessor();
            _services.AddTransient<ICurrentUserService, CurrentUserService>();

            return _services;
        }
    }
}
