using System.Windows.Forms;
using InterfaceMenu;

namespace MenuLib
{
	class Search :IMenu
	{
		public string textMenu
		{
			get { return "Search"; }
		}

		public void Execute()
		{
			Form frm2 = new Form();
			Button btn_search = new Button();
			btn_search.Location = new System.Drawing.Point(50, 50);
			btn_search.Text = "search";

			TextBox txt = new TextBox();
			txt.Multiline = true;

			frm2.Controls.Add(btn_search);
			frm2.Controls.Add(txt);
			frm2.Text = "search";
			frm2.Show();
		}
	}
}
