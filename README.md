# POS — Sistema de Facturación

API backend para punto de venta y facturación, construida con .NET 10 y ASP.NET Core. Arquitectura de monolito modular con separación por capas y principios de Domain-Driven Design (DDD).

## Stack

- **.NET 10 / ASP.NET Core** — Minimal APIs versionadas (`/api/v1`), OpenAPI
- **MediatR + CQRS** — Commands / Queries con handlers por caso de uso
- **FluentValidation** — validación con pipeline (`ValidationBehavior`)
- **AutoMapper** — mapeo Application ↔ DTOs
- **Entity Framework Core + Npgsql** — persistencia en PostgreSQL, migraciones por módulo
- **NUnit + Moq + Shouldly** — tests de dominio y aplicación

## Arquitectura

```text
Source/
├── Api/POS.Api                 # composición, Program.cs, /api/v1
├── BuildingBlocks/
│   ├── Blocks.Domain           # AggregateRoot, value objects (Money, Itbis), guards
│   ├── Blocks.Application      # ValidationBehavior, excepciones base
│   └── Blocks.EntityFramework  # repositorio genérico, Unit of Work
└── Modules/Inventory/
    ├── Inventory.Domain        # Product, Category, MeasurementUnit + value objects
    ├── Inventory.Application   # features CQRS (productos, unidades de medida)
    ├── Inventory.Persistence   # DbContext, configuraciones, migraciones, queries
    └── Inventory.Presentation  # endpoints por recurso
Tests/Modules/Inventory/Inventory.Tests
```

Módulo implementado: **Inventory** (productos por código de barras, categorías, unidades de medida, stock/cantidad).

## Correr local

```bash
dotnet restore
dotnet build POS.sln
dotnet test POS.sln
```

Configurar la conexión en `Source/Api/POS.Api/appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Database=pos;Username=postgres;Password=***"
  }
}
```

```bash
dotnet run --project Source/Api/POS.Api
```

## Diseño

- Value objects inmutables con validación en el dominio (`BarCode`, `Stock`, `Quantity`, `Money`, `Itbis`)
- Repositorios por agregado + `IUnitOfWork`; lecturas optimizadas vía `IProductQueries`
- Validación centralizada con MediatR pipeline + FluentValidation
