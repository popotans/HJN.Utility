using HJN.Dapper;
using HJN.Utility.TestNunit;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace YueWen.Utility.TestWeb.TestUnit
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        DapperAdo ado;
        public WebForm1()
        {
            ado = new SqlServerAdo("server =sh-niejunhua; uid=sa; database=CNblogs; password=niejunhua;");
        }
        protected void Page_Load(object sender, EventArgs e)
        {

            var emp = ado.Execute("delete from wfcomment where procinstid=479713337");

            var em2p = ado.SingleOrDefault<WfComment>("select * from [WfComment] where [ProcinstID]=@id or [ProcinstID]=@id2", new { id = 606347220, id2 = 479713337 });
            Console.WriteLine("em2p=null:" + em2p == null);


            WfComment comment = new WfComment
            {
                foLiO = Guid.NewGuid().ToString(),
                Comment = Guid.NewGuid().ToString(),
                Action = Guid.NewGuid().ToString(),
                ActivityName = Guid.NewGuid().ToString(),
                ApproveTime = DateTime.Now,
                IsFinal = 1,
                ReceiveTime = DateTime.Now.AddHours(-1),
                ProcinstID = DateTime.Now.ToString().GetHashCode()
            };
            int rs = ado.Insert<WfComment>(comment);
            Console.WriteLine(rs);

            Console.WriteLine("开始插入500次");
            Stopwatch sw = new Stopwatch();
            sw.Start();
            ado.BeginTrans();
            for (int i = 0; i < 500; i++)
            {
                ado.Insert<WfComment>(comment);
            }
            ado.CommitTrans();
        }
    }
}