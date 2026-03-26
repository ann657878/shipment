using System;
using System.Threading.Tasks;
using Moq;
using Xunit;
using ShipmentTrackingApp.Models;
using ShipmentTrackingApp.Models.Enums;
using ShipmentTrackingApp.Repositories;
using ShipmentTrackingApp.Services;

namespace ShipmentTrackingApp.Tests;

public class ShipmentServiceTests
{
    private readonly Mock<IShipmentRepository> _mockRepo;
    private readonly ShipmentService _service;

    public ShipmentServiceTests()
    {
        _mockRepo = new Mock<IShipmentRepository>();
        _service = new ShipmentService(_mockRepo.Object);
    }

    [Fact]
    public async Task CreateShipment_ShouldSetShipmentIdAndPendingStatus()
    {
        // Arrange
        _mockRepo.Setup(r => r.GetTodayShipmentCountAsync()).ReturnsAsync(5);
        var shipment = new Shipment { CustomerName = "John Doe" };

        // Act
        var result = await _service.CreateShipmentAsync(shipment);

        // Assert
        Assert.Equal(ShipmentStatus.Pending, result.Status);
        Assert.Contains($"SHIP-{DateTime.UtcNow:yyyyMMdd}-0006", result.ShipmentId);
        _mockRepo.Verify(r => r.AddAsync(It.IsAny<Shipment>()), Times.Once);
    }

    [Fact]
    public async Task GetShipmentById_ShouldAutoDelayIfPastExpectedDate()
    {
        // Arrange
        var pastDate = DateTime.UtcNow.AddDays(-2);
        var shipment = new Shipment 
        { 
            Id = 1, 
            Status = ShipmentStatus.InTransit, 
            ExpectedDeliveryDate = pastDate 
        };
        
        _mockRepo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(shipment);

        // Act
        var result = await _service.GetShipmentByIdAsync(1);

        // Assert
        Assert.Equal(ShipmentStatus.Delayed, result?.Status);
        _mockRepo.Verify(r => r.UpdateAsync(It.IsAny<Shipment>()), Times.Once);
    }

    [Fact]
    public async Task UpdateShipmentStatus_ToDelivered_ShouldSetActualDeliveryDate()
    {
        // Arrange
        var shipment = new Shipment { Id = 1, Status = ShipmentStatus.InTransit };
        _mockRepo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(shipment);

        // Act
        await _service.UpdateShipmentStatusAsync(1, ShipmentStatus.Delivered);

        // Assert
        Assert.Equal(ShipmentStatus.Delivered, shipment.Status);
        Assert.NotNull(shipment.ActualDeliveryDate);
        _mockRepo.Verify(r => r.UpdateAsync(It.IsAny<Shipment>()), Times.Once);
    }
}
