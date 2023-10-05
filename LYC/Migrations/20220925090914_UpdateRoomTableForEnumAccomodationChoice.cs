using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LYC.Migrations
{
    public partial class UpdateRoomTableForEnumAccomodationChoice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "AccomodationChoice",
                schema: "mcs",
                table: "Room",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MaxOccupents",
                schema: "mcs",
                table: "Room",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaxOccupents",
                schema: "mcs",
                table: "Room");

            migrationBuilder.AlterColumn<string>(
                name: "AccomodationChoice",
                schema: "mcs",
                table: "Room",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }
    }
}
