from System import *
from System.Collections.Generic import *
from System.ComponentModel import *
from System.Data import *
from System.Drawing import *
from System.Linq import *
from System.Text import *
from System.Windows.Forms import *
from SS.Ynote.Engine.Framework.Plugins.Interface import *
from WeifenLuo.WinFormsUI.Docking import *
from System.Xml.Linq import *

class MainForm(DockContent, IFormPlugin):
	def __init__(self):
		#IFormPlugin Inherits the IPlugin Interface
		self._configuration = XElement("XMLParserPluginConfig")
		self.InitializeComponent()
		self._ShowHint = DockState.DockRight

	def get_Title(self):
		return "XMLParserPlugin"

	Title = property(fget=get_Title)

	def get_Description(self):
		return "Sample XML Parser Plugin"

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

	def toolStripButton1_Click(self, sender, e):
		XmlTree.Xml = SS.Ynote.Classic.Main.ActiveEditor.codebox.Text