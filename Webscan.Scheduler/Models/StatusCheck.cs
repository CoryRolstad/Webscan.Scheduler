﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Webscan.Scheduler.Models
{
    public class StatusCheck
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string XPath { get; set; }
        public string XPathContentFailureString { get; set; }
        public string Url { get; set; }
        public bool RenderingJavasciptRequired { get; set; }
        // Shortened Bitly URL - So users can get it in text.
        public string BitlyShortenedUrl { get; set; }
        // Can either have CronExpression for more then 1 minute increments, Cron takes precidense
        public string CronExpression { get; set; }
        // Or can have QueryTimeInSeconds (will query every X seconds)
        public int QueryTimeInSeconds { get; set; }
        // Last Time the users were notified (for notification cool down, so we aren't spamming users)
        public DateTime LastNotified { get; set; }
        [NotMapped]
        public DateTime TimeScheduled { get; set; }
        // Either its enabled or disabled.
        public bool Enabled { get; set; }
        public List<User> Users { get; } = new List<User>(); 
    }
}
