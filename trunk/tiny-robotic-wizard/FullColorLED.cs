using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace tiny_robotic_wizard
{
    public partial class FullColorLED : UserControl
    {
        public FullColorLED()
        {
            InitializeComponent();
            color = Colors.black;
            this.BackgroundImage = FullColorLEDList.Images[(int)color];
        }
        public enum Colors:int
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
                this.BackgroundImage = FullColorLEDList.Images[(int)color];
            }
        }
    }
}
