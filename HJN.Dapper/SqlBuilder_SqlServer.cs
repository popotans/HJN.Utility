﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace HJN.Dapper
{
    public class SqlBuilder_SqlServer : ISqlBuilder
    {
        char ColumnBeginChar = '[', ColumnEndChar = ']', ParamPrefix = '@';
        private Dictionary<string, SqlParamModel> paramValsDic = new Dictionary<string, SqlParamModel>();

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
                paramValsDic.Add(ParamPrefix + pi.Name, spm);
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
                throw new ArgumentException("未设置主键不能使用此删除方式！");
            }
            return sb.ToString();
        }

        /// <summary> 
        /// 单表（视图）获取分页SQL语句 
        /// </summary> 
        /// <param name="tableName">表名或视图名</param> 
        /// <param name="key">唯一键</param> 
        /// <param name="fields">获取的字段</param> 
        /// <param name="condition">查询条件（不包含WHERE）</param> 
        /// <param name="collatingSequence">排序规则（不包含ORDER BY）</param> 
        /// <param name="pageSize">页大小</param> 
        /// <param name="pageIndex">页码（从1开始）</param> 
        /// <returns>分页SQL语句</returns> 
        public string GetPagingSQL(string tableName, string key, string fields, string collatingSequence, string condition, int pageIndex, int pageSize)
        {
            string whereClause = string.Empty;
            if (!string.IsNullOrEmpty(condition))
            {
                whereClause = string.Format("WHERE {0}", condition);
            }

            if (string.IsNullOrEmpty(collatingSequence))
            {
                collatingSequence = string.Format("{0} ASC", key);
            }

            StringBuilder sbSql = new StringBuilder();

            sbSql.AppendFormat("SELECT {0} ", PrependTableName(tableName, fields, ','));
            sbSql.AppendFormat("FROM ( SELECT TOP {0} ", pageSize * pageIndex);
            sbSql.AppendFormat(" [_RowNum_] = ROW_NUMBER() OVER ( ORDER BY {0} ), ", collatingSequence);
            sbSql.AppendFormat(" {0} ", key);
            sbSql.AppendFormat(" FROM {0} ", tableName);
            sbSql.AppendFormat(" {0} ", whereClause);
            sbSql.AppendFormat(" ) AS [_TempTable_] ");
            sbSql.AppendFormat(" INNER JOIN {0} ON [_TempTable_].{1} = {0}.{1} ", tableName, key);
            sbSql.AppendFormat("WHERE [_RowNum_] > {0} ", pageSize * (pageIndex - 1));
            sbSql.AppendFormat("ORDER BY [_TempTable_].[_RowNum_] ASC ");

            return sbSql.ToString();
        }

        /// <summary> 
        /// 给字段添加表名前缀 
        /// </summary> 
        /// <param name="tableName">表名</param> 
        /// <param name="fields">字段</param> 
        /// <param name="separator">标识字段间的分隔符</param> 
        /// <returns></returns> 
        public string PrependTableName(string tableName, string fields, char separator)
        {
            StringBuilder sbFields = new StringBuilder();

            string[] fieldArr = fields.Trim(separator).Split(separator);
            foreach (string str in fieldArr)
            {
                sbFields.AppendFormat("{0}.{1}{2}", tableName, str.Trim(), separator);
            }
            return sbFields.ToString().TrimEnd(separator);
        }


        public string GetPagingCountSQL(string tableName, string condition)
        {
            string whereClause = string.Empty;
            if (!string.IsNullOrEmpty(condition))
            {
                whereClause = string.Format(" WHERE {0}", condition);
            }
            return "select count(1) from " + tableName + whereClause;
        }
    }
}
