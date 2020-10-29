using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NextToMe.Database.Migrations
{
    public partial class DeleteTimeInMessage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DeleteAt",
                table: "Messages",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeleteAt",
                table: "Messages");
        }
    }
}
