namespace Simonsimon
{
    partial class HelpForm
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
            this.textBox_help = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // textBox_help
            // 
            this.textBox_help.BackColor = System.Drawing.SystemColors.Control;
            this.textBox_help.Font = new System.Drawing.Font("Times New Roman", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_help.Location = new System.Drawing.Point(28, 25);
            this.textBox_help.Multiline = true;
            this.textBox_help.Name = "textBox_help";
            this.textBox_help.Size = new System.Drawing.Size(449, 452);
            this.textBox_help.TabIndex = 0;
            // 
            // HelpForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(509, 515);
            this.Controls.Add(this.textBox_help);
            this.Name = "HelpForm";
            this.Text = "HelpForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox_help;
    }
}