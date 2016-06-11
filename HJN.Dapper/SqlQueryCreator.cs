using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace HJN.Dapper
{
    public class SqlQueryCreator<T> where T : BaseEntity
    {
        private DbType DbType;
        private char ColumnBeginChar, ColumnEndChar, ParamPrefix;
        private Dictionary<string, SqlParamModel> paramValsDic = new Dictionary<string, SqlParamModel>();

        private SqlQueryCreator()
        {
            DbType = DbType.SqlServer;

        }

        private SqlQueryCreator(DbType dbtype)
        {
            DbType = dbtype;
        }

        private void InitParam(DbType type)
        {
            if (type == DbType.SqlServer || type == DbType.Access)
            {
                this.ColumnBeginChar = '[';
                this.ColumnEndChar = ']';
                ParamPrefix = '@';
            }
            if (type == DbType.MySql || type == DbType.Sqllite)
            {
                this.ColumnBeginChar = '`';
                this.ColumnEndChar = '`';
                ParamPrefix = '@';
            }
            if (type == DbType.Oracle)
            {
                //this.ColumnBeginChar = '';
                //this.ColumnEndChar = '';
                ParamPrefix = '@';
            }
        }

        public static SqlQueryCreator<T> CreateInstance()
        {
            return new SqlQueryCreator<T>();
        }

        public static SqlQueryCreator<T> CreateInstance(DbType dbtype)
        {
            return new SqlQueryCreator<T>(dbtype);
        }

        public string CreateUpdateSql(T t)
        {
            StringBuilder sb = new StringBuilder();
            PropertyInfo[] propertiesArr = t.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
            sb.AppendFormat("update {0}{1}{2} set ", ColumnBeginChar, t.TableName, ColumnEndChar);
            int i = 0;
            foreach (PropertyInfo pi in propertiesArr)
            {
                i++;
                if (t.IdentityCols.Contains(pi.Name)) continue;
                if (i != propertiesArr.Length)
                    sb.AppendFormat("{0}{1}{2}={3}{1},", ColumnBeginChar, pi.Name, ColumnEndChar, ParamPrefix, pi.Name);
                else
                {
                    sb.AppendFormat("{0}{1}{2}={3}{1} ", ColumnBeginChar, pi.Name, ColumnEndChar, ParamPrefix, pi.Name);
                }
                SqlParamModel spm = new SqlParamModel()
                {
                    Type = pi.GetType(),
                    Value = pi.GetValue(t, null)
                };
                paramValsDic.Add(ParamPrefix + pi.Name, spm);
            }
            sb.Append(" where ");
            i = 0;
            foreach (string primary in t.PrimaryKeys)
            {
                i++;
                if (i != t.PrimaryKeys.Count)
                    sb.AppendFormat("{2}{0}{3}={1} and ", primary, paramValsDic[primary].BuildValueFormat(), ColumnBeginChar, ColumnEndChar);
                else
                    sb.AppendFormat("{2}{0}{3}={1} ", primary, paramValsDic[primary].BuildValueFormat(), ColumnBeginChar, ColumnEndChar);

            }

            return sb.ToString();
        }
    }
}
