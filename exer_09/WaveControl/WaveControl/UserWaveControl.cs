using System;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
//关于series的属性要在plot之后设置
//chart控件的背景色，要在plot之前设置
namespace WaveControl
{
    public partial class UserWaveControl : UserControl
    {
        //private delegate void UpdateDelegate(double[] data);
        //private delegate void UpdateDelegate2(double[,] data);
        //曲线条数，可自动生成
        private static int seriesNum;

        //默认横纵坐标最大值，默认为100
        private int xMax = 100;
        private int yMax = 100;



        //默认XY坐标轴的最小值
        private int xMin = 0;
        private int yMin = 0;
        //传进来的y的一维数组
        //private double[] y;
        ////传进来的y的二维数组
        //private double[,] ys;

        //把series的颜色属性暴露出来
        private Color seriesColor1;
        private Color seriesColor2;
        private Color seriesColor3;
        private Color seriesColor4;
        //把网格线的属性暴露出来
        private Color gridChartColor = Color.Gray;
        //线型
        private SeriesChartType lineType;
        private string xAxisName = "X坐标轴";
        //y轴标题
        private string yAxisName = "Y坐标轴";

        //chart控件的背景色，要在plot之前设置
        private Color chart_back_Color = Color.GhostWhite;
        //chartArea的背景色，要在plot之前设置
        private Color chartArea_back_Color = Color.GhostWhite;
        //chartArea的3D效果
        private bool chartArea3DEnable = false;



        //series颜色数组
        private Series[] seriesColor;
        //private int[] seriesColor;


        #region Field

        //series数组，如果让用户设置要产生多少条线，就不好用了
        //public int SeriesNum
        //{
        //    set
        //    {
        //        this.seriesNum = value;
        //        Series[] s = new Series[this.seriesNum];
        //    }
        //}
        public Series[] SeriesColor
        {
            set { this.seriesColor = value; }
            get { return this.seriesColor; }
        }

        public int XMax
        {
            get { return xMax; }
            set { this.xMax = value; }
        }

        public int YMax
        {
            get { return yMax; }
            set { this.yMax = value; }
        }

        public int XMin
        {
            get { return xMin; }
            set { this.xMin = value; }
        }

        public int YMin
        {
            get { return yMin; }
            set { this.yMin = value; }
        }

        //public double[] Y
        //{
        //    get { return y; }
        //    set { this.y = value; }
        //}

        public Color SeriesColor1
        {
            set { seriesColor1 = value; }
        }
        public Color SeriesColor2
        {
            set { seriesColor2 = value; }
        }
        public Color SeriesColor3
        {
            set { seriesColor3 = value; }
        }
        public Color SeriesColor4
        {
            set { seriesColor4 = value; }
        }

        public SeriesChartType LineType
        {
            set { lineType = value; }
        }

        public Color GridChartColor
        {
            set { gridChartColor = value; }
        }

        public string YAxisName
        {
            set { yAxisName = value; }
        }

        public string XAxisName
        {
            set { xAxisName = value; }
        }

        public Color Chart_Back_Color
        {
            set { chart_back_Color = value; }
        }

        public Color ChartArea_back_Color
        {
            set { chartArea_back_Color = value; }
        }

        public bool ChartArea3DEnable
        {
            set { chartArea3DEnable = value; }
        }
        //private int[] SeriesColor
        //{
        //    set { seriesColor = new int[value]; }
        //}



        #endregion



        //默认构造函数，设置默认颜色及样式
        public UserWaveControl()
        {
            InitializeComponent();

            //设置默认坐标范围
            //chartOrigin.ChartAreas[0].AxisX.Minimum = xMin;
            //chartOrigin.ChartAreas[0].AxisY.Minimum = yMin;

            //chartOrigin.ChartAreas[0].AxisX.Maximum = xMax;
            //chartOrigin.ChartAreas[0].AxisY.Maximum = yMax;

            //实现鼠标拖拽局部放大的功能
            chartOrigin.ChartAreas[0].CursorX.IsUserEnabled = true;
            chartOrigin.ChartAreas[0].CursorY.IsUserEnabled = true;

            chartOrigin.ChartAreas[0].CursorX.IsUserSelectionEnabled = true;
            chartOrigin.ChartAreas[0].CursorY.IsUserSelectionEnabled = true;

            chartOrigin.ChartAreas[0].CursorX.Interval = 0;
            chartOrigin.ChartAreas[0].CursorY.Interval = 0;

            chartOrigin.ChartAreas[0].CursorX.IntervalOffset = 0;
            chartOrigin.ChartAreas[0].CursorY.IntervalOffset = 0;

            //chartOrigin.ChartAreas[0].CursorX.IntervalType = DateTimeIntervalType.Minutes;
            //chartOrigin.ChartAreas[0].CursorY.IntervalType = DateTimeIntervalType.Minutes;

            chartOrigin.ChartAreas[0].AxisX.ScaleView.Zoomable = true;
            chartOrigin.ChartAreas[0].AxisY.ScaleView.Zoomable = true;

            chartOrigin.ChartAreas[0].AxisX.ScrollBar.IsPositionedInside = false;
            chartOrigin.ChartAreas[0].AxisY.ScrollBar.IsPositionedInside = false;
            //chartOrigin.ForeColor = Color.Brown;

            showXYValueToolStripMenuItem.Checked = true;
            //默认自动调整y轴
            autoYScaleToolStripMenuItem.Checked = true;
            //series显示
            //series1ToolStripMenuItem.Checked = true;
            //series2ToolStripMenuItem.Checked = true;
            //series3ToolStripMenuItem.Checked = true;
            //series4ToolStripMenuItem.Checked = true;
            //legend显示
            legendVisibleToolStripMenuItem.Checked = true;
            //拖拽
            zoomXAxisToolStripMenuItem.Checked = true;
            zoomYAxisToolStripMenuItem.Checked = true;
            zoomWindowToolStripMenuItem.Checked = true;

            chartOrigin.ChartAreas[0].AxisX.Title = xAxisName;
            //设置y轴标题
            chartOrigin.ChartAreas[0].AxisY.Title = yAxisName;

            //设置网格线的颜色
            chartOrigin.ChartAreas[0].AxisX.MajorGrid.LineColor = gridChartColor;
            chartOrigin.ChartAreas[0].AxisY.MajorGrid.LineColor = gridChartColor;

            chartOrigin.GetToolTipText += new EventHandler<ToolTipEventArgs>(chart_GetToolTipText);

            //设置间隔
            //chartOrigin.ChartAreas[0].AxisX.Interval = 0.5;
            //chartOrigin.ChartAreas[0].AxisY.Interval = 0.5;

            //设置线型
            //chartOrigin.Series[0].ChartType = SeriesChartType.Bubble;
        }

        //一个输入时的plot函数
        /// <summary>
        /// 只有y的输入时的plot函数
        /// </summary>
        /// <param name="y"></param>
        public void Plot(double[] y)
        {
            Series s1 = new Series("series1");
            s1.ChartType = lineType;
            chartOrigin.Series.Add(s1);
            //s1.ChartType = SeriesChartType.FastLine;
            chartOrigin.ChartAreas[0].AxisX.Title = xAxisName;
            //设置y轴标题
            chartOrigin.ChartAreas[0].AxisY.Title = yAxisName;

            //设置线条颜色
            chartOrigin.Series[0].Color = seriesColor1;


            int i = 0;
            for (; xMin < y.Length; xMin++)
            {
                chartOrigin.Series[0].Points.AddXY(xMin, y[i++]);
            }

            //for (; (xMin < y.Length) && (i < y.Length); xMin++)
            //{
            //    chartOrigin.Series[0].Points.AddXY(xMin, y[i]);
            //}

            //if (chartOrigin.InvokeRequired)
            //{
            //    UpdateDelegate updateDelegate = new UpdateDelegate(Plot);
            //    this.Invoke(updateDelegate, new object[] { y });
            //}
            //else
            //{
            s1.IsValueShownAsLabel = true;
            chartOrigin.BackColor = chart_back_Color;
            chartOrigin.ChartAreas[0].BackColor = chartArea_back_Color;
            chartOrigin.ChartAreas[0].Area3DStyle.Enable3D = chartArea3DEnable;
            chartOrigin.ResetAutoValues();
            //chartOrigin.Invalidate();
            //}

        }

        //多条线的plot函数
        //array.GetLength(0);//如果是二维数组则获取第一维的长度，即行数
        //array.GetLength(1);//如果是二维数组则获取第二维的长度，即列数
        public void Plot(double[,] ys)
        {
            int row = ys.GetLength(0);
            int col = ys.GetLength(1);

            double[] tmp = new double[col];
            //声明row个变量
            Series[] s = new Series[row];
            seriesColor = s;
            for (int i = 0; i < row; i++)
            {
                seriesColor[i] = new Series("series" + i.ToString());
                chartOrigin.Series.Add(seriesColor[i]);
                //s[i] = new Series("series" + i.ToString());
                //chartOrigin.Series.Add(s[i]);
            }

            for (int i = 0; i < row; i++)
            {
                //获取一行数据到tmp中
                for (int j = 0; j < col; j++)
                {
                    tmp[j] = ys[i, j];
                }

                int k = 0;
                for (; xMin < col; xMin++)
                {
                    chartOrigin.Series[i].Points.AddXY(xMin, tmp[k++]);
                    seriesColor[i].ChartType = lineType;
                    //chartOrigin.Series[i].Points.AddXY(xMin, tmp[k++]);
                    //s[i].ChartType = lineType;
                }
                xMin = 0;

                //chartOrigin.ResetAutoValues();
                //chartOrigin.Invalidate();
            }
            //chartOrigin.Invalidate();
            chartOrigin.BackColor = chart_back_Color;
            chartOrigin.ChartAreas[0].BackColor = chartArea_back_Color;
            chartOrigin.ChartAreas[0].Area3DStyle.Enable3D = chartArea3DEnable;
            chartOrigin.ResetAutoValues();

            //获取series的数量
            seriesNum = s.Length;

            
        }

        public void Plot(double[] x, double[] y)
        {
            Series s1 = new Series("series1");
            s1.ChartType = lineType;
            s1.ChartType = lineType;

            chartOrigin.Series.Add(s1);
            //s1.ChartType = SeriesChartType.FastLine;
            chartOrigin.ChartAreas[0].AxisX.Title = xAxisName;
            //设置y轴标题
            chartOrigin.ChartAreas[0].AxisY.Title = yAxisName;

            //设置线条颜色
            chartOrigin.Series[0].Color = seriesColor1;


            int len;
            if (x.Length > y.Length)
            {
                len = y.Length;
            }
            else
            {
                len = x.Length;
            }

            for (int i = 0; i < len; i++)
            {
                chartOrigin.Series[0].Points.AddXY(x[i++], y[i++]);
            }
            chartOrigin.BackColor = chart_back_Color;
            chartOrigin.ChartAreas[0].BackColor = chartArea_back_Color;
            chartOrigin.ChartAreas[0].Area3DStyle.Enable3D = chartArea3DEnable;
            chartOrigin.ResetAutoValues();

        }

        public void Plot(double[,] xs, double[,] ys)
        {
            int row = 0;
            if (xs.GetLength(0) > ys.GetLength(0))
            {
                row = ys.GetLength(0);
            }
            else
            {
                row = xs.GetLength(0);
            }

            int col = 0;
            if (xs.GetLength(1) > ys.GetLength(1))
            {
                col = ys.GetLength(1);
            }
            else
            {
                col = xs.GetLength(1);
            }
            //tmp用来存储一行的数据
            double[] tmp_x = new double[col];
            double[] tmp_y = new double[col];
            //声明row个变量
            Series[] s = new Series[row];
            seriesColor = s;
            for (int i = 0; i < row; i++)
            {
                seriesColor[i] = new Series("series" + i);
                seriesColor[i].ChartType = lineType;
                chartOrigin.Series.Add(seriesColor[i]);
            }

            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    //横坐标数组
                    tmp_x[j] = xs[i, j];
                    //纵坐标数组
                    tmp_y[j] = ys[i, j];
                }

                int k1 = 0;
                for (int k = 0; k < col; k++)
                {
                    k1 = k;
                    chartOrigin.Series[i].Points.AddXY(tmp_x[k1], tmp_y[k1]);
                    k1++;
                }

            }
            //获取series的数量
            seriesNum = s.Length;
            chartOrigin.ChartAreas[0].BackColor = chartArea_back_Color;
            chartOrigin.ChartAreas[0].Area3DStyle.Enable3D = chartArea3DEnable;
            chartOrigin.BackColor = chart_back_Color;
            chartOrigin.ResetAutoValues();
        }

        //输入两条线时
        //public void Plot(double[] y1, double[] y2)
        //{
        //    Series s1 = new Series("series1");
        //    Series s2 = new Series("series2");
        //    s1.ChartType = lineType;
        //    s2.ChartType = lineType;

        //    chartOrigin.Series.Add(s1);
        //    chartOrigin.Series.Add(s2);
        //    //s1.ChartType = SeriesChartType.FastLine;

        //    chartOrigin.ChartAreas[0].AxisX.Title = xAxisName;
        //    //设置y轴标题
        //    chartOrigin.ChartAreas[0].AxisY.Title = yAxisName;

        //    //设置线条颜色
        //    chartOrigin.Series[0].Color = seriesColor1;
        //    chartOrigin.Series[1].Color = seriesColor2;
        //    //设置网格线的颜色
        //    chartOrigin.ChartAreas[0].AxisX.MajorGrid.LineColor = gridChartColor;
        //    chartOrigin.ChartAreas[0].AxisY.MajorGrid.LineColor = gridChartColor;

        //    int i = 0;
        //    for (; xMin < y1.Length; xMin++)
        //    {
        //        chartOrigin.Series[0].Points.AddXY(xMin, y1[i++]);
        //    }

        //    i = 0;
        //    xMin = 0;
        //    for (; xMin < y2.Length; xMin++)
        //    {
        //        chartOrigin.Series[1].Points.AddXY(xMin, y2[i++]);
        //    }

        //    if (chartOrigin.InvokeRequired)
        //    {
        //        UpdateDelegate2 updateDelegate2 = new UpdateDelegate2(Plot);
        //        this.Invoke(updateDelegate2, new object[] { y });
        //    }
        //    else
        //    {
        //        chartOrigin.ResetAutoValues();
        //        chartOrigin.Invalidate();
        //    }
        //}

        //设置坐标轴默认显示
        public void AxisReset()
        {
            chartOrigin.ChartAreas[0].AxisX.Maximum = 100;
        }

        private void zoomXAxisToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (zoomXAxisToolStripMenuItem.Checked)
            {
                zoomXAxisToolStripMenuItem.Checked = false;

                chartOrigin.ChartAreas[0].CursorX.IsUserEnabled = false;
                chartOrigin.ChartAreas[0].CursorX.IsUserSelectionEnabled = false;
                chartOrigin.ChartAreas[0].AxisX.ScaleView.Zoomable = false;

                //chartOrigin.ChartAreas[0].AxisX.ScrollBar.Enabled = false;
                //chartOrigin.ChartAreas[0].AxisY.ScrollBar.Enabled = false;
            }
            else
            {
                zoomXAxisToolStripMenuItem.Checked = true;

                chartOrigin.ChartAreas[0].CursorX.IsUserEnabled = true;
                chartOrigin.ChartAreas[0].CursorX.IsUserSelectionEnabled = true;
                chartOrigin.ChartAreas[0].CursorX.Interval = 0;
                chartOrigin.ChartAreas[0].CursorX.IntervalOffset = 0;
                //chartOrigin.ChartAreas[0].CursorX.IntervalType = DateTimeIntervalType.Minutes;
                chartOrigin.ChartAreas[0].AxisX.ScaleView.Zoomable = true;
            }
        }

        private void zoomYAxisToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (zoomYAxisToolStripMenuItem.Checked)
            {
                zoomYAxisToolStripMenuItem.Checked = false;

                chartOrigin.ChartAreas[0].CursorY.IsUserEnabled = false;
                chartOrigin.ChartAreas[0].CursorY.IsUserSelectionEnabled = false;
                chartOrigin.ChartAreas[0].AxisY.ScaleView.Zoomable = false;
            }
            else
            {
                zoomYAxisToolStripMenuItem.Checked = true;

                chartOrigin.ChartAreas[0].CursorY.IsUserEnabled = true;
                chartOrigin.ChartAreas[0].CursorY.IsUserSelectionEnabled = true;
                chartOrigin.ChartAreas[0].CursorY.Interval = 0;
                chartOrigin.ChartAreas[0].CursorY.IntervalOffset = 0;
                //chartOrigin.ChartAreas[0].CursorY.IntervalType = DateTimeIntervalType.Minutes;
                chartOrigin.ChartAreas[0].AxisY.ScaleView.Zoomable = true;
            }
        }

        private void zoomWindowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (zoomWindowToolStripMenuItem.Checked)
            {
                zoomXAxisToolStripMenuItem.Checked = false;
                zoomYAxisToolStripMenuItem.Checked = false;
                zoomWindowToolStripMenuItem.Checked = false;

                chartOrigin.ChartAreas[0].CursorX.IsUserEnabled = false;
                chartOrigin.ChartAreas[0].CursorY.IsUserEnabled = false;

                chartOrigin.ChartAreas[0].CursorX.IsUserSelectionEnabled = false;
                chartOrigin.ChartAreas[0].CursorY.IsUserSelectionEnabled = false;

                //chartOrigin.ChartAreas[0].CursorX.Interval = 0;
                //chartOrigin.ChartAreas[0].CursorY.Interval = 0;

                //chartOrigin.ChartAreas[0].CursorX.IntervalOffset = 0;
                //chartOrigin.ChartAreas[0].CursorY.IntervalOffset = 0;

                //chartOrigin.ChartAreas[0].CursorX.IntervalType = DateTimeIntervalType.Minutes;
                //chartOrigin.ChartAreas[0].CursorY.IntervalType = DateTimeIntervalType.Minutes;

                chartOrigin.ChartAreas[0].AxisX.ScaleView.Zoomable = false;
                chartOrigin.ChartAreas[0].AxisY.ScaleView.Zoomable = false;

            }
            else
            {
                zoomWindowToolStripMenuItem.Checked = true;
                zoomXAxisToolStripMenuItem.Checked = true;
                zoomYAxisToolStripMenuItem.Checked = true;

                chartOrigin.ChartAreas[0].CursorX.IsUserEnabled = true;
                chartOrigin.ChartAreas[0].CursorY.IsUserEnabled = true;

                chartOrigin.ChartAreas[0].CursorX.IsUserSelectionEnabled = true;
                chartOrigin.ChartAreas[0].CursorY.IsUserSelectionEnabled = true;

                chartOrigin.ChartAreas[0].CursorX.Interval = 0;
                chartOrigin.ChartAreas[0].CursorY.Interval = 0;

                chartOrigin.ChartAreas[0].CursorX.IntervalOffset = 0;
                chartOrigin.ChartAreas[0].CursorY.IntervalOffset = 0;

                //chartOrigin.ChartAreas[0].CursorX.IntervalType = DateTimeIntervalType.Minutes;
                //chartOrigin.ChartAreas[0].CursorY.IntervalType = DateTimeIntervalType.Minutes;

                chartOrigin.ChartAreas[0].AxisX.ScaleView.Zoomable = true;
                chartOrigin.ChartAreas[0].AxisY.ScaleView.Zoomable = true;
            }
        }

        private void zoomResetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            chartOrigin.ChartAreas[0].AxisX.Minimum = Double.NaN;
            chartOrigin.ChartAreas[0].AxisX.Maximum = Double.NaN;

            chartOrigin.ChartAreas[0].AxisY.Minimum = Double.NaN;
            chartOrigin.ChartAreas[0].AxisY.Maximum = Double.NaN;

            chartOrigin.ChartAreas[0].AxisX.LabelStyle.Enabled = true;
            chartOrigin.ChartAreas[0].AxisY.LabelStyle.Enabled = true;

            chartOrigin.ChartAreas[0].AxisX.ScaleView.ZoomReset(0);
            chartOrigin.ChartAreas[0].AxisY.ScaleView.ZoomReset(0);
        }

        private void showXYValueToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (showXYValueToolStripMenuItem.Checked)
            {
                showXYValueToolStripMenuItem.Checked = false;
                chartOrigin.ChartAreas[0].AxisX.LabelStyle.Enabled = false;
                chartOrigin.ChartAreas[0].AxisY.LabelStyle.Enabled = false;
            }
            else
            {
                showXYValueToolStripMenuItem.Checked = true;
                chartOrigin.ChartAreas[0].AxisX.LabelStyle.Enabled = true;
                chartOrigin.ChartAreas[0].AxisY.LabelStyle.Enabled = true;
                //for (int i = 0; i < chartOrigin.Series.Count; i++)
                //{
                    
                //}
            }
        }

        private void legendVisibleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (legendVisibleToolStripMenuItem.Checked)
            {
                legendVisibleToolStripMenuItem.Checked = false;
                //Legend不可见
                //chartOrigin.Legends.Clear();
                for (int i = 0; i < chartOrigin.Legends.Count; i++)
                {
                    chartOrigin.Legends[i].Enabled = false;
                }
            }
            else
            {
                legendVisibleToolStripMenuItem.Checked = true;
                for (int i = 0; i < chartOrigin.Legends.Count; i++)
                {
                    chartOrigin.Legends[i].Enabled = true;
                }
            }
        }

        private void autoYScaleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (autoYScaleToolStripMenuItem.Checked)
            {
                autoYScaleToolStripMenuItem.Checked = false;
                //设置默认显示的最大值
                chartOrigin.ChartAreas[0].AxisY.Maximum = yMax;
                chartOrigin.ChartAreas[0].AxisY.Minimum = yMin;
            }
            else
            {
                autoYScaleToolStripMenuItem.Checked = true;

                //勾选则为自动，默认为勾选
                chartOrigin.ChartAreas[0].AxisY.Minimum = Double.NaN;
                chartOrigin.ChartAreas[0].AxisY.Maximum = Double.NaN;

            }
        }

        private void savePictureToolStripMenuItem_Click(object sender, EventArgs e)
        {

                //选中保存为图片
                SaveFileDialog saveDia = new SaveFileDialog();
                saveDia.Filter = "pic|*.png";
                saveDia.Title = "导出为图片";

                if ((saveDia.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    && !string.Empty.Equals(saveDia.FileName))
                {
                    chartOrigin.SaveImage(saveDia.FileName, ChartImageFormat.Png);
                }

        }

        private void setYAxisRangeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (setYAxisRangeToolStripMenuItem.Checked)
            {
                setYAxisRangeToolStripMenuItem.Checked = false;
            }
            else
            {
                setYAxisRangeToolStripMenuItem.Checked = true;
            }
        }

        //private void series1ToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    if (chartOrigin.Series.Count < 1)
        //    {
        //        MessageBox.Show("不存在！", "提示");
        //    }
        //    else
        //    {
        //        if (series1ToolStripMenuItem.Checked)
        //        {
        //            series1ToolStripMenuItem.Checked = false;
        //            chartOrigin.Series[0].Enabled = false;
        //        }
        //        else
        //        {
        //            series1ToolStripMenuItem.Checked = true;
        //            chartOrigin.Series[0].Enabled = true;
        //        }
        //    }
        //}

        //private void series2ToolStripMenuItem_Click_1(object sender, EventArgs e)
        //{

        //    if (chartOrigin.Series.Count < 2)
        //    {
        //        MessageBox.Show("不存在！", "提示");
        //    }
        //    else
        //    {
        //        if (series2ToolStripMenuItem.Checked)
        //        {
        //            series2ToolStripMenuItem.Checked = false;
        //            chartOrigin.Series[1].Enabled = false;
        //        }
        //        else
        //        {
        //            series2ToolStripMenuItem.Checked = true;
        //            chartOrigin.Series[1].Enabled = true;
        //        }
        //    }
        //}

        //private void series3ToolStripMenuItem_Click_1(object sender, EventArgs e)
        //{
        //    if (chartOrigin.Series.Count < 3)
        //    {
        //        MessageBox.Show("不存在！", "提示");
        //    }
        //    else
        //    {
        //        if (series3ToolStripMenuItem.Checked)
        //        {
        //            series3ToolStripMenuItem.Checked = false;
        //            chartOrigin.Series[2].Enabled = false;
        //        }
        //        else
        //        {
        //            series3ToolStripMenuItem.Checked = true;
        //            chartOrigin.Series[2].Enabled = true;
        //        }
        //    }
        //}

        //private void series4ToolStripMenuItem_Click_1(object sender, EventArgs e)
        //{
        //    if (chartOrigin.Series.Count < 4)
        //    {
        //        MessageBox.Show("不存在！","提示");
        //    }
        //    else
        //    {
        //        if (series4ToolStripMenuItem.Checked)
        //        {
        //            series4ToolStripMenuItem.Checked = false;
        //            chartOrigin.Series[3].Enabled = false;
        //        }
        //        else
        //        {
        //            series4ToolStripMenuItem.Checked = true;
        //            chartOrigin.Series[3].Enabled = true;
        //        }
        //    }
        //}

        //要解决获取series数量的问题
        //private void seriesToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    if (chartOrigin.Series.Count < 4)
        //    {
        //        MessageBox.Show("不存在！", "提示");
        //    }
        //    else
        //    {
        //        if (series4ToolStripMenuItem.Checked)
        //        {
        //            series4ToolStripMenuItem.Checked = false;
        //            chartOrigin.Series[3].Enabled = false;
        //        }
        //        else
        //        {
        //            series4ToolStripMenuItem.Checked = true;
        //            chartOrigin.Series[3].Enabled = true;
        //        }
        //    }
        //}

        private void contextMenuStrip_Right_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if ((e.ClickedItem.Tag != null) && (e.ClickedItem.Tag.ToString().CompareTo("Serials") == 0))
            {
                chartOrigin.Series[e.ClickedItem.Text].Enabled = !((ToolStripMenuItem)e.ClickedItem).Checked;
            }
        }

        private void chartOrigin_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                for (int i = 0; i < contextMenuStrip_Right.Items.Count; i++)
                {
                    var item = contextMenuStrip_Right.Items[i];
                    if (item.GetType().Name.CompareTo("ToolStripMenuItem") == 0)
                    {
                        ToolStripMenuItem it = item as ToolStripMenuItem;
                        if ((it.Tag != null) && (it.Tag.ToString().CompareTo("Serials") == 0))
                        {
                            contextMenuStrip_Right.Items.RemoveAt(i);
                            i--;
                        }
                    }
                }
                foreach (var serial in chartOrigin.Series)
                {
                    ToolStripMenuItem item = new ToolStripMenuItem(serial.Name);
                    item.Tag = "Serials";
                    item.Checked = serial.Enabled;
                    contextMenuStrip_Right.Items.Add(item);
                }
            }
        }

        
        void chart_GetToolTipText(object sender, ToolTipEventArgs e)
        {
            if (e.HitTestResult.ChartElementType == ChartElementType.DataPoint)
            {
                int i = e.HitTestResult.PointIndex;
                DataPoint dp = e.HitTestResult.Series.Points[i];
                //分别显示x轴和y轴的数值，其中{1:F3},表示显示的是float类型，精确到小数点后3位。  
                e.Text = string.Format("X:{0};Y:{1:F3} ", dp.XValue, dp.YValues[0]);
            }
        }
    }
}
