using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
using Texon.Domin.Contracts;
using Texon.Domin.Entities.Auth;
using Texon.Persistence.Attachment;
using Texon.Persistence.cashService;
using Texon.Persistence.Context;
using Texon.Persistence.DBInitializers;
using Texon.Persistence.Repository;
using Texon.Service.Abstraction.IService;
using Texon.Service.Service;

namespace Texon.Persistence.DependencyInjection
{
    public static class AddPersistenceService
    {
         
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddSingleton<IConnectionMultiplexer>(cfg =>
            {
                return ConnectionMultiplexer.Connect(configuration.GetConnectionString("Redis")!);
            });

            #region sqlConnection
            services.AddDbContext<TexonContext>(options =>
               options.UseSqlServer(configuration.GetConnectionString("ConnectionString"),
               b => b.MigrationsAssembly(typeof(TexonContext).Assembly.FullName)));

            #endregion


            #region Services
            services.AddScoped<IAttachmentService, AttachmentService>();
            services.AddScoped<IDBInitializer, DBInitializer>();
            services.AddScoped<ICashService, CashService>();
            services.AddScoped<IAuthService, AuthService>();

            services.AddScoped<IBasketRepository, BasketRepository>();
            #endregion


            #region Identity
            services.AddIdentityCore<ApplicationUser>(opt =>
            {
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequiredLength = 6;
                opt.User.RequireUniqueEmail = true;
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequireUppercase = false;
                opt.Password.RequireDigit = false;
            }).AddRoles<IdentityRole>().AddEntityFrameworkStores<TexonContext>();

            #endregion
         
            return services;
        }
    }
}
