using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HJN.Utility.Common
{
    public class ConvertHelper
    {
        public static byte[] StringToBytes(string text)
        {
            return Encoding.Default.GetBytes(text);
        }

        public static byte[] StringToBytes(string text, Encoding encoding)
        {
            return encoding.GetBytes(text);
        }

        public static string BytesToString(byte[] bytes)
        {
            return Encoding.Default.GetString(bytes);
        }

        public static string BytesToString(byte[] bytes, Encoding encoding)
        {
            return encoding.GetString(bytes);
        }

        public static int BytesToInt32(byte[] data)
        {
            int result;
            if (data.Length < 4)
            {
                result = 0;
            }
            else
            {
                int num = 0;
                if (data.Length >= 4)
                {
                    byte[] tempBuffer = new byte[4];
                    Buffer.BlockCopy(data, 0, tempBuffer, 0, 4);
                    num = BitConverter.ToInt32(tempBuffer, 0);
                }
                result = num;
            }
            return result;
        }

        public static int ToInt32(string data, int defValue)
        {
            int result;
            if (string.IsNullOrEmpty(data))
            {
                result = defValue;
            }
            else
            {
                int temp = 0;
                if (int.TryParse(data, out temp))
                {
                    result = temp;
                }
                else
                {
                    result = defValue;
                }
            }
            return result;
        }

        public static int ToInt32(object data, int defValue)
        {
            int result;
            if (data == null || Convert.IsDBNull(data))
            {
                result = defValue;
            }
            else
            {
                try
                {
                    result = Convert.ToInt32(data);
                }
                catch
                {
                    result = defValue;
                }
            }
            return result;
        }

        public static bool ToBoolean(string data, bool defValue)
        {
            bool result;
            if (string.IsNullOrEmpty(data))
            {
                result = defValue;
            }
            else
            {
                bool temp = false;
                if (bool.TryParse(data, out temp))
                {
                    result = temp;
                }
                else
                {
                    result = defValue;
                }
            }
            return result;
        }

        public static bool ToBoolean(object data, bool defValue)
        {
            bool result;
            if (data == null || Convert.IsDBNull(data))
            {
                result = defValue;
            }
            else
            {
                try
                {
                    result = Convert.ToBoolean(data);
                }
                catch
                {
                    result = defValue;
                }
            }
            return result;
        }
        public static float ToFloat(object data, float defValue)
        {
            float result;
            if (data == null || Convert.IsDBNull(data))
            {
                result = defValue;
            }
            else
            {
                try
                {
                    result = Convert.ToSingle(data);
                }
                catch
                {
                    result = defValue;
                }
            }
            return result;
        }

        public static float ToFloat(string data, float defValue)
        {
            float result;
            if (string.IsNullOrEmpty(data))
            {
                result = defValue;
            }
            else
            {
                float temp = 0f;
                if (float.TryParse(data, out temp))
                {
                    result = temp;
                }
                else
                {
                    result = defValue;
                }
            }
            return result;
        }

        public static double ToDouble(string data, double defValue)
        {
            double result;
            if (string.IsNullOrEmpty(data))
            {
                result = defValue;
            }
            else
            {
                double temp = 0.0;
                if (double.TryParse(data, out temp))
                {
                    result = temp;
                }
                else
                {
                    result = defValue;
                }
            }
            return result;
        }

        public static double ToDouble(object data, double defValue)
        {
            double result;
            if (data == null || Convert.IsDBNull(data))
            {
                result = defValue;
            }
            else
            {
                try
                {
                    result = Convert.ToDouble(data);
                }
                catch
                {
                    result = defValue;
                }
            }
            return result;
        }

        public static double ToDouble(object data, int decimals, double defValue)
        {
            double result;
            if (data == null || Convert.IsDBNull(data))
            {
                result = defValue;
            }
            else
            {
                try
                {
                    result = Math.Round(Convert.ToDouble(data), decimals);
                }
                catch
                {
                    result = defValue;
                }
            }
            return result;
        }

        public static double ToDouble(string data, int decimals, double defValue)
        {
            double result;
            if (string.IsNullOrEmpty(data))
            {
                result = defValue;
            }
            else
            {
                double temp = 0.0;
                if (double.TryParse(data, out temp))
                {
                    result = Math.Round(temp, decimals);
                }
                else
                {
                    result = defValue;
                }
            }
            return result;
        }
        public static decimal ToDecimal(object data, decimal defValue)
        {
            decimal result;
            if (data == null || Convert.IsDBNull(data))
            {
                result = defValue;
            }
            else
            {
                try
                {
                    result = Convert.ToDecimal(data);
                }
                catch
                {
                    result = defValue;
                }
            }
            return result;
        }

        public static decimal ToDecimal(string data, decimal defValue)
        {
            decimal result;
            if (string.IsNullOrEmpty(data))
            {
                result = defValue;
            }
            else
            {
                decimal temp = 0m;
                if (decimal.TryParse(data, out temp))
                {
                    result = temp;
                }
                else
                {
                    result = defValue;
                }
            }
            return result;
        }

        public static DateTime ToDateTime(object data, DateTime defValue)
        {
            DateTime result;
            if (data == null || Convert.IsDBNull(data))
            {
                result = defValue;
            }
            else
            {
                try
                {
                    result = Convert.ToDateTime(data);
                }
                catch
                {
                    result = defValue;
                }
            }
            return result;
        }

        public static DateTime ToDateTime(string data, DateTime defValue)
        {
            DateTime result;
            if (string.IsNullOrEmpty(data))
            {
                result = defValue;
            }
            else
            {
                DateTime temp = DateTime.Now;
                if (DateTime.TryParse(data, out temp))
                {
                    result = temp;
                }
                else
                {
                    result = defValue;
                }
            }
            return result;
        }

        public static string ConvertToSBC(string input)
        {
            char[] c = input.ToCharArray();
            for (int i = 0; i < c.Length; i++)
            {
                if (c[i] == ' ')
                {
                    c[i] = '\u3000';
                }
                else
                {
                    if (c[i] < '\u007f')
                    {
                        c[i] += 'ﻠ';
                    }
                }
            }
            return new string(c);
        }

        public static string ConvertToDBC(string input)
        {
            char[] c = input.ToCharArray();
            for (int i = 0; i < c.Length; i++)
            {
                if (c[i] == '\u3000')
                {
                    c[i] = ' ';
                }
                else
                {
                    if (c[i] > '＀' && c[i] < '｟')
                    {
                        c[i] -= 'ﻠ';
                    }
                }
            }
            return new string(c);
        }

    }
}
