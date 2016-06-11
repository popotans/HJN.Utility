using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Dapper;
namespace HJN.Dapper
{
    public static class DapperAdoExtension
    {
        public static T SingleOrDefault<T>(this DapperAdo dbs, string sql, object paramDic = null, CommandType? commandType = default(CommandType?))
        {
            IDbConnection db = dbs.DbConnecttion;
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

        public static IEnumerable<T> QueryList<T>(this DapperAdo dbs, string sql, object paramDic = null, CommandType? commandType = default(CommandType?))
        {
            IDbConnection db = dbs.DbConnecttion;

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

        public static DataTable QueryTable(this DapperAdo dbs, string sql, object paramDic = null, CommandType? commandType = default(CommandType?))
        {
            DataTable dt = new DataTable();
            IDbConnection db = dbs.DbConnecttion;
            try
            {
                       
            }
            catch { throw; }
            finally { if (db.State == ConnectionState.Open) db.Close(); }
            return dt;
        }

        public static int Insert<T>(this DapperAdo dbs, T t, IDbTransaction trans = null, CommandType? commandType = default(CommandType?)) where T : BaseEntity
        {
            int rsd = 0;
            IDbConnection db = dbs.DbConnecttion;
            try
            {
                var sql = dbs.SqlBuilder.Insert<T>(t);
                rsd = db.Execute(sql, new DynamicParameters(t), trans, null, commandType);
            }
            catch
            {
                throw;
            }

            finally { if (db.State == ConnectionState.Open) db.Close(); }
            return rsd;
        }

        public static int Insert(this DapperAdo dbs, string sql, object param = null, IDbTransaction trans = null, CommandType? commandType = default(CommandType?))
        {
            int rsd = 0;
            IDbConnection db = dbs.DbConnecttion;
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

            finally { if (db.State == ConnectionState.Open) db.Close(); }
            return rsd;
        }

        public static int Update<T>(this DapperAdo dbs, T t, IDbTransaction trans = null, CommandType? commandType = default(CommandType?)) where T : BaseEntity
        {
            int rsd = 0;
            IDbConnection db = dbs.DbConnecttion;
            try
            {
                var sql = dbs.SqlBuilder.Updatet<T>(t);
                rsd = db.Execute(sql, new DynamicParameters(t), trans, null, commandType);
            }
            catch
            {
                throw;
            }

            finally { if (db.State == ConnectionState.Open) db.Close(); }
            return rsd;
        }

        public static int Update(this DapperAdo dbs, string sql, object param = null, IDbTransaction trans = null, CommandType? commandType = default(CommandType?))
        {
            int rsd = 0;
            IDbConnection db = dbs.DbConnecttion;
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

            finally { if (db.State == ConnectionState.Open) db.Close(); }
            return rsd;
        }


        public static int Delete<T>(this DapperAdo dbs, T t, IDbTransaction trans = null) where T : BaseEntity
        {
            int rsd = 0;
            IDbConnection db = dbs.DbConnecttion;
            try
            {
                var p = new DynamicParameters(t);
                var sql = dbs.SqlBuilder.Delete<T>(t);
                rsd = db.Execute(sql, p, trans);
            }
            catch (Exception)
            {
                throw;
            }
            finally { if (db.State == ConnectionState.Open) db.Close(); }
            return rsd;
        }

        public static int Delete(this DapperAdo dbs, string sql, object param = null, IDbTransaction trans = null)
        {
            int rsd = 0;
            IDbConnection db = dbs.DbConnecttion;
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

            finally { if (db.State == ConnectionState.Open) db.Close(); }

            return rsd;
        }
    }
}
