using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Simonsimon
{
    public partial class CountDownSpan : UserControl
    {
        //倒计时时长，默认为8s
        static TimeSpan ts_count = new TimeSpan(0, 0, 8);

        public CountDownSpan()
        {
            InitializeComponent();

        }
    }

}
