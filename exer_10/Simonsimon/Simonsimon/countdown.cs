using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Simonsimon
{
    public partial class countdown : UserControl
    {
        Timer timer1 = new Timer();
        //倒计时时长，默认为8s
        static TimeSpan ts_count = new TimeSpan(0, 0, 8);


        TimeSpan ts_count_tmp;
        //默认从第5秒开始发出嘀嗒声
        TimeSpan ts_cut = new TimeSpan(0, 0, 3);


        public TimeSpan Ts_count
        {
            set
            {
                ts_count = value;
                ts_count_tmp = ts_count;
            }
        }

        public TimeSpan Ts_cut
        {
            set { ts_cut = value; }
        }

        //设置时间控件的可用与否
        public bool TimerEnable
        {
            set { timer1.Enabled = value; }
        }

        public countdown()
        {
            InitializeComponent();
            //timer1.Enabled = true;
            timer1.Interval = 1000;
            timer1.Tick += timer_count_Tick;
        }

        private void timer_count_Tick(object sender, EventArgs e)
        {
         
            SoundPlayer player_1 = new SoundPlayer(@"C:\Users\M0015\Desktop\pic\sound\1792.wav");
            SoundPlayer player_2 = new SoundPlayer(@"C:\Users\M0015\Desktop\pic\sound\786.wav");
            String str = ts_count_tmp.Hours + ":" + ts_count_tmp.Minutes + ":" + ts_count_tmp.Seconds;

            label1.Text = str;//label17用来显示剩余的时间

            ts_count_tmp = ts_count_tmp.Subtract(new TimeSpan(0, 0, 1));//每隔一秒减去一秒

            if (ts_count_tmp.TotalSeconds < 3.0) //当倒计时剩余3s时
            {
                player_1.Play();
            }

                if (ts_count_tmp.TotalSeconds < 0.0)//当倒计时完毕
            {

                timer1.Enabled = false;
                player_1.Stop();
                player_2.Play();
                MessageBox.Show("时间到!");//提示时间到
                
            }

        }

        private void timer2_Tick(object sender, EventArgs e)
        {

        }

        private void button_count_Click(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            //ts_count_tmp = ts_count;
        }
    }
}
