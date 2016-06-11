using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using YueWen.Utility.Common;

namespace YueWen.Utility.TestWeb
{
    public partial class Base64Encode : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string data = "聂军华1";

            string rs = StringHelper.SafeBase64Encode(data);
            Response.Write(rs);
        }
    }
}