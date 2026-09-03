using Blocks.Application.Exceptions;
using Blocks.EntityFramework;
using Inventory.Application.Features.MeasurementUnits.Queries.GetMeasurementUnitById;
using Inventory.Domain.MeasurementUnits;
using Inventory.Domain.MeasurementUnits.ValueObjects;
using Moq;

namespace Inventory.Tests.Application.Features.MeasurementUnits.Queries.GetMeasurementUnitById;

[TestFixture]
public class GetMeasurementUnitByIdQueryHandlerTests
{
    private Mock<IMeasurementUnitRepository> _repositoryMock;
    private GetMeasurementUnitByIdQueryHandler _handler;

    [SetUp]
    public void SetUp()
    {
        _repositoryMock = new Mock<IMeasurementUnitRepository>();
        _handler = new GetMeasurementUnitByIdQueryHandler(_repositoryMock.Object);
    }

    [Test]
    public async Task Handle_WhenMeasurementUnitExists_ReturnsMappedDto()
    {
        // Arrange
        var measurementUnit = CreateMeasurementUnit("Metro", "m");

        _repositoryMock
            .Setup(x => x.FindByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(measurementUnit);
        
        var query = new GetMeasurementUnitByIdQuery(Guid.NewGuid());
        
        // Act
        var result = await _handler.Handle(query, CancellationToken.None);
        
        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Id, Is.EqualTo(measurementUnit.Id));
        Assert.That(result.Name, Is.EqualTo(measurementUnit.MeasurementUnitName.Value));
        Assert.That(result.Abbreviation, Is.EqualTo(measurementUnit.MeasurementUnitAbbreviation.Value));
        
        _repositoryMock.Verify(x => x.FindByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()), Times.Once);
    }
    
    [Test]
    public void Handle_WhenMeasurementUnitDoesNotExist_ThrowsNotFoundException()
    {
        // Arrange
        var id = Guid.NewGuid();

        _repositoryMock
            .Setup(x => x.FindByIdAsync(id, It.IsAny<CancellationToken>()))
            .ReturnsAsync((MeasurementUnit?)null);

        var query = new GetMeasurementUnitByIdQuery(id);

        // Act & Assert
        var ex = Assert.ThrowsAsync<NotFoundException>(
            async () => await _handler.Handle(query, CancellationToken.None)
        );

        Assert.That(ex, Is.Not.Null);
        Assert.That(ex.Message, Does.Contain("unidad de medida"));

        _repositoryMock.Verify(
            x => x.FindByIdAsync(id, It.IsAny<CancellationToken>()),
            Times.Once
        );
    }
    
    private static MeasurementUnit CreateMeasurementUnit(string name, string abbreviation)
    {
        return new MeasurementUnit(
            new MeasurementUnitName(name),
            new MeasurementUnitAbbreviation(abbreviation)
        );
    }
}