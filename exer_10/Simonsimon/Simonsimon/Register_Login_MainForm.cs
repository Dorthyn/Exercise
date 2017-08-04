using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace Simonsimon
{
    public partial class Register_Login_MainForm : Form
    {
        public static Dictionary<string, UsersInfo> usersInfo = new Dictionary<string, UsersInfo>();
        public static string _filePath = @"D:\path\Simon.csv";
        public static string _loginUser = "";

        public Register_Login_MainForm()
        {
            InitializeComponent();

        }

        private void button_login_Click(object sender, EventArgs e)
        {
            if (!usersInfo.ContainsKey(textBox_user.Text))
            {
                MessageBox.Show(this, "用户" + textBox_user.Text + "不存在！", "提示", MessageBoxButtons.OK);
            }
            else
            {
                if (usersInfo[textBox_user.Text].PswUsers==textBox_psw.Text)
                {
                    MessageBox.Show(this, "用户" + textBox_user.Text + "登陆成功！", "提示", MessageBoxButtons.OK);
                    this.DialogResult = DialogResult.OK;    //返回一个登录成功的对话框状态
                    this.Close();    //关闭登录窗口
                    _loginUser = textBox_user.Text;
                    Form1 frmMain = new Form1();
                    frmMain.Show();
                    this.Hide();
                    
                }

                else
                {
                    MessageBox.Show(this, "用户" + textBox_user.Text + "密码错误，请重新输入！", "提示", MessageBoxButtons.OK);
                    textBox_psw.Text = "";
                }
            }
        }

        private void Register_Login_MainForm_Load(object sender, EventArgs e)
        {
            FileInfo fil = new FileInfo(_filePath);
            if (File.Exists(_filePath))
            {
                OpenCsv(usersInfo, _filePath);
            }
            else
            {
                return;
            }
        }

        private Dictionary<string, UsersInfo> OpenCsv(Dictionary<string, UsersInfo> dictionaryOpen, string path)
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
                    UsersInfo usersInformationclass = new UsersInfo();

                    usersInformationclass.NameUsers = aryLine[0];
                    usersInformationclass.PswUsers = aryLine[1];
                    try
                    {
                        usersInfo.Add(usersInformationclass.NameUsers, usersInformationclass);
                    }
                    catch (ArgumentException)
                    {
                        MessageBox.Show(this, "添加失败！" 
                                        + "用户" + usersInformationclass.NameUsers 
                                        + "已存在！", "提示", MessageBoxButtons.OK);
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

        private void button_register_Click(object sender, EventArgs e)
        {
            UsersInfo usersInformationclass = new UsersInfo();

            usersInformationclass.NameUsers = textBox_user.Text;
            usersInformationclass.PswUsers = textBox_psw.Text;
            usersInformationclass.Level1Score = 0;
            usersInformationclass.Level2Score = 0;
            usersInformationclass.Level3Score = 0;


            if ((usersInformationclass.NameUsers == "") || (usersInformationclass.NameUsers == ""))
            {
                MessageBox.Show(this, "用户名或密码不能为空！", "提示", MessageBoxButtons.OK);
            }
            else
            {
                //利用字典添加
                try
                {
                    usersInfo.Add(textBox_user.Text, usersInformationclass);
                    MessageBox.Show(this, "注册成功！", "提示", MessageBoxButtons.OK);
                }
                catch (ArgumentException)
                {
                    MessageBox.Show(this, "注册失败！" + "用户" + textBox_user.Text + "已存在！", "提示", MessageBoxButtons.OK);
                }
            }

            FileInfo fi = new FileInfo(Register_Login_MainForm._filePath);
            if (!fi.Directory.Exists)
            {
                fi.Directory.Create();

            }

            FileStream fs = new FileStream(Register_Login_MainForm._filePath, System.IO.FileMode.Create, System.IO.FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs, System.Text.Encoding.UTF8);
            //输出字段名
            string data = "";
            data = "NameUsers,PswUsers,Level1Score,Level2Score,Level3Score";
            sw.WriteLine(data);
            //写出各行数据
            foreach (KeyValuePair<string, UsersInfo> kvp in Register_Login_MainForm.usersInfo)
            {
                //Console.WriteLine("Key = {0}, Value = {1}",
                //    kvp.Key, kvp.Value);
                //kvp.Value.Level1Score = 0;
                //kvp.Value.Level2Score = 0;
                //kvp.Value.Level3Score = 0;

                data = kvp.Value.NameUsers + "," +
                       kvp.Value.PswUsers + "," +
                       kvp.Value.Level1Score + "," +
                       kvp.Value.Level2Score + "," +
                       kvp.Value.Level3Score;
                sw.WriteLine(data);
            }
            sw.Close();
            fs.Close();

        }
    }
}
