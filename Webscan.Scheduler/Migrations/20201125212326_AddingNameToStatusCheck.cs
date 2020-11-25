using Microsoft.EntityFrameworkCore.Migrations;

namespace Webscan.Scheduler.Migrations
{
    public partial class AddingNameToStatusCheck : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "StatusChecks",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "StatusChecks");
        }
    }
}
