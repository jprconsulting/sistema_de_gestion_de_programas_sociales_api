using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace beneficiariosdifapi.Migrations
{
    /// <inheritdoc />
    public partial class DescMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProgramasSociales_AreasAdscripcion_AreaAdscripcionId",
                table: "ProgramasSociales");

            migrationBuilder.DropForeignKey(
                name: "FK_Usuarios_Rols_RolId",
                table: "Usuarios");

            migrationBuilder.AlterColumn<int>(
                name: "RolId",
                table: "Usuarios",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AreaAdscripcionId",
                table: "ProgramasSociales",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Acronimo",
                table: "ProgramasSociales",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddForeignKey(
                name: "FK_ProgramasSociales_AreasAdscripcion_AreaAdscripcionId",
                table: "ProgramasSociales",
                column: "AreaAdscripcionId",
                principalTable: "AreasAdscripcion",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Usuarios_Rols_RolId",
                table: "Usuarios",
                column: "RolId",
                principalTable: "Rols",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProgramasSociales_AreasAdscripcion_AreaAdscripcionId",
                table: "ProgramasSociales");

            migrationBuilder.DropForeignKey(
                name: "FK_Usuarios_Rols_RolId",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "Acronimo",
                table: "ProgramasSociales");

            migrationBuilder.AlterColumn<int>(
                name: "RolId",
                table: "Usuarios",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "AreaAdscripcionId",
                table: "ProgramasSociales",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_ProgramasSociales_AreasAdscripcion_AreaAdscripcionId",
                table: "ProgramasSociales",
                column: "AreaAdscripcionId",
                principalTable: "AreasAdscripcion",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Usuarios_Rols_RolId",
                table: "Usuarios",
                column: "RolId",
                principalTable: "Rols",
                principalColumn: "Id");
        }
    }
}
