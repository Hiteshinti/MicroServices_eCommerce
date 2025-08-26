using eCommerce.Core.IRepository;
using eCommerce.Infrastructure.DbContext;
using eCommerce.Infrastructure.Repository;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Infrastructure
{
     public static class DependencyInjection
    { 
         public static IServiceCollection AddInfraStructure(this IServiceCollection service)
        {
            service.AddScoped<DapperDbContext>();
            service.AddTransient<IUserRepository, UserRepository>();
            return service;
        }
    }
}
