using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LYC.Migrations
{
    public partial class AddStaffBranchAssociationTableKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "StaffBranchAssociationId",
                schema: "mcs",
                table: "StaffBranchAssociation",
                type: "bigint",
                nullable: false,
                defaultValue: 0L)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StaffBranchAssociation",
                schema: "mcs",
                table: "StaffBranchAssociation",
                column: "StaffBranchAssociationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_StaffBranchAssociation",
                schema: "mcs",
                table: "StaffBranchAssociation");

            migrationBuilder.DropColumn(
                name: "StaffBranchAssociationId",
                schema: "mcs",
                table: "StaffBranchAssociation");
        }
    }
}
