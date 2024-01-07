using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Di2P1G3.Authentication.Infrastructure.Migrations
{
    public partial class UpdateClientApplications : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserApplications");

            migrationBuilder.CreateTable(
                name: "ClientApplicationUser",
                columns: table => new
                {
                    ApplicationsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UsersId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientApplicationUser", x => new { x.ApplicationsId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_ClientApplicationUser_ClientApplications_ApplicationsId",
                        column: x => x.ApplicationsId,
                        principalTable: "ClientApplications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClientApplicationUser_Users_UsersId",
                        column: x => x.UsersId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClientApplicationUser_UsersId",
                table: "ClientApplicationUser",
                column: "UsersId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClientApplicationUser");

            migrationBuilder.CreateTable(
                name: "UserApplications",
                columns: table => new
                {
                    IdUser = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdClientApplication = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserApplications", x => new { x.IdUser, x.IdClientApplication });
                    table.ForeignKey(
                        name: "FK_UserApplications_ClientApplications_IdUser",
                        column: x => x.IdUser,
                        principalTable: "ClientApplications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserApplications_Users_IdClientApplication",
                        column: x => x.IdClientApplication,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserApplications_IdClientApplication",
                table: "UserApplications",
                column: "IdClientApplication");
        }
    }
}
