using Server.Api.Extensions;

namespace Server.Api
{
    public static class Program
    {
        private static void Main(String[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services
                .AddPlugins(builder.Configuration)
                .AddApplication(builder.Configuration)
                .AddDatabase(builder.Configuration)
                .AddControllers();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}