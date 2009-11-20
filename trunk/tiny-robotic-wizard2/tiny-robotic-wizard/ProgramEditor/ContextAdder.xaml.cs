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
                        // アニメーションでヌルっと消える．  
                        var beMinimum = new System.Windows.Media.Animation.DoubleAnimation()
                        {
                            From = this.EditPanel.RenderSize.Height,
                            To = 0,
                            Duration = TimeSpan.FromMilliseconds(200)
                        };
                        // アニメーション終了してから操作する．
                        beMinimum.Completed += delegate
                        {
                            this.EditPanel.Visibility = Visibility.Collapsed;
                            this.AddContextButton.Visibility = Visibility.Visible;
                            this.FinishEditingButton.Visibility = Visibility.Collapsed;
                        };
                        this.EditPanel.BeginAnimation(StackPanel.HeightProperty, beMinimum);
                        break;
                    case ContextAdderState.Editting:
                        this.EditPanel.Visibility = Visibility.Visible;
                        var beMaximum = new System.Windows.Media.Animation.DoubleAnimation()
                        {
                            From = 0,
                            To = 50,
                            Duration = TimeSpan.FromMilliseconds(200)
                        };
                        beMaximum.Completed += delegate
                        {
                        this.AddContextButton.Visibility = Visibility.Collapsed;
                        this.FinishEditingButton.Visibility = Visibility.Visible;
                        };
                        this.EditPanel.BeginAnimation(StackPanel.HeightProperty, beMaximum);
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
            // 表示を初期化
            InitializeEditView();
            // コントロールの状態の初期化
            this.State = ContextAdderState.Addable;
        }
        private void InitializeEditView()
        {
            // キャンセルボタンの追加
            canselButton = new Button() { Content = "x" };
            canselButton.Click += delegate
            {
                this.State = ContextAdderState.Addable;
            };
            this.EditPanel.Children.Add(canselButton);
            // Inputパネルの追加
            {
                this.inputOptionSelector = new ComboBox[this.ProgramData.NestLevel, this.ProgramData.ProgramTemplate.Input.Device.Length];
                // ネストの数だけInputViewを作る．
                StackPanel[] inputView = new StackPanel[this.ProgramData.NestLevel];
                for (int nestIndex = 0; nestIndex < this.ProgramData.NestLevel; nestIndex++)
                {
                    inputView[nestIndex] = new StackPanel();
                    for (int deviceIndex = 0; deviceIndex < this.ProgramData.ProgramTemplate.Input.Device.Length; deviceIndex++)
                    {
                        this.inputOptionSelector[nestIndex, deviceIndex] = new ComboBox();
                        this.inputOptionSelector[nestIndex, deviceIndex].Items.Add("*");
                        foreach (Option option in this.ProgramData.ProgramTemplate.Input.Device[deviceIndex].Option)
                        {
                            this.inputOptionSelector[nestIndex, deviceIndex].Items.Add(option.Caption);
                        }
                        inputView[nestIndex].Children.Add(this.inputOptionSelector[nestIndex, deviceIndex]);
                    }
                    this.EditPanel.Children.Add(inputView[nestIndex]);
                    // 定義記号を挿入
                    this.EditPanel.Children.Add(new Label() { Content = ">" });
                }
            }
            // Outputパネルの追加
            {
                StackPanel outputView = new StackPanel();
                // セレクトボックスの生成
                this.outputOptionSelector = new ComboBox[this.ProgramData.ProgramTemplate.Output.Device.Length];
                for (int deviceIndex = 0; deviceIndex < this.ProgramData.ProgramTemplate.Output.Device.Length; deviceIndex++)
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
            // 選択項目の初期化
            foreach (ComboBox inputOptionSelector in this.inputOptionSelector)
            {
                inputOptionSelector.SelectedIndex = 0;
            }
            foreach (ComboBox outputOptionSelector in this.outputOptionSelector)
            {
                outputOptionSelector.SelectedIndex = 0;
            }
        }

        // Contextの編集開始ボタンが押された．
        private void AddContextButton_Click(object sender, RoutedEventArgs e)
        {
            // 選択項目の初期化
            foreach (ComboBox inputOptionSelector in this.inputOptionSelector)
            {
                inputOptionSelector.SelectedIndex = 0;
            }
            foreach (ComboBox outputOptionSelector in this.outputOptionSelector)
            {
                outputOptionSelector.SelectedIndex = 0;
            }
            // コントロールの状態を更新
            this.State = ContextAdderState.Editting;
        }

        // 編集確定ボタンが押された(新しいContextが追加された)
        private void FinishEditingButton_Click(object sender, RoutedEventArgs e)
        {
            this.State = ContextAdderState.Addable;
            // セレクトボックスの選択値からContextとOutputのペアを生成．
            // Contextを生成
            Input[] input = new Input[this.ProgramData.NestLevel];
            Context context = new Context();
            context.Clear();
            for (int nestIndex = 0; nestIndex < this.ProgramData.NestLevel; nestIndex++)
            {
                input[nestIndex] = new Input();
                input[nestIndex].Clear();
                for (int deviceIndex = 0; deviceIndex < this.ProgramData.ProgramTemplate.Input.Device.Length; deviceIndex++)
                {
                    input[nestIndex].Add(this.selectedIndexToOptionValue(this.inputOptionSelector[nestIndex, deviceIndex].SelectedIndex));
                }
                context.Add(input[nestIndex]);
            }
            // Output
            Output output = new Output();
            output.Clear();
            for (int deviceIndex = 0; deviceIndex < this.ProgramData.ProgramTemplate.Output.Device.Length; deviceIndex++)
            {
                output.Add(this.selectedIndexToOptionValue(this.outputOptionSelector[deviceIndex].SelectedIndex));
            }
            // 生成したペアをプログラムデータに追加
            this.ProgramData[context] = output;
            this.State = ContextAdderState.Addable;
        }
        private int? selectedIndexToOptionValue(int selectedIndex)
        {
            if (selectedIndex == 0)
            {
                return null;
            }
            else
            {
                return selectedIndex - 1;
            }
        }
    }
    /// <summary>
    /// このコントロールの状態
    /// </summary>
    public enum ContextAdderState
    {
        Addable, Editting
    }
}
