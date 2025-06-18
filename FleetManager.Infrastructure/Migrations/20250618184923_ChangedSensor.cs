using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FleetManager.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangedSensor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MaxThreshold",
                table: "Sensors",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MinThreshold",
                table: "Sensors",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Sensors",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "MaxThreshold", "MinThreshold" },
                values: new object[] { 95, 0 });

            migrationBuilder.UpdateData(
                table: "Sensors",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "MaxThreshold", "MinThreshold" },
                values: new object[] { 4500, 0 });

            migrationBuilder.UpdateData(
                table: "Sensors",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "MaxThreshold", "MinThreshold" },
                values: new object[] { 100, 2 });

            migrationBuilder.UpdateData(
                table: "Sensors",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "MaxThreshold", "MinThreshold" },
                values: new object[] { 100, 10 });

            migrationBuilder.UpdateData(
                table: "Sensors",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "MaxThreshold", "MinThreshold" },
                values: new object[] { 24, 12 });

            migrationBuilder.UpdateData(
                table: "Sensors",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "MaxThreshold", "MinThreshold" },
                values: new object[] { 10, 5 });

            migrationBuilder.UpdateData(
                table: "Sensors",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "MaxThreshold", "MinThreshold" },
                values: new object[] { 100, 0 });

            migrationBuilder.UpdateData(
                table: "Sensors",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "MaxThreshold", "MinThreshold" },
                values: new object[] { 95, 0 });

            migrationBuilder.UpdateData(
                table: "Sensors",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "MaxThreshold", "MinThreshold" },
                values: new object[] { 4500, 0 });

            migrationBuilder.UpdateData(
                table: "Sensors",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "MaxThreshold", "MinThreshold" },
                values: new object[] { 100, 10 });

            migrationBuilder.UpdateData(
                table: "Sensors",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "MaxThreshold", "MinThreshold" },
                values: new object[] { 1, 0 });

            migrationBuilder.UpdateData(
                table: "Sensors",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "MaxThreshold", "MinThreshold" },
                values: new object[] { 1, 0 });

            migrationBuilder.UpdateData(
                table: "Sensors",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "MaxThreshold", "MinThreshold" },
                values: new object[] { 10, 5 });

            migrationBuilder.UpdateData(
                table: "Sensors",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "MaxThreshold", "MinThreshold" },
                values: new object[] { 100, 0 });

            migrationBuilder.UpdateData(
                table: "Sensors",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "MaxThreshold", "MinThreshold" },
                values: new object[] { 95, 0 });

            migrationBuilder.UpdateData(
                table: "Sensors",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "MaxThreshold", "MinThreshold" },
                values: new object[] { 6000, 0 });

            migrationBuilder.UpdateData(
                table: "Sensors",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "MaxThreshold", "MinThreshold" },
                values: new object[] { 100, 2 });

            migrationBuilder.UpdateData(
                table: "Sensors",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "MaxThreshold", "MinThreshold" },
                values: new object[] { 100, 10 });

            migrationBuilder.UpdateData(
                table: "Sensors",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "MaxThreshold", "MinThreshold" },
                values: new object[] { 24, 12 });

            migrationBuilder.UpdateData(
                table: "Sensors",
                keyColumn: "Id",
                keyValue: 20,
                columns: new[] { "MaxThreshold", "MinThreshold" },
                values: new object[] { 100, 20 });

            migrationBuilder.UpdateData(
                table: "Sensors",
                keyColumn: "Id",
                keyValue: 21,
                columns: new[] { "MaxThreshold", "MinThreshold" },
                values: new object[] { 35, 30 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaxThreshold",
                table: "Sensors");

            migrationBuilder.DropColumn(
                name: "MinThreshold",
                table: "Sensors");
        }
    }
}
