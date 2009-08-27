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

            

        }
        public class Status
        {
            public readonly int Count;
            public readonly string Caption;
            public readonly Bitmap Image;
            public readonly string Code;
            public Status(int count, string caption, Bitmap image, string code)
            {
                Count = count;
                Caption = caption;
                Image = image;
                Code = code;
            }
        }
        public class Action
        {
            public readonly int Count;
            public readonly string Caption;
            public readonly Bitmap Image;
            public readonly string Code;
            public Action(int count, string caption, Bitmap image, string code)
            {
                Count = count;
                Caption = caption;
                Image = image;
                Code = code;
            } 
        }
    }
}
