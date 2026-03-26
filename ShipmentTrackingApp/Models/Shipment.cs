using System;
using System.ComponentModel.DataAnnotations;
using ShipmentTrackingApp.Models.Enums;

namespace ShipmentTrackingApp.Models;

public class Shipment
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string ShipmentId { get; set; } = string.Empty;

    [Required(ErrorMessage = "Customer Name is required")]
    [StringLength(100)]
    public string CustomerName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Customer Email is required")]
    [EmailAddress(ErrorMessage = "Invalid Email Address")]
    public string CustomerEmail { get; set; } = string.Empty;

    [Required]
    public string Origin { get; set; } = string.Empty;

    [Required]
    public string Destination { get; set; } = string.Empty;

    [Required]
    public ShipmentType Type { get; set; }

    [Required]
    public ShipmentStatus Status { get; set; }

    [Required]
    public DateTime BookingDate { get; set; }

    [Required]
    public DateTime ExpectedDeliveryDate { get; set; }

    public DateTime? ActualDeliveryDate { get; set; }

    public string? Notes { get; set; }
}
