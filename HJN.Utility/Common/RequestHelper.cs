using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Web;

namespace HJN.Utility.Common
{
    public class RequestHelper
    {
        /// <summary>
        /// 
        /// </summary>
        public static string GetScriptNameQueryString
        {
            get
            {
                return HttpContext.Current.Request.ServerVariables["QUERY_STRING"].ToString();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public static string GetScriptName
        {
            get
            {
                return HttpContext.Current.Request.ServerVariables["SCRIPT_NAME"].ToString();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public static string GetScriptUrl
        {
            get
            {
                return (RequestHelper.GetScriptNameQueryString == "") ? RequestHelper.GetScriptName : string.Format("{0}?{1}", RequestHelper.GetScriptName, RequestHelper.GetScriptNameQueryString);
            }
        }

        public static string GetScriptNameQuery
        {
            get
            {
                return HttpContext.Current.Request.Url.Query;
            }
        }

        [DllImport("wininet")]
        private static extern bool InternetGetConnectedState(out int connectionDescription, int reservedValue);

        public static bool IsConnectedInternet()
        {
            int i = 0;
            return RequestHelper.InternetGetConnectedState(out i, 0);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string UrlEncode(string str)
        {
            string result;
            if (string.IsNullOrEmpty(str))
            {
                result = "";
            }
            else
            {
                str = str.Replace("'", "");
                result = HttpContext.Current.Server.UrlEncode(str);
            }
            return result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string UrlDecode(string str)
        {
            string result;
            if (string.IsNullOrEmpty(str))
            {
                result = "";
            }
            else
            {
                result = HttpContext.Current.Server.UrlDecode(str);
            }
            return result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string GetClientIP()
        {
            string result = string.Empty;
            result = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (string.IsNullOrEmpty(result))
            {
                result = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            }
            if (string.IsNullOrEmpty(result))
            {
                result = HttpContext.Current.Request.UserHostAddress;
            }
            string result2;
            if (string.IsNullOrEmpty(result))
            {
                result2 = "127.0.0.1";
            }
            else
            {
                result2 = result;
            }
            return result2;
        }

        public static string BuildUrl(string url, string paramname, string paramval)
        {
            string result = url;
            if (result.IndexOf("?") > 0)
            {
                result += "&" + paramname + "=" + paramval;
            }
            else
            {
                result += "?" + paramname + "=" + paramval;
            }
            return result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static bool IsPost()
        {
            return HttpContext.Current.Request.HttpMethod.Equals("POST");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static bool IsGet()
        {
            return HttpContext.Current.Request.HttpMethod.Equals("GET");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="strName"></param>
        /// <returns></returns>
        public static string GetServerString(string strName)
        {
            string result;
            if (HttpContext.Current.Request.ServerVariables[strName] == null)
            {
                result = "";
            }
            else
            {
                result = HttpContext.Current.Request.ServerVariables[strName].ToString();
            }
            return result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string GetUrlReferrer()
        {
            string retVal = null;
            try
            {
                retVal = HttpContext.Current.Request.UrlReferrer.ToString();
            }
            catch
            {
            }
            string result;
            if (retVal == null)
            {
                result = "";
            }
            else
            {
                result = retVal;
            }
            return result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string GetCurrentFullHost()
        {
            HttpRequest request = HttpContext.Current.Request;
            string result;
            if (!request.Url.IsDefaultPort)
            {
                result = string.Format("{0}:{1}", request.Url.Host, request.Url.Port.ToString());
            }
            else
            {
                result = request.Url.Host;
            }
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string GetHost()
        {
            return HttpContext.Current.Request.Url.Host;
        }

        public static string GetDnsSafeHost()
        {
            return HttpContext.Current.Request.Url.DnsSafeHost;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string GetRawUrl()
        {
            return HttpContext.Current.Request.RawUrl;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static bool IsBrowserGet()
        {
            string[] BrowserName = new string[]
			{
				"ie",
				"opera",
				"netscape",
				"mozilla",
				"konqueror",
				"firefox"
			};
            string curBrowser = HttpContext.Current.Request.Browser.Type.ToLower();
            bool result;
            for (int i = 0; i < BrowserName.Length; i++)
            {
                if (curBrowser.IndexOf(BrowserName[i]) >= 0)
                {
                    result = true;
                    return result;
                }
            }
            result = false;
            return result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static bool IsSearchEnginesGet()
        {
            bool result;
            if (HttpContext.Current.Request.UrlReferrer == null)
            {
                result = false;
            }
            else
            {
                string[] SearchEngine = new string[]
				{
					"google",
					"yahoo",
					"msn",
					"baidu",
					"sogou",
					"sohu",
					"sina",
					"163",
					"lycos",
					"tom",
					"yisou",
					"iask",
					"soso",
					"gougou",
					"zhongsou"
				};
                string tmpReferrer = HttpContext.Current.Request.UrlReferrer.ToString().ToLower();
                for (int i = 0; i < SearchEngine.Length; i++)
                {
                    if (tmpReferrer.IndexOf(SearchEngine[i]) >= 0)
                    {
                        result = true;
                        return result;
                    }
                }
                result = false;
            }
            return result;
        }

        /// <summary>
        /// 判断IP是否在一个IP段内
        /// </summary>
        /// <param name="myIP"></param>
        /// <param name="startIp"></param>
        /// <param name="endIp"></param>
        /// <returns></returns>
        public static bool IsIPInRange(string myIP, string startIp, string endIp)
        {
            if (myIP == "::1") myIP = "127.0.0.1";
            long myipLong = IP2Long(myIP);
            long start = IP2Long(startIp);
            long end = IP2Long(startIp);
            return myipLong >= start && myipLong <= end;
        }

        public static long IP2Long(string ip)
        {
            //code from www.sharejs.com
            string[] ipBytes;
            double num = 0;
            if (!string.IsNullOrEmpty(ip))
            {
                ipBytes = ip.Split('.');
                for (int i = ipBytes.Length - 1; i >= 0; i--)
                {
                    num += ((int.Parse(ipBytes[i]) % 256) * Math.Pow(256, (3 - i)));
                }
            }
            return (long)num;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string GetUrl()
        {
            return HttpContext.Current.Request.Url.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string GetPageName()
        {
            string[] urlArr = HttpContext.Current.Request.Url.AbsolutePath.Split(new char[]
			{
				'/'
			});
            return urlArr[urlArr.Length - 1].ToLower();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static int GetParamCount()
        {
            return HttpContext.Current.Request.Form.Count + HttpContext.Current.Request.QueryString.Count;
        }

        public static int GetRequestInt(string name, int defaultval)
        {
            int rs = defaultval;
            if (!string.IsNullOrEmpty(HttpContext.Current.Request[name]))
            {
                try
                {
                    rs = int.Parse(HttpContext.Current.Request[name]);
                }
                catch
                {

                }
            }
            return rs;
        }

        public static long GetRequestLong(string name, long defaultval)
        {
            long rs = defaultval;
            if (!string.IsNullOrEmpty(HttpContext.Current.Request[name]))
            {
                try
                {
                    rs = long.Parse(HttpContext.Current.Request[name]);
                }
                catch
                {

                }
            }
            return rs;
        }

        public static Double GetRequestDouble(string name, Double defaultval)
        {
            Double rs = defaultval;
            if (!string.IsNullOrEmpty(HttpContext.Current.Request[name]))
            {
                try
                {
                    rs = Double.Parse(HttpContext.Current.Request[name]);
                }
                catch
                {

                }
            }
            return rs;
        }

        public static Decimal GetRequestDecimal(string name, Decimal defaultval)
        {
            Decimal rs = defaultval;
            if (!string.IsNullOrEmpty(HttpContext.Current.Request[name]))
            {
                try
                {
                    rs = Decimal.Parse(HttpContext.Current.Request[name]);
                }
                catch
                {

                }
            }
            return rs;
        }

        public static String GetRequestString(string name, String defaultval)
        {
            String rs = defaultval;
            if (!string.IsNullOrEmpty(HttpContext.Current.Request[name]))
            {
                rs = (HttpContext.Current.Request[name]);
            }
            return rs;
        }

        public static string GetUrlContent(string url)
        {
            return GetUrlContent(url, System.Text.Encoding.Default);
        }

        public static string GetUrlContent(string url, Encoding encoding)
        {
            string strMsg = string.Empty;
            try
            {
                WebRequest request = WebRequest.Create(url);
                WebResponse response = request.GetResponse();
                using (Stream stream = response.GetResponseStream())
                {
                    using (StreamReader reader = new StreamReader(stream, encoding))
                    {
                        strMsg = reader.ReadToEnd();
                        reader.Dispose();
                    }
                }
                response.Close();
            }
            catch
            { }
            return strMsg;
        }

        public static string GetUrlContent(string url, string savePath)
        {
            string strMsg = string.Empty;
            try
            {
                WebRequest request = WebRequest.Create(url);
                WebResponse response = request.GetResponse();
                Stream reader = response.GetResponseStream();
                //可根据实际保存为具体文件
                FileStream writer = new FileStream(savePath, FileMode.OpenOrCreate, FileAccess.Write);
                byte[] buff = new byte[512];
                int c = 0; //实际读取的字节数 
                while ((c = reader.Read(buff, 0, buff.Length)) > 0)
                {
                    writer.Write(buff, 0, c);
                }
                writer.Close();
                writer.Dispose();

                reader.Close();
                reader.Dispose();
                response.Close();

                strMsg = "1";
            }
            catch
            { }
            return strMsg;
        }

        public static string PostUrlContent(string strUrl, Dictionary<string, string> dataDic)
        {
            return PostUrlContent(strUrl, dataDic, System.Text.Encoding.Default);
        }

        public static string PostUrlContent(string strUrl, Dictionary<string, string> dataDic, Encoding encoding)
        {
            string strMsg = string.Empty;
            try
            {
                string data = string.Empty;
                foreach (var item in dataDic)
                {
                    data += item.Key + "=" + item.Value + "&";
                }
                data = data.TrimEnd('&');
                byte[] requestBuffer = encoding.GetBytes(data);

                WebRequest request = WebRequest.Create(strUrl);
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = requestBuffer.Length;
                using (Stream requestStream = request.GetRequestStream())
                {
                    requestStream.Write(requestBuffer, 0, requestBuffer.Length);
                    requestStream.Close();
                }

                WebResponse response = request.GetResponse();
                using (StreamReader reader = new StreamReader(response.GetResponseStream(), encoding))
                {
                    strMsg = reader.ReadToEnd();
                    reader.Close();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return strMsg;
        }

        /// <summary>
        /// 给当前请求签名
        /// </summary>
        /// <returns></returns>
        public static string SignRequest(Dictionary<string, string> parameters, string secretkey)
        {
            SortedDictionary<string, string> dic2 = new SortedDictionary<string, string>(parameters);
            IEnumerator<KeyValuePair<string, string>> dem = dic2.GetEnumerator();
            StringBuilder sb = new StringBuilder();
            while (dem.MoveNext())
            {
                string key = dem.Current.Key;
                string val = dem.Current.Value;
                if (!string.IsNullOrEmpty(key))
                {
                    sb.Append(key).Append(val);
                }
            }
            sb.Append(secretkey);
            string ms5 = EncryptTools.MD5(sb.ToString());
            return ms5;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="secretkey">密钥</param>
        /// <param name="signrequestName">签名的键值名称</param>
        /// <returns></returns>
        public static bool SignRequestValidate(string secretkey, string signrequestName = "sign")
        {
            string method = HttpContext.Current.Request.HttpMethod;
            System.Collections.Specialized.NameValueCollection form = HttpContext.Current.Request.QueryString;
            if (method == "POST")
            {
                form = HttpContext.Current.Request.Form;
            }
            else if (method == "GET")
            {
                form = HttpContext.Current.Request.QueryString;
            }
            else return false;
            Dictionary<string, string> dic = new Dictionary<string, string>();
            for (int f = 0; f < form.Count; f++)
            {
                string key = form.Keys[f];
                if (string.Compare(key, signrequestName, true) == 0)
                {
                    continue;
                }
                dic.Add(key, form[key]);
            }

            string mysign = SignRequest(dic, secretkey);
            return string.Compare(mysign, form[signrequestName]) == 0;
        }
    }
}
