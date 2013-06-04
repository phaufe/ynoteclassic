#CL Syntax Highlighter Plugin
from System import *
from System.Collections.Generic import *
from System.Linq import *
from System.Text import *
from WeifenLuo.WinFormsUI.Docking import *
from SS.Ynote.Engine.Framework.Plugins.Interface import *
from System.Xml.Linq import *
from FastColoredTextBoxNS import *
from SS.Ynote.Classic import *
from System.Windows.Forms import *

class MainForm(DockContent, IFormPlugin):
    def __init__(self):
        #IFormPlugin Inherits the IPlugin Interface
        self._configuration = XElement("HighlighterPluginConfig")
        self.InitializeComponent()
        self._ShowHint = DockState.DockLeft

    def get_Title(self):
        return "HighlighterPlugin"

    Title = property(fget=get_Title)
    

    def get_Description(self):
        return "Sample Highlighter Plugin"

    Description = property(fget=get_Description)

    def get_Group(self):
        return "Plugins"

    Group = property(fget=get_Group)

    def get_SubGroup(self):
        return "Samples"

    SubGroup = property(fget=get_SubGroup)

    def get_Configuration(self):
        return self._configuration

    def set_Configuration(self, value):
        self._configuration = value

    Configuration = property(fget=get_Configuration, fset=set_Configuration)

    def get_Icon(self):
        return str.Empty

    Icon = property(fget=get_Icon)

    def get_Content(self):
        return self

    Content = property(fget=get_Content)

    def get_ShowAs(self):
        return self.ShowAs.Normal

    ShowAs = property(fget=get_ShowAs)

    def codebox_TextChangedDelayed(self, sender, e):
        Custom = List[ExplorerItem]()
        HighlightingPluginSample.CLSyntaxHighlighter.Highlight(e.ChangedRange, Custom)

    def InitializeComponent(self):
        self._button1 = System.Windows.Forms.Button()
        self._button2 = System.Windows.Forms.Button()
        self.SuspendLayout()
        # 
        # button1
        # 
        self._button1.Dock = System.Windows.Forms.DockStyle.Fill
        self._button1.Font = System.Drawing.Font("Microsoft Sans Serif", 50f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((0)))
        self._button1.Location = System.Drawing.Point(0, 0)
        self._button1.Name = "button1"
        self._button1.Size = System.Drawing.Size(358, 257)
        self._button1.TabIndex = 0
        self._button1.Text = "Highlight"
        self._button1.UseVisualStyleBackColor = True
        self._button1.Click += self._button1_Click
        # 
        # button2
        # 
        self._button2.Dock = System.Windows.Forms.DockStyle.Bottom
        self._button2.Font = System.Drawing.Font("Microsoft Sans Serif", 8.25f)
        self._button2.Location = System.Drawing.Point(0, 257)
        self._button2.Name = "button2"
        self._button2.Size = System.Drawing.Size(358, 249)
        self._button2.TabIndex = 1
        self._button2.Text = "Insert Sample Text"
        self._button2.UseVisualStyleBackColor = True
        self._button2.Click += self._button2_Click
        # 
        # MainForm
        # 
        self._ClientSize = System.Drawing.Size(358, 506)
        self._Controls.Add(self._button1)
        self._Controls.Add(self._button2)
        self._Font = System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((0)))
        self._Name = "MainForm"
        self._ShowInTaskbar = False
        self._StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        self._Text = "OpenCL Highlighter Plugin"
        self._Load += self._MainForm_Load
        self.ResumeLayout(False)

    def MainForm_Load(self, sender, e):
        pass

    def button1_Click(self, sender, e):
        try:
        Main.ActiveEditor.codebox.TextChangedDelayed += self.codebox_TextChangedDelayed
            MessageBox.Show("Highlighter Initialized. Just Type in Text To Highlight")
        except Exception, ex:
            Console.WriteLine(ex.Message)
        finally:

    def button2_Click(self, sender, e):
        try:
           Main.ActiveEditor.codebox.InsertText("kernel void Mono(\n_global uchar4 *input,\nconst float2 file\n)\n\nint Main()\n{\nint coord=(int2)(get_global_id(0));\n}")
        except , :
        finally: