using eCommerce.Core.IServices;
using eCommerce.Core.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Core
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddCore(this IServiceCollection service)
        {
           
            service.AddTransient<IUserService, UserService>();
            return service;
        }
    }
}
