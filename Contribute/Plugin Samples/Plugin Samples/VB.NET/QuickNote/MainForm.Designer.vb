Namespace QuickNotePlugin
	Partial Public Class MainForm
		''' <summary>
		''' Required designer variable.
		''' </summary>
		Private components As System.ComponentModel.IContainer = Nothing

		''' <summary>
		''' Clean up any resources being used.
		''' </summary>
		''' <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		Protected Overrides Sub Dispose(ByVal disposing As Boolean)
			If disposing AndAlso (components IsNot Nothing) Then
				components.Dispose()
			End If
			MyBase.Dispose(disposing)
		End Sub

		#Region "Windows Form Designer generated code"

		''' <summary>
		''' Required method for Designer support - do not modify
		''' the contents of this method with the code editor.
		''' </summary>
		Private Sub InitializeComponent()
			Dim resources As New System.ComponentModel.ComponentResourceManager(GetType(MainForm))
			Me.txtmain = New System.Windows.Forms.TextBox()
			Me.toolStrip1 = New System.Windows.Forms.ToolStrip()
			Me.toolStripDropDownButton1 = New System.Windows.Forms.ToolStripDropDownButton()
			Me.newToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
			Me.openToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
			Me.importFromMainEditorToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
			Me.toolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator()
			Me.saveAsToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
			Me.toolStripSeparator5 = New System.Windows.Forms.ToolStripSeparator()
			Me.exitToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
			Me.toolStripDropDownButton3 = New System.Windows.Forms.ToolStripDropDownButton()
			Me.undoToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
			Me.toolStripSeparator6 = New System.Windows.Forms.ToolStripSeparator()
			Me.cutToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
			Me.copyToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
			Me.pasteToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
			Me.toolStripSeparator7 = New System.Windows.Forms.ToolStripSeparator()
			Me.selectAllToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
			Me.clearAllToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
			Me.toolStripDropDownButton2 = New System.Windows.Forms.ToolStripDropDownButton()
			Me.fontToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
			Me.backColorToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
			Me.toolStripSplitButton1 = New System.Windows.Forms.ToolStripDropDownMenu()
			Me.newToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
			Me.openToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
			Me.toolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
			Me.saveToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
			Me.saveAsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
			Me.toolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
			Me.importToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
			Me.toolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator()
			Me.exitToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
			Me.toolStrip1.SuspendLayout()
			Me.toolStripSplitButton1.SuspendLayout()
			Me.SuspendLayout()
			' 
			' txtmain
			' 
			Me.txtmain.AutoCompleteCustomSource.AddRange(New String() { "Hello", "XML", "PHP", "Ynote", "C#"})
			Me.txtmain.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystem
			Me.txtmain.Dock = System.Windows.Forms.DockStyle.Fill
			Me.txtmain.Location = New System.Drawing.Point(0, 25)
			Me.txtmain.Multiline = True
			Me.txtmain.Name = "txtmain"
			Me.txtmain.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal
			Me.txtmain.Size = New System.Drawing.Size(284, 318)
			Me.txtmain.TabIndex = 0
			' 
			' toolStrip1
			' 
			Me.toolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() { Me.toolStripDropDownButton1, Me.toolStripDropDownButton3, Me.toolStripDropDownButton2})
			Me.toolStrip1.Location = New System.Drawing.Point(0, 0)
			Me.toolStrip1.Name = "toolStrip1"
			Me.toolStrip1.Size = New System.Drawing.Size(284, 25)
			Me.toolStrip1.TabIndex = 1
			Me.toolStrip1.Text = "toolStrip1"
			' 
			' toolStripDropDownButton1
			' 
			Me.toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
			Me.toolStripDropDownButton1.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() { Me.newToolStripMenuItem1, Me.openToolStripMenuItem1, Me.importFromMainEditorToolStripMenuItem, Me.toolStripSeparator4, Me.saveAsToolStripMenuItem1, Me.toolStripSeparator5, Me.exitToolStripMenuItem1})
			Me.toolStripDropDownButton1.Image = (CType(resources.GetObject("toolStripDropDownButton1.Image"), System.Drawing.Image))
			Me.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta
			Me.toolStripDropDownButton1.Name = "toolStripDropDownButton1"
			Me.toolStripDropDownButton1.Size = New System.Drawing.Size(38, 22)
			Me.toolStripDropDownButton1.Text = "File"
			' 
			' newToolStripMenuItem1
			' 
			Me.newToolStripMenuItem1.Name = "newToolStripMenuItem1"
			Me.newToolStripMenuItem1.Size = New System.Drawing.Size(205, 22)
			Me.newToolStripMenuItem1.Text = "New"
			' 
			' openToolStripMenuItem1
			' 
			Me.openToolStripMenuItem1.Name = "openToolStripMenuItem1"
			Me.openToolStripMenuItem1.Size = New System.Drawing.Size(205, 22)
			Me.openToolStripMenuItem1.Text = "Open"
			' 
			' importFromMainEditorToolStripMenuItem
			' 
			Me.importFromMainEditorToolStripMenuItem.Name = "importFromMainEditorToolStripMenuItem"
			Me.importFromMainEditorToolStripMenuItem.Size = New System.Drawing.Size(205, 22)
			Me.importFromMainEditorToolStripMenuItem.Text = "Import From Main Editor"
			' 
			' toolStripSeparator4
			' 
			Me.toolStripSeparator4.Name = "toolStripSeparator4"
			Me.toolStripSeparator4.Size = New System.Drawing.Size(202, 6)
			' 
			' saveAsToolStripMenuItem1
			' 
			Me.saveAsToolStripMenuItem1.Name = "saveAsToolStripMenuItem1"
			Me.saveAsToolStripMenuItem1.Size = New System.Drawing.Size(205, 22)
			Me.saveAsToolStripMenuItem1.Text = "Save As"
			' 
			' toolStripSeparator5
			' 
			Me.toolStripSeparator5.Name = "toolStripSeparator5"
			Me.toolStripSeparator5.Size = New System.Drawing.Size(202, 6)
			' 
			' exitToolStripMenuItem1
			' 
			Me.exitToolStripMenuItem1.Name = "exitToolStripMenuItem1"
			Me.exitToolStripMenuItem1.Size = New System.Drawing.Size(205, 22)
			Me.exitToolStripMenuItem1.Text = "Exit"
'			Me.exitToolStripMenuItem1.Click += New System.EventHandler(Me.exitToolStripMenuItem1_Click)
			' 
			' toolStripDropDownButton3
			' 
			Me.toolStripDropDownButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
			Me.toolStripDropDownButton3.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() { Me.undoToolStripMenuItem, Me.toolStripSeparator6, Me.cutToolStripMenuItem, Me.copyToolStripMenuItem, Me.pasteToolStripMenuItem, Me.toolStripSeparator7, Me.selectAllToolStripMenuItem, Me.clearAllToolStripMenuItem})
			Me.toolStripDropDownButton3.Image = (CType(resources.GetObject("toolStripDropDownButton3.Image"), System.Drawing.Image))
			Me.toolStripDropDownButton3.ImageTransparentColor = System.Drawing.Color.Magenta
			Me.toolStripDropDownButton3.Name = "toolStripDropDownButton3"
			Me.toolStripDropDownButton3.Size = New System.Drawing.Size(40, 22)
			Me.toolStripDropDownButton3.Text = "Edit"
			' 
			' undoToolStripMenuItem
			' 
			Me.undoToolStripMenuItem.Name = "undoToolStripMenuItem"
			Me.undoToolStripMenuItem.ShortcutKeys = (CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Z), System.Windows.Forms.Keys))
			Me.undoToolStripMenuItem.Size = New System.Drawing.Size(192, 22)
			Me.undoToolStripMenuItem.Text = "Undo"
			' 
			' toolStripSeparator6
			' 
			Me.toolStripSeparator6.Name = "toolStripSeparator6"
			Me.toolStripSeparator6.Size = New System.Drawing.Size(189, 6)
			' 
			' cutToolStripMenuItem
			' 
			Me.cutToolStripMenuItem.Name = "cutToolStripMenuItem"
			Me.cutToolStripMenuItem.ShortcutKeys = (CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.X), System.Windows.Forms.Keys))
			Me.cutToolStripMenuItem.Size = New System.Drawing.Size(192, 22)
			Me.cutToolStripMenuItem.Text = "Cut"
			' 
			' copyToolStripMenuItem
			' 
			Me.copyToolStripMenuItem.Name = "copyToolStripMenuItem"
			Me.copyToolStripMenuItem.ShortcutKeys = (CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.C), System.Windows.Forms.Keys))
			Me.copyToolStripMenuItem.Size = New System.Drawing.Size(192, 22)
			Me.copyToolStripMenuItem.Text = "Copy"
			' 
			' pasteToolStripMenuItem
			' 
			Me.pasteToolStripMenuItem.Name = "pasteToolStripMenuItem"
			Me.pasteToolStripMenuItem.ShortcutKeys = (CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.V), System.Windows.Forms.Keys))
			Me.pasteToolStripMenuItem.Size = New System.Drawing.Size(192, 22)
			Me.pasteToolStripMenuItem.Text = "Paste"
			' 
			' toolStripSeparator7
			' 
			Me.toolStripSeparator7.Name = "toolStripSeparator7"
			Me.toolStripSeparator7.Size = New System.Drawing.Size(189, 6)
			' 
			' selectAllToolStripMenuItem
			' 
			Me.selectAllToolStripMenuItem.Name = "selectAllToolStripMenuItem"
			Me.selectAllToolStripMenuItem.ShortcutKeys = (CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.A), System.Windows.Forms.Keys))
			Me.selectAllToolStripMenuItem.Size = New System.Drawing.Size(192, 22)
			Me.selectAllToolStripMenuItem.Text = "Select All"
			' 
			' clearAllToolStripMenuItem
			' 
			Me.clearAllToolStripMenuItem.Name = "clearAllToolStripMenuItem"
			Me.clearAllToolStripMenuItem.ShortcutKeys = (CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift) Or System.Windows.Forms.Keys.C), System.Windows.Forms.Keys))
			Me.clearAllToolStripMenuItem.Size = New System.Drawing.Size(192, 22)
			Me.clearAllToolStripMenuItem.Text = "Clear All"
			' 
			' toolStripDropDownButton2
			' 
			Me.toolStripDropDownButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
			Me.toolStripDropDownButton2.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() { Me.fontToolStripMenuItem, Me.backColorToolStripMenuItem})
			Me.toolStripDropDownButton2.Image = (CType(resources.GetObject("toolStripDropDownButton2.Image"), System.Drawing.Image))
			Me.toolStripDropDownButton2.ImageTransparentColor = System.Drawing.Color.Magenta
			Me.toolStripDropDownButton2.Name = "toolStripDropDownButton2"
			Me.toolStripDropDownButton2.Size = New System.Drawing.Size(58, 22)
			Me.toolStripDropDownButton2.Text = "Format"
			' 
			' fontToolStripMenuItem
			' 
			Me.fontToolStripMenuItem.Name = "fontToolStripMenuItem"
			Me.fontToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
			Me.fontToolStripMenuItem.Text = "Font"
			' 
			' backColorToolStripMenuItem
			' 
			Me.backColorToolStripMenuItem.Name = "backColorToolStripMenuItem"
			Me.backColorToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
			Me.backColorToolStripMenuItem.Text = "BackColor"
			' 
			' toolStripSplitButton1
			' 
			Me.toolStripSplitButton1.Items.AddRange(New System.Windows.Forms.ToolStripItem() { Me.newToolStripMenuItem, Me.openToolStripMenuItem, Me.toolStripSeparator1, Me.saveToolStripMenuItem, Me.saveAsToolStripMenuItem, Me.toolStripSeparator2, Me.importToolStripMenuItem, Me.toolStripSeparator3, Me.exitToolStripMenuItem})
			Me.toolStripSplitButton1.Name = "toolStripSplitButton1"
			Me.toolStripSplitButton1.Size = New System.Drawing.Size(206, 154)
			Me.toolStripSplitButton1.Text = "File"
			' 
			' newToolStripMenuItem
			' 
			Me.newToolStripMenuItem.Name = "newToolStripMenuItem"
			Me.newToolStripMenuItem.Size = New System.Drawing.Size(205, 22)
			Me.newToolStripMenuItem.Text = "New"
			' 
			' openToolStripMenuItem
			' 
			Me.openToolStripMenuItem.Name = "openToolStripMenuItem"
			Me.openToolStripMenuItem.Size = New System.Drawing.Size(205, 22)
			Me.openToolStripMenuItem.Text = "Open"
			' 
			' toolStripSeparator1
			' 
			Me.toolStripSeparator1.Name = "toolStripSeparator1"
			Me.toolStripSeparator1.Size = New System.Drawing.Size(202, 6)
			' 
			' saveToolStripMenuItem
			' 
			Me.saveToolStripMenuItem.Name = "saveToolStripMenuItem"
			Me.saveToolStripMenuItem.Size = New System.Drawing.Size(205, 22)
			Me.saveToolStripMenuItem.Text = "Save"
			' 
			' saveAsToolStripMenuItem
			' 
			Me.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem"
			Me.saveAsToolStripMenuItem.Size = New System.Drawing.Size(205, 22)
			Me.saveAsToolStripMenuItem.Text = "Save As"
			' 
			' toolStripSeparator2
			' 
			Me.toolStripSeparator2.Name = "toolStripSeparator2"
			Me.toolStripSeparator2.Size = New System.Drawing.Size(202, 6)
			' 
			' importToolStripMenuItem
			' 
			Me.importToolStripMenuItem.Name = "importToolStripMenuItem"
			Me.importToolStripMenuItem.Size = New System.Drawing.Size(205, 22)
			Me.importToolStripMenuItem.Text = "Import From Main Editor"
			' 
			' toolStripSeparator3
			' 
			Me.toolStripSeparator3.Name = "toolStripSeparator3"
			Me.toolStripSeparator3.Size = New System.Drawing.Size(202, 6)
			' 
			' exitToolStripMenuItem
			' 
			Me.exitToolStripMenuItem.Name = "exitToolStripMenuItem"
			Me.exitToolStripMenuItem.Size = New System.Drawing.Size(205, 22)
			Me.exitToolStripMenuItem.Text = "Exit"
			' 
			' MainForm
			' 
			Me.ClientSize = New System.Drawing.Size(284, 343)
			Me.Controls.Add(Me.txtmain)
			Me.Controls.Add(Me.toolStrip1)
			Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, (CByte(0)))
			Me.Name = "MainForm"
			Me.ShowHint = WeifenLuo.WinFormsUI.Docking.DockState.DockRight
			Me.Text = "Quick Note"
			Me.toolStrip1.ResumeLayout(False)
			Me.toolStrip1.PerformLayout()
			Me.toolStripSplitButton1.ResumeLayout(False)
			Me.ResumeLayout(False)
			Me.PerformLayout()

		End Sub
		#End Region

		Private txtmain As System.Windows.Forms.TextBox
		Private toolStrip1 As System.Windows.Forms.ToolStrip
		Private toolStripSplitButton1 As System.Windows.Forms.ToolStripDropDownMenu
		Private newToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
		Private openToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
		Private toolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
		Private saveToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
		Private saveAsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
		Private toolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
		Private importToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
		Private toolStripSeparator3 As System.Windows.Forms.ToolStripSeparator
		Private exitToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
		Private toolStripDropDownButton1 As System.Windows.Forms.ToolStripDropDownButton
		Private newToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
		Private openToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
		Private importFromMainEditorToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
		Private toolStripSeparator4 As System.Windows.Forms.ToolStripSeparator
		Private saveAsToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
		Private toolStripSeparator5 As System.Windows.Forms.ToolStripSeparator
		Private WithEvents exitToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
		Private toolStripDropDownButton3 As System.Windows.Forms.ToolStripDropDownButton
		Private undoToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
		Private toolStripSeparator6 As System.Windows.Forms.ToolStripSeparator
		Private cutToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
		Private copyToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
		Private pasteToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
		Private toolStripSeparator7 As System.Windows.Forms.ToolStripSeparator
		Private selectAllToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
		Private clearAllToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
		Private toolStripDropDownButton2 As System.Windows.Forms.ToolStripDropDownButton
		Private fontToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
		Private backColorToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
	End Class
End Namespace