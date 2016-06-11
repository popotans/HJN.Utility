using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;

namespace YueWen.Utility.Common
{
    public class ModelBindHelper
    {
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

        public static object GetValueByType(Type type, object val)
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
    }
}
