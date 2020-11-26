using Microsoft.EntityFrameworkCore.Migrations;

namespace NextToMe.Database.Migrations
{
    public partial class MessageViews : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "Views",
                table: "Messages",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Views",
                table: "Messages");
        }
    }
}
