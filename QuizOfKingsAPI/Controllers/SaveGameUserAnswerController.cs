using System;
using System.Collections.Generic;
using System.Web.Http;
using QuizOfKingsAPI.Models;


namespace QuizOfKingsAPI.Controllers
{
    public class SaveGameUserAnswerController : ApiController
    {
        public IEnumerable<string> Get()
        {
            return new string[] { "Its Ok", "Lets work" };
        }

        [HttpPost]
        public IHttpActionResult Post([FromBody]SaveGameAnswerParam Param)
        {
            BaseObjects.GeneralResponse Result = new BaseObjects.GeneralResponse();
            if (Param == null) { return new RawJsonActionResult(Newtonsoft.Json.JsonConvert.SerializeObject(Result)); }
            if (Param.ServiceKey != BaseObjects.SERVICE_PASS) { return new RawJsonActionResult(Newtonsoft.Json.JsonConvert.SerializeObject(Result)); }

            Result=Game.SaveGameUserAnswer(Param.GameID, Param.UserID,Param.QuestionID,Param.AnswerID);

            return new RawJsonActionResult(Newtonsoft.Json.JsonConvert.SerializeObject(Result));

        }
    }
}
