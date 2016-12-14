using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HJN.Utility.WebForm
{
    public class PageControl
    {
        Page page;
        MasterPage masterPage;
        public PageControl(Page page)
        {
            this.page = page;
        }

        public List<Control> FindAllControls()
        {
            if (this.masterPage != null)
                return FindAllControls(this.masterPage);
            else
                return FindAllControls(this.page);
        }

        public List<Control> FindAllControls(Page page)
        {
            List<Control> list = new List<Control>();
            foreach (Control ctrl in page.Controls)
            {
                if (ctrl.Controls.Count == 0)
                    list.Add(ctrl);
                else
                {
                    GetControls(ctrl, ref list);
                }
            }
            return list;
        }

        public List<Control> FindAllControls(MasterPage master)
        {
            List<Control> list = new List<Control>();
            foreach (Control ctrl in master.Controls)
            {
                if (ctrl.Controls.Count == 0)
                    list.Add(ctrl);
                else
                {
                    GetControls(ctrl, ref list);
                }
            }
            return list;
        }

        private void GetControls(Control parent, ref List<Control> list)
        {
            foreach (Control ctrl in parent.Controls)
            {
                if (ctrl.Controls.Count == 0)
                    list.Add(ctrl);
                else
                    GetControls(ctrl, ref list);
            }
        }

        public Dictionary<string, ColumnData> GetControlValues()
        {
            List<Control> listCOntrols = FindAllControls(page);
            Dictionary<string, ColumnData> dic = new Dictionary<string, ColumnData>();
            foreach (Control ctr in listCOntrols)
            {
                if (string.IsNullOrEmpty(ctr.ID)) continue;
                if (ctr is Repeater)
                {

                }
                else
                {
                    getControlSingle(ctr, dic);
                }
            }
            return dic;
        }

        /// <summary>
        /// 设置页面控件的值
        /// </summary>
        public void SetValuetoPageControl(object valObject)
        {
            List<Control> listCOntrols = FindAllControls(page);
            Type type = valObject.GetType();
            PropertyInfo[] piArr = type.GetProperties();

            foreach (PropertyInfo pi in piArr)
            {
                var control = listCOntrols.Where(x => x.ID == pi.Name).FirstOrDefault();
                if (control != null)
                {
                    SetControlVal(pi, valObject, control);
                }
            }
        }

        /// <summary>
        /// 将页面控件的值赋值给对象
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="dicVals"></param>
        public void SetValuetoObject(object obj, Dictionary<string, ColumnData> dicVals)
        {
            Type type = obj.GetType();
            PropertyInfo[] piArr = type.GetProperties();
            foreach (PropertyInfo pi in piArr)
            {
                if (dicVals.ContainsKey(pi.Name))
                {
                    SetObjVal(pi, obj, dicVals[pi.Name].Value);
                }
            }
        }

        /// <summary>
        /// 将页面控件的值赋值给对象
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="dicVals"></param>
        public void SetValuetoObject(object obj)
        {
            var dicVals = GetControlValues();

            Type type = obj.GetType();
            PropertyInfo[] piArr = type.GetProperties();
            foreach (PropertyInfo pi in piArr)
            {
                if (dicVals.ContainsKey(pi.Name))
                {
                    SetObjVal(pi, obj, dicVals[pi.Name].Value);
                }
            }
        }

        private void SetControlVal(PropertyInfo pi, object obj, Control ctr)
        {
            Type controlType = null;
            string val = "";
            try
            {
                controlType = ctr.GetType();
                object objVal = pi.GetValue(obj, null);
                if (objVal != null)
                    val = objVal.ToString();
            }
            catch (Exception)
            {
                throw new Exception(ctr.GetType().ToString() + " " + ctr.ID);
            }

            switch (controlType.ToString())
            {
                case "System.Web.UI.WebControls.TextBox":
                    TextBox text = (TextBox)ctr;
                    text.Text = val;
                    break;
                case "System.Web.UI.WebControls.Label":
                    Label label = (Label)ctr;
                    label.Text = val;
                    break;
                case "System.Web.UI.WebControls.DropDownList":
                    DropDownList ddl = (DropDownList)ctr;
                    ddl.SelectedValue = val;
                    break;
                case "System.Web.UI.WebControls.RadioButtonList":
                    RadioButtonList rbl = (RadioButtonList)ctr;
                    rbl.SelectedValue = val;
                    break;
                default:
                    break;
            }
        }

        private void SetObjVal(PropertyInfo pi, object obj, object val)
        {
            Type tp = pi.PropertyType;
            if (tp == typeof(string))
            {
                if (val == null) val = "";
            }
            else if (tp == typeof(DateTime))
            {
                if (val == null || val.ToString().Length == 0)
                {
                    val = "1970-1-1";
                }
                val = DateTime.Parse(val.ToString());
            }
            else if (tp == typeof(Guid))
            {
                if (val == null || val.ToString().Length == 0)
                    val = Guid.Empty;
            }
            else if (tp == typeof(decimal))
            {
                if (val == null || val.ToString().Length == 0) val = "0";
                val = decimal.Parse(val.ToString());
            }
            else if (tp == typeof(double))
            {
                if (val == null || val.ToString().Length == 0) val = "0";
                val = double.Parse(val.ToString());
            }
            else if (tp == typeof(float))
            {
                if (val == null || val.ToString().Length == 0) val = "0";
                val = float.Parse(val.ToString());
            }
            else if (tp == typeof(long))
            {
                if (val == null || val.ToString().Length == 0) val = "0";
                val = long.Parse(val.ToString());
            }
            else if (tp == typeof(int))
            {
                if (val == null || val.ToString().Length == 0) val = "0";
                val = int.Parse(val.ToString());
            }
            pi.SetValue(obj, val, null);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="ctr"></param>
        /// <param name="ht"></param>
        /// <param name="withEditable">标识审批页面可以编辑的字段，用于更新数据</param>
        private void getControlSingle(Control ctr, Dictionary<string, ColumnData> allControlVal, bool withEditable = false)
        {
            Type controlType = ctr.GetType();
            string datatype = "";
            ColumnData col = null;
            string name = "", val = "";
            switch (controlType.ToString())
            {
                case "System.Web.UI.WebControls.TextBox":
                    TextBox text = (TextBox)ctr;
                    string controlTextBoxName = text.ID;
                    string controlTextBoxValue = text.Text;
                    datatype = text.Attributes["DataType"];
                    if (withEditable)
                    {
                        string editable = text.Attributes["Editable"];
                        if (editable != null && string.Compare(editable, "true", true) == 0)
                        {
                            col = ColumnData.GetColumn(controlTextBoxName, controlTextBoxValue, datatype);
                            allControlVal.Add(controlTextBoxName, col);
                        }
                    }
                    else
                    {
                        col = ColumnData.GetColumn(controlTextBoxName, controlTextBoxValue, datatype);
                        allControlVal.Add(controlTextBoxName, col);
                    }

                    break;
                case "System.Web.UI.WebControls.Label":
                    Label label = (Label)ctr;
                    name = label.ID;
                    val = label.Text;
                    datatype = label.Attributes["DataType"];
                    if (withEditable)
                    {

                    }
                    else
                    {
                        col = ColumnData.GetColumn(name, val, datatype);
                        allControlVal.Add(name, col);
                    }
                    //System.Web.UI.WebControls.RadioButtonList
                    break;
                case "System.Web.UI.WebControls.DropDownList":
                    DropDownList ddl = (DropDownList)ctr;
                    name = ctr.ID;
                    val = ddl.SelectedValue;
                    datatype = ddl.Attributes["DataType"];
                    if (withEditable)
                    {
                        string editable = ddl.Attributes["Editable"];
                        if (editable != null && string.Compare(editable, "true", true) == 0)
                        {
                            col = ColumnData.GetColumn(name, val, datatype);
                            allControlVal.Add(name, col);
                        }
                    }
                    else
                    {
                        col = ColumnData.GetColumn(name, val, datatype);
                        allControlVal.Add(name, col);
                    }
                    break;
                case "System.Web.UI.WebControls.RadioButtonList":
                    RadioButtonList rbl = (RadioButtonList)ctr;
                    name = rbl.ID;
                    val = rbl.SelectedValue;
                    datatype = rbl.Attributes["DataType"];
                    if (withEditable)
                    {
                        string editable = rbl.Attributes["Editable"];
                        if (editable != null && string.Compare(editable, "true", true) == 0)
                        {
                            col = ColumnData.GetColumn(name, val, datatype);
                            allControlVal.Add(name, col);
                        }
                    }
                    else
                    {
                        col = ColumnData.GetColumn(name, val, datatype);
                        allControlVal.Add(name, col);
                    }
                    break;
                default:
                    break;
            }
        }
    }
}
