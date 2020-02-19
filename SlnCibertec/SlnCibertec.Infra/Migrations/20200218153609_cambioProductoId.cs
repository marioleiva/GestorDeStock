using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SlnCibertec.Infra.Migrations
{
    public partial class cambioProductoId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Compras_Users_UserId",
                table: "Compras");

            migrationBuilder.DropColumn(
                name: "Fecha",
                table: "Productos");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Productos",
                newName: "ProductoId");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Compras",
                newName: "ProductoId");

            migrationBuilder.RenameIndex(
                name: "IX_Compras_UserId",
                table: "Compras",
                newName: "IX_Compras_ProductoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Compras_Productos_ProductoId",
                table: "Compras",
                column: "ProductoId",
                principalTable: "Productos",
                principalColumn: "ProductoId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Compras_Productos_ProductoId",
                table: "Compras");

            migrationBuilder.RenameColumn(
                name: "ProductoId",
                table: "Productos",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "ProductoId",
                table: "Compras",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Compras_ProductoId",
                table: "Compras",
                newName: "IX_Compras_UserId");

            migrationBuilder.AddColumn<DateTime>(
                name: "Fecha",
                table: "Productos",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddForeignKey(
                name: "FK_Compras_Users_UserId",
                table: "Compras",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
