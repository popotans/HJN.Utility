using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Dapper;
using System.Data.SqlClient;
using System.Data.Common;
namespace HJN.Dapper
{
    public static class DapperAdoExtension
    {
        #region query
        public static T SingleOrDefault<T>(this DapperAdo dao, string sql, object paramDic = null, CommandType? commandType = default(CommandType?))
        {
            IDbConnection db = dao.DbConnecttion;
            try
            {
                if (db.State != ConnectionState.Open) db.Open();
                IEnumerable<T> enumt = null;
                if (paramDic == null)
                    enumt = db.Query<T>(sql, null, null, true, null, commandType);
                else
                    enumt = db.Query<T>(sql, new DynamicParameters(paramDic), null, true, null, commandType);
                var var = enumt.FirstOrDefault();

                return var;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (db.State == ConnectionState.Open) db.Close();
            }
        }

        public static IEnumerable<object> QueryList(this DapperAdo dao, Type type, string sql, object paramDic = null, CommandType? commandType = default(CommandType?))
        {
            IDbConnection db = dao.DbConnecttion;
            try
            {
                if (db.State != ConnectionState.Open) db.Open();
                IEnumerable<object> list = db.Query(type, sql, new DynamicParameters(paramDic), null, true, null, commandType);
                return list;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (db.State == ConnectionState.Open) db.Close();
            }
        }

        /// <summary>
        /// object enum
        /// </summary>
        /// <param name="dao"></param>
        /// <param name="type"></param>
        /// <param name="tableviewname"></param>
        /// <param name="key"></param>
        /// <param name="fields"></param>
        /// <param name="collatingSequence"></param>
        /// <param name="condition"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="recordCount"></param>
        /// <param name="paramDic"></param>
        /// <returns></returns>
        public static IEnumerable<object> QueryList(this DapperAdo dao, Type type, string tableviewname, string key, string fields, string collatingSequence, string condition, int pageIndex, int pageSize, out int recordCount, object paramDic = null)
        {
            IDbConnection db = dao.DbConnecttion;
            recordCount = 0;
            try
            {
                string sqlcount = dao.SqlBuilder.GetPagingCountSQL(tableviewname, condition);
                string sql = dao.SqlBuilder.GetPagingSQL(tableviewname, key, fields, collatingSequence, condition, pageIndex, pageSize);
                if (db.State != ConnectionState.Open) db.Open();
                object obj = db.ExecuteScalar(sqlcount);
                if (obj == null) obj = "0";
                int.TryParse(obj.ToString(), out recordCount);
                IEnumerable<object> list = db.Query(type, sql, new DynamicParameters(paramDic), null, true, null, default(CommandType?));
                return list;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (db.State == ConnectionState.Open) db.Close();
            }
        }

        /// <summary>
        /// dynamic enum
        /// </summary>
        /// <param name="dao"></param>
        /// <param name="sql"></param>
        /// <param name="paramDic"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public static IEnumerable<dynamic> QueryList(this DapperAdo dao, string sql, object paramDic = null, CommandType? commandType = default(CommandType?))
        {
            IDbConnection db = dao.DbConnecttion;
            try
            {
                if (db.State != ConnectionState.Open) db.Open();
                IEnumerable<dynamic> list = db.Query(sql, new DynamicParameters(paramDic), null, true, null, commandType);
                return list;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (db.State == ConnectionState.Open) db.Close();
            }
        }

        public static IEnumerable<dynamic> QueryList(this DapperAdo dao, string tableviewname, string key, string fields, string collatingSequence, string condition, int pageIndex, int pageSize, out int recordCount, object paramDic = null)
        {
            IDbConnection db = dao.DbConnecttion;
            recordCount = 0;
            try
            {
                string sqlcount = dao.SqlBuilder.GetPagingCountSQL(tableviewname, condition);
                string sql = dao.SqlBuilder.GetPagingSQL(tableviewname, key, fields, collatingSequence, condition, pageIndex, pageSize);
                if (db.State != ConnectionState.Open) db.Open();
                object obj = db.ExecuteScalar(sqlcount);
                if (obj == null) obj = "0";
                int.TryParse(obj.ToString(), out recordCount);

                IEnumerable<dynamic> list = db.Query(sql, new DynamicParameters(paramDic), null, true, null, default(CommandType?));
                return list;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (db.State == ConnectionState.Open) db.Close();
            }
        }

        public static IEnumerable<T> QueryList<T>(this DapperAdo dao, string sql, object paramDic = null, CommandType? commandType = default(CommandType?))
        {
            IDbConnection db = dao.DbConnecttion;
            try
            {
                if (db.State != ConnectionState.Open) db.Open();
                if (paramDic == null)
                {
                    return db.Query<T>(sql, null, null, true, null, commandType);
                }
                else
                {
                    return db.Query<T>(sql, new DynamicParameters(paramDic), null, true, null, commandType);
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (db.State == ConnectionState.Open) db.Close();
            }
        }

        /// <summary>
        /// 分页查询列表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dao"></param>
        /// <param name="sql"></param>
        /// <param name="paramDic"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public static IEnumerable<T> QueryList<T>(this DapperAdo dao, string tableviewname, string key, string fields, string collatingSequence, string condition, int pageIndex, int pageSize, out int recordCount, object paramDic = null, CommandType? commandType = default(CommandType?))
        {
            recordCount = 0;
            IDbConnection db = dao.DbConnecttion;
            string sql = dao.SqlBuilder.GetPagingSQL(tableviewname, key, fields, collatingSequence, condition, pageIndex, pageSize);
            string sqlcount = dao.SqlBuilder.GetPagingCountSQL(tableviewname, condition);
            try
            {
                if (db.State != ConnectionState.Open) db.Open();
                object obj = db.ExecuteScalar(sqlcount);
                if (obj == null) obj = "0";
                int.TryParse(obj.ToString(), out recordCount);

                if (paramDic == null)
                {
                    return db.Query<T>(sql, null, null, true, null, commandType);
                }
                else
                {
                    return db.Query<T>(sql, new DynamicParameters(paramDic), null, true, null, commandType);
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (db.State == ConnectionState.Open) db.Close();
            }
        }

        public static DataTable QueryDataTable(this DapperAdo dao, string tableviewname, string key, string fields, string collatingSequence, string condition, int pageIndex, int pageSize, out int recordcount, object paramDic = null)
        {
            recordcount = 0;
            string sql = dao.SqlBuilder.GetPagingSQL(tableviewname, key, fields, collatingSequence, condition, pageIndex, pageSize);
            string sqlcount = dao.SqlBuilder.GetPagingCountSQL(tableviewname, condition);
            DataTable dt = new DataTable();
            IDbConnection db = dao.DbConnecttion;
            try
            {
                object obj = db.ExecuteScalar(sqlcount);
                if (obj == null) obj = "0";
                int.TryParse(obj.ToString(), out recordcount);

                IDataReader reader = null;
                reader = db.ExecuteReader(sql, paramDic, null, null, null);
                dt = ConvertDataReaderToDataTable(reader);
            }
            catch { throw; }
            finally { if (db.State == ConnectionState.Open) db.Close(); }
            return dt;
        }

        public static DataTable QueryDataTable(this DapperAdo dao, string sql, object paramDic = null, CommandType? commandType = default(CommandType?))
        {
            DataTable dt = new DataTable();
            IDbConnection db = dao.DbConnecttion;
            try
            {
                IDataReader reader = null;
                reader = db.ExecuteReader(sql, paramDic, null, null, commandType);
                dt = ConvertDataReaderToDataTable(reader);
            }
            catch { throw; }
            finally { if (db.State == ConnectionState.Open) db.Close(); }
            return dt;
        }

        private static DataTable ConvertDataReaderToDataTable(IDataReader reader)
        {
            try
            {
                DataTable objDataTable = new DataTable();
                int intFieldCount = reader.FieldCount;
                for (int intCounter = 0; intCounter < intFieldCount; ++intCounter)
                {
                    objDataTable.Columns.Add(reader.GetName(intCounter), reader.GetFieldType(intCounter));
                }

                objDataTable.BeginLoadData();

                object[] objValues = new object[intFieldCount];
                while (reader.Read())
                {
                    reader.GetValues(objValues);
                    objDataTable.LoadDataRow(objValues, true);
                }
                reader.Close();
                objDataTable.EndLoadData();

                return objDataTable;

            }
            catch (Exception ex)
            {
                throw new Exception("转换出错!", ex);
            }

        }

        #endregion


        public static int Insert<T>(this DapperAdo dao, T t, CommandType? commandType = default(CommandType?)) where T : BaseEntity
        {
            int rsd = 0;
            IDbConnection db = dao.DbConnecttion;
            IDbTransaction trans = dao.Trans;
            try
            {
                var sql = dao.SqlBuilder.Insert<T>(t);
                rsd = db.Execute(sql, new DynamicParameters(t), trans, null, commandType);
            }
            catch
            {
                throw;
            }

            finally { if (db.State == ConnectionState.Open && trans == null) db.Close(); }
            return rsd;
        }

        public static int Insert(this DapperAdo dao, string sql, object param = null, CommandType? commandType = default(CommandType?))
        {
            int rsd = 0;
            IDbConnection db = dao.DbConnecttion;
            IDbTransaction trans = dao.Trans;
            try
            {
                if (param != null)
                    rsd = db.Execute(sql, new DynamicParameters(param), trans, null, commandType);
                else rsd = db.Execute(sql, null, trans, null, commandType);
            }
            catch
            {
                throw;
            }

            finally { if (db.State == ConnectionState.Open && trans == null) db.Close(); }
            return rsd;
        }

        public static int Update<T>(this DapperAdo dao, T t, CommandType? commandType = default(CommandType?)) where T : BaseEntity
        {
            int rsd = 0;
            IDbConnection db = dao.DbConnecttion;
            IDbTransaction trans = dao.Trans;
            try
            {
                var sql = dao.SqlBuilder.Updatet<T>(t);
                rsd = db.Execute(sql, new DynamicParameters(t), trans, null, commandType);
            }
            catch
            {
                throw;
            }

            finally { if (db.State == ConnectionState.Open && trans == null) db.Close(); }
            return rsd;
        }

        public static int Update(this DapperAdo dao, string sql, object param = null, CommandType? commandType = default(CommandType?))
        {
            int rsd = 0;
            IDbConnection db = dao.DbConnecttion;
            IDbTransaction trans = dao.Trans;
            try
            {
                if (param != null)
                    rsd = db.Execute(sql, new DynamicParameters(param), trans, null, commandType);
                else rsd = db.Execute(sql, null, trans);
            }
            catch
            {
                throw;
            }

            finally { if (db.State == ConnectionState.Open && trans == null) db.Close(); }
            return rsd;
        }

        public static int Delete<T>(this DapperAdo dao, T t) where T : BaseEntity
        {
            int rsd = 0;
            IDbConnection db = dao.DbConnecttion;
            IDbTransaction trans = dao.Trans;
            try
            {
                var p = new DynamicParameters(t);
                var sql = dao.SqlBuilder.Delete<T>(t);
                rsd = db.Execute(sql, p, trans);
            }
            catch (Exception)
            {
                throw;
            }
            finally { if (db.State == ConnectionState.Open && trans == null) db.Close(); }
            return rsd;
        }

        public static int Execute(this DapperAdo dao, string sql, object param = null)
        {
            int rsd = 0;
            IDbConnection db = dao.DbConnecttion;
            IDbTransaction trans = dao.Trans;
            try
            {
                if (param != null)
                    rsd = db.Execute(sql, new DynamicParameters(param), trans);
                else rsd = db.Execute(sql, null, trans);
            }
            catch
            {
                throw;
            }

            finally { if (db.State == ConnectionState.Open && trans == null) db.Close(); }
            return rsd;
        }

        public static IDbTransaction BeginTrans(this DapperAdo dao)
        {
            IDbConnection db = dao.DbConnecttion;
            if (db.State != ConnectionState.Open) db.Open();
            dao.Trans = db.BeginTransaction();
            return dao.Trans;
        }

        public static void CommitTrans(this DapperAdo dao)
        {
            dao.Trans.Commit();
            if (dao.DbConnecttion.State == ConnectionState.Open) dao.DbConnecttion.Close();
        }

        public static void RollbackTrans(this DapperAdo dao)
        {
            dao.Trans.Rollback();
            if (dao.DbConnecttion.State == ConnectionState.Open) dao.DbConnecttion.Close();
        }
    }
}
