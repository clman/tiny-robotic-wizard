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
            programTemplatePath = Directory.GetFiles(Path.GetDirectoryName(assembly.Location) + @"\ProgramTemplate", @"ProgramTemplate.xml", SearchOption.AllDirectories);
            ProgramTemplate[] programTemplate = new ProgramTemplate[programTemplatePath.Length];
            for (int i = 0; i <= programTemplatePath.Length - 1; i++)
            {
                programTemplate[i] = new ProgramTemplate(programTemplatePath[i]);
            }
            ProgramData hoge = new ProgramData(programTemplate[0]);
        }
    }
}
