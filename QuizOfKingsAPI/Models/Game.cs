using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data;
using System.Data.SqlClient;

namespace QuizOfKingsAPI.Models
{
    public class SaveGameAnswerParam
    {
        public string GameID;
        public string UserID;
        public string QuestionID;
        public string AnswerID;
        public string UserGUID;
        public string ServiceKey;
    }
    public class GameParams
    {
        public string GameID;
        public string UserGUID;
        public string ServiceKey;
    }
    public class CreateGameParams
    {
        public string HostUserID;
        public string QuestionGroupID;
        public string UserGUID;
        public string ServiceKey;
    }
    public class Game
    {
        public string ID;
        public User HostUserID;
        public User GuestUserID;
        public QuestionGroup QuestionGroupID;
        public string IsStarted;
        public string IsFinished;
        public User WinnerUserID;
        public string StartDateTime;
        public string HostUserScore;
        public string GuestUserScore;

        public void CreateGame(string hostUserID, string questionGroupID)
        {
            HostUserID = new User();
            QuestionGroupID = new QuestionGroup();
         SqlCommand cm = new SqlCommand();
        SqlConnection cn = new SqlConnection(BaseObjects.connectionSTR);
        cm.Connection = cn;
            cm.CommandText = "[spCreateGame]";
            cm.CommandType = CommandType.StoredProcedure;
            cm.Parameters.Add("@hostUserID", SqlDbType.NVarChar, 100).Value = hostUserID;
            cm.Parameters.Add("@questionGroupID", SqlDbType.NVarChar, 100).Value = questionGroupID;
            cm.Parameters.Add("@ID", SqlDbType.NVarChar,100).Direction = ParameterDirection.Output;
            cn.Open();
            cm.ExecuteNonQuery();
            ID = cm.Parameters["@ID"].Value.ToString();
            HostUserID.ID = hostUserID;
            QuestionGroupID.ID = questionGroupID;
            cn.Close();
            cn.Dispose();
            cm.Dispose();
        }

        public void JoinGame(string GameID, string GuestUserID)
        {
            SqlCommand cm = new SqlCommand();
            SqlConnection cn = new SqlConnection(BaseObjects.connectionSTR);
            cm.Connection = cn;
            cm.CommandText = "[spJoinGame]";
            cm.CommandType = CommandType.StoredProcedure;
            cm.Parameters.Add("@GameID", SqlDbType.NVarChar, 100).Value = GameID;
            cm.Parameters.Add("@GuestUserID", SqlDbType.NVarChar, 100).Value = GuestUserID;
            cn.Open();
            cm.ExecuteNonQuery();
            cn.Close();
            cn.Dispose();
            cm.Dispose();
        }

        public string GetNextQuestion(string GameID)
        {
            Question Q = new Question();
            List<Answer> A = new List<Answer>();

            DataSet ds = new DataSet();
            SqlDataAdapter dapt = new SqlDataAdapter("[spGetNextQuestion]", BaseObjects.connectionSTR);
            dapt.SelectCommand.CommandType = CommandType.StoredProcedure;
            dapt.SelectCommand.Parameters.Add("@GameID", SqlDbType.NVarChar, 100).Value = GameID;
            dapt.Fill(ds);

            if (ds.Tables[0].Rows.Count > 0)
            {
                
                Q.ID = ds.Tables[0].Rows[0]["Id"].ToString();
                Q.CurrectAnswer.ID = ds.Tables[0].Rows[0]["CurrectAnswerID"].ToString();
                Q.Description = ds.Tables[0].Rows[0]["Description"].ToString();
            }

            if (ds.Tables[1].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    Answer a = new Answer();
                    a.ID = ds.Tables[0].Rows[i]["Id"].ToString();
                    a.OrderNo = ds.Tables[0].Rows[i]["OrderNo"].ToString();
                    a.OrderText = ds.Tables[0].Rows[i]["OrderText"].ToString();
                    a.Description = ds.Tables[0].Rows[i]["Description"].ToString();
                    A.Add(a);
                }
            }

            Newtonsoft.Json.Linq.JArray jr = Newtonsoft.Json.Linq.JArray.FromObject(Q);
            Newtonsoft.Json.Linq.JArray jr1 = Newtonsoft.Json.Linq.JArray.FromObject(A);
            Newtonsoft.Json.Linq.JArray jArray = new Newtonsoft.Json.Linq.JArray();
            jArray.Add(jr);
            jArray.Add(jr1);

            return Newtonsoft.Json.JsonConvert.SerializeObject(jArray);


        }

        public void SaveGameAnswer(string GameID, string UserID, string QuestionID, string AnswerID)
        {
            SqlCommand cm = new SqlCommand();
            SqlConnection cn = new SqlConnection(BaseObjects.connectionSTR);
            cm.Connection = cn;
            cm.CommandText = "[SaveGameAnswer]";
            cm.CommandType = CommandType.StoredProcedure;
            cm.Parameters.Add("@GameID", SqlDbType.NVarChar, 100).Value = GameID;
            cm.Parameters.Add("@UserID", SqlDbType.NVarChar, 100).Value = GuestUserID;
            cm.Parameters.Add("@QuestionID", SqlDbType.NVarChar, 100).Value = GameID;
            cm.Parameters.Add("@AnswerID", SqlDbType.NVarChar, 100).Value = GameID;
            cn.Open();
            cm.ExecuteNonQuery();
            cn.Close();
            cn.Dispose();
            cm.Dispose();
        }
        public void GetGameStatus(string GameID, string UserID)
        {
            HostUserID = new User();
            GuestUserID = new User();
            QuestionGroupID = new QuestionGroup();
            WinnerUserID = new User();


            DataSet ds = new DataSet();
            SqlDataAdapter dapt = new SqlDataAdapter("[spGetGameStatus]", BaseObjects.connectionSTR);
            dapt.SelectCommand.CommandType = CommandType.StoredProcedure;
            dapt.SelectCommand.Parameters.Add("@GameID", SqlDbType.NVarChar, 100).Value = GameID;
            dapt.Fill(ds);

            if (ds.Tables[0].Rows.Count > 0)
            {
                ID = ds.Tables[0].Rows[0]["Id"].ToString(); 
                HostUserID.ID = ds.Tables[0].Rows[0]["HostUserID"].ToString(); 
                GuestUserID.ID = ds.Tables[0].Rows[0]["GuestUserID"].ToString(); 
                QuestionGroupID.ID = ds.Tables[0].Rows[0]["QuestionGroupID"].ToString(); 
                IsStarted = ds.Tables[0].Rows[0]["IsStarted"].ToString(); 
                IsFinished = ds.Tables[0].Rows[0]["IsFinished"].ToString(); 
                WinnerUserID.ID = ds.Tables[0].Rows[0]["WinnerUserID"].ToString(); 
                StartDateTime = ds.Tables[0].Rows[0]["StartDateTime"].ToString();
                HostUserScore = ds.Tables[0].Rows[0]["HostUserScore"].ToString();
                GuestUserScore = ds.Tables[0].Rows[0]["GuestUserScore"].ToString();
            }
        }

    }
}