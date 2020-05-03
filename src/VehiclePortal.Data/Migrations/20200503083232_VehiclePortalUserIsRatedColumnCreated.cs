using Microsoft.EntityFrameworkCore.Migrations;

namespace VehiclePortal.Data.Migrations
{
    public partial class VehiclePortalUserIsRatedColumnCreated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsRated",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsRated",
                table: "AspNetUsers");
        }
    }
}
