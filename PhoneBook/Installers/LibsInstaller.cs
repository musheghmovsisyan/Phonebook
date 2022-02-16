using PhoneBook.Core.Contracts.Container;
using PhoneBook.Infrastructure;

namespace PhoneBook.Installers;

public class LibsInstaller : IInstaller
{
    public void InstallServices(IServiceCollection services, IConfiguration configuration)
    {
        services.RegisterInfrastructerServices(configuration);
    }
}