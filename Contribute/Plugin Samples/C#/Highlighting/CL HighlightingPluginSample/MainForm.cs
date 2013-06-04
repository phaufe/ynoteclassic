//CL Syntax Highlighter Plugin

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WeifenLuo.WinFormsUI.Docking;
using SS.Ynote.Engine.Framework.Plugins.Interface;
using System.Xml.Linq;
using FastColoredTextBoxNS;
using SS.Ynote.Classic;
using System.Windows.Forms;

namespace HighlightingPluginSample
{
    public class MainForm :  DockContent, IFormPlugin
    {
        #region Constructor

        public MainForm()
        {
            InitializeComponent();
            this.ShowHint = DockState.DockLeft;
        }
        #endregion

        private Button button1;
        private Button button2;

        #region IPluginMembers

        //IFormPlugin Inherits the IPlugin Interface

        public string Title
        {
            get { return "HighlighterPlugin"; }
        }
        public string Description
        {
            get { return "Sample Highlighter Plugin"; }
        }
        public string Group
        {
            get { return "Plugins"; }
        }
        public string SubGroup
        {
            get { return "Samples"; }
        }

        private XElement configuration = new XElement("HighlighterPluginConfig");
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

        private void codebox_TextChangedDelayed(object sender, TextChangedEventArgs e)
        {
            List<ExplorerItem> Custom = new List<ExplorerItem>();
            HighlightingPluginSample.CLSyntaxHighlighter.Highlight(e.ChangedRange, Custom);
        }

        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 50F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(0, 0);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(358, 257);
            this.button1.TabIndex = 0;
            this.button1.Text = "Highlight";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.button2.Location = new System.Drawing.Point(0, 257);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(358, 249);
            this.button2.TabIndex = 1;
            this.button2.Text = "Insert Sample Text";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // MainForm
            // 
            this.ClientSize = new System.Drawing.Size(358, 506);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.button2);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "MainForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "OpenCL Highlighter Plugin";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);

        }

        private void MainForm_Load(object sender, EventArgs e)
        {
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Main.ActiveEditor.codebox.TextChangedDelayed += new EventHandler<TextChangedEventArgs>(codebox_TextChangedDelayed);
                MessageBox.Show("Highlighter Initialized. Just Type in Text To Highlight");
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                Main.ActiveEditor.codebox.InsertText("kernel void Mono(\n_global uchar4 *input,\nconst float2 file\n)\n\nint Main()\n{\nint coord=(int2)(get_global_id(0));\n}");
            }
            catch { }
        }
    }
}
