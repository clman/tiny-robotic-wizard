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
                Reflesh();
            }
        }
        public void Reflesh()
        {
            if (this.ProgramData == null)
            {
                this.MainPanel.Children.Clear();
                return;
            }
            else
            {
                foreach (Context context in this.ProgramData.Keys)
                {
                }
                this.MainPanel.Children.Add(new ContextAdder(this.programData));
            }
        }
        public ProgramEditor()
        {
            InitializeComponent();
        }
    }
}
