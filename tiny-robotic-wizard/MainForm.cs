using System;
using System.IO;
using System.Reflection;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace tiny_robotic_wizard
{
    public partial class MainForm : Form
    {
        private string[] programTemplatePath;

        public MainForm()
        {
            InitializeComponent();

            // キャプションをアセンブリ名に
            this.Text = Assembly.GetExecutingAssembly().GetName().Name;

            this.AutoScroll = true;

            // ProgramTemplateの一覧を探す
            string programTemplateDirectory = Path.Combine(Application.StartupPath, "ProgramTemplate");
            programTemplatePath = Directory.GetFiles(programTemplateDirectory, "ProgramTemplate.xml", SearchOption.AllDirectories);
            ProgramTemplate[] programTemplate = new ProgramTemplate[programTemplatePath.Length];

            // ProgramTemplateをすべて読み込む
            for (int i = 0; i <= programTemplatePath.Length - 1; i++)
            {
                programTemplate[i] = new ProgramTemplate(programTemplatePath[i]);
            }

            /*
                        ProgramData programData = new ProgramData(programTemplate[3]);

                        ProgramManager programManager = new ProgramManager(Path.Combine(Application.StartupPath, "program"));
                        programManager.Save(programData, "hoge.tpx");

                        programData = programManager.Load("hoge.tpx");

                        ProgramEditor programEditor = new ProgramEditor(programData);
                        this.Controls.Add(programEditor);

                        ProgramGenerator programGenerator = new ProgramGenerator(programData);

                        WinAvrTranslator winAvrTranslator = new WinAvrTranslator();
                        MemoryStream hexStream = new MemoryStream();
                        winAvrTranslator.Translate(programGenerator.ProgramCode, hexStream);
            */
        }
    }
}
