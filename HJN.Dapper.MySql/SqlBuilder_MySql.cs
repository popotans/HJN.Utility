using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace HJN.Dapper.MySql
{
    public class SqlBuilder_MySql : ISqlBuilder
    {
        char ColumnBeginChar = '`', ColumnEndChar = '`', ParamPrefix = '?';
        public string Insert<T>(T t) where T : BaseEntity
        {
            StringBuilder sb = new StringBuilder();
            PropertyInfo[] propertiesArr = t.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
            sb.AppendFormat("INSERT INTO {0}{1}{2}(", ColumnBeginChar, t.__TableName, ColumnEndChar);
            int i = 0;
            List<string> collist = new List<string>();
            foreach (PropertyInfo pi in propertiesArr)
            {
                if (t.__IdentityKeys.Contains(pi.Name)) continue;
                collist.Add(pi.Name);
            }
            foreach (string item in collist)
            {
                i++;
                if (i != collist.Count())
                    sb.AppendFormat("{1}{0}{2},", item, ColumnBeginChar, ColumnEndChar);
                else sb.AppendFormat("{1}{0}{2})values(", item, ColumnBeginChar, ColumnEndChar);
            }
            i = 0;
            foreach (string item in collist)
            {
                i++;
                if (i != collist.Count())
                    sb.AppendFormat("{1}{0},", item, ParamPrefix);
                else sb.AppendFormat("{1}{0})", item, ParamPrefix);
            }

            return sb.ToString();
        }

        public string Updatet<T>(T t) where T : BaseEntity
        {
            StringBuilder sb = new StringBuilder();
            PropertyInfo[] propertiesArr = t.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
            sb.AppendFormat("update {0}{1}{2} set ", ColumnBeginChar, t.__TableName, ColumnEndChar);
            int i = 0;
            foreach (PropertyInfo pi in propertiesArr)
            {
                SqlParamModel spm = new SqlParamModel()
                {
                    Type = pi.PropertyType,
                    Value = pi.GetValue(t, null)
                };
                i++;
                if (t.__IdentityKeys.Contains(pi.Name)) continue;
                if (i != propertiesArr.Length)
                    sb.AppendFormat("{0}{1}{2}={3}{1},", ColumnBeginChar, pi.Name, ColumnEndChar, ParamPrefix, pi.Name);
                else
                {
                    sb.AppendFormat("{0}{1}{2}={3}{1} ", ColumnBeginChar, pi.Name, ColumnEndChar, ParamPrefix, pi.Name);
                }

            }
            sb.Append(" where ");
            i = 0;
            foreach (string primary in t.__PrimaryKeys)
            {
                // paramValsDic[primary].BuildValueFormat()
                i++;
                if (i != t.__PrimaryKeys.Count)
                    sb.AppendFormat("{1}{0}{2}={3}{0} and ", primary, ColumnBeginChar, ColumnEndChar, ParamPrefix);
                else
                    sb.AppendFormat("{1}{0}{2}={3}{0} ", primary, ColumnBeginChar, ColumnEndChar, ParamPrefix);

            }
            if (i == 0)
            {
                throw new ArgumentException("未设置主键不能使用此更新方式！");
            }

            return sb.ToString();
        }

        public string Delete<T>(T t) where T : BaseEntity
        {
            StringBuilder sb = new StringBuilder();
            PropertyInfo[] propertiesArr = t.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
            sb.AppendFormat("DELETE FROM {0}{1}{2} where ", ColumnBeginChar, t.__TableName, ColumnEndChar);
            int i = 0;
            foreach (string primaryColname in t.__PrimaryKeys)
            {
                // paramValsDic[primary].BuildValueFormat()
                i++;
                if (i != t.__PrimaryKeys.Count)
                    sb.AppendFormat("{1}{0}{2}={3}{0} and ", primaryColname, ColumnBeginChar, ColumnEndChar, ParamPrefix);
                else
                    sb.AppendFormat("{1}{0}{2}={3}{0} ", primaryColname, ColumnBeginChar, ColumnEndChar, ParamPrefix);
            }
            if (i == 0)
            {
                throw new ArgumentException("未设置主键不能使用此删除方式！");
            }
            return sb.ToString();
        }


        public string GetPagingSQL(string tableName, string key, string fields, string collatingSequence, string condition, int pageIndex, int pageSize)
        {
            throw new NotImplementedException();
        }


        public string GetPagingCountSQL(string tableName, string condition)
        {
            throw new NotImplementedException();
        }
    }
}
