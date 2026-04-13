using E_Commerce.infrastructure.services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Texon.Service.Contracts;

namespace Texon.infrastructure.Service
{
    public static class DependencyInjections
    {
        public static IServiceCollection AddInfrastrucctureService(this IServiceCollection services,
            IConfiguration configuration)
        {

            services.AddScoped<ITokenService, TokenService>();
            services.Configure<JwtOptions>(configuration.GetSection(JwtOptions.SectionName));
            return services;
        }
     }
}
