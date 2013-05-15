/* YnoteClassic/Main.cs
 * 
 * Copyright (C) 2013 Samarjeet Singh
 *
 */
using System;
using System.Text;
using System.IO;
using System.Drawing;
using System.Threading;
using System.Globalization;
using System.Windows.Forms;
using System.Linq;
using System.ComponentModel;
using System.Collections.Generic;
using System.Text.RegularExpressions;

using FastColoredTextBoxNS;
using WeifenLuo.WinFormsUI.Docking;

using SS.Ynote.Classic.Properties;

using SS.Web.Tools;
using SS.Web.Tools.WebSearch;
using SS.Ynote.Engine.Updater;
using SS.Ynote.Engine.Controls;
using SS.Ynote.Engine.Framework;
using SS.Ynote.Engine.Compression.Zip;
using SS.Ynote.Engine.Compression.IO;
using SS.Ynote.Engine.Framework.Plugins.Interface;
using SS.Ynote.Engine.Framework.Plugins.Utilities;
using SS.Ynote.Engine.Framework.Plugins.Controls;


namespace SS.Ynote.Classic
{
    public partial class Main : Form
    {
        #region Constructor

        //Variable Declarations

        int num = 1;
        private IDictionary<string, PluginInfo> plugins = null;
        private DeserializeDockContent m_deserializeDockContent;
        Style invisibleCharsStyle = new InvisibleCharsRenderer(Pens.Gray);
        TextStyle brownStyle = new TextStyle(Brushes.Brown, null, FontStyle.Regular);
        TextStyle XML = new TextStyle(Brushes.Red, Brushes.Yellow, FontStyle.Bold);
        TextStyle blueStyle = new TextStyle(Brushes.Blue, null, FontStyle.Regular);
        TextStyle magentaStyle = new TextStyle(Brushes.Magenta, null, FontStyle.Regular);
        TextStyle grayStyle = new TextStyle(Brushes.Gray, null, FontStyle.Regular);
        TextStyle redStyle = new TextStyle(Brushes.Red, null, FontStyle.Regular);
        TextStyle classstyle = new TextStyle(Brushes.SteelBlue, null, FontStyle.Regular);
        PluginTree pt = new PluginTree();
        ClipboardHistory CH = new ClipboardHistory();

        //Entry Point
        public Main()
        {
            InitializeComponent();
            PluginHelper.PluginsDirectory = Path.Combine(Application.StartupPath, "Plugins");
            Editor edit = new Editor();
            edit.Text = "New" + num;
            num = num + 1;
            edit.Show(dockPanel, DockState.Document);
            try
            {
                this.FormClosing += new FormClosingEventHandler(Main_FormClosing);
                ActiveEditor.codebox.TextChangedDelayed += new EventHandler<TextChangedEventArgs>(codebox_TextChangedDelayed);
                ActiveEditor.codebox.SelectionChanged += new EventHandler(codebox_SelectionChanged);
            }
            catch (System.Exception ex)
            {
            }
            ApplySkins();
        }
        public void ApplySkins()
        {
            string Theme = File.ReadAllText(@"Configurations/Theme.thm");
            if (Theme.Contains("Default"))
            {
                ToolStripManager.Renderer = new MenuRenderer(ToolbarTheme.Toolbar);
                statusStrip.Renderer = new MenuRenderer(ToolbarTheme.Toolbar);
            }
            if (Theme.Contains("Dark"))
            {
                ToolStripManager.Renderer = new DarkThemeRenderer();
                statusStrip.Renderer = new DarkThemeRenderer();
            }
            if (Theme.Contains("Media"))
            {
                ToolStripManager.Renderer = new MenuRenderer(ToolbarTheme.MediaToolbar);
                statusStrip.Renderer = new MenuRenderer(ToolbarTheme.MediaToolbar);
            }
            if (Theme.Contains("Comm"))
            {
                ToolStripManager.Renderer = new MenuRenderer(ToolbarTheme.CommunicationsToolbar);
                statusStrip.Renderer = new MenuRenderer(ToolbarTheme.CommunicationsToolbar);
            }
            if (Theme.Contains("BrowserTabBar"))
            {
                ToolStripManager.Renderer = new MenuRenderer(ToolbarTheme.BrowserTabBar);
                statusStrip.Renderer = new MenuRenderer(ToolbarTheme.BrowserTabBar);
            }
            if(Theme.Contains("OfficeXP")){
                ToolStripManager.Renderer = new OfficeXPRenderer();
                statusStrip.Renderer = new OfficeXPRenderer();
            }
            if(Theme.Contains("VS2010")){
                ToolStripManager.Renderer = new VS2010Renderer();
                statusStrip.Renderer = new VS2010Renderer();
            }
        }
        #endregion

        #region Properties

        public Editor ActiveEditor
        {
            get { return dockPanel.ActiveDocument as Editor; }
        }
        public string UserText
        {
            get { try { return ActiveEditor.codebox.Text; } catch (System.Exception ex) { return ex.Message; } }
            set { try { this.ActiveEditor.codebox.Text = value; } catch (System.Exception ex) { } }
        }
        #endregion

        #region Helper Classes

        public void CloseAllDocuments()
        {
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
        public void CloseAllButThisOne()
        {
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
        
        //Open's File
        public void OpenFile(string file, object sender, EventArgs e)
        {
            try
            {
                string ext = System.IO.Path.GetExtension(file);
                Editor edit = new Editor();
                edit.Text = Path.GetFileName(file);
                edit.Name = file;
                edit.codebox.TextChangedDelayed += new EventHandler<TextChangedEventArgs>(codebox_TextChangedDelayed);
                edit.codebox.SelectionChanged += new EventHandler(codebox_SelectionChanged);
                edit.Show(dockPanel, DockState.Document);
                edit.codebox.OpenBindingFile(file, Encoding.ASCII);
      
                //HTML 
                if (ext == ".html" | ext == ".htm" | ext == ".xhtml" | ext == ".shtml")
                {
                    mihtml_Click(sender, e);
                }
                //PHP
                if (ext == ".php")
                {
                    miphp_Click(sender, e);
                }
                //Javascript
                if (ext == ".js")
                {
                    mijs_Click(sender, e);
                }
                //ASP
                if (ext == ".aspx" | ext == ".asp")
                {
                    miasp_Click(sender, e);
                }
                //XML
                if (ext == ".xml")
                {
                    mixml_Click(sender, e);
                }
                if (ext == ".xsd")
                {
                    mixml_Click(sender, e);
                }
                //Batch
                if (ext == ".bat" | ext == ".cmd")
                {
                    mibatch_Click(sender, e);
                }
                //SQL
                if (ext == ".sql")
                {
                    misql_Click(sender, e);
                }
                //CSS
                if (ext == ".css")
                {
                    micss_Click(sender, e);
                }
                //Actionscript
                if (ext == ".as" | ext == ".AS")
                {
                    mias_Click(sender, e);
                }
                //Python
                if (ext == ".py" | ext == ".PY" | ext == ".pyw")
                {
                    mipython_Click(sender, e);
                }
                //Ruby
                if (ext == ".rb" | ext == ".ruby" | ext == ".rbw")
                {
                    miruby_Click(sender, e);
                }
                //Lua
                if (ext == ".lua" | ext == ".LUA")
                {
                    luaToolStripMenuItem_Click(sender, e);
                }
                //QBasic
                if (ext == ".bas" | ext == ".BAS")
                {
                    miqb_Click(sender, e);
                }
                //VB
                if (ext == ".vb" | ext == ".VB")
                {
                    mivb_Click(sender, e);
                }
                //C#
                if (ext == ".cs" | ext == ".CS")
                {
                    mics_Click(sender, e);
                }
                //Java
                if (ext == ".java" | ext == ".JAVA")
                {
                    mijava_Click(sender, e);
                }
                //C++
                if (ext == ".cpp" | ext == ".h" | ext == ".cxx" | ext == ".hpp" | ext == ".hxx" | ext == ".cc")
                {
                    micpp_Click(sender, e);
                }
                //C
                if (ext == ".c" | ext == ".C")
                {
                    micpp_Click(sender, e);
                }
                edit.codebox.SelectAll();
                edit.codebox.Cut();
                edit.codebox.Paste();
                edit.codebox.OnSyntaxHighlight(new TextChangedEventArgs(edit.codebox.Range));
                edit.codebox.IsChanged = false;
            }
            catch (System.Exception ex) { MessageBox.Show(ex.Message); }
        }
        #endregion


        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Editor edit = new Editor();
            edit.Text = "New" + num;
            num = num + 1;
            edit.codebox.TextChangedDelayed += new EventHandler<TextChangedEventArgs>(codebox_TextChangedDelayed);
            edit.codebox.SelectionChanged += new EventHandler(codebox_SelectionChanged);
            edit.Show(dockPanel, DockState.Document);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dockPanel.DocumentStyle == DocumentStyle.SystemMdi)
                ActiveMdiChild.Close();
            else if (dockPanel.ActiveDocument != null)
                dockPanel.ActiveDocument.DockHandler.Close();
        }

        private void closeAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CloseAllDocuments();
        }

        private void closeAllButThisToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CloseAllButThisOne();
        }
        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                ActiveEditor.codebox.Undo();
            }
            catch (System.Exception ex)
            {
            }
        }

        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                ActiveEditor.codebox.Redo();
            }
            catch (System.Exception ex)
            {
            }
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                ActiveEditor.codebox.Cut();
            }
            catch (System.Exception ex)
            {
            }
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                ActiveEditor.codebox.Copy();
            }
            catch (System.Exception ex)
            {
            }
        }


        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                ActiveEditor.codebox.Paste();
            }
            catch (System.Exception ex)
            {
            }
        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                ActiveEditor.codebox.SelectAll();
            }
            catch (System.Exception ex)
            {
            }
        }

        private void clearAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                ActiveEditor.codebox.Clear();
            }
            catch (System.Exception ex)
            {
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog o = new OpenFileDialog();
            o.Filter = "All Files(*.*)|*.*|Text Files(*.txt)|*.txt|XML Files (*.xml)|*.xml|XML Schema Definition File(*.xsd)|*.xsd|Log File (*.log)|*.log|HTML Document (*.html),(*.xhtml),(*.shtml)|*.html;*.xhtml;*.shtml|ASP.NET File(*.asp),(*.aspx)|*.asp;*.aspx|PHP Document (*.php)|*.php|Cascading Style Sheet (*.css)|*.css|Javascript File (*.js)|*.js|QBasic File(*.bas)|*.bas|Visual Basic File (*.vb)|*.vb|Python File (*.py)|*.py|Ruby File(*.ruby)|*.ruby|Lua File(*.lua)|Flash Actionscript file(*.as)|*.as|C# Source File(*.cs)|*.cs|C Source File(*.c)|C++ Source File (*.cpp)|*.cpp|C++ Header File(*.h)|*.h";
            o.ShowDialog();
            this.OpenFile(o.FileName, sender, e);
        }

        private void hTMLToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            string HTML = ActiveEditor.codebox.Html;
            SaveFileDialog s = new SaveFileDialog();
            s.Title = "Export as HTML Document";
            s.Filter = "HTML Documents (*.html)|*.html";
            s.ShowDialog();
            File.WriteAllText(s.FileName, HTML);
        }

        private void rTFToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string RTF = ActiveEditor.codebox.Rtf;
            SaveFileDialog s = new SaveFileDialog();
            s.Title = "Export as Rich Text Documents";
            s.Filter = "Rich Text Documents Documents (*.rtf)|*.rtf";
            s.ShowDialog();
            File.WriteAllText(s.FileName, RTF);
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ActiveEditor.codebox.Print(new PrintDialogSettings());
        }

        private void findToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ActiveEditor.codebox.ShowFindDialog();
        }

        private void replaceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ActiveEditor.codebox.ShowReplaceDialog();
        }

        private void goToToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ActiveEditor.codebox.ShowGoToDialog();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new About().Show();
        }


        private void foldAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ActiveEditor.codebox.CollapseAllFoldingBlocks();
        }

        private void unFoldAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ActiveEditor.codebox.ExpandAllFoldingBlocks();
        }

        private void foldSelectedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ActiveEditor.codebox.CollapseBlock(ActiveEditor.codebox.Selection.Start.iLine, ActiveEditor.codebox.Selection.End.iLine);
        }

        private void unFoldSelectedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ActiveEditor.codebox.ExpandBlock(ActiveEditor.codebox.Selection.Start.iLine, ActiveEditor.codebox.Selection.End.iLine);
        }

        private void toUpperToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                string upper = ActiveEditor.codebox.SelectedText.ToUpper();
                ActiveEditor.codebox.SelectedText = upper;
            }
            catch (System.Exception ex)
            {
            }
        }

        private void toLowerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                string lower = ActiveEditor.codebox.SelectedText.ToLower();
                ActiveEditor.codebox.SelectedText = lower;
            }
            catch (System.Exception ex)
            {
            }
        }
        private void micase_DropDownOpening(object sender, EventArgs e)
        {
            try
            {
                if (ActiveEditor.codebox.SelectedText == null)
                {
                    toLowerToolStripMenuItem.Enabled = false;
                    toUpperToolStripMenuItem.Enabled = false;
                }
                else
                {
                    toLowerToolStripMenuItem.Enabled = true;
                    toUpperToolStripMenuItem.Enabled = true;
                }
            }
            catch (System.Exception ex)
            {
            }
        }

        private void moveSelectedLinesDownToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ActiveEditor.codebox.MoveSelectedLinesDown();
        }

        private void moveSelectedLinesUpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ActiveEditor.codebox.MoveSelectedLinesUp();
        }

        private void duplicateLineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                DuplicateLine(ActiveEditor.codebox.Selection.Start.iLine, ActiveEditor.codebox);
            }
            catch (System.Exception ex) { }
        }

        private void startRecordingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                ActiveEditor.codebox.MacrosManager.IsRecording = true;
                macrostart.Enabled = false;
                macrostop.Enabled = true;
            }
            catch (System.Exception ex)
            {
            }
        }

        private void stopRecordingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                ActiveEditor.codebox.MacrosManager.IsRecording = false;
                macrostart.Enabled = true;
                macrostop.Enabled = false;
            }
            catch (System.Exception ex)
            {
            }
        }

        private void executeMacrosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                ActiveEditor.codebox.MacrosManager.ExecuteMacros();
            }
            catch (System.Exception ex)
            {
            }
        }

        private void executeMacrosMultipleTimesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Press Ctrl + E (Default Shortcut) Multiple Times to Execute \n\r Macros Multiple Times");
        }

        private void clearRecordedMacroDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                ActiveEditor.codebox.MacrosManager.ClearMacros();
            }
            catch (System.Exception ex)
            {
            }
        }

        #region Language Handlers

        private void mitext_Click(object sender, EventArgs e)
        {
            ActiveEditor.codebox.Language = Language.Custom;
            ActiveEditor.codebox.DescriptionFile = null;
            this.mitext.Checked = true;
            this.milua.Checked = false;
            this.mias.Checked = false;
            this.mibatch.Checked = false;
            this.mixml.Checked = false;
            this.mivb.Checked = false;
            this.mipython.Checked = false;
            this.miruby.Checked = false;
            this.miqb.Checked = false;
            this.mics.Checked = false;
            this.mihtml.Checked = false;
            this.miphp.Checked = false;
            this.mijs.Checked = false;
            this.mijava.Checked = false;
            this.micss.Checked = false;
            this.micpp.Checked = false;
            this.misql.Checked = false;
            this.miasp.Checked = false;
        }

        private void mibatch_Click(object sender, EventArgs e)
        {
            ActiveEditor.codebox.Language = Language.Custom;
            ActiveEditor.codebox.DescriptionFile = "Highlighters/Batch.xml";
            this.milua.Checked = false;
            this.mitext.Checked = false;
            this.mias.Checked = false;
            this.mibatch.Checked = true;
            this.mixml.Checked = false;
            this.mivb.Checked = false;
            this.mipython.Checked = false;
            this.miruby.Checked = false;
            this.miqb.Checked = false;
            this.mics.Checked = false;
            this.mihtml.Checked = false;
            this.miphp.Checked = false;
            this.mijs.Checked = false;
            this.mijava.Checked = false;
            this.micss.Checked = false;
            this.micpp.Checked = false;
            this.misql.Checked = false;
            this.miasp.Checked = false;
        }

        private void misql_Click(object sender, EventArgs e)
        {
            ActiveEditor.codebox.Language = Language.SQL;
            ActiveEditor.codebox.DescriptionFile = null;
            this.mitext.Checked = false;
            this.milua.Checked = false;
            this.mias.Checked = false;
            this.mibatch.Checked = false;
            this.mixml.Checked = false;
            this.mivb.Checked = false;
            this.mipython.Checked = false;
            this.miruby.Checked = false;
            this.miqb.Checked = false;
            this.mics.Checked = false;
            this.mihtml.Checked = false;
            this.miphp.Checked = false;
            this.mijs.Checked = false;
            this.mijava.Checked = false;
            this.micss.Checked = false;
            this.micpp.Checked = false;
            this.misql.Checked = true;
            this.miasp.Checked = false;
        }

        private void mixml_Click(object sender, EventArgs e)
        {
            ActiveEditor.codebox.Language = Language.HTML;
            ActiveEditor.codebox.DescriptionFile = null;
            this.mitext.Checked = false;
            this.mias.Checked = false;
            this.milua.Checked = false;
            this.mibatch.Checked = false;
            this.mixml.Checked = true;
            this.mivb.Checked = false;
            this.mipython.Checked = false;
            this.miruby.Checked = false;
            this.miqb.Checked = false;
            this.mics.Checked = false;
            this.mihtml.Checked = false;
            this.miphp.Checked = false;
            this.mijs.Checked = false;
            this.mijava.Checked = false;
            this.micss.Checked = false;
            this.micpp.Checked = false;
            this.misql.Checked = false;
            this.miasp.Checked = false;
        }

        private void mihtml_Click(object sender, EventArgs e)
        {
            ActiveEditor.codebox.Language = Language.HTML;
            ActiveEditor.codebox.DescriptionFile = null;
            this.mitext.Checked = false;
            this.mias.Checked = false;
            this.mibatch.Checked = false;
            this.milua.Checked = false;
            this.mixml.Checked = false;
            this.mivb.Checked = false;
            this.mipython.Checked = false;
            this.miruby.Checked = false;
            this.miqb.Checked = false;
            this.mics.Checked = false;
            this.mihtml.Checked = true;
            this.miphp.Checked = false;
            this.mijs.Checked = false;
            this.mijava.Checked = false;
            this.micss.Checked = false;
            this.micpp.Checked = false;
            this.misql.Checked = false;
            this.miasp.Checked = false;
        }

        private void micss_Click(object sender, EventArgs e)
        {
            ActiveEditor.codebox.Language = Language.Custom;
            ActiveEditor.codebox.DescriptionFile = null;
            this.mitext.Checked = false;
            this.mias.Checked = false;
            this.milua.Checked = false;
            this.mibatch.Checked = false;
            this.mixml.Checked = false;
            this.mivb.Checked = false;
            this.mipython.Checked = false;
            this.miruby.Checked = false;
            this.miqb.Checked = false;
            this.mics.Checked = false;
            this.mihtml.Checked = false;
            this.miphp.Checked = false;
            this.mijs.Checked = false;
            this.mijava.Checked = false;
            this.micss.Checked = true;
            this.micpp.Checked = false;
            this.misql.Checked = false;
            this.miasp.Checked = false;
        }

        private void miphp_Click(object sender, EventArgs e)
        {
            ActiveEditor.codebox.Language = Language.PHP;
            ActiveEditor.codebox.DescriptionFile = null;
            this.mitext.Checked = false;
            this.mias.Checked = false;
            this.mibatch.Checked = false;
            this.mixml.Checked = false;
            this.mivb.Checked = false;
            this.milua.Checked = false;
            this.mipython.Checked = false;
            this.miruby.Checked = false;
            this.miqb.Checked = false;
            this.mics.Checked = false;
            this.mihtml.Checked = false;
            this.miphp.Checked = true;
            this.mijs.Checked = false;
            this.mijava.Checked = false;
            this.micss.Checked = false;
            this.micpp.Checked = false;
            this.misql.Checked = false;
            this.miasp.Checked = false;
        }

        private void mijs_Click(object sender, EventArgs e)
        {
            ActiveEditor.codebox.Language = Language.JS;
            ActiveEditor.codebox.DescriptionFile = null;
            this.mitext.Checked = false;
            this.mias.Checked = false;
            this.mibatch.Checked = false;
            this.mixml.Checked = false;
            this.milua.Checked = false;
            this.mivb.Checked = false;
            this.mipython.Checked = false;
            this.miruby.Checked = false;
            this.miqb.Checked = false;
            this.mics.Checked = false;
            this.mihtml.Checked = false;
            this.miphp.Checked = false;
            this.mijs.Checked = true;
            this.mijava.Checked = false;
            this.micss.Checked = false;
            this.micpp.Checked = false;
            this.misql.Checked = false;
            this.miasp.Checked = false;
        }

        private void miasp_Click(object sender, EventArgs e)
        {
            ActiveEditor.codebox.Language = Language.HTML;
            //ASP Like HTML
            ActiveEditor.codebox.DescriptionFile = null;
            this.mitext.Checked = false;
            this.mias.Checked = false;
            this.mibatch.Checked = false;
            this.mixml.Checked = false;
            this.mivb.Checked = false;
            this.mipython.Checked = false;
            this.milua.Checked = false;
            this.miruby.Checked = false;
            this.miqb.Checked = false;
            this.mics.Checked = false;
            this.mihtml.Checked = false;
            this.miphp.Checked = false;
            this.mijs.Checked = false;
            this.mijava.Checked = false;
            this.micss.Checked = false;
            this.micpp.Checked = false;
            this.misql.Checked = false;
            this.miasp.Checked = true;
        }

        private void mias_Click(object sender, EventArgs e)
        {
            ActiveEditor.codebox.Language = Language.CSharp;
            ActiveEditor.codebox.DescriptionFile = null;
            this.mitext.Checked = false;
            this.mias.Checked = true;
            this.mibatch.Checked = false;
            this.mixml.Checked = false;
            this.mivb.Checked = false;
            this.mipython.Checked = false;
            this.milua.Checked = false;
            this.miruby.Checked = false;
            this.miqb.Checked = false;
            this.mics.Checked = false;
            this.mihtml.Checked = false;
            this.miphp.Checked = false;
            this.mijs.Checked = false;
            this.mijava.Checked = false;
            this.micss.Checked = false;
            this.micpp.Checked = false;
            this.misql.Checked = false;
            this.miasp.Checked = false;
        }

        private void mics_Click(object sender, EventArgs e)
        {
            ActiveEditor.codebox.Language = Language.CSharp;
            ActiveEditor.codebox.DescriptionFile = null;
            this.mitext.Checked = false;
            this.mias.Checked = false;
            this.mibatch.Checked = false;
            this.mixml.Checked = false;
            this.mivb.Checked = false;
            this.mipython.Checked = false;
            this.miruby.Checked = false;
            this.miqb.Checked = false;
            this.milua.Checked = false;
            this.mics.Checked = true;
            this.mihtml.Checked = false;
            this.miphp.Checked = false;
            this.mijs.Checked = false;
            this.mijava.Checked = false;
            this.micss.Checked = false;
            this.micpp.Checked = false;
            this.misql.Checked = false;
            this.miasp.Checked = false;
        }

        private void micpp_Click(object sender, EventArgs e)
        {
            ActiveEditor.codebox.Language = Language.CSharp;
            this.mitext.Checked = false;
            this.mias.Checked = false;
            this.mibatch.Checked = false;
            this.mixml.Checked = false;
            this.mivb.Checked = false;
            this.mipython.Checked = false;
            this.milua.Checked = false;
            this.miruby.Checked = false;
            this.miqb.Checked = false;
            this.mics.Checked = false;
            this.mihtml.Checked = false;
            this.miphp.Checked = false;
            this.mijs.Checked = false;
            this.mijava.Checked = false;
            this.micss.Checked = false;
            this.micpp.Checked = true;
            this.misql.Checked = false;
            this.miasp.Checked = false;
        }

        private void mijava_Click(object sender, EventArgs e)
        {
            ActiveEditor.codebox.Language = Language.CSharp;
            ActiveEditor.codebox.DescriptionFile = null;
            this.mitext.Checked = false;
            this.mias.Checked = false;
            this.mibatch.Checked = false;
            this.milua.Checked = false;
            this.mixml.Checked = false;
            this.mivb.Checked = false;
            this.mipython.Checked = false;
            this.miruby.Checked = false;
            this.miqb.Checked = false;
            this.mics.Checked = false;
            this.mihtml.Checked = false;
            this.miphp.Checked = false;
            this.mijs.Checked = false;
            this.mijava.Checked = true;
            this.micss.Checked = false;
            this.micpp.Checked = false;
            this.misql.Checked = false;
            this.miasp.Checked = false;
        }

        private void mipython_Click(object sender, EventArgs e)
        {
            ActiveEditor.codebox.Language = Language.Custom;
            ActiveEditor.codebox.DescriptionFile = "Highlighters/Python.xml";
            this.mitext.Checked = false;
            this.mias.Checked = false;
            this.milua.Checked = false;
            this.mibatch.Checked = false;
            this.mixml.Checked = false;
            this.mivb.Checked = false;
            this.mipython.Checked = true;
            this.miruby.Checked = false;
            this.miqb.Checked = false;
            this.mics.Checked = false;
            this.mihtml.Checked = false;
            this.miphp.Checked = false;
            this.mijs.Checked = false;
            this.mijava.Checked = false;
            this.micss.Checked = false;
            this.micpp.Checked = false;
            this.misql.Checked = false;
            this.miasp.Checked = false;
        }

        private void miqb_Click(object sender, EventArgs e)
        {
            ActiveEditor.codebox.Language = Language.Custom;
            ActiveEditor.codebox.DescriptionFile = "Highlighters/QBasic.xml";
            this.mitext.Checked = false;
            this.mias.Checked = false;
            this.mibatch.Checked = false;
            this.mixml.Checked = false;
            this.mivb.Checked = false;
            this.mipython.Checked = false;
            this.miruby.Checked = false;
            this.milua.Checked = false;
            this.miqb.Checked = true;
            this.mics.Checked = false;
            this.mihtml.Checked = false;
            this.miphp.Checked = false;
            this.mijs.Checked = false;
            this.mijava.Checked = false;
            this.micss.Checked = false;
            this.micpp.Checked = false;
            this.misql.Checked = false;
            this.miasp.Checked = false;
        }

        private void miruby_Click(object sender, EventArgs e)
        {
            ActiveEditor.codebox.Language = Language.Custom;
            ActiveEditor.codebox.DescriptionFile = "Highlighters/Ruby.xml";
            this.mitext.Checked = false;
            this.mias.Checked = false;
            this.mibatch.Checked = false;
            this.mixml.Checked = false;
            this.mivb.Checked = false;
            this.mipython.Checked = false;
            this.miruby.Checked = true;
            this.miqb.Checked = false;
            this.mics.Checked = false;
            this.mihtml.Checked = false;
            this.miphp.Checked = false;
            this.mijs.Checked = false;
            this.mijava.Checked = false;
            this.micss.Checked = false;
            this.micpp.Checked = false;
            this.misql.Checked = false;
            this.miasp.Checked = false;
        }

        private void mivb_Click(object sender, EventArgs e)
        {
            ActiveEditor.codebox.Language = Language.VB;
            ActiveEditor.codebox.DescriptionFile = null;
            this.mitext.Checked = false;
            this.mias.Checked = false;
            this.mibatch.Checked = false;
            this.mixml.Checked = false;
            this.mivb.Checked = true;
            this.mipython.Checked = false;
            this.miruby.Checked = false;
            this.miqb.Checked = false;
            this.mics.Checked = false;
            this.mihtml.Checked = false;
            this.miphp.Checked = false;
            this.mijs.Checked = false;
            this.mijava.Checked = false;
            this.micss.Checked = false;
            this.micpp.Checked = false;
            this.misql.Checked = false;
            this.miasp.Checked = false;
        }
        #endregion

        #region Text Editor Config

        private void codebox_SelectionChanged(object sender, EventArgs e)
        {
            var nCol = ActiveEditor.codebox.Selection.Start.iChar + 1;
            var nRow = ActiveEditor.codebox.Selection.Start.iLine + 1;
            var length = ActiveEditor.codebox.Text.Length;

            var selection = ActiveEditor.codebox.Selection.ToString();
            column.Text = nCol.ToString();
            line.Text = nRow.ToString();
            this.length.Text = length.ToString();
            this.selection.Text = selection;
        }
        private void codebox_TextChangedDelayed(object sender, FastColoredTextBoxNS.TextChangedEventArgs e)
        {
            HighlightInvisibleChars(e.ChangedRange);
            if (this.micss.Checked == true)
            {
                Regex StringRegex = new Regex(@"""""|@""""|''|@"".*?""|(?<!@)(?<range>"".*?[^\\]"")|'.*?[^\\]'");
                Regex CommentRegex2 = new Regex(@"(/\*.*?\*/)|(/\*.*)", RegexOptions.Singleline);
                Regex CommentRegex3 = new Regex(@"(/\*.*?\*/)|(.*\*/)", RegexOptions.Singleline | RegexOptions.RightToLeft);
                Regex NumberRegex = new Regex(@"\d+[\.]?\d*(pt|px|\%|em)?");
                Regex PropertyRegex = new Regex(@"(?<![\-\w])(animation|animation|animation-name|animation-duration|animation-timing-function|animation-delay|animation-iteration-count|animation-direction|animation-play-state|appearance|backface-visibility|background|background|background-attachment|background-color|background-image|background-position|background-repeat|background-clip|background-origin|background-size|border|border|border-bottom|border-bottom-color|border-bottom-style|border-bottom-width|border-collapse|border-color|border-left|border-left-color|border-left-style|border-left-width|border-right|border-right-color|border-right-style|border-right-width|border-spacing|border-style|border-top|border-top-color|border-top-style|border-top-width|border-width|border-bottom-left-radius|border-bottom-right-radius|border-image|border-image-outset|border-image-repeat|border-image-slice|border-image-source|border-image-width|border-radius|border-top-left-radius|border-top-right-radius|bottom|box|box-align|box-direction|box-flex|box-flex-group|box-lines|box-ordinal-group|box-orient|box-pack|box-sizing|box-shadow|caption-side|clear|clip|color|column|column-count|column-fill|column-gap|column-rule|column-rule-color|column-rule-style|column-rule-width|column-span|column-width|columns|content|counter-increment|counter-reset|cursor|direction|display|empty-cells|float|font|font|font-family|font-size|font-style|font-variant|font-weight|@font-face|font-size-adjust|font-stretch|grid-columns|grid-rows|hanging-punctuation|height|icon|@keyframes|left|letter-spacing|line-height|list-style|list-style|list-style-image|list-style-position|list-style-type|margin|margin|margin-bottom|margin-left|margin-right|margin-top|max-height|max-width|min-height|min-width|nav|nav-down|nav-index|nav-left|nav-right|nav-up|opacity|outline|outline|outline-color|outline-offset|outline-style|outline-width|overflow|overflow-x|overflow-y|padding|padding|padding-bottom|padding-left|padding-right|padding-top|page-break|page-break-after|page-break-before|page-break-inside|perspective|perspective-origin|position|punctuation-trim|quotes|resize|right|rotation|rotation-point|table-layout|target|target|target-name|target-new|target-position|text|text-align|text-decoration|text-indent|text-justify|text-outline|text-overflow|text-shadow|text-transform|text-wrap|top|transform|transform|transform-origin|transform-style|transition|transition|transition-property|transition-duration|transition-timing-function|transition-delay|vertical-align|visibility|width|white-space|word-spacing|word-break|word-wrap|z-index)(?![\-\w])", RegexOptions.IgnoreCase);
                Regex SelectorRegex = new Regex(@"\b(a|abbr|acronym|address|applet|area|article|aside|audio|b|base|basefont|bdi|bdo|big|blockquote|body|br|button|canvas|caption|center|cite|code|col|colgroup|command|datalist|dd|del|details|dfn|dialog|dir|div|dl|dt|em|embed|fieldset|figcaption|figure|font|footer|form|frame|frameset|h1|h2|h3|h4|h5|h6|head|header|hgroup|hr|html|i|iframe|img|input|ins|kbd|keygen|label|legend|li|link|map|mark|menu|meta|meter|nav|noframes|noscript|object|ol|optgroup|option|output|p|param|pre|progress|q|rp|rt|ruby|s|samp|script|section|select|small|source|span|strike|strong|style|sub|summary|sup|table|tbody|td|textarea|tfoot|th|thead|time|title|tr|track|tt|u|ul|var|video|wbr)\b|[#@\.][\w\-]+\b", RegexOptions.IgnoreCase);
                ActiveEditor.codebox.LeftBracket = '{';
                ActiveEditor.codebox.RightBracket = '}';
                ActiveEditor.codebox.LeftBracket2 = '(';
                ActiveEditor.codebox.RightBracket2 = ')';
                e.ChangedRange.ClearStyle(brownStyle, blueStyle, magentaStyle, grayStyle, redStyle);
                e.ChangedRange.SetFoldingMarkers("{", "}");
                e.ChangedRange.SetStyle(grayStyle, CommentRegex2);
                e.ChangedRange.SetStyle(grayStyle, CommentRegex3);
                e.ChangedRange.SetStyle(brownStyle, StringRegex);
                e.ChangedRange.SetStyle(magentaStyle, PropertyRegex);
                e.ChangedRange.SetStyle(blueStyle, SelectorRegex);
                e.ChangedRange.SetStyle(redStyle, NumberRegex);
            }
            if (this.miphp.Checked == true)
            {
                e.ChangedRange.ClearStyle(blueStyle);
                e.ChangedRange.ClearStyle(brownStyle);
                e.ChangedRange.SetStyle(blueStyle, "php", RegexOptions.IgnoreCase);
                e.ChangedRange.SetStyle(brownStyle, "<|>", RegexOptions.IgnoreCase);
                e.ChangedRange.SetFoldingMarkers("<", ">");

            }
            if (this.mixml.Checked == true)
            {
                e.ChangedRange.ClearStyle(blueStyle, XML);
                e.ChangedRange.SetStyle(blueStyle, "xml", RegexOptions.Multiline);
                e.ChangedRange.SetStyle(XML, "\\?>|<\\?|<|>", RegexOptions.IgnoreCase);

                e.ChangedRange.SetFoldingMarkers("<doc>", "</doc>");


            }
            if (this.mijava.Checked == true | mias.Checked == true)
            {
                e.ChangedRange.ClearStyle(blueStyle);
                e.ChangedRange.ClearStyle(classstyle);
                e.ChangedRange.SetStyle(blueStyle, "import|package|extends|implements|boolean|final|assert|instanceof|native|strictfp|super|synchronized|throws|transient", RegexOptions.IgnoreCase);
                e.ChangedRange.SetStyle(classstyle, "extends.*$", RegexOptions.Multiline);
                ActiveEditor.codebox.LeftBracket = '{';
                ActiveEditor.codebox.RightBracket = '}';
                ActiveEditor.codebox.LeftBracket2 = '(';
                ActiveEditor.codebox.RightBracket2 = ')';
            }
            if (this.micpp.Checked == true)
            {
                TextStyle Sharp = new TextStyle(Brushes.Peru, null, FontStyle.Regular);
                e.ChangedRange.ClearStyle(Sharp);
                e.ChangedRange.SetStyle(Sharp, "#.*$*", RegexOptions.Compiled | RegexOptions.Multiline);

            }
        }
        #endregion


        private void hotKeysToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                var form = new HotkeysEditorForm(ActiveEditor.codebox.HotkeysMapping);
                if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    ActiveEditor.codebox.HotkeysMapping = form.GetHotkeys();
            }
            catch (System.Exception ex) { }
        }

        private void mitoolbar_Click(object sender, EventArgs e)
        {
            toolstrip.Visible = mitoolbar.Checked;
        }

        private void mistatusbar_Click(object sender, EventArgs e)
        {
            statusStrip.Visible = mistatusbar.Checked;
        }

        private void zoomInToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try { ActiveEditor.codebox.Zoom = ActiveEditor.codebox.Zoom + 20; }
            catch (System.Exception ex) { }
        }

        private void zoomOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try { ActiveEditor.codebox.Zoom = ActiveEditor.codebox.Zoom - 20; }
            catch (System.Exception ex) { }
        }

        private void restoreDefaultZoomToolStripMenuItem_Click(object sender, EventArgs e)
        {

            try { ActiveEditor.codebox.Zoom = 100; }
            catch (System.Exception ex) { }
        }

        private void setSelectedTextAsReadonlyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try { ActiveEditor.codebox.Selection.ReadOnly = true; }
            catch (System.Exception ex) { }
        }

        private void setSelectedTextAsWritableToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try { ActiveEditor.codebox.Selection.ReadOnly = false; }
            catch (System.Exception ex) { }
        }

        private void charToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("charmap.exe");
        }

        private void hiddenCharsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {

                HighlightInvisibleChars((ActiveEditor.codebox as FastColoredTextBox).Range);
                ActiveEditor.codebox.Invalidate();
                if (hiddenCharsToolStripMenuItem.Checked == true)
                {
                    ActiveEditor.codebox.Invalidate();
                }
            }
            catch (System.Exception ex) { }

        }
        private void HighlightInvisibleChars(Range range)
        {
            if (hiddenCharsToolStripMenuItem.Checked == true)
            {
                range.ClearStyle(invisibleCharsStyle);
                range.SetStyle(invisibleCharsStyle, @".$|.\r\n|\s");
            }
        }
        public void DuplicateLine(int iLine, FastColoredTextBox fctb)
        {
            fctb.Selection.Start = new Place(0, iLine);
            fctb.Selection.Expand();
            object text = fctb.Selection.Text;
            fctb.Selection.Start = new Place(0, iLine);
            fctb.InsertText(text + "\r\n" + "\r\n");
        }

        private void increaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try { ActiveEditor.codebox.IncreaseIndent(); }
            catch (System.Exception ex) { }
        }

        private void decreaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try { ActiveEditor.codebox.DecreaseIndent(); }
            catch (System.Exception ex) { }
        }
        private void Main_FormClosing(object sender, EventArgs e){
            }
        private void dockPanel_ActiveContentChanged(object sender, EventArgs e)
        {
            try
            {
                CH.TextBox = ActiveEditor.codebox;
                if (dockPanel.Contents.Count == 0)
                {
                    this.editToolStripMenuItem.Enabled = false;
                    this.langaugeToolStripMenuItem.Enabled = false;
                    this.macrosToolStripMenuItem.Enabled = false;
                    this.zoomToolStripMenuItem.Enabled = false;
                    this.toolstrip.Enabled = false;
                }
                else
                {
                    this.editToolStripMenuItem.Enabled = true;
                    this.langaugeToolStripMenuItem.Enabled = true;
                    this.macrosToolStripMenuItem.Enabled = true;
                    this.zoomToolStripMenuItem.Enabled = true;
                    this.toolstrip.Enabled = true;
                }
            }
            catch (System.Exception ex) { }
        }

        private void newToolStripButton_Click(object sender, EventArgs e)
        {
            newToolStripMenuItem_Click(sender, e);
        }

        private void openToolStripButton_Click(object sender, EventArgs e)
        {
            openToolStripMenuItem_Click(sender, e);
        }

        private void printToolStripButton_Click(object sender, EventArgs e)
        {
            printToolStripMenuItem_Click(sender, e);
        }

        private void cutToolStripButton_Click(object sender, EventArgs e)
        {
            try { ActiveEditor.codebox.Cut(); }
            catch (System.Exception ex) { }
        }

        private void copyToolStripButton_Click(object sender, EventArgs e)
        {
            try { ActiveEditor.codebox.Copy(); }
            catch (System.Exception ex) { }
        }

        private void pasteToolStripButton_Click(object sender, EventArgs e)
        {
            try { ActiveEditor.codebox.Paste(); }
            catch (System.Exception ex) { }
        }

        private void fullScreenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.fullScreenToolStripMenuItem.Checked == true)
            {
                this.WindowState = FormWindowState.Maximized;
                this.FormBorderStyle = FormBorderStyle.None;
            }
            else
            {
                this.FormBorderStyle = FormBorderStyle.Sizable;
            }
        }

        private void runToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            RunDialog r = new RunDialog();
            r.Show();

        }

        private void openCurrentDirCommandPromptToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("cmd.exe");
        }

        private void googleSearchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GoogleSearch g = new GoogleSearch(ActiveEditor.codebox.Text, Common.SearchMethod.Startinbrowser);

        }

        private void bingSearchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BingSearch b = new BingSearch(ActiveEditor.codebox.Text, Common.SearchMethod.Startinbrowser);

        }

        private void wikipediaSearchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WikipediaSearch wiki = new WikipediaSearch(ActiveEditor.codebox.Text, Common.SearchMethod.Startinbrowser);
        }

        private void pluginSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SettingsForm form = new SettingsForm(plugins);

            if (!form.IsDisposed)
            { form.ShowDialog(); }
        }

        private void Main_Load(object sender, EventArgs e)
        {
            if (File.Exists(Settings.Default.PluginConfigFile))
            {
                AppContext.ConfigurationFile = ConfigurationFile.Load(Settings.Default.PluginConfigFile);
                LoadPlugins((from x in AppContext.ConfigurationFile.Startup.Plugins
                             select x.AssemblyPath).ToList());
            }
            else
            {
                AppContext.ConfigurationFile = new ConfigurationFile();
                AppContext.ConfigurationFile.Save(Settings.Default.PluginConfigFile);
            }
        }
        private void LoadPlugins(IEnumerable<string> assemblyPaths)
        {
            plugins = PluginHelper.GetPlugins(assemblyPaths);
            try
            {
                plugins = plugins.OrderBy(g => g.Value.Plugin.Group)
                    .ThenBy(sg => sg.Value.Plugin.SubGroup)
                    .ThenBy(t => t.Key)
                    .ToDictionary(k => k.Key, v => v.Value);
            }
            catch (NotImplementedException)
            {
                plugins = plugins.OrderBy(t => t.Key).ToDictionary(k => k.Key, v => v.Value);
            }

            Menustrip.RemovePlugins();
            pt.Tree.Nodes.Clear();

            foreach (PluginInfo pluginInfo in plugins.Values)
            {
                try
                {
                    if (pluginInfo.Plugin is IFormPlugin)
                    {
                        Menustrip.AddPlugin(pluginInfo);
                        pt.Tree.AddPlugin(pluginInfo);
                    }
                    else if (pluginInfo.Plugin is IUserControlPlugin)
                    {
                        pt.Tree.AddPlugin(pluginInfo);
                    }
                }
                catch (System.Exception ex)
                {//Do Nothing
                }
            }
        }

        private void pluginSettingsToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            try
            {
                SettingsForm form = new SettingsForm(plugins);
                if (!form.IsDisposed)
                    form.ShowDialog();
            }
            catch (System.Exception ex) { }
        }

        private void defaultToolStripMenuItem_Click(object sender, EventArgs e)
        {
            File.WriteAllText(@"Configurations/Theme.thm", "Default");
        }

        private void darkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            File.WriteAllText(@"Configurations/Theme.thm", "Dark");
        }

        private void communicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            File.WriteAllText(@"Configurations/Theme.thm", "Comm");
        }

        private void mediaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            File.WriteAllText(@"Configurations/Theme.thm", "Media");
        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ActiveEditor.codebox.Bookmarks.Add(ActiveEditor.codebox.Selection.Start.iLine);
        }

        private void removeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ActiveEditor.codebox.Bookmarks.Remove(ActiveEditor.codebox.Selection.Start.iLine);
        }

        private void luaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ActiveEditor.codebox.Language = Language.Custom;
            ActiveEditor.codebox.DescriptionFile = @"Highlighters/Lua.xml";
            this.mitext.Checked = false;
            this.milua.Checked = true;
            this.mias.Checked = false;
            this.mibatch.Checked = false;
            this.mixml.Checked = false;
            this.mivb.Checked = false;
            this.mipython.Checked = false;
            this.miruby.Checked = false;
            this.miqb.Checked = false;
            this.mics.Checked = false;
            this.mihtml.Checked = false;
            this.miphp.Checked = false;
            this.mijs.Checked = false;
            this.mijava.Checked = false;
            this.micss.Checked = false;
            this.micpp.Checked = false;
            this.misql.Checked = false;
            this.miasp.Checked = false;
        }

        private void clipboardHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {

            CH.TextBox = ActiveEditor.codebox;

            CH.Show(dockPanel, DockState.DockRight);

        }

        private void launchInBrowserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string extension = null;
            if (this.mihtml.Checked == true) { extension = ".html:="; }
            else if (this.miphp.Checked == true) { extension = ".php"; }
            else if (this.mijs.Checked == true) { extension = ".js"; }
            else if (this.milua.Checked == true) { extension = ".lua"; }
            else if (this.mipython.Checked == true) { extension = ".py"; }
            else if (this.mixml.Checked == true) { extension = ".xml"; }
            else if (this.micss.Checked == true) { extension = ".css"; }
            else if (this.miasp.Checked == true) { extension = ".aspx"; }
            else if (this.micpp.Checked == true) { extension = ".cpp"; }
            else if (this.mics.Checked == true) { extension = ".cs"; }
            else if (this.miqb.Checked == true) { extension = ".bas"; }
            else if (this.miruby.Checked == true) { extension = ".rb"; }
            else if (this.mivb.Checked == true) { extension = ".vb"; }
            else if (this.mias.Checked == true) { extension = ".as"; }
            else if (this.mibatch.Checked == true) { extension = ".bat"; }
            else if (this.mijava.Checked == true) { extension = ".java"; }
            else if (this.misql.Checked == true) { extension = ".as"; }
            string AppDataDir = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/SS/Ynote/";
            System.IO.File.WriteAllText(AppDataDir + "file" + extension, this.ActiveEditor.codebox.Text);
            System.Diagnostics.Process.Start(AppDataDir + "file" + extension);

        }

        private void launToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string AppDataDir = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/SS/Ynote/";
            System.IO.File.WriteAllText(AppDataDir + "file" + ".html", this.ActiveEditor.codebox.Text);
            System.Diagnostics.Process.Start("iexplore.exe", AppDataDir + "file.html");
        }

        private void launchInChromeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string AppDataDir = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/SS/Ynote/";
            System.IO.File.WriteAllText(AppDataDir + "file" + ".html", this.ActiveEditor.codebox.Text);
            System.Diagnostics.Process.Start("chrome.exe", AppDataDir + "file.html");
        }

        private void launchInFirefoxToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string AppDataDir = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/SS/Ynote/";
            System.IO.File.WriteAllText(AppDataDir + "file" + ".html", this.ActiveEditor.codebox.Text);
            System.Diagnostics.Process.Start("firefox.exe", AppDataDir + "file.html");
        }

        private void launchInCommandPromptToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string AppDataDir = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/SS/Ynote/";
            System.IO.File.WriteAllText(AppDataDir + "file.", this.ActiveEditor.codebox.Text);
            System.Diagnostics.Process.Start("cmd.exe", AppDataDir + "file.cmd");
        }

        private void visitProjectPageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://ynoteclassic.codeplex.com");
        }

        private void chooseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AvailablePluginsForm form = new AvailablePluginsForm();
            if (form.ShowDialog() == DialogResult.OK)
            {
                LoadPlugins(form.SelectedPlugins.Values);
            }
        }

        private void settingsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            SettingsForm form = new SettingsForm(plugins);
            if (!form.IsDisposed)
            { form.ShowDialog(); }
        }

        private void pluginTreeToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void browserTabBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            File.WriteAllText(@"Configurations/Theme.thm", "BrowserTabBar");
        }

        private void officeXPToolStripMenuItem_Click(object sender, EventArgs e)
        {
            File.WriteAllText(@"Configurations/Theme.thm", "OfficeXP");
        }

        private void vS2010ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            File.WriteAllText(@"Configurations/Theme.thm", "VS2010");
        }

        private void newToolStripButton1_Click(object sender, EventArgs e)
        {
            newToolStripButton_Click(sender, e);
        }

        private void openToolStripButton1_Click(object sender, EventArgs e)
        {
            openToolStripButton_Click(sender, e);
        }

        private void saveToolStripButton1_Click(object sender, EventArgs e)
        {
            saveToolStripMenuItem_Click(sender, e);
        }

        private void printToolStripButton1_Click(object sender, EventArgs e)
        {
            printToolStripMenuItem_Click(sender, e);
        }

        private void cutToolStripButton1_Click(object sender, EventArgs e)
        {
            cutToolStripButton_Click(sender, e);
        }

        private void copyToolStripButton1_Click(object sender, EventArgs e)
        {
            copyToolStripButton_Click(sender, e);
        }

        private void fromArchiveToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void reloadFromDiskToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try { ActiveEditor.codebox.Text = File.ReadAllText(ActiveEditor.Name); }
            catch (System.Exception ex) { }
        }

        private void pasteToolStripButton1_Click(object sender, EventArgs e)
        {
            pasteToolStripMenuItem_Click(sender, e);
        }
    }
}
