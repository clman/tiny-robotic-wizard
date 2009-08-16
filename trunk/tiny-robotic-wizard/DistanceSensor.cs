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
    /// 測距センサの状態を表示するコントロール
    /// </summary>
    public partial class DistanceSensor : UserControl
    {
        public DistanceSensor()
        {
            InitializeComponent();
        }
        public int Distance
        {
            get
            {
                return DistanceLevel.Value;
            }
            set
            {
                if(value < 0)
                {
                    throw new ArgumentOutOfRangeException();
                }
                switch (Resolution)
                {
                    case ResolutionList.notUse:
                        throw new ArgumentOutOfRangeException();
                    case ResolutionList.two:
                        if (1 < value)
                        {
                            throw new ArgumentOutOfRangeException();
                        }
                        break;
                    case ResolutionList.three:
                        if (2 < value)
                        {
                            throw new ArgumentOutOfRangeException();
                        }
                        break;
                }
                DistanceLevel.Value = value;
            }
        }
        public ResolutionList Resolution
        {
            get
            {
                switch(DistanceLevel.Maximum)
                {
                    case 0:
                        return ResolutionList.notUse;
                    case 1:
                        return ResolutionList.two;
                    case 2:
                        return ResolutionList.three;
                    default:
                        throw new Exception();
                }
            }
            set
            {
                switch (value)
                {
                    case ResolutionList.notUse:
                        DistanceLevel.Maximum = 0;
                        break;
                    case ResolutionList.two:
                        DistanceLevel.Maximum = 1;
                        break;
                    case ResolutionList.three:
                        DistanceLevel.Maximum = 2;
                        break;
                }
            }
        }
    }
    /// <summary>
    /// 測距センサの分解能
    /// </summary>
    public enum ResolutionList : int
    {
        notUse = 0, 
        two = 1,
        three = 2
    }
}
