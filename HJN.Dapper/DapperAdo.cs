using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace HJN.Dapper
{
    public class DapperAdo
    {
        private DapperAdo() { }
        public IDbConnection DbConnecttion;
        public string ConnectionString { get; set; }
        public DbType DbType { get; set; }
        public ISqlBuilder SqlBuilder { get; set; }

        public static DapperAdo CreateSqlServerAdo(string connectionstring)
        {
            DapperAdo ado = new DapperAdo();
            ado.DbConnecttion = new SqlConnection(connectionstring);
            ado.DbType = DbType.SqlServer;
            ado.ConnectionString = connectionstring;
            ado.SqlBuilder = new  SqlBuilder_SqlServer();
            return ado;
        }

    }
}
