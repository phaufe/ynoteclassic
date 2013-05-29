//Ynote Classic Main.cs 
//Copyright (C) 2013 Samarjeet Singh

namespace SS.Ynote.Classic
{
       #region Using Directives

    using System;
    using System.Text;
    using System.IO;
    using System.Drawing;
    using System.Threading;
    using System.Globalization;
    using System.Windows.Forms;
    using System.Linq;
    using System.Xml;
    using System.ComponentModel;
    using System.Collections.Generic;
    using System.Text.RegularExpressions;
    using FastColoredTextBoxNS;
    using WeifenLuo.WinFormsUI.Docking;
    using SS.Ynote.Classic.Properties;
    using SS.Web.Tools;
    using SS.Web.Tools.WebSearch;
    using SS.Ynote.Engine;
    using SS.Ynote.Engine.Updater;
    using SS.Ynote.Engine.Controls;
    using SS.Ynote.Engine.Framework;
    using SS.Ynote.Engine.Framework.Plugins.Interface;
    using SS.Ynote.Engine.Framework.Plugins.Utilities;
    using SS.Ynote.Engine.Framework.Plugins.Controls;
    #endregion

    public partial class Main : Form, IHost
    {
        #region Constructor

        //Variable Declarations
        int num = 1;
        private IDictionary<string, PluginInfo> plugins = null;
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
        UTF8Encoding UTF8WithoutBOM = new UTF8Encoding(false);
        UnicodeEncoding UnicodeWithoutBOM = new UnicodeEncoding(false, false);
        UnicodeEncoding UnicodeBigEndianWithoutBOM = new UnicodeEncoding(true, false);
        void InitailzeDock()
        {
            WeifenLuo.WinFormsUI.Docking.DockPanelSkin dockPanelSkin1 = new WeifenLuo.WinFormsUI.Docking.DockPanelSkin();
            WeifenLuo.WinFormsUI.Docking.AutoHideStripSkin autoHideStripSkin1 = new WeifenLuo.WinFormsUI.Docking.AutoHideStripSkin();
            WeifenLuo.WinFormsUI.Docking.DockPanelGradient dockPanelGradient1 = new WeifenLuo.WinFormsUI.Docking.DockPanelGradient();
            WeifenLuo.WinFormsUI.Docking.TabGradient tabGradient1 = new WeifenLuo.WinFormsUI.Docking.TabGradient();
            WeifenLuo.WinFormsUI.Docking.DockPaneStripSkin dockPaneStripSkin1 = new WeifenLuo.WinFormsUI.Docking.DockPaneStripSkin();
            WeifenLuo.WinFormsUI.Docking.DockPaneStripGradient dockPaneStripGradient1 = new WeifenLuo.WinFormsUI.Docking.DockPaneStripGradient();
            WeifenLuo.WinFormsUI.Docking.TabGradient tabGradient2 = new WeifenLuo.WinFormsUI.Docking.TabGradient();
            WeifenLuo.WinFormsUI.Docking.DockPanelGradient dockPanelGradient2 = new WeifenLuo.WinFormsUI.Docking.DockPanelGradient();
            WeifenLuo.WinFormsUI.Docking.TabGradient tabGradient3 = new WeifenLuo.WinFormsUI.Docking.TabGradient();
            WeifenLuo.WinFormsUI.Docking.DockPaneStripToolWindowGradient dockPaneStripToolWindowGradient1 = new WeifenLuo.WinFormsUI.Docking.DockPaneStripToolWindowGradient();
            WeifenLuo.WinFormsUI.Docking.TabGradient tabGradient4 = new WeifenLuo.WinFormsUI.Docking.TabGradient();
            WeifenLuo.WinFormsUI.Docking.TabGradient tabGradient5 = new WeifenLuo.WinFormsUI.Docking.TabGradient();
            WeifenLuo.WinFormsUI.Docking.DockPanelGradient dockPanelGradient3 = new WeifenLuo.WinFormsUI.Docking.DockPanelGradient();
            WeifenLuo.WinFormsUI.Docking.TabGradient tabGradient6 = new WeifenLuo.WinFormsUI.Docking.TabGradient();
            WeifenLuo.WinFormsUI.Docking.TabGradient tabGradient7 = new WeifenLuo.WinFormsUI.Docking.TabGradient();
            // 
            // dockPanel
            // 
            dockPanel.AccessibleDescription = null;
            dockPanel.AccessibleName = null;
            dockPanel.BackColor = System.Drawing.Color.Silver;
            dockPanel.BackgroundImage = null;
            dockPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dockPanel.DefaultFloatWindowSize = new System.Drawing.Size(500, 400);
            dockPanel.DocumentTabStripLocation = global::SS.Ynote.Classic.Properties.Settings.Default.TabLocation;
            dockPanel.Font = null;
            dockPanel.Dock = DockStyle.Fill;
            dockPanel.Name = "dockPanel"; 
            dockPanel.ShowDocumentIcon = global::SS.Ynote.Classic.Properties.Settings.Default.ShowTabIcon;
            dockPanelGradient1.EndColor = System.Drawing.SystemColors.ControlLight;
            dockPanelGradient1.StartColor = System.Drawing.SystemColors.ControlLight;
            autoHideStripSkin1.DockStripGradient = dockPanelGradient1;
            tabGradient1.EndColor = System.Drawing.SystemColors.Control;
            tabGradient1.StartColor = System.Drawing.SystemColors.Control;
            tabGradient1.TextColor = System.Drawing.SystemColors.ControlDarkDark;
            autoHideStripSkin1.TabGradient = tabGradient1;
            autoHideStripSkin1.TextFont = new System.Drawing.Font("Segoe UI", 9F);
            dockPanelSkin1.AutoHideStripSkin = autoHideStripSkin1;
            tabGradient2.EndColor = System.Drawing.SystemColors.ControlLightLight;
            tabGradient2.StartColor = System.Drawing.SystemColors.ControlLightLight;
            tabGradient2.TextColor = System.Drawing.SystemColors.ControlText;
            dockPaneStripGradient1.ActiveTabGradient = tabGradient2;
            dockPanelGradient2.EndColor = System.Drawing.SystemColors.Control;
            dockPanelGradient2.StartColor = System.Drawing.SystemColors.Control;
            dockPaneStripGradient1.DockStripGradient = dockPanelGradient2;
            tabGradient3.EndColor = System.Drawing.SystemColors.ControlLight;
            tabGradient3.StartColor = System.Drawing.SystemColors.ControlLight;
            tabGradient3.TextColor = System.Drawing.SystemColors.ControlText;
            dockPaneStripGradient1.InactiveTabGradient = tabGradient3;
            dockPaneStripSkin1.DocumentGradient = dockPaneStripGradient1;
            dockPaneStripSkin1.TextFont = new System.Drawing.Font("Segoe UI", 9F);
            tabGradient4.EndColor = System.Drawing.SystemColors.ActiveCaption;
            tabGradient4.LinearGradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            tabGradient4.StartColor = System.Drawing.SystemColors.GradientActiveCaption;
            tabGradient4.TextColor = System.Drawing.SystemColors.ActiveCaptionText;
            dockPaneStripToolWindowGradient1.ActiveCaptionGradient = tabGradient4;
            tabGradient5.EndColor = System.Drawing.SystemColors.Control;
            tabGradient5.StartColor = System.Drawing.SystemColors.Control;
            tabGradient5.TextColor = System.Drawing.SystemColors.ControlText;
            dockPaneStripToolWindowGradient1.ActiveTabGradient = tabGradient5;
            dockPanelGradient3.EndColor = System.Drawing.SystemColors.ControlLight;
            dockPanelGradient3.StartColor = System.Drawing.SystemColors.ControlLight;
            dockPaneStripToolWindowGradient1.DockStripGradient = dockPanelGradient3;
            tabGradient6.EndColor = System.Drawing.SystemColors.InactiveCaption;
            tabGradient6.LinearGradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            tabGradient6.StartColor = System.Drawing.SystemColors.GradientInactiveCaption;
            tabGradient6.TextColor = System.Drawing.SystemColors.InactiveCaptionText;
            dockPaneStripToolWindowGradient1.InactiveCaptionGradient = tabGradient6;
            tabGradient7.EndColor = System.Drawing.Color.Transparent;
            tabGradient7.StartColor = System.Drawing.Color.Transparent;
            tabGradient7.TextColor = System.Drawing.SystemColors.ControlDarkDark;
            dockPaneStripToolWindowGradient1.InactiveTabGradient = tabGradient7;
            dockPaneStripSkin1.ToolWindowGradient = dockPaneStripToolWindowGradient1;
            dockPanelSkin1.DockPaneStripSkin = dockPaneStripSkin1;
            dockPanel.Skin = dockPanelSkin1;
            dockPanel.ActiveContentChanged += new System.EventHandler(this.dockPanel_ActiveContentChanged);
        }
        //Entry Point
        public Main(string path, bool openSession)
        {
            InitailzeDock();
            InitializeComponent();
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(Settings.Default.Culture);
            PluginHelper.PluginsDirectory = Path.Combine(Application.StartupPath, "Plugins");
            if (openSession == true)
            {
                this.Opensession(path);

            }
            else if (openSession == false)
            {
                if (path != string.Empty)
                {
                    this.OpenFile(path, new object(), new EventArgs());
                }
                else
                {
                    if (string.IsNullOrEmpty(Settings.Default.File))
                    {
                        Editor edit = new Editor();
                        edit.Text = "New" + num;
                        num = num + 1;
                        edit.Show(dockPanel, DockState.Document);
                    }
                    else
                    {
                        if (Settings.Default.OpenPreviousFile == true)
                            OpenFile(Settings.Default.File, new object(), new EventArgs());
                    }
                }
            }
            try
            {
                ActiveEditor.codebox.TextChangedDelayed += new EventHandler<TextChangedEventArgs>(codebox_TextChangedDelayed);
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            ApplySkins();
        }
        
        public void ApplySkins()
        {
            string Theme = Settings.Default.Theme;
            switch (Theme)
            {
                case "Default": ToolStripManager.Renderer = new MenuRenderer(ToolbarTheme.Toolbar);
                    statusStrip.Renderer = new MenuRenderer(ToolbarTheme.Toolbar);
                    break;
                case "Dark": ToolStripManager.Renderer = new DarkThemeRenderer();
                    statusStrip.Renderer = new DarkThemeRenderer();
                    break;
                case "Media": ToolStripManager.Renderer = new MenuRenderer(ToolbarTheme.MediaToolbar);
                    statusStrip.Renderer = new MenuRenderer(ToolbarTheme.MediaToolbar);
                    break;
                case "Comm": ToolStripManager.Renderer = new MenuRenderer(ToolbarTheme.CommunicationsToolbar);
                    statusStrip.Renderer = new MenuRenderer(ToolbarTheme.CommunicationsToolbar);
                    break;
                case "BrowserTabBar": ToolStripManager.Renderer = new MenuRenderer(ToolbarTheme.BrowserTabBar);
                    statusStrip.Renderer = new MenuRenderer(ToolbarTheme.BrowserTabBar);
                    break;
                case "OfficeXP": ToolStripManager.Renderer = new OfficeXPRenderer();
                    statusStrip.Renderer = new OfficeXPRenderer();
                    break;
                case "VS2010": ToolStripManager.Renderer = new VS2010Renderer();
                    statusStrip.Renderer = new VS2010Renderer();
                    break;
                case "Help":ToolStripManager.Renderer = new MenuRenderer(ToolbarTheme.HelpBar);
                                  statusStrip.Renderer = new MenuRenderer(ToolbarTheme.HelpBar);
                                  break;
            }
                   
        }
        #endregion

        #region IHost Members

        public string CurrentFile
        {
            get { return ActiveEditor.Name; }
        }
        public FastColoredTextBox FCTB
        {
            get { return ActiveEditor.codebox; }
        }
        public string EditorText
        {
            get { return ActiveEditor.codebox.Text; }
        }

        #endregion

        #region Properties

        public static Editor ActiveEditor
        {
            get { return dockPanel.ActiveDocument as Editor; }
        }
        public static string UserText
        {
            get { try { return ActiveEditor.codebox.Text; } catch (System.Exception ex) { return ex.Message; } }
            set { try { ActiveEditor.codebox.Text = value; } catch (System.Exception ex) { Console.WriteLine(ex.Message); } }
        }
        public static FastColoredTextBox ActiveFastColoredTextBox
        {
            get { return ActiveEditor.codebox; }
        }
        #endregion

        #region Helper Classes
        //Open's File
        public void OpenFile(string file,  object sender, EventArgs e)
        {
            try
            {
                string ext = System.IO.Path.GetExtension(file);
                Editor edit = new Editor();
                edit.Text = Path.GetFileName(file);
                edit.Name = file;
                edit.codebox.TextChangedDelayed += new EventHandler<TextChangedEventArgs>(codebox_TextChangedDelayed);
                edit.Show(dockPanel, DockState.Document);
                if (ext == ".html" | ext == ".htm" | ext == ".xhtml" | ext == ".shtml"| ext == ".HTML" | ext == ".HTM")
                {
                    mihtml_Click(sender, e);
                }
                else if (ext == ".php")
                {
                    miphp_Click(sender, e);
                }
                //Javascript
                else if (ext == ".js"| ext == ".json")
                {
                    mijs_Click(sender, e);
                }
                //ASP
                else if (ext == ".aspx" | ext == ".asp")
                {
                    miasp_Click(sender, e);
                }
                //XML
                else if (ext == ".xml" | ext == ".XML")
                {
                    mixml_Click(sender, e);
                }
                else if (ext == ".xsd")
                {
                    mixml_Click(sender, e);
                }
                //Batch
                else if (ext == ".bat" | ext == ".cmd")
                {
                    mibatch_Click(sender, e);
                }
                //SQL
                else if (ext == ".sql")
                {
                    misql_Click(sender, e);
                }
                //CSS
                else if (ext == ".css")
                {
                    micss_Click(sender, e);
                }
                else if (ext == ".as" | ext == ".AS")
                {
                    mias_Click(sender, e);
                }
                else if (ext == ".py" | ext == ".PY" | ext == ".pyw")
                {
                    mipython_Click(sender, e);
                }
                else if (ext == ".rb" | ext == ".ruby" | ext == ".rbw")
                {
                    miruby_Click(sender, e);
                }
                else if (ext == ".lua" | ext == ".LUA")
                {
                    luaToolStripMenuItem_Click(sender, e);
                }
                else if (ext == ".bas" | ext == ".BAS")
                {
                    miqb_Click(sender, e);
                }
                else if (ext == ".vb" | ext == ".VB")
                {
                    mivb_Click(sender, e);
                }
                else if (ext == ".cs" | ext == ".CS")
                {
                    mics_Click(sender, e);
                }
                else if (ext == ".java" | ext == ".JAVA")
                {
                    mijava_Click(sender, e);
                }
                else if (ext == ".cpp" | ext == ".h" | ext == ".cxx" | ext == ".hpp" | ext == ".hxx" | ext == ".cc")
                {
                    micpp_Click(sender, e);
                }
                else if (ext == ".c" | ext == ".C")
                {
                    micpp_Click(sender, e);
 
                }
                switch(Settings.Default.Encoding)
                {
                    case "ANSI": edit.codebox.Text =  File.ReadAllText(file, Encoding.Default);
                        break;
                    case "ASCII": edit.codebox.Text = File.ReadAllText(file, Encoding.ASCII);
                        break;
                    case "Unicode": edit.codebox.Text = File.ReadAllText(file, Encoding.Unicode);
                        break;
                    case "UTF7": edit.codebox.Text = File.ReadAllText(file, Encoding.UTF7);
                        break;
                    case "UTF32": edit.codebox.Text = File.ReadAllText(file, Encoding.UTF32);
                        break;
                    case "UnicodeBigEndian": edit.codebox.Text = File.ReadAllText(file, Encoding.BigEndianUnicode);
                        break;
                    case "UTF8": edit.codebox.Text = File.ReadAllText(file, Encoding.UTF8);
                        break;
                        //BOM
                    case "UnicodeWithoutBOM": edit.codebox.Text = File.ReadAllText(file, UnicodeWithoutBOM);
                        break;
                    case "UnicodeBigEndianWithoutBOM": edit.codebox.Text = File.ReadAllText(file, UnicodeBigEndianWithoutBOM);
                        break;
                    case "UTF8WithoutBOM": edit.codebox.Text = File.ReadAllText(file, UTF8WithoutBOM);
                        break;
                
            }
                edit.codebox.ClearUndo();
                codebox_TextChangedDelayed(sender, new TextChangedEventArgs(edit.codebox.Range));
                edit.codebox.OnSyntaxHighlight(new TextChangedEventArgs(edit.codebox.Range));
                edit.codebox.IsChanged = false;
                AddRecentFile(file);

            }
            catch (System.Exception ex) { MessageBox.Show(ex.Message); }
        }
        void AddRecentFile(string file){
            ToolStripMenuItem m = new ToolStripMenuItem();
            m.Text = file;
            this.mirecentfiles.DropDownItems.Add(m);
            }
        public void SaveAs(){
            try{
            SaveFileDialog s = new SaveFileDialog();
            s.Filter = "All Files(*.*)|*.*|Text Files(*.txt)|*.txt|XML Files (*.xml)|*.xml|XML Schema Definition File(*.xsd)|*.xsd|Log File (*.log)|*.log|HTML Document (*.html),(*.xhtml),(*.shtml)|*.html;*.xhtml;*.shtml|ASP.NET File(*.asp),(*.aspx)|*.asp;*.aspx|PHP Document (*.php)|*.php|Cascading Style Sheet (*.css)|*.css|Javascript File (*.js)|*.js|QBasic File(*.bas)|*.bas|Visual Basic File (*.vb)|*.vb|Python File (*.py)|*.py|Ruby File(*.ruby)|*.ruby|Lua File(*.lua)|Flash Actionscript file(*.as)|*.as|C# Source File(*.cs)|*.cs|C Source File(*.c)|C++ Source File (*.cpp)|*.cpp|C++ Header File(*.h)|*.h";
            s.ShowDialog();
             if(!string.IsNullOrEmpty(s.FileName)){
                 switch(Settings.Default.Encoding){
                     case "ANSI": File.WriteAllText(s.FileName, ActiveEditor.codebox.Text, Encoding.Default);
                         break;
                     case "ASCII": File.WriteAllText(s.FileName, ActiveEditor.codebox.Text, Encoding.ASCII);
                         break;
                     case "Unicode": File.WriteAllText(s.FileName, ActiveEditor.codebox.Text, Encoding.Unicode);
                         break;
                     case "UnicodeBigEndian": File.WriteAllText(s.FileName, ActiveEditor.codebox.Text, Encoding.BigEndianUnicode);
                         break;
                     case "UTF7": File.WriteAllText(s.FileName, ActiveEditor.codebox.Text, Encoding.UTF7);
                         break;
                     case "UTF32": File.WriteAllText(s.FileName, ActiveEditor.codebox.Text, Encoding.UTF32);
                         break;
                     case "UTF8": File.WriteAllText(s.FileName, ActiveEditor.codebox.Text, Encoding.UTF8);
                         break;
                     case "UnicodeWithoutBOM": File.WriteAllText(s.FileName, ActiveEditor.codebox.Text, UnicodeWithoutBOM);
                         break;
                     case "UnicodeBigEndianWithoutBOM": File.WriteAllText(s.FileName, ActiveEditor.codebox.Text, UnicodeBigEndianWithoutBOM);
                         break;
                     case "UTF8WithoutBOM": File.WriteAllText(s.FileName, ActiveEditor.codebox.Text, UTF8WithoutBOM);
                         break;

             }
                 ActiveEditor.Text = Path.GetFileName(s.FileName);
                 ActiveEditor.Name = s.FileName;
                 ActiveEditor.codebox.IsChanged = false;
             }else{}
           } catch(System.Exception ex){MessageBox.Show(ex.Message);}
        }
        public void Save(){
            Stats.Text = "Saving File......";
            if (!(ActiveEditor.Name == "Editor"))
            {
                 switch (Settings.Default.Encoding)
                 {
                     case "ANSI": File.WriteAllText(ActiveEditor.Name, ActiveEditor.codebox.Text, Encoding.Default);
                         break;
                     case "ASCII": File.WriteAllText(ActiveEditor.Name, ActiveEditor.codebox.Text, Encoding.ASCII);
                         break;
                     case "Unicode": File.WriteAllText(ActiveEditor.Name, ActiveEditor.codebox.Text, Encoding.Unicode);
                         break;
                     case "UnicodeBigEndian": File.WriteAllText(ActiveEditor.Name, ActiveEditor.codebox.Text, Encoding.BigEndianUnicode);
                         break;
                     case "UTF7": File.WriteAllText(ActiveEditor.Name, ActiveEditor.codebox.Text, Encoding.UTF7);
                         break;
                     case "UTF32": File.WriteAllText(ActiveEditor.Name, ActiveEditor.codebox.Text, Encoding.UTF32);
                         break;
                     case "UTF8": File.WriteAllText(ActiveEditor.Name, ActiveEditor.codebox.Text, Encoding.UTF8);
                         break;
                     case "UnicodeWithoutBOM": File.WriteAllText(ActiveEditor.Name, ActiveEditor.codebox.Text, UnicodeWithoutBOM);
                         break;
                     case "UnicodeBigEndianWithoutBOM": File.WriteAllText(ActiveEditor.Name, ActiveEditor.codebox.Text, UnicodeBigEndianWithoutBOM);
                         break;
                     case "UTF8WithoutBOM": File.WriteAllText(ActiveEditor.Name, ActiveEditor.codebox.Text, UTF8WithoutBOM);
                         break;
                 }
                 ActiveEditor.codebox.IsChanged = false;
            }
            else { SaveAs(); }
            Stats.Text = "Ready";
        }
        //Zoom with Error Handling
        public void Adjustzoom(int percent){
            try { ActiveEditor.codebox.Zoom = percent; }
            catch (System.Exception ex) { Console.WriteLine(ex.Message); }
            }
        #endregion


        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Editor edit = new Editor();
            edit.Text = "New" + num;
            num = num + 1;
            edit.codebox.TextChangedDelayed += new EventHandler<TextChangedEventArgs>(codebox_TextChangedDelayed);
            edit.Show(dockPanel, DockState.Document);
        }
        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                ActiveEditor.codebox.Undo();
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
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
                Console.WriteLine(ex.Message);
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
                Console.WriteLine(ex.Message);
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
                Console.WriteLine(ex.Message);
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
                Console.WriteLine(ex.Message);
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
                Console.WriteLine(ex.Message);
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
                Console.WriteLine(ex.Message);
            }
        }
        private void gotobookmark_DropDownOpening(object sender, EventArgs e)
        {
            gotobookmark.DropDownItems.Clear();
            foreach (var bookmark in ActiveEditor.codebox.Bookmarks)
            {
                var item = gotobookmark.DropDownItems.Add(bookmark.Name);
                item.Tag = bookmark;
                item.Click += (o, a) => ((Bookmark)(o as ToolStripItem).Tag).DoVisible();
            }
        }
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog o = new OpenFileDialog();
            o.Filter = "All Files(*.*)|*.*|Text Files(*.txt)|*.txt|XML Files (*.xml)|*.xml|XML Schema Definition File(*.xsd)|*.xsd|Log File (*.log)|*.log|HTML Document (*.html),(*.xhtml),(*.shtml)|*.html;*.xhtml;*.shtml|ASP.NET File(*.asp),(*.aspx)|*.asp;*.aspx|PHP Document (*.php)|*.php|Cascading Style Sheet (*.css)|*.css|Javascript File (*.js)|*.js|QBasic File(*.bas)|*.bas|Visual Basic File (*.vb)|*.vb|Python File (*.py)|*.py|Ruby File(*.ruby)|*.ruby|Lua File(*.lua)|Flash Actionscript file(*.as)|*.as|C# Source File(*.cs)|*.cs|C Source File(*.c)|C++ Source File (*.cpp)|*.cpp|C++ Header File(*.h)|*.h";
            o.ShowDialog();
            if (!string.IsNullOrEmpty(o.FileName))
                this.OpenFile(o.FileName,sender,e);
        }

        private void hTMLToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            string HTML = ActiveEditor.codebox.Html;
            SaveFileDialog s = new SaveFileDialog();
            s.Title = "Export as HTML Document";
            s.Filter = "HTML Documents (*.html)|*.html";
            s.ShowDialog();
            if (s.FileName != null)
            {
                File.WriteAllText(s.FileName, HTML);
            }
        }

        private void rTFToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string RTF = ActiveEditor.codebox.Rtf;
            SaveFileDialog s = new SaveFileDialog();
            s.Title = "Export as Rich Text Documents";
            s.Filter = "Rich Text Documents Documents (*.rtf)|*.rtf";
            s.ShowDialog();
            if (s.FileName != null)
            {
                File.WriteAllText(s.FileName, RTF);
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Save();
        }

        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Stats.Text = "Loading Print Preview Dialog....";
            ActiveEditor.codebox.Print(new PrintDialogSettings());
            Stats.Text = "Ready";
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
                Console.WriteLine(ex.Message);
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
                Console.WriteLine(ex.Message);
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
                Console.WriteLine(ex.Message);
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
            catch (System.Exception ex) { Console.WriteLine(ex.Message); }
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
                Console.WriteLine(ex.Message);
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
                Console.WriteLine(ex.Message);
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
                Console.WriteLine(ex.Message);
            }
        }

        private void executeMacrosMultipleTimesToolStripMenuItem_Click(object sender, EventArgs e)
        {try{
            new Utils.InsertLine(ActiveEditor.codebox, SS.Ynote.Classic.Utils.InsertType.Macro).Show();}catch{}
        }

        private void clearRecordedMacroDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                ActiveEditor.codebox.MacrosManager.ClearMacros();
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        #region Language Handlers

        void InvalidateEditor()
        {
            ActiveEditor.codebox.SelectAll();
            ActiveEditor.codebox.Cut();
            ActiveEditor.codebox.Paste();
        }
        private void mitext_Click(object sender, EventArgs e)
        {
            ActiveEditor.codebox.Language = Language.Custom;
            ActiveEditor.codebox.DescriptionFile = @"Highlighters/Text.xml";
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
            this.micustom.Checked = false;
            ActiveEditor.codebox.OnSyntaxHighlight(new TextChangedEventArgs(ActiveEditor.codebox.Range));
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
            this.micustom.Checked = false;
            this.misql.Checked = false;
            this.miasp.Checked = false;
            ActiveEditor.codebox.OnSyntaxHighlight(new TextChangedEventArgs(ActiveEditor.codebox.Range));
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
            this.micustom.Checked = false;
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
            this.micustom.Checked = false;
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
            ActiveEditor.codebox.OnSyntaxHighlight(new TextChangedEventArgs(ActiveEditor.codebox.Range));
            XMLSyntaxHighlight(ActiveEditor.codebox.Range);
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
            this.micustom.Checked = false;
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
            ActiveEditor.codebox.OnSyntaxHighlight(new TextChangedEventArgs(ActiveEditor.codebox.Range));
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
            this.micustom.Checked = false;
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
            CSSHighlight(ActiveEditor.codebox.Range);
        }

        private void miphp_Click(object sender, EventArgs e)
        {
            ActiveEditor.codebox.Language = Language.PHP;
            ActiveEditor.codebox.DescriptionFile = null;
            this.mitext.Checked = false;
            this.mias.Checked = false;
            this.mibatch.Checked = false;
            this.micustom.Checked = false;
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
            ActiveEditor.codebox.OnSyntaxHighlight(new TextChangedEventArgs(ActiveEditor.codebox.Range));
            PHPSyntaxHighlight(ActiveEditor.codebox.Range);
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
            this.micustom.Checked = false;
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
            ActiveEditor.codebox.OnSyntaxHighlight(new TextChangedEventArgs(ActiveEditor.codebox.Range));
        }

        private void miasp_Click(object sender, EventArgs e)
        {
            ActiveEditor.codebox.Language = Language.HTML;
            //ASP Like HTML
            ActiveEditor.codebox.DescriptionFile = null;
            this.mitext.Checked = false;
            this.mias.Checked = false;
            this.micustom.Checked = false;
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
            ActiveEditor.codebox.OnSyntaxHighlight(new TextChangedEventArgs(ActiveEditor.codebox.Range));
        }
        private void customToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                ActiveEditor.codebox.Language = Language.Custom;
                OpenFileDialog o = new OpenFileDialog();
                o.Filter = "Description Files (*.xml), (*.syn)|*.xml;*.syn";
                o.Title = "Attatch Description File";
                o.ShowDialog();
                ActiveEditor.codebox.DescriptionFile = o.FileName;
                this.mitext.Checked = false;
                this.mias.Checked = false;
                this.mibatch.Checked = false;
                this.mixml.Checked = false;
                this.mivb.Checked = false;
                this.mipython.Checked = false;
                this.milua.Checked = false;
                this.micustom.Checked = true;
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
                ActiveEditor.codebox.OnSyntaxHighlight(new TextChangedEventArgs(ActiveEditor.codebox.Range));
            }catch(Exception ex){MessageBox.Show(ex.Message);}
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
            this.micustom.Checked = false;
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
            JavaASHighlight(ActiveEditor.codebox.Range);
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
            this.micustom.Checked = false;
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
            ActiveEditor.codebox.OnSyntaxHighlight(new TextChangedEventArgs(ActiveEditor.codebox.Range));
        }

        private void micpp_Click(object sender, EventArgs e)
        {
            ActiveEditor.codebox.Language = Language.Custom;
            ActiveEditor.codebox.DescriptionFile = @"Highlighters/C++.xml";
            this.mitext.Checked = false;
            this.mias.Checked = false;
            this.mibatch.Checked = false;
            this.mixml.Checked = false;
            this.micustom.Checked = false;
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
            ActiveEditor.codebox.OnSyntaxHighlight(new TextChangedEventArgs(ActiveEditor.codebox.Range));
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
            this.micustom.Checked = false;
            this.mijava.Checked = true;
            this.micss.Checked = false;
            this.micpp.Checked = false;
            this.misql.Checked = false;
            this.miasp.Checked = false;
            JavaASHighlight(ActiveEditor.codebox.Range);
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
            this.micustom.Checked = false;
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
            ActiveEditor.codebox.OnSyntaxHighlight(new TextChangedEventArgs(ActiveEditor.codebox.Range));
        }

        private void miqb_Click(object sender, EventArgs e)
        {
            ActiveEditor.codebox.Language = Language.VB;
            ActiveEditor.codebox.DescriptionFile = null;
            this.mitext.Checked = false;
            this.mias.Checked = false;
            this.mibatch.Checked = false;
            this.micustom.Checked = false;
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
            ActiveEditor.codebox.OnSyntaxHighlight(new TextChangedEventArgs(ActiveEditor.codebox.Range));
        }

        private void miruby_Click(object sender, EventArgs e)
        {
            ActiveEditor.codebox.Language = Language.Custom;
            ActiveEditor.codebox.DescriptionFile = "Highlighters/Ruby.xml";
            this.mitext.Checked = false;
            this.mias.Checked = false;
            this.mibatch.Checked = false;
            this.micustom.Checked = false;
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
            ActiveEditor.codebox.OnSyntaxHighlight(new TextChangedEventArgs(ActiveEditor.codebox.Range));
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
            this.micustom.Checked = false;
            this.mics.Checked = false;
            this.mihtml.Checked = false;
            this.miphp.Checked = false;
            this.mijs.Checked = false;
            this.mijava.Checked = false;
            this.micss.Checked = false;
            this.micpp.Checked = false;
            this.misql.Checked = false;
            this.miasp.Checked = false;
            ActiveEditor.codebox.OnSyntaxHighlight(new TextChangedEventArgs(ActiveEditor.codebox.Range));
        }
        #endregion

        #region Text Editor Config
        #region Highlighters
        void CSSHighlight(Range r)
        {
            Regex StringRegex = new Regex(@"""""|@""""|''|@"".*?""|(?<!@)(?<range>"".*?[^\\]"")|'.*?[^\\]'");
            Regex CommentRegex2 = new Regex(@"(/\*.*?\*/)|(/\*.*)", RegexOptions.Singleline);
            Regex CommentRegex3 = new Regex(@"(/\*.*?\*/)|(.*\*/)", RegexOptions.Singleline | RegexOptions.RightToLeft);
            Regex NumberRegex = new Regex(@"\d+[\.]?\d*(pt|px|\%|em)?");
            Regex PropertyRegex = new Regex(@"(?<![\-\w])(animation|animation|animation-name|animation-duration|animation-timing-function|animation-delay|animation-iteration-count|animation-direction|animation-play-state|appearance|backface-visibility|background|background|background-attachment|background-color|background-image|background-position|background-repeat|background-clip|background-origin|background-size|border|border|border-bottom|border-bottom-color|border-bottom-style|border-bottom-width|border-collapse|border-color|border-left|border-left-color|border-left-style|border-left-width|border-right|border-right-color|border-right-style|border-right-width|border-spacing|border-style|border-top|border-top-color|border-top-style|border-top-width|border-width|border-bottom-left-radius|border-bottom-right-radius|border-image|border-image-outset|border-image-repeat|border-image-slice|border-image-source|border-image-width|border-radius|border-top-left-radius|border-top-right-radius|bottom|box|box-align|box-direction|box-flex|box-flex-group|box-lines|box-ordinal-group|box-orient|box-pack|box-sizing|box-shadow|caption-side|clear|clip|color|column|column-count|column-fill|column-gap|column-rule|column-rule-color|column-rule-style|column-rule-width|column-span|column-width|columns|content|counter-increment|counter-reset|cursor|direction|display|empty-cells|float|font|font|font-family|font-size|font-style|font-variant|font-weight|@font-face|font-size-adjust|font-stretch|grid-columns|grid-rows|hanging-punctuation|height|icon|@keyframes|left|letter-spacing|line-height|list-style|list-style|list-style-image|list-style-position|list-style-type|margin|margin|margin-bottom|margin-left|margin-right|margin-top|max-height|max-width|min-height|min-width|nav|nav-down|nav-index|nav-left|nav-right|nav-up|opacity|outline|outline|outline-color|outline-offset|outline-style|outline-width|overflow|overflow-x|overflow-y|padding|padding|padding-bottom|padding-left|padding-right|padding-top|page-break|page-break-after|page-break-before|page-break-inside|perspective|perspective-origin|position|punctuation-trim|quotes|resize|right|rotation|rotation-point|table-layout|target|target|target-name|target-new|target-position|text|text-align|text-decoration|text-indent|text-justify|text-outline|text-overflow|text-shadow|text-transform|text-wrap|top|transform|transform|transform-origin|transform-style|transition|transition|transition-property|transition-duration|transition-timing-function|transition-delay|vertical-align|visibility|width|white-space|word-spacing|word-break|word-wrap|z-index)(?![\-\w])", RegexOptions.IgnoreCase);
            Regex SelectorRegex = new Regex(@"\b(a|abbr|acronym|address|applet|area|article|aside|audio|b|base|basefont|bdi|bdo|big|blockquote|body|br|button|canvas|caption|center|cite|code|col|colgroup|command|datalist|dd|del|details|dfn|dialog|dir|div|dl|dt|em|embed|fieldset|figcaption|figure|font|footer|form|frame|frameset|h1|h2|h3|h4|h5|h6|head|header|hgroup|hr|html|i|iframe|img|input|ins|kbd|keygen|label|legend|li|link|map|mark|menu|meta|meter|nav|noframes|noscript|object|ol|optgroup|option|output|p|param|pre|progress|q|rp|rt|ruby|s|samp|script|section|select|small|source|span|strike|strong|style|sub|summary|sup|table|tbody|td|textarea|tfoot|th|thead|time|title|tr|track|tt|u|ul|var|video|wbr)\b|[#@\.][\w\-]+\b", RegexOptions.IgnoreCase);
            r.tb.LeftBracket = '{';
            r.tb.RightBracket = '}';
            r.tb.LeftBracket2 = '(';
            r.tb.RightBracket2 = ')';
            r.ClearStyle(brownStyle, blueStyle, magentaStyle, grayStyle, redStyle);
            r.SetFoldingMarkers("{", "}");
            r.SetStyle(grayStyle, CommentRegex2);
            r.SetStyle(grayStyle, CommentRegex3);
            r.SetStyle(brownStyle, StringRegex);
            r.SetStyle(magentaStyle, PropertyRegex);
            r.SetStyle(blueStyle, SelectorRegex);
            r.SetStyle(redStyle, NumberRegex);
        }

        void PHPSyntaxHighlight(Range r)
        {
            r.ClearStyle(blueStyle);
            r.ClearStyle(brownStyle);
            r.SetStyle(blueStyle, "php", RegexOptions.IgnoreCase);
            r.SetStyle(brownStyle, "<|>", RegexOptions.IgnoreCase);
            r.SetFoldingMarkers("<", ">");
        }
        void XMLSyntaxHighlight(Range r)
        {
            r.ClearStyle(blueStyle, XML);
            r.SetStyle(blueStyle, "xml", RegexOptions.Multiline);
            r.SetStyle(XML, "\\?>|<\\?|", RegexOptions.IgnoreCase);
            r.SetFoldingMarkers("<", "/", RegexOptions.Compiled);

        }
        void JavaASHighlight(Range r)
        {
            r.ClearStyle(blueStyle);
            r.ClearStyle(classstyle);
            r.SetStyle(blueStyle, "import|package|extends|implements|boolean|final|assert|instanceof|native|strictfp|super|synchronized|throws|transient", RegexOptions.IgnoreCase);
            r.SetStyle(classstyle, "extends.*$", RegexOptions.Multiline);
            ActiveEditor.codebox.LeftBracket = '{';
            ActiveEditor.codebox.RightBracket = '}';
            ActiveEditor.codebox.LeftBracket2 = '(';
            ActiveEditor.codebox.RightBracket2 = ')';

        }
        void QBHighlight(Range r)
        {
            r.tb.LeftBracket = '(';
            r.tb.LeftBracket2 = '[';
            r.tb.RightBracket = ')';
            r.tb.RightBracket2 = ']';
            r.ClearStyle(blueStyle);
            r.SetStyle(blueStyle, "\b(PRINT|INPUT|SET|RND|RANDOMIZE|RANDOM|TIMER)\b", RegexOptions.IgnoreCase);
        }
        #endregion
        private void codebox_TextChangedDelayed(object sender, FastColoredTextBoxNS.TextChangedEventArgs e)
        {
            HighlightInvisibleChars(e.ChangedRange);
            if (this.micss.Checked == true)
            {
                CSSHighlight(e.ChangedRange);
            }
            if (this.miphp.Checked == true)
            {
                PHPSyntaxHighlight(e.ChangedRange);
            }
            if (this.mixml.Checked == true)
            {
                XMLSyntaxHighlight(e.ChangedRange);
            }
            if (this.mijava.Checked == true | mias.Checked == true)
            {
                JavaASHighlight(e.ChangedRange);
            }
            if (this.miqb.Checked == true)
            {

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
            catch (System.Exception ex) { Console.WriteLine(ex.Message); }
        }

        private void mitoolbar_Click(object sender, EventArgs e)
        {
            toolstrip.Visible = mitoolbar.Checked;
            Settings.Default.ToolBarVisible = mitoolbar.Checked;
        }

        private void mistatusbar_Click(object sender, EventArgs e)
        {
            statusStrip.Visible = mistatusbar.Checked;
            Settings.Default.StatusBarVisible = mistatusbar.Checked;
        }

        private void zoomInToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try { ActiveEditor.codebox.Zoom = ActiveEditor.codebox.Zoom + 20; }
            catch (System.Exception ex) { Console.WriteLine(ex.Message); }
        }

        private void zoomOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try { ActiveEditor.codebox.Zoom = ActiveEditor.codebox.Zoom - 20; }
            catch (System.Exception ex) { Console.WriteLine(ex.Message); }
        }

        private void restoreDefaultZoomToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try { ActiveEditor.codebox.Zoom = 100; }
            catch (System.Exception ex) { Console.WriteLine(ex.Message); }
        }

        private void setSelectedTextAsReadonlyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try { ActiveEditor.codebox.Selection.ReadOnly = true; }
            catch (System.Exception ex) { Console.WriteLine(ex.Message); }
        }

        private void setSelectedTextAsWritableToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try { ActiveEditor.codebox.Selection.ReadOnly = false; }
            catch (System.Exception ex) { Console.WriteLine(ex.Message); }
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
            catch (System.Exception ex) { Console.WriteLine(ex.Message); }

        }
        private void HighlightInvisibleChars(Range range)
        {
            if (hiddenCharsToolStripMenuItem.Checked == true)
            {
                range.ClearStyle(invisibleCharsStyle);
                range.SetStyle(invisibleCharsStyle, @".$|.\r\n|\s");
            }
            else
            {
                range.ClearStyle(invisibleCharsStyle);
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
            catch (System.Exception ex) { Console.WriteLine(ex.Message); }
        }

        private void decreaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try { ActiveEditor.codebox.DecreaseIndent(); }
            catch (System.Exception ex) { Console.WriteLine(ex.Message); }
        }
        private void Main_FormClosing(object sender, EventArgs e)
        {
            try { 
                if(!(ActiveEditor.Name == "Editor")){
            Settings.Default.File = ActiveEditor.Name;}
            }
            catch (System.Exception ex) { Console.WriteLine(ex.Message); }
            Settings.Default.Save();

        }
        protected override void OnClosing(CancelEventArgs e)
        {
            if (AppContext.ConfigurationFile.PluginConfiguration.Plugins != null)
            {
                foreach (KeyValuePair<string, PluginInfo> kv in plugins)
                {
                    PluginConfig config = AppContext.ConfigurationFile.PluginConfiguration.Plugins[kv.Key];
                    if (config == null)
                    {
                        config = new PluginConfig();
                        config.Title = kv.Key;
                        AppContext.ConfigurationFile.PluginConfiguration.Plugins.Add(config);
                    }

                    try
                    {
                        config.Configuration = kv.Value.Plugin.Configuration;
                    }
                    catch (NotImplementedException) { }
                }
            }

            AppContext.ConfigurationFile.Save(Settings.Default.PluginConfigFile);

            base.OnClosing(e);
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
            catch (System.Exception ex) { Console.WriteLine(ex.Message); }
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
            catch (System.Exception ex) { Console.WriteLine(ex.Message); }
        }

        private void copyToolStripButton_Click(object sender, EventArgs e)
        {
            try { ActiveEditor.codebox.Copy(); }
            catch (System.Exception ex) { Console.WriteLine(ex.Message); }
        }

        private void pasteToolStripButton_Click(object sender, EventArgs e)
        {
            try { ActiveEditor.codebox.Paste(); }
            catch (System.Exception ex) { Console.WriteLine(ex.Message); }
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
            try
            {
                File.WriteAllText(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/SS/Ynote/app.run", ActiveEditor.codebox.Text);
                RunDialog r = new RunDialog();
                r.Show();
            }
            catch (System.Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void openCurrentDirCommandPromptToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start("cmd.exe", ActiveEditor.Name);
            }
            catch (System.Exception ex) { Console.WriteLine(ex.Message); }
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
        private void mirecentfiles_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e){
            this.OpenFile(e.ClickedItem.Text, sender, e);
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
                {
                    Console.WriteLine(ex.Message);
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
            catch (System.Exception ex) { Console.WriteLine(ex.Message); }
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
            ActiveEditor.codebox.OnSyntaxHighlight(new TextChangedEventArgs(ActiveEditor.codebox.Range));
        }

        private void clipboardHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {

            CH.TextBox = ActiveEditor.codebox;

            CH.Show(dockPanel, DockState.DockRight);

        }

        private void launchInBrowserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string extension = null;
            if (this.mihtml.Checked == true) { extension = ".html"; }
            else if (this.mitext.Checked == true) { extension = ".txt"; }
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
            System.IO.File.WriteAllText(AppDataDir + "file" + extension, ActiveEditor.codebox.Text);
            System.Diagnostics.Process.Start(AppDataDir + "file" + extension);

        }

        private void launToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string AppDataDir = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/SS/Ynote/";
            System.IO.File.WriteAllText(AppDataDir + "file" + ".html", ActiveEditor.codebox.Text);
            System.Diagnostics.Process.Start("iexplore.exe", AppDataDir + "file.html");
        }

        private void launchInChromeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string AppDataDir = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/SS/Ynote/";
            System.IO.File.WriteAllText(AppDataDir + "file" + ".html", ActiveEditor.codebox.Text);
            System.Diagnostics.Process.Start("chrome.exe", AppDataDir + "file.html");
        }

        private void launchInFirefoxToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string AppDataDir = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/SS/Ynote/";
            System.IO.File.WriteAllText(AppDataDir + "file" + ".html", ActiveEditor.codebox.Text);
            System.Diagnostics.Process.Start("firefox.exe", AppDataDir + "file.html");
        }

        private void launchInCommandPromptToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string AppDataDir = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/SS/Ynote/";
            System.IO.File.WriteAllText(AppDataDir + "file.", ActiveEditor.codebox.Text);
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
            try { pt.Show(dockPanel, DockState.DockLeft); }
            catch (System.Exception ex) { Console.WriteLine(ex.Message); } 
        }


    
        private void reloadFromDiskToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try { ActiveEditor.codebox.Text = File.ReadAllText(ActiveEditor.Name); }
            catch (System.Exception ex) { Console.WriteLine(ex.Message); }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            addToolStripMenuItem_Click(sender, e);
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            removeToolStripMenuItem_Click(sender, e);
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.SaveAs();
            ActiveEditor.codebox.IsChanged = false;
        }

        private void exitToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void removeEmptyLinesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var iLines = ActiveEditor.codebox.FindLines(@"^\s*$", RegexOptions.None);
            ActiveEditor.codebox.RemoveLines(iLines);
        }

        private void goToEndToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try { ActiveEditor.codebox.GoEnd(); }
            catch (System.Exception ex) { Console.WriteLine(ex.Message); }
        }

        private void gotToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try { ActiveEditor.codebox.GoHome(); ; }
            catch (System.Exception ex) { Console.WriteLine(ex.Message); }
        }

        private void toolStripMenuItem6_Click(object sender, EventArgs e)
        {
            Adjustzoom(25);
        }

        private void toolStripMenuItem7_Click(object sender, EventArgs e)
        {
            Adjustzoom(50);
        }

        private void toolStripMenuItem8_Click(object sender, EventArgs e)
        {
            Adjustzoom(100);
        }

        private void toolStripMenuItem9_Click(object sender, EventArgs e)
        {
            Adjustzoom(150);
        }

        private void toolStripMenuItem10_Click(object sender, EventArgs e)
        {
            Adjustzoom(200);
        }

        private void toolStripMenuItem11_Click(object sender, EventArgs e)
        {
            Adjustzoom(300);
        }

        private void columnEditorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Please use 'Alt + Mouse Selection' or 'Alt + Shift + Arrow Keys'\n for editing in column mode.");
        }

        private void summaryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var x = System.Text.RegularExpressions.Regex.Matches(ActiveEditor.codebox.Text, "\\w+");
            var nCol = ActiveEditor.codebox.Selection.Start.iChar + 1;
            int words = x.Count;
            MessageBox.Show("Words : " + words.ToString() + "\nLetters :" + ActiveEditor.codebox.Text.Length.ToString() + "\nLines: " + ActiveEditor.codebox.Lines.Count.ToString() + "\nSel: " + ActiveEditor.codebox.Selection.ToString() + "\nCurrent Column: " + nCol.ToString(), "Summary");
        }

        private void openContainingFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try{
            string dir = Path.GetDirectoryName(ActiveEditor.Name);
            System.Diagnostics.Process.Start(dir);
            }
            catch (System.Exception ex) { MessageBox.Show("Error! File is not saved " + ex.Message); }
        }

        private void navigateForwardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try { ActiveEditor.codebox.NavigateForward(); }
            catch (System.Exception ex) { Console.WriteLine(ex.Message); }
        }

        private void navigateBackwardsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try { ActiveEditor.codebox.NavigateBackward(); }
            catch (System.Exception ex) { Console.WriteLine(ex.Message); }
        }

        private void wordWrapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try { ActiveEditor.codebox.WordWrap = wordWrapToolStripMenuItem.Checked; }
            catch (System.Exception ex) { Console.WriteLine(ex.Message); }
        }

        private void preferencesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Stats.Text = "Loading Preferences Dialog.....";
            Preferences p = new Preferences();
            p.Show();
           p.Shown += new EventHandler(p_Shown);
        }
        void p_Shown(object sender, EventArgs e){
            Stats.Text = "Ready";
            }
        private void textToSpeechToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Stats.Text = "Loading Text To Speech Dialog....";
            Text_To_Speech tts = new Text_To_Speech();
            tts.SpeakingText = ActiveEditor.codebox.Text;
            tts.Show();
            Stats.Text = "Ready";
        }

        private void autoCompleteMenuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ActiveEditor.AutoCompleteMenu.Show(true);
        }

        private void cloneDocumentToolStripMenuItem_Click(object sender, EventArgs e)
        {
                Editor edit = new Editor();
                edit.codebox.SourceTextBox = ActiveEditor.codebox;
                edit.Name = ActiveEditor.Name;
                edit.Text = ActiveEditor.Text;
                edit.Show(dockPanel, DockState.Float);
                edit.DockHandler.FloatPane.DockTo(dockPanel.DockWindows[DockState.Document]);
        }

        private void columnEditorToolStripMenuItem1_Click(object sender, EventArgs e)
        {
        }

        private void saveCurrentRecordedMacroToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog s = new SaveFileDialog();
            s.Title = "Save Macro";
            s.Filter = "Ynote Macro Files (*.ymc)|*.ymc";
            s.ShowDialog();
            File.WriteAllText(s.FileName, ActiveEditor.codebox.MacrosManager.Macros);
        }

        private void importMacroFromFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog o = new OpenFileDialog();
            o.Filter = "Ynote Macro Files (*.ymc)|*.ymc";
            o.ShowDialog();
            if(!(string.IsNullOrEmpty(o.FileName)))
               ActiveEditor.codebox.MacrosManager.Macros = File.ReadAllText(o.FileName,Encoding.Default);
        }

        private void incrementalSearcherToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IncrementalSearcher s = new IncrementalSearcher(ActiveEditor.codebox);
            s.Dock = DockStyle.Bottom ;
            this.Controls.Add(s);

        }

        private void checkForUpdatesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AutoUpdater.LetUserSelectRemindLater = true;
            AutoUpdater.RemindLaterTimeSpan = AutoUpdater.RemindLaterFormat.Minutes;
            AutoUpdater.RemindLaterAt = 10;
            URL URL = new URL("http://sscorps.webs.com/ynoteclassic/2.5.xml");
            if (URL.IsValid == true)
            {
                AutoUpdater.Start("http://sscorps.webs.com/ynoteclassic/2.5.xml");
            }
            else
            {
                MessageBox.Show("No Updates Available");
            }
        }

        private void daToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime date = DateTime.Now;
                TimeSpan time = new TimeSpan(36, 0, 0, 0);
                DateTime combined = date.Add(time);
                ActiveEditor.codebox.InsertText(combined.ToString());
            }
            catch (System.Exception ex) { Console.WriteLine(ex.Message); }
        }

        private void raiseIssueToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://ynoteclassic.codeplex.com/workitem/list/basic");
        }

        private void spellCheckToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                SpellChecker SC = new SpellChecker(ActiveEditor.codebox);
                SC.SpellCheck(sender);
            }
            catch { }
        }

        private void propertiesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (ActiveEditor.Name != "Editor")
                {
                    FileProperties.ShowFileProperties(ActiveEditor.Name);
                }
                else
                {
                    MessageBox.Show("File is not Saved!");
                }
            }
            catch { }
        }

        private void pageSetupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PageSetupDialog ps = new PageSetupDialog();
            ps.Document = new System.Drawing.Printing.PrintDocument();
            ps.ShowDialog();
        }

        private void restartToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }

        private void saveAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Editor doc in dockPanel.Documents)
            {
                if (!(doc.Name == "Editor"))
                {
                    switch (Settings.Default.Encoding)
                    {
                        case "ANSI": File.WriteAllText(ActiveEditor.Name, ActiveEditor.codebox.Text, Encoding.Default);
                            break;
                        case "ASCII": File.WriteAllText(ActiveEditor.Name, ActiveEditor.codebox.Text, Encoding.ASCII);
                            break;
                        case "Unicode": File.WriteAllText(ActiveEditor.Name, ActiveEditor.codebox.Text, Encoding.Unicode);
                            break;
                        case "UnicodeBigEndian": File.WriteAllText(ActiveEditor.Name, ActiveEditor.codebox.Text, Encoding.BigEndianUnicode);
                            break;
                        case "UTF7": File.WriteAllText(ActiveEditor.Name, ActiveEditor.codebox.Text, Encoding.UTF7);
                            break;
                        case "UTF32": File.WriteAllText(ActiveEditor.Name, ActiveEditor.codebox.Text, Encoding.UTF32);
                            break;
                        case "UTF8": File.WriteAllText(ActiveEditor.Name, ActiveEditor.codebox.Text, Encoding.UTF8);
                            break;
                        case "UnicodeWithoutBOM": File.WriteAllText(ActiveEditor.Name, ActiveEditor.codebox.Text, UnicodeWithoutBOM);
                            break;
                        case "UnicodeBigEndianWithoutBOM": File.WriteAllText(ActiveEditor.Name, ActiveEditor.codebox.Text, UnicodeBigEndianWithoutBOM);
                            break;
                        case "UTF8WithoutBOM": File.WriteAllText(ActiveEditor.Name, ActiveEditor.codebox.Text, UTF8WithoutBOM);
                            break;
                    }
                }
                else
                {
                    SaveFileDialog s = new SaveFileDialog();
                    s.Filter = "All Files(*.*)|*.*|Text Files(*.txt)|*.txt|XML Files (*.xml)|*.xml|XML Schema Definition File(*.xsd)|*.xsd|Log File (*.log)|*.log|HTML Document (*.html),(*.xhtml),(*.shtml)|*.html;*.xhtml;*.shtml|ASP.NET File(*.asp),(*.aspx)|*.asp;*.aspx|PHP Document (*.php)|*.php|Cascading Style Sheet (*.css)|*.css|Javascript File (*.js)|*.js|QBasic File(*.bas)|*.bas|Visual Basic File (*.vb)|*.vb|Python File (*.py)|*.py|Ruby File(*.ruby)|*.ruby|Lua File(*.lua)|Flash Actionscript file(*.as)|*.as|C# Source File(*.cs)|*.cs|C Source File(*.c)|C++ Source File (*.cpp)|*.cpp|C++ Header File(*.h)|*.h";
                    s.Title = "Save As " + doc.Text;
                    s.ShowDialog();
                    switch (Settings.Default.Encoding)
                    {
                        case "ANSI": File.WriteAllText(s.FileName, ActiveEditor.codebox.Text, Encoding.Default);
                            break;
                        case "ASCII": File.WriteAllText(s.FileName, ActiveEditor.codebox.Text, Encoding.ASCII);
                            break;
                        case "Unicode": File.WriteAllText(s.FileName, ActiveEditor.codebox.Text, Encoding.Unicode);
                            break;
                        case "UnicodeBigEndian": File.WriteAllText(s.FileName, ActiveEditor.codebox.Text, Encoding.BigEndianUnicode);
                            break;
                        case "UTF7": File.WriteAllText(s.FileName, ActiveEditor.codebox.Text, Encoding.UTF7);
                            break;
                        case "UTF32": File.WriteAllText(s.FileName, ActiveEditor.codebox.Text, Encoding.UTF32);
                            break;
                        case "UTF8": File.WriteAllText(s.FileName, ActiveEditor.codebox.Text, Encoding.UTF8);
                            break;
                        case "UnicodeWithoutBOM": File.WriteAllText(s.FileName, ActiveEditor.codebox.Text, UnicodeWithoutBOM);
                            break;
                        case "UnicodeBigEndianWithoutBOM": File.WriteAllText(s.FileName, ActiveEditor.codebox.Text, UnicodeBigEndianWithoutBOM);
                            break;
                        case "UTF8WithoutBOM": File.WriteAllText(s.FileName, ActiveEditor.codebox.Text, UTF8WithoutBOM);
                            break;
                    }
                }
            }
        }

        private void alwaysOnTopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.TopMost = true;
        }

        private void newToolStripButton_Click_1(object sender, EventArgs e)
        {
            newToolStripMenuItem_Click(sender, e);
        }

        private void openToolStripButton_Click_1(object sender, EventArgs e)
        {
            openToolStripMenuItem_Click(sender, e);
        }

        private void saveToolStripButton_Click(object sender, EventArgs e)
        {
            saveToolStripMenuItem_Click(sender, e);
        }

        private void printToolStripButton_Click_1(object sender, EventArgs e)
        {
            printToolStripMenuItem_Click(sender, e);
        }

        private void cutToolStripButton_Click_1(object sender, EventArgs e)
        {
            cutToolStripMenuItem_Click(sender, e);
        }

        private void copyToolStripButton_Click_1(object sender, EventArgs e)
        {
            copyToolStripMenuItem_Click(sender, e);
        }

        private void editAutoCompleteMenuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.OpenFile(@"Configurations/Autocomplete.xml", sender, e);
        }

        private void batchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.OpenFile(@"Highlighters/Batch.xml", sender, e);
        }

        private void pythonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.OpenFile(@"Highlighters/Python.xml", sender, e);
        }

        private void luaToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            this.OpenFile(@"Highlighters/Lua.xml",sender,e);
        }

        private void rubyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.OpenFile(@"Highlighters/Ruby.xml",sender,e);
        }

        private void textToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.OpenFile(@"Highlighters/Test.xml", sender, e);
        }

        private void cToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.OpenFile(@"Highlighters/C++.xml", sender, e);
        }
        private void toolStripTextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    Stats.Text = "Downloading Data...";
                    Uri URL = new Uri(this.toolStripTextBox1.Text);
                    BrowserClient Manager = new BrowserClient();
                    Manager.DownloadStringCompleted += new System.Net.DownloadStringCompletedEventHandler(Manager_DownloadStringCompleted);
                    Manager.DownloadStringAsync(URL);
                }
                catch (System.Exception ex) { MessageBox.Show(ex.Message); }
            }
        }
        private void Manager_DownloadStringCompleted(object sender, System.Net.DownloadStringCompletedEventArgs e)
        {
            Editor edit = new Editor();
            edit.codebox.Text = e.Result;
            edit.Text = this.toolStripTextBox1.Text;
            edit.Name = "Editor";
            edit.Show(dockPanel, DockState.Document);
            Stats.Text = "Ready";
        }

        private void fromDirectoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog f = new FolderBrowserDialog();
            f.Description = "Import Files From Directory";
            f.ShowDialog();
            if (f.SelectedPath != null)
            {
                string[] files = Directory.GetFiles(f.SelectedPath);
                foreach (string file in files)
                {
                    OpenFile(file, sender, e);
                }
            }
        }

        private void fromArchiveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SS.Ynote.Engine.Compression.ZipManager UnZipper = new SS.Ynote.Engine.Compression.ZipManager();
            OpenFileDialog o = new OpenFileDialog();
            o.Filter = "Zip Archives (*.zip)|*.zip";
            o.ShowDialog();
            UnZipper.ExtractFilesFromZip(o.FileName, Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\SS\\Ynote\\Archives\\",null);
            string[] files = Directory.GetFiles(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\SS\\Ynote\\Archives\\");
            foreach (string file in files)
            {
                OpenFile(file, sender, e);
            }
        }

        private void searchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://support.sscorps.tk");
        }

        private void pluginCentralToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://plugins.sscorps.tk");
        }

        private void removeCurrentLineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ActiveEditor.codebox.Selection.Start.iLine >= 0 && ActiveEditor.codebox.Selection.Start.iLine < ActiveEditor.codebox.LinesCount)
            {
                int iLine = ActiveEditor.codebox.Selection.Start.iLine;
                int LinesCount = ActiveEditor.codebox.Lines.Count;
                ActiveEditor.codebox.RemoveLines(new List<int> { iLine });
                ActiveEditor.codebox.Selection.Start = new Place(0, Math.Max(0, Math.Min(iLine, LinesCount - 1)));
            }
        }

        private void insertToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (mipython.Checked == true| miruby.Checked == true| miphp.Checked == true )
            {
                ActiveEditor.codebox.InsertLinePrefix("#");
            }
            if (milua.Checked == true | misql.Checked == true)
            {
                ActiveEditor.codebox.InsertLinePrefix("--");
            }
            if (mibatch.Checked == true)
            {
                ActiveEditor.codebox.InsertLinePrefix("rem ");
            }
            if (miqb.Checked == true)
            {
                ActiveEditor.codebox.InsertLinePrefix("'");
            }
            if (micss.Checked == true)
            {
                ActiveEditor.codebox.InsertLinePrefix("/*");
            }
            if (micpp.Checked == true | mics.Checked == true| mijava.Checked == true | mias.Checked == true| mijs.Checked == true)
            {
                ActiveEditor.codebox.InsertLinePrefix("//");
            }
            if (mihtml.Checked == true | mixml.Checked == true| miasp.Checked == true)
            {
                ActiveEditor.codebox.InsertLinePrefix("<!--");
            }
        }

        private void removeToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (mipython.Checked == true | miruby.Checked == true | miphp.Checked == true)
            {
                ActiveEditor.codebox.RemoveLinePrefix("#");
            }
            if (milua.Checked == true | misql.Checked == true)
            {
                ActiveEditor.codebox.RemoveLinePrefix("--");
            }
            if (mibatch.Checked == true)
            {
                ActiveEditor.codebox.RemoveLinePrefix("rem ");
            }
            if (miqb.Checked == true)
            {
                ActiveEditor.codebox.RemoveLinePrefix("'");
            }
            if (micss.Checked == true)
            {
                ActiveEditor.codebox.RemoveLinePrefix("/*");
            }
            if (micpp.Checked == true | mics.Checked == true | mijava.Checked == true | mias.Checked == true | mijs.Checked == true)
            {
                ActiveEditor.codebox.RemoveLinePrefix("//");
            }
            if (mihtml.Checked == true | mixml.Checked == true | miasp.Checked == true)
            {
                ActiveEditor.codebox.RemoveLinePrefix("<!--");
            }
        }

        private void editingModeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Press the 'Ins' Key to Switch Between Insert and Overwrite Editing Mode");
        }

        private void removeWordLeftToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Press Ctrl + Backspace Anytime to remove left word");
        }

        private void removeWordRightToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Press Ctrl + Delete Anytime to remove right word");

        }

        private void goWordLeftToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Press Ctrl + Left To Go Word Left");
        }

        private void goWordRightToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Press Ctrl + Right To Go Word Left");
        }

        private void goWordLeftWithSelectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Press Shift+Ctrl+Left To Go Word Left With Selection");
        }

        private void goWordRightWithSelectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Press Shift+Ctrl+Right To Go Word Right With Selection");
            
        }

        private void batchToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ActiveEditor.codebox.Text = "@echo off";
        }

        private void xMLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ActiveEditor.codebox.Text = "<?xml version='1.0'?>";
        }

        private void hTMLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ActiveEditor.codebox.Text = "<html>\n\r<head>\n\r<title></title>\n\r</head>\n\r<body>\n\r\n\r</body>\n\r</html>";
        }

        private void xHTMLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ActiveEditor.codebox.Text = "<!DOCTYPE html PUBLIC '-//W3C//DTD XHTML 1.0 Transitional//EN' 'http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd'>\n\r<html xmlns='http://www.w3.org/1999/xhtml'>\n\r<head>\n\r<meta http-equiv='Content-Type' content='text/html; charset=iso-8859-1' />\n\r<title></title>\n\r</head>\n\r</head>\n\r<body>\n\r\n\r</body>\n\r</html>";
        }

        private void cSSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ActiveEditor.codebox.Text = "/* CSS Document */ \n\r\n\rbody\n\r{\n\r}";
 
        }

        private void pHPToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ActiveEditor.codebox.Text = "<?php ?>";
        }

        private void javascriptToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ActiveEditor.codebox.Text = "function()\n\r{\n\r\n\r}";
            InvalidateEditor();
        }

        private void actionscriptToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ActiveEditor.codebox.Text = "package\n\r{\n\rpublic class Class1 \n\r{\n\r}\n\r}";
            InvalidateEditor();
        }

        private void cToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ActiveEditor.codebox.Text = "using System;\n\nnamespace Default\n  public class Class1\n\r{\n\r   \n\r}\n\r}";
            InvalidateEditor();
        }

        private void cCToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ActiveEditor.codebox.Text = "#include <iostream.h>\n\nusing namespace std;\n\nint Main()\n{\n\n\n\n}";
        }

        private void javaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ActiveEditor.codebox.Text = "public class Class1 {\n\n}";
        }

        private void qBasicToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ActiveEditor.codebox.Text = "Imports System\n\nNamespace Default\n  Public Class Class1\n\n  End Class\n\nEnd Namespace";
        }

        private void navigateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Press Ctrl + Shift + N To Navigate between bookmarks\n or simply use the toolbar");
        }

        private void fromRTFToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog o = new OpenFileDialog();
            o.Filter = "RTF Files (*.rtf)|*.rtf";
            o.ShowDialog(); if (o.FileName != "")
            {
                Editor edit = new Editor();
                edit.codebox.Text = ConvertToText(File.ReadAllText(o.FileName));
                edit.Name = o.FileName;
                edit.Text = Path.GetFileName(o.FileName);
                edit.Show(dockPanel, DockState.Document);
            }
        }
        static public string ConvertToText(string rtf)
        {
            using (RichTextBox rtb = new RichTextBox())
            {
                rtb.Rtf = rtf;
                return rtb.Text;
            }
        }

        private void emptyLinesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Utils.InsertLine Is = new SS.Ynote.Classic.Utils.InsertLine(ActiveEditor.codebox, SS.Ynote.Classic.Utils.InsertType.Line);
            Is.ShowDialog();
        }

        private void emptyColumnsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Utils.InsertLine Is = new SS.Ynote.Classic.Utils.InsertLine(ActiveEditor.codebox, SS.Ynote.Classic.Utils.InsertType.Column);
            Is.ShowDialog();
        }

        #region Session Managers

        public void Opensession(string file)
        {
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.Load(file);
            foreach (XmlNode item in xmldoc.SelectNodes("*/ynotesession/file"))
            {
                OpenFile(item.InnerText, new object(), new EventArgs());
            }
        }
       public void SaveSession(string path)
       {
           string Document = "<?xml version='1.0'?>\n\r<doc>\n\r<ynotesession>\n\r";
           foreach (Editor doc in dockPanel.Documents)
           {
               if (doc.Name != "Editor")
               {
                   Document += "<file>" + doc.Name + "</file>\n\r";
               }
           }
           Document += "</ynotesession>\n\r</doc>";
           File.WriteAllText(path, Document);
       }
        #endregion

       private void saveSessionToolStripMenuItem1_Click(object sender, EventArgs e)
       {
           SaveFileDialog s = new SaveFileDialog();
           s.Filter = "Ynote Classic Session Files(*.ycs)|*.ycs";
           s.ShowDialog();
           SaveSession(s.FileName);
       }

       private void openSessionToolStripMenuItem_Click(object sender, EventArgs e)
       {
           OpenFileDialog o = new OpenFileDialog();
           o.Filter = "Ynote Classic Sessions (*.ycs)|*.ycs";
           o.ShowDialog();
           if (o.FileName != "")
               Opensession(o.FileName);
       }

       public void minimizeToSystemTrayToolStripMenuItem_Click(object sender, EventArgs e)
       {
           Hide();
           NotifyIcon.BalloonTipTitle = "Ynote Classic";
           NotifyIcon.BalloonTipText = "Ynote Classic has been minimized to the System Tray.";
           NotifyIcon.ShowBalloonTip(3000);
       }

       private void NotifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
       {
           Show();
       }

       private void zToolStripMenuItem_Click(object sender, EventArgs e)
       {
           string HTML = ActiveEditor.codebox.Html;
           SaveFileDialog s = new SaveFileDialog();
           s.Title = "Export as HTML Document";
           s.Filter = "HTML Documents (*.html)|*.html";
           s.ShowDialog();
           if (s.FileName != null)
           {
               File.WriteAllText(s.FileName, HTML);
           }
       }
    }
}
