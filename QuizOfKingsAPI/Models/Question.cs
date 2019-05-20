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
        public Game OpenGames;
        //Done
        public static string ListWithGamesStatus(string UserID)
        {
            BaseObjects.GeneralResponse Result = new BaseObjects.GeneralResponse();
            List<QuestionGroupResult> Res = new List<QuestionGroupResult>();
            try
            { 
            DataSet ds = new DataSet();
            SqlDataAdapter dapt = new SqlDataAdapter("[spGetQuestionGroupListWithGamesStatus]", BaseObjects.connectionSTR);
            dapt.SelectCommand.Parameters.Add("@UserID", SqlDbType.NVarChar, 100).Value = UserID;
            dapt.SelectCommand.Parameters.Add("@Message", SqlDbType.NVarChar, 100).Direction = ParameterDirection.Output;
            dapt.SelectCommand.Parameters.Add("@ResponseCode", SqlDbType.NVarChar, 100).Direction = ParameterDirection.Output;
            dapt.SelectCommand.CommandType = CommandType.StoredProcedure;
            dapt.Fill(ds);

            Result.Message = dapt.SelectCommand.Parameters["@Message"].Value.ToString();
            Result.ResponseCode = dapt.SelectCommand.Parameters["@ResponseCode"].Value.ToString();

           if (ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    QuestionGroupResult Q = new QuestionGroupResult();
                    Q.ID = ds.Tables[0].Rows[i]["QuestionGroupID"].ToString();
                    Q.Description = ds.Tables[0].Rows[i]["QuestionGroupDescription"].ToString();
                    Q.CountOfClosedGame = ds.Tables[0].Rows[i]["CountOfClosedGame"].ToString();
                    Q.CountOfOpenGame = ds.Tables[0].Rows[i]["CountOfOpenGame"].ToString();
                    Res.Add(Q);
                }
            }

            return Newtonsoft.Json.JsonConvert.SerializeObject(Res);
            }
            catch(Exception ex)
            {
                return Newtonsoft.Json.JsonConvert.SerializeObject(Result);
            }
            
        }

        //Done
        public static List<Game> OnLineGames(string UserID,string QuestionGroupID)
        {
            List<Game> Res = new List<Game>();

            DataSet ds = new DataSet();
            SqlDataAdapter dapt = new SqlDataAdapter("[spGetQuestionGroupOnLineGames]", BaseObjects.connectionSTR);
            dapt.SelectCommand.Parameters.Add("@UserID", SqlDbType.NVarChar, 100).Value = UserID;
            dapt.SelectCommand.Parameters.Add("@QuestionGroupID", SqlDbType.NVarChar, 100).Value = QuestionGroupID;
            dapt.SelectCommand.Parameters.Add("@Message", SqlDbType.NVarChar, 100).Direction = ParameterDirection.Output;
            dapt.SelectCommand.Parameters.Add("@ResponseCode", SqlDbType.NVarChar, 100).Direction = ParameterDirection.Output;
            dapt.SelectCommand.CommandType = CommandType.StoredProcedure;
            dapt.Fill(ds);

            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    Game Q = new Game();
                    Q.ID= ds.Tables[0].Rows[i]["GameId"].ToString();
                    Q.PlayerCount = ds.Tables[0].Rows[i]["PlayerCount"].ToString();
                    Q.CreationTime = ds.Tables[0].Rows[i]["CreationTime"].ToString();
                    Q.JoinedPlayerCount = ds.Tables[0].Rows[i]["JoinedPlayerCount"].ToString();
                    Res.Add(Q);
                }
            }
            return Res;
        }

    }
    public class Question
    {
        public string ID;
        public string Description;
        public string QuestionTime;
        public Answer CurrectAnswer;
        public QuestionGroup QuestionGroup = new QuestionGroup();
    }

    public class QuestionGroupResult
    {
        public string ID;
        public string Description;
        public string CountOfClosedGame;
        public string CountOfOpenGame;
    }

    public class OnlineQuestionParam
    {
        public string UserID;
        public string QuestionGroupID;
        public string ServiceKey;
    }

}