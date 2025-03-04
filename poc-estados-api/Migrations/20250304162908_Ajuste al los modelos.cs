using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace poc_estados_api.Migrations
{
    /// <inheritdoc />
    public partial class Ajusteallosmodelos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Estados_Solicitudes_SolicitudIdSolicitud",
                table: "Estados");

            migrationBuilder.RenameColumn(
                name: "SolicitudIdSolicitud",
                table: "Estados",
                newName: "IdSolicitud");

            migrationBuilder.RenameIndex(
                name: "IX_Estados_SolicitudIdSolicitud",
                table: "Estados",
                newName: "IX_Estados_IdSolicitud");

            migrationBuilder.AddForeignKey(
                name: "FK_Estados_Solicitudes_IdSolicitud",
                table: "Estados",
                column: "IdSolicitud",
                principalTable: "Solicitudes",
                principalColumn: "IdSolicitud");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Estados_Solicitudes_IdSolicitud",
                table: "Estados");

            migrationBuilder.RenameColumn(
                name: "IdSolicitud",
                table: "Estados",
                newName: "SolicitudIdSolicitud");

            migrationBuilder.RenameIndex(
                name: "IX_Estados_IdSolicitud",
                table: "Estados",
                newName: "IX_Estados_SolicitudIdSolicitud");

            migrationBuilder.AddForeignKey(
                name: "FK_Estados_Solicitudes_SolicitudIdSolicitud",
                table: "Estados",
                column: "SolicitudIdSolicitud",
                principalTable: "Solicitudes",
                principalColumn: "IdSolicitud");
        }
    }
}
