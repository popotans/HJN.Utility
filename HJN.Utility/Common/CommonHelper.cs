using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace HJN.Utility.Common
{
  public  class CommonHelper
    {
        /// <summary>
        /// 获取访问的IP
        /// </summary>
        /// <param name="Request"></param>
        /// <returns></returns>
        public static string GetIP(HttpRequestBase Request)
        {
            string userIP = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (userIP == null || userIP == "")
            {
                userIP = Request.ServerVariables["REMOTE_ADDR"];
            }
            return userIP;
        }
    }
}
