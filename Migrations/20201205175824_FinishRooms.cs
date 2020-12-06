using Microsoft.EntityFrameworkCore.Migrations;

namespace DiscordRipoff.Migrations
{
    public partial class FinishRooms : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Users",
                newName: "Id");

            migrationBuilder.CreateTable(
                name: "RoomMessage",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Content = table.Column<string>(type: "TEXT", nullable: false),
                    RoomUserId = table.Column<int>(type: "INTEGER", nullable: false),
                    RoomId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomMessage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoomMessage_Rooms_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Rooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RoomMessage_RoomUsers_RoomUserId",
                        column: x => x.RoomUserId,
                        principalTable: "RoomUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RoomMessage_RoomId",
                table: "RoomMessage",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_RoomMessage_RoomUserId",
                table: "RoomMessage",
                column: "RoomUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RoomMessage");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Users",
                newName: "UserId");
        }
    }
}
