using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using YueWen.Utility.Common;
namespace YueWen.Utility.SEC
{
    public class SECLogin
    {
        string cookieid = "aspid";
        public string CreateMD5Sign(string data2Sign)
        {
            byte[] buffer = System.Text.Encoding.GetEncoding("utf-8").GetBytes(data2Sign);

            string rs = "";
            byte[] data = MD5.Create().ComputeHash(buffer);
            var sb = new StringBuilder();
            //遍历每个字节的散列数据 
            foreach (var t in data)
            {
                sb.Append(t.ToString("X2"));
            }

            rs = sb.ToString();
            return rs;
        }

        public void Login()
        {
            string ticket = HttpContext.Current.Request.QueryString["ticket"];
            string accountWithDomain = HttpContext.Current.Session["CurrentEmployeeAccountWithDomain"] as string;
            string currentEmployeeId = HttpContext.Current.Session["CurrentEmployeeId"] as string;

            if (string.IsNullOrEmpty(currentEmployeeId))
            {
                if (HttpContext.Current.Request.Cookies[cookieid] == null)
                {
                    currentEmployeeId = string.Empty;
                }
                else
                {
                    string jiamicookie = HttpContext.Current.Request.Cookies[cookieid].Value;
                    try
                    {
                        currentEmployeeId = EncryptTools.DESDecrypt(jiamicookie);
                        string[] arr = currentEmployeeId.Split('|');
                        currentEmployeeId = arr[0];
                        HttpContext.Current.Session["CurrentEmployeeId"] = currentEmployeeId;
                    }
                    catch (Exception ex)
                    {
                        //解密失败 则清空cookie
                        HttpCookie hc = HttpContext.Current.Request.Cookies[cookieid];
                        hc.Expires = DateTime.Now.AddDays(-1);
                        HttpContext.Current.Response.Cookies.Add(hc);
                        currentEmployeeId = "";
                    }
                }
            }

            if (!string.IsNullOrEmpty(ticket) && string.IsNullOrEmpty(currentEmployeeId))
            {
                /*
               http://10.97.190.110:8001/Verify.aspx?appcode=1003&ticket=AA157C51FD0E44F0356F5848FBB8C1CE6ABDA1F8D87DE65530BA344371B5EFB1B20A7D3C67EA03C57B8CAE2E30B272E63D1862A2E3CA299469C0DD6B3B74F6F797D0CA70E1BC7F47B09E81A0A0555377BB857290EB91F6FC76347B965BBEC054FA662D999C689EAAA51A233C4753C81BDEB6B70FE9C77C88
               */
                string sign = CreateMD5Sign(ticket + SecConfig.Current.Client_Secret);
                string verifyUrl = string.Format(SecConfig.Current.SecVerifyUrl, ticket, sign);
                HttpWebRequest request = HttpWebRequest.Create(verifyUrl) as HttpWebRequest;
                HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                Stream stream = response.GetResponseStream();
                string data = null;
                using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                {
                    /*
                     * 0|04C45B98-BDDF-4370-92C4-BE5769A50644|010636|wenxue|guoyue|郭玥
                     */
                    data = reader.ReadToEnd();
                    reader.Close();
                }
                string[] result = data.Split('|');
                if (result[0] != "0")
                {
                    //ErrorLogger.Log(string.Format("invalid ticket :{0}", application.Context.Request.Url.AbsoluteUri));
                    this.Login(HttpContext.Current);
                    return;
                }
                string userId = result[1];//角色ID
                string badge = result[2];//工号
                string domain = result[3];//域名
                string account = result[4];//帐号
                string name = result[5];//姓名

                accountWithDomain = string.Format("{0}\\{1}", domain, account).ToLower();
                currentEmployeeId = userId;
                HttpContext.Current.Session["CurrentEmployeeId"] = currentEmployeeId;
                HttpContext.Current.Session["CurrentEmployeeNo"] = badge;
                HttpContext.Current.Session["CurrentEmployeeName"] = name;
                HttpContext.Current.Session["CurrentEmployeeAccountWithDomain"] = accountWithDomain;


                HttpCookie cookie = new HttpCookie(cookieid);
                cookie.Value = EncryptTools.DESEncrypt(userId + "|" + DateTime.Now.Ticks);
                HttpContext.Current.Response.Cookies.Add(cookie);

            }

            if (string.IsNullOrEmpty(accountWithDomain))
            {
                this.Login(HttpContext.Current);
                return;
            }

            //HttpContext.Current.User = new GenericPrincipal(new GenericIdentity(currentEmployeeId), new string[0]);

        }

        static readonly Regex TicketRegex = new Regex(@"([?&])(ticket=[^&]*&?)");
        private void Login(HttpContext context)
        {
            string url = context.Request.Url.AbsoluteUri;
            url = TicketRegex.Replace(url, new MatchEvaluator(delegate(Match match)
            {
                return match.Groups[1].Value;
            }));
            url = url.Trim(new char[] { '?', '&' });
            context.Response.Redirect(
                string.Format(SecConfig.Current.SecLoginUrl, context.Server.UrlEncode(url)));
            context.Response.End();
        }

    }
}
