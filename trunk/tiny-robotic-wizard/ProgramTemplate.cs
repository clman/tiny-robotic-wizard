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
        public readonly Status[] Context;
        public readonly Action[] Actions;
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
                                if (contextChild.Name == "matter")
                                {

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
            public readonly string name;
            public readonly string caption;
            public readonly Bitmap image;
            public readonly string code;
            public readonly Matter[] matter;
            public Status(string name, int count, string caption, Bitmap image, string code, Matter[] matter)
            {
                this.name = name;
                this.caption = caption;
                this.image = image;
                this.code = code;
                this.matter = matter;
            }
        }
        public class Matter
        {
            public readonly string name;
            public readonly string caption;
            public Matter(string name, string caption)
            {
                this.name = name;
                this.caption = caption;
            }
        }
        public class Action
        {
            public readonly string name;
            public readonly string caption;
            public readonly Bitmap image;
            public readonly string code;
            public readonly Procedure[] procedure;
            public Action(string name, string caption, Bitmap image, string code, Procedure[] procedure)
            {
                this.name = name;
                this.caption = caption;
                this.image = image;
                this.code = code;
                this.procedure = procedure;
            } 
        }
        public class Procedure
        {
            public readonly string name;
            public readonly string caption;
            public Procedure(string name, string caption)
            {
                this.name = name;
                this.caption = caption;
            }
        }
    }
}
