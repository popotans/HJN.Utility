using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using YueWen.Utility.Common;

namespace YueWen.Utility.TestWeb
{
    public class popo
    {
        public int Num { get; set; }
        public string Name { get; set; }
        public DateTime Bir { get; set; }
        public Car Car { get; set; }
    }

    public class Car
    {
        public string Name { get; set; }
    }

    public partial class ObjectHelperPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            popo p1 = new popo()
            {
                Bir = DateTime.Now.AddDays(1),
                Name = "niejunhua",
                Num = 1,
                Car = new Car { Name = "is a car" }
            };

            popo p2 = new popo()
            {
                Bir = DateTime.Now.AddDays(12),
                Name = "niejunhua1",
                Num = 1
            };
            popo p3 = ObjectHelper.Clone<popo>(p1);
            Response.Write("y7uanshizhi：" + p1.Name + "," + p1.Car.Name);
            p1.Car.Name = "ssssssscar";
            p1.Name="jenny";
            Response.Write("<br/>复制后：" + p1.Name + "," + p1.Car.Name);

            //var itemArr = ObjectHelper.Compare<popo>(p1, p2);
            //foreach (var item in itemArr[0])
            //{
            //    Response.Write(item.Key + " " + item.Value + "<br/>");
            //}
            //Response.Write("new:<br/>");
            //foreach (var item in itemArr[1])
            //{
            //    Response.Write(item.Key + " " + item.Value + "<br/>");
            //}
        }
    }
}