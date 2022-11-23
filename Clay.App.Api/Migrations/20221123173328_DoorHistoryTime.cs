using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Clay.App.Api.Migrations
{
    public partial class DoorHistoryTime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DoorAccessedTime",
                table: "DoorHistory",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DoorAccessedTime",
                table: "DoorHistory");
        }
    }
}
