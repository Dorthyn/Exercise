using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Simonsimon
{
    public partial class HelpForm : Form
    {
        public HelpForm()
        {
            InitializeComponent();
            textBox_help.Text
                = @"Play this free simon game and have fun testing your memory. Follow the pattern of lights and sounds and repeat the same combination to move on to the next round.It starts off easy but it won’t be long before you’re questioning your memory and struggling to remember the pattern. Rack up high scores, improve your memory and enjoy this classic game as well as all our other great memory games online.";
        }
    }
}
