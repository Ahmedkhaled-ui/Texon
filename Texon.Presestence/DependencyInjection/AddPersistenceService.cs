using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Texon.Domin.Contracts;
using Texon.Persistence.Context;
using Texon.Persistence.DBInitializers;

namespace Texon.Persistence.DependencyInjection
{
    public static class AddPersistenceService
    {
         
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<TexonContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("ConnectionString"),
                b => b.MigrationsAssembly(typeof(TexonContext).Assembly.FullName)));
            services.AddScoped<IDBInitializer, DBInitializer>();
            return services;
        }
    }
}
