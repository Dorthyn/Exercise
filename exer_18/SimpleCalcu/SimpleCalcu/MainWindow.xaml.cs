using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SimpleCalcu
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }


        private void ButtonClc_Click(object sender, RoutedEventArgs e)
        {
            if (result.Text.Length != 0)
            {
                result.Text = "";
            }
        }

        private void ButtonQuit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void ButtonDel_Click(object sender, RoutedEventArgs e)
        {
            if (result.Text.Length > 0)
            {
                result.Text = result.Text.Substring(0, result.Text.Length - 1);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            result.Text += btn.Content;
        }

        public void Result(string str)
        {
            int index = 0;
            string operate;
            if (str.Contains("+"))
            {
                index = str.IndexOf("+");
            }
            else if (str.Contains("-"))
            {
                index = str.IndexOf("-");
            }
            else if (str.Contains("*"))
            {
                index = str.IndexOf("*");
            }
            else if (str.Contains("/"))
            {
                index = str.IndexOf("/");
            }
            operate = str.Substring(index, 1);
            double p1 = Convert.ToDouble(str.Substring(0, index));
            double p2 = Convert.ToDouble(str.Substring(index + 1, str.ToCharArray().Length - index - 1));
            switch (operate)
            {
                case "+":
                    result.Text += " = " + (p1 + p2);
                    break;
                case "-":
                    result.Text += " = " + (p1 - p2);
                    break;
                case "×":
                    result.Text += " = " + (p1 * p2);
                    break;
                case "÷":
                    if (p2 == 0)
                    {
                        result.Text += "除数不能为0";
                    }
                    else
                    {
                        result.Text += " = " + (p1 / p2);
                    }
                    break;
            }
        }

        private void ButtonEqual_Click(object sender, RoutedEventArgs e)
        {
            //判断输入的正确性，不支持连续运算
            bool isCorrect = false;
            char[] txtArr = result.Text.ToCharArray();
            int length = txtArr.Length;
            //首尾必须为数字
            if (Char.IsNumber(txtArr[0]) && Char.IsNumber(txtArr[length - 1]))
            {
                int num = 0;
                foreach (char item in txtArr)
                {
                    if (item.Equals('+') || item.Equals('-') || item.Equals('*') || item.Equals('/'))
                    {
                        num += 1;
                    }
                }
                //排除不含操作符和含有多个操作符的可能
                if (num != 1)
                {
                    isCorrect = false;
                }
                else
                {
                    isCorrect = true;
                }
            }
            else
            {
                isCorrect = false;
            }

            //输入正确则取计算结果
            if (isCorrect)
            {
                Result(result.Text);
            }
            else
            {
                result.Text = "输入有误，请重新输入！";
            }
        }
    }
}
