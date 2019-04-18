using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Net.Mail;
using System.IO;
using System.Text;
using System.IO.Compression;
using Newtonsoft;
using System.Net;
using System.Text.RegularExpressions;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Net.Http;
using System.Threading;
using System.Net.Http.Headers;
using System.Web.Http;

namespace QuizOfKingsAPI.Models
{
    public class RawJsonActionResult : IHttpActionResult
    {
        private readonly string _jsonString;

        public RawJsonActionResult(string jsonString)
        {
            _jsonString = jsonString;
        }

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            var content = new StringContent(_jsonString);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var response = new HttpResponseMessage(HttpStatusCode.OK) { Content = content };
            return Task.FromResult(response);
        }
    }

    public static class BaseObjects
    {
        public static string ServerName = Properties.Settings.Default.server;
        public static string DataBaseName = Properties.Settings.Default.database;
        public static string connectionSTR = "data source=" + BaseObjects.ServerName + ";initial catalog=" + BaseObjects.DataBaseName + ";user id=" + Properties.Settings.Default.user + ";password=" + Properties.Settings.Default.pass + "";
        public static string SERVICE_PASS = "kq";
        public static string SERVICE_PASS_STRONG = "h7y7%hd90!+";


        public class GeneralResponse
        {
            public string Message;
        }
        public class GeneralParams
        {
            public string UserGUID;
            public string ServiceKey;
        }

        public class AppVersionParams
        {
            public string Username;
            public string Password;
            public string ServiceKey;
        }
      
        public class VersionResult
        {
            public string AppVersion;
            public string DownloadLink;
        }
   

        public static void CopyTo(Stream src, Stream dest)
        {
            byte[] bytes = new byte[4096];

            int cnt;

            while ((cnt = src.Read(bytes, 0, bytes.Length)) != 0)
            {
                dest.Write(bytes, 0, cnt);
            }
        }
        public static byte[] Zip(string str)
        {
            var bytes = Encoding.UTF8.GetBytes(str);

            using (var msi = new MemoryStream(bytes))
            using (var mso = new MemoryStream())
            {
                using (var gs = new GZipStream(mso, CompressionMode.Compress))
                {
                    //msi.CopyTo(gs);
                    CopyTo(msi, gs);
                }

                return mso.ToArray();
            }
        }
        public static string Unzip(byte[] bytes)
        {
            using (var msi = new MemoryStream(bytes))
            using (var mso = new MemoryStream())
            {
                using (var gs = new GZipStream(msi, CompressionMode.Decompress))
                {
                    //gs.CopyTo(mso);
                    CopyTo(gs, mso);
                }

                return Encoding.UTF8.GetString(mso.ToArray());
            }
        }


    }
}
