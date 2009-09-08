using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using System.Reflection;
using System.Windows.Forms;
using System.Drawing;

namespace tiny_robotic_wizard
{
    /// <summary>
    /// プログラムのテンプレートを表現するクラス
    /// </summary>
    class ProgramTemplate
    {
        /// <summary>
        /// Context
        /// </summary>
        public Context Context{get; private set;}
        /// <summary>
        /// Actions
        /// </summary>
        public Actions Actions{get; private set;}

        /// <summary>
        /// ProgramTemplateXMLファイルがあるディレクトリのURI
        /// </summary>
        private Uri baseUri{get; set;}

        /// <summary>
        /// ProgramTemplate.xmlからProgramTemplateのインスタンスを生成
        /// </summary>
        /// <param name="filePath">ProgramTemplate.xmlの絶対パス</param>
        public ProgramTemplate(string filePath)
        {
            // XMLファイルのURIから，親ディレクトリのURIを取得，BaseUriに納める
            this.baseUri = new Uri(filePath);
            // 実行ファイルのURIを取得するためにAssemblyクラスをインスタンス化
            Assembly assembly = Assembly.GetExecutingAssembly();
            // ProgramTemplateのXML Schema(ProgramTemplate.xsd)を取ってくる．
            XmlReader programTemplateSchema = new XmlTextReader(Path.GetDirectoryName(assembly.Location) + @"\ProgramTemplate.xsd");
            // XML Validatingに関する設定を作る
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.Schemas.Add("", programTemplateSchema);
            settings.ValidationFlags = XmlSchemaValidationFlags.AllowXmlAttributes | XmlSchemaValidationFlags.ProcessIdentityConstraints | XmlSchemaValidationFlags.ReportValidationWarnings;
            settings.ValidationType = ValidationType.Schema;
            // XMLReaderをインスタンス化
            XmlReader reader = XmlReader.Create(filePath, settings);
            // XMLDocumentをインスタンス化
            XmlDocument document = new XmlDocument();
            // XMLファイルを読み込む
            document.Load(reader);
            // XMLファイルを閉じる
            reader.Close();

            // XMLファイルをパースする
            foreach(XmlNode rootChildNode in document.ChildNodes)
            {
                // ルートノードからProgramTemplate要素を探す
                if (rootChildNode.Name == "ProgramTemplate")
                {
                    foreach (XmlNode programTemplateChildNode in rootChildNode)
                    {
                        // context要素を探す
                        if (programTemplateChildNode.Name == "context")
                        {
                            // context要素の子ノードからContextクラスのインスタンスを生成
                            this.Context = new Context(getStatusList(programTemplateChildNode.ChildNodes));
                        }
                        // actions要素を探す
                        if (programTemplateChildNode.Name == "actions")
                        {
                            // actions要素の子ノードからActionsクラスのインスタンスを生成
                            this.Actions = new Actions(getActionList(programTemplateChildNode.ChildNodes));
                        }
                    }
                }
            }
        }

        /// <summary>
        /// context要素の子ノードからstatus要素の配列を生成
        /// </summary>
        /// <param name="statusNodeList">context要素の子ノード</param>
        /// <returns>status要素の配列</returns>
        private Status[] getStatusList(XmlNodeList statusNodeList)
        {
            // status要素の配列を作るためのリスト
            List<Status> statusList = new List<Status>();
            // context要素の子ノードを解析
            foreach (XmlNode statusNode in statusNodeList)
            {
                // status要素を探す
                if (statusNode.Name == "status")
                {
                    // Statusクラスの初期化はコンストラクタで一括して行うので，そのための一時的な変数を用意
                    // name属性
                    string name = "";
                    // caption属性
                    string caption = "";
                    // image属性
                    string imagePath = "";
                    // image属性で指定された画像
                    Image baseImage;
                    // image属性で指定された画像を分割したリスト
                    List<Image> image = new List<Image>();
                    // code属性
                    string codePath = "";
                    // code属性で指定されたファイルの中身
                    string code = "";
                    // matter要素の配列
                    Matter[] matterList;

                    // status要素の属性を解析
                    foreach (XmlAttribute statusAttribute in statusNode.Attributes)
                    {
                        switch (statusAttribute.Name)
                        {
                            case "name":
                                name = statusAttribute.Value;
                                break;
                            case "caption":
                                caption = statusAttribute.Value;
                                break;
                            case "image":
                                imagePath = statusAttribute.Value;
                                break;
                            case "code":
                                codePath = statusAttribute.Value;
                                break;
                        }
                    }

                    // status要素の子ノードからmatter要素の配列を取得
                    matterList = getMatterList(statusNode.ChildNodes);

                    // image属性で指定された画像を分割
                    baseImage = Image.FromFile((new Uri(this.baseUri, imagePath)).LocalPath);
                    for (int i = 0; i <= matterList.Length - 1; i++)
                    {
                        image.Add(((Bitmap)baseImage).Clone(new Rectangle(new Point(0, 100*i), new Size(180, 100)), baseImage.PixelFormat));
                    }
                    baseImage.Dispose();

                    // code属性で指定されたファイルからプログラムコードを取ってくる
                    StreamReader codeFile = new StreamReader(new Uri(this.baseUri, codePath).LocalPath);
                    code = codeFile.ReadToEnd();
                    codeFile.Close();

                    // 解析結果からStatusクラスのインスタンスを生成し，status要素のリストに追加
                    statusList.Add(new Status(name, caption, image.ToArray(), code, matterList));
                }
            }

            // status要素のリストを配列化して返す
            return statusList.ToArray();
        }

        /// <summary>
        /// status要素の子ノードからmatter要素の配列を生成
        /// </summary>
        /// <param name="matterNodeList">status要素の子ノード</param>
        /// <returns>matter要素の配列</returns>
        private Matter[] getMatterList(XmlNodeList matterNodeList)
        {
            // matter要素の配列を作るためのリスト
            List<Matter> matterList = new List<Matter>();

            // status要素の子ノードを解析
            foreach (XmlNode matterNode in matterNodeList)
            {
                // matter要素を探す
                if (matterNode.Name == "matter")
                {
                    // Matterクラスの初期化はコンストラクタで一括して行うので，そのための一時的な変数を用意
                    // name属性
                    string name = "";
                    // caption属性
                    string caption = "";

                    // matter要素の属性を解析
                    foreach (XmlAttribute matterAttribute in matterNode.Attributes)
                    {
                        switch (matterAttribute.Name)
                        {
                            case "name":
                                name = matterAttribute.Value;
                                break;
                            case "caption":
                                caption = matterAttribute.Value;
                                break;
                        }
                    }

                    // 解析結果からMatterクラスのインスタンスを生成し，matter要素のリストに追加
                    matterList.Add(new Matter(name, caption));
                }
            }

            // matter要素のリストを配列化して返す
            return matterList.ToArray();
        }

        /// <summary>
        /// actions要素の子ノードからaction要素の配列を生成
        /// </summary>
        /// <param name="actionNodeList">actions要素の子ノード</param>
        /// <returns>action要素の配列</returns>
        private Action[] getActionList(XmlNodeList actionNodeList)
        {
            // action要素の配列を作るためのリスト
            List<Action> actionList = new List<Action>();

            // actions要素の子ノードを解析
            foreach (XmlNode actionNode in actionNodeList)
            {
                // action要素を探す
                if (actionNode.Name == "action")
                {
                    // actionクラスの初期化はコンストラクタで一括して行うので，そのための一時的な変数を用意
                    // name属性
                    string name = "";
                    // caption属性
                    string caption = "";
                    // image属性
                    string imagePath = "";
                    // image属性で指定された画像
                    Image baseImage;
                    // image属性で指定された画像を分割したリスト
                    List<Image> image = new List<Image>();
                    // code属性
                    string codePath = "";
                    // code属性で指定されたファイルの中身
                    string code ="";
                    // procedure要素の配列
                    Procedure[] procedureList;

                    // action要素の属性を解析
                    foreach (XmlAttribute actionAttribute in actionNode.Attributes)
                    {
                        switch (actionAttribute.Name)
                        {
                            case "name":
                                name = actionAttribute.Value;
                                break;
                            case "caption":
                                caption = actionAttribute.Value;
                                break;
                            case "image":
                                imagePath = actionAttribute.Value;
                                break;
                            case "code":
                                codePath = actionAttribute.Value;
                                break;
                        }
                    }

                    // action要素の子ノードからprocedure要素の配列を生成
                    procedureList = getProcedureList(actionNode.ChildNodes);

                    // image属性で指定された画像を分割
                    baseImage = Image.FromFile((new Uri(this.baseUri, imagePath)).LocalPath);
                    for (int i = 0; i <= procedureList.Length - 1; i++)
                    {
                        image.Add(((Bitmap)baseImage).Clone(new Rectangle(new Point(0, 100 * i), new Size(180, 100)), baseImage.PixelFormat));
                    }
                    baseImage.Dispose();

                    // code属性で指定されたファイルからプログラムコードを取ってくる
                    StreamReader codeFile = new StreamReader(new Uri(this.baseUri, codePath).LocalPath);
                    code = codeFile.ReadToEnd();
                    codeFile.Close();

                    // 解析結果からActionクラスのインスタンスを生成し，action要素のリストに追加
                    actionList.Add(new Action(name, caption, image.ToArray(), code, procedureList));
                }
            }

            // action要素のリストを配列化して返す
            return actionList.ToArray();
        }

        /// <summary>
        /// action要素の子ノードからprocedure要素の配列を生成
        /// </summary>
        /// <param name="procedureNodeList">action要素の子ノード</param>
        /// <returns>procedure要素の配列</returns>
        private Procedure[] getProcedureList(XmlNodeList procedureNodeList)
        {
            // procedure要素の配列を作るためのリスト
            List<Procedure> procedureList = new List<Procedure>();

            // action要素の子ノードを解析
            foreach (XmlNode procedureNode in procedureNodeList)
            {
                // procedure要素を探す
                if (procedureNode.Name == "procedure")
                {
                    // actionクラスの初期化はコンストラクタで一括して行うので，そのための一時的な変数を用意
                    // name属性
                    string name = "";
                    // caption属性
                    string caption = "";

                    // procedure要素の属性を解析
                    foreach (XmlAttribute procedureAttribute in procedureNode.Attributes)
                    {
                        switch (procedureAttribute.Name)
                        {
                            case "name":
                                name = procedureAttribute.Value;
                                break;
                            case "caption":
                                caption = procedureAttribute.Value;
                                break;
                        }
                    }

                    // 解析結果からProcedureクラスのインスタンスを生成し，procedure要素のリストに追加
                    procedureList.Add(new Procedure(name, caption));
                }
            }

            // procedure要素のリストを配列化して返す
            return procedureList.ToArray();
        }
    }

    /// <summary>
    /// context要素を表現するクラス
    /// </summary>
    class Context
    {
        /// <summary>
        /// status要素の配列
        /// </summary>
        public Status[] Status{get; private set;}

        /// <summary>
        /// status要素の配列からContextのインスタンスを生成
        /// </summary>
        /// <param name="status">status要素の配列</param>
        public Context(Status[] status)
        {
            this.Status = status;
        }
    }

    /// <summary>
    /// status要素を表現するクラス
    /// </summary>
    class Status
    {
        /// <summary>
        /// name属性
        /// </summary>
        public string Name{get; private set;}
        /// <summary>
        /// caption属性
        /// </summary>
        public string Caption{get; private set;}
        /// <summary>
        /// image属性で指定された画像を分割した配列
        /// </summary>
        public Image[] Image{get; private set;}
        /// <summary>
        /// code属性で指定されたファイルの中身
        /// </summary>
        public string Code{get; private set;}
        /// <summary>
        /// matter要素の配列
        /// </summary>
        public Matter[] Matter{get; private set;}

        /// <summary>
        /// status要素の属性とmatter要素の配列からStatusのインスタンスを生成
        /// </summary>
        /// <param name="name">name属性</param>
        /// <param name="caption">caption属性</param>
        /// <param name="image">image属性で指定された画像を分割した配列</param>
        /// <param name="code">code属性で指定されたファイの中身</param>
        /// <param name="matter">matter要素の配列</param>
        public Status(string name, string caption, Image[] image, string code, Matter[] matter)
        {
            this.Name = name;
            this.Caption = caption;
            this.Image = image;
            this.Code = code;
            this.Matter = matter;
        }
    }

    /// <summary>
    /// matter要素を表現するクラス
    /// </summary>
    class Matter
    {
        /// <summary>
        /// name属性
        /// </summary>
        public string Name{get; private set;}
        /// <summary>
        /// caption属性
        /// </summary>
        public string Caption{get; private set;}

        /// <summary>
        /// matter要素の属性からMatterのインスタンスを生成
        /// </summary>
        /// <param name="name">name属性</param>
        /// <param name="caption">caption属性</param>
        public Matter(string name, string caption)
        {
            this.Name = name;
            this.Caption = caption;
        }
    }

    /// <summary>
    /// actions要素を表現するクラス
    /// </summary>
    class Actions
    {
        /// <summary>
        /// action要素の配列
        /// </summary>
        public Action[] Action{get; private set;}

        /// <summary>
        /// action要素の配列からActionsのインスタンスを生成
        /// </summary>
        /// <param name="action">action要素の配列</param>
        public Actions(Action[] action)
        {
            this.Action = action;
        }
    }

    /// <summary>
    /// action要素を表現するクラス
    /// </summary>
    class Action
    {
        /// <summary>
        /// name属性
        /// </summary>
        public string Name{get; private set;}
        /// <summary>
        /// caption属性
        /// </summary>
        public string Caption{get; private set;}
        /// <summary>
        /// image属性で指定された画像を分割した配列
        /// </summary>
        public Image[] Image{get; private set;}
        /// <summary>
        /// code属性で指定されたファイルの中身
        /// </summary>
        public string Code{get; private set;}
        /// <summary>
        /// procedure要素の配列
        /// </summary>
        public Procedure[] Procedure{get; private set;}

        /// <summary>
        /// action要素の属性とprocedure要素の配列からActionのインスタンスを生成
        /// </summary>
        /// <param name="name">name属性</param>
        /// <param name="caption">caption属性</param>
        /// <param name="image">image属性で指定された画像を分割した配列</param>
        /// <param name="code">code属性で指定されたファイルの中身</param>
        /// <param name="procedure">procedure要素の配列</param>
        public Action(string name, string caption, Image[] image, string code, Procedure[] procedure)
        {
            this.Name = name;
            this.Caption = caption;
            this.Image = image;
            this.Code = code;
            this.Procedure = procedure;
        }
    }

    /// <summary>
    /// procedure要素を表現するクラス
    /// </summary>
    class Procedure
    {
        /// <summary>
        /// name属性
        /// </summary>
        public string Name{get; private set;}
        /// <summary>
        /// caption属性
        /// </summary>
        public string Caption{get; private set;}

        /// <summary>
        /// procedure要素の属性からProcedureのインスタンスを生成
        /// </summary>
        /// <param name="name">name属性</param>
        /// <param name="caption">caption属性</param>
        public Procedure(string name, string caption)
        {
            this.Name = name;
            this.Caption = caption;
        }
    }
}
