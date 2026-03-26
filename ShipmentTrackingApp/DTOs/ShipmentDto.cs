using System;

namespace ShipmentTrackingApp.DTOs;

public record ShipmentDto(
    string ShipmentId,
    string CustomerName,
    string CustomerEmail,
    string Origin,
    string Destination,
    string Type,
    string Status,
    DateTime BookingDate,
    DateTime ExpectedDeliveryDate,
    DateTime? ActualDeliveryDate
);
