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
    /// ContextAdder.xaml の相互作用ロジック
    /// </summary>
    public partial class ContextAdder : UserControl
    {
        private ContextAdderState state;
        public ContextAdderState State
        {
            get { return this.state; }
            set
            {
                this.state = value;
                switch (this.state)
                {
                    case ContextAdderState.Addable:
                        this.EditPanel.Visibility = Visibility.Collapsed;
                        this.AddContextButton.Visibility = Visibility.Visible;
                        this.FinishEditingButton.Visibility = Visibility.Collapsed;
                        break;
                    case ContextAdderState.Editting:
                        this.EditPanel.Visibility = Visibility.Visible;
                        this.AddContextButton.Visibility = Visibility.Collapsed;
                        this.FinishEditingButton.Visibility = Visibility.Visible;
                        break;
                }
            }
        }
        public ProgramData ProgramData { get; private set; }
        private Button canselButton;
        private ComboBox[] outputOptionSelector;
        private ComboBox[,] inputOptionSelector;
        public ContextAdder(ProgramData programData)
        {
            InitializeComponent();
            this.ProgramData = programData;
            InitializeEditView();
            this.State = ContextAdderState.Addable;
        }
        private void InitializeEditView()
        {
            // 削除パネルの追加
            canselButton = new Button() { Content = "x" };
            canselButton.Click += delegate(object sender, RoutedEventArgs e)
            {
                this.State = ContextAdderState.Addable;
            };
            // Inputパネルの追加
            {
                this.inputOptionSelector = new ComboBox[this.ProgramData.NestLevel, this.ProgramData.ProgramTemplate.Input.Device.Length];
                // ネストの数だけInputViewを作る．
                StackPanel[] inputView = new StackPanel[this.ProgramData.NestLevel];
                for (int inputIndex = 0; inputIndex <= this.ProgramData.NestLevel - 1; inputIndex++)
                {
                    inputView[inputIndex] = new StackPanel();
                    for (int deviceIndex = 0; deviceIndex <= this.ProgramData.ProgramTemplate.Input.Device.Length - 1; deviceIndex++)
                    {
                        this.inputOptionSelector[inputIndex, deviceIndex] = new ComboBox();
                        this.inputOptionSelector[inputIndex, deviceIndex].Items.Add("*");
                        foreach (Option option in this.ProgramData.ProgramTemplate.Input.Device[deviceIndex].Option)
                        {
                            this.inputOptionSelector[inputIndex, deviceIndex].Items.Add(option.Caption);
                        }
                        inputView[inputIndex].Children.Add(this.inputOptionSelector[inputIndex, deviceIndex]);
                    }
                    this.EditPanel.Children.Add(inputView[inputIndex]);
                    // 定義記号を挿入
                    this.EditPanel.Children.Add(new Label() { Content = ">" });
                }
            }
            // Outputパネルの追加
            {
                StackPanel outputView = new StackPanel();
                // セレクトボックスの生成
                this.outputOptionSelector = new ComboBox[this.ProgramData.ProgramTemplate.Output.Device.Length];
                for (int deviceIndex = 0; deviceIndex <= this.ProgramData.ProgramTemplate.Output.Device.Length - 1; deviceIndex++)
                {
                    this.outputOptionSelector[deviceIndex] = new ComboBox();
                    this.outputOptionSelector[deviceIndex].Items.Add("*");
                    foreach (Option option in this.ProgramData.ProgramTemplate.Output.Device[deviceIndex].Option)
                    {
                        this.outputOptionSelector[deviceIndex].Items.Add(option.Caption);
                    }
                    outputView.Children.Add(this.outputOptionSelector[deviceIndex]);
                }
                this.EditPanel.Children.Add(outputView);
            }
        }

        private void AddContextButton_Click(object sender, RoutedEventArgs e)
        {
            this.State = ContextAdderState.Editting;
        }

        private void FinishEditingButton_Click(object sender, RoutedEventArgs e)
        {
            this.State = ContextAdderState.Addable;
        }
    }
    public enum ContextAdderState
    {
        Addable, Editting
    }
}
