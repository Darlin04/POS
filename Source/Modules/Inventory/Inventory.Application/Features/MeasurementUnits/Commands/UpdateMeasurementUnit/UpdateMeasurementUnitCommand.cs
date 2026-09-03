using System.Text.Json.Serialization;
using AutoMapper.Configuration.Annotations;
using Inventory.Application.Features.MeasurementUnits.Queries;
using MediatR;

namespace Inventory.Application.Features.MeasurementUnits.Commands.UpdateMeasurementUnit;

public record UpdateMeasurementUnitCommand([property: JsonIgnore] Guid Id, string Name, string Abbreviation) 
    : IRequest<MeasurementUnitDto>;