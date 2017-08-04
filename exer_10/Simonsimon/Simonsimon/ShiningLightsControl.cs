using System;
using System.Collections.Generic;
using System.Media;
using System.Threading;
using System.Windows.Forms;
using Simonsimon.Properties;

namespace Simonsimon
{
    public delegate void fixedDelegate();
    public delegate void numDelegate(int i);
    public partial class ShiningLightsControl : UserControl
    {
        public fixedDelegate watchThread;
        public fixedDelegate clickThread;
        public fixedDelegate nullThread;

        public fixedDelegate enableThread;
        public numDelegate numThread;

        //以下实现计时器
        /// <summary>
        /// 实现计时器
        /// </summary>
        #region count_down
        System.Windows.Forms.Timer timer1 = new System.Windows.Forms.Timer();

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




        #endregion

        public ShiningLightsControl()
        {
            InitializeComponent();
            timer1.Interval = 1000;
            timer1.Tick += timer_count_Tick;
        }

        static List<int> pattern = new List<int>();
        Random rand = new Random();
        //按顺序按下，此时的序号
        private int onList = 0;
        //用来显示当前闪烁次数
        private int onList_tmp = 0;
        //控制是否要继续闪烁
        private bool playing = true;

        private int score = 0;

        //闪灯之间的时间间隔
        private int level = 1000;
        //private int level2_interval = 500;
        //private int level3_interval = 300;

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            TestCorrect(0);
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            TestCorrect(1);
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            TestCorrect(2);
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            TestCorrect(3);
        }

        public int Level
        {
            get { return this.level; }
            set { this.level = value; }

        }

        //TestCorrect用来校验点击顺序是否正确
        private void TestCorrect(int color)
        {
            if (playing)
                return;
            //此时闪烁的值和按钮按下的值相同
            if (pattern[onList] == color)
            {
                ++score;
                labelScore.Text = "Score:" + score;
                //看下一个值
                //onList++;
                onList_tmp = onList++;
                if (onList >= pattern.Count)
                {
                    pattern.Add(rand.Next(0, 4));
                    //开始下一轮，将比较值置为第一个
                    onList = 0;
                    onList_tmp = 0;
                    new Thread(autoPlay).Start();
                }
            }
            //如果不相同，说明摁错了
            else
            {
                //做判断，是否破排行榜记录或者自己的记录了
                int score_tmp;
                //查找操作，以简单等级为例，后面还需要修改
                score_tmp = Register_Login_MainForm.usersInfo[Register_Login_MainForm._loginUser].Level1Score;
                if (score > score_tmp)
                {
                    //破自己记录了
                    Register_Login_MainForm.usersInfo[Register_Login_MainForm._loginUser].Level1Score = score;
                    MessageBox.Show("Game's over but you've broken your own record!");
                }
                else
                {
                    MessageBox.Show("You Failed!");
                }
                
                score = 0;
                labelScore.Text = "Score:" + score;
                nullThread();
                pattern.Clear();
                pattern.Add(rand.Next(0, 4));
                //MessageBox.Show("You Fail!The score is" + pattern.Count.ToString());
            }
        }

        private void UserControl1_Load(object sender, EventArgs e)
        {
            pattern.Add(rand.Next(0, 4));
        }

        public void autoPlay()
        {
            //系统输入时点击事件失效
            //注意跨线程访问UI
            enableThread();
            //pictureBox1.Enabled = false;
            //pictureBox2.Enabled = false;
            //pictureBox3.Enabled = false;
            //pictureBox4.Enabled = false;

            playing = true;
            //这里要注意跨线程访问UI
            //label_watch.Text = "Watch!";
            watchThread();
            Thread.Sleep(2000);
            //准备开始show
            foreach (int color in pattern)
            {
                switch (color)
                {
                    case 0:
                        pictureBox1.Image = Resources.redbright;
                        SoundPlayer player = new SoundPlayer(@"C:\Users\M0015\Desktop\pic\sound\1 C.wav");
                        player.Play();
                        //这里要注意跨线程访问UI
                        refreshLabelWatch_3(++onList_tmp);
                        Thread.Sleep(100);
                        pictureBox1.Image = Resources.red;
                        break;
                    case 1:
                        pictureBox2.Image = Resources.bluebright;
                        player = new SoundPlayer(@"C:\Users\M0015\Desktop\pic\sound\3 E.wav");
                        player.Play();
                        //这里要注意跨线程访问UI
                        refreshLabelWatch_3(++onList_tmp);
                        Thread.Sleep(100);
                        pictureBox2.Image = Resources.blue;
                        break;
                    case 2:
                        pictureBox3.Image = Resources.darkbluebright;
                        player = new SoundPlayer(@"C:\Users\M0015\Desktop\pic\sound\5 G.wav");
                        player.Play();
                        //这里要注意跨线程访问UI
                        refreshLabelWatch_3(++onList_tmp);
                        Thread.Sleep(100);
                        pictureBox3.Image = Resources.darkblue;
                        break;
                    case 3:
                        pictureBox4.Image = Resources.greenbright;
                        player = new SoundPlayer(@"C:\Users\M0015\Desktop\pic\sound\7 B.wav");
                        player.Play();
                        //这里要注意跨线程访问UI
                        refreshLabelWatch_3(++onList_tmp);
                        Thread.Sleep(100);
                        pictureBox4.Image = Resources.green;
                        break;
                }
                Thread.Sleep(level);
            }
            //这里要注意跨线程访问UI
            clickThread();
            //用户输入时可用
            //注意跨线程访问

            enableThread();
            //开始倒计时
            timer1.Enabled = true;
            //countdown1.Enabled = true;
            playing = false;
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            pictureBox1.Image = Resources.redbright;
            SoundPlayer player = new SoundPlayer(@"C:\Users\M0015\Desktop\pic\sound\1 C.wav");
            player.Play();
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            pictureBox1.Image = Resources.red;
        }

        private void pictureBox2_MouseDown(object sender, MouseEventArgs e)
        {
            pictureBox2.Image = Resources.bluebright;
            SoundPlayer player = new SoundPlayer(@"C:\Users\M0015\Desktop\pic\sound\3 E.wav");
            player.Play();
        }

        private void pictureBox2_MouseUp(object sender, MouseEventArgs e)
        {
            pictureBox2.Image = Resources.blue;
        }

        private void pictureBox3_MouseDown(object sender, MouseEventArgs e)
        {
            pictureBox3.Image = Resources.darkbluebright;
            SoundPlayer player = new SoundPlayer(@"C:\Users\M0015\Desktop\pic\sound\5 G.wav");
            player.Play();
        }

        private void pictureBox3_MouseUp(object sender, MouseEventArgs e)
        {
            pictureBox3.Image = Resources.darkblue;
        }

        private void pictureBox4_MouseDown(object sender, MouseEventArgs e)
        {
            pictureBox4.Image = Resources.greenbright;
            SoundPlayer player = new SoundPlayer(@"C:\Users\M0015\Desktop\pic\sound\7 B.wav");
            player.Play();
        }

        private void pictureBox4_MouseUp(object sender, MouseEventArgs e)
        {
            pictureBox4.Image = Resources.green;
        }

        /// <summary>
        /// 更新label，显示Watch
        /// </summary>
        public void refreshLabelWatch_1()
        {
            //InvokeRequired判断该函数现在是否是主线程在调用，如果是其他线程则返回true
            if (label_watch.InvokeRequired)
            {
                //如若label不能操作了，则返回，防止异常
                if (label_watch.Disposing || label_watch.IsDisposed)
                {
                    return;
                }
                //把自己绑定
                watchThread = refreshLabelWatch_1;

                Invoke(this.watchThread, new object[]{});
            }
            else
            {
                label_watch.Text = "Watch!";
            }
        }

        /// <summary>
        /// 更新label显示Click
        /// </summary>
        public void refreshLabelWatch_2()
        {
            //InvokeRequired判断该函数现在是否是主线程在调用，如果是其他线程则返回true
            if (label_watch.InvokeRequired)
            {
                //如若label不能操作了，则返回，防止异常
                if (label_watch.Disposing || label_watch.IsDisposed)
                {
                    return;
                }
                //把自己绑定
                clickThread = refreshLabelWatch_2;

                Invoke(this.clickThread, new object[] { });
            }
            else
            {
                label_watch.Text = "Click!";
            }
        }

        public void refreshLabelWatch_null()
        {
            //InvokeRequired判断该函数现在是否是主线程在调用，如果是其他线程则返回true
            if (label_watch.InvokeRequired)
            {
                //如若label不能操作了，则返回，防止异常
                if (label_watch.Disposing || label_watch.IsDisposed)
                {
                    return;
                }
                //把自己绑定
                nullThread = refreshLabelWatch_null;

                Invoke(this.nullThread, new object[] { });
            }
            else
            {
                label_watch.Text = "";
            }
        }

        /// <summary>
        /// 更新label显示当前闪烁次数
        /// </summary>
        /// <param name="i">i为当前闪烁次数，传onList进来</param>
        public void refreshLabelWatch_3(int i)
        {
            //InvokeRequired判断该函数现在是否是主线程在调用，如果是其他线程则返回true
            if (label_watch.InvokeRequired)
            {
                //如若label不能操作了，则返回，防止异常
                if (label_watch.Disposing || label_watch.IsDisposed)
                {
                    return;
                }
                //把自己绑定
                numThread = refreshLabelWatch_3;

                Invoke(this.numThread, new object[] { i });
            }
            else
            {
                label_watch.Text = i.ToString();
            }
        }

        public void picEnable()
        {
            //InvokeRequired判断该函数现在是否是主线程在调用，如果是其他线程则返回true
            if (pictureBox1.InvokeRequired|| pictureBox2.InvokeRequired|| pictureBox3.InvokeRequired|| pictureBox4.InvokeRequired)
            {
                //如若label不能操作了，则返回，防止异常
                if ((pictureBox1.Disposing || pictureBox1.IsDisposed) || (pictureBox2.Disposing || pictureBox2.IsDisposed) || (pictureBox3.Disposing || pictureBox3.IsDisposed) || (pictureBox4.Disposing || pictureBox4.IsDisposed))
                {
                    return;
                }
                //把自己绑定
                enableThread = picEnable;

                Invoke(this.enableThread, new object[] { });
            }
            else
            {
                pictureBox1.Enabled = !pictureBox1.Enabled;
                pictureBox2.Enabled = !pictureBox2.Enabled;
                pictureBox3.Enabled = !pictureBox3.Enabled;
                pictureBox4.Enabled = !pictureBox4.Enabled;
            }
        }

        private void timer_count_Tick(object sender, EventArgs e)
        {

            System.Media.SoundPlayer player_1 
                         = new System.Media.SoundPlayer(@"C:\Users\M0015\Desktop\pic\sound\1792.wav");
            System.Media.SoundPlayer player_2
                         = new System.Media.SoundPlayer(@"C:\Users\M0015\Desktop\pic\sound\786.wav");
            String str = ts_count_tmp.Hours + ":" + ts_count_tmp.Minutes + ":" + ts_count_tmp.Seconds;

            label1.Text = str;//label1用来显示剩余的时间

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

        #region control with the keyboard 

        protected override bool ProcessDialogKey(Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Up: KeyBoardClick(0); break;
                case Keys.Right: KeyBoardClick(1); break;
                case Keys.Down: KeyBoardClick(2); break;
                case Keys.Left: KeyBoardClick(3); break;
            }

            return true; //返回 true 以指示已处理该键。
            //return base.ProcessDialogKey(keyData);
        }

        void KeyBoardClick(int i)
        {
            switch (i)
            {
                case 0: pictureBox1_Click(pictureBox1, null); break;
                case 1: pictureBox2_Click(pictureBox2, null); break;
                case 2: pictureBox3_Click(pictureBox3, null); break;
                case 3: pictureBox4_Click(pictureBox4, null); break;
                default: break;
            }
        }
        #endregion
    }

}


