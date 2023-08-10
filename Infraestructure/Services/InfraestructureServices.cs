using Application.Interfaces;
using Infraestructure.Context;
using Infraestructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Services
{
    public static class InfraestructureServices
    {
        public static IServiceCollection AddInfraestructure(this IServiceCollection _services, IConfiguration _config)
        {
            _services.AddDbContext<SQLDBContext>(options =>
            {
                options.UseSqlServer(_config.GetSection("EFConnection").Value);
            });

            _services.AddTransient(typeof(IUnitOfWork<>), typeof(UnitOfWork<>));
            _services.AddTransient(typeof(IMongoContext<>), typeof(MongoContext<>));

            return _services;
        }
    }
}
