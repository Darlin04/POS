using AutoMapper;
using Blocks.Application.Exceptions;
using Blocks.Domain.Abstractions;
using Blocks.Domain.Exceptions;
using Blocks.Domain.ValueObjects;
using Inventory.Domain.MeasurementUnits;
using Inventory.Domain.Products;
using Inventory.Domain.Products.ValueObjects;
using MediatR;

namespace Inventory.Application.Features.Products.Commands.CreateProduct;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ProductDto>
{
    private readonly IProductRepository _productRepository;
    private readonly IMeasurementUnitRepository _measurementUnitRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateProductCommandHandler(IProductRepository productRepository,
        IMeasurementUnitRepository measurementUnitRepository,
        IUnitOfWork unitOfWork, IMapper mapper)
    {
        _productRepository = productRepository;
        _measurementUnitRepository = measurementUnitRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public async Task<ProductDto> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        if (!await _productRepository.MeasurementUnitExistsAsync(request.MeasurementUnitId))
            throw new NotFoundException("Esa unidad de medida no existe");
        
        if (await _productRepository.BarCodeExistsAsync(request.BarCode))
            throw new DomainException("Ya existe un producto con ese código de barras");
        
        var product = new Product(
            new BarCode(request.BarCode),
            request.Description,
            new Money(request.Cost),
            new Money(request.Price),
            new Quantity(request.WholesaleQuantity),
            new Money(request.WholesalePrice),
            new Stock(request.Stock),
            new Stock(request.MinStock),
            request.MeasurementUnitId 
            );

        await _productRepository.AddAsync(product, cancellationToken);
        await _unitOfWork.CommitAsync(cancellationToken);

        var productDto = _mapper.Map<ProductDto>(product);
        
        var measurementUnit = await _measurementUnitRepository.GetByIdAsync(product.MeasurementUnitId, cancellationToken);
        productDto.MeasurementUnit = measurementUnit!.MeasurementUnitName.Value;
        
        return productDto;
    }
}