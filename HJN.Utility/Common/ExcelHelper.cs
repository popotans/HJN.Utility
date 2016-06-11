using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace YueWen.Utility.Common
{
    public class ExcelHelper
    {     
        /// <summary>
        /// DataTable导出到Excel
        /// </summary>
        /// <param name="pData">DataTable</param>
        /// <param name="pFileName">导出文件名</param>
        /// <param name="pHeader">导出标题以|分割</param>
        public static void ExportExcel(System.Data.DataTable pData, string pFileName)
        {
            System.Web.UI.WebControls.GridView dgExport = new GridView();
            dgExport.DataSource = pData;
            dgExport.DataBind();
            //HttpContext.Current.Response.Charset = "utf-8";
            HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("gb2312");//注意编码
            HttpContext.Current.Response.AppendHeader("Content-Disposition",
                "attachment;filename=" + HttpUtility.UrlEncode(pFileName, System.Text.Encoding.UTF8).ToString());
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";//image/JPEG;text/HTML;image/GIF;vnd.ms-excel/msword 

            StringWriter tw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(tw);
            dgExport.RenderControl(hw);
            HttpContext.Current.Response.Write(tw.ToString());
            HttpContext.Current.Response.End();

        }

        public static void ExportExcel2(DataTable data, string fileName)
        {
            try
            {
                if (data != null && data.Rows.Count > 0)
                {
                    HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
                    HttpContext.Current.Response.ContentEncoding = Encoding.UTF8;
                    HttpContext.Current.Response.Charset = "Utf-8";
                    HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(fileName + ".xls", Encoding.UTF8));
                    StringBuilder sbHtml = new StringBuilder();
                    sbHtml.AppendLine("<meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\">");
                    sbHtml.AppendLine("<table cellspacing=\"0\" cellpadding=\"5\" rules=\"all\" border=\"1\">");
                    sbHtml.AppendLine("<tr style=\"background-color: #FFE88C;font-weight: bold; white-space: nowrap;\">");
                    foreach (DataColumn column in data.Columns)
                    {
                        sbHtml.AppendLine("<td>" + column.ColumnName + "</td>");
                    }
                    sbHtml.AppendLine("</tr>");
                    foreach (DataRow row in data.Rows)
                    {
                        sbHtml.Append("<tr>");
                        foreach (DataColumn column in data.Columns)
                        {
                            sbHtml.Append("<td>").Append(row[column].ToString()).Append("</td>");
                        }
                        sbHtml.AppendLine("</tr>");
                    }
                    sbHtml.AppendLine("</table>");
                    HttpContext.Current.Response.Write(sbHtml.ToString());
                    HttpContext.Current.Response.End();
                }
            }
            catch (Exception)
            {
                //  ExcelHelper.Logger.WriteLog("-----------Excel导出数据异常-----------\r\n" + ex.ToString() + "\r\n");
            }
        }

        private static string ConnectionString(string fileName)
        {
            return string.Format(fileName.EndsWith(".xls") ? "Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties=Excel 8.0;" : "Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=\"Excel 12.0 Xml;HDR=YES\"", fileName);
        }

        public static DataTable ExcelToDataSet(string sheet, string filename)
        {
            DataTable result;
            try
            {
                OleDbConnection myConn = new OleDbConnection(ExcelHelper.ConnectionString(filename));
                string strCom = " SELECT * FROM [" + sheet + "$]";
                myConn.Open();
                OleDbDataAdapter myCommand = new OleDbDataAdapter(strCom, myConn);
                DataSet ds = new DataSet();
                myCommand.Fill(ds);
                myConn.Close();
                result = ds.Tables[0];
            }
            catch (Exception ex)
            {
                //  ExcelHelper.Logger.WriteLog("-----------Excel导入数据异常-----------\r\n" + ex.ToString() + "\r\n");
                result = null;
            }
            return result;
        }
    }
}
