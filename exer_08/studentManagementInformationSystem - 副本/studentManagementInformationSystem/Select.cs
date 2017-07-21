using System;
using System.Windows.Forms;

namespace studentManagementInformationSystem
{
    public partial class SelectForm : Form
    {
        public SelectForm()
        {
            InitializeComponent();
        }

        private void button_select_Click(object sender, EventArgs e)
        {
            if (!frmMain.StudentsInfo.ContainsKey(textBox_studentNo.Text))
            {
                MessageBox.Show(this, "查找失败！" + "An element with Key =" + textBox_studentNo.Text + "is not found", "提示", MessageBoxButtons.OK);
            }
            else
            {
                //textBox_show.AppendText("班级"+ frmMain.StudentsInfo[textBox_studentNo.Text].NoClass);

                    textBox_show.AppendText(string.Format("班级：{0},学号：{1},姓名：{2},性别：{3},出生日期：{4},成绩：{5}\n",
                                            frmMain.StudentsInfo[textBox_studentNo.Text].NoClass,
                                            frmMain.StudentsInfo[textBox_studentNo.Text].StudentNo,
                                            frmMain.StudentsInfo[textBox_studentNo.Text].NameStudent,
                                            frmMain.StudentsInfo[textBox_studentNo.Text].Sex,
                                            frmMain.StudentsInfo[textBox_studentNo.Text].BirthdayStudent,
                                            frmMain.StudentsInfo[textBox_studentNo.Text].GradeStudent));

            }


        }

        private void textBox_studentNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsNumber(e.KeyChar)) && e.KeyChar != (char)8)
            {
                e.Handled = true;
                MessageBox.Show("请输入学号数字序列！");
            }
        }
    }
}
