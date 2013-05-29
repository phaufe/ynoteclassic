using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;
using SS.Ynote.Classic;
using SS.Ynote.Engine.Framework.Plugins;
using SS.Ynote.Engine.Framework.Plugins.Controls;
using SS.Ynote.Engine.Framework.Plugins.Interface;
using WeifenLuo.WinFormsUI.Docking;

namespace QuickNotePlugin
{
    ///<summary>
    ///Created with Ynote Classic
    ///Compiled with csc.exe
    ///</summary>
    public partial class MainForm : DockContent, IFormPlugin
    {
        #region Constructor

        public MainForm()
        {
            InitializeComponent();
            this.newToolStripMenuItem1.Click += new EventHandler(menuItem2_Click);
            this.openToolStripMenuItem1.Click += new EventHandler(menuItem3_Click);
            this.importFromMainEditorToolStripMenuItem.Click += new EventHandler(menuItem18_Click);
            this.saveAsToolStripMenuItem1.Click += new EventHandler(menuItem5_Click);
            this.clearAllToolStripMenuItem.Click += new EventHandler(menuItem2_Click);
            this.cutToolStripMenuItem.Click += new EventHandler(menuItem8_Click);
            this.copyToolStripMenuItem.Click += new EventHandler(menuItem9_Click);
            this.pasteToolStripMenuItem.Click += new EventHandler(menuItem10_Click);
            this.selectAllToolStripMenuItem.Click += new EventHandler(menuItem11_Click);
            this.undoToolStripMenuItem.Click += new EventHandler(menuItem7_Click);
            this.fontToolStripMenuItem.Click += new EventHandler(menuItem14_Click);
            this.backColorToolStripMenuItem.Click += new EventHandler(menuItem15_Click);
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

        #region IPlugin Members

        public string Title
        {
            get { return "QuickNotePlugin"; }
        }
        public string Description
        {
            get { return "This is  a Quick Note Plugin for Ynote Classic"; }
        }
        public string Group
        {
            get { return "Plugins"; }
        }
        public string SubGroup
        {
            get { return "Note"; }
        }

        private XElement configuration = new XElement("ThisFormConfig");
        public XElement Configuration
        {
            get { return configuration; }
            set { configuration = value; }
        }

        public string Icon
        {
            //get { return "C:\\Icons\\Folder.ico"; }
            get { return string.Empty; }
        }
        #endregion

        #region Events

        private void menuItem7_Click(object sender, EventArgs e)
        {
            this.txtmain.Undo();
        }

        private void menuItem18_Click(object sender, EventArgs e)
        {
            this.txtmain.Text = Main.UserText;
        }

        private void menuItem2_Click(object sender, EventArgs e)
        {
            this.txtmain.Clear();
        }

        private void menuItem3_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.OpenFileDialog o = new System.Windows.Forms.OpenFileDialog();
            o.Filter = "Text Files (*.txt)|*.txt|All Files(*.*)|*.*";
            o.ShowDialog();
            txtmain.Text = File.ReadAllText(o.FileName, Encoding.Default);

        }

        private void menuItem8_Click(object sender, EventArgs e)
        {
            this.txtmain.Cut();
        }

        private void menuItem9_Click(object sender, EventArgs e)
        {
            this.txtmain.Copy();
        }

        private void menuItem10_Click(object sender, EventArgs e)
        {
            this.txtmain.Paste();
        }

        private void menuItem11_Click(object sender, EventArgs e)
        {
            this.txtmain.SelectAll();
        }
        private void menuItem14_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.FontDialog f = new System.Windows.Forms.FontDialog();
            f.ShowEffects = true;
            f.ShowColor = true;
            f.ShowDialog();
            txtmain.Font = f.Font;
            txtmain.ForeColor = f.Color;
  
        }

        private void menuItem15_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.ColorDialog c = new System.Windows.Forms.ColorDialog();
            c.ShowDialog();
            this.txtmain.BackColor = c.Color;
        }

        private void menuItem5_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.SaveFileDialog s = new System.Windows.Forms.SaveFileDialog();
            s.Filter = "All Files (*.*)|*.*";
            s.ShowDialog();
            if (s.FileName != "")
                File.WriteAllText(s.FileName, txtmain.Text, Encoding.Default);
        }

        private void menuItem19_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void exitToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        #endregion
    }
}
