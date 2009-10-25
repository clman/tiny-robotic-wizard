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
                ReDraw();
            }
        }
        public void ReDraw()
        {
            if (this.ProgramData == null)
            {
                this.MainPanel.Children.Clear();
                return;
            }
            foreach (KeyValuePair<Context, Output> ContextAndAction in this.ProgramData)
            {
                StackPanel bar = new StackPanel() { Orientation = Orientation.Horizontal };
                foreach (Input input in ContextAndAction.Key)
                {
                    StackPanel inputView = new StackPanel();
                    int deviceIndex = 0;
                    foreach (int? optionValue in input.ToArray())
                    {
                        if (optionValue != null)
                        {
                            inputView.Children.Add(new Label() { Content = this.ProgramData.ProgramTemplate.Input.Device[deviceIndex].Caption[(int)optionValue] });
                        }
                        else
                        {
                            inputView.Children.Add(new Label() { Content = "*" });
                        }
                        deviceIndex++;
                    }
                    bar.Children.Add(inputView);
                }
                this.MainPanel.Children.Add(bar);
            }
        }
        public ProgramEditor()
        {
            InitializeComponent();
        }
    }
}
