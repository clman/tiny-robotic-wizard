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
        // プログラムテンプレート
        private ProgramTemplate[] programTemplate;

        // 編集されたかどうかのフラグ
        private bool isModified = !false;

        // 基本フォント
        private readonly Font baseFont = new Font(FontFamily.GenericSansSerif, 18);

        public MainForm()
        {
            InitializeComponent();

            // キャプションをアセンブリ名に
            this.Text = Assembly.GetExecutingAssembly().GetName().Name;

            this.AutoScroll = true;

            // ProgramTemplateの一覧を探す
            string programTemplateDirectory = Path.Combine(Application.StartupPath, "ProgramTemplate");
            string[] programTemplatePath = Directory.GetFiles(programTemplateDirectory, "ProgramTemplate.xml", SearchOption.AllDirectories);
            programTemplate = new ProgramTemplate[programTemplatePath.Length];

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

        private void new__MouseClick(object sender, MouseEventArgs e)
        {
            if (this.isModified)
            {
                DialogResult result = MessageBox.Show("現在編集中のプログラムは変更されています．" + Environment.NewLine + "新しいプログラムを作る前に保存しますか？", "新規作成", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                if (result == DialogResult.Cancel)
                {
                    return;
                }
                else if (result == DialogResult.Yes)
                {
                }
                else if (result == DialogResult.No)
                {
                    ListBox templateSelectBox = new ListBox();

                    foreach (ProgramTemplate hoge in this.programTemplate)
                    {
                        templateSelectBox.Items.Add(hoge.Description);
                    }

                    Label description = new Label();
                    description.AutoSize = true;
                    description.Location = new Point(0, 0);
                    description.Font = this.baseFont;
                    description.Text = "テンプレートを選択してください";

                    templateSelectBox.Location = new Point(0, description.Height + 10);
                    templateSelectBox.Size = new Size(this.container.Width, this.container.Height - description.Height);
                    templateSelectBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Right;
                    templateSelectBox.Font = this.baseFont;
                    templateSelectBox.BorderStyle = BorderStyle.None;

                    this.container.Controls.Add(description);
                    this.container.Controls.Add(templateSelectBox);

                    templateSelectBox.MouseMove += delegate(object sender2, MouseEventArgs e2)
                    {
                        templateSelectBox.SelectedIndex = templateSelectBox.IndexFromPoint(e2.X, e2.Y);
                        if(templateSelectBox.SelectedIndex != -1)
                            Cursor.Current = Cursors.Hand;
                    };
                    templateSelectBox.MouseLeave += delegate(object sender2, EventArgs e2)
                    {
                        templateSelectBox.SelectedIndex = -1;
                    };
                    templateSelectBox.MouseClick += delegate(object sender2, MouseEventArgs e2)
                    {
                        if (templateSelectBox.SelectedIndex != -1)
                        {
                            ProgramData programData = new ProgramData(programTemplate[templateSelectBox.SelectedIndex]);
                            ProgramEditor programEditor = new ProgramEditor(programData);

                            this.container.Controls.Remove(description);
                            this.container.Controls.Remove(templateSelectBox);
                            this.container.Controls.Add(programEditor);
                        }
                    };
                }
            }
        }
    }
}
