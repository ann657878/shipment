using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ShipmentTrackingApp.Models.Enums;
using ShipmentTrackingApp.Services;

namespace ShipmentTrackingApp.Controllers.Api;

[Route("api/shipments")]
[ApiController]
public class ShipmentApiController : ControllerBase
{
    private readonly IShipmentService _shipmentService;

    public ShipmentApiController(IShipmentService shipmentService)
    {
        _shipmentService = shipmentService;
    }

    // GET: api/shipments/{shipmentId}
    [HttpGet("{shipmentId}")]
    public async Task<IActionResult> Get(string shipmentId)
    {
        var shipmentDto = await _shipmentService.GetShipmentDtoByShipmentIdAsync(shipmentId);
        if (shipmentDto == null) return NotFound();
        return Ok(shipmentDto);
    }

    // GET: api/shipments?status=InTransit
    [HttpGet]
    public async Task<IActionResult> GetByStatus([FromQuery] string status)
    {
        if (Enum.TryParse<ShipmentStatus>(status, true, out var parsedStatus))
        {
            var shipments = await _shipmentService.GetShipmentsByStatusAsync(parsedStatus);
            return Ok(shipments);
        }
        return BadRequest("Invalid status value.");
    }
}
