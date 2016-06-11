using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using YueWen.Utility.Common;

namespace YueWen.Utility.TestWeb
{
    public partial class Base64Decode : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string bas64 = "6IGC5Yab5Y2OMQ";

            string rs = StringHelper.SafeBase64Decode(bas64);
            Response.Write(rs);
        }
    }
}