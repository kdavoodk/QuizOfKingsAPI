using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using QuizOfKingsAPI.Models;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Net.Http.Headers;

namespace QuizOfKingsAPI.Controllers
{

    public class LoginController : ApiController
    {
        public IEnumerable<string> Get()
        {
            return new string[] {"Its Ok", "Lets work" };
        }

        [HttpPost]
        public IHttpActionResult Post([FromBody]BaseObjects.LoginParams Param)
        {
            Models.User Result = new Models.User();
            if (Param == null) { return new RawJsonActionResult(Newtonsoft.Json.JsonConvert.SerializeObject(Result)); }
            if (Param.ServicePass != BaseObjects.SERVICE_PASS) { return new RawJsonActionResult(Newtonsoft.Json.JsonConvert.SerializeObject(Result)); }

            Result.login(Param.Username, Param.Password);

            return new RawJsonActionResult(Newtonsoft.Json.JsonConvert.SerializeObject(Result));

        }

    }
}
