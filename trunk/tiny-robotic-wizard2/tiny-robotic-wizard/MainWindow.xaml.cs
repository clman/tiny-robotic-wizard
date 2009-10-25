using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;

namespace tiny_robotic_wizard
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        // プログラムテンプレート
        private ProgramTemplate[] programTemplates;

        public MainWindow()
        {
            InitializeComponent();

            // ProgramTemplateの一覧を探す
            string programTemplateDirectory = Path.Combine(System.Windows.Forms.Application.StartupPath, "ProgramTemplate");
            string[] programTemplatePath = Directory.GetFiles(programTemplateDirectory, "ProgramTemplate.xml", SearchOption.AllDirectories);
            programTemplates = new ProgramTemplate[programTemplatePath.Length];

            // ProgramTemplateをすべて読み込む
            for (int i = 0; i <= programTemplatePath.Length - 1; i++)
            {
                programTemplates[i] = new ProgramTemplate(programTemplatePath[i]);
            }

            ProgramData programData = new ProgramData(programTemplates[0]);

            this.EditorPanel.Content = new ProgramEditor.ProgramEditor() { ProgramData = programData };
        }
    }
}