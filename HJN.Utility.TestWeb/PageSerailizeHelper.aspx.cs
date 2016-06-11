using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using YueWen.Utility.Common;

namespace YueWen.Utility.TestWeb
{
    public partial class PageSerailizeHelper : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //string json = "{\"access_token\":\"123token\",\"expires\":\"11123366666\"}";
            //var item = SerializeHelper.JsonToDictionary(json);
            //Response.Write(item["access_token"]);
            //Response.Write("  ::"+item["expires"]);



            DataTable dt = new DataTable();
            dt.Columns.Add("name");
            dt.Columns.Add("age");
            dt.Columns.Add("birthday", typeof(DateTime));
            dt.Columns.Add("mobile");
            DataRow dr = dt.NewRow();
            dr[0] = "niejunhua";
            dr[1] = "25";
            dr[2] = "2015-02-02";
            dr[3] = "13761138329";
            dt.Rows.Add(dr);
            dr = dt.NewRow();
            dr[0] = "\"jennyyang";
            dr[1] = "26";
            dr[2] = "2015-1-1";
            dr[3] = "15995846696";
            dt.Rows.Add(dr);
            string ss = SerializeHelper.DataTableToJson(dt);
            Response.Write(ss);
        }
    }
}