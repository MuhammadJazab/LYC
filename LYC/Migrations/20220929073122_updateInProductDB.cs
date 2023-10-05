using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LYC.Migrations
{
    public partial class updateInProductDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProductNo",
                schema: "mcs",
                table: "Product",
                newName: "ProductId");

            migrationBuilder.AlterColumn<string>(
                name: "ProductStatus",
                schema: "mcs",
                table: "Product",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<decimal>(
                name: "DisplayCost",
                schema: "mcs",
                table: "Product",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "ProductNumber",
                schema: "mcs",
                table: "Product",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "State",
                schema: "mcs",
                table: "Product",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DisplayCost",
                schema: "mcs",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "ProductNumber",
                schema: "mcs",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "State",
                schema: "mcs",
                table: "Product");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                schema: "mcs",
                table: "Product",
                newName: "ProductNo");

            migrationBuilder.AlterColumn<int>(
                name: "ProductStatus",
                schema: "mcs",
                table: "Product",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
