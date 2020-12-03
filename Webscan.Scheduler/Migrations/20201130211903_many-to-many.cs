using Microsoft.EntityFrameworkCore.Migrations;

namespace Webscan.Scheduler.Migrations
{
    public partial class manytomany : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StatusChecks_Users_UserId",
                table: "StatusChecks");

            migrationBuilder.DropIndex(
                name: "IX_StatusChecks_UserId",
                table: "StatusChecks");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "StatusChecks");

            migrationBuilder.CreateTable(
                name: "StatusCheckUser",
                columns: table => new
                {
                    StatusChecksId = table.Column<int>(type: "int", nullable: false),
                    UsersId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatusCheckUser", x => new { x.StatusChecksId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_StatusCheckUser_StatusChecks_StatusChecksId",
                        column: x => x.StatusChecksId,
                        principalTable: "StatusChecks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StatusCheckUser_Users_UsersId",
                        column: x => x.UsersId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StatusCheckUser_UsersId",
                table: "StatusCheckUser",
                column: "UsersId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StatusCheckUser");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "StatusChecks",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_StatusChecks_UserId",
                table: "StatusChecks",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_StatusChecks_Users_UserId",
                table: "StatusChecks",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
