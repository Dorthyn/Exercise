using System;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace WaveControl
{
    public partial class Form1 : Form
    {
        private double[] y1 = new double[120];
        private double[] y2 = new double[120];
        public Form1()
        {
            InitializeComponent();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            double f = 0.01;
            for (int i = 0; i < 120; i++)
            {
                y1[i] = Math.Sin(Math.PI * 2 * f * i);

            }

            userWaveControl1.Plot(y1);
            //userWaveControl1.LineType = SeriesChartType.FastLine;
            ////userWaveControl1.SeriesColor1 = Color.Black;
            //userWaveControl1.GridChartColor = TransparencyKey;
            ////userWaveControl1.AxisReset();

            button1.Enabled = false;
            button2.Enabled = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            double f = 0.01;
            double[,] y = new double[5,100];
            for (int i = 0; i < 100; i++)
            {
                y[0,i] =100 * Math.Sin(Math.PI * 2 * f * i);
            }

            for (int j = 0; j < 100; j++)
            {
                y[1, j] =100 * Math.Cos(Math.PI * 2 * f * j);
            }

            for (int j = 0; j < 100; j++)
            {
                y[2, j] =Math.Cosh(Math.PI * 2 * f * j);
            }

            for (int j = 0; j < 100; j++)
            {
                y[3, j] = Math.Cosh(Math.PI * 2 * f * j);
            }

            for (int j = 0; j < 100; j++)
            {
                y[4, j] = Math.Cosh(Math.PI * 2 * f * j);
            }
            
            userWaveControl1.Plot(y);
            userWaveControl1.SeriesColor[0].Color = Color.BlueViolet;
            button1.Enabled = false;
            button2.Enabled = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            double f = 0.01;
            for (int i = 0; i < 120; i++)
            {
                y1[i] =Math.Sin(Math.PI * 2 * f * i);

            }
            for (int i = 0; i < 120; i++)
            {
                y2[i] =Math.Cos(Math.PI * 2 * f * i);

            }
            userWaveControl1.LineType = SeriesChartType.FastLine;
            userWaveControl1.Plot(y1,y2);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            double[,] x = new double[3, 10];
            double[,] y = new double[4, 10];

            for (int i = 0; i < 10; i++)
            {
                x[0, i] = i;
            }

            for (int j = 0; j < 10; j++)
            {
                x[1, j] = j;
            }

            for (int j = 0; j < 10; j++)
            {
                x[2, j] = j;
            }

            //设置的二维数组
            for (int i = 0; i < 10; i++)
            {
                y[0, i] = i;
            }

            for (int j = 0; j < 10; j++)
            {
                y[1, j] = j;
            }

            for (int j = 0; j < 10; j++)
            {
                y[2, j] = j;
            }

            for (int j = 0; j < 10; j++)
            {
                y[3, j] = j;
            }
            //userWaveControl1.Chart_Back_Color = Color.Black;
            userWaveControl1.ChartArea_back_Color = Color.Aquamarine;
            //userWaveControl1.ChartArea3DEnable = true;
            userWaveControl1.Plot(x,y);
            //可以用数组取代原来的属性，把整个series的接口暴露出来
            userWaveControl1.SeriesColor[0].Color = Color.BlueViolet;
            userWaveControl1.SeriesColor[0].ChartType = SeriesChartType.FastLine;
            //userWaveControl1.SeriesColor[0].IsValueShownAsLabel = true;
        }
    }
}
