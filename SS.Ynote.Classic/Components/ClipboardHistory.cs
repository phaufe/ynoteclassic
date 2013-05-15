using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace SS.Ynote.Classic
{
    public class ClipboardHistory : WeifenLuo.WinFormsUI.Docking.DockContent
    {
        #region Constructor

        public ClipboardHistory()
        {
            InitializeComponent();
            Bitmap bmp = SS.Ynote.Classic.Properties.Resources.page_paste;
            this.Icon = Icon.FromHandle(bmp.GetHicon());
            this.history.Items.Add(Clipboard.GetText());
            this.Monitor.ClipboardChanged += new EventHandler<ClipboardChangedEventArgs>(Monitor_ClipboardChanged);
        }

        #endregion

        private ClipboardMonitor Monitor;
        private Engine.Controls.A1Panel Panel;
        private Engine.Controls.Button button1;
        private Engine.Controls.Button button2;

        #region Designer

        private System.Windows.Forms.ListBox history;

        private void InitializeComponent()
        {
            this.history = new System.Windows.Forms.ListBox();
            this.Panel = new SS.Ynote.Engine.Controls.A1Panel();
            this.button2 = new SS.Ynote.Engine.Controls.Button();
            this.button1 = new SS.Ynote.Engine.Controls.Button();
            this.Monitor = new SS.Ynote.Classic.ClipboardMonitor();
            this.Panel.SuspendLayout();
            this.SuspendLayout();
            // 
            // history
            // 
            this.history.Dock = System.Windows.Forms.DockStyle.Fill;
            this.history.FormattingEnabled = true;
            this.history.Location = new System.Drawing.Point(0, 0);
            this.history.Name = "history";
            this.history.Size = new System.Drawing.Size(284, 341);
            this.history.TabIndex = 0;
            this.history.DoubleClick += new System.EventHandler(this.history_DoubleClick);
            // 
            // Panel
            // 
            this.Panel.BorderColor = System.Drawing.Color.Gray;
            this.Panel.Controls.Add(this.button2);
            this.Panel.Controls.Add(this.button1);
            this.Panel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Panel.GradientEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.Panel.GradientStartColor = System.Drawing.Color.Yellow;
            this.Panel.Image = null;
            this.Panel.ImageLocation = new System.Drawing.Point(4, 4);
            this.Panel.Location = new System.Drawing.Point(0, 341);
            this.Panel.Name = "Panel";
            this.Panel.Size = new System.Drawing.Size(284, 75);
            this.Panel.TabIndex = 2;
            // 
            // button2
            // 
            this.button2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.button2.Location = new System.Drawing.Point(3, 16);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(129, 38);
            this.button2.TabIndex = 1;
            this.button2.Text = "Clear History";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.button1.Location = new System.Drawing.Point(138, 16);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(134, 38);
            this.button1.TabIndex = 0;
            this.button1.Text = "Clear Windows Clipboard Data";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Monitor
            // 
            this.Monitor.BackColor = System.Drawing.Color.Red;
            this.Monitor.Location = new System.Drawing.Point(0, 30);
            this.Monitor.Name = "Monitor";
            this.Monitor.Size = new System.Drawing.Size(284, 110);
            this.Monitor.TabIndex = 1;
            this.Monitor.Visible = false;
            // 
            // ClipboardHistory
            // 
            this.ClientSize = new System.Drawing.Size(284, 416);
            this.Controls.Add(this.Monitor);
            this.Controls.Add(this.history);
            this.Controls.Add(this.Panel);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "ClipboardHistory";
            this.Text = "Clipboard History";
            this.Panel.ResumeLayout(false);
            this.ResumeLayout(false);

        }
        #endregion

        #region Properties

        FastColoredTextBoxNS.FastColoredTextBox fctb;

       public FastColoredTextBoxNS.FastColoredTextBox TextBox {
            get { return fctb; }
            set { fctb = value; }
        }

        object[] Items = null;

        public object [] HistoryItems {
            get
            {
                return Items;
            }
            set { 
                Items = value; 
            }
        }
        #endregion

        #region Events

        private void Monitor_ClipboardChanged(object sender, ClipboardChangedEventArgs e){
            try { this.history.Items.Add(Clipboard.GetText()); }catch(Exception ex){}
        }

        private void history_DoubleClick(object sender, EventArgs e){
            try{
            this.TextBox.InsertText(this.history.SelectedItem.ToString());
        }catch(Exception ex){}
        }
        #endregion

        private void Monitor_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Clipboard.Clear();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.history.Items.Clear();
        }




    }
}
