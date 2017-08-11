using System;
using System.Windows.Forms;

/// <summary>
/// 本程序实现实时监测数据输入，只允许输入数字，当输入其他字符时，button的Enable属性为False
/// </summary>
namespace realTimeButtonClick
{
    public partial class Form1 : Form
    {
        //private bool IsNum = false;
        public Form1()
        {
            InitializeComponent();
        }

        //TextBox_TextChanged事件在TextBox中的内容发生改变时触发
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            int result;
            if (!isNumberic(textBox1.Text, out result))
            {
                button1.Enabled = false;
            }
            else
            {
                button1.Enabled = true;
            }
            //this.label1.Text = textBox1.Text;

        }

        //通过正则表达式判断是否为数字字符串
        protected bool isNumberic(string message, out int result)
        {
            System.Text.RegularExpressions.Regex rex =
                new System.Text.RegularExpressions.Regex(@"^\d+$");
            result = -1;
            if (rex.IsMatch(message))
            {
                result = int.Parse(message);
                return true;
            }
            else
                return false;
        }


        //private void textBox1_KeyPress_1(object sender, KeyPressEventArgs e)
        //{
        //    if (textBox1.Focused)
        //    {
        //        if (e.KeyChar != '\b')//这是允许输入退格键
        //        {
        //            if ((e.KeyChar < '0') || (e.KeyChar > '9'))//这是允许输入0-9数字
        //            {
        //                e.Handled = true;
        //                //MessageBox.Show("Only numbers is legal input!");


        //            }
        //            else
        //            {
        //                button1.Enabled = true;
        //            }
        //        }
        //    }
        //}
    }
}
