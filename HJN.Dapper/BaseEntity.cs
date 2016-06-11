using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HJN.Dapper
{
    public class BaseEntity
    {
        public string TableName = string.Empty;
        public List<string> PrimaryKeys = new List<string>();
        public List<string> IdentityCols = new List<string>();
    }
}
