using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using YueWen.Utility.Common;

namespace YueWen.Utility.TestWeb
{
    public partial class aes : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string mobile = "13761138329";
            string s1 = EncryptTools.AESEncrypt(mobile);
            Response.Write("加密结果：" + s1);
            Response.Write("<br/>解密结果:");
            Response.Write(EncryptTools.AESDecrypt(s1));
        }
    }
}