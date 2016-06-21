using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HJN.Dapper
{
    public abstract class BaseEntity
    {
        public string __TableName = string.Empty;
        public List<string> __PrimaryKeys = new List<string>();
        public List<string> __IdentityKeys = new List<string>();
    }
}
