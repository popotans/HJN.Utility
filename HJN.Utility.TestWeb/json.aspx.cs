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
    public class _Person111
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
            _Person111 per = new _Person111()
            {
                Datetime = DateTime.Now,
                Name = "name",
                School = "school"
            };
            _Person111 per2 = new _Person111()
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