using InterfaceMenu;
using System.Windows.Forms;

namespace MenuLib
{
	//要实现IMenu下的所有属性和函数
    public class Open : IMenu
    {
	    //public string textMenu
	    //{
		   // get { return "Open"; }
	    //}

	    public void Execute()
	    {
			OpenFileDialog fileDialog = new OpenFileDialog();
		    fileDialog.Multiselect = true;
		    fileDialog.Title = "请选择文件";
		    fileDialog.Filter = "所有文件|*.*";

		    string file = "";

		    if (fileDialog.ShowDialog() == DialogResult.OK)
		    {
			    file = fileDialog.FileName;
		    }
		    else
		    {
			    return;
		    }
		}
	}
}
