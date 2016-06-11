using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
//using WinF = System.Windows.Forms;
using System.Xml;
using System.Web.UI;
using System.Configuration;
using System.IO;


namespace YueWen.Utility.SEC
{
    /// <summary>
    /// SEC配置
    /// </summary>
    class SecConfig
    {
        private static Dictionary<object, SecConfig> cachedContext = new Dictionary<object, SecConfig>();

        #region SecConfig Fields
        private string secEmpServiceUrl;
        private string secOrgServiceUrl;
        private string secLoginUrl;
        private string secVerifyUrl;
        private string secLogoutUrl;
        private string loginSiteUrl;

        private string rootPath = string.Empty;
        private XmlDocument k2Dom = null;
        private XmlNodeList excludePages = null;
        private string client_secret = "";
        #endregion

        #region SecConfig Properties
        /// <summary>
        /// 当前站点的标识页面集合（未经登录认证也可访问的页面集合）
        /// </summary>
        public XmlNodeList ExcludePages
        {
            get
            {
                if (excludePages == null)
                {
                    if (GetK2ConfigurationNode("Sec/ExcludePages") != null)
                    {
                        excludePages = GetK2ConfigurationNode("Sec/ExcludePages").ChildNodes;
                    }
                }
                return excludePages;
            }
        }

        /// <summary>
        /// K2配置文件XML文档
        /// </summary>
        public XmlDocument K2Dom
        {
            get
            {
                if (k2Dom == null)
                {
                    k2Dom = new XmlDocument();
                    /*
                     SEC.config支持两种方式，在网站根目录下，或者在网站配置文件中AppSettings节点指定
                     */
                    string secConfigPath = string.Format("{0}sec.Config", rootPath);
#if DEBUG
                    //    Console.WriteLine("K2Dom" + secConfigPath);
#endif
                    if (!System.IO.File.Exists(secConfigPath))
                    {
                        secConfigPath = ConfigurationManager.AppSettings["SecConfigPath"];
                        if (string.IsNullOrEmpty(secConfigPath)) throw new FileNotFoundException("SecConfigPath File sec.config is not Configed :" + secConfigPath);
                        secConfigPath = string.Format("{0}sec.Config", secConfigPath);
                    }

                    k2Dom.Load(secConfigPath);
                }
                return k2Dom;
            }
        }

        public string Client_Secret
        {
            get
            {
                if (string.IsNullOrEmpty(client_secret))
                {
                    LoadConfiguration();
                }
                return client_secret;
            }
            set
            {
                client_secret = value;
            }
        }

        /// <summary>
        /// 员工信息中枢员工服务地址
        /// </summary>
        public string SecEmpServiceUrl
        {
            get
            {
                if (secEmpServiceUrl == null)
                {
                    LoadConfiguration();
                }
                return secEmpServiceUrl;
            }
            set
            {
                secEmpServiceUrl = value;
            }
        }

        /// <summary>
        /// 员工信息中枢组织服务地址
        /// </summary>
        public string SecOrgServiceUrl
        {
            get
            {
                if (secOrgServiceUrl == null)
                {
                    LoadConfiguration();
                }
                return secOrgServiceUrl;
            }
            set
            {
                secOrgServiceUrl = value;
            }
        }

        /// <summary>
        /// 员工信息中枢登录地址
        /// </summary>
        public string SecLoginUrl
        {
            get
            {
                if (secLoginUrl == null)
                {
                    LoadConfiguration();
                }
                return secLoginUrl;
            }
            set
            {
                secLoginUrl = value;
            }
        }

        /// <summary>
        /// 员工信息中枢验证地址
        /// </summary>
        public string SecVerifyUrl
        {
            get
            {
                if (secVerifyUrl == null)
                {
                    LoadConfiguration();
                }
                return secVerifyUrl;
            }
            set
            {
                secVerifyUrl = value;
            }
        }

        /// <summary>
        /// 员工信息中枢注销地址
        /// </summary>
        public string SecLogoutUrl
        {
            get
            {
                if (secLogoutUrl == null)
                {
                    LoadConfiguration();
                }
                return secLogoutUrl;
            }
            set
            {
                secLogoutUrl = value;
            }
        }

        /// <summary>
        /// 当前站点的登陆页面
        /// </summary>
        public string LoginSiteUrl
        {
            get
            {
                if (loginSiteUrl == null)
                {
                    LoadConfiguration();
                }
                return loginSiteUrl;
            }
            set
            {
                loginSiteUrl = value;
            }
        }
        #endregion

        /// <summary>
        /// 当前请求的K2Context实例
        /// </summary>
        public static SecConfig Current
        {
            get
            {
                SecConfig current;
                object keyObject = new object();
                if (HttpContext.Current != null)
                {
                    keyObject = HttpContext.Current;
                }
                else
                {
                    keyObject = System.Diagnostics.Process.GetCurrentProcess();
                }
                if (!cachedContext.TryGetValue(keyObject, out current))
                {
                    current = new SecConfig();
                }
                return current;
            }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public SecConfig()
        {
            if (HttpContext.Current != null)
            {
                rootPath = AppDomain.CurrentDomain.BaseDirectory.Trim('\\', '/') + "\\";// HttpContext.Current.Request.PhysicalApplicationPath;
            }
            else
            {
                //rootPath = WinF.Application.StartupPath.Trim('\\', '/') + "\\";
                rootPath = AppDomain.CurrentDomain.BaseDirectory.Trim('\\', '/') + "\\config\\";
                //if (rootPath.ToLower().IndexOf("k2") > 0)
                //{
                //    rootPath = rootPath + "config\\";
                //}
            }
            if (HttpContext.Current != null)
            {
                //if (HttpContext.Current.Handler != null)
                //{
                //    Page current = HttpContext.Current.Handler as Page;
                //    object objKey = HttpContext.Current;
                //    if (cachedContext.ContainsKey(objKey))
                //    {
                //        cachedContext[objKey] = this;
                //    }
                //    else
                //    {
                //        lock (cachedContext)
                //        {
                //            cachedContext.Add(objKey, this);
                //        }
                //    }
                //    if (current != null)
                //    {
                //        current.Unload += new EventHandler(current_Disposed);
                //    }
                //}
            }
            this.LoadConfiguration();
        }

        /// <summary>
        /// 实例销毁事件，删除缓存SecConfig实例
        /// </summary>
        /// <param name="sender">事件调用者</param>
        /// <param name="e">事件参数</param>
        void current_Disposed(object sender, EventArgs e)
        {
            try
            {
                object key = HttpContext.Current;
                lock (cachedContext)
                {
                    cachedContext.Remove(key);
                }
            }
            catch
            {
            }
        }

        /// <summary>
        /// 读取k2.config文件中SEC的配置节
        /// </summary>
        public void LoadConfiguration()
        {
            secEmpServiceUrl = this.GetK2Configuration("Sec/EmpServiceUrl");
            secOrgServiceUrl = this.GetK2Configuration("Sec/OrgServiceUrl");
            secLoginUrl = this.GetK2Configuration("Sec/LoginUrl");
            secVerifyUrl = this.GetK2Configuration("Sec/VerifyUrl");
            secLogoutUrl = this.GetK2Configuration("Sec/LogoutUrl");
            loginSiteUrl = this.GetK2Configuration("Sec/LoginSiteUrl");
            client_secret = this.GetK2Configuration("Sec/client_secret");
        }

        /// <summary>
        /// 获取XML节点值
        /// </summary>
        /// <param name="config">XML节点</param>
        /// <returns>值</returns>
        public string GetNodeValue(XmlNode config)
        {
            if (config != null)
            {
                if (config is XmlAttribute)
                {
                    return config.Value;
                }
                else if (config is XmlText)
                {
                    return config.Value;
                }
                else
                {
                    return config.InnerText;
                }
            }
            return "";
        }

        /// <summary>
        /// 获取全局配置
        /// </summary>
        /// <param name="key">配置名称</param>
        /// <returns>配置值</returns>
        public string GetK2Configuration(string key)
        {
            return GetNodeValue(GetK2ConfigurationNode(key));
        }

        /// <summary>
        /// 获取全局配置文件中的节点
        /// </summary>
        /// <param name="key">节点名称</param>
        /// <returns>XML配置节点</returns>
        public XmlNode GetK2ConfigurationNode(string key)
        {
            if (K2Dom == null)
            {
                throw new Exception("Can not get k2dom");
            }
            XmlNode config;
            //eHrConnectionString
            try
            {
                config = K2Dom.SelectSingleNode("Configuration/Item[@Name='" + key + "']");
                if (config != null)
                {
                    return config;
                }
            }
            catch
            {
            }
            try
            {
                config = K2Dom.SelectSingleNode("Configuration/" + key);
                if (config != null)
                {
                    return config;
                }
            }
            catch
            {
            }
            return null;
        }
    }
}
