using System.Collections.Generic;
using System.Threading.Tasks;
using ShipmentTrackingApp.Models;
using ShipmentTrackingApp.Models.Enums;

namespace ShipmentTrackingApp.Repositories;

public interface IShipmentRepository
{
    Task<Shipment?> GetByIdAsync(int id);
    Task<Shipment?> GetByShipmentIdAsync(string shipmentId);
    Task<IEnumerable<Shipment>> GetAllAsync();
    Task<IEnumerable<Shipment>> GetFilteredAsync(string? search, ShipmentStatus? status, ShipmentType? type);
    Task AddAsync(Shipment shipment);
    Task UpdateAsync(Shipment shipment);
    Task<int> GetTodayShipmentCountAsync();
}
