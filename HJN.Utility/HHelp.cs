using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using YueWen.Utility.Common;

namespace YueWen.Utility
{
    public class HHelp
    {
        public static string GetSubstring(string text, int maxLength)
        {
            if (string.IsNullOrEmpty(text))
            {
                return string.Empty;
            }
            int num = 0;
            int num2 = 0;
            for (int i = 0; i < text.Length; i++)
            {
                char c = text[i];
                if (num2 + 2 > maxLength)
                {
                    break;
                }
                if (Encoding.Default.GetBytes(c.ToString()).Length > 1)
                {
                    num2 += 2;
                }
                else
                {
                    num2++;
                }
                num++;
            }
            if (num == text.Length)
            {
                return text;
            }
            return text.Substring(0, num);
        }

        public static string GetSubstring(string text, int maxLength, bool addSymbol)
        {
            if (string.IsNullOrEmpty(text))
            {
                return string.Empty;
            }
            int num = 0;
            int num2 = 0;
            for (int i = 0; i < text.Length; i++)
            {
                char c = text[i];
                if (num2 + 2 > maxLength)
                {
                    break;
                }
                if (Encoding.Default.GetBytes(c.ToString()).Length > 1)
                {
                    num2 += 2;
                }
                else
                {
                    num2++;
                }
                num++;
            }
            if (num == text.Length)
            {
                return text;
            }
            if (addSymbol)
            {
                return text.Substring(0, num) + "…";
            }
            return text.Substring(0, num);
        }

        public static string GetUserIp()
        {
            if (HttpContext.Current == null || HttpContext.Current.Request == null)
            {
                return string.Empty;
            }
            string text = GetSquidForwardIp();
            if (!string.IsNullOrEmpty(text))
            {
                return text;
            }
            text = HttpContext.Current.Request.ServerVariables["HTTP_CLIENT_IP"];
            if (!string.IsNullOrEmpty(text))
            {
                return text;
            }
            return HttpContext.Current.Request.UserHostAddress;
        }

        private static string GetSquidForwardIp()
        {
            string text = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (string.IsNullOrEmpty(text))
            {
                return string.Empty;
            }
            string[] array = text.Split(new char[]
	{
		','
	});
            if (array.Length >= 2)
            {
                return array[0];
            }
            if (array.Length == 1)
            {
                return array[0];
            }
            if (!text.StartsWith("10."))
            {
                return text;
            }
            return string.Empty;
        }



        public static bool IsIdCard(string text)
        {
            string pattern = "^\\d{15}|\\d{17}[\\dx]$";
            Regex regex = new Regex(pattern);
            return regex.IsMatch(text);
        }

        public static int Len(string str)
        {
            int num = 0;
            for (int i = 0; i < str.Length; i++)
            {
                char c = str[i];
                if (c > '\u007f')
                {
                    num += 2;
                }
                else
                {
                    num++;
                }
            }
            return num;
        }
        public static string ReverseString(string text)
        {
            char[] array = text.ToCharArray();
            Array.Reverse(array);
            return new string(array);
        }

        public static string StrDecode(string str)
        {
            str = str.Replace("<br>", "\n");
            str = str.Replace("&gt;", ">");
            str = str.Replace("&lt;", "<");
            str = str.Replace("&nbsp;", " ");
            str = str.Replace("&quot;", "\"");
            return str;
        }

        public static string StrEncode(string str)
        {
            str = str.Replace("&", "&amp;");
            str = str.Replace("'", "''");
            str = str.Replace("\"", "&quot;");
            str = str.Replace(" ", "&nbsp;");
            str = str.Replace("<", "&lt;");
            str = str.Replace(">", "&gt;");
            str = str.Replace("\n", "<br>");
            return str;
        }

        public static string StringRepalce(string strSource, string strRe, string strTo)
        {
            string text = strSource.ToLower();
            string value = strRe.ToLower();
            int num = text.IndexOf(value);
            if (num != -1)
            {
                strSource = strSource.Substring(0, num) + strTo + StringRepalce(strSource.Substring(num + strRe.Length), strRe, strTo);
            }
            return strSource;
        }

        public static string RemoveHtml(string str)
        {
            throw new NotImplementedException();
        }

        public static long ToTimeMillis(DateTime date)
        {
            return (date.Ticks - 621355968000000000L) / 10000L;
        }

        public static string SerailizeToXML(object obj)
        {
            return SerializeHelper.ToXml(obj);
        }

        public static object SerailizeFromXML(Type t, string objxml)
        {
            return SerializeHelper.FromXML(t, objxml);
        }

        public static string SerailizeToJsonSelf(object obj)
        {
            return SerializeHelper.ToJsonSelf(obj);
        }

        public static object SerailizeFromJsonSelf<T>(string objjson)
        {
            return SerializeHelper.FromJsonSelf<T>(objjson);
        }

        public static string GetFileExtension(string filePath)
        {
            FileInfo fileInfo = new FileInfo(filePath);
            return fileInfo.Extension;
        }

        public static string GetFileNameNoExtension(string filePath)
        {
            FileInfo fileInfo = new FileInfo(filePath);
            return fileInfo.Name.Split(new char[]
			{
				'.'
			})[0];
        }

        public static string GetFileName(string filePath)
        {
            FileInfo fileInfo = new FileInfo(filePath);
            return fileInfo.Name;
        }

        public static byte[] StreamToBytes(Stream stream)
        {
            byte[] result;
            try
            {
                byte[] array = new byte[stream.Length];
                stream.Read(array, 0, Convert.ToInt32(stream.Length));
                result = array;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                stream.Close();
            }
            return result;
        }
        public static byte[] FileToBytes(string filePath)
        {
            int fileSize = GetFileSize(filePath);
            byte[] array = new byte[fileSize];
            FileInfo fileInfo = new FileInfo(filePath);
            byte[] result;
            using (FileStream fileStream = fileInfo.Open(FileMode.Open))
            {
                fileStream.Read(array, 0, fileSize);
                result = array;
            }
            return result;
        }
        public static string FileToString(string filePath)
        {
            return FileToString(filePath, Encoding.UTF8);
        }
        public static string FileToString(string filePath, Encoding encoding)
        {
            StreamReader streamReader = new StreamReader(filePath, encoding);
            string result;
            try
            {
                result = streamReader.ReadToEnd();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                streamReader.Close();
            }
            return result;
        }

        public static int GetLineCount(string filePath)
        {
            int result;
            using (StreamReader streamReader = new StreamReader(filePath))
            {
                int num = 0;
                while (streamReader.ReadLine() != null)
                {
                    num++;
                }
                result = num;
            }
            return result;
        }
        public static int GetFileSize(string filePath)
        {
            FileInfo fileInfo = new FileInfo(filePath);
            return (int)fileInfo.Length;
        }

        public static void DeleteFile(string filePath)
        {
            if (IsExistFile(filePath))
            {
                File.Delete(filePath);
            }
        }
        public static void DeleteDirectory(string directoryPath)
        {
            if (IsExistDirectory(directoryPath))
            {
                Directory.Delete(directoryPath, true);
            }
        }

        public static bool IsExistDirectory(string directoryPath)
        {
            return Directory.Exists(directoryPath);
        }

        public static bool IsExistFile(string filePath)
        {
            return File.Exists(filePath);
        }

        public static double GetFileSizeByKB(string filePath)
        {
            FileInfo fileInfo = new FileInfo(filePath);
            return Math.Round(Convert.ToDouble(fileInfo.Length) / 1024.0, 1);
        }

        public static double GetFileSizeByMB(string filePath)
        {
            FileInfo fileInfo = new FileInfo(filePath);
            return Math.Round(Convert.ToDouble(fileInfo.Length) / 1024.0 / 1024.0, 1);
        }

        public static string[] GetFileNames(string directoryPath)
        {
            if (!IsExistDirectory(directoryPath))
            {
                throw new FileNotFoundException();
            }
            return Directory.GetFiles(directoryPath);
        }

        public static string[] GetFileNames(string directoryPath, string searchPattern, bool isSearchChild)
        {
            if (!IsExistDirectory(directoryPath))
            {
                throw new FileNotFoundException();
            }
            string[] files;
            try
            {
                if (isSearchChild)
                {
                    files = Directory.GetFiles(directoryPath, searchPattern, SearchOption.AllDirectories);
                }
                else
                {
                    files = Directory.GetFiles(directoryPath, searchPattern, SearchOption.TopDirectoryOnly);
                }
            }
            catch (IOException ex)
            {
                throw ex;
            }
            return files;
        }

        public static string[] GetDirectories(string directoryPath)
        {
            string[] directories;
            try
            {
                directories = Directory.GetDirectories(directoryPath);
            }
            catch (IOException ex)
            {
                throw ex;
            }
            return directories;
        }

        public static string[] GetDirectories(string directoryPath, string searchPattern, bool isSearchChild)
        {
            string[] directories;
            try
            {
                if (isSearchChild)
                {
                    directories = Directory.GetDirectories(directoryPath, searchPattern, SearchOption.AllDirectories);
                }
                else
                {
                    directories = Directory.GetDirectories(directoryPath, searchPattern, SearchOption.TopDirectoryOnly);
                }
            }
            catch (IOException ex)
            {
                throw ex;
            }
            return directories;
        }

        public static void OpenDirectory(string directoryPath)
        {
            if (!IsExistDirectory(directoryPath))
            {
                return;
            }
            Process.Start(directoryPath);
        }

        public static void OpenFile(string filePath)
        {
            if (!FileHelper.IsExistFile(filePath))
            {
                return;
            }
            Process.Start(filePath);
        }

        public static string GetDirectoryFromFilePath(string filePath)
        {
            FileInfo fileInfo = new FileInfo(filePath);
            DirectoryInfo directory = fileInfo.Directory;
            return directory.FullName;
        }

        public static void CreateDirectory(string directoryPath)
        {
            if (!IsExistDirectory(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
        }

        public const string DefaultCharList = "!\"#$%&'()*+,-./0123456789:;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[\\]^_`abcdefghijklmnopqrstuvwxyz{|}~";
        public const string ReadCharList = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
        private static byte[] GetRandomBytes(int length)
        {
            if (length <= 0)
            {
                return new byte[0];
            }
            RNGCryptoServiceProvider rNGCryptoServiceProvider = new RNGCryptoServiceProvider();
            byte[] array = new byte[length];
            rNGCryptoServiceProvider.GetNonZeroBytes(array);
            return array;
        }
        public static string GetRandomString(int length, string charList)
        {
            int length2 = charList.Length;
            char[] array = new char[length];
            byte[] randomBytes = GetRandomBytes(length);
            for (int i = 0; i < randomBytes.Length; i++)
            {
                array[i] = charList[(int)randomBytes[i] % length2];
            }
            return new string(array);
        }

        public static bool IsEmail(string s)
        {
            string pattern = "^[\\w-]+(\\.[\\w-]+)*@[\\w-]+(\\.[\\w-]+)+$";
            return Regex.IsMatch(s, pattern);
        }

        public static bool IsUrl(string s)
        {
            string pattern = "^(http|https|ftp|rtsp|mms):(\\/\\/|\\\\\\\\)[A-Za-z0-9%\\-_@]+\\.[A-Za-z0-9%\\-_@]+[A-Za-z0-9\\.\\/=\\?%\\-&_~`@:\\+!;]*$";
            return Regex.IsMatch(s, pattern, RegexOptions.IgnoreCase);
        }

        public static bool IsIp(string s)
        {
            string pattern = "^\\d{1,3}\\.\\d{1,3}\\.\\d{1,3}\\.\\d{1,3}$";
            return Regex.IsMatch(s, pattern);
        }

        public static bool isChinese(string s)
        {
            string pattern = "^[\\u4e00-\\u9fa5]{2,}$";
            return Regex.IsMatch(s, pattern);
        }

        public static bool IsNumeric(string s)
        {
            string pattern = "^\\-?[0-9]+$";
            return Regex.IsMatch(s, pattern);
        }
    }
}
