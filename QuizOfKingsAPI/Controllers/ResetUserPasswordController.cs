using System;
using System.Collections.Generic;
using System.Web.Http;
using QuizOfKingsAPI.Models;

namespace QuizOfKingsAPI.Controllers
{
    public class ResetUserPasswordController : ApiController
    {
        public IEnumerable<string> Get()
        {
            return new string[] { "Its Ok", "Lets work" };
        }

        [HttpPost]
        public IHttpActionResult Post([FromBody]ResetPasswordParams Param)
        {
            User Result = new User();
            UserResultParams U = new UserResultParams();
            if (Param == null) { return new RawJsonActionResult(Newtonsoft.Json.JsonConvert.SerializeObject(Result)); }
            if (Param.ServiceKey != BaseObjects.SERVICE_PASS) { return new RawJsonActionResult(Newtonsoft.Json.JsonConvert.SerializeObject(Result)); }

            U = Result.ResetUserPassword(Param.Mobile,Param.SecurityQuestionAnswer,Param.NewPassword);

            return new RawJsonActionResult(Newtonsoft.Json.JsonConvert.SerializeObject(U));

        }
    }
}
