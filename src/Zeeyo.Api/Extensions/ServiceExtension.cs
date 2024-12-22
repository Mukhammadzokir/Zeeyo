using Zeeyo.Service.Mappers;
using Zeeyo.Data.Repositories;
using Zeeyo.Data.IRepositories;
using Zeeyo.Service.Services.Branches;
using Zeeyo.Service.Interfaces.Branches;

namespace Zeeyo.Api.Extensions;

public static class ServiceExtension
{
    public static void AddCustomService(this IServiceCollection services)
    {
        // Mapper
        services.AddAutoMapper(typeof(MappingProfile));
        
        // Repository
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

        // Services
        services.AddScoped<IBranchService, BranchService>();
    }
}
