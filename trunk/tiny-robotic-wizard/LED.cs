using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace tiny_robotic_wizard
{
    public partial class LED : UserControl
    {
        public LED()
        {
            InitializeComponent();
            color = Colors.black;
            this.BackgroundImage = LEDColorList.Images[(int)color];
        }

        public enum Colors : int
        {
            black=0,
            blue=1,
            green=2,
            cyan=3,
            red=4,
            magenta=5,
            yellow=6,
            white=7
        }

        private Colors color;

        public Colors Color
        {
            get
            {
                return color;
            }
            set
            {
                color = value;
                this.BackgroundImage = LEDColorList.Images[(int)color];
            }
        }

        private void changeToBlack_Click(object sender, EventArgs e)
        {
            Color = Colors.black;
        }

        private void changeToBlue_Click(object sender, EventArgs e)
        {
            Color = Colors.blue;
        }

        private void changeToGreen_Click(object sender, EventArgs e)
        {
            Color = Colors.green;
        }

        private void changeToCyan_Click(object sender, EventArgs e)
        {
            Color = Colors.cyan;
        }

        private void changeToRed_Click(object sender, EventArgs e)
        {
            Color = Colors.red;
        }

        private void changeToMagenta_Click(object sender, EventArgs e)
        {
            Color = Colors.magenta;
        }

        private void changeToYellow_Click(object sender, EventArgs e)
        {
            Color = Colors.yellow;
        }

        private void changeToWhite_Click(object sender, EventArgs e)
        {
            Color = Colors.white;
        }

        private void LED_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                changeColorMenu.Show(this, new Point(e.X, e.Y));
            }
        }
    }
}
