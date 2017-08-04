namespace studentManagementInformationSystem
{
    partial class UpdateForm
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
            this.button_select = new System.Windows.Forms.Button();
            this.textBox_studentNo = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.textBox_grade = new System.Windows.Forms.TextBox();
            this.label_grade = new System.Windows.Forms.Label();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.label_birth = new System.Windows.Forms.Label();
            this.radioButton2_sex = new System.Windows.Forms.RadioButton();
            this.radioButton1_sex = new System.Windows.Forms.RadioButton();
            this.label_sex = new System.Windows.Forms.Label();
            this.textBox_name = new System.Windows.Forms.TextBox();
            this.label_studentName = new System.Windows.Forms.Label();
            this.textBox_student = new System.Windows.Forms.TextBox();
            this.label_studentNo = new System.Windows.Forms.Label();
            this.textBox_class = new System.Windows.Forms.TextBox();
            this.label_class = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.button_update = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button_select
            // 
            this.button_select.Location = new System.Drawing.Point(212, 147);
            this.button_select.Name = "button_select";
            this.button_select.Size = new System.Drawing.Size(75, 23);
            this.button_select.TabIndex = 6;
            this.button_select.Text = "查找";
            this.button_select.UseVisualStyleBackColor = true;
            this.button_select.Click += new System.EventHandler(this.button_select_Click);
            // 
            // textBox_studentNo
            // 
            this.textBox_studentNo.Location = new System.Drawing.Point(71, 105);
            this.textBox_studentNo.Name = "textBox_studentNo";
            this.textBox_studentNo.Size = new System.Drawing.Size(393, 21);
            this.textBox_studentNo.TabIndex = 5;
            this.textBox_studentNo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_studentNo_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 30F);
            this.label1.Location = new System.Drawing.Point(64, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(417, 40);
            this.label1.TabIndex = 4;
            this.label1.Text = "请输入要修改学生学号";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.textBox_grade);
            this.panel1.Controls.Add(this.label_grade);
            this.panel1.Controls.Add(this.dateTimePicker1);
            this.panel1.Controls.Add(this.label_birth);
            this.panel1.Controls.Add(this.radioButton2_sex);
            this.panel1.Controls.Add(this.radioButton1_sex);
            this.panel1.Controls.Add(this.label_sex);
            this.panel1.Controls.Add(this.textBox_name);
            this.panel1.Controls.Add(this.label_studentName);
            this.panel1.Controls.Add(this.textBox_student);
            this.panel1.Controls.Add(this.label_studentNo);
            this.panel1.Controls.Add(this.textBox_class);
            this.panel1.Controls.Add(this.label_class);
            this.panel1.Location = new System.Drawing.Point(71, 214);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(379, 301);
            this.panel1.TabIndex = 8;
            // 
            // textBox_grade
            // 
            this.textBox_grade.Location = new System.Drawing.Point(74, 271);
            this.textBox_grade.Name = "textBox_grade";
            this.textBox_grade.Size = new System.Drawing.Size(100, 21);
            this.textBox_grade.TabIndex = 12;
            this.textBox_grade.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_grade_KeyPress);
            // 
            // label_grade
            // 
            this.label_grade.AutoSize = true;
            this.label_grade.Location = new System.Drawing.Point(27, 271);
            this.label_grade.Name = "label_grade";
            this.label_grade.Size = new System.Drawing.Size(29, 12);
            this.label_grade.TabIndex = 11;
            this.label_grade.Text = "成绩";
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(85, 202);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(200, 21);
            this.dateTimePicker1.TabIndex = 10;
            // 
            // label_birth
            // 
            this.label_birth.AutoSize = true;
            this.label_birth.Location = new System.Drawing.Point(25, 202);
            this.label_birth.Name = "label_birth";
            this.label_birth.Size = new System.Drawing.Size(53, 12);
            this.label_birth.TabIndex = 9;
            this.label_birth.Text = "出生日期";
            // 
            // radioButton2_sex
            // 
            this.radioButton2_sex.AutoSize = true;
            this.radioButton2_sex.Location = new System.Drawing.Point(245, 134);
            this.radioButton2_sex.Name = "radioButton2_sex";
            this.radioButton2_sex.Size = new System.Drawing.Size(59, 16);
            this.radioButton2_sex.TabIndex = 8;
            this.radioButton2_sex.TabStop = true;
            this.radioButton2_sex.Text = "female";
            this.radioButton2_sex.UseVisualStyleBackColor = true;
            // 
            // radioButton1_sex
            // 
            this.radioButton1_sex.AutoSize = true;
            this.radioButton1_sex.Location = new System.Drawing.Point(245, 99);
            this.radioButton1_sex.Name = "radioButton1_sex";
            this.radioButton1_sex.Size = new System.Drawing.Size(47, 16);
            this.radioButton1_sex.TabIndex = 7;
            this.radioButton1_sex.TabStop = true;
            this.radioButton1_sex.Text = "male";
            this.radioButton1_sex.UseVisualStyleBackColor = true;
            // 
            // label_sex
            // 
            this.label_sex.AutoSize = true;
            this.label_sex.Location = new System.Drawing.Point(209, 99);
            this.label_sex.Name = "label_sex";
            this.label_sex.Size = new System.Drawing.Size(29, 12);
            this.label_sex.TabIndex = 6;
            this.label_sex.Text = "性别";
            // 
            // textBox_name
            // 
            this.textBox_name.Location = new System.Drawing.Point(74, 91);
            this.textBox_name.Name = "textBox_name";
            this.textBox_name.Size = new System.Drawing.Size(100, 21);
            this.textBox_name.TabIndex = 5;
            // 
            // label_studentName
            // 
            this.label_studentName.AutoSize = true;
            this.label_studentName.Location = new System.Drawing.Point(23, 91);
            this.label_studentName.Name = "label_studentName";
            this.label_studentName.Size = new System.Drawing.Size(53, 12);
            this.label_studentName.TabIndex = 4;
            this.label_studentName.Text = "学生姓名";
            // 
            // textBox_student
            // 
            this.textBox_student.Enabled = false;
            this.textBox_student.Location = new System.Drawing.Point(274, 37);
            this.textBox_student.Name = "textBox_student";
            this.textBox_student.Size = new System.Drawing.Size(100, 21);
            this.textBox_student.TabIndex = 3;
            // 
            // label_studentNo
            // 
            this.label_studentNo.AutoSize = true;
            this.label_studentNo.Location = new System.Drawing.Point(207, 37);
            this.label_studentNo.Name = "label_studentNo";
            this.label_studentNo.Size = new System.Drawing.Size(53, 12);
            this.label_studentNo.TabIndex = 2;
            this.label_studentNo.Text = "学生学号";
            // 
            // textBox_class
            // 
            this.textBox_class.Location = new System.Drawing.Point(74, 34);
            this.textBox_class.Name = "textBox_class";
            this.textBox_class.Size = new System.Drawing.Size(100, 21);
            this.textBox_class.TabIndex = 1;
            this.textBox_class.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_class_KeyPress);
            // 
            // label_class
            // 
            this.label_class.AutoSize = true;
            this.label_class.Location = new System.Drawing.Point(23, 34);
            this.label_class.Name = "label_class";
            this.label_class.Size = new System.Drawing.Size(41, 12);
            this.label_class.TabIndex = 0;
            this.label_class.Text = "班级号";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(227, 180);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 7;
            this.label2.Text = "学生信息";
            // 
            // button_update
            // 
            this.button_update.Location = new System.Drawing.Point(212, 551);
            this.button_update.Name = "button_update";
            this.button_update.Size = new System.Drawing.Size(75, 23);
            this.button_update.TabIndex = 9;
            this.button_update.Text = "确认修改";
            this.button_update.UseVisualStyleBackColor = true;
            this.button_update.Click += new System.EventHandler(this.button_update_Click);
            // 
            // UpdateForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(599, 654);
            this.Controls.Add(this.button_update);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button_select);
            this.Controls.Add(this.textBox_studentNo);
            this.Controls.Add(this.label1);
            this.Name = "UpdateForm";
            this.Text = "Update";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_select;
        private System.Windows.Forms.TextBox textBox_studentNo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox textBox_grade;
        private System.Windows.Forms.Label label_grade;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Label label_birth;
        private System.Windows.Forms.RadioButton radioButton2_sex;
        private System.Windows.Forms.RadioButton radioButton1_sex;
        private System.Windows.Forms.Label label_sex;
        private System.Windows.Forms.TextBox textBox_name;
        private System.Windows.Forms.Label label_studentName;
        private System.Windows.Forms.TextBox textBox_student;
        private System.Windows.Forms.Label label_studentNo;
        private System.Windows.Forms.TextBox textBox_class;
        private System.Windows.Forms.Label label_class;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button_update;
    }
}