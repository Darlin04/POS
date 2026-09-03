using Inventory.Presentation.Endpoints.MeasurementUnits;
using Inventory.Presentation.Endpoints.Products;
using Microsoft.AspNetCore.Routing;
namespace Inventory.Presentation;

public static class EndpointsRegistration
{
    public static IEndpointRouteBuilder MapAllEndpoints(this IEndpointRouteBuilder app)
    {
        #region MeasurementUnits
        app
            .MapCreateMeasurementUnitEndpoint()
            .MapGetMeasurementUnitsEndpoint()
            .MapGetMeasurementUnitByIdEndpoint()
            .MapUpdateMeasurementUnitEndpoint()
            .MapDeleteMeasurementUnitEndpoint();
        #endregion
        
        #region Products
        app
            .MapCreateProductEndpoint()
            .MapGetProductsEndpoint()
            .MapGetProductByBarCodeEndpoint()
            ;
        #endregion
        
        return app;
    }
}