using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsersPasswordStore.Application.Interfaces;
using UsersPasswordStore.Application.Interfaces.ICache;
using UsersPasswordStore.Application.Services;
using UsersPasswordStore.Application.Services.Cache;

namespace UsersPasswordStore.Application
{
    public static class ConfigureService
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IMemoryCacheService, MemoryCacheService>();
            services.AddScoped<IUserService, UserService>();
            return services;
        }
    }
}
