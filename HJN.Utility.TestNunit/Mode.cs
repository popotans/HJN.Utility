using HJN.Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HJN.Utility.TestNunit
{
    public partial class WfComment
    {
        public int IDx { get; set; }
        public int ProcinstID { get; set; }

        public string foLiO { get; set; }
        public string ActivityName { get; set; }
        public string UserName { get; set; }
        public string Action { get; set; }
        public string Comment { get; set; }
        public int IsFinal { get; set; }
        public DateTime ApproveTime { get; set; }
        public DateTime ReceiveTime { get; set; }


    }

    public partial class WfComment : BaseEntity
    {
        public WfComment()
        {
            this.__TableName = "WfComment";
            this.__PrimaryKeys.Add("IDx");
            this.__IdentityKeys.Add("IDx");
        }

    }
}
