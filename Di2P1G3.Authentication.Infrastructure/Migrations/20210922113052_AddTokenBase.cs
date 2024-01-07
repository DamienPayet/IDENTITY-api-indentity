using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Di2P1G3.Authentication.Infrastructure.Migrations
{
    public partial class AddTokenBase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccessTokens_Users_UserId1",
                table: "AccessTokens");

            migrationBuilder.DropForeignKey(
                name: "FK_RefreshTokens_AccessTokens_AccessTokenId1",
                table: "RefreshTokens");

            migrationBuilder.DropForeignKey(
                name: "FK_UserApplications_ClientApplications_ApplicationId",
                table: "UserApplications");

            migrationBuilder.DropForeignKey(
                name: "FK_UserApplications_Users_UserId",
                table: "UserApplications");

            migrationBuilder.DropIndex(
                name: "IX_UserApplications_ApplicationId",
                table: "UserApplications");

            migrationBuilder.DropIndex(
                name: "IX_UserApplications_UserId",
                table: "UserApplications");

            migrationBuilder.DropIndex(
                name: "IX_RefreshTokens_AccessTokenId1",
                table: "RefreshTokens");

            migrationBuilder.DropIndex(
                name: "IX_AccessTokens_UserId1",
                table: "AccessTokens");

            migrationBuilder.DropColumn(
                name: "ApplicationId",
                table: "UserApplications");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "UserApplications");

            migrationBuilder.DropColumn(
                name: "AccessTokenId1",
                table: "RefreshTokens");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "AccessTokens");

            migrationBuilder.AlterColumn<string>(
                name: "Username",
                table: "Users",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Token",
                table: "RefreshTokens",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Token",
                table: "AccessTokens",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Username",
                table: "Users",
                column: "Username",
                unique: true,
                filter: "[Username] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Users_Username",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Token",
                table: "RefreshTokens");

            migrationBuilder.DropColumn(
                name: "Token",
                table: "AccessTokens");

            migrationBuilder.AlterColumn<string>(
                name: "Username",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ApplicationId",
                table: "UserApplications",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "UserApplications",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "AccessTokenId1",
                table: "RefreshTokens",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UserId1",
                table: "AccessTokens",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserApplications_ApplicationId",
                table: "UserApplications",
                column: "ApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_UserApplications_UserId",
                table: "UserApplications",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_AccessTokenId1",
                table: "RefreshTokens",
                column: "AccessTokenId1");

            migrationBuilder.CreateIndex(
                name: "IX_AccessTokens_UserId1",
                table: "AccessTokens",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_AccessTokens_Users_UserId1",
                table: "AccessTokens",
                column: "UserId1",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RefreshTokens_AccessTokens_AccessTokenId1",
                table: "RefreshTokens",
                column: "AccessTokenId1",
                principalTable: "AccessTokens",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserApplications_ClientApplications_ApplicationId",
                table: "UserApplications",
                column: "ApplicationId",
                principalTable: "ClientApplications",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserApplications_Users_UserId",
                table: "UserApplications",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
