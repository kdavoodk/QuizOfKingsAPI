using System;
using System.Collections.Generic;
using System.Web.Http;
using QuizOfKingsAPI.Models;

namespace QuizOfKingsAPI.Controllers
{
    public class QuestionGroupController : ApiController
    {
        public IEnumerable<string> Get()
        {
            return new string[] { "Its Ok", "Lets work" };
        }

        [HttpPost]
        public IHttpActionResult Post([FromBody]BaseObjects.GeneralParams Param)
        {
            List<QuestionGroupResult> Result = new List<QuestionGroupResult>();
            if (Param == null) { return new RawJsonActionResult(Newtonsoft.Json.JsonConvert.SerializeObject(Result)); }
            if (Param.ServiceKey != BaseObjects.SERVICE_PASS) { return new RawJsonActionResult(Newtonsoft.Json.JsonConvert.SerializeObject(Result)); }

            //Result = QuestionGroup.ListWithGamesStatus(Param.UserID);

            return new RawJsonActionResult(QuestionGroup.ListWithGamesStatus(Param.UserID));

        }
    }
}
