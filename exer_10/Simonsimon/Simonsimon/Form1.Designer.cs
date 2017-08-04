using System.Windows.Forms;

namespace Simonsimon
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
            this.shiningLightsControl1 = new Simonsimon.ShiningLightsControl();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.DifficultyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.primaryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.junioToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.seniorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SaveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rankToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.InstructionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.LoginToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // shiningLightsControl1
            // 
            this.shiningLightsControl1.Level = 1000;
            this.shiningLightsControl1.Location = new System.Drawing.Point(12, 41);
            this.shiningLightsControl1.Name = "shiningLightsControl1";
            this.shiningLightsControl1.Size = new System.Drawing.Size(366, 406);
            this.shiningLightsControl1.TabIndex = 4;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.DifficultyToolStripMenuItem,
            this.InstructionToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(539, 25);
            this.menuStrip1.TabIndex = 5;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // DifficultyToolStripMenuItem
            // 
            this.DifficultyToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.primaryToolStripMenuItem,
            this.junioToolStripMenuItem,
            this.seniorToolStripMenuItem,
            this.SaveToolStripMenuItem,
            this.rankToolStripMenuItem});
            this.DifficultyToolStripMenuItem.Name = "DifficultyToolStripMenuItem";
            this.DifficultyToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.DifficultyToolStripMenuItem.Text = "难度";
            // 
            // primaryToolStripMenuItem
            // 
            this.primaryToolStripMenuItem.Name = "primaryToolStripMenuItem";
            this.primaryToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.primaryToolStripMenuItem.Text = "初级";
            this.primaryToolStripMenuItem.Click += new System.EventHandler(this.primaryToolStripMenuItem_Click);
            // 
            // junioToolStripMenuItem
            // 
            this.junioToolStripMenuItem.Name = "junioToolStripMenuItem";
            this.junioToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.junioToolStripMenuItem.Text = "中级";
            this.junioToolStripMenuItem.Click += new System.EventHandler(this.junioToolStripMenuItem_Click);
            // 
            // seniorToolStripMenuItem
            // 
            this.seniorToolStripMenuItem.Name = "seniorToolStripMenuItem";
            this.seniorToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.seniorToolStripMenuItem.Text = "高级";
            this.seniorToolStripMenuItem.Click += new System.EventHandler(this.seniorToolStripMenuItem_Click);
            // 
            // SaveToolStripMenuItem
            // 
            this.SaveToolStripMenuItem.Name = "SaveToolStripMenuItem";
            this.SaveToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.SaveToolStripMenuItem.Text = "保存本次游戏分数";
            this.SaveToolStripMenuItem.Click += new System.EventHandler(this.SaveToolStripMenuItem_Click);
            // 
            // rankToolStripMenuItem
            // 
            this.rankToolStripMenuItem.Name = "rankToolStripMenuItem";
            this.rankToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.rankToolStripMenuItem.Text = "排行榜";
            this.rankToolStripMenuItem.Click += new System.EventHandler(this.rankToolStripMenuItem_Click);
            // 
            // InstructionToolStripMenuItem
            // 
            this.InstructionToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.helpToolStripMenuItem,
            this.LoginToolStripMenuItem});
            this.InstructionToolStripMenuItem.Name = "InstructionToolStripMenuItem";
            this.InstructionToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.InstructionToolStripMenuItem.Text = "说明";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.helpToolStripMenuItem.Text = "帮助";
            this.helpToolStripMenuItem.Click += new System.EventHandler(this.helpToolStripMenuItem_Click);
            // 
            // LoginToolStripMenuItem
            // 
            this.LoginToolStripMenuItem.Name = "LoginToolStripMenuItem";
            this.LoginToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.LoginToolStripMenuItem.Text = "登录界面";
            this.LoginToolStripMenuItem.Click += new System.EventHandler(this.LoginToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(539, 524);
            this.Controls.Add(this.shiningLightsControl1);
            this.Controls.Add(this.menuStrip1);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private ShiningLightsControl shiningLightsControl1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem DifficultyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem primaryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem junioToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem seniorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem InstructionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem LoginToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem SaveToolStripMenuItem;
        private ToolStripMenuItem rankToolStripMenuItem;
    }
}

