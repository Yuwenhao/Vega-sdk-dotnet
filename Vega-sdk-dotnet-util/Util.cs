using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Vega_sdk_dotnet_util
{
    public class Util
    {
        public static string ToBase64hmac(string strText, string strKey)
        {

            HMACSHA1 myHMACSHA1 = new HMACSHA1(Encoding.UTF8.GetBytes(strKey), false);

            byte[] byteText = myHMACSHA1.ComputeHash(Encoding.UTF8.GetBytes(strText));

            return System.Convert.ToBase64String(byteText);

        }

        public static string sendGet(string url, Dictionary<string, string> param)
        {
            //组合参数
            string parameters = "";
            if (param != null)
            {
                foreach (KeyValuePair<string, string> kv in param)
                {
                    parameters += parameters == "" ? "" : "&";
                    parameters += kv.Key + "=" + kv.Value;
                }
            }
            //拼接get参数
            url += parameters == "" ? "" : "?" + parameters;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.ContentType = "application/x-www-form-urlencoded";
            //请求接口并获得返回值
            HttpWebResponse response;
            try
            {
                response = (HttpWebResponse)request.GetResponse();
            }
            catch (WebException ex)
            {
                //返回错误信息
                response = (HttpWebResponse)ex.Response;
            }
            string encoding = response.ContentEncoding;
            if (encoding == null || encoding.Length < 1)
            {
                encoding = "UTF-8";
            }
            StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding(encoding));
            return reader.ReadToEnd();
        }

        /// <summary>
        /// post请求
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="param">请求参数</param>
        public static string sendPostUTF8(string url, Dictionary<string, object> param)
        {
            //组合参数
            string parameters = "";
            if (param != null)
            {
                foreach (KeyValuePair<string, object> kv in param)
                {
                    parameters += parameters == "" ? "" : "&";
                    parameters += kv.Key + "=" + kv.Value.ToString();
                }
            }
            byte[] byteArray = Encoding.UTF8.GetBytes(parameters); //转化
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            request.Accept = "application/json";
            request.ContentType = "application/x-www-form-urlencoded";
            //传入post参数
            request.ContentLength = byteArray.Length;
            using (Stream reqStream = request.GetRequestStream())
            {
                reqStream.Write(byteArray, 0, byteArray.Length);
            }
            //请求接口并获得返回值
            HttpWebResponse response;
            try
            {
                response = (HttpWebResponse)request.GetResponse();
            }
            catch (WebException ex)
            {
                //返回错误信息
                response = (HttpWebResponse)ex.Response;
            }
            string encoding = response.ContentEncoding;
            if (encoding == null || encoding.Length < 1)
            {
                encoding = "UTF-8";
            }
            StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding(encoding));
            return reader.ReadToEnd();
        }

        /// <summary>
        /// 获取时间戳
        /// </summary>
        /// <returns></returns>
        public static string GetTimeStamp(System.DateTime time)
        {
            long ts = ConvertDateTimeToInt(time);
            return ts.ToString();
        }
        /// <summary>  
        /// 将c# DateTime时间格式转换为Unix时间戳格式  
        /// </summary>  
        /// <param name="time">时间</param>  
        /// <returns>long</returns>  
        public static long ConvertDateTimeToInt(System.DateTime time)
        {
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1, 0, 0, 0, 0));
            long t = (time.Ticks - startTime.Ticks) / 10000;   //除10000调整为13位      
            return t;
        }
        /// <summary>        
        /// 时间戳转为C#格式时间        
        /// </summary>        
        /// <param name=”timeStamp”></param>        
        /// <returns></returns>        
        private DateTime ConvertStringToDateTime(string timeStamp)
        {
            DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            long lTime = long.Parse(timeStamp + "0000");
            TimeSpan toNow = new TimeSpan(lTime);
            return dtStart.Add(toNow);
        }

    }
}
