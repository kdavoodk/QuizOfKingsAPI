using QuizOfKingsAPI.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace QuizOfKingsAPI.Controllers
{
    public class AppVersionController : ApiController
    {
        // GET: api/AppVersion
        public IEnumerable<string> Get()
        {
            return new string[] { "Its Ok", "Lets work" };
        }

        // GET: api/AppVersion/5
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //POST: api/AppVersion
        [HttpPost]
        public IHttpActionResult Post([FromBody]BaseObjects.AppVersionParams Param)
        {
            BaseObjects.VersionResult AppVersion = new BaseObjects.VersionResult();

            if (Param.ServicePass != BaseObjects.SERVICE_PASS) { return new RawJsonActionResult(Newtonsoft.Json.JsonConvert.SerializeObject(AppVersion)); }

            AppVersion.AppVersion = ConfigurationManager.AppSettings["AppVersion"].ToString();
            AppVersion.DownloadLink = ConfigurationManager.AppSettings["AppLink"].ToString();

            return new RawJsonActionResult(Newtonsoft.Json.JsonConvert.SerializeObject(AppVersion));
        }

    }
}
