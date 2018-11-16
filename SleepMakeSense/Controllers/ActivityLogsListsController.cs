using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Net;
using System.Net.Http;
using System.Xml;
using System.Net.Http.Headers;
using System.Configuration;

using Fitbit.Models;
using SleepMakeSense.Models;
using SleepMakeSense.DataAccessLayer;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;
using Fitbit.Api.Portable;
using Fitbit.Api.Portable.Models;

using MathNet.Numerics.Statistics;

namespace SleepMakeSense.Controllers
{
    public class ActivityLogsListsController : Controller
    {
        private SleepbetaDataContext db = new SleepbetaDataContext();

        public ActionResult Index()
        {
            return View();
        }
        
    }
}