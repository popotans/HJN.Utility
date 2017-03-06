using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Xml;

namespace HJN.Utility.Common
{
    public class AppConfig
    {
        private FileSystemWatcher watcher;
        private string filepath = "";
        private XmlDocument doc;
        XmlElement Root;
        private static object innerlock = new object();
        private Dictionary<string, object> diccache = new Dictionary<string, object>();
        private AppConfig()
        {
            string folder = AppDomain.CurrentDomain.BaseDirectory + "\\config\\" + ConfigurationManager.AppSettings["environment"];
            filepath = folder + "\\appconfig.config";
            this.doc = new XmlDocument();
            this.doc.Load(filepath);
            this.Root = doc.DocumentElement;
            this.watcher = new FileSystemWatcher();
            this.watcher.Path = folder;
            this.watcher.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite | NotifyFilters.FileName | NotifyFilters.DirectoryName;
            this.watcher.Filter = "*.*";// Path.GetFileName(filepath);// "*.xml|*.config";
            this.watcher.EnableRaisingEvents = true;
            this.watcher.Changed += new FileSystemEventHandler(watcher_Changed);

        }
        public static AppConfig Instance = new AppConfig();

        void watcher_Changed(object sender, FileSystemEventArgs e)
        {
            //  Thread.Sleep(5000);
            Monitor.Enter(innerlock);
            try
            {
                diccache = new Dictionary<string, object>();
                this.watcher.EnableRaisingEvents = false;
                doc = new XmlDocument();
                doc.Load(filepath);
                Root = doc.DocumentElement;
                Console.WriteLine(filepath + " is changed ");
                this.watcher.EnableRaisingEvents = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                this.watcher.EnableRaisingEvents = true;
            }
            finally
            {
                Monitor.Exit(innerlock);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="csspath"></param>
        /// <returns></returns>
        public List<string> GetTextList(string csspath)
        {
            if (diccache.ContainsKey(csspath)) return (List<string>)diccache[csspath];
            List<string> list = new List<string>();
            XmlNodeList xnl = Root.SelectNodes(csspath);
            foreach (XmlNode xn in xnl)
            {
                string text = xn.InnerText;
                list.Add(text);
            }
            diccache.Add(csspath, list);
            return list;
        }

        public string GetText(string csspath)
        {
            if (diccache.ContainsKey(csspath)) return (string)diccache[csspath];
            XmlNode xnl = Root.SelectSingleNode(csspath);
            if (xnl != null)
            {
                diccache.Add(csspath, xnl.InnerText);
                return xnl.InnerText;
            }
            return "";
        }

        public string GetTextAttr(string csspath, string attributekeyname)
        {
            if (diccache.ContainsKey(csspath + attributekeyname)) return (string)diccache[csspath + attributekeyname];
            XmlNodeList xnl = Root.SelectNodes(csspath);
            if (xnl != null)
            {
                foreach (XmlNode xn in xnl)
                {
                    XmlAttribute xnlattr = xn.Attributes["key"];
                    if (xnlattr == null) return string.Empty;
                    if (attributekeyname == xnlattr.Value)
                    {
                        XmlAttribute xnlattrvalue = xn.Attributes["value"];
                        if (xnlattrvalue == null) return string.Empty;
                        string attr = xnlattrvalue.Value;
                        diccache.Add(csspath + attributekeyname, attr);
                        return attr;
                    }
                }
            }
            return "";
        }

        ///// <summary>
        ///// 获取合并发送邮件的白名单设置,返回的数据为小写
        ///// </summary>
        ///// <returns></returns>
        //public List<string> GetMergeEmailSendWhiteList()
        //{
        //    List<string> list = new List<string>();
        //    list = GetTextList("mergemailwhitelist/emp");
        //    list.ForEach(x => x = x.ToLower());
        //    return list.Distinct().ToList();
        //}

    }
}
