using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ShipmentTrackingApp.Data;
using ShipmentTrackingApp.Models;
using ShipmentTrackingApp.Models.Enums;

namespace ShipmentTrackingApp.Repositories;

public class ShipmentRepository : IShipmentRepository
{
    private readonly AppDbContext _context;

    public ShipmentRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Shipment?> GetByIdAsync(int id) => await _context.Shipments.FindAsync(id);

    public async Task<Shipment?> GetByShipmentIdAsync(string shipmentId) => 
        await _context.Shipments.FirstOrDefaultAsync(s => s.ShipmentId == shipmentId);

    public async Task<IEnumerable<Shipment>> GetAllAsync() => 
        await _context.Shipments.OrderByDescending(s => s.BookingDate).ToListAsync();

    public async Task<IEnumerable<Shipment>> GetFilteredAsync(string? search, ShipmentStatus? status, ShipmentType? type)
    {
        var query = _context.Shipments.AsQueryable();

        if (!string.IsNullOrWhiteSpace(search))
            query = query.Where(s => s.ShipmentId.Contains(search) || s.CustomerName.Contains(search));
        
        if (status.HasValue)
            query = query.Where(s => s.Status == status.Value);
        
        if (type.HasValue)
            query = query.Where(s => s.Type == type.Value);

        return await query.OrderByDescending(s => s.BookingDate).ToListAsync();
    }

    public async Task AddAsync(Shipment shipment)
    {
        await _context.Shipments.AddAsync(shipment);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Shipment shipment)
    {
        _context.Shipments.Update(shipment);
        await _context.SaveChangesAsync();
    }

    public async Task<int> GetTodayShipmentCountAsync()
    {
        var today = DateTime.UtcNow.Date;
        return await _context.Shipments.CountAsync(s => s.BookingDate.Date == today);
    }
}
