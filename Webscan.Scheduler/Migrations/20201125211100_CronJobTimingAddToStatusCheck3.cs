using Microsoft.EntityFrameworkCore.Migrations;

namespace Webscan.Scheduler.Migrations
{
    public partial class CronJobTimingAddToStatusCheck3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "QueryTimeInSeconds",
                table: "StatusChecks",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "QueryTimeInSeconds",
                table: "StatusChecks");
        }
    }
}
