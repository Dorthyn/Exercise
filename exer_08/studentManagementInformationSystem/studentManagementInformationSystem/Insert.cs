using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace studentManagementInformationSystem
{
    public partial class Insert : Form
    {
        public Insert()
        {
            InitializeComponent();
        }

        private void button_insert_Click(object sender, EventArgs e)
        {
            StudentInformationClass studentInformationclass = new StudentInformationClass();

            studentInformationclass.NoClass = textBox_class.Text;
            studentInformationclass.StudentNo = textBox_student.Text;
            studentInformationclass.NameStudent = textBox_name.Text;

            if (!radioButton1_sex.Checked&&!radioButton2_sex.Checked)
            {
                MessageBox.Show(this, "请选择性别！默认为male", "提示", MessageBoxButtons.OK);
                radioButton1_sex.Checked = true;
                studentInformationclass.Sex = radioButton1_sex.Text;
            }
            else
            {
                if (radioButton1_sex.Checked)
                {
                    studentInformationclass.Sex = radioButton1_sex.Text;
                }
                else
                {
                    studentInformationclass.Sex = radioButton2_sex.Text;
                }
            }

            if (textBox_grade.Text == "")
            {
                MessageBox.Show(this, "成绩不能为空！默认为0分", "提示", MessageBoxButtons.OK);
                textBox_grade.Text = "0";
            }
            else
            {
                studentInformationclass.GradeStudent = Convert.ToInt32(textBox_grade.Text);
            }

            studentInformationclass.BirthdayStudent = dateTimePicker1.Text;
            
            if ((studentInformationclass.NameStudent=="")||(studentInformationclass.StudentNo=="")|| (studentInformationclass.NoClass==""))
            {
                MessageBox.Show(this, "姓名、班级号、学号不能为空！", "提示", MessageBoxButtons.OK);
            }
            else
            {
                //利用字典添加
                try
                {
                    frmMain.StudentsInfo.Add(textBox_student.Text, studentInformationclass);
                    MessageBox.Show(this, "添加成功！", "提示", MessageBoxButtons.OK);
                }
                catch (ArgumentException)
                {
                    MessageBox.Show(this, "添加失败！" + "An element with Key = \"txt\" already exists.", "提示", MessageBoxButtons.OK);
                }
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void button_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox_class_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsNumber(e.KeyChar)) && e.KeyChar != (char)8)
            {
                e.Handled = true;
                MessageBox.Show("请输入班级号数字序列！");
            }
        }

        private void textBox_student_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsNumber(e.KeyChar)) && e.KeyChar != (char)8)
            {
                e.Handled = true;
                MessageBox.Show("请输入学号数字序列！");
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
