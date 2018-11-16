using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SleepMakeSense.Models
{
    public class SyncViewModel
    { 
        public List<CorrList> MinutesAsleepCorrList { get; set; }

        public List<CorrList> AwakeCountCorrList { get; set; }

        public List<CorrList> SleepEffiencyCorrList { get; set; }

        public List<Userdata> UserData { get; set; }
    }
}

