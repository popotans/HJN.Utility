using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace HJN.Utility.Common
{
    public class ObjectHelper
    {

        public static T Clone<T>(T RealObject)
        {
            using (Stream stream = new MemoryStream())
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                serializer.Serialize(stream, RealObject);
                stream.Seek(0, SeekOrigin.Begin);
                return (T)serializer.Deserialize(stream);
                //IFormatter formatter = new BinaryFormatter();
                //formatter.Serialize(stream, RealObject);
                //stream.Seek(0, SeekOrigin.Begin);
                //return (T)formatter.Deserialize(stream);   
            }
        }

        public static Dictionary<string, object> GetPropertiesValuesWithSimpleType<T>(T t)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            if (t == null)
            {
                return dic;
            }
            System.Reflection.PropertyInfo[] properties = t.GetType().GetProperties(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public);

            if (properties.Length <= 0)
            {
                return dic;
            }
            foreach (System.Reflection.PropertyInfo item in properties)
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
        /// 比较两个对象返回值不同的字段,返回一个字典数组，第一个数组为数据源不同字段的值，旧值 。
        /// 第二个数组为数据目标不同字段的值，新值 。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="tsource"></param>
        /// <param name="ttarget"></param>
        public static Dictionary<string, object>[] Compare<T>(T tsourceOld, T ttargetNew)
        {
            var itemOld = GetPropertiesValuesWithSimpleType(tsourceOld);
            var itemNew = GetPropertiesValuesWithSimpleType(ttargetNew);
            Dictionary<string, object> notSamePropertiesValOld = new Dictionary<string, object>();
            Dictionary<string, object> notSamePropertiesValNew = new Dictionary<string, object>();
            foreach (string key in itemOld.Keys)
            {
                var valold = itemOld[key];
                var oldType = valold.GetType();
                var valnew = itemNew[key];

                if (valold.ToString() != valnew.ToString())
                {
                    notSamePropertiesValOld.Add(key, valold);
                    notSamePropertiesValNew.Add(key, valnew);
                }
            }
            return new Dictionary<string, object>[] { notSamePropertiesValOld, notSamePropertiesValNew };
        }



    }
}
