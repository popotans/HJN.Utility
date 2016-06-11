using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Web;
using System.Text.RegularExpressions;
using System.IO;
using System.Security.Principal;
using System.Xml;
using System.Data;

namespace YueWen.HttpModule
{
    /// <summary>
    /// 验证模块，返回的为当前用户的GUID;（
    /// </summary>
    public class SecAuthenticateModule : IHttpModule
    {
        #region IHttpModule 成员

        public void Dispose()
        {

        }

        public void Init(HttpApplication context)
        {
            context.PreRequestHandlerExecute += new EventHandler(OnAuthenticate);
        }

        void OnAuthenticate(object sender, EventArgs e)
        {
            HttpApplication application = sender as HttpApplication;
            if (!application.Request.Url.AbsolutePath.Trim().ToLower().EndsWith(".aspx")
                || application.Request.Url.AbsolutePath.Trim().ToLower().Contains("/js/")
                || application.Request.Url.AbsolutePath.Trim().ToLower().Contains("/scripts/")
                || application.Request.Url.AbsolutePath.Trim().ToLower().Contains("/css/")
                || application.Request.Url.AbsolutePath.Trim().ToLower().Contains("/img/")
                || application.Request.Url.AbsolutePath.Trim().ToLower().Contains("/images/")
                || application.Request.Url.AbsolutePath.Trim().ToLower().Contains("/styles/")
                )
            {
                return;
            }

            //如果标识页面为未经登录认证也可访问，则直接跳出
            if (CheckExcludePage())
            {
                return;
            }

            string ticket = application.Request.QueryString["ticket"];
            string accountWithDomain = application.Session["CurrentEmployeeAccountWithDomain"] as string;
            string CurrentEmployeeId = application.Session["CurrentEmployeeId"] as string; ;
            if (!string.IsNullOrEmpty(ticket) && string.IsNullOrEmpty(CurrentEmployeeId))
            {
                /*
                 http://10.97.190.110:8001/Verify.aspx?appcode=1003&ticket=AA157C51FD0E44F0356F5848FBB8C1CE6ABDA1F8D87DE65530BA344371B5EFB1B20A7D3C67EA03C57B8CAE2E30B272E63D1862A2E3CA299469C0DD6B3B74F6F797D0CA70E1BC7F47B09E81A0A0555377BB857290EB91F6FC76347B965BBEC054FA662D999C689EAAA51A233C4753C81BDEB6B70FE9C77C88
                 */
                string verifyUrl = string.Format(SecConfig.Current.SecVerifyUrl, ticket);

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
                    this.Login(application);
                    return;
                }
                string userId = result[1];//角色ID
                string no = result[2];
                string domain = result[3];
                string account = result[4];
                string name = result[5];

                accountWithDomain = string.Format("{0}\\{1}", domain, account).ToLower();
                CurrentEmployeeId = userId;
                application.Session["CurrentEmployeeId"] = userId;
                application.Session["CurrentEmployeeNo"] = no;
                application.Session["CurrentEmployeeAccountWithDomain"] = accountWithDomain;

                HttpCookie cookie = new HttpCookie("SECEmployee");
                TimeSpan ts = new TimeSpan(365, 24, 59, 59, 59);
                cookie.Expires = DateTime.Now.Add(ts);
                cookie.Values.Add("CurrentEmployeeId", CurrentEmployeeId);
                application.Response.AppendCookie(cookie);
            }

            if (string.IsNullOrEmpty(accountWithDomain))
            {
                //ErrorLogger.Log(string.Format("not authenticated :{0}", application.Context.Request.Url.AbsoluteUri));
                this.Login(application);
                return;
            }
            //ErrorLogger.Log(string.Format("Authenticated :{0}", application.Context.Request.Url.AbsoluteUri));
            application.Context.User = new GenericPrincipal(new GenericIdentity(CurrentEmployeeId), new string[0]);
        }

        private void Login(HttpApplication application)
        {
            string url = application.Request.Url.AbsoluteUri;
            url = TicketRegex.Replace(url, new MatchEvaluator(delegate(Match match)
            {
                return match.Groups[1].Value;
            }));
            url = url.Trim(new char[] { '?', '&' });
            application.Response.Redirect(
                string.Format(SecConfig.Current.SecLoginUrl, application.Context.Server.UrlEncode(url)));
            application.Response.End();
        }

        private bool CheckExcludePage()
        {
            XmlNodeList excludePages = SecConfig.Current.ExcludePages;
            if (excludePages != null && excludePages.Count > 0)
            {
                foreach (XmlNode page in excludePages)
                {
                    if (page.Name != "Page")
                    {
                        continue;
                    }

                    if (!string.IsNullOrEmpty(SecConfig.Current.GetNodeValue(page)) && HttpContext.Current.Request.Path.EndsWith(SecConfig.Current.GetNodeValue(page), StringComparison.CurrentCultureIgnoreCase))
                    {
                        return true;
                    }
                }
            }

            if (string.IsNullOrEmpty(SecConfig.Current.LoginSiteUrl))
            {
                return false;
            }
            else
            {
                string path = string.Empty;
                if (SecConfig.Current.LoginSiteUrl.IndexOf("http") > -1)
                {
                    string[] list = SecConfig.Current.LoginSiteUrl.Split(new char[] { '/' });
                    if (list != null && list.Length > 1)
                    {
                        path = list[list.Length - 1];
                    }
                }
                else
                {
                    path = SecConfig.Current.LoginSiteUrl;
                }
                if (HttpContext.Current.Request.Path.EndsWith(path, StringComparison.CurrentCultureIgnoreCase))
                {
                    return true;
                }
            }

            return false;
        }

        static readonly Regex TicketRegex = new Regex(@"([?&])(ticket=[^&]*&?)");

        #endregion

        #region 查看页面生成图片处理

        #endregion

    }
}
