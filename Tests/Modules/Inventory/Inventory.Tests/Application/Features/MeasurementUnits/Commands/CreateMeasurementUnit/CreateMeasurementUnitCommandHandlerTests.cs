using Blocks.Domain.Abstractions;
using Blocks.Domain.Exceptions;
using Inventory.Application.Features.MeasurementUnits.Commands.CreateMeasurementUnit;
using Inventory.Domain.MeasurementUnits;
using Moq;
using Shouldly;

namespace Inventory.Tests.Application.Features.MeasurementUnits.Commands.CreateMeasurementUnit;

[TestFixture]
public class CreateMeasurementUnitCommandHandlerTests
{
    private Mock<IMeasurementUnitRepository> _measurementUnitRepositoryMock;
    private Mock<IUnitOfWork> _unitOfWorkMock;
    private CreateMeasurementUnitCommandHandler _handler;

    [SetUp]
    public void Setup()
    {
        _measurementUnitRepositoryMock = new Mock<IMeasurementUnitRepository>();
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _handler = new CreateMeasurementUnitCommandHandler(
            _measurementUnitRepositoryMock.Object,
            _unitOfWorkMock.Object);
    }

    [Test]
    public async Task ShouldCreateMeasurementUnitWhenDataIsValid()
    {
        // Arrange
        var command = new CreateMeasurementUnitCommand("Kilogramo", "kg");
        
        _measurementUnitRepositoryMock
            .Setup(x => x.ExistsByNameAsync(command.Name, null, It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);
            
        _measurementUnitRepositoryMock
            .Setup(x => x.ExistsByAbbreviationAsync(command.Abbreviation, null, It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);
            
        _unitOfWorkMock
            .Setup(x => x.CommitAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(1);

        // Act
        await _handler.Handle(command, CancellationToken.None);

        // Assert
        _measurementUnitRepositoryMock.Verify(
            x => x.AddAsync(It.Is<MeasurementUnit>(mu => 
                mu.MeasurementUnitName == command.Name &&
                mu.MeasurementUnitAbbreviation == command.Abbreviation),
                It.IsAny<CancellationToken>()),
            Times.Once);
            
        _unitOfWorkMock.Verify(
            x => x.CommitAsync(It.IsAny<CancellationToken>()), 
            Times.Once);
    }

    [Test]
    public void ShouldThrowDomainExceptionWhenNameAlreadyExists()
    {
        // Arrange
        var command = new CreateMeasurementUnitCommand("Metro", "m");
        
        _measurementUnitRepositoryMock
            .Setup(x => x.ExistsByNameAsync(command.Name, null, It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        // Act & Assert
        Assert.ThrowsAsync<DomainException>(async () =>
            await _handler.Handle(command, CancellationToken.None));
        
        _measurementUnitRepositoryMock.Verify(
            x => x.AddAsync(It.IsAny<MeasurementUnit>(), It.IsAny<CancellationToken>()), 
            Times.Never);
            
        _unitOfWorkMock.Verify(
            x => x.CommitAsync(It.IsAny<CancellationToken>()), 
            Times.Never);
    }

    [Test]
    public void ShouldThrowDomainExceptionWhenAbbreviationAlreadyExists()
    {
        // Arrange
        var command = new CreateMeasurementUnitCommand("Liter", "L");
        
        _measurementUnitRepositoryMock
            .Setup(x => x.ExistsByNameAsync(command.Name, null, It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);
            
        _measurementUnitRepositoryMock
            .Setup(x => x.ExistsByAbbreviationAsync(command.Abbreviation, null,It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        // Act & Assert
        Assert.ThrowsAsync<DomainException>(async () =>
            await _handler.Handle(command, CancellationToken.None));
        
        _measurementUnitRepositoryMock.Verify(
            x => x.AddAsync(It.IsAny<MeasurementUnit>(), It.IsAny<CancellationToken>()), 
            Times.Never);
            
        _unitOfWorkMock.Verify(
            x => x.CommitAsync(It.IsAny<CancellationToken>()), 
            Times.Never);
    }
}
