using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using YueWen.Utility.Common;

namespace YueWen.Utility.TestWeb
{
    public partial class TestUnicodeChange : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
          //  string ss = "\u83b7\u53d6\u7528\u6237\u4fe1\u606f\u5931\u8d22";
            string rsss = StringHelper.Unicode2Chinese_js("\u8042\u519b\u534eyangjing");
         Response.Write(rsss);
        }
    }
}