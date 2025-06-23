using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FleetManager.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ImprovedSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ErrorCodes",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Sensors",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Sensors",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Sensors",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.AddColumn<int>(
                name: "ErrorCodeId",
                table: "Sensors",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "ErrorCodes",
                columns: new[] { "Id", "Code", "Description", "ProtocolId" },
                values: new object[,]
                {
                    { 17, "C006", "Low Oil Pressure", 3 },
                    { 18, "B006", "Engine Overheating", 2 },
                    { 19, "B007", "Fuel System Leak", 2 }
                });

            migrationBuilder.UpdateData(
                table: "Sensors",
                keyColumn: "Id",
                keyValue: 1,
                column: "ErrorCodeId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Sensors",
                keyColumn: "Id",
                keyValue: 3,
                column: "ErrorCodeId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Sensors",
                keyColumn: "Id",
                keyValue: 4,
                column: "ErrorCodeId",
                value: 3);

            migrationBuilder.UpdateData(
                table: "Sensors",
                keyColumn: "Id",
                keyValue: 5,
                column: "ErrorCodeId",
                value: 5);

            migrationBuilder.UpdateData(
                table: "Sensors",
                keyColumn: "Id",
                keyValue: 6,
                column: "ErrorCodeId",
                value: 4);

            migrationBuilder.UpdateData(
                table: "Sensors",
                keyColumn: "Id",
                keyValue: 7,
                column: "ErrorCodeId",
                value: 6);

            migrationBuilder.UpdateData(
                table: "Sensors",
                keyColumn: "Id",
                keyValue: 8,
                column: "ErrorCodeId",
                value: 18);

            migrationBuilder.UpdateData(
                table: "Sensors",
                keyColumn: "Id",
                keyValue: 10,
                column: "ErrorCodeId",
                value: 19);

            migrationBuilder.UpdateData(
                table: "Sensors",
                keyColumn: "Id",
                keyValue: 11,
                column: "ErrorCodeId",
                value: 7);

            migrationBuilder.UpdateData(
                table: "Sensors",
                keyColumn: "Id",
                keyValue: 12,
                column: "ErrorCodeId",
                value: 8);

            migrationBuilder.UpdateData(
                table: "Sensors",
                keyColumn: "Id",
                keyValue: 13,
                column: "ErrorCodeId",
                value: 9);

            migrationBuilder.UpdateData(
                table: "Sensors",
                keyColumn: "Id",
                keyValue: 14,
                column: "ErrorCodeId",
                value: 10);

            migrationBuilder.UpdateData(
                table: "Sensors",
                keyColumn: "Id",
                keyValue: 15,
                column: "ErrorCodeId",
                value: 12);

            migrationBuilder.UpdateData(
                table: "Sensors",
                keyColumn: "Id",
                keyValue: 17,
                column: "ErrorCodeId",
                value: 17);

            migrationBuilder.UpdateData(
                table: "Sensors",
                keyColumn: "Id",
                keyValue: 18,
                column: "ErrorCodeId",
                value: 16);

            migrationBuilder.UpdateData(
                table: "Sensors",
                keyColumn: "Id",
                keyValue: 19,
                column: "ErrorCodeId",
                value: 15);

            migrationBuilder.UpdateData(
                table: "Sensors",
                keyColumn: "Id",
                keyValue: 20,
                column: "ErrorCodeId",
                value: 13);

            migrationBuilder.UpdateData(
                table: "Sensors",
                keyColumn: "Id",
                keyValue: 21,
                column: "ErrorCodeId",
                value: 14);

            migrationBuilder.CreateIndex(
                name: "IX_Sensors_ErrorCodeId",
                table: "Sensors",
                column: "ErrorCodeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Sensors_ErrorCodes_ErrorCodeId",
                table: "Sensors",
                column: "ErrorCodeId",
                principalTable: "ErrorCodes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sensors_ErrorCodes_ErrorCodeId",
                table: "Sensors");

            migrationBuilder.DropIndex(
                name: "IX_Sensors_ErrorCodeId",
                table: "Sensors");

            migrationBuilder.DeleteData(
                table: "ErrorCodes",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "ErrorCodes",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "ErrorCodes",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DropColumn(
                name: "ErrorCodeId",
                table: "Sensors");

            migrationBuilder.InsertData(
                table: "ErrorCodes",
                columns: new[] { "Id", "Code", "Description", "ProtocolId" },
                values: new object[] { 11, "B005", "Communication Bus Error", 2 });

            migrationBuilder.InsertData(
                table: "Sensors",
                columns: new[] { "Id", "MaxThreshold", "MinThreshold", "Name", "ProtocolId", "Unit" },
                values: new object[,]
                {
                    { 2, 4500, 0, "Engine Speed (RPM)", 1, "RPM" },
                    { 9, 4500, 0, "Engine Speed (RPM)", 2, "RPM" },
                    { 16, 6000, 0, "Engine Speed (RPM)", 3, "RPM" }
                });
        }
    }
}
