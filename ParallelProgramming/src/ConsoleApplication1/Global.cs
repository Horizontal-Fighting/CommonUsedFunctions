using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    public class Global
    {

        //后台API服务器地址
        //private static String _port = "9000";
        private static String _server = "vautointerview.azurewebsites.net";

        //拼接出一个完整的后台API调用地址
        //如果传入一个http开头的绝对路径，则不做处理直接原样返回
        //如果传入一个相对路径，则自动补全完整url地址并返回
        public static string GetCompleteUrl(string url)
        {
            if (url.ToUpper().StartsWith("http"))
                return url;

            if (url[0] == '/')
                return "http://" + _server + url;
            //return _server;

            return url;
        }


        // Returns JSON string
        public static string Get(string url)
        {
            url = GetCompleteUrl(url);

            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                try
                {
                    request.Timeout = 600000;
                    WebResponse response = request.GetResponse();
                    using (Stream responseStream = response.GetResponseStream())
                    {
                        StreamReader reader = new StreamReader(responseStream, Encoding.UTF8);
                        return reader.ReadToEnd();
                    }
                }
                catch (WebException ex)
                {
                    WebResponse errorResponse = ex.Response;
                    using (Stream responseStream = errorResponse.GetResponseStream())
                    {
                        StreamReader reader = new StreamReader(responseStream, Encoding.GetEncoding("utf-8"));
                        String errorText = "*" + reader.ReadToEnd();
                        // log errorText
                        return errorText;
                    }
                }
            }
            catch (Exception ex)
            {
                //recordLog.WriteLogFile(ex.ToString());
                return "*" + ex.Message;
            }
        }


        public static async Task<string> GetAsync(string url)
        {
            url = GetCompleteUrl(url);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            WebResponse response = await request.GetResponseAsync();
            using (Stream responseStream = response.GetResponseStream())
            {
                StreamReader reader = new StreamReader(responseStream, Encoding.UTF8);
                return reader.ReadToEnd();
            }
        }


        // POST a JSON string
        public static string Post(string url, string jsonContent)
        {
            url = GetCompleteUrl(url);

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";

            System.Text.UTF8Encoding encoding = new System.Text.UTF8Encoding();
            Byte[] byteArray = encoding.GetBytes(jsonContent);

            request.ContentLength = byteArray.Length;
            request.ContentType = @"application/json";

            try
            {
                using (Stream dataStream = request.GetRequestStream())
                {
                    dataStream.Write(byteArray, 0, byteArray.Length);
                }
                try
                {
                    using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                    {
                        using (Stream responseStream = response.GetResponseStream())
                        {
                            StreamReader reader = new StreamReader(responseStream, Encoding.UTF8);
                            return reader.ReadToEnd();
                        }
                    }
                }
                catch (WebException ex)
                {
                    using (Stream responseStream = ex.Response.GetResponseStream())
                    {
                        StreamReader reader = new StreamReader(responseStream, Encoding.UTF8);
                        return "*" + reader.ReadToEnd();
                    }

                    throw;
                }
            }
            catch (Exception ex)
            {
                //recordLog.WriteLogFile(ex.ToString());
                return "*" + ex.Message;
            }
        }


        

    }
}
