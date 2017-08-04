namespace Simonsimon
{
    partial class Register_Login_MainForm
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
            this.label2 = new System.Windows.Forms.Label();
            this.textBox_user = new System.Windows.Forms.TextBox();
            this.textBox_psw = new System.Windows.Forms.TextBox();
            this.button_register = new System.Windows.Forms.Button();
            this.button_login = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(31, 56);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "用户名";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(34, 94);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "密码";
            // 
            // textBox_user
            // 
            this.textBox_user.Location = new System.Drawing.Point(90, 56);
            this.textBox_user.Name = "textBox_user";
            this.textBox_user.Size = new System.Drawing.Size(100, 21);
            this.textBox_user.TabIndex = 2;
            // 
            // textBox_psw
            // 
            this.textBox_psw.Location = new System.Drawing.Point(90, 94);
            this.textBox_psw.Name = "textBox_psw";
            this.textBox_psw.PasswordChar = '*';
            this.textBox_psw.Size = new System.Drawing.Size(100, 21);
            this.textBox_psw.TabIndex = 3;
            // 
            // button_register
            // 
            this.button_register.Location = new System.Drawing.Point(197, 200);
            this.button_register.Name = "button_register";
            this.button_register.Size = new System.Drawing.Size(75, 23);
            this.button_register.TabIndex = 4;
            this.button_register.Text = "register";
            this.button_register.UseVisualStyleBackColor = true;
            this.button_register.Click += new System.EventHandler(this.button_register_Click);
            // 
            // button_login
            // 
            this.button_login.Location = new System.Drawing.Point(100, 154);
            this.button_login.Name = "button_login";
            this.button_login.Size = new System.Drawing.Size(75, 23);
            this.button_login.TabIndex = 5;
            this.button_login.Text = "login";
            this.button_login.UseVisualStyleBackColor = true;
            this.button_login.Click += new System.EventHandler(this.button_login_Click);
            // 
            // Register_Login_MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.button_login);
            this.Controls.Add(this.button_register);
            this.Controls.Add(this.textBox_psw);
            this.Controls.Add(this.textBox_user);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "Register_Login_MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Register_Login_MainForm";
            this.Load += new System.EventHandler(this.Register_Login_MainForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox_user;
        private System.Windows.Forms.TextBox textBox_psw;
        private System.Windows.Forms.Button button_register;
        private System.Windows.Forms.Button button_login;
    }
}