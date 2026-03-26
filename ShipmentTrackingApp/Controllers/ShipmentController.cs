using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ShipmentTrackingApp.Models;
using ShipmentTrackingApp.Models.Enums;
using ShipmentTrackingApp.Services;

namespace ShipmentTrackingApp.Controllers;

public class ShipmentController : Controller
{
    private readonly IShipmentService _shipmentService;

    public ShipmentController(IShipmentService shipmentService)
    {
        _shipmentService = shipmentService;
    }

    public async Task<IActionResult> Index(string? search, ShipmentStatus? status, ShipmentType? type)
    {
        ViewBag.Search = search;
        ViewBag.Status = status;
        ViewBag.Type = type;

        var shipments = await _shipmentService.GetFilteredShipmentsAsync(search, status, type);
        return View(shipments);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Shipment shipment)
    {
        ModelState.Remove("ShipmentId");
        
        if (ModelState.IsValid)
        {
            await _shipmentService.CreateShipmentAsync(shipment);
            TempData["SuccessMessage"] = $"Booking successful! Tracking ID: {shipment.ShipmentId}";
            return RedirectToAction(nameof(Index));
        }
        return View(shipment);
    }

    public async Task<IActionResult> Details(int id)
    {
        var shipment = await _shipmentService.GetShipmentByIdAsync(id);
        if (shipment == null) return NotFound();
        return View(shipment);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> UpdateStatus(int id, ShipmentStatus newStatus)
    {
        try
        {
            await _shipmentService.UpdateShipmentStatusAsync(id, newStatus);
        }
        catch (System.Collections.Generic.KeyNotFoundException)
        {
            return NotFound();
        }
        return RedirectToAction(nameof(Details), new { id });
    }
}
