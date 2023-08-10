using Application.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using AutoMapper;
using System.Reflection;

namespace Application.Services
{
    public static class ApplicationServices
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection _services)
        {
            _services.AddMediatR(configuration =>
            {
                configuration.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            });

            return _services;
        }
    }
}
