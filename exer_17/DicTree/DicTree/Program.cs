using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;

namespace DicTree
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            SetAutoRun(@"C:\Users\M0015\source\repos\DicTree\DicTree\bin\Debug\DicTree.exe", true);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }

        /// <summary>
        /// 设置应用程序开机自动运行
        /// </summary>
        /// <param name="fileName">应用程序的文件名</param>
        /// <param name="isAutoRun">是否自动运行，为false时，取消自动运行</param>
        /// <exception cref="System.Exception">设置不成功时抛出异常</exception>
        public static void SetAutoRun(string fileName, bool isAutoRun)
        {
            RegistryKey reg = null;
            try
            {
                if (!System.IO.File.Exists(fileName))
                    throw new Exception("该文件不存在!");
                String name = fileName.Substring(fileName.LastIndexOf(@"\") + 1);
                reg = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true);
                if (reg == null)
                    reg = Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run");
                if (isAutoRun)
                    reg.SetValue(name, fileName);
                else
                    reg.SetValue(name, false);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                if (reg != null)
                    reg.Close();
            }

        }
    }
}
