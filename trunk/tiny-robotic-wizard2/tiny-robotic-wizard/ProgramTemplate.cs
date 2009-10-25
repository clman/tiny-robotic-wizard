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
    [Serializable()]
    public class ProgramTemplate : IDisposable
    {
        /// <summary>
        /// ProgramTemplateの名前
        /// </summary>
        public string Name { get; private set; }
        /// <summary>
        /// ProgramTemplateの説明
        /// </summary>
        public string Description { get; private set; }
        /// <summary>
        /// Input
        /// </summary>
        public InputOutput Input { get; private set; }
        /// <summary>
        /// Output
        /// </summary>
        public InputOutput Output { get; private set; }
        /// <summary>
        /// ProgramTemplateXMLファイルがあるディレクトリのURI
        /// </summary>
        private Uri baseUri { get; set; }

        /// <summary>
        /// ProgramTemplate.xmlからProgramTemplateのインスタンスを生成
        /// </summary>
        /// <param name="filePath">ProgramTemplate.xmlの絶対パス</param>
        public ProgramTemplate(string filePath)
        {
            // XMLファイルのURIから，親ディレクトリのURIを取得，BaseUriに納める
            this.baseUri = new Uri(filePath);
            // ProgramTemplateのXML Schema(ProgramTemplate.xsd)を取ってくる．
            XmlReader programTemplateSchema = new XmlTextReader(Path.Combine(Application.StartupPath, "ProgramTemplate.xsd"));
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
            foreach (XmlNode rootChildNode in document.ChildNodes)
            {
                // ルートノードからProgramTemplate要素を探す
                if (rootChildNode.Name == "ProgramTemplate")
                {
                    // device要素の属性を解析
                    foreach (XmlAttribute programTemplateAttribute in rootChildNode.Attributes)
                    {
                        switch (programTemplateAttribute.Name)
                        {
                            case "Name":
                                this.Name = programTemplateAttribute.Value;
                                break;
                            case "Description":
                                this.Description = programTemplateAttribute.Value;
                                break;
                        }
                    }
                    // 子要素を解析
                    foreach (XmlNode programTemplateChildNode in rootChildNode)
                    {
                        // input要素を探す
                        if (programTemplateChildNode.Name == "Input")
                        {
                            // input要素の子ノードからIOクラスのインスタンスを生成
                            this.Input = new InputOutput(getDeviceList(programTemplateChildNode.ChildNodes));
                        }
                        // output要素を探す
                        if (programTemplateChildNode.Name == "Output")
                        {
                            // output要素の子ノードからIOクラスのインスタンスを生成
                            this.Output = new InputOutput(getDeviceList(programTemplateChildNode.ChildNodes));
                        }
                    }
                }
            }
        }

        /// <summary>
        /// input/output要素の子ノードからdevice要素の配列を生成
        /// </summary>
        /// <param name="deviceNodeList">input/output要素の子ノード</param>
        /// <returns>device要素の配列</returns>
        private Device[] getDeviceList(XmlNodeList deviceNodeList)
        {
            // device要素の配列を作るためのリスト
            List<Device> deviceList = new List<Device>();
            // context要素の子ノードを解析
            foreach (XmlNode deviceNode in deviceNodeList)
            {
                // device要素を探す
                if (deviceNode.Name == "Device")
                {
                    // Deviceクラスの初期化はコンストラクタで一括して行うので，そのための一時的な変数を用意
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
                    // option要素の配列
                    Option[] optionList;

                    // device要素の属性を解析
                    foreach (XmlAttribute deviceAttribute in deviceNode.Attributes)
                    {
                        switch (deviceAttribute.Name)
                        {
                            case "Name":
                                name = deviceAttribute.Value;
                                break;
                            case "Caption":
                                caption = deviceAttribute.Value;
                                break;
                            case "Image":
                                imagePath = deviceAttribute.Value;
                                break;
                            case "Code":
                                codePath = deviceAttribute.Value;
                                break;
                        }
                    }

                    // device要素の子ノードからoption要素の配列を取得
                    optionList = getOptionList(deviceNode.ChildNodes);

                    // image属性で指定された画像を分割
                    baseImage = Image.FromFile((new Uri(this.baseUri, imagePath)).LocalPath);
                    for (int i = 0; i <= optionList.Length - 1; i++)
                    {
                        image.Add(((Bitmap)baseImage).Clone(new Rectangle(new Point(0, 100 * i), new Size(180, 100)), baseImage.PixelFormat));
                    }
                    baseImage.Dispose();

                    // code属性で指定されたファイルからプログラムコードを取ってくる
                    StreamReader codeFile = new StreamReader(new Uri(this.baseUri, codePath).LocalPath);
                    code = codeFile.ReadToEnd();
                    codeFile.Close();

                    // 解析結果からDeviceクラスのインスタンスを生成し，device要素のリストに追加
                    deviceList.Add(new Device(name, caption, image.ToArray(), code, optionList));
                }
            }

            // device要素のリストを配列化して返す
            return deviceList.ToArray();
        }

        /// <summary>
        /// device要素の子ノードからoption要素の配列を生成
        /// </summary>
        /// <param name="optionNodeList">device要素の子ノード</param>
        /// <returns>option要素の配列</returns>
        private Option[] getOptionList(XmlNodeList optionNodeList)
        {
            // option要素の配列を作るためのリスト
            List<Option> optionList = new List<Option>();

            // device要素の子ノードを解析
            foreach (XmlNode optionNode in optionNodeList)
            {
                // option要素を探す
                if (optionNode.Name == "Option")
                {
                    // Optionクラスの初期化はコンストラクタで一括して行うので，そのための一時的な変数を用意
                    // name属性
                    string name = "";
                    // caption属性
                    string caption = "";

                    // option要素の属性を解析
                    foreach (XmlAttribute optionAttribute in optionNode.Attributes)
                    {
                        switch (optionAttribute.Name)
                        {
                            case "Name":
                                name = optionAttribute.Value;
                                break;
                            case "Caption":
                                caption = optionAttribute.Value;
                                break;
                        }
                    }

                    // 解析結果からOptionクラスのインスタンスを生成し，option要素のリストに追加
                    optionList.Add(new Option(name, caption));
                }
            }

            // option要素のリストを配列化して返す
            return optionList.ToArray();
        }

        #region IDisposable メンバ

        private bool disposed = false;
        private void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    this.Input.Dispose();
                    this.Output.Dispose();
                }
                this.disposed = true;
            }
        }
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }
        ~ProgramTemplate()
        {
            this.Dispose(false);
        }

        #endregion
    }

    /// <summary>
    /// input/output要素を表現するクラス
    /// </summary>
    [Serializable()]
    public class InputOutput : IDisposable
    {
        /// <summary>
        /// device要素の配列
        /// </summary>
        public Device[] Device { get; private set; }

        /// <summary>
        /// device要素の配列からIOのインスタンスを生成
        /// </summary>
        /// <param name="device">device要素の配列</param>
        public InputOutput(Device[] device)
        {
            this.Device = device;
        }

        #region IDisposable メンバ

        private bool disposed = false;
        private void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    foreach (Device device in this.Device)
                        device.Dispose();
                }
                this.disposed = true;
            }
        }
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }
        ~InputOutput()
        {
            this.Dispose(false);
        }

        #endregion
    }

    /// <summary>
    /// device要素を表現するクラス
    /// </summary>
    [Serializable()]
    public class Device : IDisposable
    {
        /// <summary>
        /// name属性
        /// </summary>
        public string Name { get; private set; }
        /// <summary>
        /// caption属性
        /// </summary>
        public string Caption { get; private set; }
        /// <summary>
        /// image属性で指定された画像を分割した配列
        /// </summary>
        public Image[] Image { get; private set; }
        /// <summary>
        /// code属性で指定されたファイルの中身
        /// </summary>
        public string Code { get; private set; }
        /// <summary>
        /// option要素の配列
        /// </summary>
        public Option[] Option { get; private set; }

        /// <summary>
        /// device要素の属性とoption要素の配列からDeviceのインスタンスを生成
        /// </summary>
        /// <param name="name">name属性</param>
        /// <param name="caption">caption属性</param>
        /// <param name="image">image属性で指定された画像を分割した配列</param>
        /// <param name="code">code属性で指定されたファイの中身</param>
        /// <param name="option">option要素の配列</param>
        public Device(string name, string caption, Image[] image, string code, Option[] option)
        {
            this.Name = name;
            this.Caption = caption;
            this.Image = image;
            this.Code = code;
            this.Option = option;
        }

        #region IDisposable メンバ

        private bool disposed = false;
        private void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    foreach (Image image in this.Image)
                        image.Dispose();
                }
                this.disposed = true;
            }
        }
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }
        ~Device()
        {
            this.Dispose(false);
        }

        #endregion
    }

    /// <summary>
    /// option要素を表現するクラス
    /// </summary>
    [Serializable()]
    public class Option
    {
        /// <summary>
        /// name属性
        /// </summary>
        public string Name { get; private set; }
        /// <summary>
        /// caption属性
        /// </summary>
        public string Caption { get; private set; }

        /// <summary>
        /// option要素の属性からOptionのインスタンスを生成
        /// </summary>
        /// <param name="name">name属性</param>
        /// <param name="caption">caption属性</param>
        public Option(string name, string caption)
        {
            this.Name = name;
            this.Caption = caption;
        }
    }
}
