namespace WaveControl
{
    partial class UserWaveControl
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            this.chartOrigin = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.contextMenuStrip_Right = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.zoomXAxisToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.zoomYAxisToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.zoomWindowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.zoomResetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.showXYValueToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.legendVisibleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.autoYScaleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.savePictureToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsCSVToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.setYAxisRangeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            ((System.ComponentModel.ISupportInitialize)(this.chartOrigin)).BeginInit();
            this.contextMenuStrip_Right.SuspendLayout();
            this.SuspendLayout();
            // 
            // chartOrigin
            // 
            chartArea2.Name = "ChartArea1";
            this.chartOrigin.ChartAreas.Add(chartArea2);
            this.chartOrigin.ContextMenuStrip = this.contextMenuStrip_Right;
            legend2.Name = "Legend1";
            this.chartOrigin.Legends.Add(legend2);
            this.chartOrigin.Location = new System.Drawing.Point(22, 22);
            this.chartOrigin.Name = "chartOrigin";
            this.chartOrigin.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.chartOrigin.Size = new System.Drawing.Size(500, 500);
            this.chartOrigin.TabIndex = 0;
            this.chartOrigin.Text = "chartOrigin";
            this.chartOrigin.MouseDown += new System.Windows.Forms.MouseEventHandler(this.chartOrigin_MouseDown);
            // 
            // contextMenuStrip_Right
            // 
            this.contextMenuStrip_Right.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.zoomXAxisToolStripMenuItem,
            this.zoomYAxisToolStripMenuItem,
            this.zoomWindowToolStripMenuItem,
            this.zoomResetToolStripMenuItem,
            this.toolStripSeparator1,
            this.showXYValueToolStripMenuItem,
            this.legendVisibleToolStripMenuItem,
            this.autoYScaleToolStripMenuItem,
            this.savePictureToolStripMenuItem,
            this.saveAsCSVToolStripMenuItem,
            this.setYAxisRangeToolStripMenuItem,
            this.toolStripSeparator2});
            this.contextMenuStrip_Right.Name = "contextMenuStrip_Right";
            this.contextMenuStrip_Right.ShowCheckMargin = true;
            this.contextMenuStrip_Right.Size = new System.Drawing.Size(192, 236);
            this.contextMenuStrip_Right.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.contextMenuStrip_Right_ItemClicked);
            // 
            // zoomXAxisToolStripMenuItem
            // 
            this.zoomXAxisToolStripMenuItem.Name = "zoomXAxisToolStripMenuItem";
            this.zoomXAxisToolStripMenuItem.Size = new System.Drawing.Size(191, 22);
            this.zoomXAxisToolStripMenuItem.Text = "Zoom XAxis";
            this.zoomXAxisToolStripMenuItem.Click += new System.EventHandler(this.zoomXAxisToolStripMenuItem_Click);
            // 
            // zoomYAxisToolStripMenuItem
            // 
            this.zoomYAxisToolStripMenuItem.Name = "zoomYAxisToolStripMenuItem";
            this.zoomYAxisToolStripMenuItem.Size = new System.Drawing.Size(191, 22);
            this.zoomYAxisToolStripMenuItem.Text = "Zoom YAxis";
            this.zoomYAxisToolStripMenuItem.Click += new System.EventHandler(this.zoomYAxisToolStripMenuItem_Click);
            // 
            // zoomWindowToolStripMenuItem
            // 
            this.zoomWindowToolStripMenuItem.Name = "zoomWindowToolStripMenuItem";
            this.zoomWindowToolStripMenuItem.Size = new System.Drawing.Size(191, 22);
            this.zoomWindowToolStripMenuItem.Text = "Zoom Window";
            this.zoomWindowToolStripMenuItem.Click += new System.EventHandler(this.zoomWindowToolStripMenuItem_Click);
            // 
            // zoomResetToolStripMenuItem
            // 
            this.zoomResetToolStripMenuItem.Name = "zoomResetToolStripMenuItem";
            this.zoomResetToolStripMenuItem.Size = new System.Drawing.Size(191, 22);
            this.zoomResetToolStripMenuItem.Text = "Zoom Reset";
            this.zoomResetToolStripMenuItem.Click += new System.EventHandler(this.zoomResetToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(188, 6);
            // 
            // showXYValueToolStripMenuItem
            // 
            this.showXYValueToolStripMenuItem.Name = "showXYValueToolStripMenuItem";
            this.showXYValueToolStripMenuItem.Size = new System.Drawing.Size(191, 22);
            this.showXYValueToolStripMenuItem.Text = "Show XYValue";
            this.showXYValueToolStripMenuItem.Click += new System.EventHandler(this.showXYValueToolStripMenuItem_Click);
            // 
            // legendVisibleToolStripMenuItem
            // 
            this.legendVisibleToolStripMenuItem.Name = "legendVisibleToolStripMenuItem";
            this.legendVisibleToolStripMenuItem.Size = new System.Drawing.Size(191, 22);
            this.legendVisibleToolStripMenuItem.Text = "Legend Visible";
            this.legendVisibleToolStripMenuItem.Click += new System.EventHandler(this.legendVisibleToolStripMenuItem_Click);
            // 
            // autoYScaleToolStripMenuItem
            // 
            this.autoYScaleToolStripMenuItem.Name = "autoYScaleToolStripMenuItem";
            this.autoYScaleToolStripMenuItem.Size = new System.Drawing.Size(191, 22);
            this.autoYScaleToolStripMenuItem.Text = "Auto YScale";
            this.autoYScaleToolStripMenuItem.Click += new System.EventHandler(this.autoYScaleToolStripMenuItem_Click);
            // 
            // savePictureToolStripMenuItem
            // 
            this.savePictureToolStripMenuItem.Name = "savePictureToolStripMenuItem";
            this.savePictureToolStripMenuItem.Size = new System.Drawing.Size(191, 22);
            this.savePictureToolStripMenuItem.Text = "Save Picture";
            this.savePictureToolStripMenuItem.Click += new System.EventHandler(this.savePictureToolStripMenuItem_Click);
            // 
            // saveAsCSVToolStripMenuItem
            // 
            this.saveAsCSVToolStripMenuItem.Name = "saveAsCSVToolStripMenuItem";
            this.saveAsCSVToolStripMenuItem.Size = new System.Drawing.Size(191, 22);
            this.saveAsCSVToolStripMenuItem.Text = "Save as CSV";
            // 
            // setYAxisRangeToolStripMenuItem
            // 
            this.setYAxisRangeToolStripMenuItem.Name = "setYAxisRangeToolStripMenuItem";
            this.setYAxisRangeToolStripMenuItem.Size = new System.Drawing.Size(191, 22);
            this.setYAxisRangeToolStripMenuItem.Text = "Set YAxis Range";
            this.setYAxisRangeToolStripMenuItem.Click += new System.EventHandler(this.setYAxisRangeToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(188, 6);
            // 
            // UserWaveControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.chartOrigin);
            this.Name = "UserWaveControl";
            this.Size = new System.Drawing.Size(550, 550);
            ((System.ComponentModel.ISupportInitialize)(this.chartOrigin)).EndInit();
            this.contextMenuStrip_Right.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart chartOrigin;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip_Right;
        private System.Windows.Forms.ToolStripMenuItem zoomXAxisToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem zoomYAxisToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem zoomWindowToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem zoomResetToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showXYValueToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem legendVisibleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem autoYScaleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem savePictureToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsCSVToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem setYAxisRangeToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
    }
}
