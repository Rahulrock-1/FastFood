using FastFood.Application.Services.Authentication;
using Microsoft.Extensions.DependencyInjection;

namespace FastFood.Application;

public static class DependenyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IAuthenticationService, AuthenticationService>();
        return services;
    }
}