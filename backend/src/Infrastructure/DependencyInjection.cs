using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using QH.Domain.Repositories;
using QH.Infrastructure.Data;
using QH.Infrastructure.Repositories;

namespace QH.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(
                configuration.GetConnectionString("DefaultConnection"),
                b => b.MigrationsAssembly("QH.Infrastructure")));

        services.AddScoped<ITaskRepository, TaskRepository>();

        return services;
    }
}
