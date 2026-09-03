using System.Text.Json.Serialization;
using MediatR;

namespace Inventory.Application.Features.MeasurementUnits.Commands.DeleteMeasurementUnit;

public record DeleteMeasurementUnitCommand([property: JsonIgnore] Guid Id) : IRequest;