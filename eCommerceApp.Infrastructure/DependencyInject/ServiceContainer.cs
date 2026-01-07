using eCommerceApp.Domain.Entities;
using eCommerceApp.Domain.Interfaces;
using eCommerceApp.Infrastructure.Data;
using eCommerceApp.Infrastructure.Repositories;
using EntityFramework.Exceptions.MySQL;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
namespace eCommerceApp.Infrastructure.DependencyInject
{
    public static class ServiceContainer
    {
        public static IServiceCollection AddInfrastructureServices
            (this IServiceCollection services, IConfiguration config)
        {
            string connectionString = "Default";

            var serverVersion = new MySqlServerVersion(new Version(8, 0, 33));

            services.AddDbContext<AppDbContext>(options =>
                options.UseMySql(
                    config.GetConnectionString(connectionString),
                    serverVersion,
                    MySqlOptions =>
                    {
                        MySqlOptions.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName);
                        MySqlOptions.EnableRetryOnFailure();
                    }
                ).UseExceptionProcessor(),
                ServiceLifetime.Scoped
            );

            services.AddScoped<IGeneric<Product>, GenericRepository<Product>>();
            services.AddScoped<IGeneric<Category>, GenericRepository<Category>>();
            return services;
        }

        public static IApplicationBuilder UseInfrastructureServices
            (this IApplicationBuilder app)
        {
            app.UseMiddleware<Middleware.ExceptionHandlingMiddleware>();
            return app;
        }
    }
}
