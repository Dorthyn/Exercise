namespace Simonsimon
{
    partial class countdown
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

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.button_count = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 48);
            this.label1.TabIndex = 0;
            // 
            // button_count
            // 
            this.button_count.Location = new System.Drawing.Point(111, 91);
            this.button_count.Name = "button_count";
            this.button_count.Size = new System.Drawing.Size(75, 23);
            this.button_count.TabIndex = 1;
            this.button_count.Text = "Count";
            this.button_count.UseVisualStyleBackColor = true;
            this.button_count.Click += new System.EventHandler(this.button_count_Click);
            // 
            // countdown
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.button_count);
            this.Controls.Add(this.label1);
            this.Name = "countdown";
            this.Size = new System.Drawing.Size(213, 128);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button_count;
    }
}
