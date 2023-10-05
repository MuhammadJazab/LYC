using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LYC.Migrations
{
    public partial class UpdatePromotionsAndPackages : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HairWash",
                schema: "mcs",
                table: "Package");

            migrationBuilder.DropColumn(
                name: "PackagePoster",
                schema: "mcs",
                table: "Package");

            migrationBuilder.DropColumn(
                name: "PostnatalRitual",
                schema: "mcs",
                table: "Package");

            migrationBuilder.DropColumn(
                name: "SecondBabyPerCost",
                schema: "mcs",
                table: "Package");

            migrationBuilder.AddColumn<long>(
                name: "PackageId",
                schema: "mcs",
                table: "Service",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PackageNumber",
                schema: "mcs",
                table: "Package",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PackageId",
                schema: "mcs",
                table: "Service");

            migrationBuilder.DropColumn(
                name: "PackageNumber",
                schema: "mcs",
                table: "Package");

            migrationBuilder.AddColumn<bool>(
                name: "HairWash",
                schema: "mcs",
                table: "Package",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<byte[]>(
                name: "PackagePoster",
                schema: "mcs",
                table: "Package",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "PostnatalRitual",
                schema: "mcs",
                table: "Package",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<long>(
                name: "SecondBabyPerCost",
                schema: "mcs",
                table: "Package",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }
    }
}
