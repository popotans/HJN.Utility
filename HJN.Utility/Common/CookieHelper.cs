using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace HJN.Utility.Common
{
    public class CookieHelper
    {

        public static void WriteCookie(string strName, string strValue)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[strName];
            if (cookie == null)
            {
                cookie = new HttpCookie(strName);
            }
            cookie.Value = strValue;
            HttpContext.Current.Response.AppendCookie(cookie);
        }

        public static void WriteCookie(string strName, string strValue, int expiresMinutes)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[strName];
            if (cookie == null)
            {
                cookie = new HttpCookie(strName);
            }
            cookie.Value = strValue;
            cookie.Expires = DateTime.Now.AddMinutes((double)expiresMinutes);
            HttpContext.Current.Response.AppendCookie(cookie);
        }

        public static string GetCookie(string strName)
        {
            string result;
            if (HttpContext.Current.Request.Cookies != null && HttpContext.Current.Request.Cookies[strName] != null)
            {
                result = HttpContext.Current.Request.Cookies[strName].Value.ToString();
            }
            else
            {
                result = "";
            }
            return result;
        }


        public static void WriteEncryptCookie(string strName, string strValue, int expiresMinutes)
        {
            strValue = EncryptTools.DESEncrypt(strValue);

            WriteCookie(strName, strValue, expiresMinutes);
        }

        public static string GetDecryptCookie(string strName)
        {

            try
            {
                string result = GetCookie(strName);
                if (!string.IsNullOrEmpty(result))
                {
                    result = EncryptTools.DESDecrypt(result);
                }

                return result;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
