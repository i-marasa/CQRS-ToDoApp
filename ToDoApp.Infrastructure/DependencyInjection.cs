using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using ToDoApp.Infrastructure.Data;
using ToDoApp.Infrastructure.Entities;
using ToDoApp.Domain.Interfaces;
using ToDoApp.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace ToDoApp.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ToDoDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IToDoRepository, ToDoRepository>();
            // Configure Identity with the custom IdentityUserEntity
            services.AddIdentityCore<IdentityUserEntity>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
            })
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<ToDoDbContext>()
            .AddDefaultTokenProviders();

            return services;
        }
    }
}
