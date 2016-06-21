using HJN.Dapper;
using HJN.Dapper.MySql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using YueWen.Utility.TestWeb.Dappertest;

namespace YueWen.Utility.TestWeb.Sqlbuilder_MySql
{
    public partial class MySqlBUilder : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            BuildDelete();
        }

        void BuildInsert()
        {
            Person person = new Person();
            ISqlBuilder builder = new SqlBuilder_MySql();
            DapperAdo ado = new MySqlAdo("");

            string insertSql = builder.Insert<Person>(person);
            Response.Write(insertSql);

        }

        void BuildUpdate()
        {
            Person person = new Person();
            ISqlBuilder builder = new SqlBuilder_MySql();
            DapperAdo ado = new MySqlAdo("");

            string insertSql = builder.Updatet<Person>(person);
            Response.Write(insertSql);

        }

        void BuildDelete()
        {
            Person person = new Person();
            ISqlBuilder builder = new SqlBuilder_MySql();
            DapperAdo ado = new MySqlAdo("");

            string insertSql = builder.Delete<Person>(person);
            Response.Write(insertSql);

        }
    }
}