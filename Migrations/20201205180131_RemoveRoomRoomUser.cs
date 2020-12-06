using Microsoft.EntityFrameworkCore.Migrations;

namespace DiscordRipoff.Migrations
{
    public partial class RemoveRoomRoomUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RoomMessage_Rooms_RoomId",
                table: "RoomMessage");

            migrationBuilder.DropIndex(
                name: "IX_RoomMessage_RoomId",
                table: "RoomMessage");

            migrationBuilder.DropColumn(
                name: "RoomId",
                table: "RoomMessage");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RoomId",
                table: "RoomMessage",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_RoomMessage_RoomId",
                table: "RoomMessage",
                column: "RoomId");

            migrationBuilder.AddForeignKey(
                name: "FK_RoomMessage_Rooms_RoomId",
                table: "RoomMessage",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
