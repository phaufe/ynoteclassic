from System import *
from System.Drawing import *
from System.Collections import *
from System.ComponentModel import *
from System.Windows.Forms import *
from System.Data import *
from Microsoft.CSharp import *
from System.CodeDom.Compiler import *
from System.Reflection import *
from System.Diagnostics import *
from SS.Ynote.Engine.Framework.Plugins.Interface import *
from System.Xml.Linq import *
#-------------------------------------------------
#
#Simple C# Compiler Plugin
#
#Make a Compiler Plugin For Any languge using Process Info
#
#--------------------------------------------
class Form1(WeifenLuo.WinFormsUI.Docking.DockContent, IFormPlugin):
	def __init__(self): # <summary>
		# Required method for Designer support - do not modify
		# the contents of this method with the code editor.
		# </summary>
		# 
		# button1
		# 
		# 
		# label2
		# 
		# 
		# appName
		# 
		# 
		# label1
		# 
		# 
		# mainClass
		# 
		# 
		# includeDebug
		# 
		# 
		# Form1
		# 
		self._configuration = XElement("HighlighterPluginConfig")
		self.InitializeComponent()

	def Dispose(self, disposing):
		if disposing:
			if self._components != None:
				self._components.Dispose()
		self.Dispose(disposing)

	def InitializeComponent(self):
		self._button1 = System.Windows.Forms.Button()
		self._label2 = System.Windows.Forms.Label()
		self._appName = System.Windows.Forms.TextBox()
		self._label1 = System.Windows.Forms.Label()
		self._mainClass = System.Windows.Forms.TextBox()
		self._includeDebug = System.Windows.Forms.CheckBox()
		self.SuspendLayout()
		self._button1.BackColor = System.Drawing.SystemColors.Control
		self._button1.Location = System.Drawing.Point(114, 179)
		self._button1.Name = "button1"
		self._button1.Size = System.Drawing.Size(160, 24)
		self._button1.TabIndex = 1
		self._button1.Text = "&Compile and Execute"
		self._button1.UseVisualStyleBackColor = False
		self._button1.Click += self._button1_Click
		self._label2.Location = System.Drawing.Point(31, 65)
		self._label2.Name = "label2"
		self._label2.Size = System.Drawing.Size(104, 23)
		self._label2.TabIndex = 5
		self._label2.Text = "Main Class Name"
		self._appName.Location = System.Drawing.Point(137, 26)
		self._appName.Name = "appName"
		self._appName.Size = System.Drawing.Size(152, 20)
		self._appName.TabIndex = 2
		self._appName.Text = "Application.exe"
		self._label1.Location = System.Drawing.Point(31, 29)
		self._label1.Name = "label1"
		self._label1.Size = System.Drawing.Size(100, 23)
		self._label1.TabIndex = 4
		self._label1.Text = "OutputFileName"
		self._mainClass.Location = System.Drawing.Point(137, 68)
		self._mainClass.Name = "mainClass"
		self._mainClass.Size = System.Drawing.Size(152, 20)
		self._mainClass.TabIndex = 3
		self._mainClass.Text = "SS.Ynote.Classic.Main"
		self._includeDebug.Location = System.Drawing.Point(34, 111)
		self._includeDebug.Name = "includeDebug"
		self._includeDebug.Size = System.Drawing.Size(160, 24)
		self._includeDebug.TabIndex = 7
		self._includeDebug.Text = "Include Debug Info"
		self._AutoScaleBaseSize = System.Drawing.Size(5, 13)
		self._ClientSize = System.Drawing.Size(316, 233)
		self._Controls.Add(self._includeDebug)
		self._Controls.Add(self._label2)
		self._Controls.Add(self._label1)
		self._Controls.Add(self._mainClass)
		self._Controls.Add(self._appName)
		self._Controls.Add(self._button1)
		self._Font = System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((0)))
		self._FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
		self._Name = "Form1"
		self._StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
		self._Text = "C# Compiler Plugin"
		self.ResumeLayout(False)
		self.PerformLayout()

	def get_Title(self):
		return "C# Compiler Plugin"

	Title = property(fget=get_Title)

	def get_Description(self):
		return "Sample Compiler Plugin"

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
		#eg : C:\\PluginIcon.ico
		return str.Empty

	Icon = property(fget=get_Icon)

	def get_Content(self):
		return self

	Content = property(fget=get_Content)

	def get_ShowAs(self):
		return self.ShowAs.Dialog

	ShowAs = property(fget=get_ShowAs)

	def menuItem4_Click(self, sender, e):
		self.Dispose()
		Application.Exit()

	def menuItem3_Click(self, sender, e):
		System.Windows.Forms.MessageBox.Show(self, "CSharp sample compiler :)", "CodeProject Rulez")

	def button1_Click(self, sender, e):
		codeProvider = CSharpCodeProvider()
		# For Visual Basic Compiler 
		#Microsoft.VisualBasic.VBCodeProvider
		compiler = codeProvider.CreateCompiler()
		parameters = CompilerParameters()
		parameters.GenerateExecutable = True
		if self._appName.Text == "":
			System.Windows.Forms.MessageBox.Show(self, "Application name cannot be empty")
			return 
		parameters.OutputAssembly = self._appName.Text.ToString()
		if self._mainClass.Text.ToString() == "":
			System.Windows.Forms.MessageBox.Show(self, "Main Class Name cannot be empty")
			return 
		parameters.MainClass = self._mainClass.Text.ToString()
		parameters.IncludeDebugInformation = self._includeDebug.Checked
		enumerator = AppDomain.CurrentDomain.GetAssemblies().GetEnumerator()
		while enumerator.MoveNext():
			asm = enumerator.Current
			parameters.ReferencedAssemblies.Add(asm.Location)
		code = SS.Ynote.Classic.Main.ActiveFastColoredTextBox.Text
		results = compiler.CompileAssemblyFromSource(parameters, code)
		if results.Errors.Count > 0:
			errors = "Compilation failed:\n"
			enumerator = results.Errors.GetEnumerator()
			while enumerator.MoveNext():
				err = enumerator.Current
				errors += err.ToString() + "\n"
			System.Windows.Forms.MessageBox.Show(self, errors, "There were compilation errors")
		else: # try to execute application
			try:
				if not System.IO.File.Exists(self._appName.Text.ToString()):
					MessageBox.Show(String.Format("Can't find {0}", self._appName), "Can't execute.", MessageBoxButtons.OK, MessageBoxIcon.Error)
					return 
				pInfo = ProcessStartInfo(self._appName.Text.ToString())
				Process.Start(pInfo)
			except Exception, ex:
				MessageBox.Show(String.Format("Error while executing {0}", self._appName) + ex.ToString(), "Can't execute.", MessageBoxButtons.OK, MessageBoxIcon.Error)
			finally: