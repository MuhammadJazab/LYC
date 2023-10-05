using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LYC.Migrations
{
    public partial class UpdatesInConfigurations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ServiceStatus",
                schema: "mcs",
                table: "Service",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ServiceStatus",
                schema: "mcs",
                table: "Service");
        }
    }
}
