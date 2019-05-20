using System;
using System.Collections.Generic;
using System.Web.Http;
using QuizOfKingsAPI.Models;


namespace QuizOfKingsAPI.Controllers
{
    public class FinishGameController : ApiController
    {
        public IEnumerable<string> Get()
        {
            return new string[] { "Its Ok", "Lets work" };
        }

        [HttpPost]
        public IHttpActionResult Post([FromBody]GameParams Param)
        {
            Game Result = new Game();
            if (Param == null) { return new RawJsonActionResult(Newtonsoft.Json.JsonConvert.SerializeObject(Result)); }
            if (Param.ServiceKey != BaseObjects.SERVICE_PASS) { return new RawJsonActionResult(Newtonsoft.Json.JsonConvert.SerializeObject(Result)); }

            //Result.FinishGame(Param.GameID);

            return new RawJsonActionResult(Newtonsoft.Json.JsonConvert.SerializeObject(Result));

        }
    }
}
