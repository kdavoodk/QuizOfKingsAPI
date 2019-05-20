using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace QuizOfKingsAPI.Models
{
    public class LoginParams
    {
        public string Mobile;
        public string Password;
        public string ServiceKey;
    }

    public class ResetPasswordParams
    {
        public string Mobile;
        public string NewPassword;
        public string SecurityQuestionAnswer;
        public string ServiceKey;
    }
    public class RegisterParams
    {
        public string Name;
        public string Mobile;
        public string Password;
        public string SecurityQuestionID;
        public string SecurityQuestionAnswer;
        public string ServiceKey;
    }

    public class UserResultParams
    {
        public string ID;
        public string Name;
        public string Mobile;
        public string GUID;
        public string Message;
        public string ResponseCode;
    }

    public class SecurityQuestion
    {
        public string ID;
        public string Description;
    }

    public class Gamer
    {
        public string ID;
        public string Name;
        public string UserScore;
        public string IsWinner;
        public string JoinTime;
        public string RedinessTime;
        public string UserStartTime;
        public string UserEndTime;
    }

    public class User
    {
        public string ID;
        public string Mobile;
        public string Name;
        public string GUID;
        //Done
        public UserResultParams login(string mobile , string password)
        {
            UserResultParams Res = new UserResultParams();
            SqlCommand cm = new SqlCommand();
            SqlConnection cn = new SqlConnection(BaseObjects.connectionSTR);
            cm.Connection = cn;
            cm.CommandText = "[spLogin]";
            cm.CommandType = CommandType.StoredProcedure;
            cm.Parameters.Add("@MobileNumber", SqlDbType.NVarChar, 100).Value = mobile;
            cm.Parameters.Add("@password", SqlDbType.NVarChar, 100).Value = password;
            cm.Parameters.Add("@UserGUID", SqlDbType.NVarChar,100).Direction = ParameterDirection.Output;
            cm.Parameters.Add("@UserName", SqlDbType.NVarChar, 100).Direction = ParameterDirection.Output;
            cm.Parameters.Add("@UserID", SqlDbType.NVarChar, 100).Direction = ParameterDirection.Output;
            cm.Parameters.Add("@Message", SqlDbType.NVarChar, 100).Direction = ParameterDirection.Output;
            cm.Parameters.Add("@ResponseCode", SqlDbType.NVarChar, 100).Direction = ParameterDirection.Output;

            cn.Open();
            cm.ExecuteNonQuery();
            Res.Mobile = mobile;
            Res.GUID = cm.Parameters["@UserGUID"].Value.ToString();
            Res.Name = cm.Parameters["@UserName"].Value.ToString();
            Res.ID = cm.Parameters["@UserID"].Value.ToString();
            Res.Message = cm.Parameters["@Message"].Value.ToString();
            Res.ResponseCode = cm.Parameters["@ResponseCode"].Value.ToString();
            cn.Close();
            cn.Dispose();
            cm.Dispose();

            return Res;
        }
        //Done
        public UserResultParams register(string mobile, string password,string name, string SecurityQuestionID, string SecurityQuestionAnswer)
        {
            UserResultParams Res = new UserResultParams();
            try
            {

                SqlCommand cm = new SqlCommand();
                SqlConnection cn = new SqlConnection(BaseObjects.connectionSTR);
                cm.Connection = cn;
                cm.CommandText = "[spRegister]";
                cm.CommandType = CommandType.StoredProcedure;
                cm.Parameters.Add("@UserName", SqlDbType.NVarChar, 20).Value = name;
                cm.Parameters.Add("@MobileNumber", SqlDbType.Char, 11).Value = mobile;
                cm.Parameters.Add("@password", SqlDbType.NVarChar, 20).Value = password;
                cm.Parameters.Add("@SecurityQuestionID", SqlDbType.TinyInt).Value = SecurityQuestionID;
                cm.Parameters.Add("@SecurityQuestionAnswer", SqlDbType.NVarChar, 255).Value = SecurityQuestionAnswer;
                cm.Parameters.Add("@UserGUID", SqlDbType.UniqueIdentifier).Direction = ParameterDirection.Output;
                cm.Parameters.Add("@UserID", SqlDbType.Int).Direction = ParameterDirection.Output;
                cm.Parameters.Add("@Message", SqlDbType.NVarChar, 4000).Direction = ParameterDirection.Output;
                cm.Parameters.Add("@ResponseCode", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                cn.Open();
                cm.ExecuteNonQuery();
                Res.Mobile = mobile;
                Res.GUID = cm.Parameters["@UserGUID"].Value.ToString();
                Res.ID = cm.Parameters["@UserID"].Value.ToString();
                Res.Name = name;
                Res.Message = cm.Parameters["@Message"].Value.ToString();
                Res.ResponseCode = cm.Parameters["@ResponseCode"].Value.ToString();
                cn.Close();
                cn.Dispose();
                cm.Dispose();

            }
            catch (Exception ex)
            {
                return Res;
            }
            return Res;

        }
        //Done
        public List<SecurityQuestion> SecurityQuestionList()
        {
            List<SecurityQuestion> SecurityQ = new List<SecurityQuestion>();
            DataSet ds = new DataSet();
            SqlDataAdapter dapt = new SqlDataAdapter("[spGetSecurityQuestionList]", BaseObjects.connectionSTR);
            dapt.SelectCommand.CommandType = CommandType.StoredProcedure;
            dapt.SelectCommand.Parameters.Add("@Message", SqlDbType.NVarChar, 100).Direction = ParameterDirection.Output;
            dapt.SelectCommand.Parameters.Add("@ResponseCode", SqlDbType.NVarChar, 100).Direction = ParameterDirection.Output;
            dapt.Fill(ds);


            foreach(DataRow R in ds.Tables[0].Rows)
            { 
                SecurityQuestion Q = new SecurityQuestion();
                Q.ID =R["SecurityQuestionID"].ToString();
                Q.Description = R["SecurityQuestionDescription"].ToString();
                SecurityQ.Add(Q);
            }
            return SecurityQ;
        }

        //Done
        public UserResultParams ResetUserPassword(string MobileNumber, string SecurityQuestionAnswer,string NewPassword)
        {
            UserResultParams Res = new UserResultParams();
            SqlCommand cm = new SqlCommand();
            SqlConnection cn = new SqlConnection(BaseObjects.connectionSTR);
            cm.Connection = cn;
            cm.CommandText = "[spResetUserPassword]";
            cm.CommandType = CommandType.StoredProcedure;

            cm.Parameters.Add("@MobileNumber", SqlDbType.NVarChar, 100).Value = MobileNumber;
            cm.Parameters.Add("@SecurityQuestionAnswer", SqlDbType.NVarChar, 100).Value = SecurityQuestionAnswer;
            cm.Parameters.Add("@NewPassword", SqlDbType.NVarChar, 100).Value = NewPassword;
            cm.Parameters.Add("@Message", SqlDbType.NVarChar, 100).Direction = ParameterDirection.Output;
            cm.Parameters.Add("@ResponseCode", SqlDbType.NVarChar, 100).Direction = ParameterDirection.Output;
            cn.Open();
            cm.ExecuteNonQuery();
            Res.Message= cm.Parameters["@Message"].Value.ToString();
            Res.ResponseCode = cm.Parameters["@ResponseCode"].Value.ToString();
            cn.Close();
            cn.Dispose();
            cm.Dispose();
            return Res;
        }
    }
}