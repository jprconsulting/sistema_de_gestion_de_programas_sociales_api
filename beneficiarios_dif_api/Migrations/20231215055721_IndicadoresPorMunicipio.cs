using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace beneficiariosdifapi.Migrations
{
    /// <inheritdoc />
    public partial class IndicadoresPorMunicipio : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IndicadorId",
                table: "Indicadores",
                newName: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Indicadores",
                newName: "IndicadorId");
        }
    }
}
