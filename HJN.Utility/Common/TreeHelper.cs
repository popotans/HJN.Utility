using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YueWen.Utility.Common
{
    public class Treenode
    {
        public string ID { get; set; }
        public string Name { get; set; }
        /// <summary>
        /// 深度
        /// </summary>
        public int Depth { get; internal set; }
        public string ParentID { get; set; }
        public List<Treenode> Childs { get; internal set; }

        public object Ext1 { get; set; }
        public object Ext2 { get; set; }
        public object Ext3 { get; set; }
        public object Ext4 { get; set; }
        public object Ext5 { get; set; }
        public object Ext6 { get; set; }
        public object Ext7 { get; set; }
        public object Ext8 { get; set; }
        public object Ext9 { get; set; }
        public object Ext10 { get; set; }
    }

    public class TreeHelper
    {
        public static List<Treenode> GetTree(List<Treenode> list)
        {
            foreach (Treenode treenode in list)
            {
                treenode.Depth = GetTreeDepth(treenode, list);
                treenode.Childs = list.Where(x => x.ParentID == treenode.ID).ToList();
            }
            List<Treenode> rsList = new List<Treenode>();
            var rootList = list.Where(x => IsRoot(x));
            foreach (var node in rootList)
            {
                rsList.Add(node);
                ResortList(node, list, ref rsList);
            }
            return rsList;
        }

        private static void ResortList(Treenode node, List<Treenode> list, ref List<Treenode> rsList)
        {
            foreach (var n in node.Childs)
            {
                rsList.Add(n);
                ResortList(n, list, ref rsList);
            } 
        }

        public static string PadLeftByCount(int length, string chars = "-")
        {
            string rs = "";
            for (int i = 0; i < length; i++)
            {
                rs += chars;
            }

            return rs;
        }

        /// <summary>
        /// 获取树的深度
        /// </summary>
        /// <param name="tn"></param>
        /// <param name="listSource"></param>
        /// <returns></returns>
        public static int GetTreeDepth(Treenode tn, List<Treenode> listSource)
        {
            int maxCOunt = 500;
            int rs = 0;
            while (tn != null && !IsRoot(tn))
            {
                tn = listSource.Where(x => x.ID == tn.ParentID).FirstOrDefault();
                rs++;
                if (rs > maxCOunt) break;
            }
            return rs;
        }

        private static bool IsRoot(Treenode node)
        {
            if (string.IsNullOrEmpty(node.ParentID) || node.ParentID == "0" || node.ParentID == Guid.Empty.ToString())
            {
                return true;
            }
            return false;
        }

    }
}
