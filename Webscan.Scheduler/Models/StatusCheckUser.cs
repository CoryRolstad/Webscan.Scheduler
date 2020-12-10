using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Webscan.Scheduler.Models
{
    public class StatusCheckUser
    {
        public int StatusCheckId { get; set; }
        public int UserId { get; set; }
    }
}
