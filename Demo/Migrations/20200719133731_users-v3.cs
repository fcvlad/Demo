using Microsoft.EntityFrameworkCore.Migrations;

namespace Demo.Migrations
{
    public partial class usersv3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdNo",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "AspNetUsers",
                maxLength: 18,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "IdNo",
                table: "AspNetUsers",
                type: "nvarchar(18)",
                maxLength: 18,
                nullable: true);
        }
    }
}
