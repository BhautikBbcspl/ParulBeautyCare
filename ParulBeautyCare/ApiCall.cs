using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace ParulBeautyCare
{
    public static class ApiCall
    {
        public static string GetApi(string ApiUrl)
        {
            var responseString = "";
            var request = (HttpWebRequest)WebRequest.Create(ApiUrl);
            request.Method = "GET";
            request.ContentType = "application/json";

            using (var response1 = request.GetResponse())
            {
                using (var reader = new StreamReader(response1.GetResponseStream()))
                {
                    responseString = reader.ReadToEnd();
                }
            }
            return responseString;
        }

        public static string PostApi(string ApiUrl, string model)
        {
            WebRequest tRequest = WebRequest.Create(ConfigurationManager.AppSettings["BaseUrl"] + ApiUrl);
            tRequest.UseDefaultCredentials = true;
            tRequest.Method = "Post";
            tRequest.ContentType = "application/json";
            Byte[] byteArray = Encoding.UTF8.GetBytes(model);
            tRequest.ContentLength = byteArray.Length;
            using (Stream dataStream = tRequest.GetRequestStream())
            {
                dataStream.Write(byteArray, 0, byteArray.Length);
                using (WebResponse tResponse = tRequest.GetResponse())
                {
                    using (Stream dataStreamResponse = tResponse.GetResponseStream())
                    {
                        using (StreamReader tReader = new StreamReader(dataStreamResponse))
                        {
                            var response1 = (HttpWebResponse)tRequest.GetResponse();
                            var responseString1 = new StreamReader(response1.GetResponseStream()).ReadToEnd();
                            return responseString1;
                        }
                    }
                }
            }
        }
    }

}