using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Webscan.Scheduler.Migrations
{
    public partial class AddingLastNotifiedToStatusCheck : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "LastNotified",
                table: "StatusChecks",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastNotified",
                table: "StatusChecks");
        }
    }
}
