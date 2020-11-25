using Microsoft.EntityFrameworkCore.Migrations;

namespace Webscan.Scheduler.Migrations
{
    public partial class CronJobTimingAddToStatusCheck2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CronJobTiming",
                table: "StatusChecks",
                newName: "CronExpression");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CronExpression",
                table: "StatusChecks",
                newName: "CronJobTiming");
        }
    }
}
