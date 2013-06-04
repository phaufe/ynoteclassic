import clr

class MainForm(object):
	def __init__(self):
		# <summary>
		# Required designer variable.
		# </summary>
		self._components = None

	def Dispose(self, disposing):
		""" <summary>
		 Clean up any resources being used.
		 </summary>
		 <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		"""
		if disposing and (self._components != None):
			self._components.Dispose()
		self.Dispose(disposing)

	def InitializeComponent(self):
		""" <summary>
		 Required method for Designer support - do not modify
		 the contents of this method with the code editor.
		 </summary>
		"""
		resources = System.ComponentModel.ComponentResourceManager(clr.GetClrType(MainForm))
		self._txtmain = System.Windows.Forms.TextBox()
		self._toolStrip1 = System.Windows.Forms.ToolStrip()
		self._toolStripDropDownButton1 = System.Windows.Forms.ToolStripDropDownButton()
		self._newToolStripMenuItem1 = System.Windows.Forms.ToolStripMenuItem()
		self._openToolStripMenuItem1 = System.Windows.Forms.ToolStripMenuItem()
		self._importFromMainEditorToolStripMenuItem = System.Windows.Forms.ToolStripMenuItem()
		self._toolStripSeparator4 = System.Windows.Forms.ToolStripSeparator()
		self._saveAsToolStripMenuItem1 = System.Windows.Forms.ToolStripMenuItem()
		self._toolStripSeparator5 = System.Windows.Forms.ToolStripSeparator()
		self._exitToolStripMenuItem1 = System.Windows.Forms.ToolStripMenuItem()
		self._toolStripDropDownButton3 = System.Windows.Forms.ToolStripDropDownButton()
		self._undoToolStripMenuItem = System.Windows.Forms.ToolStripMenuItem()
		self._toolStripSeparator6 = System.Windows.Forms.ToolStripSeparator()
		self._cutToolStripMenuItem = System.Windows.Forms.ToolStripMenuItem()
		self._copyToolStripMenuItem = System.Windows.Forms.ToolStripMenuItem()
		self._pasteToolStripMenuItem = System.Windows.Forms.ToolStripMenuItem()
		self._toolStripSeparator7 = System.Windows.Forms.ToolStripSeparator()
		self._selectAllToolStripMenuItem = System.Windows.Forms.ToolStripMenuItem()
		self._clearAllToolStripMenuItem = System.Windows.Forms.ToolStripMenuItem()
		self._toolStripDropDownButton2 = System.Windows.Forms.ToolStripDropDownButton()
		self._fontToolStripMenuItem = System.Windows.Forms.ToolStripMenuItem()
		self._backColorToolStripMenuItem = System.Windows.Forms.ToolStripMenuItem()
		self._toolStripSplitButton1 = System.Windows.Forms.ToolStripDropDownMenu()
		self._newToolStripMenuItem = System.Windows.Forms.ToolStripMenuItem()
		self._openToolStripMenuItem = System.Windows.Forms.ToolStripMenuItem()
		self._toolStripSeparator1 = System.Windows.Forms.ToolStripSeparator()
		self._saveToolStripMenuItem = System.Windows.Forms.ToolStripMenuItem()
		self._saveAsToolStripMenuItem = System.Windows.Forms.ToolStripMenuItem()
		self._toolStripSeparator2 = System.Windows.Forms.ToolStripSeparator()
		self._importToolStripMenuItem = System.Windows.Forms.ToolStripMenuItem()
		self._toolStripSeparator3 = System.Windows.Forms.ToolStripSeparator()
		self._exitToolStripMenuItem = System.Windows.Forms.ToolStripMenuItem()
		self._toolStrip1.SuspendLayout()
		self._toolStripSplitButton1.SuspendLayout()
		self.SuspendLayout()
		# 
		# txtmain
		# 
		self._txtmain.AutoCompleteCustomSource.AddRange(Array[str](("Hello", "XML", "PHP", "Ynote", "C#")))
		self._txtmain.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystem
		self._txtmain.Dock = System.Windows.Forms.DockStyle.Fill
		self._txtmain.Location = System.Drawing.Point(0, 25)
		self._txtmain.Multiline = True
		self._txtmain.Name = "txtmain"
		self._txtmain.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal
		self._txtmain.Size = System.Drawing.Size(284, 318)
		self._txtmain.TabIndex = 0
		# 
		# toolStrip1
		# 
		self._toolStrip1.Items.AddRange(Array[System.Windows.Forms.ToolStripItem]((self._toolStripDropDownButton1, self._toolStripDropDownButton3, self._toolStripDropDownButton2)))
		self._toolStrip1.Location = System.Drawing.Point(0, 0)
		self._toolStrip1.Name = "toolStrip1"
		self._toolStrip1.Size = System.Drawing.Size(284, 25)
		self._toolStrip1.TabIndex = 1
		self._toolStrip1.Text = "toolStrip1"
		# 
		# toolStripDropDownButton1
		# 
		self._toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
		self._toolStripDropDownButton1.DropDownItems.AddRange(Array[System.Windows.Forms.ToolStripItem]((self._newToolStripMenuItem1, self._openToolStripMenuItem1, self._importFromMainEditorToolStripMenuItem, self._toolStripSeparator4, self._saveAsToolStripMenuItem1, self._toolStripSeparator5, self._exitToolStripMenuItem1)))
		self._toolStripDropDownButton1.Image = ((resources.GetObject("toolStripDropDownButton1.Image")))
		self._toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta
		self._toolStripDropDownButton1.Name = "toolStripDropDownButton1"
		self._toolStripDropDownButton1.Size = System.Drawing.Size(38, 22)
		self._toolStripDropDownButton1.Text = "File"
		# 
		# newToolStripMenuItem1
		# 
		self._newToolStripMenuItem1.Name = "newToolStripMenuItem1"
		self._newToolStripMenuItem1.Size = System.Drawing.Size(205, 22)
		self._newToolStripMenuItem1.Text = "New"
		# 
		# openToolStripMenuItem1
		# 
		self._openToolStripMenuItem1.Name = "openToolStripMenuItem1"
		self._openToolStripMenuItem1.Size = System.Drawing.Size(205, 22)
		self._openToolStripMenuItem1.Text = "Open"
		# 
		# importFromMainEditorToolStripMenuItem
		# 
		self._importFromMainEditorToolStripMenuItem.Name = "importFromMainEditorToolStripMenuItem"
		self._importFromMainEditorToolStripMenuItem.Size = System.Drawing.Size(205, 22)
		self._importFromMainEditorToolStripMenuItem.Text = "Import From Main Editor"
		# 
		# toolStripSeparator4
		# 
		self._toolStripSeparator4.Name = "toolStripSeparator4"
		self._toolStripSeparator4.Size = System.Drawing.Size(202, 6)
		# 
		# saveAsToolStripMenuItem1
		# 
		self._saveAsToolStripMenuItem1.Name = "saveAsToolStripMenuItem1"
		self._saveAsToolStripMenuItem1.Size = System.Drawing.Size(205, 22)
		self._saveAsToolStripMenuItem1.Text = "Save As"
		# 
		# toolStripSeparator5
		# 
		self._toolStripSeparator5.Name = "toolStripSeparator5"
		self._toolStripSeparator5.Size = System.Drawing.Size(202, 6)
		# 
		# exitToolStripMenuItem1
		# 
		self._exitToolStripMenuItem1.Name = "exitToolStripMenuItem1"
		self._exitToolStripMenuItem1.Size = System.Drawing.Size(205, 22)
		self._exitToolStripMenuItem1.Text = "Exit"
		self._exitToolStripMenuItem1.Click += self._exitToolStripMenuItem1_Click
		# 
		# toolStripDropDownButton3
		# 
		self._toolStripDropDownButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
		self._toolStripDropDownButton3.DropDownItems.AddRange(Array[System.Windows.Forms.ToolStripItem]((self._undoToolStripMenuItem, self._toolStripSeparator6, self._cutToolStripMenuItem, self._copyToolStripMenuItem, self._pasteToolStripMenuItem, self._toolStripSeparator7, self._selectAllToolStripMenuItem, self._clearAllToolStripMenuItem)))
		self._toolStripDropDownButton3.Image = ((resources.GetObject("toolStripDropDownButton3.Image")))
		self._toolStripDropDownButton3.ImageTransparentColor = System.Drawing.Color.Magenta
		self._toolStripDropDownButton3.Name = "toolStripDropDownButton3"
		self._toolStripDropDownButton3.Size = System.Drawing.Size(40, 22)
		self._toolStripDropDownButton3.Text = "Edit"
		# 
		# undoToolStripMenuItem
		# 
		self._undoToolStripMenuItem.Name = "undoToolStripMenuItem"
		self._undoToolStripMenuItem.ShortcutKeys = (((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Z)))
		self._undoToolStripMenuItem.Size = System.Drawing.Size(192, 22)
		self._undoToolStripMenuItem.Text = "Undo"
		# 
		# toolStripSeparator6
		# 
		self._toolStripSeparator6.Name = "toolStripSeparator6"
		self._toolStripSeparator6.Size = System.Drawing.Size(189, 6)
		# 
		# cutToolStripMenuItem
		# 
		self._cutToolStripMenuItem.Name = "cutToolStripMenuItem"
		self._cutToolStripMenuItem.ShortcutKeys = (((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)))
		self._cutToolStripMenuItem.Size = System.Drawing.Size(192, 22)
		self._cutToolStripMenuItem.Text = "Cut"
		# 
		# copyToolStripMenuItem
		# 
		self._copyToolStripMenuItem.Name = "copyToolStripMenuItem"
		self._copyToolStripMenuItem.ShortcutKeys = (((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)))
		self._copyToolStripMenuItem.Size = System.Drawing.Size(192, 22)
		self._copyToolStripMenuItem.Text = "Copy"
		# 
		# pasteToolStripMenuItem
		# 
		self._pasteToolStripMenuItem.Name = "pasteToolStripMenuItem"
		self._pasteToolStripMenuItem.ShortcutKeys = (((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)))
		self._pasteToolStripMenuItem.Size = System.Drawing.Size(192, 22)
		self._pasteToolStripMenuItem.Text = "Paste"
		# 
		# toolStripSeparator7
		# 
		self._toolStripSeparator7.Name = "toolStripSeparator7"
		self._toolStripSeparator7.Size = System.Drawing.Size(189, 6)
		# 
		# selectAllToolStripMenuItem
		# 
		self._selectAllToolStripMenuItem.Name = "selectAllToolStripMenuItem"
		self._selectAllToolStripMenuItem.ShortcutKeys = (((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)))
		self._selectAllToolStripMenuItem.Size = System.Drawing.Size(192, 22)
		self._selectAllToolStripMenuItem.Text = "Select All"
		# 
		# clearAllToolStripMenuItem
		# 
		self._clearAllToolStripMenuItem.Name = "clearAllToolStripMenuItem"
		self._clearAllToolStripMenuItem.ShortcutKeys = ((((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) | System.Windows.Forms.Keys.C)))
		self._clearAllToolStripMenuItem.Size = System.Drawing.Size(192, 22)
		self._clearAllToolStripMenuItem.Text = "Clear All"
		# 
		# toolStripDropDownButton2
		# 
		self._toolStripDropDownButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
		self._toolStripDropDownButton2.DropDownItems.AddRange(Array[System.Windows.Forms.ToolStripItem]((self._fontToolStripMenuItem, self._backColorToolStripMenuItem)))
		self._toolStripDropDownButton2.Image = ((resources.GetObject("toolStripDropDownButton2.Image")))
		self._toolStripDropDownButton2.ImageTransparentColor = System.Drawing.Color.Magenta
		self._toolStripDropDownButton2.Name = "toolStripDropDownButton2"
		self._toolStripDropDownButton2.Size = System.Drawing.Size(58, 22)
		self._toolStripDropDownButton2.Text = "Format"
		# 
		# fontToolStripMenuItem
		# 
		self._fontToolStripMenuItem.Name = "fontToolStripMenuItem"
		self._fontToolStripMenuItem.Size = System.Drawing.Size(152, 22)
		self._fontToolStripMenuItem.Text = "Font"
		# 
		# backColorToolStripMenuItem
		# 
		self._backColorToolStripMenuItem.Name = "backColorToolStripMenuItem"
		self._backColorToolStripMenuItem.Size = System.Drawing.Size(152, 22)
		self._backColorToolStripMenuItem.Text = "BackColor"
		# 
		# toolStripSplitButton1
		# 
		self._toolStripSplitButton1.Items.AddRange(Array[System.Windows.Forms.ToolStripItem]((self._newToolStripMenuItem, self._openToolStripMenuItem, self._toolStripSeparator1, self._saveToolStripMenuItem, self._saveAsToolStripMenuItem, self._toolStripSeparator2, self._importToolStripMenuItem, self._toolStripSeparator3, self._exitToolStripMenuItem)))
		self._toolStripSplitButton1.Name = "toolStripSplitButton1"
		self._toolStripSplitButton1.Size = System.Drawing.Size(206, 154)
		self._toolStripSplitButton1.Text = "File"
		# 
		# newToolStripMenuItem
		# 
		self._newToolStripMenuItem.Name = "newToolStripMenuItem"
		self._newToolStripMenuItem.Size = System.Drawing.Size(205, 22)
		self._newToolStripMenuItem.Text = "New"
		# 
		# openToolStripMenuItem
		# 
		self._openToolStripMenuItem.Name = "openToolStripMenuItem"
		self._openToolStripMenuItem.Size = System.Drawing.Size(205, 22)
		self._openToolStripMenuItem.Text = "Open"
		# 
		# toolStripSeparator1
		# 
		self._toolStripSeparator1.Name = "toolStripSeparator1"
		self._toolStripSeparator1.Size = System.Drawing.Size(202, 6)
		# 
		# saveToolStripMenuItem
		# 
		self._saveToolStripMenuItem.Name = "saveToolStripMenuItem"
		self._saveToolStripMenuItem.Size = System.Drawing.Size(205, 22)
		self._saveToolStripMenuItem.Text = "Save"
		# 
		# saveAsToolStripMenuItem
		# 
		self._saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem"
		self._saveAsToolStripMenuItem.Size = System.Drawing.Size(205, 22)
		self._saveAsToolStripMenuItem.Text = "Save As"
		# 
		# toolStripSeparator2
		# 
		self._toolStripSeparator2.Name = "toolStripSeparator2"
		self._toolStripSeparator2.Size = System.Drawing.Size(202, 6)
		# 
		# importToolStripMenuItem
		# 
		self._importToolStripMenuItem.Name = "importToolStripMenuItem"
		self._importToolStripMenuItem.Size = System.Drawing.Size(205, 22)
		self._importToolStripMenuItem.Text = "Import From Main Editor"
		# 
		# toolStripSeparator3
		# 
		self._toolStripSeparator3.Name = "toolStripSeparator3"
		self._toolStripSeparator3.Size = System.Drawing.Size(202, 6)
		# 
		# exitToolStripMenuItem
		# 
		self._exitToolStripMenuItem.Name = "exitToolStripMenuItem"
		self._exitToolStripMenuItem.Size = System.Drawing.Size(205, 22)
		self._exitToolStripMenuItem.Text = "Exit"
		# 
		# MainForm
		# 
		self._ClientSize = System.Drawing.Size(284, 343)
		self._Controls.Add(self._txtmain)
		self._Controls.Add(self._toolStrip1)
		self._Font = System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((0)))
		self._Name = "MainForm"
		self._ShowHint = WeifenLuo.WinFormsUI.Docking.DockState.DockRight
		self._Text = "Quick Note"
		self._toolStrip1.ResumeLayout(False)
		self._toolStrip1.PerformLayout()
		self._toolStripSplitButton1.ResumeLayout(False)
		self.ResumeLayout(False)
		self.PerformLayout()