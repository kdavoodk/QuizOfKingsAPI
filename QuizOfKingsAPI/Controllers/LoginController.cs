﻿using System;
using System.Collections.Generic;
using System.Web.Http;
using QuizOfKingsAPI.Models;


namespace QuizOfKingsAPI.Controllers
{

    public class LoginController : ApiController
    {
        public IEnumerable<string> Get()
        {
            return new string[] {"Its Ok", "Lets work" };
        }

        [HttpPost]
        public IHttpActionResult Post([FromBody]LoginParams Param)
        {
            Models.User Result = new Models.User();
            if (Param == null) { return new RawJsonActionResult(Newtonsoft.Json.JsonConvert.SerializeObject(Result)); }
            if (Param.ServiceKey != BaseObjects.SERVICE_PASS) { return new RawJsonActionResult(Newtonsoft.Json.JsonConvert.SerializeObject(Result)); }

            Result.login(Param.Mobile, Param.Password);

            return new RawJsonActionResult(Newtonsoft.Json.JsonConvert.SerializeObject(Result));

        }

    }
}
