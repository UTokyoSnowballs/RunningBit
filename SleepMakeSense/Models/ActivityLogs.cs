using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SleepMakeSense.Models
{
    public class ActivityLogs
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("AspNetUser")]
        public string AspNetUserId { get; set; }
        public AspNetUser AspNetUser { get; set; }

        [Required]
        public string Logid { get; set; }
    }
}