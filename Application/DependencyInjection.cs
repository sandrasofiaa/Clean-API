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

        // Lägg till registreringen för IUserRepository och dess implementation här
        services.AddScoped<IUserRepository, UserRepository>(); // Byt ut UserRepository mot den faktiska implementationen

        // Lägg till registreringen för IAnimalRepository och dess implementation här
        services.AddScoped<IAnimalRepository, AnimalRepository>(); // För AnimalRepository

        return services;
    }
}