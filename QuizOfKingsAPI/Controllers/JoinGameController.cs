﻿using System;
using System.Collections.Generic;
using System.Web.Http;
using QuizOfKingsAPI.Models;

namespace QuizOfKingsAPI.Controllers
{
    public class JoinGameController : ApiController
    {
        public IEnumerable<string> Get()
        {
            return new string[] { "Its Ok", "Lets work" };
        }

        [HttpPost]
        public IHttpActionResult Post([FromBody]GameParams Param)
        {
            JoinGameResult Result = new JoinGameResult();
            if (Param == null) { return new RawJsonActionResult(Newtonsoft.Json.JsonConvert.SerializeObject(Result)); }
            if (Param.ServiceKey != BaseObjects.SERVICE_PASS) { return new RawJsonActionResult(Newtonsoft.Json.JsonConvert.SerializeObject(Result)); }

            Result=Game.JoinGame(Param.GameID, Param.UserID);

            return new RawJsonActionResult(Newtonsoft.Json.JsonConvert.SerializeObject(Result));

        }
    }
}
