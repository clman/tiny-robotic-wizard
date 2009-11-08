using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace tiny_robotic_wizard.ProgramEditor
{
    /// <summary>
    /// ProgramEditor.xaml の相互作用ロジック
    /// </summary>
    public partial class ProgramEditor : UserControl
    {
        private ProgramData programData;
        public ProgramData ProgramData
        {
            get { return this.programData; }
            set
            {
                this.programData = value;
                this.programData.ContextAdded += delegate(ProgramData sender, Context context)
                {
                    this.Reflesh();
                };
                this.Reflesh();
            }
        }
        public void Reflesh()
        {
            this.MainPanel.Children.Clear();
            if (this.ProgramData == null)
            {
                return;
            }
            else
            {
                foreach (Context context in this.ProgramData.Keys)
                {
                    this.MainPanel.Children.Add(new ContextAndAction(context, this.ProgramData));
                }
                this.MainPanel.Children.Add(new ContextAdder(this.ProgramData));
            }
        }
        public ProgramEditor()
        {
            InitializeComponent();
        }
    }
}
