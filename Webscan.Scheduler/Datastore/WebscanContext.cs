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
    }
}
