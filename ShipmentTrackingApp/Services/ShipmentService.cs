using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShipmentTrackingApp.DTOs;
using ShipmentTrackingApp.Models;
using ShipmentTrackingApp.Models.Enums;
using ShipmentTrackingApp.Repositories;

namespace ShipmentTrackingApp.Services;

public class ShipmentService : IShipmentService
{
    private readonly IShipmentRepository _repository;

    public ShipmentService(IShipmentRepository repository)
    {
        _repository = repository;
    }

    public async Task<Shipment> CreateShipmentAsync(Shipment shipment)
    {
        int todayCount = await _repository.GetTodayShipmentCountAsync();
        shipment.ShipmentId = $"SHIP-{DateTime.UtcNow:yyyyMMdd}-{(todayCount + 1):D4}";
        shipment.BookingDate = DateTime.UtcNow;
        shipment.Status = ShipmentStatus.Pending;

        await _repository.AddAsync(shipment);
        return shipment;
    }

    public async Task<Shipment?> GetShipmentByIdAsync(int id)
    {
        var shipment = await _repository.GetByIdAsync(id);
        if (shipment != null)
        {
            await CheckAndAutoDelayAsync(shipment);
        }
        return shipment;
    }

    public async Task<ShipmentDto?> GetShipmentDtoByShipmentIdAsync(string shipmentId)
    {
        var shipment = await _repository.GetByShipmentIdAsync(shipmentId);
        return shipment == null ? null : MapToDto(shipment);
    }

    public async Task<IEnumerable<Shipment>> GetFilteredShipmentsAsync(string? search, ShipmentStatus? status, ShipmentType? type)
    {
        await CheckAndAutoDelayAllPendingAsync();
        return await _repository.GetFilteredAsync(search, status, type);
    }

    public async Task<IEnumerable<ShipmentDto>> GetShipmentsByStatusAsync(ShipmentStatus status)
    {
        await CheckAndAutoDelayAllPendingAsync();
        var shipments = await _repository.GetFilteredAsync(null, status, null);
        return shipments.Select(MapToDto).ToList();
    }

    public async Task UpdateShipmentStatusAsync(int id, ShipmentStatus newStatus)
    {
        var shipment = await _repository.GetByIdAsync(id) ?? throw new KeyNotFoundException("Shipment not found");

        shipment.Status = newStatus;
        if (newStatus == ShipmentStatus.Delivered)
        {
            shipment.ActualDeliveryDate = DateTime.UtcNow;
        }

        await _repository.UpdateAsync(shipment);
    }

    private async Task CheckAndAutoDelayAsync(Shipment shipment)
    {
        if (shipment.ExpectedDeliveryDate.Date < DateTime.UtcNow.Date && shipment.Status != ShipmentStatus.Delivered && shipment.Status != ShipmentStatus.Delayed)
        {
            shipment.Status = ShipmentStatus.Delayed;
            await _repository.UpdateAsync(shipment);
        }
    }

    private async Task CheckAndAutoDelayAllPendingAsync()
    {
        var shipments = await _repository.GetAllAsync();
        foreach (var shipment in shipments)
        {
            await CheckAndAutoDelayAsync(shipment);
        }
    }

    private static ShipmentDto MapToDto(Shipment shipment) => new ShipmentDto(
        shipment.ShipmentId,
        shipment.CustomerName,
        shipment.CustomerEmail,
        shipment.Origin,
        shipment.Destination,
        shipment.Type.ToString(),
        shipment.Status.ToString(),
        shipment.BookingDate,
        shipment.ExpectedDeliveryDate,
        shipment.ActualDeliveryDate
    );
}
