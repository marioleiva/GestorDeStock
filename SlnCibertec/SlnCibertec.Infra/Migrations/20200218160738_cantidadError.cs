using Microsoft.EntityFrameworkCore.Migrations;

namespace SlnCibertec.Infra.Migrations
{
    public partial class cantidadError : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Cantindad",
                table: "Productos",
                newName: "Cantidad");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Cantidad",
                table: "Productos",
                newName: "Cantindad");
        }
    }
}
