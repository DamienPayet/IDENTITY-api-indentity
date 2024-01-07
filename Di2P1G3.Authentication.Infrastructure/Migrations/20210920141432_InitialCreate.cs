using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Di2P1G3.Authentication.Infrastructure.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ClientApplications",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientApplications", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Firstname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    password_salt = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    password_hash = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AccessTokens",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdUser = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId1 = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccessTokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccessTokens_Users_IdUser",
                        column: x => x.IdUser,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AccessTokens_Users_UserId1",
                        column: x => x.UserId1,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserApplications",
                columns: table => new
                {
                    IdUser = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdClientApplication = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ApplicationId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserApplications", x => new { x.IdUser, x.IdClientApplication });
                    table.ForeignKey(
                        name: "FK_UserApplications_ClientApplications_ApplicationId",
                        column: x => x.ApplicationId,
                        principalTable: "ClientApplications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                    table.ForeignKey(
                        name: "FK_UserApplications_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RefreshTokens",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdAccessToken = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AccessTokenId1 = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshTokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RefreshTokens_AccessTokens_AccessTokenId1",
                        column: x => x.AccessTokenId1,
                        principalTable: "AccessTokens",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RefreshTokens_AccessTokens_IdAccessToken",
                        column: x => x.IdAccessToken,
                        principalTable: "AccessTokens",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccessTokens_IdUser",
                table: "AccessTokens",
                column: "IdUser");

            migrationBuilder.CreateIndex(
                name: "IX_AccessTokens_UserId1",
                table: "AccessTokens",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_AccessTokenId1",
                table: "RefreshTokens",
                column: "AccessTokenId1");

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_IdAccessToken",
                table: "RefreshTokens",
                column: "IdAccessToken");

            migrationBuilder.CreateIndex(
                name: "IX_UserApplications_ApplicationId",
                table: "UserApplications",
                column: "ApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_UserApplications_IdClientApplication",
                table: "UserApplications",
                column: "IdClientApplication");

            migrationBuilder.CreateIndex(
                name: "IX_UserApplications_UserId",
                table: "UserApplications",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RefreshTokens");

            migrationBuilder.DropTable(
                name: "UserApplications");

            migrationBuilder.DropTable(
                name: "AccessTokens");

            migrationBuilder.DropTable(
                name: "ClientApplications");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
