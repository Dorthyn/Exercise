using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace studentManagementInformationSystem
{
    public partial class DeleteFrm : Form
    {
        public DeleteFrm()
        {
            InitializeComponent();
        }

        private void button_delete_Click(object sender, EventArgs e)
        {
            //long deleStudentNo = Convert.ToInt64(textBox_delete.Text);
            if (!frmMain.StudentsInfo.ContainsKey(textBox_delete.Text))
            {
                MessageBox.Show(this, "删除失败！" + "An element with Key =" + textBox_delete.Text +"is not found", "提示", MessageBoxButtons.OK);
            }
            else
            {
                frmMain.StudentsInfo.Remove(textBox_delete.Text);
                MessageBox.Show(this, "删除成功！" + "An element with Key =" + textBox_delete.Text + "has been removed", "提示", MessageBoxButtons.OK);
            }
        }
    }
}
