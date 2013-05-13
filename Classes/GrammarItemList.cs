﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Windows.Forms;
using System.Reflection;
using Irony.Parsing;
using System.IO;
using System.Threading; 

namespace SS.Ynote.Classic
{
    public class GrammarItem
    {
        public readonly string Caption;
        public readonly string LongCaption;
        public readonly string Location; 
        public readonly string TypeName; 
        internal bool _loading;
        public GrammarItem(string caption, string location, string typeName)
        {
            Caption = caption;
            Location = location;
            TypeName = typeName;
        }
        public GrammarItem(Type grammarClass, string assemblyLocation)
        {
            _loading = true;
            Location = assemblyLocation;
            TypeName = grammarClass.FullName;
            //Get language name from Language attribute
            Caption = grammarClass.Name; //default caption
            LongCaption = Caption;
            var langAttr = LanguageAttribute.GetValue(grammarClass);
            if (langAttr != null)
            {
                Caption = langAttr.LanguageName;
                if (!string.IsNullOrEmpty(langAttr.Version))
                    Caption += ", version " + langAttr.Version;
                LongCaption = Caption;
                if (!string.IsNullOrEmpty(langAttr.Description))
                    LongCaption += ": " + langAttr.Description;
            }
        }
        public GrammarItem(XmlElement element)
        {
            Caption = element.GetAttribute("Caption");
            Location = element.GetAttribute("Location");
            TypeName = element.GetAttribute("TypeName");
        }
        public void Save(XmlElement toElement)
        {
            toElement.SetAttribute("Caption", Caption);
            toElement.SetAttribute("Location", Location);
            toElement.SetAttribute("TypeName", TypeName);
        }
        public override string ToString()
        {
            return _loading ? LongCaption : Caption;
        }

    }//class

    public class GrammarItemList : List<GrammarItem>
    {
        public static GrammarItemList FromXml(string xml)
        {
            GrammarItemList list = new GrammarItemList();
            XmlDocument xdoc = new XmlDocument();
            xdoc.LoadXml(xml);
            XmlNodeList xlist = xdoc.SelectNodes("//Grammar");
            foreach (XmlElement xitem in xlist)
            {
                GrammarItem item = new GrammarItem(xitem);
                list.Add(item);
            }
            return list;
        }
        public static GrammarItemList FromCombo(ComboBox combo)
        {
            GrammarItemList list = new GrammarItemList();
            foreach (GrammarItem item in combo.Items)
                list.Add(item);
            return list;
        }

        public string ToXml()
        {
            XmlDocument xdoc = new XmlDocument();
            XmlElement xlist = xdoc.CreateElement("Grammars");
            xdoc.AppendChild(xlist);
            foreach (GrammarItem item in this)
            {
                XmlElement xitem = xdoc.CreateElement("Grammar");
                xlist.AppendChild(xitem);
                item.Save(xitem);
            } 
            return xdoc.OuterXml;
        }

        public void ShowIn(ComboBox combo)
        {
            combo.Items.Clear();
            foreach (GrammarItem item in this)
                combo.Items.Add(item);
        }

    }
}
