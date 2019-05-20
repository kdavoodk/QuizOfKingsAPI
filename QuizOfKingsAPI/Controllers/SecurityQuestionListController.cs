using System;
using System.Collections.Generic;
using System.Web.Http;
using QuizOfKingsAPI.Models;

namespace QuizOfKingsAPI.Controllers
{
    public class SecurityQuestionListController : ApiController
    {
        public IEnumerable<string> Get()
        {
            return new string[] { "Its Ok", "Lets work" };
        }

        [HttpPost]
        public IHttpActionResult Post([FromBody]BaseObjects.GeneralParams Param)
        {
            User Result = new User();
            List<SecurityQuestion> U = new List<SecurityQuestion>();
            if (Param == null) { return new RawJsonActionResult(Newtonsoft.Json.JsonConvert.SerializeObject(Result)); }
            if (Param.ServiceKey != BaseObjects.SERVICE_PASS) { return new RawJsonActionResult(Newtonsoft.Json.JsonConvert.SerializeObject(Result)); }

            U = Result.SecurityQuestionList();

            return new RawJsonActionResult(Newtonsoft.Json.JsonConvert.SerializeObject(U));

        }
    }
}
