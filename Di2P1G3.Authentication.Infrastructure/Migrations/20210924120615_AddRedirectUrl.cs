using Microsoft.EntityFrameworkCore.Migrations;

namespace Di2P1G3.Authentication.Infrastructure.Migrations
{
    public partial class AddRedirectUrl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RedirectUrl",
                table: "ClientApplications",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RedirectUrl",
                table: "ClientApplications");
        }
    }
}
