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
    /// ContextAndAction.xaml の相互作用ロジック
    /// </summary>
    public partial class ContextAndAction : UserControl
    {
        public Context Context { get; private set; }
        public ProgramData ProgramData { get; private set; }
        private Button deleteButton;
        private ComboBox[] outputOptionSelector;

        public ContextAndAction(Context context, ProgramData programData)
        {
            InitializeComponent();
            this.Context = context;
            this.ProgramData = programData;
            // 削除ボタンの追加
            deleteButton = new Button() { Content = "x" };
            deleteButton.Click += delegate(object sender, RoutedEventArgs e)
            {
                // アニメーションでヌルっと消える．  
                var beMinimum = new System.Windows.Media.Animation.DoubleAnimation
                {
                    From = this.RenderSize.Height,
                    To = 0,  
                    Duration = TimeSpan.FromMilliseconds(200)
                };
                var beTransParent = new System.Windows.Media.Animation.DoubleAnimation
                {
                    From = 1,
                    To = 0,
                    Duration = TimeSpan.FromMilliseconds(200)
                };
                // アニメーション終了時にコントロールとデータを削除する．
                beTransParent.Completed += delegate
                {
                    ((StackPanel)this.Parent).Children.Remove(this);
                    this.ProgramData.Remove(context);
                };
                this.BeginAnimation(StackPanel.HeightProperty, beMinimum);
                this.BeginAnimation(StackPanel.OpacityProperty, beTransParent);
            };
            this.MainPanel.Children.Add(deleteButton);
            // Inputパネルの追加
            foreach (Input input in this.Context)
            {
                StackPanel inputView = new StackPanel();
                int deviceIndex = 0;
                foreach (int? optionIndex in input.ToArray())
                {
                    if (optionIndex != null)
                    {
                        inputView.Children.Add(new Label() { Content = this.ProgramData.ProgramTemplate.Input.Device[deviceIndex].Option[(int)optionIndex].Caption });
                    }
                    else
                    {
                        inputView.Children.Add(new Label() { Content = "*" });
                    }
                    deviceIndex++;
                }
                this.MainPanel.Children.Add(inputView);
                // 定義記号パネルの追加
                this.MainPanel.Children.Add(new Label() { Content = ">" });
            }
            // Outputパネルの追加
            {
                StackPanel outputView = new StackPanel();
                // セレクトボックスの生成
                outputOptionSelector = new ComboBox[this.ProgramData.ProgramTemplate.Output.Device.Length];
                for (int i = 0; i < this.ProgramData.ProgramTemplate.Output.Device.Length; i++)
                {
                    outputOptionSelector[i] = new ComboBox();
                    outputOptionSelector[i].Items.Add("*");
                    foreach (Option option in this.ProgramData.ProgramTemplate.Output.Device[i].Option)
                    {
                        outputOptionSelector[i].Items.Add(option.Caption);
                    }
                    if (this.ProgramData[context][i] == null)
                    {
                        outputOptionSelector[i].SelectedIndex = 0;
                    }
                    else
                    {
                        outputOptionSelector[i].SelectedIndex = (int)this.ProgramData[context][i] + 1;
                    }
                    outputView.Children.Add(outputOptionSelector[i]);
                    outputOptionSelector[i].SelectionChanged += delegate(object sender, SelectionChangedEventArgs e)
                    {
                        Output output = new Output();
                        foreach (ComboBox os in this.outputOptionSelector)
                        {
                            if (os.SelectedIndex == 0)
                            {
                                output.Add(null);
                            }
                            else
                            {
                                output.Add(os.SelectedIndex - 1);
                            }
                        }
                        this.ProgramData[this.Context] = output;
                    };
                }
                this.MainPanel.Children.Add(outputView);
            }
        }
    }
}
