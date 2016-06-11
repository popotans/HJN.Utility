using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using YueWen.Utility.Common;

namespace YueWen.Utility.TestWeb
{
    [DataContract]
    [Serializable]
    public class Person
    {
        [DataMember]
        public string Name { get; set; }
        public string School { get; set; }
          [DataMember]
        public DateTime Datetime { get; set; }
    }
    public partial class json : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Person per = new Person()
            {
                Datetime = DateTime.Now,
                Name = "name",
                School = "school"
            };
            Person per2 = new Person()
            {
                Datetime = DateTime.Now,
                Name = "name",
                School = "school"
            };

            var xml = SerializeHelper.ToJsonSelf(per);
            Response.Write(xml);
        }
    }
}