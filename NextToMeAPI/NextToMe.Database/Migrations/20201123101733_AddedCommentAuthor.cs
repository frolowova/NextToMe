using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NextToMe.Database.Migrations
{
    public partial class AddedCommentAuthor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "MessageComments",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_MessageComments_UserId",
                table: "MessageComments",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_MessageComments_AspNetUsers_UserId",
                table: "MessageComments",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MessageComments_AspNetUsers_UserId",
                table: "MessageComments");

            migrationBuilder.DropIndex(
                name: "IX_MessageComments_UserId",
                table: "MessageComments");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "MessageComments");
        }
    }
}
