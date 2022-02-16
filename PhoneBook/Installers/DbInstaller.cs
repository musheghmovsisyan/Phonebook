using Microsoft.EntityFrameworkCore;
using PhoneBook.Core.Contracts.Common;
using PhoneBook.Core.Contracts.Container;
using PhoneBook.Infrastructure.Data;

namespace PhoneBook.Installers;

public class DbInstaller : IInstaller
{
    public void InstallServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<PhoneBookDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString(DbConnections.PhoneBookConnectionString)));
    }
}