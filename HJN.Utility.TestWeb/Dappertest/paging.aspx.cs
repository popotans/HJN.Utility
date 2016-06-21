using HJN.Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace YueWen.Utility.TestWeb.Dappertest
{
    public partial class paging : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SqlBuilder_SqlServer ss = new SqlBuilder_SqlServer();

            //string sql = ss.GetPagingSQL("K2_ProcInstComment", "commentid", "*", "ApproveTime desc", "", 1, 10);
            //Response.Write(sql);

            DapperAdo ado = new SqlServerAdo("server =10.97.190.134; uid=k2test;database=BPM_K2Sln; password=ini7*cEaj7; Connect Timeout=30; Connection Lifetime =120;  Pooling=true; Max Pool Size=10000; Min Pool Size=5;");
            int totla = 0;
            DataTable dt = ado.QueryDataTable("K2_ProcInstComment", "Commentid", "*", "Approvetime desc", "", 2, 10, out totla);

            Response.Write(dt.Rows.Count + "totla=" + totla+"<br/>");

            foreach (DataColumn dc in dt.Columns)
            {
                Response.Write(dc.ColumnName + "," + dc.DataType + "<br/>");
            }

            foreach (DataRow dr in dt.Rows)
            {
                foreach (DataColumn dc in dt.Columns)
                {
                    Response.Write(dr[dc.ColumnName] + " ");
                }
                Response.Write("<br/>");
            }
        }


    }
}