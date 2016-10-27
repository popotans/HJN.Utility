using Newtonsoft.Json.Converters;
using PWMIS.Core.YueWen.DataProvider;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Xml.Serialization;

namespace HJN.Utility.AllInOne
{
    public class H
    {
        #region 配置操作
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
        #endregion

        #region 字符串操作
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

        /// <summary>
        /// unicode转中文（符合js规则的）
        /// </summary>
        /// <returns></returns>
        public static string Unicode_jsFrom(string str)
        {
            string outStr = str;
            Regex reg = new Regex(@"(?i)\\u([0-9a-f]{4})");
            outStr = reg.Replace(str, delegate(Match m1)
            {
                return ((char)Convert.ToInt32(m1.Groups[1].Value, 16)).ToString();
            });
            return outStr;
        }

        /// <summary>
        /// 中文转unicode（符合js规则的）
        /// </summary>
        /// <returns></returns>
        public static string Unicode_jsTo(string str)
        {
            string outStr = str;
            if (!string.IsNullOrEmpty(str))
            {
                for (int i = 0; i < str.Length; i++)
                {
                    if (Regex.IsMatch(str[i].ToString(), @"[\u4e00-\u9fa5]")) { outStr += "\\u" + ((int)str[i]).ToString("x"); }
                    else { outStr += str[i]; }
                }
            }
            return outStr;
        }

        public static string UnicodeTo(string str)
        {
            string outStr = "";
            if (!string.IsNullOrEmpty(str))
            {
                for (int i = 0; i < str.Length; i++)
                {
                    if (Regex.IsMatch(str[i].ToString(), @"[\u4e00-\u9fa5]")) { outStr += "\\u" + ((int)str[i]).ToString("x"); }
                    else { outStr += str[i]; }
                }
            }
            return outStr;
        }

        public static string UnicodeFrom(string str)
        {
            string outStr = "";
            Regex reg = new Regex(@"(?i)\\u([0-9a-f]{4})");
            outStr = reg.Replace(str, delegate(Match m1)
            {
                return ((char)Convert.ToInt32(m1.Groups[1].Value, 16)).ToString();
            });
            return outStr;
        }

        public static string Chinese2Pinyin(string text)
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


        public static string CleanScript(string html)
        {
            html = new Regex("<script[\\s\\S]+</script*>", RegexOptions.IgnoreCase).Replace(html, string.Empty);
            html = new Regex("<link[\\s\\S]+/>", RegexOptions.IgnoreCase).Replace(html, string.Empty);
            html = new Regex("<style[\\s\\S]+</style*>", RegexOptions.IgnoreCase).Replace(html, string.Empty);
            html = new Regex("<iframe[\\s\\S]+</iframe*>", RegexOptions.IgnoreCase).Replace(html, string.Empty);
            html = new Regex("<frameset[\\s\\S]+</frameset*>", RegexOptions.IgnoreCase).Replace(html, string.Empty);
            return html;
        }

        public static string CleanHtmlToTxt(string html)
        {
            return Regex.Replace(html, "<[^>]+>", string.Empty);
        }

        /// <summary> 
        /// 取得HTML中所有图片的 URL。 
        /// </summary> 
        /// <param name="sHtmlText">HTML代码</param> 
        /// <returns>图片的URL列表</returns> 
        public static string[] GetHtmlImageUrlList(string sHtmlText)
        {
            // 定义正则表达式用来匹配 img 标签 
            Regex regImg = new Regex(@"<img\b[^<>]*?\bsrc[\s\t\r\n]*=[\s\t\r\n]*[""']?[\s\t\r\n]*(?<imgUrl>[^\s\t\r\n""'<>]*)[^<>]*?/?[\s\t\r\n]*>", RegexOptions.IgnoreCase);

            // 搜索匹配的字符串 
            MatchCollection matches = regImg.Matches(sHtmlText);
            int i = 0;
            string[] sUrlList = new string[matches.Count];

            // 取得匹配项列表 
            foreach (Match match in matches)
                sUrlList[i++] = match.Groups["imgUrl"].Value;
            return sUrlList;
        }

        public static int GetBytesLength(string text, Encoding encoding = null)
        {
            if (encoding == null) encoding = Encoding.Default;
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

        public static string RMBDaXie(string numstr)
        {
            decimal num = decimal.Parse(numstr);
            string str = "零壹贰叁肆伍陆柒捌玖";
            string str2 = "万仟佰拾亿仟佰拾万仟佰拾元角分";
            string str3 = "";
            string ch2 = "";
            int nzero = 0;
            num = Math.Round(Math.Abs(num), 2);
            string str4 = ((long)(num * 100m)).ToString();
            int i = str4.Length;
            string result;
            if (i > 15)
            {
                result = "溢出";
            }
            else
            {
                str2 = str2.Substring(15 - i);
                for (int j = 0; j < i; j++)
                {
                    string str5 = str4.Substring(j, 1);
                    int temp = Convert.ToInt32(str5);
                    string ch3;
                    if (j != i - 3 && j != i - 7 && j != i - 11 && j != i - 15)
                    {
                        if (str5 == "0")
                        {
                            ch3 = "";
                            ch2 = "";
                            nzero++;
                        }
                        else
                        {
                            if (str5 != "0" && nzero != 0)
                            {
                                ch3 = "零" + str.Substring(temp, 1);
                                ch2 = str2.Substring(j, 1);
                                nzero = 0;
                            }
                            else
                            {
                                ch3 = str.Substring(temp, 1);
                                ch2 = str2.Substring(j, 1);
                                nzero = 0;
                            }
                        }
                    }
                    else
                    {
                        if (str5 != "0" && nzero != 0)
                        {
                            ch3 = "零" + str.Substring(temp, 1);
                            ch2 = str2.Substring(j, 1);
                            nzero = 0;
                        }
                        else
                        {
                            if (str5 != "0" && nzero == 0)
                            {
                                ch3 = str.Substring(temp, 1);
                                ch2 = str2.Substring(j, 1);
                                nzero = 0;
                            }
                            else
                            {
                                if (str5 == "0" && nzero >= 3)
                                {
                                    ch3 = "";
                                    ch2 = "";
                                    nzero++;
                                }
                                else
                                {
                                    if (i >= 11)
                                    {
                                        ch3 = "";
                                        nzero++;
                                    }
                                    else
                                    {
                                        ch3 = "";
                                        ch2 = str2.Substring(j, 1);
                                        nzero++;
                                    }
                                }
                            }
                        }
                    }
                    if (j == i - 11 || j == i - 3)
                    {
                        ch2 = str2.Substring(j, 1);
                    }
                    str3 = str3 + ch3 + ch2;
                    if (j == i - 1 && str5 == "0")
                    {
                        str3 += '整';
                    }
                }
                if (num == 0m)
                {
                    str3 = "零元整";
                }
                result = str3;
            }
            return result;
        }
        #endregion


        #region 数组

        /// <summary>
        /// 将数组按照分隔拆分为字符串
        /// </summary>
        /// <param name="array"></param>
        /// <param name="separator"></param>
        /// <returns></returns>
        public static string ArrayJoin(object[] array, char separator)
        {
            string s = "";
            foreach (string iem in array)
            {
                s += iem + separator.ToString();
            }
            return s.TrimEnd(separator);
        }

        #endregion

        #region 日期时间操作

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

        #endregion


        #region 加密，编码，哈希
        public static string Base64SafeEncode(string str)
        {
            string srs = Convert.ToBase64String(Encoding.GetEncoding("utf-8").GetBytes(str));
            srs = srs.Replace("+", "-");
            srs = srs.Replace("/", "_");
            srs = srs.Replace("=", "");
            return srs;
        }

        public static string Base64SafeDecode(string base64str)
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

        /// <summary>
        /// DES加密
        /// </summary>
        /// <param name="encryptString">待加密的字符串</param>
        /// <returns></returns>
        public static string DESEncrypt(string encryptString, string key)
        {
            return MyDes.Encode(encryptString, key);
            //if (key.Length < 8)
            //{
            //    while (key.Length != 8)
            //    {
            //        key += "x";
            //    }
            //}
            //else if (key.Length > 8)
            //{
            //    key = key.Substring(0, 8);
            //}
            //byte[] inputByteArray = Encoding.Default.GetBytes(encryptString);

            //DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            //des.Key = ASCIIEncoding.ASCII.GetBytes(key);
            //des.IV = ASCIIEncoding.ASCII.GetBytes(key);

            //MemoryStream ms = new MemoryStream();
            //CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);
            //cs.Write(inputByteArray, 0, inputByteArray.Length);
            //cs.FlushFinalBlock();


            //byte[] cstr = ms.ToArray();
            //StringBuilder ret = new StringBuilder();
            //foreach (byte b in cstr)
            //{
            //    ret.AppendFormat("{0:X2}", b);
            //}

            //return ret.ToString();
        }

        /// <summary>
        /// DES解密
        /// </summary>
        /// <param name="decryptString">待解密的字符串</param>
        /// <returns></returns>
        public static string DESDecrypt(string decryptString, string key)
        {
            return MyDes.Decode(decryptString, key);
            //if (key.Length < 8)
            //{
            //    while (key.Length != 8)
            //    {
            //        key += "x";
            //    }
            //}
            //else if (key.Length > 8)
            //{
            //    key = key.Substring(0, 8);
            //}
            //DESCryptoServiceProvider des = new DESCryptoServiceProvider();

            //byte[] inputByteArray = new byte[decryptString.Length / 2];
            //for (int x = 0; x < decryptString.Length / 2; x++)
            //{
            //    int i = (Convert.ToInt32(decryptString.Substring(x * 2, 2), 16));
            //    inputByteArray[x] = (byte)i;
            //}

            //des.Key = ASCIIEncoding.ASCII.GetBytes(key);
            //des.IV = ASCIIEncoding.ASCII.GetBytes(key);
            //MemoryStream ms = new MemoryStream();
            //CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write);
            //cs.Write(inputByteArray, 0, inputByteArray.Length);
            //cs.FlushFinalBlock();

            //StringBuilder ret = new StringBuilder();

            //return System.Text.Encoding.Default.GetString(ms.ToArray());
        }


        #endregion

        #region 序列化
        public static string ToJson(object obj)
        {
            IsoDateTimeConverter timeFormat = new IsoDateTimeConverter();
            timeFormat.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";
            return Newtonsoft.Json.JsonConvert.SerializeObject(obj, Newtonsoft.Json.Formatting.Indented, timeFormat);
        }
        public static object FromJson(string jsonStr)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject(jsonStr);
        }
        public static T FromJson<T>(string jsonStr)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(jsonStr);
        }
        public static string ToXml(object obj)
        {
            MemoryStream Stream = new MemoryStream();
            XmlSerializer xml = new XmlSerializer(obj.GetType());
            try
            {
                //序列化对象
                xml.Serialize(Stream, obj);
            }
            catch (InvalidOperationException)
            {
                throw;
            }
            Stream.Position = 0;
            StreamReader sr = new StreamReader(Stream);
            string str = sr.ReadToEnd();

            sr.Dispose();
            Stream.Dispose();

            return str;

        }

        public static object FromXML(Type type, string xml)
        {
            try
            {
                using (StringReader sr = new StringReader(xml))
                {
                    XmlSerializer xmldes = new XmlSerializer(type);
                    return xmldes.Deserialize(sr);
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region relfection

        public static DataTable DbReaderToDataTable(IDataReader reader)
        {
            DataTable objDataTable = new DataTable("Table");
            int intFieldCount = reader.FieldCount;
            for (int intCounter = 0; intCounter < intFieldCount; intCounter++)
            {
                objDataTable.Columns.Add(reader.GetName(intCounter).ToUpper(), reader.GetFieldType(intCounter));
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

        public static T DbReaderToModel<T>(IDataReader dr)
        {
            T result;
            try
            {
                try
                {
                    if (dr.Read())
                    {
                        List<string> fieldList = new List<string>(dr.FieldCount);
                        for (int i = 0; i < dr.FieldCount; i++)
                        {
                            fieldList.Add(dr.GetName(i).ToLower());
                        }
                        T model = Activator.CreateInstance<T>();
                        PropertyInfo[] properties = model.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.GetProperty);
                        for (int j = 0; j < properties.Length; j++)
                        {
                            PropertyInfo pi = properties[j];
                            if (fieldList.Contains(pi.Name, StringComparer.OrdinalIgnoreCase))
                            {
                                if (!IsNullOrDBNull(dr[pi.Name]))
                                {
                                    pi.SetValue(model, HackType(dr[pi.Name], pi.PropertyType), null);
                                }
                            }
                        }
                        result = model;
                        return result;
                    }
                }
                finally
                {
                    if (dr != null)
                    {
                        dr.Dispose();
                    }
                }
                result = default(T);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public static List<DataRow> DbGetDataRows(DataTable dt)
        {
            List<DataRow> list = new List<DataRow>();
            foreach (DataRow dr in dt.Rows)
            {
                list.Add(dr);
            }
            return list;
        }

        public static DataRow DbGetDataRow(DataTable dt)
        {
            foreach (DataRow dr in dt.Rows)
            {
                return dr;
            }
            return null;
        }

        public static List<DataRow> DbGetDataRows(string sql, YwDb db, params IDataParameter[] arrParam)
        {
            DataTable dt = db.ExecuteDataTable(sql, arrParam);
            return DbGetDataRows(dt);
        }

        public static DataRow DbGetDataRow(string sql, YwDb db, params IDataParameter[] arrParam)
        {
            DataTable dt = db.ExecuteDataTable(sql, arrParam);
            return DbGetDataRow(dt);
        }

        public static int DbGetDrInt(DataRow dr, string columna, int defval = -1)
        {
            int rs = 0;
            if (int.TryParse(dr[columna].ToString(), out rs))
            {
                return rs;
            }
            else return defval;
        }

        public static long DbGetDrLong(DataRow dr, string columna, long defval = -1)
        {
            long rs = 0;
            if (long.TryParse(dr[columna].ToString(), out rs))
            {
                return rs;
            }
            else return defval;
        }

        public static double DbGetDrDouble(DataRow dr, string columna, double defval = -1)
        {
            double rs = 0;
            if (double.TryParse(dr[columna].ToString(), out rs))
            {
                return rs;
            }
            else return defval;
        }

        public static decimal DbGetDrDecimal(DataRow dr, string columna, decimal defval = -1M)
        {
            decimal rs = 0M;
            if (decimal.TryParse(dr[columna].ToString(), out rs))
            {
                return rs;
            }
            else return defval;
        }

        public static DateTime DbGetDrDateTime(DataRow dr, string columna, DateTime? defval = null)
        {
            DateTime rs = new DateTime(1970, 1, 1);
            if (DateTime.TryParse(dr[columna].ToString(), out rs))
            {
                return rs;
            }
            else
            {
                if (defval == null) return new DateTime(1970, 1, 1);
                else return (DateTime)defval;
            }
        }

        public static string DbGetDrStr(DataRow dr, string columna, string defval = "")
        {
            return dr[columna].ToString();
        }

        public static List<T> DbReaderToList<T>(IDataReader dr)
        {
            List<T> result;
            try
            {
                List<string> fieldList = new List<string>(dr.FieldCount);
                for (int i = 0; i < dr.FieldCount; i++)
                {
                    fieldList.Add(dr.GetName(i).ToLower());
                }
                List<T> list = new List<T>();
                while (dr.Read())
                {
                    T model = Activator.CreateInstance<T>();
                    PropertyInfo[] properties = model.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.GetProperty);
                    for (int j = 0; j < properties.Length; j++)
                    {
                        PropertyInfo property = properties[j];
                        if (fieldList.Contains(property.Name, StringComparer.OrdinalIgnoreCase))
                        {
                            if (!IsNullOrDBNull(dr[property.Name]))
                            {
                                property.SetValue(model, HackType(dr[property.Name], property.PropertyType), null);
                            }
                        }
                    }
                    list.Add(model);
                }
                result = list;
            }
            finally
            {
                if (dr != null)
                {
                    dr.Dispose();
                }
            }
            return result;
        }

        private static object HackType(object value, Type conversionType)
        {
            object result;
            if (conversionType.IsGenericType && conversionType.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
            {
                if (value == null)
                {
                    result = null;
                    return result;
                }
                NullableConverter nullableConverter = new NullableConverter(conversionType);
                conversionType = nullableConverter.UnderlyingType;
            }
            result = Convert.ChangeType(value, conversionType);
            return result;
        }

        private static bool IsNullOrDBNull(object obj)
        {
            return obj is DBNull || string.IsNullOrEmpty(obj.ToString());
        }

        /// <summary>
        /// 获取一个对象的简单属性值 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public static Dictionary<string, object> GetPropertiesValuesWithSimpleType<T>(T t)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            if (t == null)
            {
                return dic;
            }
            PropertyInfo[] properties = t.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);

            if (properties.Length <= 0)
            {
                return dic;
            }
            foreach (PropertyInfo item in properties)
            {
                string name = item.Name;
                object value = item.GetValue(t, null);
                if (item.PropertyType.IsValueType || item.PropertyType.Name.StartsWith("String"))
                {
                    dic.Add(name, value);
                }
                else
                {
                    //GetProperties(value);
                }
            }
            return dic;
        }

        /// <summary>
        /// 从表单中根据元素name的值，为对象赋值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <param name="form"></param>
        /// <returns></returns>
        public static T BindPropertyValue<T>(T t, NameValueCollection form)
        {
            if (t == null) return default(T);
            //  var dic = ObjectHelper.GetPropertiesValuesWithSimpleType<T>(t);
            System.Reflection.PropertyInfo[] properties = t.GetType().GetProperties(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public);
            foreach (System.Reflection.PropertyInfo pi in properties)
            {
                foreach (string key in form.Keys)
                {
                    if (string.Compare(pi.Name, key, true) == 0)
                    {
                        object val = form[key];
                        Type type = pi.PropertyType;
                        val = GetValueByType(type, val);
                        pi.SetValue(t, val, null);
                        break;
                    }
                }
            }
            return t;
        }

        private static object GetValueByType(Type type, object val)
        {
            //  Type type = pi.PropertyType;
            if (type == typeof(int))
            {
                val = int.Parse(val.ToString());
            }
            else if (type == typeof(long))
                val = long.Parse(val.ToString());
            else if (type == typeof(Int32))
                val = Int32.Parse(val.ToString());
            else if (type == typeof(Int64))
                val = Int64.Parse(val.ToString());
            else if (type == typeof(Int16))
                val = Int16.Parse(val.ToString());
            else if (type == typeof(decimal))
                val = decimal.Parse(val.ToString());
            else if (type == typeof(float))
                val = float.Parse(val.ToString());
            else if (type == typeof(DateTime))
                val = DateTime.Parse(val.ToString());
            else val = val.ToString();
            return val;
        }

        #endregion

        /// <summary>
        /// 写出分页
        /// </summary>
        /// <param name="totalPage">页数</param>
        /// <param name="currentPage">当前页</param>
        /// <param name="FieldName">地址栏参数</param>
        /// <param name="FieldValue">地址栏参数值</param>
        /// <returns></returns>
        public static string GetPagedNumStr(int totalPage, int currentPage, Dictionary<string, string> dic, string uriPath = "")
        {
            StringBuilder style = new StringBuilder();
            style.Append("<style type=\"text/css\">");
            style.Append(".pager{ background-color:#fff;}");
            style.Append(" .pager a { text-decoration:none; color:#039;padding:0 7px;} ");
            style.Append(" .pager a:hover { text-decoration:none;padding:0 7px;} ");
            style.Append(" .pager td  { text-decoration:none; border:#e5e5e5 solid 1px;color:#039; text-align:center; }");
            style.Append(" .pager td:hover  { background-color:#eee; border:#06c solid 1px;}");
            style.Append(" .pager .current{ background-color:#06c;color:White; border:#06c solid 1px; padding:0 7px; }");
            style.Append("</style>");

            string pString = "";
            StringBuilder _sb = new StringBuilder();
            if (null != dic)
            {
                foreach (KeyValuePair<string, string> item in dic)
                {
                    // pString += "&" + item.Key + "=" + item.Value;
                    _sb.Append("&").Append(item.Key).Append("=").Append(item.Value);
                }
            }
            pString = _sb.ToString();

            int stepNum = 5;
            int pageRoot = 1;
            totalPage = totalPage == 0 ? 1 : totalPage;
            currentPage = currentPage == 0 ? 1 : currentPage;

            StringBuilder sb = new StringBuilder(style.ToString());
            sb.Append("<table cellpadding=0 cellspacing=1 class=\"pager\">\r<tr>\r");
            sb.Append("<td class=pagerTitle>分页</td>\r");
            sb.Append("<td class=pagerTitle>" + currentPage.ToString() + "/" + totalPage.ToString() + "</td>\r");
            if (currentPage - stepNum < 2)
                pageRoot = 1;
            else
                pageRoot = currentPage - stepNum;
            int pageFoot = totalPage;
            if (currentPage + stepNum >= totalPage)
                pageFoot = totalPage;
            else
                pageFoot = currentPage + stepNum;
            if (pageRoot == 1)
            {
                if (currentPage > 1)
                {
                    sb.Append("<td><a href='" + uriPath + "?page=1" + pString + "' title='首页'>首页</a></td>\r");
                    sb.Append("<td><a href='" + uriPath + "?page=" + Convert.ToString(currentPage - 1) + pString + "' title='上页'>上页</a></td>\r");
                }
            }
            else
            {
                sb.Append("<td><a href='" + uriPath + "?page=1" + pString + "' title='首页'>首页</a>&nbsp;</td>");
                sb.Append("<td><a href='" + uriPath + "?page=" + Convert.ToString(currentPage - 1) + pString + "' title='上页'>上页</a></td>\r");
            }
            for (int i = pageRoot; i <= pageFoot; i++)
            {
                if (i == currentPage)
                {
                    sb.Append("<td class='current'>" + i.ToString() + "</td>\r");
                }
                else
                {
                    sb.Append("<td><a href='" + uriPath + "?page=" + i.ToString() + pString + "' title='第" + i.ToString() + "页'>" + i.ToString() + "</a></td>\r");
                }
                if (i == totalPage)
                    break;
            }
            if (pageFoot == totalPage)
            {
                if (totalPage > currentPage)
                {
                    sb.Append("<td><a href='" + uriPath + "?page=" + Convert.ToString(currentPage + 1) + pString + "' title='下页'>下页</a></td>\r");
                    sb.Append("<td><a href='" + uriPath + "?page=" + totalPage.ToString() + pString + "' title='尾页'>尾页</a></td>\r");
                }
            }
            else
            {
                sb.Append("<td><a href='" + uriPath + "?page=" + Convert.ToString(currentPage + 1) + pString + "' title='下页'>下页</a></td>\r");
                sb.Append("<td><a href='" + uriPath + "?page=" + totalPage.ToString() + pString + "' title='尾页'>尾页</a></td>\r");
            }
            sb.Append("</tr>\r</table>");

            return sb.ToString();
        }

        public static string GetPagedNumStr(int totalPage, int currentPage, string prefix, string suffix)
        {
            StringBuilder style = new StringBuilder();
            style.Append("<style type=\"text/css\">");
            style.Append(".pager{ background-color:#fff;}");
            style.Append(" .pager a { text-decoration:none; color:#039;padding:0 7px;} ");
            style.Append(" .pager a:hover { text-decoration:none;padding:0 7px;} ");
            style.Append(" .pager td  { text-decoration:none; border:#e5e5e5 solid 1px;color:#039; text-align:center; }");
            style.Append(" .pager td:hover  { background-color:#eee; border:#06c solid 1px;}");
            style.Append(" .pager .current{ background-color:#06c;color:White; border:#06c solid 1px; padding:0 7px; }");
            style.Append("</style>");
            int stepNum = 4;
            int pageRoot = 1;
            totalPage = totalPage == 0 ? 1 : totalPage;
            currentPage = currentPage == 0 ? 1 : currentPage;

            StringBuilder sb = new StringBuilder(style.ToString());
            sb.Append("<table cellpadding=0 cellspacing=1 class=\"pager\">\r<tr>\r");
            sb.Append("<td class=pagerTitle>&nbsp;分页&nbsp;</td>\r");
            sb.Append("<td class=pagerTitle>&nbsp;" + currentPage.ToString() + "/" + totalPage.ToString() + "&nbsp;</td>\r");
            if (currentPage - stepNum < 2)
                pageRoot = 1;
            else
                pageRoot = currentPage - stepNum;
            int pageFoot = totalPage;
            if (currentPage + stepNum >= totalPage)
                pageFoot = totalPage;
            else
                pageFoot = currentPage + stepNum;
            if (pageRoot == 1)
            {
                if (currentPage > 1)
                {
                    sb.Append("<td>&nbsp;<a href='" + prefix + "1" + suffix + "' title='首页'>首页</a>&nbsp;</td>\r");
                    sb.Append("<td>&nbsp;<a href='" + prefix + Convert.ToString(currentPage - 1) + suffix + "' title='上页'>上页</a>&nbsp;</td>\r");
                }
            }
            else
            {
                sb.Append("<td>&nbsp;<a href='" + prefix + "1" + suffix + "' title='首页'>首页</a>&nbsp;</td>");
                sb.Append("<td>&nbsp;<a href='" + prefix + Convert.ToString(currentPage - 1) + suffix + "' title='上页'>上页</a>&nbsp;</td>\r");
            }
            for (int i = pageRoot; i <= pageFoot; i++)
            {
                if (i == currentPage)
                {
                    sb.Append("<td class='current'>&nbsp;" + i.ToString() + "&nbsp;</td>\r");
                }
                else
                {
                    sb.Append("<td>&nbsp;<a href='" + prefix + i.ToString() + suffix + "' title='第" + i.ToString() + "页'>" + i.ToString() + "</a>&nbsp;</td>\r");
                }
                if (i == totalPage)
                    break;
            }
            if (pageFoot == totalPage)
            {
                if (totalPage > currentPage)
                {
                    sb.Append("<td>&nbsp;<a href='" + prefix + Convert.ToString(currentPage + 1) + suffix + "' title='下页'>下页</a>&nbsp;</td>\r");
                    sb.Append("<td>&nbsp;<a href='" + prefix + totalPage.ToString() + suffix + "' title='尾页'>尾页</a>&nbsp;</td>\r");
                }
            }
            else
            {
                sb.Append("<td>&nbsp;<a href='" + prefix + Convert.ToString(currentPage + 1) + suffix + "' title='下页'>下页</a>&nbsp;</td>\r");
                sb.Append("<td>&nbsp;<a href='" + prefix + totalPage.ToString() + suffix + "' title='尾页'>尾页</a>&nbsp;</td>\r");
            }
            sb.Append("</tr>\r</table>");
            return sb.ToString();
        }

        #region web
        public static string WebLoadTemplate(string fileFullName)
        {
            //string encoding = "utf-8";
            string result = null;
            if (!fileFullName.StartsWith("~"))
            {
                if (fileFullName.StartsWith("/")) fileFullName = "~" + fileFullName;
                else fileFullName = "~/" + fileFullName;
            }
            string realPath = ((Page)HttpContext.Current.CurrentHandler).ResolveUrl(fileFullName);
            TextWriter tw = new StringWriter();
            HttpContext.Current.Server.Execute(fileFullName, tw);
            result = tw.ToString();
            return result;
        }


        #endregion

    }
}
