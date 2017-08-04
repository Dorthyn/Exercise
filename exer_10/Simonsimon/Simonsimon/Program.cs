using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Simonsimon
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Register_Login_MainForm rl = new Register_Login_MainForm();
            rl.ShowDialog();
            if (rl.DialogResult == DialogResult.OK)
            {
                Application.Run(new Form1());
            }
            else
            {
                return;
            }
        }
    }
}
