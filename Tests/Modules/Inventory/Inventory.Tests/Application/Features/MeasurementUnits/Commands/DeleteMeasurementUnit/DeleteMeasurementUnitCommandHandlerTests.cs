using Blocks.Application.Exceptions;
using Blocks.Domain.Abstractions;
using Blocks.EntityFramework;
using Inventory.Application.Features.MeasurementUnits.Commands.DeleteMeasurementUnit;
using Inventory.Domain.MeasurementUnits;
using Inventory.Domain.MeasurementUnits.ValueObjects;
using Moq;

namespace Inventory.Tests.Application.Features.MeasurementUnits.Commands.DeleteMeasurementUnit;

[TestFixture]
public class DeleteMeasurementUnitCommandHandlerTests
{
    private Mock<IMeasurementUnitRepository> _repositoryMock;
    private Mock<IUnitOfWork> _unitOfWorkMock;
    private DeleteMeasurementUnitCommandHandler _handler;

    [SetUp]
    public void SetUp()
    {
        _repositoryMock = new Mock<IMeasurementUnitRepository>();
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _handler = new DeleteMeasurementUnitCommandHandler(_repositoryMock.Object, _unitOfWorkMock.Object);
    }

    [Test]
    public async Task ShouldDeleteMeasurementUnitWhenExists()
    {
        // Arrange
        var id = Guid.NewGuid();
        var measurementUnit = new MeasurementUnit(new MeasurementUnitName("Kilogramo"), new MeasurementUnitAbbreviation("Kg"));
        var command = new DeleteMeasurementUnitCommand(id);

        _repositoryMock
            .Setup(x => x.FindByIdAsync(id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(measurementUnit);

        _unitOfWorkMock
            .Setup(x => x.CommitAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(1);

        // Act
        await _handler.Handle(command, CancellationToken.None);

        // Assert
        _repositoryMock.Verify(
            x => x.Remove(measurementUnit),
            Times.Once
        );

        _unitOfWorkMock.Verify(
            x => x.CommitAsync(It.IsAny<CancellationToken>()),
            Times.Once
        );
    }

    [Test]
    public void Handle_WhenMeasurementUnitDoesNotExist_ThrowsNotFoundException()
    {
        // Arrange
        var id = Guid.NewGuid();
        var command = new DeleteMeasurementUnitCommand(id);

        _repositoryMock
            .Setup(x => x.FindByIdAsync(id, It.IsAny<CancellationToken>()))
            .ReturnsAsync((MeasurementUnit?)null);

        // Act & Assert
        var ex = Assert.ThrowsAsync<NotFoundException>(
            async () => await _handler.Handle(command, CancellationToken.None)
        );

        Assert.That(ex, Is.Not.Null);
        Assert.That(ex.Message, Does.Contain("unidad de medida"));

        _repositoryMock.Verify(
            x => x.FindByIdAsync(id, It.IsAny<CancellationToken>()),
            Times.Once
        );

        _repositoryMock.Verify(
            x => x.Remove(It.IsAny<MeasurementUnit>()),
            Times.Never
        );

        _unitOfWorkMock.Verify(
            x => x.CommitAsync(It.IsAny<CancellationToken>()),
            Times.Never
        );
    }
}