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

            foreach(XmlNode rootChildNode in document.ChildNodes)
            {
                if (rootChildNode.Name == "ProgramTemplate")
                {
                    foreach (XmlNode programTemplateChildNode in rootChildNode)
                    {
                        if (programTemplateChildNode.Name == "context")
                        {
                            Context = getContext(programTemplateChildNode.ChildNodes);
                        }
                        if (programTemplateChildNode.Name == "actions")
                        {
                            Actions = getActions(programTemplateChildNode.ChildNodes);
                        }
                    }
                }
            }
        }

        private Status[] getContext(XmlNodeList statusNodeList)
        {
            List<Status> context = new List<Status>();
            foreach (XmlNode statusNode in statusNodeList)
            {
                if (statusNode.Name == "status")
                {
                    string name = "";
                    string caption = "";
                    string image = "";
                    string code = "";
                    Matter[] matterList;
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
                                image = statusAttribute.Value;
                                break;
                            case "code":
                                code = statusAttribute.Value;
                                break;
                        }
                    }
                    matterList = getMatterList(statusNode.ChildNodes);
                    context.Add(new Status(name, caption, image, code, matterList));
                }
            }
            return context.ToArray();
        }

        private Matter[] getMatterList(XmlNodeList matterNodeList)
        {
            List<Matter> matterList = new List<Matter>();
            foreach (XmlNode matterNode in matterNodeList)
            {
                if (matterNode.Name == "matter")
                {
                    string name = "";
                    string caption = "";
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
                    matterList.Add(new Matter(name, caption));
                }
            }
            return matterList.ToArray();
        }

        private Action[] getActions(XmlNodeList actionNodeList)
        {
            List<Action> actions = new List<Action>();
            foreach (XmlNode actionNode in actionNodeList)
            {
                if (actionNode.Name == "action")
                {
                    string name = "";
                    string caption = "";
                    string image = "";
                    string code = "";
                    Procedure[] procedureList;
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
                                image = actionAttribute.Value;
                                break;
                            case "code":
                                code = actionAttribute.Value;
                                break;
                        }
                    }
                    procedureList = getProcedureList(actionNode.ChildNodes);
                    actions.Add(new Action(name, caption, image, code, procedureList));
                }
            }
            return actions.ToArray();
        }

        public Procedure[] getProcedureList(XmlNodeList procedureNodeList)
        {
            List<Procedure> procedureList = new List<Procedure>();
            foreach (XmlNode procedureNode in procedureNodeList)
            {
                if (procedureNode.Name == "procedure")
                {
                    string name = "";
                    string caption = "";
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
                    procedureList.Add(new Procedure(name, caption));
                }
            }
            return procedureList.ToArray();
        }
    }
    public class Status
    {
        public readonly string name;
        public readonly string caption;
        public readonly string image;
        public readonly string code;
        public readonly Matter[] matter;
        public Status(string name, string caption, string image, string code, Matter[] matter)
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
        public readonly string image;
        public readonly string code;
        public readonly Procedure[] procedure;
        public Action(string name, string caption, string image, string code, Procedure[] procedure)
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
