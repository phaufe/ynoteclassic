from System import *
from System.Collections.Generic import *
from System.ComponentModel import *
from System.Data import *
from System.Drawing import *
from System.Linq import *
from System.Text import *
from System.Windows.Forms import *
from SS.Ynote.Engine.Framework.Plugins.Interface import *
from System.Xml.Linq import *
from FastColoredTextBoxNS import *
from SS.Ynote.Classic import *
#========================================
#
#Wavy Style can be use in error checking
#you can make a plugin for error checking for any language
#
#This is just a sample
#
#========================================
class Form1(WeifenLuo.WinFormsUI.Docking.DockContent, IFormPlugin):
    """ <summary>
     Simple Spell Checker
     </summary>
    """
    def __init__(self):
        self._RedWavy = WavyLineStyle(100, Color.Red)
        self._configuration = XElement("WavyStylePluginConfig")
        self.InitializeComponent()
        self.Hide()
        self._Visible = False
        Main.ActiveEditor.codebox.TextChangedDelayed += self.codebox_TextChangedDelayed
        MessageBox.Show("Spell Checker Initialized!")

    def get_Title(self):
        return "WavyStylePlugin"

    Title = property(fget=get_Title)

    def get_Description(self):
        return "Wavy Style Sample Plugin"

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
        #get { return "C:\\Icons\\youricon.ico"; }
        return str.Empty

    Icon = property(fget=get_Icon)

    def get_Content(self):
        return self

    Content = property(fget=get_Content)

    def get_ShowAs(self):
        return self.ShowAs.Normal

    ShowAs = property(fget=get_ShowAs)

    def codebox_TextChangedDelayed(self, sender, e):
        SpellChecker = SpellChecker(Main.ActiveEditor.codebox, @"Dictionary.dic")
        SpellChecker.SpellCheck(sender)