namespace trans
{
    partial class ClientForm
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
            this.textBox_path = new System.Windows.Forms.TextBox();
            this.button_DownLoad = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBox_path
            // 
            this.textBox_path.Location = new System.Drawing.Point(12, 12);
            this.textBox_path.Name = "textBox_path";
            this.textBox_path.Size = new System.Drawing.Size(407, 21);
            this.textBox_path.TabIndex = 0;
            this.textBox_path.Text = "C:\\Users\\M0015\\Desktop\\uiui.txt";
            // 
            // button_DownLoad
            // 
            this.button_DownLoad.Location = new System.Drawing.Point(162, 86);
            this.button_DownLoad.Name = "button_DownLoad";
            this.button_DownLoad.Size = new System.Drawing.Size(75, 23);
            this.button_DownLoad.TabIndex = 1;
            this.button_DownLoad.Text = "下载";
            this.button_DownLoad.UseVisualStyleBackColor = true;
            this.button_DownLoad.Click += new System.EventHandler(this.button_DownLoad_Click);
            // 
            // ClientForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(431, 158);
            this.Controls.Add(this.button_DownLoad);
            this.Controls.Add(this.textBox_path);
            this.Name = "ClientForm";
            this.Text = "ClientForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox_path;
        private System.Windows.Forms.Button button_DownLoad;
    }
}

