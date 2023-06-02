using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TODOListUniProject.Domain.Commands;
using TODOListUniProject.Domain.Database;

namespace TODOList.Domain;

public static class ListExtensions
{
    public static IServiceCollection AddDomainServices(this IServiceCollection services, Action<IServiceProvider, 
        DbContextOptionsBuilder> dbOptionsAction)
    {
        services.AddMediatR(o => o.RegisterServicesFromAssemblies(typeof(CreateObjectiveCommand).Assembly));
        services.AddDbContext<ListDbContext>(dbOptionsAction);
        return services;
    }
}