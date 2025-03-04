using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace poc_estados_api.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SolicitudIdSolicitud",
                table: "Estados",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Estados_SolicitudIdSolicitud",
                table: "Estados",
                column: "SolicitudIdSolicitud");

            migrationBuilder.AddForeignKey(
                name: "FK_Estados_Solicitudes_SolicitudIdSolicitud",
                table: "Estados",
                column: "SolicitudIdSolicitud",
                principalTable: "Solicitudes",
                principalColumn: "IdSolicitud");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Estados_Solicitudes_SolicitudIdSolicitud",
                table: "Estados");

            migrationBuilder.DropIndex(
                name: "IX_Estados_SolicitudIdSolicitud",
                table: "Estados");

            migrationBuilder.DropColumn(
                name: "SolicitudIdSolicitud",
                table: "Estados");
        }
    }
}
