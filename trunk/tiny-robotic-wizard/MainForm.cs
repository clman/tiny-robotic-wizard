using System;
using System.IO;
using System.Reflection;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using tiny_robotic_wizard.GUI;

namespace tiny_robotic_wizard
{
    public partial class MainForm : Form
    {
        // プログラムテンプレート
        private ProgramTemplate[] programTemplates;

        // 各種データ
        private string applicationName;
        private ProgramManager programManager = new ProgramManager(Path.Combine(Application.StartupPath, "program"));
        private ProgramTemplate programTemplate;
        private ProgramData programData;
        private string currentFileName;

        // 各種GUI
        private ProgramEditor programEditor;
        private ClickableList templateSelectBox;
        private ClickableList fileSelectBox;
        private ClickableList deleteSelectBox;

        // 各種フラグ
        /// <summary>
        /// 編集されたかどうか
        /// </summary>
        private bool isModified = false;
        /// <summary>
        /// 編集中のファイルが新規ファイルかどうか
        /// </summary>
        private bool isNew = false;

        public MainForm()
        {
            InitializeComponent();

            // キャプションをアセンブリ名に
            this.applicationName = Assembly.GetExecutingAssembly().GetName().Name;
            this.Text = this.applicationName;

            // ProgramTemplateの一覧を探す
            string programTemplateDirectory = Path.Combine(Application.StartupPath, "ProgramTemplate");
            string[] programTemplatePath = Directory.GetFiles(programTemplateDirectory, "ProgramTemplate.xml", SearchOption.AllDirectories);
            programTemplates = new ProgramTemplate[programTemplatePath.Length];

            // ProgramTemplateをすべて読み込む
            for (int i = 0; i <= programTemplatePath.Length - 1; i++)
            {
                programTemplates[i] = new ProgramTemplate(programTemplatePath[i]);
            }

            // 各種GUIのインスタンスを生成
            this.programEditor = new ProgramEditor();
            this.templateSelectBox = new ClickableList();
            this.fileSelectBox = new ClickableList();
            this.deleteSelectBox = new ClickableList();

            // 各種GUIの初期化
            this.programEditor.ModifiedChanged += delegate(bool modified)
            {
                this.isModified = modified;
                updateFormState();
            };
            this.programEditor.Location = new Point(0, 0);
            this.programEditor.Font = Program.BaseFont;
            this.mainPanel.Controls.Add(this.programEditor);

            this.templateSelectBox.Location = new Point(0, 0);
            this.templateSelectBox.Dock = DockStyle.Fill;
            this.templateSelectBox.Font = Program.BaseFont;
            this.mainPanel.Controls.Add(this.templateSelectBox);
            // 選択時のイベント設定
            this.templateSelectBox.ItemMouseClick += delegate(int selectedIndex)
            {
                // 選択されたProgramTemplateでProgramDataを作り，ProgramEditorにセットする．
                this.programTemplate = this.programTemplates[selectedIndex];
                this.programData = new ProgramData(programTemplates[selectedIndex]);
                this.programEditor.ProgramData = this.programData;

                // 各種データを更新
                this.isNew = true;
                this.isModified = false;
                this.currentFileName = Properties.Resources.NewFileName;

                // 編集画面に戻る
                this.functionEdit();
            };

            this.fileSelectBox.Location = new Point(0, 0);
            this.fileSelectBox.Dock = DockStyle.Fill;
            this.fileSelectBox.Font = Program.BaseFont;
            this.mainPanel.Controls.Add(this.fileSelectBox);
            // 選択時のイベント設定
            this.fileSelectBox.ItemMouseClick += delegate(int selectedIndex)
            {
                // 選択されたファイルからProgramDataを復元し，ProgramEditorにセットする．
                this.programData = this.programManager.Load(programManager.GetFileList()[selectedIndex]);
                this.programEditor.ProgramData = this.programData;

                // 各種データの更新
                this.isNew = false;
                this.isModified = false;
                currentFileName = programManager.GetFileList()[selectedIndex];

                // 編集画面に戻る
                this.functionEdit();
            };

            this.deleteSelectBox.Location = new Point(0, 0);
            this.deleteSelectBox.Dock = DockStyle.Fill;
            this.deleteSelectBox.Font = Program.BaseFont;
            this.mainPanel.Controls.Add(this.deleteSelectBox);
            // 選択時のイベント設定
            this.deleteSelectBox.ItemMouseClick += delegate(int selectedIndex)
            {
                // 選択されたファイルを削除する
                this.programManager.Delete(programManager.GetFileList()[selectedIndex]);

                if (currentFileName == null)
                    this.loadForm();
                else
                    this.functionEdit();
            };

            // フォームの状態を更新
            this.updateFormState();

            // ガイドテキストの設定
            guideText.Font = Program.BaseFont;

            // 起動時のフォームの設定を反映
            this.loadForm();

            /*
            ProgramData programData = new ProgramData(programTemplate[3]);

            ProgramManager programManager = new ProgramManager(this.savedProgramDirectory);
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

        /// <summary>
        /// すべてのコントロールを非表示にする
        /// </summary>
        private void hideAllControl()
        {
            this.programEditor.Visible = false;
            this.templateSelectBox.Visible = false;
            this.fileSelectBox.Visible = false;
            this.deleteSelectBox.Visible = false;
        }

        /// <summary>
        /// 各種パラメータに応じて，
        /// ボタンの有効無効を更新する
        /// </summary>
        private void updateFormState()
        {
            this.new_.Enabled = true;
            
            this.open.Enabled = (this.programManager.GetFileList().Length != 0);

            this.deleteSelectBox.Items.Clear();
            foreach (string fileName in this.programManager.GetFileList())
            {
                if(fileName != currentFileName)
                this.deleteSelectBox.Items.Add(fileName);
            }
            this.delete.Enabled = (this.deleteSelectBox.Items.Count != 0);

            this.saveAs.Enabled = (this.programEditor.Visible);
            
            this.save.Enabled = (this.programEditor.Visible && !isNew && isModified);

            this.edit.Enabled = (this.currentFileName != null);
            
            this.transfer.Enabled = (this.programEditor.Visible);

            if (currentFileName != null)
                this.Text = this.applicationName + Properties.Resources.FormCaptionSplitter + this.currentFileName + (isModified ? "*" : "");
            else
                this.Text = this.applicationName;
        }

        private bool saveOrSaveAs(bool saveAs)
        {
            if (saveAs)
            {
            retry:
                string inputText = Microsoft.VisualBasic.Interaction.InputBox("保存するプログラムに付ける名前を入力してください", "名前を付けて保存", "", -1, -1);
                if (inputText != "")
                {
                    // ファイル名に使えない文字を取り除いて，拡張子を変更する
                    inputText = FileNameValidator.ValidFileName(inputText);
                    inputText = Path.ChangeExtension(inputText, Properties.Resources.Extension);

                    // すでに同じ名前のファイルがあった場合
                    if (!programManager.IsUnique(inputText))
                    {
                        DialogResult result = MessageBox.Show("そのファイルは既に存在します．" + Environment.NewLine + "上書きしますか？", "名前を付けて保存", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                        if (result == DialogResult.No)
                            goto retry;
                        else if (result == DialogResult.Cancel)
                            return false;
                    }

                    // 指定された名前で保存する
                    this.programManager.Save(this.programData, inputText);

                    // 各種データを更新
                    this.isNew = false;
                    this.isModified = false;
                    this.currentFileName = inputText;

                    this.updateFormState();

                    return true;
                }
                else
                    return false;
            }
            else
            {
                // 編集中のファイルを保存
                programManager.Save(this.programData, currentFileName);

                // 各種データを更新
                isNew = false;
                isModified = false;

                // フォームの状態を更新
                this.updateFormState();

                return true;
            }
        }

        // 起動時
        private void loadForm()
        {
            this.hideAllControl();
            this.updateFormState();

            if (this.open.Enabled)
                this.guideText.Text = "プログラムを[開く]か[新規作成]できます";
            else
                this.guideText.Text = "プログラムを[新規作成]できます";
        }

        // [新規作成]がクリックされたとき
        private void new__MouseClick(object sender, MouseEventArgs e)
        {
            functionNew();
        }
        private void functionNew()
        {

            // プログラムが変更されている場合
            if (this.isModified)
            {
                // 保存するか破棄するか問い合わせる
                DialogResult result = MessageBox.Show("現在編集中のプログラムは変更されています．" + Environment.NewLine + "新しいプログラムを作る前に保存しますか？", "新規作成", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

                if (result == DialogResult.Yes)
                {
                    // 保存する場合
                    bool saved = saveOrSaveAs(isNew);
                    if (!saved)
                        return;
                }
                else if (result == DialogResult.Cancel)
                {
                    // キャンセルする場合
                    return;
                }
            }

            // テンプレートを選択するリストを作る
            this.templateSelectBox.Items.Clear();
            foreach (ProgramTemplate hoge in this.programTemplates)
            {
                this.templateSelectBox.Items.Add(hoge.Description);
            }

            // 各種GUIの表示設定
            this.hideAllControl();
            this.templateSelectBox.Visible = true;

            // ガイドテキストの設定
            this.guideText.Text = "テンプレートを選択してください";
        }

        // [開く]がクリックされたとき
        private void open_MouseClick(object sender, MouseEventArgs e)
        {
            functionOpen();
        }
        private void functionOpen()
        {
            // プログラムが変更されている場合
            if (this.isModified)
            {
                // 保存するか破棄するか問い合わせる
                DialogResult result = MessageBox.Show("現在編集中のプログラムは変更されています．" + Environment.NewLine + "新しいプログラムを作る前に保存しますか？", "開く", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);

                if (result == DialogResult.Yes)
                {
                    bool saved = saveOrSaveAs(isNew);
                    if (!saved)
                        return;
                }
                else if (result == DialogResult.Cancel)
                {
                    // キャンセルする場合
                    return;
                }
            }

            // ファイルを選択するリストを作る
            this.fileSelectBox.Items.Clear();
            foreach (string fileName in this.programManager.GetFileList())
            {
                this.fileSelectBox.Items.Add(fileName);
            }

            // 各種GUIの表示設定
            this.hideAllControl();
            this.fileSelectBox.Visible = true;

            // ガイドテキストの設定
            this.guideText.Text = "開きたいプログラムを選択してください";
        }

        // [削除]がクリックされたとき
        private void delete_MouseClick(object sender, MouseEventArgs e)
        {
            functionDelete();
        }
        private void functionDelete()
        {
            // ファイルを選択するリストを作る
            this.deleteSelectBox.Items.Clear();
            foreach (string fileName in this.programManager.GetFileList())
            {
                if (fileName != currentFileName)
                    this.deleteSelectBox.Items.Add(fileName);
            }

            // 各種GUIの表示設定
            this.hideAllControl();
            this.deleteSelectBox.Visible = true;

            // フォームの状態を更新
            this.updateFormState();

            // ガイドテキストの設定
            this.guideText.Text = "削除するプログラムを選択してください";
        }

        // [名前を付けて保存]がクリックされたとき
        private void saveAs_MouseClick(object sender, MouseEventArgs e)
        {
            saveOrSaveAs(true);
        }

        // [保存]がクリックされたとき
        private void save_MouseClick(object sender, MouseEventArgs e)
        {
            saveOrSaveAs(false);
        }

        // [編集]がクリックされたとき
        private void edit_MouseClick(object sender, MouseEventArgs e)
        {
            functionEdit();
        }
        private void functionEdit()
        {
            // 各種GUIの表示設定
            this.hideAllControl();
            this.programEditor.Visible = true;

            // フォームの状態を更新
            this.updateFormState();

            // ガイドテキストの設定
            this.guideText.Text = "プログラムを編集できます";
        }
    }
}
