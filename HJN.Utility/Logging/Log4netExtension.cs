using log4net.Core;
using log4net.Layout;
using log4net.Layout.Pattern;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace HJN.Utility.Logging
{
    /// <summary>
    /// 定义log日志布局的参数信息
    /// </summary>
    public class HCustomLayout : PatternLayout
    {
        string pattern = "";
        /// <summary>
        /// 构造函数
        /// </summary>
        public HCustomLayout()
        {
            #region 支持开发人员自定义
            //AddConverter("ht", typeof(HashTablePatternConverter));
            ActivateOptions();
            //%ht{name}
            AddConverter("custom", typeof(LogMessagePatternConverter));
            ActivateOptions();
            #endregion
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public HCustomLayout(string pattern)
            : base(pattern)
        {
            this.pattern = pattern;
            #region 支持开发人员自定义
            //  AddConverter("ht", typeof(HashTablePatternConverter));
            AddConverter("custom", typeof(LogMessagePatternConverter));
            //%ht{name}
            #endregion
            ActivateOptions();
        }
    }

    public class LogMessage
    {
        public string AppId { get; set; }
        public string ModuleName { get; set; }
        public string LogType { get; set; }
        public string UserID { get; set; }
        public string OperationName { get; set; }
        public string Code { get; set; }
        public string ClientIP { get; set; }
        public string Uri { get; set; }
        public string Msg { get; set; }
        public string ServerName { get; set; }
        public string Title { get; set; }

        public string InputBody { get; set; }
        public string OutputBody { get; set; }
        public string InvokeTime { get; set; }
        public string ElapsedTimes { get; set; }
        public string AppDomain { get; set; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("AppId=" + AppId + ",");
            sb.Append("ModuleName=" + ModuleName + ",");
            sb.Append("UserID=" + UserID + ",");
            sb.Append("OperationName=" + OperationName + ",");
            sb.Append("Code=" + Code + ",");
            sb.Append("ClientIP=" + ClientIP + ",");
            sb.Append("Uri=" + Uri + ",");
            sb.Append("Msg=" + Msg + ",");
            sb.Append("ServerName=" + ServerName + ",");
            sb.Append("Title=" + Title + ",");
            sb.Append("InputBody=" + InputBody + ",");
            sb.Append("OutputBody=" + OutputBody + ",");
            sb.Append("InvokeTime=" + InvokeTime + ",");
            sb.Append("ElapsedTimes=" + ElapsedTimes + ",");
            sb.Append("AppDomain=" + AppDomain + "");
            return sb.ToString();
        }
    }

    internal class LogMessagePatternConverter : PatternLayoutConverter
    {
        protected override void Convert(TextWriter writer, LoggingEvent loggingEvent)
        {
            if (Option != null)
            {
                // Write the value for the specified key
                WriteObject(writer, loggingEvent.Repository, LookupProperty(Option, loggingEvent));
            }
            else
            {
                // Write all the key value pairs
                WriteDictionary(writer, loggingEvent.Repository, loggingEvent.GetProperties());
            }
        }

        /// <summary>
        /// 通过反射获取传入的日志对象的某个属性的值
        /// </summary>
        /// <param name="property"></param>
        /// <param name="loggingEvent"></param>
        /// <returns></returns>
        private object LookupProperty(string property, LoggingEvent loggingEvent)
        {
            object propertyValue = string.Empty;
            PropertyInfo propertyInfo = loggingEvent.MessageObject.GetType().GetProperty(property);

            //   Hashtable table = loggingEvent.MessageObject as Hashtable;
            try
            {
                if (propertyInfo != null)
                    propertyValue = propertyInfo.GetValue(loggingEvent.MessageObject, null);
                // propertyValue = table[property];
            }
            catch { propertyValue = null; }
            return propertyValue;
        }
    }

    internal class HashTablePatternConverter : PatternLayoutConverter
    {
        protected override void Convert(TextWriter writer, LoggingEvent loggingEvent)
        {
            if (Option != null)
            {
                // Write the value for the specified key
                WriteObject(writer, loggingEvent.Repository, LookupProperty(Option, loggingEvent));
            }
            else
            {
                // Write all the key value pairs
                WriteDictionary(writer, loggingEvent.Repository, loggingEvent.GetProperties());
            }
        }

        /// <summary>
        /// 通过反射获取传入的日志对象的某个属性的值
        /// </summary>
        /// <param name="property"></param>
        /// <param name="loggingEvent"></param>
        /// <returns></returns>
        private object LookupProperty(string property, LoggingEvent loggingEvent)
        {
            object propertyValue = string.Empty;
            //PropertyInfo propertyInfo = loggingEvent.MessageObject.GetType().GetProperty(property);
            //if (propertyInfo != null)
            //    propertyValue = propertyInfo.GetValue(loggingEvent.MessageObject, null);
            Hashtable table = loggingEvent.MessageObject as Hashtable;
            try
            {
                propertyValue = table[property];
            }
            catch { propertyValue = null; }
            return propertyValue;
        }
    }
}
