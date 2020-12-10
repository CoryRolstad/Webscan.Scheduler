using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Webscan.Scheduler.Migrations
{
    public partial class dbInit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StatusChecks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    XPath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    XPathContentFailureString = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CronExpression = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QueryTimeInSeconds = table.Column<int>(type: "int", nullable: false),
                    LastNotified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Enabled = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatusChecks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StatusCheckUsers",
                columns: table => new
                {
                    StatusCheckId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatusCheckUsers", x => new { x.StatusCheckId, x.UserId });
                    table.ForeignKey(
                        name: "FK_StatusCheckUsers_StatusChecks_StatusCheckId",
                        column: x => x.StatusCheckId,
                        principalTable: "StatusChecks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StatusCheckUsers_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "StatusChecks",
                columns: new[] { "Id", "CronExpression", "Enabled", "LastNotified", "Name", "QueryTimeInSeconds", "Url", "XPath", "XPathContentFailureString" },
                values: new object[,]
                {
                    { 1, "* * * * *", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "NewEgg: EVGA 3080 FTW3", 0, "https://www.newegg.com/evga-geforce-rtx-3080-10g-p5-3897-kr/p/N82E16814487518?Description=3080%20ftw&cm_re=3080_ftw-_-14-487-518-_-Product", "//*[@id=\"ProductBuy\"]/div/div/span", "Sold Out" },
                    { 18, "* * * * *", true, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "AMD.com: 5000 Series Processors", 0, "https://www.amd.com/en/where-to-buy/ryzen-5000-series-processors", "/html/body/div[1]/main/div/div/div/article/div/div[1]/div[1]/div[2]/div/div[1]/div/div/div/div[2]/div/p/a", "AMD.com - Out of Stock" },
                    { 17, "* * * * *", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Microcenter Instore ONLY St. Louis Park: EVGA 3070 XC3", 0, "https://www.microcenter.com/product/630578/evga-geforce-rtx-3070-xc3-ultra-gaming-triple-fan-8gb-gddr6-pcie-40-graphics-card?storeid=045", "//*[@id=\"pnlInventory\"]/p/span", "Sold Out" },
                    { 16, "* * * * *", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Microcenter Instore ONLY St. Louis Park: ASUS TUF 3080", 0, "https://www.microcenter.com/product/628318/asus-geforce-rtx-3080-tuf-gaming-overclocked-triple-fan-10gb-gddr6x-pcie-40-graphics-card?storeid=045", "//*[@id=\"pnlInventory\"]/p/span", "Sold Out" },
                    { 15, "* * * * *", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Microcenter Instore ONLY St. Louis Park: EVGA 3080 FTW", 0, "https://www.microcenter.com/product/628346/evga-geforce-rtx-3080-ftw3-ultra-gaming-triple-fan-10gb-gddr6x-pcie-40-graphics-card?storeid=045", "//*[@id=\"pnlInventory\"]/p/span", "Sold Out" },
                    { 14, "* * * * *", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "B&H: ASUS 3070 GPU", 0, "https://www.bhphotovideo.com/c/product/1602755-REG/asus_dualrtx30708g_geforce_rtx_3070_8g.html", "//*[@id=\"bh-app\"]/section/div/div[2]/div[4]/div/div[2]/div/div/div[6]/div[1]/div[1]/div/button", "Notify When Available" },
                    { 13, "* * * * *", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "B&H: AMD 5800x CPU", 0, "https://www.bhphotovideo.com/c/product/1598376-REG/amd_100_100000063wof_ryzen_7_5800x_3_8.html", "//*[@id=\"bh-app\"]/section/div/div[2]/div[4]/div/div[2]/div/div/div[6]/div[1]/div[1]/div/button", "Notify When Available" },
                    { 12, "* * * * *", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "B&H: AMD 5600x CPU", 0, "https://www.bhphotovideo.com/c/product/1598377-REG/amd_100_100000065box_ryzen_5_5600x_3_7.html", "//*[@id=\"bh-app\"]/section/div/div[2]/div[4]/div/div[2]/div/div/div[6]/div[1]/div[1]/div/button", "Notify When Available" },
                    { 11, "* * * * *", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "B&H: AMD 5900x CPU", 0, "https://www.bhphotovideo.com/c/product/1598373-REG/amd_100_100000061wof_ryzen_9_5900x_3_7.html", "//*[@id=\"bh-app\"]/section/div/div[2]/div[4]/div/div[2]/div/div/div[6]/div[1]/div[1]/div/button", "Notify When Available" },
                    { 10, null, false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "NewEgg: AMD 5600x CPU", 30, "https://www.newegg.com/amd-ryzen-5-5600x/p/N82E16819113666?Description=5600x&cm_re=5600x-_-19-113-666-_-Product", "//*[@id=\"ProductBuy\"]/div/div/span", "Sold Out" },
                    { 9, null, false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "NewEgg: AMD 5900x CPU", 30, "https://www.newegg.com/amd-ryzen-9-5900x/p/N82E16819113664?Description=5900x&cm_re=5900x-_-19-113-664-_-Product", "//*[@id=\"ProductBuy\"]/div/div/span", "Sold Out" },
                    { 8, null, false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "NewEgg: ASUS ROG 3080 Strix", 30, "https://www.newegg.com/asus-geforce-rtx-3080-rog-strix-rtx3080-o10g-gaming/p/N82E16814126457?Description=3080&cm_re=3080-_-14-126-457-_-Product", "//*[@id=\"ProductBuy\"]/div/div/span", "Sold Out" },
                    { 7, null, false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "NewEgg: PNY 3080", 30, "https://www.newegg.com/pny-geforce-rtx-3080-vcg308010tfxmpb/p/N82E16814133810?Description=3080&cm_re=3080-_-14-133-810-_-Product", "//*[@id=\"ProductBuy\"]/div/div/span", "Out of Stock " },
                    { 6, null, false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "NewEgg: Gigabyte 3080 Auros", 30, "https://www.newegg.com/gigabyte-geforce-rtx-3080-gv-n3080vision-oc-10gd/p/N82E16814932337?Description=3080&cm_re=3080-_-14-932-337-_-Product", "//*[@id=\"ProductBuy\"]/div/div/span", "Sold Out" },
                    { 5, null, false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "NewEgg: Asus 3080 TUF", 30, "https://www.newegg.com/asus-geforce-rtx-3080-tuf-rtx3080-o10g-gaming/p/N82E16814126452?Description=3080&cm_re=3080-_-14-126-452-_-Product", "//*[@id=\"ProductBuy\"]/div/div/span", "Sold Out" },
                    { 4, null, false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "NewEgg: EVGA 3080 XC3", 30, "https://www.newegg.com/evga-geforce-rtx-3080-10g-p5-3885-kr/p/N82E16814487520?Description=3080&cm_re=3080-_-14-487-520-_-Product&quicklink=true", "//*[@id=\"ProductBuy\"]/div/div/span", "Sold Out" },
                    { 3, "* * * * *", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "BestBuy: AMD 5600x", 0, "https://www.bestbuy.com/site/amd-ryzen-5-5600x-4th-gen-6-core-12-threads-unlocked-desktop-processor-with-wraith-stealth-cooler/6438943.p?skuId=6438943", "/html/body/div[3]/main/div[2]/div[3]/div[2]/div/div/div[6]/div[1]/div/div/div/button", "Sold Out" },
                    { 2, "* * * * *", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "BestBuy: AMD 5900x", 0, "https://www.bestbuy.com/site/amd-ryzen-9-5900x-4th-gen-12-core-24-threads-unlocked-desktop-processor-without-cooler/6438942.p?skuId=6438942", "/html/body/div[3]/main/div[2]/div[3]/div[2]/div/div/div[6]/div[1]/div/div/div/button", "Sold Out" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "Username" },
                values: new object[,]
                {
                    { 1, "6513437334@messaging.sprintpcs.com", "Cory" },
                    { 2, "6129786995@tmomail.net", "Aaron" }
                });

            migrationBuilder.InsertData(
                table: "StatusCheckUsers",
                columns: new[] { "StatusCheckId", "UserId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 3, 2 },
                    { 4, 2 },
                    { 5, 2 },
                    { 6, 2 },
                    { 7, 2 },
                    { 8, 2 },
                    { 2, 2 },
                    { 9, 2 },
                    { 11, 2 },
                    { 12, 2 },
                    { 13, 2 },
                    { 14, 2 },
                    { 15, 2 },
                    { 16, 2 },
                    { 10, 2 },
                    { 1, 2 },
                    { 18, 1 },
                    { 17, 1 },
                    { 2, 1 },
                    { 3, 1 },
                    { 4, 1 },
                    { 5, 1 },
                    { 6, 1 },
                    { 7, 1 },
                    { 8, 1 },
                    { 9, 1 },
                    { 10, 1 },
                    { 11, 1 },
                    { 12, 1 },
                    { 13, 1 },
                    { 14, 1 },
                    { 15, 1 },
                    { 16, 1 },
                    { 17, 2 },
                    { 18, 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_StatusCheckUsers_UserId",
                table: "StatusCheckUsers",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StatusCheckUsers");

            migrationBuilder.DropTable(
                name: "StatusChecks");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
