using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PhoneBook.Core.Contracts.Common;
using PhoneBook.Core.Contracts.Services;
using PhoneBook.Infrastructure.Data;
using PhoneBook.Infrastructure.Services;

namespace PhoneBook.Infrastructure;

public static class InfrastructerLibrary
{
    public static IServiceCollection RegisterInfrastructerServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<PhoneBookDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString(DbConnections.PhoneBookConnectionString)));

        services.AddScoped<IUserProfileServices, UserProfileServices>();

        return services;
    }
}