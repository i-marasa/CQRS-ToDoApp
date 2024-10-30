using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using ToDoApp.Application.Interfaces;
using ToDoApp.Application.Services;

namespace ToDoApp.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

            services.AddScoped<IAuthService, AuthService>();
            return services;
        }
    }
}
