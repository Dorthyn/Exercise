using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace xmlxml
{
	public partial class Form1 : Form
	{
		private static bool ch = true;
		public Form1()
		{
			InitializeComponent();
			//ReadXML_Ex1();
		}

		private void ReadXML_Ex1()
		{
			// this is how you load an xml document object from a file like yours...
			XmlDocument doc = new XmlDocument();
			doc.Load(@"d:\path\form.xml");

			//获取根节点
			XmlNode rootNode = doc.SelectSingleNode("controls");

			//获取根节点的所有子节点，以列表形式存放
			XmlNodeList subNode = rootNode.ChildNodes;

			foreach (XmlNode xNode in subNode)
			{
				XmlElement x = (XmlElement) xNode;
				switch (x.GetAttribute("type"))
				{
					case "MenuStrip":
					    //label1.Text = "MenuStrip";
						//MenuStrip menuStrip1 = new MenuStrip();
						//获取MenuStrip的所有子节点,其中第一个子节点为MenuStrip的属性值，第二个节点为子节点subcontrol
						XmlNodeList subMenuStrip = x.ChildNodes;
						//所有属性值，subMenuStrip[0]为属性值
						string menuStripName = subMenuStrip[0].ChildNodes[0].Name;
						int x_location = Convert.ToInt32(subMenuStrip[0].ChildNodes[1].Attributes["x"].Value);
						int y_location = Convert.ToInt32(subMenuStrip[0].ChildNodes[1].Attributes["y"].Value);

						int x_size = Convert.ToInt32(subMenuStrip[0].ChildNodes[2].Attributes["x"].Value);
						int y_size = Convert.ToInt32(subMenuStrip[0].ChildNodes[2].Attributes["y"].Value);
						//获取MenuStrip的属性值
						//添加菜单栏
						MenuStrip menuStrip1;
						menuStrip1 = new System.Windows.Forms.MenuStrip();
						menuStrip1.Name = menuStripName;
						menuStrip1.Size = new System.Drawing.Size(x_size, y_size);
						menuStrip1.Location = new System.Drawing.Point(x_location, y_location);
						

						//添加ToolStrip
						//subMenuStrip[1]为子节点subcontrol
						XmlNodeList subSubMenuStrip = subMenuStrip[1].ChildNodes;
						ToolStripMenuItem[] toolStripMenuItems = new ToolStripMenuItem[5];
						int i = 0;
						//获取到每一个ToolStrip的content
						foreach (XmlNode x2 in subSubMenuStrip)
						{
							//[0]为name属性
							string toolStripMenuItemsName = x2.ChildNodes[0].Name;
							int x_size_toolStrip = Convert.ToInt32(x2.ChildNodes[1].Attributes["height"].Value);
							int y_size_toolStrip = Convert.ToInt32(x2.ChildNodes[1].Attributes["width"].Value);
							string text_toolStripMenuItems = "";
							if (ch)
							{
								text_toolStripMenuItems = x2.ChildNodes[2].ChildNodes[0].InnerText;
							}
							else
							{
								text_toolStripMenuItems = x2.ChildNodes[2].ChildNodes[1].InnerText;
							}
							//声明变量
							toolStripMenuItems[i] = new ToolStripMenuItem();
							toolStripMenuItems[i].Name = toolStripMenuItemsName;
							toolStripMenuItems[i].Text = text_toolStripMenuItems;
							toolStripMenuItems[i].Size = new Size(x_size_toolStrip,y_size_toolStrip);
							menuStrip1.Items.Add(toolStripMenuItems[i]);
							++i;
						}
						//【0】实现打开
						toolStripMenuItems[0].Click += fileToolStripMenuItem_Click;
						//[1]实现示例查找
						toolStripMenuItems[1].Click += searchToolStripMenuItem_Click;
						Controls.Add(menuStrip1);
						this.MainMenuStrip = menuStrip1;

						break;
					case "TreeView":
					    //label1.Text = "TreeView";
						TreeView[] treeViews = new TreeView[2];
						int j = 0;
						//获取TreeView的所有子节点
						XmlNodeList subTreeView = x.ChildNodes;
						//每一个x3为一个treeView
						foreach (XmlNode x3 in subTreeView)
						{
							XmlNodeList subSubTreeView = x3.ChildNodes;

							//subSubTreeView[0]为properties
							string treeViewName = subSubTreeView[0].ChildNodes[0].InnerText;
							int x_location_treeView = Convert.ToInt32(subSubTreeView[0].ChildNodes[1].Attributes["x"].Value);
							int y_location_treeView = Convert.ToInt32(subSubTreeView[0].ChildNodes[1].Attributes["y"].Value);

							int x_size_treeView = Convert.ToInt32(subSubTreeView[0].ChildNodes[2].Attributes["height"].Value);
							int y_size_treeView = Convert.ToInt32(subSubTreeView[0].ChildNodes[2].Attributes["width"].Value);

							string rootNodeTreeView = subSubTreeView[0].ChildNodes[3].ChildNodes[0].InnerText;
							TreeNode rooTreeNode = new TreeNode();
							rooTreeNode.Name = rootNodeTreeView;
							if (ch)
							{
								rooTreeNode.Text = subSubTreeView[0].ChildNodes[3].ChildNodes[1].ChildNodes[0].InnerText;
							}
							else
							{
								rooTreeNode.Text = subSubTreeView[0].ChildNodes[3].ChildNodes[1].ChildNodes[1].InnerText;
							}






							//string treeViewName = x3.ChildNodes[0].InnerText;
							//int x_location_treeView = Convert.ToInt32(x3.ChildNodes[1].Attributes["x"].Value);
							//int y_location_treeView = Convert.ToInt32(x3.ChildNodes[1].Attributes["y"].Value);

							//int x_size_treeView = Convert.ToInt32(x3.ChildNodes[2].Attributes["height"].Value);
							//int y_size_treeView = Convert.ToInt32(x3.ChildNodes[2].Attributes["width"].Value);

							treeViews[j] = new TreeView();
							treeViews[j].Name = treeViewName;
							treeViews[j].Location = new Point(x_location_treeView,y_location_treeView);
							treeViews[j].Size = new Size(x_size_treeView, y_size_treeView);

							Controls.Add(treeViews[j]);
							treeViews[j].Nodes.Add(rooTreeNode);

							TreeNode[] treeNodes = new TreeNode[3];
							int p = 0;
							foreach (XmlNode x5 in subSubTreeView[1].ChildNodes)
							{
								treeNodes[p] = new TreeNode();
								treeNodes[p].Name = x5.FirstChild.InnerText;
								if (ch)
								{
									treeNodes[p].Text = x5.LastChild.ChildNodes[0].InnerText;
								}
								else
								{
									treeNodes[p].Text = x5.LastChild.ChildNodes[1].InnerText;
								}


								
								rooTreeNode.Nodes.Add(treeNodes[p]);
								++p;
							}


							++j;
						}
						break;
					case "TextBox":
						//label1.Text = "TextBox";
						//获取TextBox的所有子节点
						XmlNodeList subTextBox = x.ChildNodes;
						string textBoxName = subTextBox[0].InnerText;
						bool isMultiline = Convert.ToBoolean(subTextBox[0].Attributes["multiline"].Value);

						int x_location_textBox = Convert.ToInt32(subTextBox[1].Attributes["x"].Value);
						int y_location_textBox = Convert.ToInt32(subTextBox[1].Attributes["y"].Value);

						int x_size_textBox = Convert.ToInt32(subTextBox[2].Attributes["height"].Value);
						int y_size_textBox = Convert.ToInt32(subTextBox[2].Attributes["width"].Value);

						TextBox textBox = new TextBox();
						textBox.Multiline = isMultiline;
						textBox.Name = textBoxName;
						textBox.Location = new Point(x_location_textBox,y_location_textBox);
						textBox.Size = new Size(x_size_textBox,y_size_textBox);
						Controls.Add(textBox);

						break;

					case "StatusStrip":
						XmlNodeList subStatusStrip = x.ChildNodes;

						string statusStripName = subStatusStrip[0].InnerText;
						int x_size_statusStrip = Convert.ToInt32(subStatusStrip[1].Attributes["height"].Value);
						int y_size_statusStrip = Convert.ToInt32(subStatusStrip[1].Attributes["width"].Value);

						StatusStrip statusStrip = new StatusStrip();
						statusStrip.Name = statusStripName;
						statusStrip.Size = new Size(x_size_statusStrip,y_size_statusStrip);
						Controls.Add(statusStrip);
						break;

					case "ToolStrip":
						XmlNodeList subToolStrip = x.ChildNodes;
						//subToolStrip[0]为属性值
						string toolStrip_Name = subToolStrip[0].ChildNodes[0].InnerText;
						int x_location_toolStrip = Convert.ToInt32(subToolStrip[0].ChildNodes[1].Attributes["x"].Value);
						int y_location_toolStrip = Convert.ToInt32(subToolStrip[0].ChildNodes[1].Attributes["y"].Value);

						int xSize_toolStrip = Convert.ToInt32(subToolStrip[0].ChildNodes[2].Attributes["height"].Value);
						int ySize_toolStrip = Convert.ToInt32(subToolStrip[0].ChildNodes[2].Attributes["width"].Value);
						string toolStrip_Text = subToolStrip[0].ChildNodes[3].InnerText;

						ToolStrip toolStrip = new ToolStrip();
						toolStrip.Name = toolStrip_Name;
						toolStrip.Location = new Point(x_location_toolStrip,y_location_toolStrip);
						toolStrip.Size = new Size(xSize_toolStrip,ySize_toolStrip);
						toolStrip.Text = toolStrip_Text;
						toolStrip.SuspendLayout();
						toolStrip.ResumeLayout(false);
						toolStrip.PerformLayout();
						Controls.Add(toolStrip);

						XmlNodeList subSubToolStrip = subToolStrip[1].ChildNodes;
						ToolStripButton[] toolStripButtons = new ToolStripButton[3];
						int k = 0;
						foreach (XmlNode x4 in subSubToolStrip)
						{
							string toolStripButtonName = x4.ChildNodes[0].InnerText;
							int x_size_toolStripButton = Convert.ToInt32(x4.ChildNodes[1].Attributes["height"].Value);
							int y_size_toolStripButton = Convert.ToInt32(x4.ChildNodes[1].Attributes["width"].Value);
							string text_toolStripButton = "";
							if (ch)
							{
								text_toolStripButton = x4.ChildNodes[2].ChildNodes[0].InnerText;
							}
							else
							{
								text_toolStripButton = x4.ChildNodes[2].ChildNodes[1].InnerText;
							}

							
							//string ImageTransparentColor = x4.ChildNodes[3].InnerText;
							//string Image = x4.ChildNodes[4].InnerText;

							toolStripButtons[k] = new ToolStripButton();
							toolStripButtons[k].Name = toolStripButtonName;
							toolStripButtons[k].Size = new Size(x_size_toolStripButton, y_size_toolStripButton);
							toolStripButtons[k].Text = text_toolStripButton;//toolStripButtons[k].ImageTransparentColor = ImageTransparentColor;
							//toolStripButtons[k].Image = ((System.Drawing.Image)(resources.GetObject("Image")));
							toolStripButtons[k].DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
							toolStrip.Items.Add(toolStripButtons[k]);
							++k;
						}

						break;
				}

			}



		}

		private void button1_Click(object sender, EventArgs e)
		{
			ch = !ch;
			if (ch)
			{
				button1.Text = "中文";
				
				ReadXML_Ex1();
				//Invalidate();
			}
			else
			{
				button1.Text = "English";
				ReadXML_Ex1();
				//Invalidate();
			}
		}

		private void fileToolStripMenuItem_Click(object sender, EventArgs e)
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

		private void searchToolStripMenuItem_Click(object sendor, EventArgs e)
		{
			Form frm2 = new Form();
			Button btn_search = new Button();
			btn_search.Location = new Point(50,50);
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
