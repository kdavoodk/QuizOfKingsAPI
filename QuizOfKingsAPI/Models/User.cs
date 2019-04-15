
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

    public class User
    {
        public string Mobile;
        public string Name;
        public string GUID;
        public void login(string mobile , string password)
        {
            User Res = new User();
            SqlCommand cm = new SqlCommand();
            SqlConnection cn = new SqlConnection(BaseObjects.connectionSTR);
            cm.Connection = cn;
            cm.CommandText = "[spLogin]";
            cm.CommandType = CommandType.StoredProcedure;
            cm.Parameters.Add("@mobile", SqlDbType.NVarChar, 100).Value = mobile;
            cm.Parameters.Add("@password", SqlDbType.NVarChar, 100).Value = password;
            cm.Parameters.Add("@GUID", SqlDbType.NVarChar, 100).Direction = ParameterDirection.Output;
            cm.Parameters.Add("@Name", SqlDbType.NVarChar, 100).Direction = ParameterDirection.Output;
            cn.Open();
            cm.ExecuteNonQuery();
            GUID = cm.Parameters["@GUID"].Value.ToString();
            Name = cm.Parameters["@Name"].Value.ToString();
            cn.Close();
            cn.Dispose();
            cm.Dispose();
        }
    }
}