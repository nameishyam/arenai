using FluentValidation;
using MediatR;
using Server.Application;

namespace Server.Api.Extensions
{
    public static class ApplicationExtensions
    {
        public static IServiceCollection AddApplication(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var currentAssembly = typeof(ApplicationReference).Assembly;

            services.AddMediatR(cfg =>
                cfg.RegisterServicesFromAssembly(currentAssembly));

            services.AddValidatorsFromAssembly(currentAssembly);

            services.AddTransient(
                typeof(IPipelineBehavior<,>),
                typeof(ValidationBehavior<,>));

            return services;
        }
    }
}
