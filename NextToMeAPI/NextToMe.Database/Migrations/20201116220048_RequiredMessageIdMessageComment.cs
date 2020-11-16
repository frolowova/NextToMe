using Microsoft.EntityFrameworkCore.Migrations;

namespace NextToMe.Database.Migrations
{
    public partial class RequiredMessageIdMessageComment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MessageComment_Messages_MessageId",
                table: "MessageComment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MessageComment",
                table: "MessageComment");

            migrationBuilder.RenameTable(
                name: "MessageComment",
                newName: "MessageComments");

            migrationBuilder.RenameIndex(
                name: "IX_MessageComment_MessageId",
                table: "MessageComments",
                newName: "IX_MessageComments_MessageId");

            migrationBuilder.AlterColumn<string>(
                name: "MessageId",
                table: "MessageComments",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255) CHARACTER SET utf8mb4",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_MessageComments",
                table: "MessageComments",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MessageComments_Messages_MessageId",
                table: "MessageComments",
                column: "MessageId",
                principalTable: "Messages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MessageComments_Messages_MessageId",
                table: "MessageComments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MessageComments",
                table: "MessageComments");

            migrationBuilder.RenameTable(
                name: "MessageComments",
                newName: "MessageComment");

            migrationBuilder.RenameIndex(
                name: "IX_MessageComments_MessageId",
                table: "MessageComment",
                newName: "IX_MessageComment_MessageId");

            migrationBuilder.AlterColumn<string>(
                name: "MessageId",
                table: "MessageComment",
                type: "varchar(255) CHARACTER SET utf8mb4",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddPrimaryKey(
                name: "PK_MessageComment",
                table: "MessageComment",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MessageComment_Messages_MessageId",
                table: "MessageComment",
                column: "MessageId",
                principalTable: "Messages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
