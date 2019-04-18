using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data;
using System.Data.SqlClient;

namespace QuizOfKingsAPI.Models
{
    public class Answer
    {
        public string ID;
        public Question Question = new Question();
        public string OrderNo;
        public string OrderText;
        public string Description;
    }

}