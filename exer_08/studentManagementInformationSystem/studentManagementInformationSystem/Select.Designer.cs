namespace studentManagementInformationSystem
{
    partial class SelectForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.textBox_show = new System.Windows.Forms.TextBox();
            this.textBox_studentNo = new System.Windows.Forms.TextBox();
            this.button_select = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 30F);
            this.label1.Location = new System.Drawing.Point(82, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(417, 40);
            this.label1.TabIndex = 0;
            this.label1.Text = "请输入要查找学生学号";
            // 
            // textBox_show
            // 
            this.textBox_show.Enabled = false;
            this.textBox_show.Location = new System.Drawing.Point(12, 197);
            this.textBox_show.Multiline = true;
            this.textBox_show.Name = "textBox_show";
            this.textBox_show.Size = new System.Drawing.Size(524, 250);
            this.textBox_show.TabIndex = 1;
            // 
            // textBox_studentNo
            // 
            this.textBox_studentNo.Location = new System.Drawing.Point(89, 101);
            this.textBox_studentNo.Name = "textBox_studentNo";
            this.textBox_studentNo.Size = new System.Drawing.Size(393, 21);
            this.textBox_studentNo.TabIndex = 2;
            this.textBox_studentNo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_studentNo_KeyPress);
            // 
            // button_select
            // 
            this.button_select.Location = new System.Drawing.Point(230, 143);
            this.button_select.Name = "button_select";
            this.button_select.Size = new System.Drawing.Size(75, 23);
            this.button_select.TabIndex = 3;
            this.button_select.Text = "查找";
            this.button_select.UseVisualStyleBackColor = true;
            this.button_select.Click += new System.EventHandler(this.button_select_Click);
            // 
            // SelectForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(548, 526);
            this.Controls.Add(this.button_select);
            this.Controls.Add(this.textBox_studentNo);
            this.Controls.Add(this.textBox_show);
            this.Controls.Add(this.label1);
            this.Name = "SelectForm";
            this.Text = "查找";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox_show;
        private System.Windows.Forms.TextBox textBox_studentNo;
        private System.Windows.Forms.Button button_select;
    }
}