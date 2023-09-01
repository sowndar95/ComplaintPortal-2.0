using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ComplaintPortalServices;

namespace ComplaintPortalServices
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection service, ConfigurationManager configuration)
        {
            service.AddDataProtection();
            service.AddScoped<AuthenticationService>();
            service.AddScoped<AuthTokenService>();
            service.AddScoped<UserService>();
            service.AddScoped<MenuService>();
            service.AddScoped<RoleService>();
            return service;
        }
    }
}
