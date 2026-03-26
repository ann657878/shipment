using System;
using System.Collections.Generic;
using System.Linq;
using ShipmentTrackingApp.Models;
using ShipmentTrackingApp.Models.Enums;

namespace ShipmentTrackingApp.Data;

public static class DbSeeder
{
    public static void Seed(AppDbContext context)
    {
        context.Database.EnsureCreated();

        if (!context.Shipments.Any())
        {
            var today = DateTime.UtcNow;
            var shipments = new List<Shipment>
            {
                new Shipment { ShipmentId = "SHIP-20260323-0001", CustomerName = "Acme Corp", CustomerEmail = "logistics@acme.com", Origin = "New York", Destination = "London", Type = ShipmentType.Air, Status = ShipmentStatus.Delivered, BookingDate = today.AddDays(-10), ExpectedDeliveryDate = today.AddDays(-5), ActualDeliveryDate = today.AddDays(-6) },
                new Shipment { ShipmentId = "SHIP-20260323-0002", CustomerName = "Global Tech", CustomerEmail = "supply@global.com", Origin = "Shanghai", Destination = "San Francisco", Type = ShipmentType.Sea, Status = ShipmentStatus.InTransit, BookingDate = today.AddDays(-5), ExpectedDeliveryDate = today.AddDays(15) },
                new Shipment { ShipmentId = "SHIP-20260323-0003", CustomerName = "Local Mart", CustomerEmail = "store1@local.com", Origin = "Chicago", Destination = "Detroit", Type = ShipmentType.Road, Status = ShipmentStatus.Delayed, BookingDate = today.AddDays(-3), ExpectedDeliveryDate = today.AddDays(-1) },
                new Shipment { ShipmentId = "SHIP-20260323-0004", CustomerName = "Express LLC", CustomerEmail = "info@express.com", Origin = "Paris", Destination = "Berlin", Type = ShipmentType.Rail, Status = ShipmentStatus.Pending, BookingDate = today, ExpectedDeliveryDate = today.AddDays(2) },
                new Shipment { ShipmentId = "SHIP-20260324-0001", CustomerName = "Mega Store", CustomerEmail = "admin@megastore.com", Origin = "Los Angeles", Destination = "Tokyo", Type = ShipmentType.Air, Status = ShipmentStatus.InTransit, BookingDate = today.AddDays(-2), ExpectedDeliveryDate = today.AddDays(1) },
                new Shipment { ShipmentId = "SHIP-20260324-0002", CustomerName = "Tech Bros", CustomerEmail = "ceo@techbros.com", Origin = "Seattle", Destination = "Austin", Type = ShipmentType.Road, Status = ShipmentStatus.Delivered, BookingDate = today.AddDays(-7), ExpectedDeliveryDate = today.AddDays(-2), ActualDeliveryDate = today.AddDays(-2) },
                new Shipment { ShipmentId = "SHIP-20260325-0001", CustomerName = "Euro Imports", CustomerEmail = "import@euro.com", Origin = "Rome", Destination = "Madrid", Type = ShipmentType.Rail, Status = ShipmentStatus.Pending, BookingDate = today, ExpectedDeliveryDate = today.AddDays(4) },
                new Shipment { ShipmentId = "SHIP-20260326-0001", CustomerName = "Oceanic", CustomerEmail = "freight@oceanic.com", Origin = "Miami", Destination = "Sydney", Type = ShipmentType.Sea, Status = ShipmentStatus.InTransit, BookingDate = today.AddDays(-1), ExpectedDeliveryDate = today.AddDays(20) }
            };

            context.Shipments.AddRange(shipments);
            context.SaveChanges();
        }
    }
}
