using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HJN.Dapper
{
    public struct SqlParamModel
    {
        public object Value { get; set; }
        public Type Type { get; set; }

        public string BuildValueFormat()
        {
            string rs = "";
            if (Type == typeof(Int16)
                || Type == typeof(Int32)
                || Type == typeof(Int64)
                || Type == typeof(long)
                || Type == typeof(float)
                || Type == typeof(decimal)
                ) return Value.ToString();
            else if (Type == typeof(string)
                || Type == typeof(DateTime)
                || Type == typeof(char)
                ) rs = "'" + Value.ToString() + "'";
            return rs;
        }
    }
}
