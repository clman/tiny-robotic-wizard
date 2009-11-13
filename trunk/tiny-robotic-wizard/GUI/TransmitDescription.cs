using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace tiny_robotic_wizard.GUI
{
    public partial class TransmitDescription : UserControl
    {
        private DescriptionMode mode;
        public DescriptionMode Mode
        {
            get
            {
                return this.mode;
            }
            set
            {
                this.mode = value;
                this.ready.Visible = false;
                this.successed.Visible = false;
                switch (this.mode)
                {
                    case DescriptionMode.Ready:
                        this.ready.Visible = true;
                        break;
                    case DescriptionMode.Successed:
                        this.successed.Visible = true;
                        break;
                }
            }
        }
        public TransmitDescription()
        {
            InitializeComponent();
        }
        public enum DescriptionMode
        {
            Ready, Successed
        }
    }
}
