using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HJN.Dapper.MySql
{
    public class MySqlAdo : DapperAdo
    {
        public MySqlAdo(string connstr)
            : base(connstr)
        {
            InitAdo(connstr);
        }

        protected override void InitAdo(string connstr)
        {
            this.ConnectionString = connstr;
            this.DbConnecttion = new MySqlConnection(ConnectionString);
            this.DbType = DbType.SqlServer;
            this.SqlBuilder = new SqlBuilder_MySql();
        }
    }
}
