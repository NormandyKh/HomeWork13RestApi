using Microsoft.EntityFrameworkCore;
using RestApiHW13.Data.Context;
using RestApiHW13.Service;

namespace HomeWork13RestApi.Modules
{
    public static class CoreModule
    {
        public static IServiceCollection AddCore(this IServiceCollection services, IConfiguration configuration)
        {
            services.Scan(scan => scan
                .FromAssembliesOf(typeof(IRequestHandler<>))
                .AddClasses(classes => classes.AssignableTo(typeof(IRequestHandler<,>)))
                    .AsImplementedInterfaces()
                    .WithTransientLifetime()
                .AddClasses(classes => classes.AssignableTo(typeof(IRequestHandler<>)))
                    .AsImplementedInterfaces()
                    .WithTransientLifetime());

            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("RestApiHW13"));
            });

            return services;
        }
    }
}
