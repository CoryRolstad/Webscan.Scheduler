using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webscan.Scheduler.Models;

namespace Webscan.Scheduler.datastore
{
    public class WebscanContext : DbContext
    {
        public WebscanContext(DbContextOptions<WebscanContext> options) : base(options)
        {

        }
        public DbSet<StatusCheck> StatusChecks { get; set; }
        public DbSet<User> Users { get; set; }

        public DbSet<StatusCheckUser> StatusCheckUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<User>()
                .HasMany(u => u.StatusChecks)
                .WithMany(sc => sc.Users)
                .UsingEntity<StatusCheckUser>(
                    scu => scu.HasOne<StatusCheck>().WithMany(),
                    scu => scu.HasOne<User>().WithMany());



            modelBuilder.Entity<StatusCheck>().HasData(new List<StatusCheck>() {
                                new StatusCheck() {
                                    Id = 1,
                                    XPath = "//*[@id=\"ProductBuy\"]/div/div/span",
                                    XPathContentFailureString = "Sold Out",
                                    Url = "https://www.newegg.com/evga-geforce-rtx-3080-10g-p5-3897-kr/p/N82E16814487518?Description=3080%20ftw&cm_re=3080_ftw-_-14-487-518-_-Product",
                                    RenderingJavasciptRequired = false,
                                    BitlyShortenedUrl = "https://bit.ly/3mcVrRa",
                                    CronExpression = "* * * * *",
                                    QueryTimeInSeconds = 0,
                                    Name = "NewEgg: EVGA 3080 FTW3",
                                    LastNotified = new DateTime(),
                                    Enabled = false
                                },
                                new StatusCheck() {
                                    Id = 2,
                                    XPath = "/html/body/div[3]/main/div[2]/div[3]/div[2]/div/div/div[6]/div[1]/div/div/div/button",
                                    XPathContentFailureString = "Sold Out",
                                    Url = "https://www.bestbuy.com/site/amd-ryzen-9-5900x-4th-gen-12-core-24-threads-unlocked-desktop-processor-without-cooler/6438942.p?skuId=6438942",
                                    RenderingJavasciptRequired = true,
                                    BitlyShortenedUrl = "https://bit.ly/3oOo2h8",
                                    CronExpression = "* * * * *",
                                    QueryTimeInSeconds = 0,
                                    Name = "BestBuy: AMD 5900x",
                                    LastNotified = new DateTime(),
                                    Enabled = true
                                },
                                new StatusCheck() {
                                    Id = 3,
                                    XPath = "/html/body/div[3]/main/div[2]/div[3]/div[2]/div/div/div[6]/div[1]/div/div/div/button",
                                    XPathContentFailureString = "Sold Out",
                                    Url = "https://www.bestbuy.com/site/amd-ryzen-5-5600x-4th-gen-6-core-12-threads-unlocked-desktop-processor-with-wraith-stealth-cooler/6438943.p?skuId=6438943",
                                    RenderingJavasciptRequired = true,
                                    BitlyShortenedUrl = "https://bit.ly/3440VHF",
                                    CronExpression = "* * * * *",
                                    QueryTimeInSeconds = 0,
                                    Name = "BestBuy: AMD 5600x",
                                    LastNotified = new DateTime(),
                                    Enabled = true
                                },
                                new StatusCheck() {
                                    Id = 4,
                                    XPath = "//*[@id=\"ProductBuy\"]/div/div/span",
                                    XPathContentFailureString = "Sold Out",
                                    Url = "https://www.newegg.com/evga-geforce-rtx-3080-10g-p5-3885-kr/p/N82E16814487520?Description=3080&cm_re=3080-_-14-487-520-_-Product&quicklink=true",
                                    RenderingJavasciptRequired = false,
                                    BitlyShortenedUrl = "https://bit.ly/3njzaCC",
                                    CronExpression = "* * * * *",
                                    QueryTimeInSeconds = 0,
                                    Name = "NewEgg: EVGA 3080 XC3",
                                    LastNotified = new DateTime(),
                                    Enabled = false
                                },
                                new StatusCheck() {
                                    Id = 5,
                                    XPath = "//*[@id=\"ProductBuy\"]/div/div/span",
                                    XPathContentFailureString = "Sold Out",
                                    Url = "https://www.newegg.com/asus-geforce-rtx-3080-tuf-rtx3080-o10g-gaming/p/N82E16814126452?Description=3080&cm_re=3080-_-14-126-452-_-Product",
                                    RenderingJavasciptRequired = false,
                                    BitlyShortenedUrl = "https://bit.ly/37g0awU",
                                    CronExpression = "* * * * *",
                                    QueryTimeInSeconds = 0,
                                    Name = "NewEgg: Asus 3080 TUF",
                                    LastNotified = new DateTime(),
                                    Enabled = false
                                },
                                new StatusCheck() {
                                    Id = 6,
                                    XPath = "//*[@id=\"ProductBuy\"]/div/div/span",
                                    XPathContentFailureString = "Sold Out",
                                    Url = "https://www.newegg.com/gigabyte-geforce-rtx-3080-gv-n3080vision-oc-10gd/p/N82E16814932337?Description=3080&cm_re=3080-_-14-932-337-_-Product",
                                    RenderingJavasciptRequired = false,
                                    BitlyShortenedUrl = "https://bit.ly/3r0o9bF",
                                    CronExpression = "* * * * *",
                                    QueryTimeInSeconds = 0,
                                    Name = "NewEgg: Gigabyte 3080 Auros",
                                    LastNotified = new DateTime(),
                                    Enabled = false
                                },
                                new StatusCheck() {
                                    Id = 7,
                                    XPath = "//*[@id=\"ProductBuy\"]/div/div/span",
                                    XPathContentFailureString = "Out of Stock ",
                                    Url = "https://www.newegg.com/pny-geforce-rtx-3080-vcg308010tfxmpb/p/N82E16814133810?Description=3080&cm_re=3080-_-14-133-810-_-Product",
                                    RenderingJavasciptRequired = false,
                                    BitlyShortenedUrl = "https://bit.ly/2KiQU2n",
                                    CronExpression = "* * * * *",
                                    QueryTimeInSeconds = 0,
                                    Name = "NewEgg: PNY 3080",
                                    LastNotified = new DateTime(),
                                    Enabled = false
                                },
                                new StatusCheck() {
                                    Id = 8,
                                    XPath = "//*[@id=\"ProductBuy\"]/div/div/span",
                                    XPathContentFailureString = "Sold Out",
                                    Url = "https://www.newegg.com/asus-geforce-rtx-3080-rog-strix-rtx3080-o10g-gaming/p/N82E16814126457?Description=3080&cm_re=3080-_-14-126-457-_-Product",
                                    RenderingJavasciptRequired = false,
                                    BitlyShortenedUrl = "https://bit.ly/383V3iV",
                                    CronExpression = "* * * * *",
                                    QueryTimeInSeconds = 0,
                                    Name = "NewEgg: ASUS ROG 3080 Strix",
                                    LastNotified = new DateTime(),
                                    Enabled = false
                                },
                                new StatusCheck() {
                                    Id = 9,
                                    XPath = "//*[@id=\"ProductBuy\"]/div/div/span",
                                    XPathContentFailureString = "Sold Out",
                                    Url = "https://www.newegg.com/amd-ryzen-9-5900x/p/N82E16819113664?Description=5900x&cm_re=5900x-_-19-113-664-_-Product",
                                    RenderingJavasciptRequired = false,
                                    BitlyShortenedUrl = "https://bit.ly/2W9eelw",
                                    CronExpression = "* * * * *",
                                    QueryTimeInSeconds = 0,
                                    Name = "NewEgg: AMD 5900x CPU",
                                    LastNotified = new DateTime(),
                                    Enabled = false
                                },
                                new StatusCheck() {
                                    Id = 10,
                                    XPath = "//*[@id=\"ProductBuy\"]/div/div/span",
                                    XPathContentFailureString = "Sold Out",
                                    Url = "https://www.newegg.com/amd-ryzen-5-5600x/p/N82E16819113666?Description=5600x&cm_re=5600x-_-19-113-666-_-Product",
                                    RenderingJavasciptRequired = false,
                                    BitlyShortenedUrl = "https://bit.ly/2KiKLmO",
                                    CronExpression = "* * * * *",
                                    QueryTimeInSeconds = 0,
                                    Name = "NewEgg: AMD 5600x CPU",
                                    LastNotified = new DateTime(),
                                    Enabled = false
                                },
                                new StatusCheck() {
                                    Id = 11,
                                    XPath = "//*[@id=\"bh-app\"]/section/div/div[2]/div[4]/div/div[2]/div/div/div[6]/div[1]/div[1]/div/button",
                                    XPathContentFailureString = "Notify When Available",
                                    Url = "https://www.bhphotovideo.com/c/product/1598373-REG/amd_100_100000061wof_ryzen_9_5900x_3_7.html",
                                    RenderingJavasciptRequired = false,
                                    BitlyShortenedUrl = "https://bhpho.to/3oNIbnM",
                                    CronExpression = "* * * * *",
                                    QueryTimeInSeconds = 0,
                                    Name = "B&H: AMD 5900x CPU",
                                    LastNotified = new DateTime(),
                                    Enabled = false
                                },
                                new StatusCheck() {
                                    Id = 12,
                                    XPath = "//*[@id=\"bh-app\"]/section/div/div[2]/div[4]/div/div[2]/div/div/div[6]/div[1]/div[1]/div/button",
                                    XPathContentFailureString = "Notify When Available",
                                    Url = "https://www.bhphotovideo.com/c/product/1598377-REG/amd_100_100000065box_ryzen_5_5600x_3_7.html",
                                    RenderingJavasciptRequired = false,
                                    BitlyShortenedUrl = "https://bhpho.to/3gGKvtT",
                                    CronExpression = "* * * * *",
                                    QueryTimeInSeconds = 0,
                                    Name = "B&H: AMD 5600x CPU",
                                    LastNotified = new DateTime(),
                                    Enabled = false
                                },
                                new StatusCheck() {
                                    Id = 13,
                                    XPath = "//*[@id=\"bh-app\"]/section/div/div[2]/div[4]/div/div[2]/div/div/div[6]/div[1]/div[1]/div/button",
                                    XPathContentFailureString = "Notify When Available",
                                    Url = "https://www.bhphotovideo.com/c/product/1598376-REG/amd_100_100000063wof_ryzen_7_5800x_3_8.html",
                                    RenderingJavasciptRequired = false,
                                    BitlyShortenedUrl = "https://bhpho.to/2KiKRuG",
                                    CronExpression = "* * * * *",
                                    QueryTimeInSeconds = 0,
                                    Name = "B&H: AMD 5800x CPU",
                                    LastNotified = new DateTime(),
                                    Enabled = false
                                },
                                new StatusCheck() {
                                    Id = 14,
                                    XPath = "//*[@id=\"bh-app\"]/section/div/div[2]/div[4]/div/div[2]/div/div/div[6]/div[1]/div[1]/div/button",
                                    XPathContentFailureString = "Notify When Available",
                                    Url = "https://www.bhphotovideo.com/c/product/1602755-REG/asus_dualrtx30708g_geforce_rtx_3070_8g.html",
                                    RenderingJavasciptRequired = false,
                                    BitlyShortenedUrl = "https://bhpho.to/346hDpW",
                                    CronExpression = "* * * * *",
                                    QueryTimeInSeconds = 0,
                                    Name = "B&H: ASUS 3070 GPU",
                                    LastNotified = new DateTime(),
                                    Enabled = false
                                },
                                new StatusCheck() {
                                    Id = 15,
                                    XPath = "//*[@id=\"pnlInventory\"]/p/span",
                                    XPathContentFailureString = "Sold Out",
                                    Url = "https://www.microcenter.com/product/628346/evga-geforce-rtx-3080-ftw3-ultra-gaming-triple-fan-10gb-gddr6x-pcie-40-graphics-card?storeid=045",
                                    RenderingJavasciptRequired = false,
                                    BitlyShortenedUrl = "https://bit.ly/3oLKnvU",
                                    CronExpression = "* * * * *",
                                    QueryTimeInSeconds = 0,
                                    Name = "Microcenter Instore ONLY St. Louis Park: EVGA 3080 FTW",
                                    LastNotified = new DateTime(),
                                    Enabled = false
                                },
                                new StatusCheck() {
                                    Id = 16,
                                    XPath = "//*[@id=\"pnlInventory\"]/p/span",
                                    XPathContentFailureString = "Sold Out",
                                    Url = "https://www.microcenter.com/product/628318/asus-geforce-rtx-3080-tuf-gaming-overclocked-triple-fan-10gb-gddr6x-pcie-40-graphics-card?storeid=045",
                                    RenderingJavasciptRequired = false,
                                    BitlyShortenedUrl = "https://bit.ly/3r0orPN",
                                    CronExpression = "* * * * *",
                                    QueryTimeInSeconds = 0,
                                    Name = "Microcenter Instore ONLY St. Louis Park: ASUS TUF 3080",
                                    LastNotified = new DateTime(),
                                    Enabled = false
                                },
                                new StatusCheck() {
                                    Id = 17,
                                    XPath = "//*[@id=\"pnlInventory\"]/p/span",
                                    XPathContentFailureString = "Sold Out",
                                    Url = "https://www.microcenter.com/product/630578/evga-geforce-rtx-3070-xc3-ultra-gaming-triple-fan-8gb-gddr6-pcie-40-graphics-card?storeid=045",
                                    RenderingJavasciptRequired = false,
                                    BitlyShortenedUrl = "https://bit.ly/3qURSCL",
                                    CronExpression = "* * * * *",
                                    QueryTimeInSeconds = 0,
                                    Name = "Microcenter Instore ONLY St. Louis Park: EVGA 3070 XC3",
                                    LastNotified = new DateTime(),
                                    Enabled = false
                                },
                                new StatusCheck() {
                                    Id = 18,
                                    XPath = "/html/body/div[1]/main/div/div/div/article/div/div[1]/div[1]/div[2]/div/div[1]/div/div/div/div[2]/div/p/a",
                                    XPathContentFailureString = "AMD.com - Out of Stock",
                                    Url = "https://www.amd.com/en/where-to-buy/ryzen-5000-series-processors",
                                    RenderingJavasciptRequired = true,
                                    BitlyShortenedUrl = "https://bit.ly/2Kx8EHc",
                                    CronExpression = "* * * * *",
                                    QueryTimeInSeconds = 0,
                                    Name = "AMD.com: 5000 Series Processors",
                                    LastNotified = new DateTime(),
                                    Enabled = true
                                },
                                new StatusCheck() {
                                    Id = 19,
                                    XPath = "/html/body/div[3]/main/div[2]/div[3]/div[2]/div/div/div[6]/div[1]/div/div/div/button",
                                    XPathContentFailureString = "Sold Out",
                                    Url = "https://www.bestbuy.com/site/evga-geforce-rtx-3080-ftw3-ultra-gaming-10gb-gddr6-pci-express-4-0-graphics-card/6436196.p?skuId=6436196",
                                    RenderingJavasciptRequired = false,
                                    BitlyShortenedUrl = "https://bit.ly/3oOW8lm",
                                    CronExpression = "* * * * *",
                                    QueryTimeInSeconds = 0,
                                    Name = "BestBuy: 3080 FTW3",
                                    LastNotified = new DateTime(),
                                    Enabled = false
                                },
                                new StatusCheck() {
                                    Id = 20,
                                    XPath = "/html/body/div[3]/main/div[2]/div[3]/div[2]/div/div/div[7]/div[1]/div/div/div/button",
                                    XPathContentFailureString = "Sold Out",
                                    Url = "https://www.bestbuy.com/site/evga-geforce-rtx-3080-xc3-ultra-gaming-10gb-gddr6-pci-express-4-0-graphics-card/6432400.p?skuId=6432400",
                                    RenderingJavasciptRequired = false,
                                    BitlyShortenedUrl = "https://bit.ly/34abicR",
                                    CronExpression = "* * * * *",
                                    QueryTimeInSeconds = 0,
                                    Name = "BestBuy: 3080 XC3",
                                    LastNotified = new DateTime(),
                                    Enabled = false
                                },
                                new StatusCheck() {
                                    Id = 21,
                                    XPath = "//*[@id=\"outOfStock\"]/div/div[1]/span",
                                    XPathContentFailureString = "Currently unavailable.",
                                    Url = "https://www.amazon.com/gp/product/B08166SLDF/ref=ppx_od_dt_b_asin_title_s00?ie=UTF8&psc=12",
                                    RenderingJavasciptRequired = false,
                                    BitlyShortenedUrl = "https://amzn.to/348mpD0",
                                    CronExpression = "* * * * *",
                                    QueryTimeInSeconds = 0,
                                    Name = "Amazon: AMD 5600x",
                                    LastNotified = new DateTime(),
                                    Enabled = false
                                }

                            }); ;

            modelBuilder.Entity<User>().HasData(new List<User>()
                            {
                                    new User()
                                    {
                                        Id = 1,
                                        Username = "Cory",
                                        Email = "6513437334@messaging.sprintpcs.com"
                                    },
                                    new User()
                                    {
                                        Id = 2,
                                        Username = "Aaron",
                                        Email = "6129786995@tmomail.net"
                                    }
                            });


            modelBuilder.Entity<StatusCheckUser>().HasData(new List<StatusCheckUser>()
                        {
                            new StatusCheckUser()
                            {
                                Enabled = true,
                                StatusCheckId = 1,
                                UserId = 1
                                
                            },
                            new StatusCheckUser()
                            {
                                Enabled = true,
                                StatusCheckId = 2,
                                UserId = 1
                            },
                            new StatusCheckUser()
                            {
                                Enabled = true,
                                StatusCheckId = 3,
                                UserId = 1
                            },
                            new StatusCheckUser()
                            {
                                Enabled = true,
                                StatusCheckId = 4,
                                UserId = 1
                            },
                            new StatusCheckUser()
                            {
                                Enabled = true,
                                StatusCheckId = 5,
                                UserId = 1
                            },
                            new StatusCheckUser()
                            {
                                Enabled = true,
                                StatusCheckId = 6,
                                UserId = 1
                            },
                            new StatusCheckUser()
                            {
                                Enabled = true,
                                StatusCheckId = 7,
                                UserId = 1
                            },
                            new StatusCheckUser()
                            {
                                Enabled = true,
                                StatusCheckId = 8,
                                UserId = 1
                            },
                            new StatusCheckUser()
                            {
                                Enabled = true,
                                StatusCheckId = 9,
                                UserId = 1
                            },
                            new StatusCheckUser()
                            {
                                Enabled = true,
                                StatusCheckId = 10,
                                UserId = 1
                            },
                            new StatusCheckUser()
                            {
                                Enabled = true,
                                StatusCheckId = 11,
                                UserId = 1
                            },
                            new StatusCheckUser()
                            {
                                Enabled = true,
                                StatusCheckId = 12,
                                UserId = 1
                            },
                            new StatusCheckUser()
                            {
                                Enabled = true,
                                StatusCheckId = 13,
                                UserId = 1
                            },
                            new StatusCheckUser()
                            {
                                Enabled = true,
                                StatusCheckId = 14,
                                UserId = 1
                            },
                            new StatusCheckUser()
                            {
                                Enabled = true,
                                StatusCheckId = 15,
                                UserId = 1
                            },
                            new StatusCheckUser()
                            {
                                Enabled = true,
                                StatusCheckId = 16,
                                UserId = 1
                            },
                            new StatusCheckUser()
                            {
                                Enabled = true,
                                StatusCheckId = 17,
                                UserId = 1
                            },
                            new StatusCheckUser()
                            {
                                Enabled = true,
                                StatusCheckId = 18,
                                UserId = 1
                            },
                            new StatusCheckUser()
                            {
                                Enabled = true,
                                StatusCheckId = 19,
                                UserId = 1
                            },
                            new StatusCheckUser()
                            {
                                Enabled = true,
                                StatusCheckId = 20,
                                UserId = 1
                            },
                            new StatusCheckUser()
                            {
                                Enabled = true,
                                StatusCheckId = 21,
                                UserId = 1
                            },
                            new StatusCheckUser()
                            {
                                Enabled = true,
                                StatusCheckId = 1,
                                UserId = 2
                            },
                            new StatusCheckUser()
                            {
                                Enabled = true,
                                StatusCheckId = 2,
                                UserId = 2
                            },
                            new StatusCheckUser()
                            {
                                Enabled = true,
                                StatusCheckId = 3,
                                UserId = 2
                            },
                            new StatusCheckUser()
                            {
                                Enabled = true,
                                StatusCheckId = 4,
                                UserId = 2
                            },
                            new StatusCheckUser()
                            {
                                Enabled = true,
                                StatusCheckId = 5,
                                UserId = 2
                            },
                            new StatusCheckUser()
                            {
                                Enabled = true,
                                StatusCheckId = 6,
                                UserId = 2
                            },
                            new StatusCheckUser()
                            {
                                Enabled = true,
                                StatusCheckId = 7,
                                UserId = 2
                            },
                            new StatusCheckUser()
                            {
                                Enabled = true,
                                StatusCheckId = 8,
                                UserId = 2
                            },
                            new StatusCheckUser()
                            {
                                Enabled = true,
                                StatusCheckId = 9,
                                UserId = 2
                            },
                            new StatusCheckUser()
                            {
                                Enabled = true,
                                StatusCheckId = 10,
                                UserId = 2
                            },
                            new StatusCheckUser()
                            {
                                Enabled = true,
                                StatusCheckId = 11,
                                UserId = 2
                            },
                            new StatusCheckUser()
                            {
                                Enabled = true,
                                StatusCheckId = 12,
                                UserId = 2
                            },
                            new StatusCheckUser()
                            {
                                Enabled = true,
                                StatusCheckId = 13,
                                UserId = 2
                            },
                            new StatusCheckUser()
                            {
                                Enabled = true,
                                StatusCheckId = 14,
                                UserId = 2
                            },
                            new StatusCheckUser()
                            {
                                Enabled = true,
                                StatusCheckId = 15,
                                UserId = 2
                            },
                            new StatusCheckUser()
                            {
                                Enabled = true,
                                StatusCheckId = 16,
                                UserId = 2
                            },
                            new StatusCheckUser()
                            {
                                Enabled = true,
                                StatusCheckId = 17,
                                UserId = 2
                            },
                            new StatusCheckUser()
                            {
                                Enabled = true,
                                StatusCheckId = 18,
                                UserId = 2
                            },
                            new StatusCheckUser()
                            {
                                Enabled = true,
                                StatusCheckId = 19,
                                UserId = 2
                            },
                            new StatusCheckUser()
                            {
                                Enabled = true,
                                StatusCheckId = 20,
                                UserId = 2
                            },
                            new StatusCheckUser()
                            {
                                Enabled = true,
                                StatusCheckId = 21,
                                UserId = 2
                            }

                        });


        }

    }
}
