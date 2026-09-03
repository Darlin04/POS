using System.Reflection;
using Blocks.Application.Behaviors;
using FluentValidation;
using Inventory.Application.Features.MeasurementUnits.Commands.CreateMeasurementUnit;
using Microsoft.Extensions.DependencyInjection;

namespace Inventory.Application;

public static class ServiceRegistration
{
    public static void AddApplicationServices(this IServiceCollection services)
    {
        var assembly = Assembly.GetExecutingAssembly();

        services.AddAutoMapper(assembly);
        services
            .AddValidatorsFromAssemblyContaining<CreateMeasurementUnitCommandValidator>()
            .AddMediatR(config =>
            {
                config.RegisterServicesFromAssembly(assembly);
                config.AddOpenBehavior(typeof(ValidationBehavior<,>));
            });
    }
}