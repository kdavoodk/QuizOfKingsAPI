﻿using System;
using System.Collections.Generic;
using System.Web.Http;
using QuizOfKingsAPI.Models;

namespace QuizOfKingsAPI.Controllers
{
    public class RegisterController : ApiController
    {
        public IEnumerable<string> Get()
        {
            return new string[] { "Its Ok", "Lets work" };
        }

        [HttpPost]
        public IHttpActionResult Post([FromBody]RegisterParams Param)
        {
            User Result = new User();
            UserResultParams U = new UserResultParams(); 
            if (Param == null) { return new RawJsonActionResult(Newtonsoft.Json.JsonConvert.SerializeObject(Result)); }
            if (Param.ServiceKey != BaseObjects.SERVICE_PASS) { return new RawJsonActionResult(Newtonsoft.Json.JsonConvert.SerializeObject(Result)); }

            U = Result.register(Param.Mobile, Param.Password,Param.Name,Param.SecurityQuestionID,Param.SecurityQuestionAnswer);

            return new RawJsonActionResult(Newtonsoft.Json.JsonConvert.SerializeObject(U));

        }
    }
}
 