using System;
using System.Windows.Forms;

namespace studentManagementInformationSystem
{
    public partial class UpdateForm : Form
    {
        public UpdateForm()
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
                textBox_class.Text = frmMain.StudentsInfo[textBox_studentNo.Text].NoClass;
                textBox_student.Text = frmMain.StudentsInfo[textBox_studentNo.Text].StudentNo;
                textBox_name.Text = frmMain.StudentsInfo[textBox_studentNo.Text].NameStudent;
                if (radioButton1_sex.Text== frmMain.StudentsInfo[textBox_studentNo.Text].Sex)
                {
                    radioButton1_sex.Checked = true;
                }
                else
                {
                    radioButton2_sex.Checked = true;
                }
                dateTimePicker1.Text = frmMain.StudentsInfo[textBox_studentNo.Text].BirthdayStudent;
                textBox_grade.Text = frmMain.StudentsInfo[textBox_studentNo.Text].GradeStudent.ToString();
            }
        }

        private void button_update_Click(object sender, EventArgs e)
        {
            StudentInformationClass studentInformationclass = new StudentInformationClass();

            studentInformationclass.NoClass = textBox_class.Text;
            studentInformationclass.StudentNo = textBox_student.Text;
            studentInformationclass.NameStudent = textBox_name.Text;
            studentInformationclass.Sex = radioButton1_sex.Text;
            studentInformationclass.BirthdayStudent = dateTimePicker1.Text;
            studentInformationclass.GradeStudent = Convert.ToInt32(textBox_grade.Text);

            frmMain.StudentsInfo[textBox_studentNo.Text].NoClass = textBox_class.Text;
            frmMain.StudentsInfo[textBox_studentNo.Text].NameStudent = textBox_name.Text;
            if (radioButton1_sex.Checked)
            {
                frmMain.StudentsInfo[textBox_studentNo.Text].Sex = radioButton1_sex.Text;
            }
            else
            {
                frmMain.StudentsInfo[textBox_studentNo.Text].Sex = radioButton2_sex.Text;
            }
            frmMain.StudentsInfo[textBox_studentNo.Text].BirthdayStudent = dateTimePicker1.Text;
            frmMain.StudentsInfo[textBox_studentNo.Text].GradeStudent = Convert.ToInt32(textBox_grade.Text);
            MessageBox.Show(this, "修改成功！" + "An element with Key =" + textBox_studentNo.Text + "has been changed", "提示", MessageBoxButtons.OK);
        }

        private void textBox_studentNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsNumber(e.KeyChar)) && e.KeyChar != (char)8)
            {
                e.Handled = true;
                MessageBox.Show("请输入学号数字序列！");
            }
        }

        private void textBox_class_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsNumber(e.KeyChar)) && e.KeyChar != (char)8)
            {
                e.Handled = true;
                MessageBox.Show("请输入班级号数字序列！");
            }
        }

        private void textBox_grade_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsNumber(e.KeyChar)) && e.KeyChar != (char)8)
            {
                e.Handled = true;
                MessageBox.Show("请输入成绩！");
            }
        }
    }
}
