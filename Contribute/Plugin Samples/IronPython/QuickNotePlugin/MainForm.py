from System import *
from System.IO import *
from System.Collections.Generic import *
from System.ComponentModel import *
from System.Data import *
from System.Drawing import *
from System.Linq import *
from System.Text import *
from System.Windows.Forms import *
from System.Xml.Linq import *
from SS.Ynote.Classic import *
from SS.Ynote.Engine.Framework.Plugins import *
from SS.Ynote.Engine.Framework.Plugins.Controls import *
from SS.Ynote.Engine.Framework.Plugins.Interface import *
from WeifenLuo.WinFormsUI.Docking import *

class MainForm(DockContent, IFormPlugin):
	def __init__(self):
		self._configuration = XElement("ThisFormConfig")
		self.InitializeComponent()
		self._newToolStripMenuItem1.Click += self.menuItem2_Click
		self._openToolStripMenuItem1.Click += self.menuItem3_Click
		self._importFromMainEditorToolStripMenuItem.Click += self.menuItem18_Click
		self._saveAsToolStripMenuItem1.Click += self.menuItem5_Click
		self._clearAllToolStripMenuItem.Click += self.menuItem2_Click
		self._cutToolStripMenuItem.Click += self.menuItem8_Click
		self._copyToolStripMenuItem.Click += self.menuItem9_Click
		self._pasteToolStripMenuItem.Click += self.menuItem10_Click
		self._selectAllToolStripMenuItem.Click += self.menuItem11_Click
		self._undoToolStripMenuItem.Click += self.menuItem7_Click
		self._fontToolStripMenuItem.Click += self.menuItem14_Click
		self._backColorToolStripMenuItem.Click += self.menuItem15_Click

	def get_Content(self):
		return self

	Content = property(fget=get_Content)

	def get_ShowAs(self):
		return self.ShowAs.Normal

	ShowAs = property(fget=get_ShowAs)

	def get_Title(self):
		return "QuickNotePlugin"

	Title = property(fget=get_Title)

	def get_Description(self):
		return "This is  a Quick Note Plugin for Ynote Classic"

	Description = property(fget=get_Description)

	def get_Group(self):
		return "Plugins"

	Group = property(fget=get_Group)

	def get_SubGroup(self):
		return "Note"

	SubGroup = property(fget=get_SubGroup)

	def get_Configuration(self):
		return self._configuration

	def set_Configuration(self, value):
		self._configuration = value

	Configuration = property(fget=get_Configuration, fset=set_Configuration)

	def get_Icon(self):
		#get { return "C:\\Icons\\Folder.ico"; }
		return str.Empty

	Icon = property(fget=get_Icon)

	def menuItem7_Click(self, sender, e):
		self._txtmain.Undo()

	def menuItem18_Click(self, sender, e):
		self._txtmain.Text = Main.UserText

	def menuItem2_Click(self, sender, e):
		self._txtmain.Clear()

	def menuItem3_Click(self, sender, e):
		o = System.Windows.Forms.OpenFileDialog()
		o.Filter = "Text Files (*.txt)|*.txt|All Files(*.*)|*.*"
		o.ShowDialog()
		txtmain.Text = File.ReadAllText(o.FileName, Encoding.Default)

	def menuItem8_Click(self, sender, e):
		self._txtmain.Cut()

	def menuItem9_Click(self, sender, e):
		self._txtmain.Copy()

	def menuItem10_Click(self, sender, e):
		self._txtmain.Paste()

	def menuItem11_Click(self, sender, e):
		self._txtmain.SelectAll()

	def menuItem14_Click(self, sender, e):
		f = System.Windows.Forms.FontDialog()
		f.ShowEffects = True
		f.ShowColor = True
		f.ShowDialog()
		txtmain.Font = f.Font
		txtmain.ForeColor = f.Color

	def menuItem15_Click(self, sender, e):
		c = System.Windows.Forms.ColorDialog()
		c.ShowDialog()
		self._txtmain.BackColor = c.Color

	def menuItem5_Click(self, sender, e):
		s = System.Windows.Forms.SaveFileDialog()
		s.Filter = "All Files (*.*)|*.*"
		s.ShowDialog()
		if s.FileName != "":
			File.WriteAllText(s.FileName, txtmain.Text, Encoding.Default)

	def menuItem19_Click(self, sender, e):
		self.Close()

	def exitToolStripMenuItem1_Click(self, sender, e):
		Application.Exit()