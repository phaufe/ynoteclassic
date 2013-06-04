using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SS.Ynote.Engine.Framework.Plugins.Interface;
using WeifenLuo.WinFormsUI.Docking;
using System.Xml.Linq;

namespace XMLParserPlugin
{
    public partial class MainForm : DockContent, IFormPlugin
    {
        public MainForm()
        {
            InitializeComponent();
            this.ShowHint = DockState.DockRight;
        }
        #region IPluginMembers

        //IFormPlugin Inherits the IPlugin Interface

        public string Title
        {
            get { return "XMLParserPlugin"; }
        }
        public string Description
        {
            get { return "Sample XML Parser Plugin"; }
        }
        public string Group
        {
            get { return "Plugins"; }
        }
        public string SubGroup
        {
            get { return "Samples"; }
        }

        private XElement configuration = new XElement("XMLParserPluginConfig");
        public XElement Configuration
        {
            get { return configuration; }
            set { configuration = value; }
        }

        public string Icon
        {
            get { return string.Empty; }
        }

        #endregion

        #region IFormPlugin Members

        public DockContent Content
        {
            get { return this; }
        }
        public ShowAs ShowAs
        {
            get { return ShowAs.Normal; }
        }
        #endregion
        

        

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            XmlTree.Xml = SS.Ynote.Classic.Main.ActiveEditor.codebox.Text;
        }
    }
}
