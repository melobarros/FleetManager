using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FleetManager.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangedErrorDescriptions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "ErrorCodes",
                keyColumn: "Id",
                keyValue: 2,
                column: "Description",
                value: "High Oil Pressure");

            migrationBuilder.UpdateData(
                table: "ErrorCodes",
                keyColumn: "Id",
                keyValue: 4,
                column: "Description",
                value: "Brake Air Pressure High");

            migrationBuilder.UpdateData(
                table: "ErrorCodes",
                keyColumn: "Id",
                keyValue: 5,
                column: "Description",
                value: "Battery Voltage High");

            migrationBuilder.UpdateData(
                table: "ErrorCodes",
                keyColumn: "Id",
                keyValue: 9,
                column: "Description",
                value: "Brake Air Pressure High");

            migrationBuilder.UpdateData(
                table: "ErrorCodes",
                keyColumn: "Id",
                keyValue: 13,
                column: "Description",
                value: "High Brake Fluid");

            migrationBuilder.UpdateData(
                table: "ErrorCodes",
                keyColumn: "Id",
                keyValue: 14,
                column: "Description",
                value: "High Tire Pressure");

            migrationBuilder.UpdateData(
                table: "ErrorCodes",
                keyColumn: "Id",
                keyValue: 15,
                column: "Description",
                value: "Battery Voltage High");

            migrationBuilder.UpdateData(
                table: "ErrorCodes",
                keyColumn: "Id",
                keyValue: 17,
                column: "Description",
                value: "High Oil Pressure");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "ErrorCodes",
                keyColumn: "Id",
                keyValue: 2,
                column: "Description",
                value: "Low Oil Pressure");

            migrationBuilder.UpdateData(
                table: "ErrorCodes",
                keyColumn: "Id",
                keyValue: 4,
                column: "Description",
                value: "Brake Air Pressure Low");

            migrationBuilder.UpdateData(
                table: "ErrorCodes",
                keyColumn: "Id",
                keyValue: 5,
                column: "Description",
                value: "Battery Voltage Low");

            migrationBuilder.UpdateData(
                table: "ErrorCodes",
                keyColumn: "Id",
                keyValue: 9,
                column: "Description",
                value: "Brake Air Pressure Low");

            migrationBuilder.UpdateData(
                table: "ErrorCodes",
                keyColumn: "Id",
                keyValue: 13,
                column: "Description",
                value: "Low Brake Fluid");

            migrationBuilder.UpdateData(
                table: "ErrorCodes",
                keyColumn: "Id",
                keyValue: 14,
                column: "Description",
                value: "Low Tire Pressure");

            migrationBuilder.UpdateData(
                table: "ErrorCodes",
                keyColumn: "Id",
                keyValue: 15,
                column: "Description",
                value: "Battery Voltage Low");

            migrationBuilder.UpdateData(
                table: "ErrorCodes",
                keyColumn: "Id",
                keyValue: 17,
                column: "Description",
                value: "Low Oil Pressure");
        }
    }
}
