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
            Assembly assembly = Assembly.GetExecutingAssembly();
            programTemplatePath = Directory.GetFiles(Application.StartupPath + @"\ProgramTemplate", @"ProgramTemplate.xml", SearchOption.AllDirectories);
            ProgramTemplate[] programTemplate = new ProgramTemplate[programTemplatePath.Length];
            for (int i = 0; i <= programTemplatePath.Length - 1; i++)
            {
                programTemplate[i] = new ProgramTemplate(programTemplatePath[i]);
            }

            this.AutoScroll = true;

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
        }
    }
}
