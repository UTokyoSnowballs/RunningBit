using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity.Spatial;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SleepMakeSense.Models
{
    public class RunningRoute
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("AspNetUser")]
        public string AspNetUserId { get; set; }
        public AspNetUser AspNetUser { get; set; }

        public string TotalTimeSeconds { get; set; }
        public string TotalDistanceMeters { get; set; }
        public string TimeStamp { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string Altitude { get; set; }
        public string Distance { get; set; }
        public string HeartRateBpm { get; set; }
        public string AverageSpeed { get; set; }
        public string InstantaneousSpeed { get; set; }


    }
}