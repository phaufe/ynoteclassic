using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SS.Ynote.Engine.Framework.Plugins.Interface;
using System.Xml.Linq;
using FastColoredTextBoxNS;
using SS.Ynote.Classic;

//========================================
//
//Wavy Style can be use in error checking
//you can make a plugin for error checking for any language
//
//This is just a sample
//
//========================================

namespace WavyStylePlugin
{
    /// <summary>
    /// Simple Spell Checker
    /// </summary>
    public partial class Form1 : WeifenLuo.WinFormsUI.Docking.DockContent, IFormPlugin
    {
        #region Constants

        WavyLineStyle RedWavy = new WavyLineStyle(100, Color.Red);

        #endregion

        #region Constructor
        public Form1()
        {
            InitializeComponent();
            Hide();
            this.Visible = false;
            Main.ActiveEditor.codebox.TextChangedDelayed += new EventHandler<TextChangedEventArgs>(codebox_TextChangedDelayed);
            MessageBox.Show("Spell Checker Initialized!");
        }
        #endregion

        #region IPlugin Members

        public string Title
        {
            get { return "WavyStylePlugin"; }
        }
        public string Description
        {
            get { return "Wavy Style Sample Plugin"; }
        }
        public string Group
        {
            get { return "Plugins"; }
        }
        public string SubGroup
        {
            get { return "Samples"; }
        }

        private XElement configuration = new XElement("WavyStylePluginConfig");
        public XElement Configuration
        {
            get { return configuration; }
            set { configuration = value; }
        }

        public string Icon
        {
            //get { return "C:\\Icons\\youricon.ico"; }
            get { return string.Empty; }
        }

        #endregion

        #region IFormPluginMembers

        public WeifenLuo.WinFormsUI.Docking.DockContent Content
        {
            get { return this; }
        }
        public ShowAs ShowAs
        {
            get { return ShowAs.Normal; }
        }
        #endregion

        private void codebox_TextChangedDelayed(object sender, TextChangedEventArgs e){
            SpellChecker SpellChecker = new SpellChecker(Main.ActiveEditor.codebox, @"Dictionary.dic");
            SpellChecker.SpellCheck(sender);
        }
    }
}
