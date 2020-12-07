using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DiscordRipoff.Migrations
{
    public partial class AddTimeRoomMessage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RoomMessage_RoomUsers_RoomUserId",
                table: "RoomMessage");

            migrationBuilder.DropIndex(
                name: "IX_RoomUsers_RoomId",
                table: "RoomUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RoomMessage",
                table: "RoomMessage");

            migrationBuilder.RenameTable(
                name: "RoomMessage",
                newName: "RoomMessages");

            migrationBuilder.RenameIndex(
                name: "IX_RoomMessage_RoomUserId",
                table: "RoomMessages",
                newName: "IX_RoomMessages_RoomUserId");

            migrationBuilder.AddColumn<int>(
                name: "RoomId",
                table: "RoomMessages",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "TimeCreated",
                table: "RoomMessages",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddUniqueConstraint(
                name: "AK_RoomUsers_RoomId_UserId",
                table: "RoomUsers",
                columns: new[] { "RoomId", "UserId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_RoomMessages",
                table: "RoomMessages",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_RoomMessages_RoomId",
                table: "RoomMessages",
                column: "RoomId");

            migrationBuilder.AddForeignKey(
                name: "FK_RoomMessages_Rooms_RoomId",
                table: "RoomMessages",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RoomMessages_RoomUsers_RoomUserId",
                table: "RoomMessages",
                column: "RoomUserId",
                principalTable: "RoomUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RoomMessages_Rooms_RoomId",
                table: "RoomMessages");

            migrationBuilder.DropForeignKey(
                name: "FK_RoomMessages_RoomUsers_RoomUserId",
                table: "RoomMessages");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_RoomUsers_RoomId_UserId",
                table: "RoomUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RoomMessages",
                table: "RoomMessages");

            migrationBuilder.DropIndex(
                name: "IX_RoomMessages_RoomId",
                table: "RoomMessages");

            migrationBuilder.DropColumn(
                name: "RoomId",
                table: "RoomMessages");

            migrationBuilder.DropColumn(
                name: "TimeCreated",
                table: "RoomMessages");

            migrationBuilder.RenameTable(
                name: "RoomMessages",
                newName: "RoomMessage");

            migrationBuilder.RenameIndex(
                name: "IX_RoomMessages_RoomUserId",
                table: "RoomMessage",
                newName: "IX_RoomMessage_RoomUserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RoomMessage",
                table: "RoomMessage",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_RoomUsers_RoomId",
                table: "RoomUsers",
                column: "RoomId");

            migrationBuilder.AddForeignKey(
                name: "FK_RoomMessage_RoomUsers_RoomUserId",
                table: "RoomMessage",
                column: "RoomUserId",
                principalTable: "RoomUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
