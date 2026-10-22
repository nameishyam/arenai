using Microsoft.EntityFrameworkCore;
using Server.Application.Configurations;
using Server.Application.Interfaces.Repository;
using Server.Persistence.Context;
using Server.Persistence.Repositories;

namespace Server.Api.Extensions
{
    public static class DatabaseExtensions
    {
        public static IServiceCollection AddDatabase(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseNpgsql(
                    configuration.GetConnectionString("DefaultConnection")
                );
            });

            services.Configure<ConnectionStrings>(configuration.GetSection("ConnectionStrings"));

            services.AddScoped<IUserRepository, UserRepository>();

            return services;
        }
    }
}
