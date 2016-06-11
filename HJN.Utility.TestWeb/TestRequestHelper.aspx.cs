using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using YueWen.Utility.Common;

namespace YueWen.Utility.TestWeb
{
    public partial class TestRequestHelper : System.Web.UI.Page
    {
        //private static Lazy<StackExchange.Redis.ConnectionMultiplexer> lazyConnection = new Lazy<StackExchange.Redis.ConnectionMultiplexer>(() =>
        //{
        //    return StackExchange.Redis.ConnectionMultiplexer.Connect("");
        //});

        protected void Page_Load(object sender, EventArgs e)
        {

     

            string url = "http://www.baidu.com?a";

            string rs = RequestHelper.GetUrlContent(url);
            rs = RequestHelper.BuildUrl(url, "name","2");

            Response.Write(rs);
        }
    }
}