using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace YueWen.Utility.Common
{
    public class StringHelper
    {
        public static int GetLength(string str)
        {
            int result;
            if (str.Length == 0)
            {
                result = 0;
            }
            else
            {
                ASCIIEncoding ascii = new ASCIIEncoding();
                int tempLen = 0;
                byte[] s = ascii.GetBytes(str);
                for (int i = 0; i < s.Length; i++)
                {
                    if (s[i] == 63)
                    {
                        tempLen += 2;
                    }
                    else
                    {
                        tempLen++;
                    }
                }
                result = tempLen;
            }
            return result;
        }

        public static string GetCenterShow(string p, int size, int font)
        {
            string result;
            if (font == 2)
            {
                int s = 0;
                int len = p.Length;
                if (len >= 11)
                {
                    result = p;
                }
                else
                {
                    if (len == 9)
                    {
                        s = 2;
                    }
                    if (len == 8)
                    {
                        s = 2;
                    }
                    if (len == 7)
                    {
                        s = 3;
                    }
                    if (len == 6)
                    {
                        s = 4;
                    }
                    if (len == 5)
                    {
                        s = 4;
                    }
                    if (len == 4)
                    {
                        s = 5;
                    }
                    if (len == 3)
                    {
                        s = 6;
                    }
                    if (len == 2)
                    {
                        s = 7;
                    }
                    string ttt = "";
                    ttt = ttt.PadLeft(s, ' ');
                    result = ttt + p;
                }
            }
            else
            {
                if (font == 1)
                {
                    int len = StringHelper.GetLength(p);
                    int tmp = 34 - len;
                    if (tmp < 0)
                    {
                        result = p;
                    }
                    else
                    {
                        double index = (double)tmp / 2.0;
                        string ttt = "";
                        int s = (int)index;
                        ttt = ttt.PadLeft(s, ' ');
                        result = ttt + p;
                    }
                }
                else
                {
                    result = p;
                }
            }
            return result;
        }

        public static string FormatTextArea(string s)
        {
            s = s.Replace("\n", "<br>");
            s = s.Replace(" ", "&nbsp;");
            return s;
        }

        public static Guid GenerateGuid()
        {
            byte[] guidArray = Guid.NewGuid().ToByteArray();

            var baseDate = new DateTime(1900, 1, 1);
            DateTime now = DateTime.Now;
            var days = new TimeSpan(now.Ticks - baseDate.Ticks);
            TimeSpan msecs = now.TimeOfDay;

            byte[] daysArray = BitConverter.GetBytes(days.Days);
            byte[] msecsArray = BitConverter.GetBytes((long)(msecs.TotalMilliseconds / 3.333333));

            Array.Reverse(daysArray);
            Array.Reverse(msecsArray);

            Array.Copy(daysArray, daysArray.Length - 2, guidArray, guidArray.Length - 6, 2);
            Array.Copy(msecsArray, msecsArray.Length - 4, guidArray, guidArray.Length - 4, 4);

            return new Guid(guidArray);
        }


        public static string GetOmitString(string str, int length, string status)
        {
            string result;
            if (status == "1")
            {
                int i = 0;
                int j = 0;
                for (int k = 0; k < str.Length; k++)
                {
                    if (Regex.IsMatch(str.Substring(k, 1), "[\\u4e00-\\u9fa5]+"))
                    {
                        i += 2;
                    }
                    else
                    {
                        i++;
                    }
                    if (i <= length)
                    {
                        j++;
                    }
                    if (i >= length)
                    {
                        result = str.Substring(0, j) + "...";
                        return result;
                    }
                }
            }
            result = str;
            return result;
        }

        /// <summary>
        /// unicode转中文（符合js规则的）
        /// </summary>
        /// <returns></returns>
        public static string Unicode2Chinese_js(string str)
        {
            return str.FromUnicode();
        }

        /// <summary>
        /// 中文转unicode（符合js规则的）
        /// </summary>
        /// <returns></returns>
        public static string Chinese2Unicode_js(string str)
        {
            return str.ToUnicode();
        }

        public static string ConvertSpellFull(string text)
        {
            #region arr
            int[] array = new int[]
	{
		-20319,
		-20317,
		-20304,
		-20295,
		-20292,
		-20283,
		-20265,
		-20257,
		-20242,
		-20230,
		-20051,
		-20036,
		-20032,
		-20026,
		-20002,
		-19990,
		-19986,
		-19982,
		-19976,
		-19805,
		-19784,
		-19775,
		-19774,
		-19763,
		-19756,
		-19751,
		-19746,
		-19741,
		-19739,
		-19728,
		-19725,
		-19715,
		-19540,
		-19531,
		-19525,
		-19515,
		-19500,
		-19484,
		-19479,
		-19467,
		-19289,
		-19288,
		-19281,
		-19275,
		-19270,
		-19263,
		-19261,
		-19249,
		-19243,
		-19242,
		-19238,
		-19235,
		-19227,
		-19224,
		-19218,
		-19212,
		-19038,
		-19023,
		-19018,
		-19006,
		-19003,
		-18996,
		-18977,
		-18961,
		-18952,
		-18783,
		-18774,
		-18773,
		-18763,
		-18756,
		-18741,
		-18735,
		-18731,
		-18722,
		-18710,
		-18697,
		-18696,
		-18526,
		-18518,
		-18501,
		-18490,
		-18478,
		-18463,
		-18448,
		-18447,
		-18446,
		-18239,
		-18237,
		-18231,
		-18220,
		-18211,
		-18201,
		-18184,
		-18183,
		-18181,
		-18012,
		-17997,
		-17988,
		-17970,
		-17964,
		-17961,
		-17950,
		-17947,
		-17931,
		-17928,
		-17922,
		-17759,
		-17752,
		-17733,
		-17730,
		-17721,
		-17703,
		-17701,
		-17697,
		-17692,
		-17683,
		-17676,
		-17496,
		-17487,
		-17482,
		-17468,
		-17454,
		-17433,
		-17427,
		-17417,
		-17202,
		-17185,
		-16983,
		-16970,
		-16942,
		-16915,
		-16733,
		-16708,
		-16706,
		-16689,
		-16664,
		-16657,
		-16647,
		-16474,
		-16470,
		-16465,
		-16459,
		-16452,
		-16448,
		-16433,
		-16429,
		-16427,
		-16423,
		-16419,
		-16412,
		-16407,
		-16403,
		-16401,
		-16393,
		-16220,
		-16216,
		-16212,
		-16205,
		-16202,
		-16187,
		-16180,
		-16171,
		-16169,
		-16158,
		-16155,
		-15959,
		-15958,
		-15944,
		-15933,
		-15920,
		-15915,
		-15903,
		-15889,
		-15878,
		-15707,
		-15701,
		-15681,
		-15667,
		-15661,
		-15659,
		-15652,
		-15640,
		-15631,
		-15625,
		-15454,
		-15448,
		-15436,
		-15435,
		-15419,
		-15416,
		-15408,
		-15394,
		-15385,
		-15377,
		-15375,
		-15369,
		-15363,
		-15362,
		-15183,
		-15180,
		-15165,
		-15158,
		-15153,
		-15150,
		-15149,
		-15144,
		-15143,
		-15141,
		-15140,
		-15139,
		-15128,
		-15121,
		-15119,
		-15117,
		-15110,
		-15109,
		-14941,
		-14937,
		-14933,
		-14930,
		-14929,
		-14928,
		-14926,
		-14922,
		-14921,
		-14914,
		-14908,
		-14902,
		-14894,
		-14889,
		-14882,
		-14873,
		-14871,
		-14857,
		-14678,
		-14674,
		-14670,
		-14668,
		-14663,
		-14654,
		-14645,
		-14630,
		-14594,
		-14429,
		-14407,
		-14399,
		-14384,
		-14379,
		-14368,
		-14355,
		-14353,
		-14345,
		-14170,
		-14159,
		-14151,
		-14149,
		-14145,
		-14140,
		-14137,
		-14135,
		-14125,
		-14123,
		-14122,
		-14112,
		-14109,
		-14099,
		-14097,
		-14094,
		-14092,
		-14090,
		-14087,
		-14083,
		-13917,
		-13914,
		-13910,
		-13907,
		-13906,
		-13905,
		-13896,
		-13894,
		-13878,
		-13870,
		-13859,
		-13847,
		-13831,
		-13658,
		-13611,
		-13601,
		-13406,
		-13404,
		-13400,
		-13398,
		-13395,
		-13391,
		-13387,
		-13383,
		-13367,
		-13359,
		-13356,
		-13343,
		-13340,
		-13329,
		-13326,
		-13318,
		-13147,
		-13138,
		-13120,
		-13107,
		-13096,
		-13095,
		-13091,
		-13076,
		-13068,
		-13063,
		-13060,
		-12888,
		-12875,
		-12871,
		-12860,
		-12858,
		-12852,
		-12849,
		-12838,
		-12831,
		-12829,
		-12812,
		-12802,
		-12607,
		-12597,
		-12594,
		-12585,
		-12556,
		-12359,
		-12346,
		-12320,
		-12300,
		-12120,
		-12099,
		-12089,
		-12074,
		-12067,
		-12058,
		-12039,
		-11867,
		-11861,
		-11847,
		-11831,
		-11798,
		-11781,
		-11604,
		-11589,
		-11536,
		-11358,
		-11340,
		-11339,
		-11324,
		-11303,
		-11097,
		-11077,
		-11067,
		-11055,
		-11052,
		-11045,
		-11041,
		-11038,
		-11024,
		-11020,
		-11019,
		-11018,
		-11014,
		-10838,
		-10832,
		-10815,
		-10800,
		-10790,
		-10780,
		-10764,
		-10587,
		-10544,
		-10533,
		-10519,
		-10331,
		-10329,
		-10328,
		-10322,
		-10315,
		-10309,
		-10307,
		-10296,
		-10281,
		-10274,
		-10270,
		-10262,
		-10260,
		-10256,
		-10254
	};
            string[] array2 = new string[]
	{
		"a",
		"ai",
		"an",
		"ang",
		"ao",
		"ba",
		"bai",
		"ban",
		"bang",
		"bao",
		"bei",
		"ben",
		"beng",
		"bi",
		"bian",
		"biao",
		"bie",
		"bin",
		"bing",
		"bo",
		"bu",
		"ca",
		"cai",
		"can",
		"cang",
		"cao",
		"ce",
		"ceng",
		"cha",
		"chai",
		"chan",
		"chang",
		"chao",
		"che",
		"chen",
		"cheng",
		"chi",
		"chong",
		"chou",
		"chu",
		"chuai",
		"chuan",
		"chuang",
		"chui",
		"chun",
		"chuo",
		"ci",
		"cong",
		"cou",
		"cu",
		"cuan",
		"cui",
		"cun",
		"cuo",
		"da",
		"dai",
		"dan",
		"dang",
		"dao",
		"de",
		"deng",
		"di",
		"dian",
		"diao",
		"die",
		"ding",
		"diu",
		"dong",
		"dou",
		"du",
		"duan",
		"dui",
		"dun",
		"duo",
		"e",
		"en",
		"er",
		"fa",
		"fan",
		"fang",
		"fei",
		"fen",
		"feng",
		"fo",
		"fou",
		"fu",
		"ga",
		"gai",
		"gan",
		"gang",
		"gao",
		"ge",
		"gei",
		"gen",
		"geng",
		"gong",
		"gou",
		"gu",
		"gua",
		"guai",
		"guan",
		"guang",
		"gui",
		"gun",
		"guo",
		"ha",
		"hai",
		"han",
		"hang",
		"hao",
		"he",
		"hei",
		"hen",
		"heng",
		"hong",
		"hou",
		"hu",
		"hua",
		"huai",
		"huan",
		"huang",
		"hui",
		"hun",
		"huo",
		"ji",
		"jia",
		"jian",
		"jiang",
		"jiao",
		"jie",
		"jin",
		"jing",
		"jiong",
		"jiu",
		"ju",
		"juan",
		"jue",
		"jun",
		"ka",
		"kai",
		"kan",
		"kang",
		"kao",
		"ke",
		"ken",
		"keng",
		"kong",
		"kou",
		"ku",
		"kua",
		"kuai",
		"kuan",
		"kuang",
		"kui",
		"kun",
		"kuo",
		"la",
		"lai",
		"lan",
		"lang",
		"lao",
		"le",
		"lei",
		"leng",
		"li",
		"lia",
		"lian",
		"liang",
		"liao",
		"lie",
		"lin",
		"ling",
		"liu",
		"long",
		"lou",
		"lu",
		"lv",
		"luan",
		"lue",
		"lun",
		"luo",
		"ma",
		"mai",
		"man",
		"mang",
		"mao",
		"me",
		"mei",
		"men",
		"meng",
		"mi",
		"mian",
		"miao",
		"mie",
		"min",
		"ming",
		"miu",
		"mo",
		"mou",
		"mu",
		"na",
		"nai",
		"nan",
		"nang",
		"nao",
		"ne",
		"nei",
		"nen",
		"neng",
		"ni",
		"nian",
		"niang",
		"niao",
		"nie",
		"nin",
		"ning",
		"niu",
		"nong",
		"nu",
		"nv",
		"nuan",
		"nue",
		"nuo",
		"o",
		"ou",
		"pa",
		"pai",
		"pan",
		"pang",
		"pao",
		"pei",
		"pen",
		"peng",
		"pi",
		"pian",
		"piao",
		"pie",
		"pin",
		"ping",
		"po",
		"pu",
		"qi",
		"qia",
		"qian",
		"qiang",
		"qiao",
		"qie",
		"qin",
		"qing",
		"qiong",
		"qiu",
		"qu",
		"quan",
		"que",
		"qun",
		"ran",
		"rang",
		"rao",
		"re",
		"ren",
		"reng",
		"ri",
		"rong",
		"rou",
		"ru",
		"ruan",
		"rui",
		"run",
		"ruo",
		"sa",
		"sai",
		"san",
		"sang",
		"sao",
		"se",
		"sen",
		"seng",
		"sha",
		"shai",
		"shan",
		"shang",
		"shao",
		"she",
		"shen",
		"sheng",
		"shi",
		"shou",
		"shu",
		"shua",
		"shuai",
		"shuan",
		"shuang",
		"shui",
		"shun",
		"shuo",
		"si",
		"song",
		"sou",
		"su",
		"suan",
		"sui",
		"sun",
		"suo",
		"ta",
		"tai",
		"tan",
		"tang",
		"tao",
		"te",
		"teng",
		"ti",
		"tian",
		"tiao",
		"tie",
		"ting",
		"tong",
		"tou",
		"tu",
		"tuan",
		"tui",
		"tun",
		"tuo",
		"wa",
		"wai",
		"wan",
		"wang",
		"wei",
		"wen",
		"weng",
		"wo",
		"wu",
		"xi",
		"xia",
		"xian",
		"xiang",
		"xiao",
		"xie",
		"xin",
		"xing",
		"xiong",
		"xiu",
		"xu",
		"xuan",
		"xue",
		"xun",
		"ya",
		"yan",
		"yang",
		"yao",
		"ye",
		"yi",
		"yin",
		"ying",
		"yo",
		"yong",
		"you",
		"yu",
		"yuan",
		"yue",
		"yun",
		"za",
		"zai",
		"zan",
		"zang",
		"zao",
		"ze",
		"zei",
		"zen",
		"zeng",
		"zha",
		"zhai",
		"zhan",
		"zhang",
		"zhao",
		"zhe",
		"zhen",
		"zheng",
		"zhi",
		"zhong",
		"zhou",
		"zhu",
		"zhua",
		"zhuai",
		"zhuan",
		"zhuang",
		"zhui",
		"zhun",
		"zhuo",
		"zi",
		"zong",
		"zou",
		"zu",
		"zuan",
		"zui",
		"zun",
		"zuo"
	};
            #endregion
            byte[] array3 = new byte[2];
            string text2 = "";
            char[] array4 = text.ToCharArray();
            for (int i = 0; i < array4.Length; i++)
            {
                array3 = Encoding.Default.GetBytes(array4[i].ToString());
                int num = (int)array3[0];
                int num2 = (int)array3[1];
                int num3 = num * 256 + num2 - 65536;
                if (num3 > 0 && num3 < 160)
                {
                    text2 += array4[i];
                }
                else
                {
                    for (int j = array.Length - 1; j >= 0; j--)
                    {
                        if (array[j] < num3)
                        {
                            text2 += array2[j];
                            break;
                        }
                    }
                }
            }
            return text2;
        }

        public static string SafeSql(string str)
        {
            str = str.Trim();
            str = str.Replace("'", "''");
            str = str.Replace(";--", "");
            str = str.Replace("=", "");
            str = str.Replace(" or ", "");
            str = str.Replace(" and ", "");
            return str;
        }

        public static string FilterScript(string html)
        {
            html = new Regex("<script[\\s\\S]+</script*>", RegexOptions.IgnoreCase).Replace(html, string.Empty);
            html = new Regex("<link[\\s\\S]+/>", RegexOptions.IgnoreCase).Replace(html, string.Empty);
            html = new Regex("<style[\\s\\S]+</style*>", RegexOptions.IgnoreCase).Replace(html, string.Empty);
            html = new Regex("<iframe[\\s\\S]+</iframe*>", RegexOptions.IgnoreCase).Replace(html, string.Empty);
            html = new Regex("<frameset[\\s\\S]+</frameset*>", RegexOptions.IgnoreCase).Replace(html, string.Empty);
            return html;
        }

        public static string GetAppSetting(string argkey)
        {
            if (string.IsNullOrEmpty(argkey))
            {
                return string.Empty;
            }
            string text = ConfigurationManager.AppSettings[argkey];
            if (string.IsNullOrEmpty(text))
            {
                text = string.Empty;
            }
            return text;
        }

        public static int GetBytesLength(string text, Encoding encoding)
        {
            int result;
            if (string.IsNullOrEmpty(text))
            {
                result = 0;
            }
            else
            {
                result = encoding.GetBytes(text).Length;
            }
            return result;
        }

        public static int GetBytesLength(string text)
        {
            return GetBytesLength(text, Encoding.Default);
        }

        public static string GetCreateTimeStr(DateTime createTime)
        {
            TimeSpan timeSpan = DateTime.Now - createTime;
            if (timeSpan.TotalDays > 1.0)
            {
                return createTime.ToString("yyyy年M月dd日 H:mm");
            }
            if (timeSpan.TotalHours > 1.0)
            {
                return Math.Truncate(timeSpan.TotalHours) + "小时前";
            }
            if (timeSpan.TotalMinutes > 1.0)
            {
                return Math.Truncate(timeSpan.TotalMinutes) + "分钟前";
            }
            if (timeSpan.TotalSeconds > 0.0)
            {
                return Math.Truncate(timeSpan.TotalSeconds) + "秒前";
            }
            return "1秒前";
        }

        public static string GetEndTimeStr(DateTime endTime)
        {
            DateTime t = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, 0, 0);
            if (t >= endTime)
            {
                return "已结束";
            }
            TimeSpan timeSpan = endTime - DateTime.Now;
            if (timeSpan.TotalDays > 1.0)
            {
                return endTime.ToString("yyyy年M月dd日 H:mm");
            }
            if (timeSpan.TotalHours > 1.0)
            {
                return Math.Truncate(timeSpan.TotalHours) + "小时后";
            }
            return Math.Truncate(timeSpan.TotalMinutes) + "分钟后";
        }

        public static string GetInnerText(string html)
        {
            return Regex.Replace(html, "<[^>]+>", string.Empty);
        }

        public static string GetMapPath(string strPath)
        {
            if (HttpContext.Current != null)
            {
                return HttpContext.Current.Server.MapPath(strPath);
            }
            strPath = strPath.Replace("/", "\\");
            if (strPath.StartsWith("\\"))
            {
                strPath = strPath.Substring(strPath.IndexOf('\\', 1)).TrimStart(new char[]
		{
			'\\'
		});
            }
            strPath = strPath.Replace("~\\", "");
            return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, strPath);
        }

        public static string GetRandomNumber(int length)
        {
            Random random = new Random();
            string text = random.Next(1, 10).ToString();
            for (int i = 1; i < length; i++)
            {
                text += random.Next(10).ToString();
            }
            return text;
        }
        
        public static string SafeBase64Encode(string str)
        {
            string srs = Convert.ToBase64String(Encoding.GetEncoding("utf-8").GetBytes(str));
            srs = srs.Replace("+", "-");
            srs = srs.Replace("/", "_");
            srs = srs.Replace("=", "");
            return srs;
        }

        public static string SafeBase64Decode(string base64str)
        {
            string srs = base64str;
            srs = srs.Replace("-", "+");
            srs = srs.Replace("_", "/");
            int mod = srs.Length % 4;
            while (mod > 0)
            {
                srs += "=";
                mod = srs.Length % 4;
            }
            srs = Encoding.GetEncoding("utf-8").GetString(Convert.FromBase64String(srs));
            return srs;
        }


    }
}
