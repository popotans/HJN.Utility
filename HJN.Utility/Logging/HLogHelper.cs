using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;
using log4net.Core;
using log4net.Repository;
using log4net.Appender;
using log4net.Layout;
using log4net.Filter;
using log4net.Config;
using System.Data;
using System.Configuration;

namespace YueWen.Utility.Logging
{
    public class HDefaultLog : IYWLog
    {
        public static HDefaultLog Logger = CreateFileLogger("Logs");

        ILog InfoLog;
        ILog ErrorLog;
        ILog DebugLog;
        private bool IncludeDB { get; set; }
        private string AppID { get; set; }
        private string RootPath { get; set; }
        private bool IncludeFile { get; set; }
        private string DbConnectionstring { get; set; }
        private string LogTableName = "Log4NetLog";

        private HDefaultLog(string name)
        {
            this.IncludeFile = true;
            //InfoLog = GetLogger(name, Level.Info);
            //ErrorLog = GetLogger(name, Level.Error);
            //DebugLog = GetLogger(name, Level.Debug);
        }

        private HDefaultLog(string AppID, string name, bool includeDB = false)
        {
            this.IncludeFile = true;
            this.IncludeDB = includeDB;
            this.AppID = AppID;
            //InfoLog = GetLogger(name, Level.Info);
            //ErrorLog = GetLogger(name, Level.Error);
            //DebugLog = GetLogger(name, Level.Debug);
        }

        private HDefaultLog(string AppID, string name, bool includeFile, bool includeDB = false)
        {
            if (includeDB == false && includeFile == false)
            {
                throw new ArgumentException("文件和数据库至少指定一个存储");
            }
            this.IncludeFile = includeFile;
            this.IncludeDB = includeDB;
            this.AppID = AppID;
            //InfoLog = GetLogger(name, Level.Info);
            //ErrorLog = GetLogger(name, Level.Error);
            //DebugLog = GetLogger(name, Level.Debug);
        }

        private HDefaultLog(string rootPath, string AppID, string name, bool includeDB = false)
        {
            this.IncludeFile = true;
            this.RootPath = rootPath;
            this.IncludeDB = includeDB;
            this.AppID = AppID;
            //InfoLog = GetLogger(name, Level.Info);
            //ErrorLog = GetLogger(name, Level.Error);
            //DebugLog = GetLogger(name, Level.Debug);
        }

        private HDefaultLog(string rootPath, string AppID, string name, bool includeFile, bool includeDB = false)
        {
            if (includeDB == false && includeFile == false)
            {
                throw new ArgumentException("文件和数据库至少指定一个存储");
            }
            this.IncludeFile = includeFile;
            this.RootPath = rootPath;
            this.IncludeDB = includeDB;
            this.AppID = AppID;
            //InfoLog = GetLogger(name, Level.Info);
            //ErrorLog = GetLogger(name, Level.Error);
            //DebugLog = GetLogger(name, Level.Debug);
        }

        private HDefaultLog(string name, bool includeDB)
        {
            this.IncludeDB = includeDB;
            //InfoLog = GetLogger(name, Level.Info);
            //ErrorLog = GetLogger(name, Level.Error);
            //DebugLog = GetLogger(name, Level.Debug);
        }

        #region creator
        public static HDefaultLog CreateFileLogger(string moduleName)
        {
            var logger = new HDefaultLog(moduleName);
            logger.InitLogger(moduleName);
            return logger;
        }
        public static HDefaultLog CreateFileLogger(string appcode, string moduleName)
        {
            var logger = new HDefaultLog(appcode, moduleName, false);
            logger.InitLogger(moduleName);
            return logger;

        }
        public static HDefaultLog CreateFileLogger(string rootPath, string appcode, string moduleName)
        {
            var logger = new HDefaultLog(rootPath, appcode, moduleName, false);
            logger.InitLogger(moduleName);
            return logger;
        }

        public static HDefaultLog CreateDBLogger(string moduleName, string tableName, string Connectionstring)
        {
            var logger = new HDefaultLog(moduleName, true);
            logger.LogTableName = tableName;
            logger.DbConnectionstring = Connectionstring;
            logger.IncludeFile = false;
            logger.InitLogger(moduleName);
            return logger;
        }
        public static HDefaultLog CreateDBLogger(string appcode, string moduleName, string tableName, string Connectionstring)
        {
            var logger = new HDefaultLog(appcode, moduleName, false, true);
            logger.LogTableName = tableName;
            logger.DbConnectionstring = Connectionstring;
            logger.InitLogger(moduleName);
            return logger;
        }
        public static HDefaultLog CreateDBLogger(string rootPath, string appcode, string moduleName, string tableName, string Connectionstring)
        {
            var logger = new HDefaultLog(rootPath, appcode, moduleName, false, true);
            logger.LogTableName = tableName;
            logger.DbConnectionstring = Connectionstring;
            logger.InitLogger(moduleName);
            return logger;
        }

        public static HDefaultLog CreateFileDBLogger(string moduleName, string tableName, string Connectionstring)
        {
            var logger = new HDefaultLog(moduleName, true);
            logger.LogTableName = tableName;
            logger.DbConnectionstring = Connectionstring;
            logger.IncludeFile = true;
            logger.InitLogger(moduleName);
            return logger;
        }

        public static HDefaultLog CreateFileDBLogger(string appcode, string moduleName, string tableName, string Connectionstring)
        {
            var logger = new HDefaultLog(appcode, moduleName, true);
            logger.LogTableName = tableName;
            logger.DbConnectionstring = Connectionstring;
            logger.IncludeFile = true;
            logger.InitLogger(moduleName);
            return logger;
        }

        public static HDefaultLog CreateFileDBLoggerwithPath(string rootPath, string moduleName, string tableName, string Connectionstring)
        {
            var logger = new HDefaultLog(moduleName, true);
            logger.LogTableName = tableName;
            logger.DbConnectionstring = Connectionstring;
            logger.RootPath = rootPath;
            logger.IncludeFile = true;
            logger.InitLogger(moduleName);
            return logger;
        }

        public static HDefaultLog CreateFileDBLogger(string rootPath, string appcode, string moduleName, string tableName, string Connectionstring)
        {
            var logger = new HDefaultLog(rootPath, appcode, moduleName, true, true);
            logger.LogTableName = tableName;
            logger.DbConnectionstring = Connectionstring;
            logger.IncludeFile = true;
            logger.InitLogger(moduleName);
            return logger;
        }

        #endregion

        #region
        public void Info(object msg)
        {
            InfoLog.Info(msg);
        }

        public void Info(object msg, Exception ex)
        {
            InfoLog.Info(msg, ex);
        }

        public void InfoFormat(string msg, params object[] args)
        {
            InfoLog.InfoFormat(msg, args);
        }

        public void Error(object msg)
        {
            ErrorLog.Error(msg);
        }
        public void Error(object msg, Exception ex)
        {
            ErrorLog.Error(msg, ex);
        }
        public void ErrorFormat(string msg, params object[] args)
        {
            ErrorLog.ErrorFormat(msg, args);
        }

        public void Debug(object msg)
        {
            DebugLog.Debug(msg);
        }
        public void Debug(object msg, Exception ex)
        {
            DebugLog.Debug(msg, ex);
        }
        public void DebugFormat(string msg, params object[] args)
        {
            DebugLog.DebugFormat(msg, args);
        }
        #endregion

        private void InitLogger(string name)
        {
            InfoLog = GetLogger(name, Level.Info);
            ErrorLog = GetLogger(name, Level.Error);
            DebugLog = GetLogger(name, Level.Debug);
        }

        private IAppender GetAdoLogger(ILoggerRepository repository)
        {
            /*
   IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Log]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Log4NetLog](
[Id] [int] IDENTITY(1,1) NOT NULL,
[Date] [datetime] NOT NULL,
[AppID] [varchar(50)] NULL,
[Thread] [varchar](255) NULL,
[Level] [varchar](50) NULL,
[Logger] [varchar](255) NULL,
[Message] [varchar](4000) NULL,
[Exception] [varchar](4000) NULL,
PRIMARY KEY CLUSTERED 
(
[Id] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
   */
            //  LoadFileAppender();

            log4net.Repository.Hierarchy.Hierarchy hier =
             repository as log4net.Repository.Hierarchy.Hierarchy;
            if (hier != null)
            {
                var concollection = this.DbConnectionstring;// ConfigurationManager.ConnectionStrings["Log4NetDbString"];
                string _ConnectionString = "";
                if (concollection != null)
                {
                    //_ConnectionString = concollection.ConnectionString;
                    //_Provider = concollection.ProviderName;
                    //    if (string.IsNullOrEmpty(_ConnectionString))
                    //        _ConnectionString = "server =10.97.178.251; uid=sa; database=cnblogs; password=niejunhua; Connect Timeout=30; Connection Lifetime =120;  Pooling=true; Max Pool Size=10000; Min Pool Size=5;";
                    _ConnectionString = this.DbConnectionstring;
                }
                if (string.IsNullOrEmpty(_ConnectionString)) return null;
                log4net.Appender.AdoNetAppender adoAppender = new log4net.Appender.AdoNetAppender();
                adoAppender.Name = "AdoNetAppender";
                adoAppender.CommandType = CommandType.Text;
                adoAppender.BufferSize = 1;
                adoAppender.ConnectionType = "System.Data.SqlClient.SqlConnection, System.Data, Version=1.0.3300.0, Culture=neutral, PublicKeyToken=b77a5c561934e089";
                adoAppender.ConnectionString = _ConnectionString;
                StringBuilder sb = new StringBuilder("INSERT INTO [" + LogTableName + "] (");
                sb.Append("[Date],[Thread],[Level],[Logger],[Message],[Exception]");
                sb.Append(",[AppId]");
                sb.Append(",[ModuleName]");
                sb.Append(",[LogType],[UserID],[OperationName],[Code],[ClientIP],[Uri],[Msg],[ServerName]");
                sb.Append(",[Title],[InputBody],[OutputBody],[InvokeTime],[ElapsedTimes],[AppDomain]");
                //sb.Append("");
                sb.Append(")values(");
                sb.Append("@log_date,@thread,@log_level,@logger,@message,@exception");
                sb.Append(",@AppId");
                sb.Append(",@ModuleName");
                sb.Append(",@LogType,@UserID,@OperationName,@Code,@ClientIP,@Uri,@Msg,@ServerName");
                sb.Append(",@Title,@InputBody,@OutputBody,@InvokeTime,@ElapsedTimes,@AppDomain");

                //sb.Append("");
                sb.Append(")");
                adoAppender.CommandText = sb.ToString();
                //  adoAppender.CommandText = @"INSERT INTO Log4NetLog ([Date],[Thread],[AppID],[Level],[Logger],[Message],[Exception],ht1,ht2) VALUES (@log_date, @thread,'" + appid + "', @log_level, @logger, @message, @exception,@ht1,@ht2)";

                adoAppender.AddParameter(new AdoNetAppenderParameter { ParameterName = "@log_date", DbType = System.Data.DbType.DateTime, Layout = new log4net.Layout.RawTimeStampLayout() });
                adoAppender.AddParameter(new AdoNetAppenderParameter { ParameterName = "@thread", DbType = System.Data.DbType.String, Size = 255, Layout = new Layout2RawLayoutAdapter(new PatternLayout("%thread")) });
                adoAppender.AddParameter(new AdoNetAppenderParameter { ParameterName = "@log_level", DbType = System.Data.DbType.String, Size = 50, Layout = new Layout2RawLayoutAdapter(new PatternLayout("%level")) });
                adoAppender.AddParameter(new AdoNetAppenderParameter { ParameterName = "@logger", DbType = System.Data.DbType.String, Size = 255, Layout = new Layout2RawLayoutAdapter(new PatternLayout("%logger")) });
                adoAppender.AddParameter(new AdoNetAppenderParameter { ParameterName = "@message", DbType = System.Data.DbType.String, Size = 4000, Layout = new Layout2RawLayoutAdapter(new PatternLayout("")) });//%message
                adoAppender.AddParameter(new AdoNetAppenderParameter { ParameterName = "@exception", DbType = System.Data.DbType.String, Size = 4000, Layout = new Layout2RawLayoutAdapter(new ExceptionLayout()) });

                adoAppender.AddParameter(new AdoNetAppenderParameter
                {
                    ParameterName = "@AppId",
                    DbType = System.Data.DbType.String,
                    Size = 4000,
                    Layout = new Layout2RawLayoutAdapter(new HCustomLayout("%custom{AppId}"))
                });
                adoAppender.AddParameter(new AdoNetAppenderParameter
                {
                    ParameterName = "@ModuleName",
                    DbType = System.Data.DbType.String,
                    Size = 4000,
                    Layout = new Layout2RawLayoutAdapter(new HCustomLayout("%custom{ModuleName}"))
                });
                adoAppender.AddParameter(new AdoNetAppenderParameter
                {
                    ParameterName = "@LogType",
                    DbType = System.Data.DbType.String,
                    Size = 4000,
                    Layout = new Layout2RawLayoutAdapter(new HCustomLayout("%custom{LogType}"))
                });
                adoAppender.AddParameter(new AdoNetAppenderParameter
                {
                    ParameterName = "@UserID",
                    DbType = System.Data.DbType.String,
                    Size = 4000,
                    Layout = new Layout2RawLayoutAdapter(new HCustomLayout("%custom{UserID}"))
                });
                adoAppender.AddParameter(new AdoNetAppenderParameter
                {
                    ParameterName = "@OperationName",
                    DbType = System.Data.DbType.String,
                    Size = 4000,
                    Layout = new Layout2RawLayoutAdapter(new HCustomLayout("%custom{OperationName}"))
                });
                adoAppender.AddParameter(new AdoNetAppenderParameter
                {
                    ParameterName = "@Code",
                    DbType = System.Data.DbType.String,
                    Size = 4000,
                    Layout = new Layout2RawLayoutAdapter(new HCustomLayout("%custom{Code}"))
                });
                adoAppender.AddParameter(new AdoNetAppenderParameter
                {
                    ParameterName = "@ClientIP",
                    DbType = System.Data.DbType.String,
                    Size = 4000,
                    Layout = new Layout2RawLayoutAdapter(new HCustomLayout("%custom{ClientIP}"))
                });
                adoAppender.AddParameter(new AdoNetAppenderParameter
                {
                    ParameterName = "@Uri",
                    DbType = System.Data.DbType.String,
                    Size = 4000,
                    Layout = new Layout2RawLayoutAdapter(new HCustomLayout("%custom{Uri}"))
                });
                adoAppender.AddParameter(new AdoNetAppenderParameter
                {
                    ParameterName = "@Msg",
                    DbType = System.Data.DbType.String,
                    Size = 4000,
                    Layout = new Layout2RawLayoutAdapter(new HCustomLayout("%custom{Msg}"))
                });
                adoAppender.AddParameter(new AdoNetAppenderParameter
                {
                    ParameterName = "@ServerName",
                    DbType = System.Data.DbType.String,
                    Size = 4000,
                    Layout = new Layout2RawLayoutAdapter(new HCustomLayout("%custom{ServerName}"))
                });
                adoAppender.AddParameter(new AdoNetAppenderParameter
                {
                    ParameterName = "@Title",
                    DbType = System.Data.DbType.String,
                    Size = 4000,
                    Layout = new Layout2RawLayoutAdapter(new HCustomLayout("%custom{Title}"))
                });
                adoAppender.AddParameter(new AdoNetAppenderParameter
                {
                    ParameterName = "@InputBody",
                    DbType = System.Data.DbType.String,
                    Size = 4000,
                    Layout = new Layout2RawLayoutAdapter(new HCustomLayout("%custom{InputBody}"))
                });
                adoAppender.AddParameter(new AdoNetAppenderParameter
                {
                    ParameterName = "@OutputBody",
                    DbType = System.Data.DbType.String,
                    Size = 4000,
                    Layout = new Layout2RawLayoutAdapter(new HCustomLayout("%custom{OutputBody}"))
                });
                adoAppender.AddParameter(new AdoNetAppenderParameter
                {
                    ParameterName = "@InvokeTime",
                    DbType = System.Data.DbType.DateTime,
                    Size = 4000,
                    Layout = new Layout2RawLayoutAdapter(new HCustomLayout("%custom{InvokeTime}"))
                });
                adoAppender.AddParameter(new AdoNetAppenderParameter
                {
                    ParameterName = "@ElapsedTimes",
                    DbType = System.Data.DbType.String,
                    Size = 4000,
                    Layout = new Layout2RawLayoutAdapter(new HCustomLayout("%custom{ElapsedTimes}"))
                });
                adoAppender.AddParameter(new AdoNetAppenderParameter
                {
                    ParameterName = "@AppDomain",
                    DbType = System.Data.DbType.String,
                    Size = 4000,
                    Layout = new Layout2RawLayoutAdapter(new HCustomLayout("%custom{AppDomain}"))
                });

                adoAppender.ActivateOptions();
                //BasicConfigurator.Configure(adoAppender);
                return adoAppender;
            }
            return null;
        }

        /// <summary>
        /// mail#info
        /// </summary>
        /// <param name="repositoryName"></param>
        /// <returns></returns>
        private ILog GetLogger(string repositoryName, Level level)
        {
            if (string.IsNullOrEmpty(repositoryName)) return LogManager.GetLogger("Defalut");

            string getkey = repositoryName + level.ToString();
            ILoggerRepository repository = null;
            try
            {
                repository = LogManager.GetRepository(getkey);
            }
            catch (Exception) { }

            //找到直接返回ilog
            if (repository != null)
                return LogManager.GetLogger(getkey, "Defalut");

            //未找到则创建，多线程下很有可能创建时，就存在了
            try
            {
                repository = LogManager.CreateRepository(getkey);
            }
            catch (Exception)
            {
                repository = LogManager.GetRepository(getkey);
            }


            #region create fileappender
            if (this.IncludeFile)
            {
                //查找输出Appender
                RollingFileAppender fileAppender = new RollingFileAppender();
                fileAppender.Name = "RollingFileAppender_" + getkey;
                fileAppender.RollingStyle = RollingFileAppender.RollingMode.Date;
                fileAppender.AppendToFile = true;
                fileAppender.DatePattern = "yyyyMMdd'.txt'";
                fileAppender.StaticLogFileName = false;
                //\\mail\info\ \mail\error

                if (repositoryName.ToLower().EndsWith("info"))
                    repository.Threshold = Level.Info;
                if (repositoryName.ToLower().EndsWith("error"))
                    repository.Threshold = Level.Error;
                else
                    repository.Threshold = Level.All;

                PatternLayout patternLayout = new PatternLayout();

                patternLayout.Header = "";
                //patternLayout.ConversionPattern = "记录时间：%date 线程ID:[%thread] 日志级别：%-5level 出错类：%logger property:[%property{NDC}] - 错误描述：%message%newline";
                patternLayout.ConversionPattern = "%date [msg: %message] %newline";
                patternLayout.ActivateOptions();
                fileAppender.Layout = patternLayout;

                string file = AppDomain.CurrentDomain.BaseDirectory + "_logs\\" + repositoryName + "\\";
                if (!string.IsNullOrEmpty(AppID) && !string.IsNullOrEmpty(AppID))
                {
                    file = AppDomain.CurrentDomain.BaseDirectory + "\\_logs\\" + AppID + "\\" + repositoryName + "\\";
                }

                LevelRangeFilter lrf = new LevelRangeFilter();
                if (level == Level.Info)
                {
                    lrf.LevelMin = Level.All;
                    lrf.LevelMax = Level.Info;
                    file += "info\\";
                }
                else if (level == Level.Error)
                {
                    lrf.LevelMin = Level.Error;
                    lrf.LevelMax = Level.Fatal;
                    file += "error\\";
                }
                else if (level == Level.Debug)
                {
                    lrf.LevelMin = Level.Debug;
                    lrf.LevelMax = Level.Debug;
                    file += "debug\\";
                }
                else
                {
                    lrf.LevelMin = Level.All;
                    lrf.LevelMax = Level.Fatal;
                    file += "other\\";
                }

                fileAppender.AddFilter(lrf);
                fileAppender.File = file;

                //LevelMatchFilter lmf = new LevelMatchFilter();

                //if (repositoryName.ToLower().EndsWith("info"))
                //    lmf.LevelToMatch = Level.Info;
                //if (repositoryName.ToLower().EndsWith("error"))
                //    lmf.LevelToMatch = Level.Error;
                //else
                //    lmf.LevelToMatch = Level.All;

                //选择UTF8编码，确保中文不乱码。
                fileAppender.Encoding = System.Text.Encoding.UTF8;
                fileAppender.ActivateOptions();

                BasicConfigurator.Configure(repository, fileAppender);
            }
            #endregion

            if (this.IncludeDB)
            {
                IAppender adoAppender = GetAdoLogger(repository);
                if (adoAppender != null)
                    BasicConfigurator.Configure(repository, adoAppender);
            }


            //  return LogManager.GetLogger(getkey, System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            return LogManager.GetLogger(getkey, getkey);
        }
    }

    public class YWLogHelper
    {
        public static HDefaultLog Logger;

        static YWLogHelper()
        {
            Logger = HDefaultLog.CreateFileLogger("Log");
        }

        #region instance
        private HDefaultLog __Logger;
        public YWLogHelper(string name)
        {
            __Logger = HDefaultLog.CreateFileLogger(name);
        }
        public HDefaultLog InstanceLogger
        {
            get
            {
                return __Logger;
            }
        }

        #endregion
    }

    public interface IYWLog
    {
        void Info(object msg);
        void InfoFormat(string msg, params object[] args);
        void Error(object msg);
        void Error(object msg, Exception ex);
        void ErrorFormat(string msg, params object[] args);
        void Debug(object msg);
        void Debug(object msg, Exception ex);
        void DebugFormat(string msg, params object[] args);
    }

    public class LogRequestInfo
    {
        /// <summary>
        /// 应用名称
        /// </summary>
        public string AppID { get; set; }
        /// <summary>
        /// 模块名称
        /// </summary>
        public string CategoryName { get; set; }
        /// <summary>
        /// 日志级别
        /// </summary>
        public string LogLevel { get; set; }
        /// <summary>
        /// 主机名
        /// </summary>
        public string MachineName { get; set; }
        /// <summary>
        /// 应用程序及
        /// </summary>
        public string AssemblyName { get; set; }
        /// <summary>
        /// 应用程序域
        /// </summary>
        public string AppdomainName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ThreadIdentity { get; set; }
        public string WindowsIdentity { get; set; }

        public DateTime LogTime { get; set; }

        public string Title { get; set; }

        public string Message { get; set; }
        public string StackTrace { get; set; }
        public string ExceptionType { get; set; }
        public string TargetSite { get; set; }

    }




}
