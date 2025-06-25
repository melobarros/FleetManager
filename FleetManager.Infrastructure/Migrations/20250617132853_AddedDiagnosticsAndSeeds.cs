using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FleetManager.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedDiagnosticsAndSeeds : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DiagnosticProtocolId",
                table: "Vehicles",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "DiagnosticProtocols",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    VehicleType = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiagnosticProtocols", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ErrorCodes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Code = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    ProtocolId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ErrorCodes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ErrorCodes_DiagnosticProtocols_ProtocolId",
                        column: x => x.ProtocolId,
                        principalTable: "DiagnosticProtocols",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Sensors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Unit = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    ProtocolId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sensors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sensors_DiagnosticProtocols_ProtocolId",
                        column: x => x.ProtocolId,
                        principalTable: "DiagnosticProtocols",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "DiagnosticProtocols",
                columns: new[] { "Id", "Name", "VehicleType" },
                values: new object[,]
                {
                    { 1, "Truck Diagnostic Protocol", 1 },
                    { 2, "Bus Diagnostic Protocol", 2 },
                    { 3, "Car Diagnostic Protocol", 0 }
                });

            migrationBuilder.InsertData(
                table: "ErrorCodes",
                columns: new[] { "Id", "Code", "Description", "ProtocolId" },
                values: new object[,]
                {
                    { 1, "T001", "Engine Overheating", 1 },
                    { 2, "T002", "Low Oil Pressure", 1 },
                    { 3, "T003", "Fuel System Leak", 1 },
                    { 4, "T004", "Brake Air Pressure Low", 1 },
                    { 5, "T005", "Battery Voltage Low", 1 },
                    { 6, "T006", "Transmission Oil Overheating", 1 },
                    { 7, "B001", "Passenger Door Sensor Fault", 2 },
                    { 8, "B002", "AC Compressor Failure", 2 },
                    { 9, "B003", "Brake Air Pressure Low", 2 },
                    { 10, "B004", "Wheel Speed Sensor Failure", 2 },
                    { 11, "B005", "Communication Bus Error", 2 },
                    { 12, "C001", "Engine Overheating", 3 },
                    { 13, "C002", "Low Brake Fluid", 3 },
                    { 14, "C003", "Low Tire Pressure", 3 },
                    { 15, "C004", "Battery Voltage Low", 3 },
                    { 16, "C005", "Fuel System Leak", 3 }
                });

            migrationBuilder.InsertData(
                table: "Sensors",
                columns: new[] { "Id", "Name", "ProtocolId", "Unit" },
                values: new object[,]
                {
                    { 1, "Engine Coolant Temperature", 1, "°C" },
                    { 2, "Engine Speed (RPM)", 1, "RPM" },
                    { 3, "Oil Pressure", 1, "Bar" },
                    { 4, "Fuel Level", 1, "%" },
                    { 5, "Battery Voltage", 1, "V" },
                    { 6, "Brake Air Pressure", 1, "Bar" },
                    { 7, "Transmission Oil Temperature", 1, "°C" },
                    { 8, "Engine Coolant Temperature", 2, "°C" },
                    { 9, "Engine Speed (RPM)", 2, "RPM" },
                    { 10, "Fuel Level", 2, "%" },
                    { 11, "Passenger Door Status", 2, "" },
                    { 12, "AC Compressor Status", 2, "" },
                    { 13, "Brake Air Pressure", 2, "Bar" },
                    { 14, "Wheel Speed Front Axle", 2, "km/h" },
                    { 15, "Engine Coolant Temperature", 3, "°C" },
                    { 16, "Engine Speed (RPM)", 3, "RPM" },
                    { 17, "Oil Pressure", 3, "Bar" },
                    { 18, "Fuel Level", 3, "%" },
                    { 19, "Battery Voltage", 3, "V" },
                    { 20, "Brake Fluid Level", 3, "%" },
                    { 21, "Tire Pressure", 3, "PSI" }
                });

            migrationBuilder.Sql("PRAGMA foreign_keys = OFF;");
            migrationBuilder.Sql(@"
                DELETE FROM Vehicles
                WHERE DiagnosticProtocolId = 0
                OR DiagnosticProtocolId NOT IN (SELECT Id FROM DiagnosticProtocols);
            ");
            migrationBuilder.Sql("PRAGMA foreign_keys = ON;");

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_DiagnosticProtocolId",
                table: "Vehicles",
                column: "DiagnosticProtocolId");

            migrationBuilder.CreateIndex(
                name: "IX_ErrorCodes_ProtocolId",
                table: "ErrorCodes",
                column: "ProtocolId");

            migrationBuilder.CreateIndex(
                name: "IX_Sensors_ProtocolId",
                table: "Sensors",
                column: "ProtocolId");

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicles_DiagnosticProtocols_DiagnosticProtocolId",
                table: "Vehicles",
                column: "DiagnosticProtocolId",
                principalTable: "DiagnosticProtocols",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vehicles_DiagnosticProtocols_DiagnosticProtocolId",
                table: "Vehicles");

            migrationBuilder.DropTable(
                name: "ErrorCodes");

            migrationBuilder.DropTable(
                name: "Sensors");

            migrationBuilder.DropTable(
                name: "DiagnosticProtocols");

            migrationBuilder.DropIndex(
                name: "IX_Vehicles_DiagnosticProtocolId",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "DiagnosticProtocolId",
                table: "Vehicles");
        }
    }
}
