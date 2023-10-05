using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LYC.Migrations
{
    public partial class UpdateDatabaseConnection : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Permissiondescription",
                schema: "mcs",
                table: "Permission");

            migrationBuilder.DropColumn(
                name: "RolePermission",
                table: "AspNetRoles");

            migrationBuilder.AddColumn<bool>(
                name: "AddPermission",
                schema: "mcs",
                table: "Permission",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "DeletePermission",
                schema: "mcs",
                table: "Permission",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "EditPermission",
                schema: "mcs",
                table: "Permission",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "MobileViewPermission",
                schema: "mcs",
                table: "Permission",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<long>(
                name: "RoleId",
                schema: "mcs",
                table: "Permission",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<bool>(
                name: "ViewPermission",
                schema: "mcs",
                table: "Permission",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AddPermission",
                schema: "mcs",
                table: "Permission");

            migrationBuilder.DropColumn(
                name: "DeletePermission",
                schema: "mcs",
                table: "Permission");

            migrationBuilder.DropColumn(
                name: "EditPermission",
                schema: "mcs",
                table: "Permission");

            migrationBuilder.DropColumn(
                name: "MobileViewPermission",
                schema: "mcs",
                table: "Permission");

            migrationBuilder.DropColumn(
                name: "RoleId",
                schema: "mcs",
                table: "Permission");

            migrationBuilder.DropColumn(
                name: "ViewPermission",
                schema: "mcs",
                table: "Permission");

            migrationBuilder.AddColumn<string>(
                name: "Permissiondescription",
                schema: "mcs",
                table: "Permission",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "RolePermission",
                table: "AspNetRoles",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }
    }
}
