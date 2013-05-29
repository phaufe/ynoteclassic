using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Reflection;
using SS.Ynote.Classic.Properties;
using System.Runtime.InteropServices;

namespace SS.Ynote.Classic
{
    public partial class Preferences : Form
    {
        public Preferences()
        {
            InitializeComponent();
            Bitmap bmp = Properties.Resources.application_osx_terminal;
            this.Icon = Icon.FromHandle(bmp.GetHicon());
            listBox1.SelectedIndex = 0;
            this.openfileonstartup.Checked = Settings.Default.OpenPreviousFile;
            this.txtculture.Text = Settings.Default.Culture;
            switch (Properties.Settings.Default.TabLocation)
            {
                case WeifenLuo.WinFormsUI.Docking.DocumentTabStripLocation.Top: this.tablocation.Text = "Top";
                    break;
                case WeifenLuo.WinFormsUI.Docking.DocumentTabStripLocation.Bottom: this.tablocation.Text = "Bottom";
                    break;
            }
            #region Theme
            switch (Settings.Default.Theme)
            {
                case "Default": radiodefault.Checked = true;
                    break;
                case "Dark": radiodark.Checked = true;
                    break;
                case "Comm": radiocomm.Checked = true;
                    break;
                case "Media":  radiomedia.Checked = true;
                    break;
                case "VS2010": radiovs2010.Checked = true;
                    break;
                case "OfficeXP":radioofficeXP.Checked = true;
                    break;
                case "BrowserTabBar":radiobrowsertabbar.Checked = true;
                    break;
                case "Help":radioHelp.Checked = true;
                    break;
            }
     
            #endregion

            #region Encoding
            switch (Settings.Default.Encoding)
            {
                case "ANSI": this.rbansi.Checked = true;
                    break;
                case "ASCII": this.rbascii.Checked = true;
                    break;
                case "Unicode": this.rbunicode.Checked = true;
                    break;
                case "UnicodeBigEndian": this.rbunicodebigendian.Checked = true;
                    break;
                case "UTF7": this.rbutf7.Checked = true;
                    break;
                case "UTF32": this.rbutf32.Checked = true;
                    break;
                case "UTF8": this.rbutf8.Checked = true;
                    break;
                case "UnicodeWithoutBOM": this.rbunicodewithoutbom.Checked = true;
                    break;
                case "UnicodeBigEndianWithoutBOM": this.rbunicodebigendianwithoutBOM.Checked = true;
                    break;
                case "UTF8WithoutBOM": this.rbutf8withoutbom.Checked = true;
                    break;
            }
            #endregion

            #region VisibilityOfBars

            if (Settings.Default.ToolBarVisible == true)
            {
                this.toolbarvisible.Checked = true;
            }
            else { this.toolbarvisible.Checked = false; }
            if (Settings.Default.StatusBarVisible == true)
            {
                this.statusbarvisible.Checked = true;
            }
            else { this.statusbarvisible.Checked = false; }
            #endregion

            #region DockPanel & Others

            this.txtlineinterval.Text = Settings.Default.LineInterval.ToString();
            this.txtpaddingwidth.Text = Settings.Default.PaddingWidth.ToString();
            this.ShowLineNumber.Checked = Settings.Default.LineNumbers;
            this.ShowCaret.Checked = Settings.Default.ShowCaret;
            this.ShowTextArea.Checked = Settings.Default.ShowTextArea;
            this.ShowTabIcons.Checked = Settings.Default.ShowTabIcon;

            #endregion

            #region Colors
            this.HyperlinkColor.Color = Settings.Default.HyperLinkColor;
            this.indentbackColor.Color = Settings.Default.IndentBackColor;
            this.backcolor.Color = Settings.Default.EditorBackColor;
            this.FoldingLinesColor.Color = Settings.Default.FoldingLinesColor;
            this.paddingbackcolor.Color = Settings.Default.PaddingColor;
            this.LineNumberColor.Color = Settings.Default.LineNumberColor;
            this.BookMarksColor.Color = Settings.Default.BookMarksColor;
            this.BracketHighlighttColor2.Color = Settings.Default.BracketHighlightColor2;
            this.selectioncolor.Color = Color.Blue;
            #endregion
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
        }

        private void radioButton8_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default.Theme = "Default";
        }

        private void radioButton9_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default.Theme = "Comm";
        }

        private void radioButton10_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default.Theme = "Dark";
        }

        private void radioButton11_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default.Theme = "Media";
        }

        private void radioButton12_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default.Theme = "BrowserTabBar";
        }

        private void radioButton13_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default.Theme = "OfficeXP";
        }
        protected override void OnLoad(EventArgs e)
        {
            this.Focus();
            base.OnLoad(e);
        }

        private void radioButton14_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default.Theme = "VS2010";
        }

        private void radioButton15_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default.Theme = "Help";
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (listBox1.SelectedIndex)
            {
                case 0: listBox2.Items.Clear();
                    listBox2.Items.Add(".txt");
                    listBox2.Items.Add(".log");
                    break;
                case 1: listBox2.Items.Clear(); listBox2.Items.Add(".html");
                    listBox2.Items.Add(".htm");
                    break;
                case 2: listBox2.Items.Clear(); listBox2.Items.Add(".asp");
                    listBox2.Items.Add(".aspx");
                    break;
                case 3: listBox2.Items.Clear(); listBox2.Items.Add(".php");
                    listBox2.Items.Add(".php3");
                    break;
                case 4: listBox2.Items.Clear(); listBox2.Items.Add(".js");
                    break;
                case 5: listBox2.Items.Clear(); listBox2.Items.Add(".css");
                    break;
                case 6: listBox2.Items.Clear(); listBox2.Items.Add(".xml");
                    listBox2.Items.Add(".xsd");
                    listBox2.Items.Add(".xslt");
                    break;
                case 7: listBox2.Items.Clear(); listBox2.Items.Add(".bat");
                    listBox2.Items.Add(".cmd");
                    break;
                case 8: listBox2.Items.Clear(); listBox2.Items.Add(".sql");
                    break;
                case 9: listBox2.Items.Clear(); listBox2.Items.Add(".as");
                    break;
                case 10: listBox2.Items.Clear(); listBox2.Items.Add(".c");
                    listBox2.Items.Add(".h");
                    listBox2.Items.Add(".cpp");
                    listBox2.Items.Add(".hpp");
                    listBox2.Items.Add(".cxx");
                    listBox2.Items.Add(".hxx");
                    break;
                case 11: listBox2.Items.Clear(); listBox2.Items.Add(".cs");
                    break;
                case 12: listBox2.Items.Clear(); listBox2.Items.Add(".java");
                    break;
                case 13: listBox2.Items.Clear(); listBox2.Items.Add(".lua");
                    break;
                case 14: listBox2.Items.Clear(); listBox2.Items.Add(".bas");
                    listBox2.Items.Add(".BAS");
                    break;
                case 15: listBox2.Items.Clear(); listBox2.Items.Add(".py");
                    listBox2.Items.Add(".pyw");
                    break;
                case 16: listBox2.Items.Clear(); listBox2.Items.Add(".rb");
                    listBox2.Items.Add(".rbw");
                    break;
                case 17: listBox2.Items.Clear(); listBox2.Items.Add(".vb");
                    break;

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
        void SetEncodings()
        {
            if (this.rbansi.Checked == true)
            {
                Settings.Default.Encoding = "ANSI";
            }
            else if (this.rbascii.Checked == true)
            {
                Settings.Default.Encoding = "ASCII";
            }
            else if (this.rbunicode.Checked == true)
            {
                Settings.Default.Encoding = "Unicode";
            }
            else if (this.rbunicodewithoutbom.Checked == true)
            {
                Settings.Default.Encoding = "UnicodeWithoutBOM";
            }
            else if (this.rbunicodebigendian.Checked == true)
            {
                Settings.Default.Encoding = "UnicodeBigEndian";
            }
            else if (this.rbunicodebigendianwithoutBOM.Checked == true)
            {
                Settings.Default.Encoding = "UnicodeBigEndianWithoutBOM";
            }
            else if (this.rbutf7.Checked == true)
            {
                Settings.Default.Encoding = "UTF7";
            }
            else if (this.rbutf32.Checked == true)
            {
                Settings.Default.Encoding = "UTF32";
            }
            else if (this.rbutf8.Checked == true)
            {
                Settings.Default.Encoding = "UTF8";
            }
            else if (this.rbutf8withoutbom.Checked == true)
            {
                Settings.Default.Encoding = "UTF8WithoutBOM";
            }
        }
        void SetVisibilityOfBars()
        {
            if (this.toolbarvisible.Checked == true)
            {
                Settings.Default.ToolBarVisible = true;
            }
            else { Settings.Default.ToolBarVisible = false; }
            if (this.statusbarvisible.Checked == true)
            {
                Settings.Default.StatusBarVisible = true;
            }
            else { Settings.Default.StatusBarVisible = false; }
        }
        void SetColors()
        {
            Settings.Default.HyperLinkColor = this.HyperlinkColor.Color;
            Settings.Default.IndentBackColor = this.indentbackColor.Color;
            Settings.Default.EditorBackColor = this.backcolor.Color;
            Settings.Default.FoldingLinesColor = this.FoldingLinesColor.Color;
            Settings.Default.PaddingColor = this.paddingbackcolor.Color;
            Settings.Default.LineNumberColor = this.LineNumberColor.Color;
            Settings.Default.BookMarksColor = this.BookMarksColor.Color;
            Settings.Default.BracketHighlightColor2 = this.BracketHighlighttColor2.Color;
            if (!(this.selectioncolor.Color == Color.Blue))
            {
                Settings.Default.SelectionColor = this.selectioncolor.Color;
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            SetEncodings();
            SetVisibilityOfBars();
            SetColors();
            Settings.Default.OpenPreviousFile = this.openfileonstartup.Checked;
            Settings.Default.ShowFoldingLines = this.showfoldinglines.Checked;
            Settings.Default.LineInterval = this.txtlineinterval.IntValue;
            Settings.Default.PaddingWidth = this.txtpaddingwidth.IntValue;
            Settings.Default.LineNumbers = this.ShowLineNumber.Checked;
            Settings.Default.ShowCaret = this.ShowCaret.Checked;
            Settings.Default.ShowTextArea = this.ShowTextArea.Checked;
            Settings.Default.ShowTabIcon = this.ShowTabIcons.Checked;
            Settings.Default.Culture = this.txtculture.Text;
            if (this.tablocation.SelectedIndex == 0)
            {
                Settings.Default.TabLocation = WeifenLuo.WinFormsUI.Docking.DocumentTabStripLocation.Top;
            }
            else if (this.tablocation.SelectedIndex == 1)
            {
                Settings.Default.TabLocation = WeifenLuo.WinFormsUI.Docking.DocumentTabStripLocation.Bottom;
            }
            Settings.Default.Save();
            MessageBox.Show("Changes will take place after Restart");
            Close();
        }
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
        }
        private void Preferences_Load(object sender, EventArgs e)
        {
           
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SS.Ynote.Engine.Framework.FileSystem FileManager = new SS.Ynote.Engine.Framework.FileSystem();
            string AppDataDir = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)  + "/SS/Ynote/";
            FileManager.DeleteDirectory(AppDataDir);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Settings.Default.Reset();
        }

        private void button5_Click(object sender, EventArgs e)
        {
        }

        private void button6_Click(object sender, EventArgs e)
        {
           
        }

        private void linkLabel11_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show("Please Edit AutoComplete.xml To Edit AutoComplete Menu");
        }

        private void openfileonstartup_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default.OpenPreviousFile = this.openfileonstartup.Checked;
        }
    }
}
