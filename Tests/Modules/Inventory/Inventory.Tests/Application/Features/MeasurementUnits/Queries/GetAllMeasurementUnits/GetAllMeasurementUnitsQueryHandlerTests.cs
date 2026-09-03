using Inventory.Application.Features.MeasurementUnits.Queries.GetAllMeasurementUnits;
using Inventory.Domain.MeasurementUnits;
using Inventory.Domain.MeasurementUnits.ValueObjects;
using Moq;

namespace Inventory.Tests.Application.Features.MeasurementUnits.Queries.GetAllMeasurementUnits;

[TestFixture]
public class GetAllMeasurementUnitsQueryHandlerTests
{
    private Mock<IMeasurementUnitRepository> _repositoryMock;
    private GetAllMeasurementUnitsQueryHandler _handler;

    [SetUp]
    public void SetUp()
    {
        _repositoryMock = new Mock<IMeasurementUnitRepository>();
        _handler = new GetAllMeasurementUnitsQueryHandler(_repositoryMock.Object);
    }

    [Test]
    public async Task Handle_WhenMeasurementUnitsExist_ReturnsMappedDtos()
    {
        // Arrange
        var measurementUnits = new List<MeasurementUnit>
        {
            CreateMeasurementUnit("Kilogram", "kg"),
            CreateMeasurementUnit("Meter", "m"),
            CreateMeasurementUnit("Liter", "L")
        };

        _repositoryMock
            .Setup(x => x.GetAllAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(measurementUnits);

        var query = new GetAllMeasurementUnitsQuery();

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result, Has.Count.EqualTo(3));

        for (int i = 0; i < measurementUnits.Count; i++)
        {
            Assert.That(result[i].Id, Is.EqualTo(measurementUnits[i].Id));
            Assert.That(result[i].Name, Is.EqualTo(measurementUnits[i].MeasurementUnitName.Value));
            Assert.That(result[i].Abbreviation, Is.EqualTo(measurementUnits[i].MeasurementUnitAbbreviation.Value));
        }

        _repositoryMock.Verify(x => x.GetAllAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Test]
    public async Task Handle_WhenNoMeasurementUnitsExist_ReturnsEmptyList()
    {
        // Arrange
        var emptyList = new List<MeasurementUnit>();

        _repositoryMock
            .Setup(x => x.GetAllAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(emptyList);

        var query = new GetAllMeasurementUnitsQuery();

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result, Is.Empty);

        _repositoryMock.Verify(x => x.GetAllAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    private static MeasurementUnit CreateMeasurementUnit(string name, string abbreviation)
    {
        return new MeasurementUnit(
            new MeasurementUnitName(name),
            new MeasurementUnitAbbreviation(abbreviation));
    }
}
