using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace tiny_robotic_wizard
{
    /// <summary>
    /// ラインセンサの状態を表示するコントロール
    /// </summary>
    public partial class LineSensor : UserControl
    {
        public LineSensor()
        {
            InitializeComponent();
            this.BackgroundImage = lineSensorStatusList.Images[0];
        }
        private bool rightLineSensor = false;
        public bool RightLineSensor
        {
            get
            {
                return rightLineSensor;
            }
            set
            {
                rightLineSensor = value;
                statusNumber = (leftLineSensor ? 2 : 0) + (rightLineSensor ? 1 : 0);
                this.BackgroundImage = lineSensorStatusList.Images[statusNumber];
            }
        }
        private bool leftLineSensor = false;
        public bool LeftLineSensor
        {
            get
            {
                return leftLineSensor;
            }
            set
            {
                leftLineSensor = value;
                statusNumber = (leftLineSensor ? 2 : 0) + (rightLineSensor ? 1 : 0);
                this.BackgroundImage = lineSensorStatusList.Images[statusNumber];
            }
        }
        private int statusNumber = 0;
        public int StatusNumber
        {
            get
            {
                return statusNumber;
            }
            set
            {
                if (value < 0 || 3 < value)
                {
                    throw new ArgumentOutOfRangeException();
                }
                else
                {
                    statusNumber = value;
                    this.BackgroundImage = lineSensorStatusList.Images[statusNumber];
                    RightLineSensor = (((byte)statusNumber & (1 << 0)) != 0);
                    LeftLineSensor = (((byte)statusNumber & (1 << 1)) != 0);
                }
            }
        }
    }
}
