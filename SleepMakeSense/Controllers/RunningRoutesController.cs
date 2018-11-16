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
using System.Data.SqlClient;

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
    public class RunningRoutesController : Controller
    {
        private SleepbetaDataContext db = new SleepbetaDataContext();

        public async Task<ActionResult> Index(string logid)
        {
            // Get numofDays data entries to correlation analysis
            // 20170214 Pandita: I feel this is parameter can be tweeked, but 40 sounds like a good value for the time being
            int numOfDays = 40;

            // 20170302 Pandita: use local time
            DateTime baseTime = DateTime.UtcNow;
            baseTime = DateTime.SpecifyKind(baseTime, DateTimeKind.Unspecified);

            TimeSpan offset = TimeZoneInfo.FindSystemTimeZoneById("E. Australia Standard Time").GetUtcOffset(baseTime); // offset value is between -14.0 (towards easthemisphere) ~ 14.0 (towards westhemisphere)
            DateTimeOffset sourceTime = new DateTimeOffset(baseTime, -offset);
            DateTime dateNow = sourceTime.LocalDateTime;

            //Comment out the bellow line to disable getting the current logged in user data
            string userId = System.Web.HttpContext.Current.User.Identity.GetUserId();

            //Get Running Data with designated log-id
            FitbitClient client = GetFitbitClient();

            var apiCall = FitbitClientHelperExtensions.ToFullUrl("/1/user/-/activities/{0}.tcx", logid);
            //logid: 11231322224

            string runningdata = await client.GetRunningData(apiCall);
            /*
            ViewBag.runningdata = runningdata;

            //Parsing the Running Data
            char delimiterChar = '<';
            string[] parsingrunningdata = runningdata.Split(delimiterChar);
            ViewBag.parsingrunningdata = parsingrunningdata;
            */

            //Xml analysis
            XmlDocument doc = new XmlDocument();
            List<RunningRoute> records = new List<RunningRoute>();
            doc.LoadXml(runningdata);

            //Creating namespace object
            XmlNamespaceManager nsmgr = new XmlNamespaceManager(doc.NameTable);
            nsmgr.AddNamespace("ns", "http://www.garmin.com/xmlschemas/TrainingCenterDatabase/v2");

            //Parsing tcx data
            int i = 0;
            int count = doc.SelectNodes("//ns:Trackpoint", nsmgr).Count;
            string[] ts = new string[count];
            string[] lat = new string[count];
            string[] lng = new string[count];
            string[] altd = new string[count];
            double[] dist = new double[count];
            string[] hr = new string[count];

            foreach (XmlNode node in doc.SelectNodes("//ns:Trackpoint", nsmgr))
            {
                ts[i] = node["Time"].InnerText;
                lat[i] = node["Position"]["LatitudeDegrees"].InnerText;
                lng[i] = node["Position"]["LongitudeDegrees"].InnerText;
                altd[i] = node["AltitudeMeters"].InnerText;
                dist[i] = double.Parse(node["DistanceMeters"].InnerText);
                hr[i] = node["HeartRateBpm"]["Value"].InnerText;
                i++;
            }


            //Parsing timestamp and Calculating instantaneous speed
            string[] instantspeed = new string[count];
            instantspeed[0] = "0.000";
            DateTime[] ts_compute = new DateTime[count];
            string ts_chart = null;

            for (int j = 0; j < count; j++)
            {
                ts_compute[j] = new DateTime(Int32.Parse(ts[j].Substring(0, 4)), Int32.Parse(ts[j].Substring(5, 2)), Int32.Parse(ts[j].Substring(8, 2)), Int32.Parse(ts[j].Substring(11, 2)), Int32.Parse(ts[j].Substring(14, 2)), Int32.Parse(ts[j].Substring(17, 2)), Int32.Parse(ts[j].Substring(20, 3)));
                TimeSpan ts_chart0 = new TimeSpan(ts_compute[0].Ticks);
                if (j == 0)
                {
                    ts_chart = "0.0,";
                }
                if (j == 1)
                {
                    TimeSpan ts_chart1 = new TimeSpan(ts_compute[1].Ticks);
                    ts_chart = ((ts_chart1.Subtract(ts_chart0).Duration().TotalMilliseconds) / 1000).ToString("0.0");
                    ts_chart += ",";
                }
                if (j > 1)
                {
                    TimeSpan ts1 = new TimeSpan(ts_compute[j - 2].Ticks);
                    TimeSpan ts2 = new TimeSpan(ts_compute[j].Ticks);
                    double timediff = (ts1.Subtract(ts2).Duration().TotalMilliseconds) / 1000;
                    instantspeed[j - 1] = ((dist[j] - dist[j - 2]) / timediff).ToString("0.000");
                    ts_chart += ((ts2.Subtract(ts_chart0).Duration().TotalMilliseconds) / 1000).ToString("0.0");
                    ts_chart += ",";
                }
            }
            instantspeed[count - 1] = instantspeed[count - 2];

            // Modify the format of speed and heartrate for the chart
            string spd_chart = null;
            string hr_chart = null;
            string dist_chart = null;

            for (int j = 0; j < count; j++)
            {
                spd_chart += instantspeed[j];
                spd_chart += ",";
                hr_chart += hr[j];
                hr_chart += ",";
                dist_chart += (dist[j] / 1000).ToString("0.00");
                dist_chart += ",";
            }
            ViewBag.TS = ts_chart;
            ViewBag.SPD = spd_chart;
            ViewBag.HR = hr_chart;
            ViewBag.DIST = dist_chart;

            // Save data into database
            string routes = "[";
            for (int k = 0; k < count; k++)
            {
                records.Add(new RunningRoute
                {
                    TimeStamp = ts[k],
                    Latitude = lat[k],
                    Longitude = lng[k],
                    Altitude = altd[k],
                    Distance = dist[k].ToString(),
                    HeartRateBpm = hr[k],
                    InstantaneousSpeed = instantspeed[k]
                });

                routes += "{";
                routes += string.Format("'lat': '{0}',", lat[k]);
                routes += string.Format("'lng': '{0}',", lng[k]);
                routes += string.Format("'color': '{0}',", "#FF0000");
                routes += "},";
            }
            routes += "];";
            ViewBag.Routes = routes;


            //Read TotalTime
            string avespeed = null;
            string totalperformance = null;

            foreach (XmlNode node in doc.SelectNodes("//ns:Activity", nsmgr))
            {
                // Calculate average speed
                avespeed = (float.Parse(node["Lap"]["DistanceMeters"].InnerText) / float.Parse(node["Lap"]["TotalTimeSeconds"].InnerText)).ToString("0.000");

                // Input values into database
                records.Add(new RunningRoute
                {
                    TotalTimeSeconds = node["Lap"]["TotalTimeSeconds"].InnerText,
                    TotalDistanceMeters = node["Lap"]["DistanceMeters"].InnerText,
                    AverageSpeed = avespeed

                });

                totalperformance = string.Format("Total Time: {0} seconds,  Total Distance: {1} meters,  Average Speed: {2} m/s", node["Lap"]["TotalTimeSeconds"].InnerText, node["Lap"]["DistanceMeters"].InnerText, avespeed);

            }
            ViewBag.Totalperformance = totalperformance;

            
            return View(records);
        }

        public ActionResult Details(List<RunningRoute> records)
        {
            return View(records);
        }
        
        private FitbitClient GetFitbitClient(OAuth2AccessToken accessToken = null)
        {
            if (Session["FitbitClient"] == null)
            {
                if (accessToken != null)
                {
                    var appCredentials = (FitbitAppCredentials)Session["AppCredentials"];
                    FitbitClient client = new FitbitClient(appCredentials, accessToken);
                    Session["FitbitClient"] = client;
                    return client;
                }
                else
                {
                    throw new Exception("First time requesting a FitbitClient from the session you must pass the AccessToken.");
                }

            }
            else
            {
                return (FitbitClient)Session["FitbitClient"];
            }
        }


    }
}