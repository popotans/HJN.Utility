using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace HJN.Dapper
{
    public abstract class DapperAdo
    {
        public DapperAdo(string connectionstring) { }
        public IDbConnection DbConnecttion;
        public IDbTransaction Trans { get; set; }
        public string ConnectionString { get; set; }
        public DbType DbType { get; set; }
        public ISqlBuilder SqlBuilder { get; set; }

        protected abstract void InitAdo(string connstr);

    }

    public class SqlServerAdo : DapperAdo
    {
        public SqlServerAdo(string connectionstring)
            : base(connectionstring)
        {
            InitAdo(connectionstring);
        }

        protected override void InitAdo(string connstr)
        {
            this.ConnectionString = connstr;
            this.DbConnecttion = new SqlConnection(this.ConnectionString);
            this.DbType = DbType.SqlServer;
            this.SqlBuilder = new SqlBuilder_SqlServer();
        }
    }
}
