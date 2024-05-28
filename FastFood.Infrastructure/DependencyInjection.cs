
using FastFood.Application.Common.Interfaces.Authentication;
using FastFood.Application.Common.Interfaces.Services;
using FastFood.Infrastructure.Authentication;
using FastFood.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using FastFood.Infrastructure.Persistence;
using FastFood.Application.Common.Interfaces.Persistence;

namespace FastFood.Infrastructure;

public static class DependenyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, ConfigurationManager configuration)
    {
        services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.SectionName));
        services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
        services.AddSingleton<IDateTieProviders, DateTimeProvider>();
        services.AddScoped<IUserRepository, UserRepository>();
        return services;
    }
}