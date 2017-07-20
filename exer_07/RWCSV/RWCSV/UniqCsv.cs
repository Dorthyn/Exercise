using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Collections;

namespace Uniq
{
    public delegate string[] FieldDelegate(string data);

    public delegate DataTable MergeDelegate(int data,int[] select);

    public delegate void ExportDelegate(DataTable data1, string data2);
    

    public partial class UniqCsv : Form
    {
        #region fields
        static DataTable mergeResult;
        static string[] resultField;
        int _alignSelected;
        ArrayList _list = new ArrayList();

        int[] selectedIndex;
        //声明委托变量并赋值
        private FieldDelegate fieldelegate = new FieldDelegate(ope.File_read);

        private MergeDelegate mergedelegate = new MergeDelegate(ope.Merge_csv);

        private ExportDelegate exportdelegate = new ExportDelegate(ope.ExportToExcel);

        #endregion

        #region Properties

        #endregion

        public UniqCsv()
        {
            InitializeComponent();

        }

        public string MessageInfor
        {
            set {
                labelProgress.Text = value;
            }
        }

        private void ButtonOpenClick(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Multiselect = true;
            fileDialog.Title = "请选择文件";
            fileDialog.Filter = "CSV文件(*.csv)|*.csv";

            string file = "";

            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                file = fileDialog.FileName;
                label1.Text = "已选择路径文件:" + file;
                //MessageBox.Show("已选择文件:" + file, "选择文件提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                return;
            }

            FieldDelegate fieldelegate = ope.File_read;
            IAsyncResult asyncResult = fieldelegate.BeginInvoke(file, null, null);

            //EndInvoke方法
            resultField = fieldelegate.EndInvoke(asyncResult);

            for (int k = 0; k < resultField.Length; k++)
            {
                listBox1.Items.Add(resultField[k]);
            }
        }

        private void ButtonSaveClick(object sender, EventArgs e)
        {

            SaveFileDialog saveDia = new SaveFileDialog();
            saveDia.Filter = "Excel|*.xlsx";
            saveDia.Title = "导出为Excel文件";

            //将DataTable写入xls
            if (   (saveDia.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                && !string.Empty.Equals(saveDia.FileName))
            {
                ope.exportThread = new ope.operationDelegate(SetProgressBarVal);

                Thread exportThread = new Thread(() => ope.ExportToExcel(mergeResult, saveDia.FileName));
                exportThread.Start();
            }
        }
        //保存文件
        private string ShowSaveFileDialog()
        {
            string localFilePath = "";
            //string localFilePath, fileNameExt, newFileName, FilePath; 
            SaveFileDialog sfd = new SaveFileDialog();
            //设置文件类型 
            sfd.Filter = "Excel表格（*.xls）|*.xls";

            //设置默认文件类型显示顺序 
            sfd.FilterIndex = 1;

            //保存对话框是否记忆上次打开的目录 
            sfd.RestoreDirectory = true;

            //点了保存按钮进入 
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                localFilePath = sfd.FileName.ToString(); //获得文件路径 
                string fileNameExt = localFilePath.Substring(localFilePath.LastIndexOf("\\") + 1); //获取文件名，不带路径

                //获取文件路径，不带文件名 
                //FilePath = localFilePath.Substring(0, localFilePath.LastIndexOf("\\")); 

                //给文件名前加上时间 
                //newFileName = DateTime.Now.ToString("yyyyMMdd") + fileNameExt; 

                //在文件名里加字符 
                //saveFileDialog1.FileName.Insert(1,"dameng"); 

                //System.IO.FileStream fs = (System.IO.FileStream)sfd.OpenFile();//输出文件 

                ////fs输出带文字或图片的文件，就看需求了 
            }

            return localFilePath;
        }

        private void ButtonMergeClick(object sender, EventArgs e)
        {
            //选定合并标准
            int appointList = _alignSelected;

            MergeDelegate mergedelegate = ope.Merge_csv;
            IAsyncResult result_merge = mergedelegate.BeginInvoke(appointList, selectedIndex,null, null);
            mergeResult = mergedelegate.EndInvoke(result_merge);

            this.progressBar1.Maximum = mergeResult.DefaultView.Count;

            label3.Text = "已合成";
            
        }

        private void ButtonAlignClick(object sender, EventArgs e)
        {
            label5.Text = "当前选定对齐标准为" + listBox1.SelectedItem;
            _alignSelected = listBox1.SelectedIndex + 1;
        }

        private void ButtonOthersClick(object sender, EventArgs e)
        {
            textBox1.AppendText(listBox1.SelectedItem.ToString()+"\n");
            //需要显示的字段序号
            _list.Add(listBox1.SelectedIndex);
            selectedIndex = (int[])_list.ToArray(typeof(int));
        }

        public void SetProgressBarVal(int SetVal)
        {
            if(this.progressBar1.InvokeRequired)
            {
                if(progressBar1.Disposing||progressBar1.IsDisposed)
                {
                    return;
                }

                ope.exportThread = new ope.operationDelegate(SetProgressBarVal);
                this.Invoke(ope.exportThread, new object[] { SetVal });
            }

            else
            {
                progressBar1.Value = SetVal;
            }
        }
    }
}
