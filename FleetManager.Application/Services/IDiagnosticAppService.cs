using FleetManager.Application.DTOs;

namespace FleetManager.Application.Services
{
    public interface IDiagnosticAppService
    {
        DiagnosticResultDto RunDiagnostic(string chassisSeries, uint chassisNumber);
    }
}
