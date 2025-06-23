using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FleetManager.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedFK : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vehicles_DiagnosticProtocols_DiagnosticProtocolId",
                table: "Vehicles");

            migrationBuilder.RenameColumn(
                name: "DiagnosticProtocolId",
                table: "Vehicles",
                newName: "ProtocolId");

            migrationBuilder.RenameIndex(
                name: "IX_Vehicles_DiagnosticProtocolId",
                table: "Vehicles",
                newName: "IX_Vehicles_ProtocolId");

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicles_DiagnosticProtocols_ProtocolId",
                table: "Vehicles",
                column: "ProtocolId",
                principalTable: "DiagnosticProtocols",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vehicles_DiagnosticProtocols_ProtocolId",
                table: "Vehicles");

            migrationBuilder.RenameColumn(
                name: "ProtocolId",
                table: "Vehicles",
                newName: "DiagnosticProtocolId");

            migrationBuilder.RenameIndex(
                name: "IX_Vehicles_ProtocolId",
                table: "Vehicles",
                newName: "IX_Vehicles_DiagnosticProtocolId");

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicles_DiagnosticProtocols_DiagnosticProtocolId",
                table: "Vehicles",
                column: "DiagnosticProtocolId",
                principalTable: "DiagnosticProtocols",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
