using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit;
using NUnit.Framework;
using HJN.Dapper;
using System.Diagnostics;
using System.Data;
namespace HJN.Utility.TestNunit
{
    public class TestAdd
    {
        DapperAdo ado;
        public TestAdd()
        {
            ado = new SqlServerAdo("server =sh-niejunhua; uid=sa; database=CNblogs; password=niejunhua;");
        }

        [Test]
        public void Delete()
        {
            //var emp = ado.Execute("delete from wfcomment where procinstid=606347220");

            //var em2p = ado.SingleOrDefault<WfComment>("select * from [WfComment] where [ProcinstID]=@id or [ProcinstID]=@id2", new { id = 606347220, id2 = 479713337 });
            //Console.WriteLine("em2p=null:" +( em2p == null));
            var emp = ado.SingleOrDefault<WfComment>("select * from [WfComment] where [ProcinstID]=@id or [ProcinstID]=@id2", new { id = -170340317, id2 = -170340317 });
            Console.WriteLine(emp.foLiO);
            ado.Delete(emp);
            emp = ado.SingleOrDefault<WfComment>("select * from [WfComment] where [ProcinstID]=@id or [ProcinstID]=@id2", new { id = -170340317, id2 = -170340317 });
            Console.WriteLine(emp == null);
        }

        [Test]
        public void Update()
        {
            var emp = ado.SingleOrDefault<WfComment>("select * from [WfComment] where [ProcinstID]=@id or [ProcinstID]=@id2", new { id = 606347220, id2 = 479713337 });
            Console.WriteLine(emp.foLiO);
            Console.WriteLine("开始更新：");
            emp.foLiO = "niejunhua";
            ado.Update(emp);

            emp = ado.SingleOrDefault<WfComment>("select * from [WfComment] where [ProcinstID]=@id or [ProcinstID]=@id2", new { id = 606347220, id2 = 479713337 });
            Console.WriteLine("更新后folio=" + emp.foLiO);
        }

        [Test]
        public void QueryDatatablePaging()
        {
            int recordcount = 0;
            DataTable dt = ado.QueryDataTable("WfComment", "idx", "*", "procinstid desc", "procinstid>0", 2, 20, out recordcount);

            foreach (DataRow dr in dt.Rows)
            {
                Console.WriteLine(dr["Folio"]);
            }

            Console.WriteLine("Rows.Count=" + dt.Rows.Count);
        }

        [Test]
        public void QueryDatatable()
        {
            DataTable dt = ado.QueryDataTable("select * from WfComment where procinstid<0 or procinstid in (606347220,-170340317,479713337)");

            foreach (DataRow dr in dt.Rows)
            {
                Console.WriteLine(dr["Folio"]);
            }

            Console.WriteLine("Rows.Count=" + dt.Rows.Count);
        }

        [Test]
        public void QueryListGeneragic()
        {
            IEnumerable<WfComment> enumobj = ado.QueryList<WfComment>("select * from WfComment where procinstid in (606347220,-170340317,479713337)");
            foreach (WfComment obj in enumobj)
            {
                Console.WriteLine(obj.foLiO);
            }
        }

        [Test]
        public void QueryListGeneragicPaging()
        {
            int recordcount = 0;
            IEnumerable<WfComment> enumobj = ado.QueryList<WfComment>("wfcomment", "idx", "*", "procinstid desc", "procinstid in (606347220,479713337,-170340317,10250433,-1939062721)", 1, 20, out recordcount);
            foreach (WfComment obj in enumobj)
            {
                Console.WriteLine(obj.foLiO);
            }
            Console.WriteLine("total:" + recordcount);
        }

        [Test]
        public void QueryListDynamic()
        {
            IEnumerable<dynamic> enumobj = ado.QueryList(typeof(WfComment), "select * from WfComment where procinstid in (606347220,-170340317,479713337)");
            foreach (dynamic obj in enumobj)
            {
                Console.WriteLine(obj.Folio);
            }
        }

        [Test]
        public void QueryListDynamicPaging()
        {
            int recordcount = 0;
            IEnumerable<dynamic> enumobj = ado.QueryList("wfcomment", "idx", "*", "procinstid desc", "procinstid in (606347220,479713337,-170340317,10250433,-1939062721)", 1, 20, out recordcount);
            foreach (dynamic obj in enumobj)
            {
                Console.WriteLine(obj.Folio);
            }
            Console.WriteLine("total:" + recordcount);
        }

        [Test]
        public void QueryList()
        {
            IEnumerable<object> enumobj = ado.QueryList(typeof(WfComment), "select * from WfComment where procinstid in (606347220,-170340317,479713337)");
            foreach (object obj in enumobj)
            {
                Console.WriteLine(((WfComment)obj).foLiO);
            }
        }

        [Test]
        public void QueryListPaging()
        {
            int recordcount = 0;
            IEnumerable<object> enumobj = ado.QueryList(typeof(WfComment), "wfcomment", "idx", "*", "procinstid desc", "procinstid in (606347220,479713337,-170340317,10250433,-1939062721)", 1, 20, out recordcount);
            foreach (object obj in enumobj)
            {
                Console.WriteLine(((WfComment)obj).foLiO);
            }
            Console.WriteLine("total:" + recordcount);
        }

        [Test]
        public void GetOne()
        {
            var emp = ado.SingleOrDefault<WfComment>("select * from [WfComment] where [ProcinstID]=@id or [ProcinstID]=@id2", new { id = -1939062721, id2 = 479713337 });
            Console.WriteLine(emp.foLiO);
        }

        [Test]
        public void Add()
        {
            //WfComment comment = new WfComment
            //{
            //    Folio = Guid.NewGuid().ToString(),
            //    Comment = Guid.NewGuid().ToString(),
            //    Action = Guid.NewGuid().ToString(),
            //    ActivityName = Guid.NewGuid().ToString(),
            //    ApproveTime = DateTime.Now,
            //    IsFinal = 1,
            //    ReceiveTime = DateTime.Now.AddHours(-1),
            //    ProcinstID = DateTime.Now.ToString().GetHashCode()
            //};
            //int rs = ado.Insert<WfComment>(comment);
            //Console.WriteLine(rs);

            Console.WriteLine("开始插入500次");
            Stopwatch sw = new Stopwatch();
            sw.Start();
            ado.BeginTrans();
            for (int i = 0; i < 5000; i++)
            {
                WfComment comment = new WfComment
                {
                    foLiO = Guid.NewGuid().ToString(),
                    Comment = Guid.NewGuid().ToString(),
                    Action = Guid.NewGuid().ToString(),
                    ActivityName = Guid.NewGuid().ToString(),
                    ApproveTime = DateTime.Now,
                    IsFinal = 1,
                    ReceiveTime = DateTime.Now.AddHours(-1),
                    ProcinstID = Guid.NewGuid().ToString().GetHashCode()
                };
                ado.Insert<WfComment>(comment);
            }
            ado.CommitTrans();
            Console.WriteLine("结束插入500次：" + sw.Elapsed);
        }
    }
}
