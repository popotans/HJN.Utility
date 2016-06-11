using System;
using System.Linq;
using System.Collections.Generic;
using System.Data;

//#if DNXCORE50
//using ApplicationException = global::System.InvalidOperationException;
//#endif

namespace YueWen.Utility.Common
{
    /// <summary>
    /// Assert extensions borrowed from Sam's code in DapperTests
    /// </summary>
    static internal class AssertHelper
    {
        public static void IsEqualTo<T>(T obj, T other)
        {
            if (!obj.Equals(other))
            {
                throw new ArgumentException(string.Format("{0} should be equals to {1}", obj, other));
            }
        }

        public static void IsMoreThan(int obj, int other)
        {
            if (obj < other)
            {
                throw new ArgumentException(string.Format("{0} should be larger than {1}", obj, other));
            }
        }

        public static void IsMoreThan(long obj, int other)
        {
            if (obj < other)
            {
                throw new ArgumentException(string.Format("{0} should be larger than {1}", obj, other));
            }
        }

        public static void IsSequenceEqualTo<T>(IEnumerable<T> obj, IEnumerable<T> other)
        {
            if (!obj.SequenceEqual(other))
            {
                throw new ArgumentException(string.Format("{0} should be equals to {1}", obj, other));
            }
        }

        public static void IsFalse(bool b)
        {
            if (b)
            {
                throw new ArgumentException("Expected false");
            }
        }

        public static void IsTrue(bool b)
        {
            if (!b)
            {
                throw new ArgumentException("Expected true");
            }
        }

        public static void IsNull(object obj, string desc = "")
        {
            if (obj != null)
            {
                throw new ArgumentException("Expected null for " + desc);
            }
        }
        public static void IsNotNull(object obj, string desc = "")
        {
            if (obj == null)
            {
                throw new ArgumentException("Expected not null for " + desc);
            }
        }

        public static void IsEmpty(string str)
        {
            if (!string.IsNullOrEmpty(str))
            {
                throw new ArgumentException("Expected Empty");
            }
        }

        public static void IsNotEmpty(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                throw new ArgumentException("Expected not Empty");
            }
        }

        public static void IsNotNullorEmpty(string str, string desc = "")
        {
            if (string.IsNullOrEmpty(str))
            {
                throw new ArgumentException("Expected not Empty or Null for " + desc);
            }
        }

        public static void IsVaidDataTable(DataTable dt, string desc)
        {
            if (dt == null)
            {
                throw new ArgumentException("Expected not null for " + desc);
            }
            if (dt.Rows.Count == 0)
            {
                throw new ArgumentException("Expected RowsCount not 0 for DataTable" + desc);
            }
        }

        public static void CanConvertToInt(object obj, string desc)
        {
            int t = 0;
            if (!int.TryParse(obj.ToString(), out t))
            {
                throw new ArgumentException(" can't convert to int  " + obj + desc);
            }
        }

        public static void CanConvertToDatetime(object obj, string desc)
        {
            DateTime t = DateTime.Now;
            if (!DateTime.TryParse(obj.ToString(), out t))
            {
                throw new ArgumentException(" can't convert to DateTime  " + obj + desc);
            }
        }


    }
}