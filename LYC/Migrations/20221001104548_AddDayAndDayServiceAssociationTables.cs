using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LYC.Migrations
{
    public partial class AddDayAndDayServiceAssociationTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ServiceDuration",
                schema: "mcs",
                table: "Service");

            migrationBuilder.DropColumn(
                name: "ServiceImage",
                schema: "mcs",
                table: "Service");

            migrationBuilder.RenameColumn(
                name: "ServiceNo",
                schema: "mcs",
                table: "Service",
                newName: "MaxOccupants");

            migrationBuilder.AddColumn<DateTime>(
                name: "EndingDateTime",
                schema: "mcs",
                table: "Service",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "StartingDateTime",
                schema: "mcs",
                table: "Service",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "Day",
                schema: "mcs",
                columns: table => new
                {
                    DayId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Day", x => x.DayId);
                });

            migrationBuilder.CreateTable(
                name: "ServiceDayAssociation",
                schema: "mcs",
                columns: table => new
                {
                    ServiceDayAssociationId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DayId = table.Column<int>(type: "int", nullable: false),
                    ServiceId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceDayAssociation", x => x.ServiceDayAssociationId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Day",
                schema: "mcs");

            migrationBuilder.DropTable(
                name: "ServiceDayAssociation",
                schema: "mcs");

            migrationBuilder.DropColumn(
                name: "EndingDateTime",
                schema: "mcs",
                table: "Service");

            migrationBuilder.DropColumn(
                name: "StartingDateTime",
                schema: "mcs",
                table: "Service");

            migrationBuilder.RenameColumn(
                name: "MaxOccupants",
                schema: "mcs",
                table: "Service",
                newName: "ServiceNo");

            migrationBuilder.AddColumn<int>(
                name: "ServiceDuration",
                schema: "mcs",
                table: "Service",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<byte[]>(
                name: "ServiceImage",
                schema: "mcs",
                table: "Service",
                type: "varbinary(max)",
                nullable: true);
        }
    }
}
