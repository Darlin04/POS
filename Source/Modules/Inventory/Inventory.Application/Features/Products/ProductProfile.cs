using AutoMapper;
using Inventory.Domain.Products;

namespace Inventory.Application.Features.Products;

public class ProductProfile : Profile
{
    public ProductProfile()
    {
        CreateMap<Product, ProductDto>()
            .ForMember(dto => dto.BarCode, opt => opt.MapFrom(src => src.BarCode.Value))
            .ForMember(dto => dto.Cost, opt => opt.MapFrom(src => src.Cost.Amount))
            .ForMember(dto => dto.Currency, opt => opt.MapFrom(src => src.Cost.Currency))
            .ForMember(dto => dto.Price, opt => opt.MapFrom(src => src.Price.Amount))
            .ForMember(dto=> dto.WholesaleQuantity, opt => opt.MapFrom(src => src.WholesaleQuantity.Value))
            .ForMember(dto => dto.WholesalePrice, opt => opt.MapFrom(src => src.WholesalePrice.Amount))
            .ForMember(dto => dto.Stock, opt => opt.MapFrom(src => src.Stock.Value));
    }
}