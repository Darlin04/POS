using Blocks.Application.Exceptions;
using Blocks.Domain.Abstractions;
using Blocks.Domain.Exceptions;
using Inventory.Application.Features.MeasurementUnits.Commands.UpdateMeasurementUnit;
using Inventory.Domain.MeasurementUnits;
using Inventory.Domain.MeasurementUnits.ValueObjects;
using Moq;
using Shouldly;

namespace Inventory.Tests.Application.Features.MeasurementUnits.Commands.UpdateMeasurementUnit;

[TestFixture]
public class UpdateMeasurementUnitCommandHandlerTests
{
    private Mock<IMeasurementUnitRepository> _repositoryMock;
    private Mock<IUnitOfWork> _unitOfWorkMock;
    private UpdateMeasurementUnitCommandHandler _handler;

    [SetUp]
    public void SetUp()
    {
        _repositoryMock = new Mock<IMeasurementUnitRepository>();
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _handler = new UpdateMeasurementUnitCommandHandler(_repositoryMock.Object, _unitOfWorkMock.Object);
    }

    [Test]
    public async Task ShouldUpdateMeasurementUnitWhenDataIsValid()
    {
        // Arrange
        var id = Guid.NewGuid();
        var measurementUnit = new MeasurementUnit(new MeasurementUnitName("Kilogramo"), new MeasurementUnitAbbreviation("Kg"));
        var command = new UpdateMeasurementUnitCommand(id, "Metro", "m");

        _repositoryMock
            .Setup(x => x.FindByIdAsync(id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(measurementUnit);
            
        _repositoryMock
            .Setup(x => x.ExistsByNameAsync(It.IsAny<string>(), id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);
        
        _repositoryMock
            .Setup(x => x.ExistsByAbbreviationAsync(It.IsAny<string>(), id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        _unitOfWorkMock
            .Setup(x => x.CommitAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(1);
        
        // Act
        await _handler.Handle(command, CancellationToken.None);
        
        // Assert
        measurementUnit.MeasurementUnitName.Value.ShouldBe(command.Name);
        measurementUnit.MeasurementUnitAbbreviation.Value.ShouldBe(command.Abbreviation);
        
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
        var command = new UpdateMeasurementUnitCommand(id, "Metro", "m");

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
    }
    
    [Test]
    public async Task ShouldThrowExceptionWhenNameAlreadyExists()
    {
        // Arrange
        var id = Guid.NewGuid();
        var measurementUnit = new MeasurementUnit(new MeasurementUnitName("Kilogramo"), new MeasurementUnitAbbreviation("Kg"));
        var command = new UpdateMeasurementUnitCommand(id, "Metro", "m");

        _repositoryMock
            .Setup(x => x.FindByIdAsync(id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(measurementUnit);
        
        _repositoryMock
            .Setup(x => x.ExistsByNameAsync("Metro", id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);
    
        // Act & Assert
        var ex = await Should.ThrowAsync<DomainException>(
            () => _handler.Handle(command, CancellationToken.None)
        );
    
        ex.Message.ShouldBe("Ya existe una unidad de medida con ese nombre");
    }
    
    [Test]
    public async Task ShouldThrowExceptionWhenAbbreviationAlreadyExists()
    {
        // Arrange
        var id = Guid.NewGuid();
        var measurementUnit = new MeasurementUnit(new MeasurementUnitName("Kilogramo"), new MeasurementUnitAbbreviation("Kg"));
        var command = new UpdateMeasurementUnitCommand(id, "Metro", "m");

        _repositoryMock
            .Setup(x => x.FindByIdAsync(id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(measurementUnit);
        
        _repositoryMock
            .Setup(x => x.ExistsByNameAsync("Metro", id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);
        
        _repositoryMock
            .Setup(x => x.ExistsByAbbreviationAsync("m", id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);
    
        // Act & Assert
        var ex = await Should.ThrowAsync<DomainException>(
            () => _handler.Handle(command, CancellationToken.None)
        );
    
        ex.Message.ShouldBe("Ya existe una unidad de medida con esa abreviatura");
    }
}