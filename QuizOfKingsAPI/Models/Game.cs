using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data;
using System.Data.SqlClient;

namespace QuizOfKingsAPI.Models
{

    public class NextQuestionResult
    {
        public Question Question;
        public List<Answer> Answer;
    }

    public class SaveGameAnswerParam
    {
        public string GameID;
        public string UserID;
        public string QuestionID;
        public string AnswerID;
        public string ServiceKey;
    }
    public class GameParams
    {
        public string GameID;
        public string UserID;
        public string ServiceKey;
    }
    public class CreateGameParams
    {
        public string UserID;
        public string QuestionGroupID;
        public string PlayerCount;
        public string ServiceKey;
    }
    public class CreateGameResult
    {
        public string GameID;
        public string Message;
        public string ResponseCode;
    }

    public class JoinGameResult
    {
        public string GameID;
        public string Message;
        public string ResponseCode;
    }

    public class GameResult
    {
        public Game Game;
        public List<Gamer> Gamers;
        public string Message;
        public string ResponseCode;
    }

    public class GameStatus
    {
        public Game Game;
        public List<Gamer> Gamers;
        public string Message;
        public string ResponseCode;
    }

    public class Game
    {
        public string ID;
        public User WinnerUser;
        public QuestionGroup QuestionGroup;
        public string IsStarted;
        public string IsEnded;
        public string StartTime;
        public string EndTime;
        public string ExpireTime;
        public string PlayerCount;
        public string CreationTime;
        public string JoinedPlayerCount;
        //Done
        public static BaseObjects.GeneralResponse CancelGame(string GameID,string UserID)
        {
            BaseObjects.GeneralResponse Result = new BaseObjects.GeneralResponse();
            SqlCommand cm = new SqlCommand();
            SqlConnection cn = new SqlConnection(BaseObjects.connectionSTR);
            cm.Connection = cn;
            cm.CommandText = "[spCancelGame]";
            cm.CommandType = CommandType.StoredProcedure;
            cm.Parameters.Add("@GameID", SqlDbType.NVarChar, 100).Value = GameID;
            cm.Parameters.Add("@UserID", SqlDbType.NVarChar, 100).Value = UserID;
            cm.Parameters.Add("@Message", SqlDbType.NVarChar, 100).Direction = ParameterDirection.Output;
            cm.Parameters.Add("@ResponseCode", SqlDbType.NVarChar, 100).Direction = ParameterDirection.Output;
            cn.Open();
            cm.ExecuteNonQuery();
            Result.Message= cm.Parameters["@Message"].Value.ToString();
            Result.ResponseCode = cm.Parameters["@ResponseCode"].Value.ToString();
            cn.Close();
            cn.Dispose();
            cm.Dispose();

            return Result;
        }
        //Done
        public static CreateGameResult CreateGame(string UserID, string questionGroupID,string PlayerCount)
        {
            CreateGameResult Result = new CreateGameResult();
            SqlCommand cm = new SqlCommand();
            SqlConnection cn = new SqlConnection(BaseObjects.connectionSTR);
            cm.Connection = cn;
            cm.CommandText = "[spCreateGame]";
            cm.CommandType = CommandType.StoredProcedure;
            cm.Parameters.Add("@UserID", SqlDbType.NVarChar, 100).Value = UserID;
            cm.Parameters.Add("@QuestionGroupID", SqlDbType.NVarChar, 100).Value = questionGroupID;
            cm.Parameters.Add("@PlayerCount", SqlDbType.NVarChar, 100).Value = PlayerCount;
            cm.Parameters.Add("@GameID", SqlDbType.NVarChar,100).Direction = ParameterDirection.Output;
            cm.Parameters.Add("@Message", SqlDbType.NVarChar, 100).Direction = ParameterDirection.Output;
            cm.Parameters.Add("@ResponseCode", SqlDbType.NVarChar, 100).Direction = ParameterDirection.Output;
            cn.Open();
            cm.ExecuteNonQuery();
            Result.GameID = cm.Parameters["@GameID"].Value.ToString();
            Result.Message = cm.Parameters["@Message"].Value.ToString();
            Result.ResponseCode = cm.Parameters["@ResponseCode"].Value.ToString();
            cn.Close();
            cn.Dispose();
            cm.Dispose();
            return Result;
        }
        //Done
        public static GameResult GameResult(string GameID, string UserID)
        {

            GameResult Result = new GameResult();
            try
            {
            Result.Gamers = new List<Gamer>();
            DataSet ds = new DataSet();
            SqlDataAdapter dapt = new SqlDataAdapter("[spGetGameResult]", BaseObjects.connectionSTR);
            dapt.SelectCommand.CommandType = CommandType.StoredProcedure;
            dapt.SelectCommand.Parameters.Add("@GameID", SqlDbType.NVarChar, 100).Value = GameID;
            dapt.SelectCommand.Parameters.Add("@UserID", SqlDbType.NVarChar, 100).Value = UserID;
            dapt.SelectCommand.Parameters.Add("@Message", SqlDbType.NVarChar, 100).Direction = ParameterDirection.Output;
            dapt.SelectCommand.Parameters.Add("@ResponseCode", SqlDbType.NVarChar, 100).Direction = ParameterDirection.Output;
            dapt.Fill(ds);

            Result.Message = dapt.SelectCommand.Parameters["@Message"].Value.ToString();
            Result.ResponseCode = dapt.SelectCommand.Parameters["@ResponseCode"].Value.ToString();

                foreach (DataRow R in ds.Tables[0].Rows)
            {
                Result.Game = new Game();
                Result.Game.QuestionGroup = new QuestionGroup();
                Result.Game.WinnerUser = new User();
                Result.Game.ID = R["GameID"].ToString();
                Result.Game.QuestionGroup.ID = R["QuestionGroupID"].ToString();
                Result.Game.QuestionGroup.Description = R["QuestionGroupDescription"].ToString();
                Result.Game.PlayerCount = R["PlayerCount"].ToString();
                Result.Game.WinnerUser.ID = R["WinnerUserID"].ToString();
                Result.Game.WinnerUser.Name = R["WinnerUserName"].ToString();
            }

            foreach (DataRow R in ds.Tables[1].Rows)
            {
                Gamer G = new Gamer();
                G.ID = R["UserID"].ToString();
                G.Name = R["UserName"].ToString();
                G.UserScore = R["UserScore"].ToString();
                G.IsWinner = R["IsWinner"].ToString();
                Result.Gamers.Add(G);
            }

            }
            catch(Exception ex)
            {

            }
            return Result;
        }
        //Done
            public static GameStatus GameStatus(string GameID, string UserID)
            {

            GameStatus Result = new GameStatus();
            try
            {
            DataSet ds = new DataSet();
            SqlDataAdapter dapt = new SqlDataAdapter("[spGetGameStatus]", BaseObjects.connectionSTR);
            dapt.SelectCommand.CommandType = CommandType.StoredProcedure;
            dapt.SelectCommand.Parameters.Add("@GameID", SqlDbType.NVarChar, 100).Value = GameID;
            dapt.SelectCommand.Parameters.Add("@UserID", SqlDbType.NVarChar, 100).Value = UserID;
            dapt.SelectCommand.Parameters.Add("@Message", SqlDbType.NVarChar, 100).Direction = ParameterDirection.Output;
            dapt.SelectCommand.Parameters.Add("@ResponseCode", SqlDbType.NVarChar, 100).Direction = ParameterDirection.Output;
            dapt.Fill(ds);

            Result.Message = dapt.SelectCommand.Parameters["@Message"].Value.ToString();
            Result.ResponseCode = dapt.SelectCommand.Parameters["@ResponseCode"].Value.ToString();

            foreach (DataRow R in ds.Tables[0].Rows)
            {
                Result.Game = new Game();
                Result.Game.QuestionGroup = new QuestionGroup();
                Result.Game.WinnerUser = new User();
                Result.Game.ID = R["GameID"].ToString();
                Result.Game.QuestionGroup.ID = R["QuestionGroupID"].ToString();
                Result.Game.QuestionGroup.Description = R["QuestionGroupDescription"].ToString();
                Result.Game.PlayerCount = R["PlayerCount"].ToString();
                Result.Game.WinnerUser.ID = R["WinnerUserID"].ToString();
                Result.Game.WinnerUser.Name = R["WinnerUserName"].ToString();
                Result.Game.IsEnded = R["GameIsEnded"].ToString();
                Result.Game.IsStarted = R["GameIsStarted"].ToString();
                Result.Game.CreationTime = R["CreationTime"].ToString();
                Result.Game.EndTime = R["GameEndTime"].ToString();
                Result.Game.JoinedPlayerCount = R["joindplayercount"].ToString();
                    
            }

            foreach (DataRow R in ds.Tables[1].Rows)
            {
                Gamer G = new Gamer();
                G.ID = R["UserID"].ToString();
                G.Name = R["UserName"].ToString();
                G.UserScore = R["UserScore"].ToString();
                G.IsWinner = R["IsWinner"].ToString();
                G.JoinTime = R["JoinTime"].ToString();
                G.RedinessTime = R["RedinessTime"].ToString();
                G.UserStartTime = R["UserStartTime"].ToString();
                G.UserEndTime = R["UserEndTime"].ToString();
                Result.Gamers.Add(G);
            }
            }
            catch(Exception ex)
            { }
            return Result;

        }

        //Done
        public static string NextQuestion(string GameID, string UserID)
        {
            BaseObjects.GeneralResponse Result = new BaseObjects.GeneralResponse();

            NextQuestionResult Response = new NextQuestionResult();
            Response.Question = new Question();
            Response.Question.CurrectAnswer = new Answer();
            Response.Answer = new List<Answer>();

            try
            { 
                DataSet ds = new DataSet();
                SqlDataAdapter dapt = new SqlDataAdapter("[spGetNextQuestion]", BaseObjects.connectionSTR);
                dapt.SelectCommand.CommandType = CommandType.StoredProcedure;
                dapt.SelectCommand.Parameters.Add("@GameID", SqlDbType.NVarChar, 100).Value = GameID;
                dapt.SelectCommand.Parameters.Add("@UserID", SqlDbType.NVarChar, 100).Value = UserID;
                dapt.SelectCommand.Parameters.Add("@Message", SqlDbType.NVarChar, 100).Direction = ParameterDirection.Output;
                dapt.SelectCommand.Parameters.Add("@ResponseCode", SqlDbType.NVarChar, 100).Direction = ParameterDirection.Output;
                dapt.Fill(ds);

                Result.Message = dapt.SelectCommand.Parameters["@Message"].Value.ToString();
                Result.ResponseCode = dapt.SelectCommand.Parameters["@ResponseCode"].Value.ToString();

                if (ds.Tables[0].Rows.Count > 0)
            {

                    Response.Question.ID = ds.Tables[0].Rows[0]["QuestionID"].ToString();
                    Response.Question.Description = ds.Tables[0].Rows[0]["QuestionDescription"].ToString();
                    Response.Question.CurrectAnswer.ID = ds.Tables[0].Rows[0]["CorrectAnswerID"].ToString();
                    Response.Question.CurrectAnswer.Description = ds.Tables[0].Rows[0]["CorrectAnswerDescription"].ToString();
                    Response.Question.QuestionTime = ds.Tables[0].Rows[0]["QuestionTime"].ToString();

            }

            if (ds.Tables[1].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                {
                    Answer a = new Answer();
                    a.ID = ds.Tables[1].Rows[i]["AnswerID"].ToString();
                    a.OrderNo = ds.Tables[1].Rows[i]["OrderNo"].ToString();
                    a.OrderText = ds.Tables[1].Rows[i]["OrderText"].ToString();
                    a.Description = ds.Tables[1].Rows[i]["AnswerDescription"].ToString();
                        Response.Answer.Add(a);
                }
            }
                return Newtonsoft.Json.JsonConvert.SerializeObject(Response);
            }
            catch(Exception ex)
            {
                return Newtonsoft.Json.JsonConvert.SerializeObject(Result);
            }

        }

        public static JoinGameResult JoinGame(string GameID, string UserID)
        {
            JoinGameResult J = new JoinGameResult();
            SqlCommand cm = new SqlCommand();
            SqlConnection cn = new SqlConnection(BaseObjects.connectionSTR);
            cm.Connection = cn;
            cm.CommandText = "[spJoinGame]";
            cm.CommandType = CommandType.StoredProcedure;
            cm.Parameters.Add("@GameID", SqlDbType.NVarChar, 100).Value = GameID;
            cm.Parameters.Add("@UserID", SqlDbType.NVarChar, 100).Value = UserID;
            cm.Parameters.Add("@Message", SqlDbType.NVarChar, 100).Direction = ParameterDirection.Output;
            cm.Parameters.Add("@ResponseCode", SqlDbType.NVarChar, 100).Direction = ParameterDirection.Output;
            cn.Open();
            cm.ExecuteNonQuery();
            J.Message= cm.Parameters["@Message"].Value.ToString();
            J.ResponseCode = cm.Parameters["@ResponseCode"].Value.ToString();
            cn.Close();
            cn.Dispose();
            cm.Dispose();

            return J;
        }

        //Done
        public static BaseObjects.GeneralResponse SaveGameUserAnswer(string GameID, string UserID, string QuestionID, string AnswerID)
        {
            BaseObjects.GeneralResponse Result = new BaseObjects.GeneralResponse();
            SqlCommand cm = new SqlCommand();
            SqlConnection cn = new SqlConnection(BaseObjects.connectionSTR);
            cm.Connection = cn;
            cm.CommandText = "[spSaveGameUserAnswer]";
            cm.CommandType = CommandType.StoredProcedure;
            cm.Parameters.Add("@GameID", SqlDbType.NVarChar, 100).Value = GameID;
            cm.Parameters.Add("@UserID", SqlDbType.NVarChar, 100).Value = UserID;
            cm.Parameters.Add("@QuestionID", SqlDbType.NVarChar, 100).Value = QuestionID;
            cm.Parameters.Add("@AnswerID", SqlDbType.NVarChar, 100).Value = AnswerID;
            cm.Parameters.Add("@Message", SqlDbType.NVarChar, 100).Direction = ParameterDirection.Output;
            cm.Parameters.Add("@ResponseCode", SqlDbType.NVarChar, 100).Direction = ParameterDirection.Output;
            cn.Open();
            cm.ExecuteNonQuery();
            Result.Message = cm.Parameters["@Message"].Value.ToString();
            Result.ResponseCode = cm.Parameters["@ResponseCode"].Value.ToString();
            cn.Close();
            cn.Dispose();
            cm.Dispose();
            return Result;
        }


        public static BaseObjects.GeneralResponse UserReadiness(string GameID, string UserID)
        {
            BaseObjects.GeneralResponse Result = new BaseObjects.GeneralResponse();
            SqlCommand cm = new SqlCommand();
            SqlConnection cn = new SqlConnection(BaseObjects.connectionSTR);
            cm.Connection = cn;
            cm.CommandText = "[spSetUserReadiness]";
            cm.CommandType = CommandType.StoredProcedure;
            cm.Parameters.Add("@GameID", SqlDbType.NVarChar, 100).Value = GameID;
            cm.Parameters.Add("@UserID", SqlDbType.NVarChar, 100).Value = UserID;
            cm.Parameters.Add("@Message", SqlDbType.NVarChar, 100).Direction = ParameterDirection.Output;
            cm.Parameters.Add("@ResponseCode", SqlDbType.NVarChar, 100).Direction = ParameterDirection.Output;
            cn.Open();
            cm.ExecuteNonQuery();
            Result.Message = cm.Parameters["@Message"].Value.ToString();
            Result.ResponseCode = cm.Parameters["@ResponseCode"].Value.ToString();
            cn.Close();
            cn.Dispose();
            cm.Dispose();
            return Result;
        }

        public static BaseObjects.GeneralResponse StartGame(string GameID)
        {
            BaseObjects.GeneralResponse Result = new BaseObjects.GeneralResponse();
            SqlCommand cm = new SqlCommand();
            SqlConnection cn = new SqlConnection(BaseObjects.connectionSTR);
            cm.Connection = cn;
            cm.CommandText = "[spStartGame]";
            cm.CommandType = CommandType.StoredProcedure;
            cm.Parameters.Add("@GameID", SqlDbType.NVarChar, 100).Value = GameID;
            cm.Parameters.Add("@Message", SqlDbType.NVarChar, 100).Direction = ParameterDirection.Output;
            cm.Parameters.Add("@ResponseCode", SqlDbType.NVarChar, 100).Direction = ParameterDirection.Output;
            cn.Open();
            cm.ExecuteNonQuery();
            Result.Message = cm.Parameters["@Message"].Value.ToString();
            Result.ResponseCode = cm.Parameters["@ResponseCode"].Value.ToString();
            cn.Close();
            cn.Dispose();
            cm.Dispose();
            return Result;
        }


    }
}