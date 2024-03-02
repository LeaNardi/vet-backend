using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace vet_backend.Migrations
{
    public partial class vet74 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Veterinario",
                table: "Historias",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Veterinario",
                table: "Historias");
        }
    }
}
