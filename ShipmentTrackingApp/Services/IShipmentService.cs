using System.Collections.Generic;
using System.Threading.Tasks;
using ShipmentTrackingApp.DTOs;
using ShipmentTrackingApp.Models;
using ShipmentTrackingApp.Models.Enums;

namespace ShipmentTrackingApp.Services;

public interface IShipmentService
{
    Task<Shipment> CreateShipmentAsync(Shipment shipment);
    Task<Shipment?> GetShipmentByIdAsync(int id);
    Task<ShipmentDto?> GetShipmentDtoByShipmentIdAsync(string shipmentId);
    Task<IEnumerable<Shipment>> GetFilteredShipmentsAsync(string? search, ShipmentStatus? status, ShipmentType? type);
    Task<IEnumerable<ShipmentDto>> GetShipmentsByStatusAsync(ShipmentStatus status);
    Task UpdateShipmentStatusAsync(int id, ShipmentStatus newStatus);
}
