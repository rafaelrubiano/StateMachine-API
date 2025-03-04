using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace poc_estados_api.Migrations
{
    /// <inheritdoc />
    public partial class Ajustesalmodelo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IdAccion",
                table: "AccionesEstado",
                newName: "IdAccionEstado");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IdAccionEstado",
                table: "AccionesEstado",
                newName: "IdAccion");
        }
    }
}
