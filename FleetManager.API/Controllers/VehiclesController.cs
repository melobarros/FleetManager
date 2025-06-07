using FleetManager.Application.DTOs;
using FleetManager.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace FleetManager.API.Controllers
{
    [ApiController]
    [Route("api/vehicles")]
    public class VehiclesController : ControllerBase
    {
        private readonly IVehicleAppService _vehicleAppService;

        public VehiclesController(IVehicleAppService vehicleAppService)
        {
            _vehicleAppService = vehicleAppService;
        }

        [HttpGet("{chassisSeries}/{chassisNumber}")]
        public ActionResult<VehicleDto> GetByChassis(string chassisSeries, uint chassisNumber)
        {
            var vehicle = _vehicleAppService.GetByChassis(chassisSeries, chassisNumber);

            if (vehicle == null)
                return NotFound($"Vehicle with chassis series {chassisSeries} and number {chassisNumber} not found.");

            return Ok(vehicle);
        }

        [HttpPost]
        public ActionResult<VehicleDto> Create([FromBody] VehicleDto dto)
        {
            if (dto == null)
                return BadRequest("Vehicle data is required.");

            var createdVehicle = _vehicleAppService.Create(dto);
            return CreatedAtAction(nameof(GetByChassis), new { chassisSeries = createdVehicle.ChassisSeries, chassisNumber = createdVehicle.ChassisNumber }, createdVehicle);
        }

        [HttpGet]
        public ActionResult<IEnumerable<VehicleDto>> GetAll()
        {
            var list = _vehicleAppService.GetAll();
            return Ok(list);
        }

        [HttpPut("{chassisSeries}/{chassisNumber}/color")]
        public ActionResult<VehicleDto> ChangeColor(string chassisSeries, uint chassisNumber, [FromBody] string color)
        {
            var updatedVehicle = _vehicleAppService.ChangeColor(chassisSeries, chassisNumber, color);
            return Ok(updatedVehicle);
        }
    }
}
