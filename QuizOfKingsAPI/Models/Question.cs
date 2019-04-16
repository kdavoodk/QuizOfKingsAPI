using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;



namespace QuizOfKingsAPI.Models
{
    public class QuestionGroup
    {
        public string ID;
        public string Description;
        public string OpenGames;

        public static List<QuestionGroup> GetList()
        {
            List<QuestionGroup> Res = new List<QuestionGroup>();

            DataSet ds = new DataSet();
            SqlDataAdapter dapt = new SqlDataAdapter("[spGetQuestionGroup]", BaseObjects.connectionSTR);
            dapt.SelectCommand.CommandType = CommandType.StoredProcedure;
            dapt.Fill(ds);

            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    QuestionGroup Q = new QuestionGroup();
                    Q.ID = ds.Tables[0].Rows[i]["Id"].ToString();
                    Q.Description = ds.Tables[0].Rows[i]["Description"].ToString();
                    Q.OpenGames = ds.Tables[0].Rows[i]["OpenGames"].ToString();
                    Res.Add(Q);
                }
            }
            return Res;
        }
    }
    public class Question
    {
    }
}