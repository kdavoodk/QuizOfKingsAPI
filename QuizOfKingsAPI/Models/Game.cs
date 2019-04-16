using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data;
using System.Data.SqlClient;

namespace QuizOfKingsAPI.Models
{
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
        public string HostUserID;
        public string GuestUserID;
        public string QuestionGroupID;
        public string IsStarted;
        public string IsFinished;
        public string WinnerUserID;
        public string StartDateTime;

        public void CreateGame(string hostUserID, string questionGroupID)
        {
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
            HostUserID = hostUserID;
            QuestionGroupID = questionGroupID;
            cn.Close();
            cn.Dispose();
            cm.Dispose();
        }

        public void GetGameStatus(string GameID, string GuestUserID)
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
            ID = cm.Parameters["@ID"].Value.ToString();
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
            ID = cm.Parameters["@ID"].Value.ToString();
            cn.Close();
            cn.Dispose();
            cm.Dispose();
        }

        public void StartGame(string GameID, string GuestUserID)
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
            ID = cm.Parameters["@ID"].Value.ToString();
            cn.Close();
            cn.Dispose();
            cm.Dispose();
        }

        public void GetNextQuestion(string GameID, string GuestUserID)
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
            ID = cm.Parameters["@ID"].Value.ToString();
            cn.Close();
            cn.Dispose();
            cm.Dispose();
        }

        public void SaveAnswer(string GameID, string GuestUserID)
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
            ID = cm.Parameters["@ID"].Value.ToString();
            cn.Close();
            cn.Dispose();
            cm.Dispose();
        }

        public void GetCorrectAnswer(string GameID, string GuestUserID)
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
            ID = cm.Parameters["@ID"].Value.ToString();
            cn.Close();
            cn.Dispose();
            cm.Dispose();
        }

        public void GameResult(string GameID, string GuestUserID)
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
            ID = cm.Parameters["@ID"].Value.ToString();
            cn.Close();
            cn.Dispose();
            cm.Dispose();
        }

        public void GetQuestionAnwser(string GameID, string GuestUserID)
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
            ID = cm.Parameters["@ID"].Value.ToString();
            cn.Close();
            cn.Dispose();
            cm.Dispose();
        }

    }
}