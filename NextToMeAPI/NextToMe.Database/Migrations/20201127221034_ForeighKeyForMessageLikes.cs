using Microsoft.EntityFrameworkCore.Migrations;

namespace NextToMe.Database.Migrations
{
    public partial class ForeighKeyForMessageLikes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserLikedMessages_Messages_UserId",
                table: "UserLikedMessages");

            migrationBuilder.CreateIndex(
                name: "IX_UserLikedMessages_MessageId",
                table: "UserLikedMessages",
                column: "MessageId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserLikedMessages_Messages_MessageId",
                table: "UserLikedMessages",
                column: "MessageId",
                principalTable: "Messages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserLikedMessages_Messages_MessageId",
                table: "UserLikedMessages");

            migrationBuilder.DropIndex(
                name: "IX_UserLikedMessages_MessageId",
                table: "UserLikedMessages");

            migrationBuilder.AddForeignKey(
                name: "FK_UserLikedMessages_Messages_UserId",
                table: "UserLikedMessages",
                column: "UserId",
                principalTable: "Messages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
