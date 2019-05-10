using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HRS.Migrations
{
    public partial class m2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserInfos_Users_UserId",
                table: "UserInfos");

            migrationBuilder.DropIndex(
                name: "IX_UserInfos_UserId",
                table: "UserInfos");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "UserInfos");

            migrationBuilder.AddColumn<Guid>(
                name: "UserInfoId",
                table: "Users",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserInfoId",
                table: "Users",
                column: "UserInfoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_UserInfos_UserInfoId",
                table: "Users",
                column: "UserInfoId",
                principalTable: "UserInfos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_UserInfos_UserInfoId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_UserInfoId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "UserInfoId",
                table: "Users");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "UserInfos",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserInfos_UserId",
                table: "UserInfos",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserInfos_Users_UserId",
                table: "UserInfos",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
