namespace SS.Ynote.Classic
{
       #region Using Directives
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Linq;
    using System.Text;
    using System.Xml;
    using System.Diagnostics;
    using System.Windows.Forms;
    using WeifenLuo.WinFormsUI.Docking;
    using FastColoredTextBoxNS;
    using System.Text.RegularExpressions;
    using System.IO;
    #endregion

    public partial class Editor : WeifenLuo.WinFormsUI.Docking.DockContent
    {
        #region Constants
        UTF8Encoding UTF8WithoutBOM = new UTF8Encoding(false);
        UnicodeEncoding UnicodeWithoutBOM = new UnicodeEncoding(false, false);
        UnicodeEncoding UnicodeBigEndianWithoutBOM = new UnicodeEncoding(true, false);
        List<AutocompleteItem> menuItemList = new List<AutocompleteItem>();
        TextStyle bluestyle = new TextStyle(Brushes.Blue,null,FontStyle.Regular);
        TextStyle greenstyle = new TextStyle(Brushes.Green, null, FontStyle.Regular);
        TextStyle redstyle = new TextStyle(Brushes.Red, null, FontStyle.Regular);
        static SolidBrush bracket = new SolidBrush(Color.FromArgb(30, 0, 0, 255));
        static SolidBrush HyperlinkBrush = new SolidBrush(Properties.Settings.Default.HyperLinkColor);
        Timer t = new Timer();
        FastColoredTextBoxNS.AutocompleteMenu menu;
        #endregion

        #region Constructor

        public Editor()
        {
            InitializeComponent();
            BuildAutocomplete();
            Bitmap bmp = SS.Ynote.Classic.Properties.Resources.script_code;
            this.Icon = Icon.FromHandle(bmp.GetHicon());
            this.codebox.BracketsStyle = new MarkerStyle(bracket);
            Color c;
            c = Color.FromArgb(80, Properties.Settings.Default.BracketHighlightColor2);
            SolidBrush bracket2 = new SolidBrush(c);
            this.codebox.BracketsStyle2 = new MarkerStyle(bracket2);
            this.codebox.CaretVisible = Properties.Settings.Default.ShowCaret;
            if (Properties.Settings.Default.ShowTextArea == true)
            {
                this.codebox.TextAreaBorder = TextAreaBorderType.Single;
            }
            else
            {
                this.codebox.TextAreaBorder = TextAreaBorderType.None;
            }
            this.codebox.LineInterval = Properties.Settings.Default.LineInterval;
            this.codebox.ShowLineNumbers = Properties.Settings.Default.LineNumbers;
            this.codebox.BookmarkColor = Properties.Settings.Default.BookMarksColor;
            this.codebox.SelectionChangedDelayed += new EventHandler(codebox_SelectionChangedDelayed);
            //Define Events
            codebox.MouseMove += new MouseEventHandler(codebox_MouseMove);
            codebox.MouseDown += new MouseEventHandler(codebox_MouseDown);
        }
        public FastColoredTextBoxNS.AutocompleteMenu AutoCompleteMenu
        {
            get { return menu; }
        }
        #region Stings
        string[] snippets = { "if(^)\n{\n;\n}", "if(^)\n{\n;\n}\nelse\n{\n;\n}", "for(^;;)\n{\n;\n}", "while(^)\n{\n;\n}", "do${\n^;\n}while();", "switch(^)\n{\ncase : break;\n}" };
        string[] declarationSnippets = { 
               "public class ^\n{\n}", "private class ^\n{\n}", "internal class ^\n{\n}",
               "public struct ^\n{\n;\n}", "private struct ^\n{\n;\n}", "internal struct ^\n{\n;\n}",
               "public void ^()\n{\n;\n}", "private void ^()\n{\n;\n}", "internal void ^()\n{\n;\n}", "protected void ^()\n{\n;\n}",
               "public ^{ get; set; }", "private ^{ get; set; }", "internal ^{ get; set; }", "protected ^{ get; set; }"
               };
        #endregion
        private void BuildAutocomplete()
        {
           menu = new AutocompleteMenu(codebox);
   
            menu.Items.SetAutocompleteItems(menuItemList);
            menu.SearchPattern = "[\\w\\.:=!<>]";
            menu.AllowTabKey = true;
            foreach (var item in declarationSnippets)
                menuItemList.Add(new DeclarationSnippet(item));
            foreach (var item in snippets)
                menuItemList.Add(new SnippetAutocompleteItem(item));
            var xmldoc = new XmlDocument();
            xmldoc.Load(@"Configurations\Autocomplete.xml");
            foreach (XmlNode item in xmldoc.SelectNodes("*/autocomplete/item"))
                menuItemList.Add(new AutocompleteItem(item.InnerText));
        }
        #endregion

        #region Events
        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.codebox.Cut();
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.codebox.Copy();
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.codebox.Undo();
        }

        private void pToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.codebox.Paste();
        }

        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.codebox.Redo();
        }
        private void Editor_Closing(object sender, FormClosingEventArgs e)
        {
            if (this.codebox.IsChanged == true)
            {
                DialogResult dialogResult = MessageBox.Show("Save Changes to " + this.Text + "?", "Save", MessageBoxButtons.YesNoCancel);
                if (dialogResult == DialogResult.Yes)
                {
                    SaveAs(e);
      
                }
                else if (dialogResult == DialogResult.No)
                {
                }
                else if (dialogResult == DialogResult.Cancel)
                {
                    e.Cancel = true;
                }
                
            }
            
        }
        public void SaveAs(FormClosingEventArgs e)
        {
            if (!(this.Name == "Editor"))
            {
                switch (SS.Ynote.Classic.Properties.Settings.Default.Encoding)
                {
                    case "ANSI": File.WriteAllText(this.Name, this.codebox.Text, Encoding.Default);
                        break;
                    case "ASCII": File.WriteAllText(this.Name, this.codebox.Text, Encoding.ASCII);
                        break;
                    case "Unicode": File.WriteAllText(this.Name, this.codebox.Text, Encoding.Unicode);
                        break;
                    case "UnicodeBigEndian": File.WriteAllText(this.Name, this.codebox.Text, Encoding.BigEndianUnicode);
                        break;
                    case "UTF7": File.WriteAllText(this.Name, this.codebox.Text, Encoding.UTF7);
                        break;
                    case "UTF32": File.WriteAllText(this.Name, this.codebox.Text, Encoding.UTF32);
                        break;
                    case "UTF8": File.WriteAllText(this.Name, this.codebox.Text, Encoding.UTF8);
                        break;
                    case "UnicodeWithoutBOM": File.WriteAllText(this.Name, this.codebox.Text, UnicodeWithoutBOM);
                        break;
                    case "UnicodeBigEndianWithoutBOM": File.WriteAllText(this.Name, this.codebox.Text, UnicodeBigEndianWithoutBOM);
                        break;
                    case "UTF8WithoutBOM": File.WriteAllText(this.Name, this.codebox.Text, UTF8WithoutBOM);
                        break;
                }
            }
            else
            {
                SaveFileDialog s = new SaveFileDialog();
                s.Title = "Save As..";
                s.Filter = "All Files(*.*)|*.*|Text Files(*.txt)|*.txt|XML Files (*.xml)|*.xml|XML Schema Definition File(*.xsd)|*.xsd|Log File (*.log)|*.log|HTML Document (*.html),(*.xhtml),(*.shtml)|*.html;*.xhtml;*.shtml|ASP.NET File(*.asp),(*.aspx)|*.asp;*.aspx|PHP Document (*.php)|*.php|Cascading Style Sheet (*.css)|*.css|Javascript File (*.js)|*.js|QBasic File(*.bas)|*.bas|Visual Basic File (*.vb)|*.vb|Python File (*.py)|*.py|Ruby File(*.ruby)|*.ruby|Lua File(*.lua)|Flash Actionscript file(*.as)|*.as|C# Source File(*.cs)|*.cs|C Source File(*.c)|C++ Source File (*.cpp)|*.cpp|C++ Header File(*.h)|*.h";
                s.ShowDialog();
                if (!(string.IsNullOrEmpty(s.FileName)))
                {
                    switch (SS.Ynote.Classic.Properties.Settings.Default.Encoding)
                    {
                        case "ANSI": File.WriteAllText(s.FileName, this.codebox.Text, Encoding.Default);
                            break;
                        case "ASCII": File.WriteAllText(s.FileName, this.codebox.Text, Encoding.ASCII);
                            break;
                        case "Unicode": File.WriteAllText(s.FileName, this.codebox.Text, Encoding.Unicode);
                            break;
                        case "UnicodeBigEndian": File.WriteAllText(s.FileName, this.codebox.Text, Encoding.BigEndianUnicode);
                            break;
                        case "UTF7": File.WriteAllText(s.FileName, this.codebox.Text, Encoding.UTF7);
                            break;
                        case "UTF32": File.WriteAllText(s.FileName, this.codebox.Text, Encoding.UTF32);
                            break;
                        case "UTF8": File.WriteAllText(s.FileName, this.codebox.Text, Encoding.UTF8);
                            break;
                        case "UnicodeWithoutBOM": File.WriteAllText(s.FileName, this.codebox.Text, UnicodeWithoutBOM);
                            break;
                        case "UnicodeBigEndianWithoutBOM": File.WriteAllText(s.FileName, this.codebox.Text, UnicodeBigEndianWithoutBOM);
                            break;
                        case "UTF8WithoutBOM": File.WriteAllText(s.FileName, this.codebox.Text, UTF8WithoutBOM);
                            break;
                    }
                }
                else
                {
                    e.Cancel = true;
                }
            }
        }
        #endregion

        #region Hyperlink
        TextStyle hyperlink = new TextStyle(HyperlinkBrush, null, FontStyle.Underline);
        bool CharIsHyperlink(Place place)
        {
            var mask = codebox.GetStyleIndexMask(new Style[] { hyperlink });
            if (place.iChar < codebox.GetLineLength(place.iLine))
                if ((codebox[place].style & mask) != 0)
                    return true;

            return false;
        }

        private void codebox_MouseMove(object sender, MouseEventArgs e)
        {
            var p = codebox.PointToPlace(e.Location);
            if (CharIsHyperlink(p))
   
                codebox.Cursor = Cursors.Hand;
            else
               codebox.Cursor = Cursors.IBeam;
        }

        private void codebox_MouseDown(object sender, MouseEventArgs e)
        {
            var p = codebox.PointToPlace(e.Location);
            if (CharIsHyperlink(p))
            {
                try
                {
                    var url = codebox.GetRange(p, p).GetFragment(@"[\S]").Text;
                    Process.Start(url);
                }

                catch (Exception ex) { Console.WriteLine(ex.Message); }
            }
        }

        #endregion

        #region Dock/Context/Text

        private void fctb_textchangeddelayed(object sender, FastColoredTextBoxNS.TextChangedEventArgs e)
        {
            e.ChangedRange.ClearStyle(hyperlink);
            e.ChangedRange.SetStyle(hyperlink, @"(http|ftp|https):\/\/[\w\-_]+(\.[\w\-_]+)+([\w\-\.,@?^=%&amp;:/~\+#]*[\w\-\@?^=%&amp;/~\+#])?");
        }
        protected MarkerStyle SameWordsStyle = new MarkerStyle(new SolidBrush(Color.FromArgb(40, Color.Gray)));
        private void codebox_SelectionChangedDelayed(object sender, EventArgs e)
        {
            codebox.VisibleRange.ClearStyle(SameWordsStyle);

            if (!codebox.Selection.IsEmpty)
                return;//user selected diapason

            //get fragment around caret
            var fragment = codebox.Selection.GetFragment(@"\w");
            string text = fragment.Text;
            if (text.Length == 0)
                return;
            //highlight same words
            var ranges = codebox.VisibleRange.GetRanges("\\b" + text + "\\b").ToArray();
            if (ranges.Length > 1)
                foreach (var r in ranges)
                    r.SetStyle(SameWordsStyle);
        }
        private void closeAllButThisToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var dockPanel = this.DockPanel;
            if (dockPanel.DocumentStyle == DocumentStyle.SystemMdi)
            {
                Form activeMdi = ActiveMdiChild;
                foreach (Form form in MdiChildren)
                {
                    if (form != activeMdi)
                        form.Close();
                }
            }
            else
            {
                foreach (IDockContent document in dockPanel.DocumentsToArray())
                {
                    if (!document.DockHandler.IsActivated)
                        document.DockHandler.Close();
                }
            }
        }

        private void closeAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var dockPanel = this.DockPanel;
            if (dockPanel.DocumentStyle == DocumentStyle.SystemMdi)
            {
                foreach (Form form in MdiChildren)
                    form.Close();
            }
            else
            {
                IDockContent[] documents = dockPanel.DocumentsToArray();
                foreach (IDockContent content in documents)
                    content.DockHandler.Close();
            }
        }

        private void copyFileNameToClipboardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(this.Text);
        }

        private void copyFilePathToClipboardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(this.Name);
        }

        private void openContainingFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
           string dir= System.IO.Path.GetDirectoryName(this.Name);
           Process.Start(dir);
        }

        private void autoCompleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.AutoCompleteMenu.Show(true);
        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.codebox.SelectAll();
        }

        private void foldSelectedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.codebox.CollapseBlock(codebox.Selection.Start.iLine, codebox.Selection.End.iLine);
        }

        private void unFoldSelectedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.codebox.ExpandBlock(codebox.Selection.Start.iLine, codebox.Selection.End.iLine);
        }
        private void findToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.codebox.ShowFindDialog();
        }

        private void replaceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.codebox.ShowReplaceDialog();
        }

        private void goToToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.codebox.ShowGoToDialog();
        }

        private void usingDefaultToolStripMenuItem_Click(object sender, EventArgs e)
        {
            codebox[codebox.Selection.Start.iLine].BackgroundBrush = bracket;
        }

        private void usingRedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Color red = Color.FromArgb(50, Color.Red);
            SolidBrush redbrush = new SolidBrush(red);
            codebox[codebox.Selection.Start.iLine].BackgroundBrush = redbrush;
        }

        private void usingYellowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Color red = Color.FromArgb(50, Color.Yellow);
            SolidBrush redbrush = new SolidBrush(red);
            codebox[codebox.Selection.Start.iLine].BackgroundBrush = redbrush;
        }

        private void usingGreenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Color red = Color.FromArgb(50, Color.Green);
            SolidBrush redbrush = new SolidBrush(red);
            codebox[codebox.Selection.Start.iLine].BackgroundBrush = redbrush;
        }

        private void usingGrayToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Color red = Color.FromArgb(50, Color.Gray);
            SolidBrush redbrush = new SolidBrush(red);
            codebox[codebox.Selection.Start.iLine].BackgroundBrush = redbrush;
        }

        private void unmarkLineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            codebox[codebox.Selection.Start.iLine].BackgroundBrush = null;
        }
        #endregion

        private void usingYellowStyleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MarkerStyle YellowStyle = new MarkerStyle(new SolidBrush(Color.FromArgb(180, Color.Yellow)));
            codebox.Selection.SetStyle(YellowStyle);
        }

        private void usingRedStyleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MarkerStyle YellowStyle = new MarkerStyle(new SolidBrush(Color.FromArgb(50, Color.Red)));
            codebox.Selection.SetStyle(YellowStyle);
        }

        private void usingGreenStyleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MarkerStyle YellowStyle = new MarkerStyle(new SolidBrush(Color.FromArgb(50, Color.Green)));
            codebox.Selection.SetStyle(YellowStyle);
        }

        private void usingGrayStyleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MarkerStyle YellowStyle = new MarkerStyle(new SolidBrush(Color.FromArgb(50, Color.Gray)));
            codebox.Selection.SetStyle(YellowStyle);
        }

        private void usingBlueStyleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MarkerStyle YellowStyle = new MarkerStyle(new SolidBrush(Color.FromArgb(50, Color.Blue)));
            codebox.Selection.SetStyle(YellowStyle);
        }

        private void unmarkSelectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            codebox[codebox.Selection.Start.iLine].BackgroundBrush = null;
        }
    }
        #region Declaration Snippet
    class DeclarationSnippet : SnippetAutocompleteItem
    {
        public DeclarationSnippet(string snippet)
            : base(snippet)
        {
        }

        public override CompareResult Compare(string fragmentText)
        {
            var pattern = Regex.Escape(fragmentText);
            if (Regex.IsMatch(Text, "\\b" + pattern, RegexOptions.IgnoreCase))
                return CompareResult.Visible;
            return CompareResult.Hidden;
        }
    }
    #endregion

}
