using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace tiny_robotic_wizard
{
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
                    case ResolutionLevel.notUse:
                        throw new ArgumentOutOfRangeException();
                    case ResolutionLevel.two:
                        if (1 < value)
                        {
                            throw new ArgumentOutOfRangeException();
                        }
                        break;
                    case ResolutionLevel.three:
                        if (2 < value)
                        {
                            throw new ArgumentOutOfRangeException();
                        }
                        break;
                }
                DistanceLevel.Value = value;
            }
        }
        public enum ResolutionLevel
        {
            notUse, two, three
        }
        public ResolutionLevel Resolution
        {
            get
            {
                switch(DistanceLevel.Maximum)
                {
                    case 0:
                        return ResolutionLevel.notUse;
                    case 1:
                        return ResolutionLevel.two;
                    case 2:
                        return ResolutionLevel.three;
                    default:
                        throw new Exception();
                }
            }
            set
            {
                switch (value)
                {
                    case ResolutionLevel.notUse:
                        DistanceLevel.Maximum = 0;
                        break;
                    case ResolutionLevel.two:
                        DistanceLevel.Maximum = 1;
                        break;
                    case ResolutionLevel.three:
                        DistanceLevel.Maximum = 2;
                        break;
                }
            }
        }
    }
}
