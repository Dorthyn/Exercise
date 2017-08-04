using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace studentManagementInformationSystem
{
    public partial class frmMain : Form
    {
        //public static StudentInformationClass[] StudentsInfo = new StudentInformationClass[0];
        public static Dictionary <string, StudentInformationClass> StudentsInfo = new Dictionary<string,StudentInformationClass>();
        private string _filePath = @"D:\path\test.csv";

        public frmMain()
        {
            InitializeComponent();
        }

        private void QuickAppToolStripMenuItemClick(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void tsBtnQuit_Click(object sender, EventArgs e)
        {
            QuickAppToolStripMenuItemClick(sender, e);
        }

        private void DeleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DeleteFrm delFrm = new DeleteFrm();
            delFrm.StartPosition = FormStartPosition.CenterScreen;
            delFrm.Show();
        }

        private void InsertToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Insert esiFrm = new Insert();
            esiFrm.StartPosition = FormStartPosition.CenterScreen;
            esiFrm.Show();
        }

        private void RangeToolStripMenuItemClick(object sender, EventArgs e)
        {
            SortDictionary_Asc(StudentsInfo);
            foreach (KeyValuePair<string, StudentInformationClass> kvp in StudentsInfo)
            {
                textBoxMainShow.AppendText(string.Format("班级：{0},学号：{1},姓名：{2},性别：{3},出生日期：{4},成绩：{5}\n",
                    kvp.Value.NoClass,
                    kvp.Value.StudentNo,
                    kvp.Value.NameStudent,
                    kvp.Value.Sex,
                    kvp.Value.BirthdayStudent,
                    kvp.Value.GradeStudent));
            }
        }

        private void UpdateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UpdateForm updateForm = new UpdateForm();
            updateForm.StartPosition = FormStartPosition.CenterScreen;
            updateForm.Show();
        }

        private void SelectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectForm selectForm = new SelectForm();
            selectForm.StartPosition = FormStartPosition.CenterScreen;
            selectForm.Show();
        }

        //降序字典排名
        public static Dictionary<string, StudentInformationClass> SortDictionary_Desc(Dictionary<string, StudentInformationClass> dic)
        {
            List<KeyValuePair<string, StudentInformationClass>> myList = new List<KeyValuePair<string, StudentInformationClass>>(dic);
            myList.Sort(delegate (KeyValuePair<string, StudentInformationClass> s1, KeyValuePair<string, StudentInformationClass> s2)
            {
                return s2.Value.GradeStudent.CompareTo(s1.Value.GradeStudent);
            });
            dic.Clear();
            foreach (KeyValuePair<string, StudentInformationClass> pair in myList)
            {
                dic.Add(pair.Key, pair.Value);
            }
            return dic;
        }

        //升序字典排名
        public static Dictionary<string, StudentInformationClass> SortDictionary_Asc(Dictionary<string, StudentInformationClass> dic)
        {
            List<KeyValuePair<string, StudentInformationClass>> myList = new List<KeyValuePair<string, StudentInformationClass>>(dic);
            myList.Sort(delegate (KeyValuePair<string, StudentInformationClass> s1, KeyValuePair<string, StudentInformationClass> s2)
            {
                return s1.Value.GradeStudent.CompareTo(s2.Value.GradeStudent);
            });

            dic.Clear();

            foreach (KeyValuePair<string, StudentInformationClass> pair in myList)
            {
                dic.Add(pair.Key, pair.Value);
            }
            return dic;
        }

        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //应该设置固定路径，而不是让用户选择
            FileInfo fi = new FileInfo(_filePath);
            if (!fi.Directory.Exists)
            {
                fi.Directory.Create();

            }

            FileStream fs = new FileStream(_filePath, System.IO.FileMode.Create, System.IO.FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs, System.Text.Encoding.UTF8);
            //输出字段名
            string data = "";
            data = "班级,学号,姓名,性别,出生日期,成绩";
            sw.WriteLine(data);
            //写出各行数据
            foreach (KeyValuePair<string, StudentInformationClass> kvp in StudentsInfo)
            {
                //Console.WriteLine("Key = {0}, Value = {1}",
                //    kvp.Key, kvp.Value);
                data = "";
                data = kvp.Value.NoClass         + "," +
                       kvp.Value.StudentNo       + "," + 
                       kvp.Value.NameStudent     + "," +
                       kvp.Value.Sex             + "," +
                       kvp.Value.BirthdayStudent + "," +
                       kvp.Value.GradeStudent;
                sw.WriteLine(data);
            }
            sw.Close();
            fs.Close();
            MessageBox.Show("保存成功！");

        }
        /// <summary>
        /// 读取csv文件到字典
        /// </summary>
        /// <param name="dictionaryOpen"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        private Dictionary<string, StudentInformationClass> OpenCsv(Dictionary<string, StudentInformationClass> dictionaryOpen, string path)
        {
            //StudentsInfo
            Encoding encoding = GetType(path); //Encoding.ASCII;//
            System.IO.FileStream fs = new System.IO.FileStream(path, System.IO.FileMode.Open,
                                                               System.IO.FileAccess.Read);
            System.IO.StreamReader sr = new System.IO.StreamReader(fs, encoding);

            //记录每次读取的一行记录
            string strLine = "";

            //记录每行记录中的各字段内容
            string[] aryLine = null;
            //tableHead = null;


            //标示是否是读取的第一行
            bool IsFirst = true;

            //逐行读取CSV中的数据，直到结尾
            while ((strLine = sr.ReadLine()) != null)
            {
                if (IsFirst == true)
                {
                    //tableHead = strLine;
                    IsFirst = false;
                }
                else
                {
                    aryLine = strLine.Split(',');
                    StudentInformationClass studentInformationclass = new StudentInformationClass();

                    studentInformationclass.NoClass = aryLine[0];
                    studentInformationclass.StudentNo = aryLine[1];
                    studentInformationclass.NameStudent = aryLine[2];
                    studentInformationclass.Sex = aryLine[3];
                    studentInformationclass.BirthdayStudent = aryLine[4];
                    studentInformationclass.GradeStudent = Convert.ToInt32(aryLine[5]);
                    try
                    {
                        frmMain.StudentsInfo.Add(studentInformationclass.StudentNo, studentInformationclass);
                    }
                    catch (ArgumentException)
                    {
                        MessageBox.Show(this, "添加失败！" + "An element with Key =" + studentInformationclass.StudentNo + "already exists.", "提示", MessageBoxButtons.OK);
                    }
                }

            }

            sr.Close();
            fs.Close();

            return dictionaryOpen;
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

        private void frmMain_Load(object sender, EventArgs e)
        {
            FileInfo fil = new FileInfo(_filePath);
            if (File.Exists(_filePath))
            {
                OpenCsv(StudentsInfo, _filePath);
            }
            else
            {
                return;
            }
        }
    }

}
