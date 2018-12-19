using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace Caojin.Common
{
    public static class DataTableHelper
    {
        //指定列，查找datatable中的字符串首次在第几行； colnum从0开始
        public static int RowIndex(DataTable dt, string str, int colnum)
        {
            DataView dv = dt.DefaultView;
            if (colnum < dt.Columns.Count)
            {
                foreach (DataRowView drv in dv)
                {
                    if (drv[colnum].ToString() == str)
                    { return dt.Rows.IndexOf(drv.Row); }

                }
            }
            throw new Exception(string.Format("表\"{0}\"中第{2}列不存在\"{1}\"字符串", dt.TableName, str,colnum));
        }
        //指定列，查找datatable中的字符串首次在第几行； colnum从0开始,模糊查询
        public static int RowIndexContain(DataTable dt, string str, int colnum)
        {
            DataView dv = dt.DefaultView;
            if (colnum < dt.Columns.Count)
            {
                foreach (DataRowView drv in dv)
                {
                    if (drv[colnum].ToString().Contains(str))
                    { return dt.Rows.IndexOf(drv.Row); }

                }
            }
            throw new Exception(string.Format("表\"{0}\"中第{2}列不存在\"{1}\"字符串", dt.TableName, str, colnum));
        }


        //指定行，查找datatable中字符串首次在第几列
        public static int ColumnIndex(DataTable dt, string str, int row)
        {
            DataView dv = dt.DefaultView;
            if (row < dt.Rows.Count)
            {
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    if (dv[row][i].ToString() == str)
                    { return i; }
                }
            }
            throw new Exception(string.Format("表\"{0}\"中第{2}行不存在\"{1}\"字符串", dt.TableName, str, row));
        }

        //指定行，查找datatable中字符串首次在第几列，模糊查询
        public static int ColumnIndexContain(DataTable dt, string str, int row)
        {
            DataView dv = dt.DefaultView;
            if (row < dt.Rows.Count)
            {
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    if (dv[row][i].ToString().Replace(" ", "").Contains(str.Replace(" ", "")))
                    { return i; }
                }
            }
            throw new Exception(string.Format("表\"{0}\"中第{2}行不存在\"{1}\"字符串", dt.TableName, str, row));
        }

        //colnum、row都是从0开始算起
        public static int[] Cellindex(DataTable dt, string str)
        {
            for (int r = 0; r < dt.Rows.Count; r++)
            {
                for (int c = 0; c < dt.Columns.Count; c++)
                {
                    if (dt.DefaultView[r][c].ToString() == str)
                    {
                        return new int[] { r, c };
                    }
                }
            }
            return null;
        }
        //colnum、row都是从0开始算起，单元格包含关键词
        public static int[] CellindexContain(DataTable dt, string str)
        {
            for (int r = 0; r < dt.Rows.Count; r++)
            {
                for (int c = 0; c < dt.Columns.Count; c++)
                {
                    if (dt.DefaultView[r][c].ToString().Contains(str))
                    {
                        return new int[] { r, c };
                    }
                }
            }
            return null;
        }
    }
}
