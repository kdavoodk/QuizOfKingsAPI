using System;
using System.Collections.Generic;
using System.Web.Http;
using QuizOfKingsAPI.Models;


namespace QuizOfKingsAPI.Controllers
{
    public class SaveGameAnswerController : ApiController
    {
        public IEnumerable<string> Get()
        {
            return new string[] { "Its Ok", "Lets work" };
        }

        [HttpPost]
        public IHttpActionResult Post([FromBody]SaveGameAnswerParam Param)
        {
            Game Result = new Game();
            if (Param == null) { return new RawJsonActionResult(Newtonsoft.Json.JsonConvert.SerializeObject(Result)); }
            if (Param.ServiceKey != BaseObjects.SERVICE_PASS) { return new RawJsonActionResult(Newtonsoft.Json.JsonConvert.SerializeObject(Result)); }

            Result.SaveGameAnswer(Param.GameID, Param.UserID,Param.QuestionID,Param.AnswerID);

            BaseObjects.GeneralResponse Response = new BaseObjects.GeneralResponse();
            Response.Message = "Done";

            return new RawJsonActionResult(Newtonsoft.Json.JsonConvert.SerializeObject(Response));

        }
    }
}
