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
        private readonly IDiagnosticAppService _diagnosticAppService;

        public VehiclesController(IVehicleAppService vehicleAppService, IDiagnosticAppService diagnosticAppService)
        {
            _vehicleAppService = vehicleAppService;
            _diagnosticAppService = diagnosticAppService;
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

        [HttpDelete("{chassisSeries}/{chassisNumber}")]
        public ActionResult<VehicleDto> Delete(string chassisSeries, uint chassisNumber)
        {
            var dto = _vehicleAppService.Delete(chassisSeries, chassisNumber);
            return Ok(dto);
        }

        [HttpGet("{chassisSeries}/{chassisNumber}/diagnostic")]
        public ActionResult<DiagnosticResultDto> GetDiagnostic(string chassisSeries, uint chassisNumber)
        {
            var diag = _diagnosticAppService.RunDiagnostic(chassisSeries, chassisNumber);
            return Ok(diag);
        }
    }
}
