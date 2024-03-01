using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace vet_backend.Migrations
{
    public partial class vet72 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Mascotas_Color_ColorId",
                table: "Mascotas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Color",
                table: "Color");

            migrationBuilder.RenameTable(
                name: "Color",
                newName: "Colores");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Colores",
                table: "Colores",
                column: "ColorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Mascotas_Colores_ColorId",
                table: "Mascotas",
                column: "ColorId",
                principalTable: "Colores",
                principalColumn: "ColorId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Mascotas_Colores_ColorId",
                table: "Mascotas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Colores",
                table: "Colores");

            migrationBuilder.RenameTable(
                name: "Colores",
                newName: "Color");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Color",
                table: "Color",
                column: "ColorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Mascotas_Color_ColorId",
                table: "Mascotas",
                column: "ColorId",
                principalTable: "Color",
                principalColumn: "ColorId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
