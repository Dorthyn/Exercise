using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Data;
using System.IO;
using System.Windows.Forms;

namespace Uniq
{
    /// <summary>
    /// 读取CSV文件的类，为方法函数
    /// </summary>
    class read_CSV
    {
        public static DataTable OpenCSV(string filePath, out string[] tableHead)//从csv读取数据返回table
        {
            System.Text.Encoding encoding = GetType(filePath); //Encoding.ASCII;//
            DataTable dt = new DataTable();
            System.IO.FileStream fs = new System.IO.FileStream(filePath, System.IO.FileMode.Open,
                System.IO.FileAccess.Read);

            System.IO.StreamReader sr = new System.IO.StreamReader(fs, encoding);

            //记录每次读取的一行记录
            string strLine = "";
            //记录每行记录中的各字段内容
            string[] aryLine = null;
            tableHead = null;
            //标示列数
            int columnCount = 0;
            //标示是否是读取的第一行
            bool IsFirst = true;
            //逐行读取CSV中的数据，直到结尾
            while ((strLine = sr.ReadLine()) != null)
            {
                if (IsFirst == true)
                {
                    tableHead = strLine.Split(',');
                    IsFirst = false;
                    columnCount = tableHead.Length;
                    //创建列
                    for (int i = 0; i < columnCount; i++)
                    {
                        DataColumn dc = new DataColumn(tableHead[i]);
                        dt.Columns.Add(dc);
                    }
                }
                else
                {
                    //首先对strLine进行判断&分割：若为多组数据，则对str1分割，再把“多组”加在数组的下一个元素；
                    //接着对str2分割，最后将数组合并到aryLine中
                    if (strLine.Contains("\""))//表示包含“多组”数据
                    {
                        string tmp_1 = ",\"";
                        string tmp_2 = "\",";
                        string middle = "";
                        string[] aryLine_1 = null;//子字符串数组_2
                        string[] aryLine_2 = null;//子字符串数组_2
                        //查找出现的位置，包括首位和末位，返回值为数组0标记
                        int index_start = 0;
                        int index_end = 0;
                        index_start = KMP(strLine, tmp_1, 0);
                        index_end = KMP(strLine, tmp_2, index_start);
                        middle = strLine.Substring(index_start + 2, index_end - 2);
                        aryLine_1 = (strLine.Substring(0, index_start)).Split(',');
                        aryLine_2 = (strLine.Substring(index_end + 3, strLine.Length - index_end - 3)).Split(',');
                        //添加元素
                        List<string> str_tmp = aryLine_1.ToList();
                        str_tmp.Add(middle);
                        aryLine_1 = str_tmp.ToArray();
                        //数组相连
                        aryLine = new string[aryLine_1.Length + aryLine_2.Length];
                        aryLine_1.CopyTo(aryLine, 0);
                        aryLine_2.CopyTo(aryLine, aryLine_1.Length);
                    }

                    else
                    {
                        aryLine = strLine.Split(',');
                    }

                    DataRow dr = dt.NewRow();
                    for (int j = 0; j < columnCount; j++)
                    {
                        dr[j] = aryLine[j];
                    }
                    dt.Rows.Add(dr);
                }
            }
            if (aryLine != null && aryLine.Length > 0)
            {
                dt.DefaultView.Sort = tableHead[0] + " " + "asc";
            }

            sr.Close();
            fs.Close();
            return dt;
        }
        /// 给定文件的路径，读取文件的二进制数据，判断文件的编码类型
        /// <param name="FILE_NAME">文件路径</param>
        /// <returns>文件的编码类型</returns>

        public static System.Text.Encoding GetType(string FILE_NAME)
        {
            System.IO.FileStream fs = new System.IO.FileStream(FILE_NAME, System.IO.FileMode.Open,
                System.IO.FileAccess.Read);
            System.Text.Encoding r = GetType(fs);
            fs.Close();
            return r;
        }

        /// 通过给定的文件流，判断文件的编码类型
        /// <param name="fs">文件流</param>
        /// <returns>文件的编码类型</returns>
        public static System.Text.Encoding GetType(System.IO.FileStream fs)
        {
            byte[] Unicode = new byte[] { 0xFF, 0xFE, 0x41 };
            byte[] UnicodeBIG = new byte[] { 0xFE, 0xFF, 0x00 };
            byte[] UTF8 = new byte[] { 0xEF, 0xBB, 0xBF }; //带BOM
            System.Text.Encoding reVal = System.Text.Encoding.Default;

            System.IO.BinaryReader r = new System.IO.BinaryReader(fs, System.Text.Encoding.Default);
            int i;
            int.TryParse(fs.Length.ToString(), out i);
            byte[] ss = r.ReadBytes(i);
            if (IsUTF8Bytes(ss) || (ss[0] == 0xEF && ss[1] == 0xBB && ss[2] == 0xBF))
            {
                reVal = System.Text.Encoding.UTF8;
            }
            else if (ss[0] == 0xFE && ss[1] == 0xFF && ss[2] == 0x00)
            {
                reVal = System.Text.Encoding.BigEndianUnicode;
            }
            else if (ss[0] == 0xFF && ss[1] == 0xFE && ss[2] == 0x41)
            {
                reVal = System.Text.Encoding.Unicode;
            }
            r.Close();
            return reVal;
        }

        /// 判断是否是不带 BOM 的 UTF8 格式
        /// <param name="data"></param>
        /// <returns></returns>
        private static bool IsUTF8Bytes(byte[] data)
        {
            int charByteCounter = 1;  //计算当前正分析的字符应还有的字节数
            byte curByte; //当前分析的字节.
            for (int i = 0; i < data.Length; i++)
            {
                curByte = data[i];
                if (charByteCounter == 1)
                {
                    if (curByte >= 0x80)
                    {
                        //判断当前
                        while (((curByte <<= 1) & 0x80) != 0)
                        {
                            charByteCounter++;
                        }
                        //标记位首位若为非0 则至少以2个1开始 如:110XXXXX...........1111110X　
                        if (charByteCounter == 1 || charByteCounter > 6)
                        {
                            return false;
                        }
                    }
                }
                else
                {
                    //若是UTF-8 此时第一位必须为1
                    if ((curByte & 0xC0) != 0x80)
                    {
                        return false;
                    }
                    charByteCounter--;
                }
            }
            if (charByteCounter > 1)
            {
                throw new Exception("非预期的byte格式");
            }
            return true;
        }

        public static void GetNext(string s, int[] next)
        {
            int m = 0;
            int n = -1;
            next[0] = -1;
            while (m + 1 < s.Length)
            {
                if (n == -1 || s[m] == s[n])
                {
                    ++m;
                    ++n;
                    if (s[m] != s[n])
                        next[m] = n;
                    else
                        next[m] = next[n];
                }
                else
                {
                    n = next[n];
                }
            }
        }

        public static int KMP(string sString, string dString, int pos)
        {
            int i = pos;
            int j = 0;
            int index = 0;
            int[] next = new int[dString.Length + 1];
            GetNext(dString, next);
            while (i < sString.Length && j < dString.Length)
            {
                if (sString[i] == dString[j])
                {
                    ++i;
                    ++j;
                }
                else
                {
                    index += j - next[j];
                    if (next[j] != -1)
                        j = next[j];
                    else
                    {
                        j = 0;
                        ++i;
                    }
                }
            }
            next = null;
            if (j == dString.Length)
                return index;
            else
                return -1;
        }

        //appoint为指定合并的标准，为自然排序的字段排序
        public static DataTable ReturnMergeData(DataTable dataTable, int appoint,int[] selected)
        {
            appoint = --appoint;

            if (dataTable.Rows.Count > 0)
            {
                //合并
                System.Collections.Hashtable ht = new System.Collections.Hashtable();
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    if (ht.ContainsKey(dataTable.Rows[i][appoint]))
                    {
                        //获取行索引
                        int index = (int)ht[dataTable.Rows[i][appoint]];

                        dataTable.Rows[index][0] = Convert.ToInt32(dataTable.Rows[index][0]) + Convert.ToInt32(dataTable.Rows[i][0]);

                        for (int j = 1; j < dataTable.Columns.Count; j++)
                        //for (int j = 1; j < selected.Length + 1; j++)
                        {
                            //Iselected = Array.IndexOf(selected, j);
                            //if (-1 == Iselected)
                            //    continue;
                            if (appoint == j)
                                continue;
                            if (!dataTable.Rows[i][j].ToString().Contains(dataTable.Rows[index][j].ToString()))
                            {
                                dataTable.Rows[index][j] = dataTable.Rows[index][j] + "," + dataTable.Rows[i][j];
                            }
                        }
                        //删除重复行  
                        dataTable.Rows.RemoveAt(i);
                        //调整索引减1  
                        i--;
                    }
                    else
                    {
                        //保存名称以及行索引
                        ht.Add(dataTable.Rows[i][appoint], i);
                    }
                }
            }
            //删除不需要的列，引号内为列名称
            for (int p = 0; p < dataTable.Columns.Count;p++)
            {
                if (selected.Contains(p))
                {
                    continue;
                }
                string tmp_unselected = "";
                tmp_unselected = dataTable.Columns[p].ColumnName;
                dataTable.Columns.Remove(tmp_unselected);
            }
            return dataTable;
        }
    }

    class ope
    {
        public delegate void operationDelegate(int i);
        public static operationDelegate exportThread;

        static DataTable dt = new DataTable();
        static DataTable dt_merge = new DataTable();
        //可以返回表头和字段数
        public static string[] File_read(string path)
        {

            string[] dtHead;
            dt = read_CSV.OpenCSV(@path, out dtHead);
            //显示表名
            return dtHead;
        }

        public static DataTable Merge_csv(int appointList,int[] selected)
        {

            DataTable mergeResult = read_CSV.ReturnMergeData(dt,appointList, selected);
            return mergeResult;
        }

        //输出文件到xlsx
        /// <summary>  
        /// 导出文件。该方法使用的数据源为DataTable,导出Excel文件。  
        /// </summary>  
        /// <param name="dt"></param>  
        public static void ExportToExcel(DataTable dt, string path)
        {
            Microsoft.Office.Interop.Excel.Application xlxsApp = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel.Workbooks workbooks = xlxsApp.Workbooks;
            
            Microsoft.Office.Interop.Excel.Workbook workbook = workbooks.Add(Microsoft.Office.Interop.Excel.XlWBATemplate.xlWBATWorksheet);
            Microsoft.Office.Interop.Excel.Worksheet worksheet = (Microsoft.Office.Interop.Excel.Worksheet)workbook.Worksheets[1];//取得sheet1
            Microsoft.Office.Interop.Excel.Range range = null;

            long totalCount = dt.Rows.Count;
            long rowRead = 0;
            float percent = 0;
            string fileName = path;

            //写入标题
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                worksheet.Cells[1, i + 1] = dt.Columns[i].ColumnName;
                range = (Microsoft.Office.Interop.Excel.Range)worksheet.Cells[1, i + 1];
                //range.Interior.ColorIndex = 15;//背景颜色
                range.Font.Bold = true;//粗体
                range.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;//居中
                                                                                                   //加边框
                range.BorderAround(Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous, Microsoft.Office.Interop.Excel.XlBorderWeight.xlThin, Microsoft.Office.Interop.Excel.XlColorIndex.xlColorIndexAutomatic, null);

                //range.ColumnWidth = 4.63;//设置列宽
                //range.EntireColumn.AutoFit();//自动调整列宽
                //r1.EntireRow.AutoFit();//自动调整行高
            }

            //写入内容

            for (int r = 0; r < dt.DefaultView.Count; r++)
            {
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    worksheet.Cells[r + 2, i + 1] = dt.DefaultView[r][i];
                    range = (Microsoft.Office.Interop.Excel.Range)worksheet.Cells[r + 2, i + 1];
                    range.Font.Size = 9;//字体大小
                                        //加边框
                    range.BorderAround(Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous, Microsoft.Office.Interop.Excel.XlBorderWeight.xlThin, Microsoft.Office.Interop.Excel.XlColorIndex.xlColorIndexAutomatic, null);
                    range.EntireColumn.AutoFit();//自动调整列宽
                }

                rowRead++;
                percent = ((float)(100 * rowRead)) / totalCount;
                System.Windows.Forms.Application.DoEvents();

                //进度条
                exportThread(r);
            }
            //满格显示
            exportThread(dt.DefaultView.Count);

            range.Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlInsideHorizontal].Weight = Microsoft.Office.Interop.Excel.XlBorderWeight.xlThin;
            if (dt.Columns.Count > 1)
            {
                range.Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlInsideVertical].Weight = Microsoft.Office.Interop.Excel.XlBorderWeight.xlThin;
            }

            try
            {
                workbook.Saved = true;
                workbook.SaveCopyAs(fileName);
            }
            catch (Exception ex)
            {
                MessageBox.Show("导出文件时出错,文件可能正被打开！\n" + ex.Message);
                return;
            }

            workbooks.Close();
            if (xlxsApp != null)
            {
                xlxsApp.Workbooks.Close();
                xlxsApp.Quit();
                int generation = System.GC.GetGeneration(xlxsApp);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(xlxsApp);
                xlxsApp = null;
                System.GC.Collect(generation);

            }

            GC.Collect();//强行销毁
            #region 强行杀死最近打开的Excel进程

            System.Diagnostics.Process[] excelProc = System.Diagnostics.Process.GetProcessesByName("EXCEL");
            System.DateTime startTime = new DateTime();

            int m, killId = 0;
            for (m = 0; m < excelProc.Length; m++)
            {
                if (startTime < excelProc[m].StartTime)
                {
                    startTime = excelProc[m].StartTime;
                    killId = m;
                }
            }

            if (excelProc[killId].HasExited == false)
            {
                excelProc[killId].Kill();
            }

            #endregion
            MessageBox.Show("导出成功!");
        }
        /// <summary>  
        /// 结束进程  
        /// </summary>  
        private static void KillSpecialExcel()
        {
            foreach (System.Diagnostics.Process theProc in System.Diagnostics.Process.GetProcessesByName("EXCEL"))
            {
                if (!theProc.HasExited)
                {
                    bool b = theProc.CloseMainWindow();
                    if (b == false)
                    {
                        theProc.Kill();
                    }
                    theProc.Close();
                }
            }
        }

    }
}
