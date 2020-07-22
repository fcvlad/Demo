using Microsoft.EntityFrameworkCore.Migrations;

namespace Demo.Migrations
{
    public partial class usersv2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "IdNo",
                table: "AspNetUsers",
                maxLength: 18,
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldMaxLength: 18);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "IdNo",
                table: "AspNetUsers",
                type: "int",
                maxLength: 18,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 18,
                oldNullable: true);
        }
    }
}
