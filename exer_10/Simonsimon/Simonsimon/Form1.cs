using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Simonsimon
{
    public partial class Form1 : Form
    {
        //private int level1_interval = 1000;
        //private int level2_interval = 500;
        //private int level3_interval = 300;

        public Form1()
        {
            InitializeComponent();
        }

        private void button_level1_Click(object sender, EventArgs e)
        {
            //要跨线程访问UI
            shiningLightsControl1.watchThread = shiningLightsControl1.refreshLabelWatch_1;
            shiningLightsControl1.clickThread = shiningLightsControl1.refreshLabelWatch_2;
            shiningLightsControl1.enableThread = shiningLightsControl1.picEnable;
            new Thread(shiningLightsControl1.autoPlay).Start();
        }

        //此处应该设置线程睡眠时间，来实现难易程度
        private void primaryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //int interval = 500;
            //要跨线程访问UI
            shiningLightsControl1.Level = 1000;
            shiningLightsControl1.watchThread = shiningLightsControl1.refreshLabelWatch_1;
            shiningLightsControl1.clickThread = shiningLightsControl1.refreshLabelWatch_2;
            shiningLightsControl1.nullThread = shiningLightsControl1.refreshLabelWatch_null;
            shiningLightsControl1.enableThread = shiningLightsControl1.picEnable;
            new Thread(shiningLightsControl1.autoPlay).Start();
        }

        private void junioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //实现难易程度，即间隔时间的长短
            shiningLightsControl1.Level = 500;
            shiningLightsControl1.watchThread = shiningLightsControl1.refreshLabelWatch_1;
            shiningLightsControl1.clickThread = shiningLightsControl1.refreshLabelWatch_2;
            shiningLightsControl1.nullThread = shiningLightsControl1.refreshLabelWatch_null;
            shiningLightsControl1.enableThread = shiningLightsControl1.picEnable;
            new Thread(shiningLightsControl1.autoPlay).Start();
        }

        private void seniorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            shiningLightsControl1.Level = 300;
            shiningLightsControl1.watchThread = shiningLightsControl1.refreshLabelWatch_1;
            shiningLightsControl1.clickThread = shiningLightsControl1.refreshLabelWatch_2;
            shiningLightsControl1.nullThread = shiningLightsControl1.refreshLabelWatch_null;
            shiningLightsControl1.enableThread = shiningLightsControl1.picEnable;
            new Thread(shiningLightsControl1.autoPlay).Start();
        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HelpForm helpForm = new HelpForm();
            helpForm.Show();
        }

        private void LoginToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Register_Login_MainForm register = new Register_Login_MainForm();
            register.Show();
        }

        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //保存本次等级的分数
            //应该设置固定路径，而不是让用户选择
            FileInfo fi = new FileInfo(Register_Login_MainForm._filePath);
            if (!fi.Directory.Exists)
            {
                fi.Directory.Create();

            }

            FileStream fs = new FileStream(Register_Login_MainForm._filePath, System.IO.FileMode.Create, System.IO.FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs, System.Text.Encoding.UTF8);
            //输出字段名
            string data = "";
            data = "NameUsers,PswUsers";
            sw.WriteLine(data);
            //写出各行数据
            foreach (KeyValuePair<string, UsersInfo> kvp in Register_Login_MainForm.usersInfo)
            {
                //Console.WriteLine("Key = {0}, Value = {1}",
                //    kvp.Key, kvp.Value);
                data = kvp.Value.NameUsers + "," +
                       kvp.Value.PswUsers;
                sw.WriteLine(data);
            }
            sw.Close();
            fs.Close();
            MessageBox.Show("保存成功！");
        }

        private void rankToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RankForm rankForm = new RankForm();

            SortDictionary_Desc(Register_Login_MainForm.usersInfo);
            foreach (KeyValuePair<string, UsersInfo> kvp in Register_Login_MainForm.usersInfo)
            {
                rankForm.textBox1.AppendText(string.Format("玩家：{0},分值：{1}\n",
                    kvp.Value.NameUsers,
                    kvp.Value.Level1Score));
            }
            rankForm.Show();
        }

        //降序字典排名
        public static Dictionary<string, UsersInfo> SortDictionary_Desc(Dictionary<string, UsersInfo> dic)
        {
            List<KeyValuePair<string, UsersInfo>> myList = new List<KeyValuePair<string, UsersInfo>>(dic);
            myList.Sort(delegate (KeyValuePair<string, UsersInfo> s1, KeyValuePair<string, UsersInfo> s2)
            {
                return s2.Value.Level1Score.CompareTo(s1.Value.Level1Score);
            });
            dic.Clear();
            foreach (KeyValuePair<string, UsersInfo> pair in myList)
            {
                dic.Add(pair.Key, pair.Value);
            }
            return dic;
        }
    }
}
