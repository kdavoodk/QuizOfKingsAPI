using System;
using System.Collections.Generic;
using System.Web.Http;
using QuizOfKingsAPI.Models;

namespace QuizOfKingsAPI.Controllers
{
    public class OnLineGameListController : ApiController
    {
        public IEnumerable<string> Get()
        {
            return new string[] { "Its Ok", "Lets work" };
        }

        [HttpPost]
        public IHttpActionResult Post([FromBody]OnlineQuestionParam Param)
        {
            List<Game> G = new List<Game>();
            if (Param == null) { return new RawJsonActionResult(Newtonsoft.Json.JsonConvert.SerializeObject(G)); }
            if (Param.ServiceKey != BaseObjects.SERVICE_PASS) { return new RawJsonActionResult(Newtonsoft.Json.JsonConvert.SerializeObject(G)); }

            G = QuestionGroup.OnLineGames(Param.UserID,Param.QuestionGroupID);

            return new RawJsonActionResult(Newtonsoft.Json.JsonConvert.SerializeObject(G));

        }
    }
}
