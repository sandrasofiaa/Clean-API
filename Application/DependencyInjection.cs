using FluentValidation;
using Infrastructure.Interface;
using Infrastructure.Repositories.AnimalRepository;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var assembly = typeof(DependencyInjection).Assembly;
        services.AddMediatR(configuration => configuration.RegisterServicesFromAssembly(assembly));
        services.AddValidatorsFromAssembly(assembly);

        services.AddScoped<IUserRepository, UserRepository>();

        services.AddScoped<IAnimalRepository, AnimalRepository>();

        return services;
    }
}