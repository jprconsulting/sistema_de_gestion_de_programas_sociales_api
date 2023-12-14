using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace beneficiariosdifapi.Migrations
{
    /// <inheritdoc />
    public partial class Indicadores : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Beneficiarios_Municipios_MunicipioId",
                table: "Beneficiarios");

            migrationBuilder.DropForeignKey(
                name: "FK_Beneficiarios_ProgramasSociales_ProgramaSocialId",
                table: "Beneficiarios");

            migrationBuilder.DropIndex(
                name: "IX_Beneficiarios_ProgramaSocialId",
                table: "Beneficiarios");

            migrationBuilder.AlterColumn<string>(
                name: "Descripcion",
                table: "Indicadores",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Color",
                table: "Indicadores",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<int>(
                name: "ProgramaSocialId",
                table: "Beneficiarios",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "MunicipioId",
                table: "Beneficiarios",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Beneficiarios_Municipios_MunicipioId",
                table: "Beneficiarios",
                column: "MunicipioId",
                principalTable: "Municipios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Beneficiarios_Municipios_MunicipioId",
                table: "Beneficiarios");

            migrationBuilder.AlterColumn<string>(
                name: "Descripcion",
                table: "Indicadores",
                type: "varchar(255)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Color",
                table: "Indicadores",
                type: "varchar(50)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<int>(
                name: "ProgramaSocialId",
                table: "Beneficiarios",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "MunicipioId",
                table: "Beneficiarios",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Beneficiarios_ProgramaSocialId",
                table: "Beneficiarios",
                column: "ProgramaSocialId");

            migrationBuilder.AddForeignKey(
                name: "FK_Beneficiarios_Municipios_MunicipioId",
                table: "Beneficiarios",
                column: "MunicipioId",
                principalTable: "Municipios",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Beneficiarios_ProgramasSociales_ProgramaSocialId",
                table: "Beneficiarios",
                column: "ProgramaSocialId",
                principalTable: "ProgramasSociales",
                principalColumn: "Id");
        }
    }
}
