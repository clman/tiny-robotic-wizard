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
    class ProgramTemplate
    {
        public readonly List<Status> Context = new List<Status>();
        public readonly List<Action> Actions = new List<Action>();
        public ProgramTemplate(string filePath)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            XmlReader programTemplateSchema = new XmlTextReader(Path.GetDirectoryName(assembly.Location) + @"\ProgramTemplate.xsd");
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.Schemas.Add("", programTemplateSchema);
            settings.ValidationFlags = XmlSchemaValidationFlags.AllowXmlAttributes | XmlSchemaValidationFlags.ProcessIdentityConstraints | XmlSchemaValidationFlags.ReportValidationWarnings;
            settings.ValidationType = ValidationType.Schema;
            XmlReader reader = XmlReader.Create(filePath, settings);
            XmlDocument document = new XmlDocument();
            document.Load(reader);

            System.Diagnostics.Debug.WriteLine("デバッグ＞" + document.ChildNodes[1].Name);

            foreach(XmlNode rootChild in document.ChildNodes)
            {
                if (rootChild.Name == "ProgramTemplate")
                {
                    foreach (XmlNode programTemplateChild in rootChild)
                    {
                        if (programTemplateChild.Name == "context")
                        {
                            foreach (XmlNode contextChild in programTemplateChild)
                            {
                                if (contextChild.Name == "case")
                                {
                                    int count;
                                    string caption;
                                    Bitmap image;
                                    string code;
                                    foreach (XmlAttribute caseAttribute in contextChild.Attributes)
                                    {
                                    }
                                }
                            }
                        }
                        if (programTemplateChild.Name == "actions")
                        {
                            foreach (XmlNode actionsChild in programTemplateChild)
                            {
                            }
                        }
                    }
                }
            }

        }
        public class Status
        {
            public readonly int count;
            public readonly string caption;
            public readonly Bitmap image;
            public readonly string code;
            public Status(int count, string caption, Bitmap image, string code)
            {
                this.count = count;
                this.caption = caption;
                this.image = image;
                this.code = code;
            }
        }
        public class Case
        {
            public readonly string name;
            public readonly string caption;
        }
        public class Action
        {
            public readonly int count;
            public readonly string caption;
            public readonly Bitmap image;
            public readonly string code;
            public Action(int count, string caption, Bitmap image, string code)
            {
                this.count = count;
                this.caption = caption;
                this.image = image;
                this.code = code;
            } 
        }
    }
}
