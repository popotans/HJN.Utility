using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dapper;
using System.Data.SqlClient;
using System.Data;
using HJN.Dapper;
using System.Collections;

namespace YueWen.Utility.TestWeb.Dappertest
{
    public partial class _1 : System.Web.UI.Page
    {
        string dbconnstr = "server=127.0.0.1;uid=sa;password=niejunhua;database=hjntest";
        DapperAdo db;
        protected void Page_Load(object sender, EventArgs e)
        {
            db = DapperAdo.CreateSqlServerAdo(dbconnstr);

            //GetOne();

            //  Delete();
            UpdateBySql();
        }

        private void GetList()
        {

        }

        private void GetOne()
        {
            string sqlcmdtext = "select * from [Person] where id in (@id1)";// or id=@id2
            //Dictionary<string, object> dic = new Dictionary<string, object>();
            //dic.Add("id1", "2");
            //dic.Add("id2", "1");
            var psnList = db.SingleOrDefault<Person>(sqlcmdtext, new { id1 = 1 });
            //   Response.Write("psnlist.count=" + psnList.Count() + "<br/>");

            //psnList.PersonName = psnList.PersonName + DateTime.Now;
            Response.Write(psnList.PersonName);

            ////  db.Update<Person>(psnList);

            //Person ppp = new Person()
            //{
            //    Address = "address",
            //    Birthday = DateTime.Now,
            //    Gender = 1,
            //    Gid = Guid.NewGuid(),
            //    Position = "开发",
            //    Salary = 2500,
            //    PersonName = "捏捏"
            //};
            //int rsd = db.Insert<Person>(ppp);
            //Response.Write(rsd);
        }


        private void Delete()
        {
            //string sqlcmdtext = "select * from [Person] where id in (@id1)";
            //var psn = db.SingleOrDefault<Person>(sqlcmdtext, new { id1 = 4 });
            //if (psn == null) Response.Write(" psn is null ");
            //else
            //{
            //    int i = db.Delete<Person>(psn);
            //    Response.Write("Affect rows:" + i + "<br/>");
            //    Response.Write("delete success");
            //}

            string sql = "delete from [person] where id=@id";
            int rt = db.Delete(sql, new { id = 3 });
            Response.Write("Delete Rows:" + rt);
        }

        private void UpdateBySql()
        {
            string sql = "update person set personname=@name where id=@id";
            int rs = db.Update(sql, new { name = "内诶诶额", id = 1 });
            Response.Write(rs);
        }
    }

    public partial class Person
    {
        public int ID { get; set; }
        public Guid Gid { get; set; }
        public string PersonName { get; set; }
        public int Gender { get; set; }
        public DateTime Birthday { get; set; }
        public decimal Salary { get; set; }
        public string Address { get; set; }
        public string Position { get; set; }
    }

    public partial class Person : BaseEntity
    {
        public Person()
        {
            this.TableName = "Person";
            this.PrimaryKeys.Add("ID");
            this.IdentityCols.Add("ID");
        }
    }


}