using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FleetManager.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CleanupOrphanVehicles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
              PRAGMA foreign_keys = OFF;
              DELETE FROM Vehicles
              WHERE DiagnosticProtocolId IS NULL
                 OR DiagnosticProtocolId NOT IN (SELECT Id FROM DiagnosticProtocols);
              PRAGMA foreign_keys = ON;
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
