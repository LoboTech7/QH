using Microsoft.Extensions.DependencyInjection;
using QH.Application.Interfaces;
using QH.Application.Services;

namespace QH.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IWorkCalendarService, WorkCalendarService>();
        services.AddScoped<ITaskService, TaskService>();
        return services;
    }
}
