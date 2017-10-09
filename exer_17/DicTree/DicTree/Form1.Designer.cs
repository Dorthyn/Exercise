namespace DicTree
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.treeView2 = new System.Windows.Forms.TreeView();
            this.button_enable = new System.Windows.Forms.Button();
            this.button_disable = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button_details = new System.Windows.Forms.Button();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.显示ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.退出ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // treeView2
            // 
            this.treeView2.Location = new System.Drawing.Point(12, 12);
            this.treeView2.Name = "treeView2";
            this.treeView2.Size = new System.Drawing.Size(490, 516);
            this.treeView2.TabIndex = 1;
            // 
            // button_enable
            // 
            this.button_enable.Location = new System.Drawing.Point(127, 551);
            this.button_enable.Name = "button_enable";
            this.button_enable.Size = new System.Drawing.Size(75, 23);
            this.button_enable.TabIndex = 2;
            this.button_enable.Text = "启用";
            this.button_enable.UseVisualStyleBackColor = true;
            this.button_enable.Click += new System.EventHandler(this.button_enable_Click);
            // 
            // button_disable
            // 
            this.button_disable.Location = new System.Drawing.Point(236, 551);
            this.button_disable.Name = "button_disable";
            this.button_disable.Size = new System.Drawing.Size(75, 23);
            this.button_disable.TabIndex = 2;
            this.button_disable.Text = "禁用";
            this.button_disable.UseVisualStyleBackColor = true;
            this.button_disable.Click += new System.EventHandler(this.button_Disable_Click);
            // 
            // textBox1
            // 
            this.textBox1.Enabled = false;
            this.textBox1.Location = new System.Drawing.Point(509, 12);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(422, 516);
            this.textBox1.TabIndex = 3;
            // 
            // button_details
            // 
            this.button_details.Location = new System.Drawing.Point(509, 551);
            this.button_details.Name = "button_details";
            this.button_details.Size = new System.Drawing.Size(75, 23);
            this.button_details.TabIndex = 4;
            this.button_details.Text = "详细信息";
            this.button_details.UseVisualStyleBackColor = true;
            this.button_details.Click += new System.EventHandler(this.button_details_Click);
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.ContextMenuStrip = this.contextMenuStrip1;
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "DicMgr";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseDoubleClick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.显示ToolStripMenuItem,
            this.退出ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(101, 48);
            // 
            // 显示ToolStripMenuItem
            // 
            this.显示ToolStripMenuItem.Name = "显示ToolStripMenuItem";
            this.显示ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.显示ToolStripMenuItem.Text = "显示";
            this.显示ToolStripMenuItem.Click += new System.EventHandler(this.showToolStripMenuItem_Click);
            // 
            // 退出ToolStripMenuItem
            // 
            this.退出ToolStripMenuItem.Name = "退出ToolStripMenuItem";
            this.退出ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.退出ToolStripMenuItem.Text = "退出";
            this.退出ToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1047, 618);
            this.Controls.Add(this.button_details);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button_disable);
            this.Controls.Add(this.button_enable);
            this.Controls.Add(this.treeView2);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.SizeChanged += new System.EventHandler(this.Form1_SizeChanged);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TreeView treeView2;
        private System.Windows.Forms.Button button_enable;
        private System.Windows.Forms.Button button_disable;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button_details;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 显示ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 退出ToolStripMenuItem;
    }
}

