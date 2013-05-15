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


namespace SS.Ynote.Classic
{
    public partial class Editor : WeifenLuo.WinFormsUI.Docking.DockContent
    {
        List<AutocompleteItem> menuItemList = new List<AutocompleteItem>();
        TextStyle bluestyle = new TextStyle(Brushes.Blue,null,FontStyle.Regular);
        TextStyle greenstyle = new TextStyle(Brushes.Green, null, FontStyle.Regular);
        TextStyle hyperlink = new TextStyle(Brushes.Blue, null, FontStyle.Underline);
        TextStyle redstyle = new TextStyle(Brushes.Red, null, FontStyle.Regular);
        Timer t = new Timer();
       

        public Editor()
        {
            InitializeComponent();
            BuildAutocomplete();
            Bitmap bmp = SS.Ynote.Classic.Properties.Resources.script_code;
            this.Icon = Icon.FromHandle(bmp.GetHicon());
            //Define Events
            codebox.MouseMove += new MouseEventHandler(codebox_MouseMove);
            codebox.MouseDown += new MouseEventHandler(codebox_MouseDown);
            codebox.Click += new EventHandler(codebox_Click);
            codebox.MouseClick += new MouseEventHandler(codebox_MouseClick);
            //Timer
            t.Enabled = true;
            t.Interval = 1000;
            t.Tick += new EventHandler(t_Tick);
        }

        private void BuildAutocomplete()
        {
            var menu = new AutocompleteMenu(codebox);
   
            menu.Items.SetAutocompleteItems(menuItemList);
            var xmldoc = new XmlDocument();
            xmldoc.Load(@"Configurations\Autocomplete.xml");
            foreach (XmlNode item in xmldoc.SelectNodes("*/autocomplete/item"))
                menuItemList.Add(new AutocompleteItem(item.InnerText));
        }
        private void codebox_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.X < codebox.LeftIndent)
            {
                var place = codebox.PointToPlace(e.Location);
                if (codebox.Bookmarks.Contains(place.iLine))
                    codebox.Bookmarks.Remove(place.iLine);
                else
                    codebox.Bookmarks.Add(place.iLine);
            }
        }
        private void t_Tick(object sender, EventArgs e)
        {
            var range = this.codebox.Range;
            range.ClearStyle(sameWordsStyle);
            t.Stop();
        }
        private void codebox_Click(object sender, EventArgs e)
        {
            if (!this.codebox.Selection.IsEmpty)
                return;

            var fragment = this.codebox.Selection.GetFragment(@"\w");
            string text = fragment.Text;
            if (text.Length == 0)
                return;

            //highlight same words
            var ranges = this.codebox.VisibleRange.GetRanges(@"\b" + text + @"\b");
            foreach (var range in ranges)
                range.SetStyle(sameWordsStyle);
            t.Start();
        }
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
                    SaveAs();
                    e.Cancel = true;
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
        public void SaveAs()
        {
            SaveFileDialog s = new SaveFileDialog();
            s.Filter = "All Files(*.*)|*.*";
            s.ShowDialog();
            this.codebox.SaveToFile(s.FileName, Encoding.ASCII);
        }

        #region Hyperlink

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
                
                catch (Exception ex) { }
            }
        }

        #endregion

        private void fctb_textchangeddelayed(object sender, FastColoredTextBoxNS.TextChangedEventArgs e)
        {
            e.ChangedRange.ClearStyle(hyperlink);
            e.ChangedRange.SetStyle(hyperlink, @"(http|ftp|https|ftps):\/\/[\w\-_]+(\.[\w\-_]+)+([\w\-\.,@?^=%&amp;:/~\+#]*[\w\-\@?^=%&amp;/~\+#])?");
        }
        protected MarkerStyle sameWordsStyle = new MarkerStyle(new SolidBrush(Color.FromArgb(40, Color.Gray)));
    }

}
