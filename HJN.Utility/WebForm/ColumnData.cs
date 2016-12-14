using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HJN.Utility.WebForm
{
    public partial class ColumnData
    {
        public ColumnData() { }

        public ColumnData(string name, string value)
        {
            this.Name = name;
            this.Value = value;
            this.Type = DbColumnType.@string;
        }

        public ColumnData(string name, object value)
        {
            this.Name = name;
            this.Value = value;
            this.Type = DbColumnType.@string;
        }

        public ColumnData(string name, object value, DbColumnType type)
        {
            this.Name = name;
            this.Value = value;
            this.Type = type;
        }

        public static ColumnData GetLongColumn(string name, long value)
        {
            return new ColumnData(name, value, DbColumnType.@long);
        }
        public static ColumnData GetIntColumn(string name, int value)
        {
            return new ColumnData(name, value, DbColumnType.@int);
        }
        public static ColumnData GetStringColumn(string name, string value)
        {
            return new ColumnData(name, value, DbColumnType.@string);
        }
        public static ColumnData GetGUIDColumn(string name, Guid value)
        {
            return new ColumnData(name, value, DbColumnType.@guid);
        }
        public static ColumnData GetDatetimeColumn(string name, DateTime value)
        {
            return new ColumnData(name, value, DbColumnType.@datetime);
        }
        public static ColumnData GetDecimalColumn(string name, decimal value)
        {
            return new ColumnData(name, value, DbColumnType.@decimal);
        }

        //public static ColumnData GetNumberColumn(string name, object value)
        //{
        //    return new ColumnData(name, value, DbColumnType.number);
        //}

        public static ColumnData GetColumn(string name, object value, DbColumnType type = DbColumnType.@string)
        {
            return new ColumnData(name, value, type);
        }

        public static ColumnData GetColumn(string name, object value, string datatype)
        {
            return new ColumnData(name, value, TypeParse(datatype));
        }

        public static DbColumnType TypeParse(string datatype)
        {
            #region
            /*
               /// <remarks/>
        @int,

        /// <remarks/>
        @string,

        /// <remarks/>
        @decimal,

        /// <remarks/>
        datetime,

        /// <remarks/>
        guid,

        /// <remarks/>
        @long,
             */
            #endregion

            if (string.IsNullOrEmpty(datatype)) return DbColumnType.@string;
            // if (string.Compare(datatype, DbColumnType.@int.ToString(), true) == 0) return DbColumnType.@int;

            DbColumnType type = DbColumnType.@string;
            try
            {
                type = (DbColumnType)Enum.Parse(typeof(DbColumnType), datatype.ToLower());
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return type;
        }
    }
    public partial class ColumnData
    {

        private string nameField;

        private DbColumnType typeField;

        private object valueField;

        /// <remarks/>
        public string Name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }

        /// <remarks/>
        public DbColumnType Type
        {
            get
            {
                return this.typeField;
            }
            set
            {
                this.typeField = value;
            }
        }

        /// <remarks/>
        public object Value
        {
            get
            {
                return this.valueField;
            }
            set
            {
                this.valueField = value;
            }
        }
    }
    public enum DbColumnType
    {

        /// <remarks/>
        @int,

        /// <remarks/>
        @string,

        /// <remarks/>
        @decimal,

        /// <remarks/>
        datetime,

        /// <remarks/>
        guid,

        /// <remarks/>
        @long,
    }


}
