using Microsoft.EntityFrameworkCore.Migrations;

namespace DiscordRipoff.Migrations
{
    public partial class RemoveRoomXMessage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RoomMessages_Rooms_RoomId",
                table: "RoomMessages");

            migrationBuilder.DropIndex(
                name: "IX_RoomMessages_RoomId",
                table: "RoomMessages");

            migrationBuilder.DropColumn(
                name: "RoomId",
                table: "RoomMessages");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RoomId",
                table: "RoomMessages",
                type: "INTEGER",
                nullable: true);

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
        }
    }
}
