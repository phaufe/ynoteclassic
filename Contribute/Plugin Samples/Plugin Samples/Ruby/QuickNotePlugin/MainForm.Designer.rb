require "mscorlib"

module QuickNotePlugin
	class MainForm
		def initialize()
			# <summary>
			# Required designer variable.
			# </summary>
			@components = nil
		end

		# <summary>
		# Clean up any resources being used.
		# </summary>
		# <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		def Dispose(disposing)
			if disposing and (@components != nil) then
				@components.Dispose()
			end
			self.Dispose(disposing)
		end

		# <summary>
		# Required method for Designer support - do not modify
		# the contents of this method with the code editor.
		# </summary>
		def InitializeComponent()
			resources = System.ComponentModel.ComponentResourceManager.new(MainForm.to_clr_type)
			self.@txtmain = System.Windows.Forms.TextBox.new()
			self.@toolStrip1 = System.Windows.Forms.ToolStrip.new()
			self.@toolStripDropDownButton1 = System.Windows.Forms.ToolStripDropDownButton.new()
			self.@newToolStripMenuItem1 = System.Windows.Forms.ToolStripMenuItem.new()
			self.@openToolStripMenuItem1 = System.Windows.Forms.ToolStripMenuItem.new()
			self.@importFromMainEditorToolStripMenuItem = System.Windows.Forms.ToolStripMenuItem.new()
			self.@toolStripSeparator4 = System.Windows.Forms.ToolStripSeparator.new()
			self.@saveAsToolStripMenuItem1 = System.Windows.Forms.ToolStripMenuItem.new()
			self.@toolStripSeparator5 = System.Windows.Forms.ToolStripSeparator.new()
			self.@exitToolStripMenuItem1 = System.Windows.Forms.ToolStripMenuItem.new()
			self.@toolStripDropDownButton3 = System.Windows.Forms.ToolStripDropDownButton.new()
			self.@undoToolStripMenuItem = System.Windows.Forms.ToolStripMenuItem.new()
			self.@toolStripSeparator6 = System.Windows.Forms.ToolStripSeparator.new()
			self.@cutToolStripMenuItem = System.Windows.Forms.ToolStripMenuItem.new()
			self.@copyToolStripMenuItem = System.Windows.Forms.ToolStripMenuItem.new()
			self.@pasteToolStripMenuItem = System.Windows.Forms.ToolStripMenuItem.new()
			self.@toolStripSeparator7 = System.Windows.Forms.ToolStripSeparator.new()
			self.@selectAllToolStripMenuItem = System.Windows.Forms.ToolStripMenuItem.new()
			self.@clearAllToolStripMenuItem = System.Windows.Forms.ToolStripMenuItem.new()
			self.@toolStripDropDownButton2 = System.Windows.Forms.ToolStripDropDownButton.new()
			self.@fontToolStripMenuItem = System.Windows.Forms.ToolStripMenuItem.new()
			self.@backColorToolStripMenuItem = System.Windows.Forms.ToolStripMenuItem.new()
			self.@toolStripSplitButton1 = System.Windows.Forms.ToolStripDropDownMenu.new()
			self.@newToolStripMenuItem = System.Windows.Forms.ToolStripMenuItem.new()
			self.@openToolStripMenuItem = System.Windows.Forms.ToolStripMenuItem.new()
			self.@toolStripSeparator1 = System.Windows.Forms.ToolStripSeparator.new()
			self.@saveToolStripMenuItem = System.Windows.Forms.ToolStripMenuItem.new()
			self.@saveAsToolStripMenuItem = System.Windows.Forms.ToolStripMenuItem.new()
			self.@toolStripSeparator2 = System.Windows.Forms.ToolStripSeparator.new()
			self.@importToolStripMenuItem = System.Windows.Forms.ToolStripMenuItem.new()
			self.@toolStripSeparator3 = System.Windows.Forms.ToolStripSeparator.new()
			self.@exitToolStripMenuItem = System.Windows.Forms.ToolStripMenuItem.new()
			self.@toolStrip1.SuspendLayout()
			self.@toolStripSplitButton1.SuspendLayout()
			self.SuspendLayout()
			# 
			# txtmain
			# 
			self.@txtmain.AutoCompleteCustomSource.AddRange(Array[System::String].new(["Hello", "XML", "PHP", "Ynote", "C#"]))
			self.@txtmain.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystem
			self.@txtmain.Dock = System.Windows.Forms.DockStyle.Fill
			self.@txtmain.Location = System.Drawing.Point.new(0, 25)
			self.@txtmain.Multiline = true
			self.@txtmain.Name = "txtmain"
			self.@txtmain.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal
			self.@txtmain.Size = System.Drawing.Size.new(284, 318)
			self.@txtmain.TabIndex = 0
			# 
			# toolStrip1
			# 
			self.@toolStrip1.Items.AddRange(Array[System::Windows::Forms::ToolStripItem].new([self.@toolStripDropDownButton1, self.@toolStripDropDownButton3, self.@toolStripDropDownButton2]))
			self.@toolStrip1.Location = System.Drawing.Point.new(0, 0)
			self.@toolStrip1.Name = "toolStrip1"
			self.@toolStrip1.Size = System.Drawing.Size.new(284, 25)
			self.@toolStrip1.TabIndex = 1
			self.@toolStrip1.Text = "toolStrip1"
			# 
			# toolStripDropDownButton1
			# 
			self.@toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
			self.@toolStripDropDownButton1.DropDownItems.AddRange(Array[System::Windows::Forms::ToolStripItem].new([self.@newToolStripMenuItem1, self.@openToolStripMenuItem1, self.@importFromMainEditorToolStripMenuItem, self.@toolStripSeparator4, self.@saveAsToolStripMenuItem1, self.@toolStripSeparator5, self.@exitToolStripMenuItem1]))
			self.@toolStripDropDownButton1.Image = ((resources.GetObject("toolStripDropDownButton1.Image")))
			self.@toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta
			self.@toolStripDropDownButton1.Name = "toolStripDropDownButton1"
			self.@toolStripDropDownButton1.Size = System.Drawing.Size.new(38, 22)
			self.@toolStripDropDownButton1.Text = "File"
			# 
			# newToolStripMenuItem1
			# 
			self.@newToolStripMenuItem1.Name = "newToolStripMenuItem1"
			self.@newToolStripMenuItem1.Size = System.Drawing.Size.new(205, 22)
			self.@newToolStripMenuItem1.Text = "New"
			# 
			# openToolStripMenuItem1
			# 
			self.@openToolStripMenuItem1.Name = "openToolStripMenuItem1"
			self.@openToolStripMenuItem1.Size = System.Drawing.Size.new(205, 22)
			self.@openToolStripMenuItem1.Text = "Open"
			# 
			# importFromMainEditorToolStripMenuItem
			# 
			self.@importFromMainEditorToolStripMenuItem.Name = "importFromMainEditorToolStripMenuItem"
			self.@importFromMainEditorToolStripMenuItem.Size = System.Drawing.Size.new(205, 22)
			self.@importFromMainEditorToolStripMenuItem.Text = "Import From Main Editor"
			# 
			# toolStripSeparator4
			# 
			self.@toolStripSeparator4.Name = "toolStripSeparator4"
			self.@toolStripSeparator4.Size = System.Drawing.Size.new(202, 6)
			# 
			# saveAsToolStripMenuItem1
			# 
			self.@saveAsToolStripMenuItem1.Name = "saveAsToolStripMenuItem1"
			self.@saveAsToolStripMenuItem1.Size = System.Drawing.Size.new(205, 22)
			self.@saveAsToolStripMenuItem1.Text = "Save As"
			# 
			# toolStripSeparator5
			# 
			self.@toolStripSeparator5.Name = "toolStripSeparator5"
			self.@toolStripSeparator5.Size = System.Drawing.Size.new(202, 6)
			# 
			# exitToolStripMenuItem1
			# 
			self.@exitToolStripMenuItem1.Name = "exitToolStripMenuItem1"
			self.@exitToolStripMenuItem1.Size = System.Drawing.Size.new(205, 22)
			self.@exitToolStripMenuItem1.Text = "Exit"
			self.@exitToolStripMenuItem1.Click { |sender, e| self.@exitToolStripMenuItem1_Click(sender, e) }
			# 
			# toolStripDropDownButton3
			# 
			self.@toolStripDropDownButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
			self.@toolStripDropDownButton3.DropDownItems.AddRange(Array[System::Windows::Forms::ToolStripItem].new([self.@undoToolStripMenuItem, self.@toolStripSeparator6, self.@cutToolStripMenuItem, self.@copyToolStripMenuItem, self.@pasteToolStripMenuItem, self.@toolStripSeparator7, self.@selectAllToolStripMenuItem, self.@clearAllToolStripMenuItem]))
			self.@toolStripDropDownButton3.Image = ((resources.GetObject("toolStripDropDownButton3.Image")))
			self.@toolStripDropDownButton3.ImageTransparentColor = System.Drawing.Color.Magenta
			self.@toolStripDropDownButton3.Name = "toolStripDropDownButton3"
			self.@toolStripDropDownButton3.Size = System.Drawing.Size.new(40, 22)
			self.@toolStripDropDownButton3.Text = "Edit"
			# 
			# undoToolStripMenuItem
			# 
			self.@undoToolStripMenuItem.Name = "undoToolStripMenuItem"
			self.@undoToolStripMenuItem.ShortcutKeys = (((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Z)))
			self.@undoToolStripMenuItem.Size = System.Drawing.Size.new(192, 22)
			self.@undoToolStripMenuItem.Text = "Undo"
			# 
			# toolStripSeparator6
			# 
			self.@toolStripSeparator6.Name = "toolStripSeparator6"
			self.@toolStripSeparator6.Size = System.Drawing.Size.new(189, 6)
			# 
			# cutToolStripMenuItem
			# 
			self.@cutToolStripMenuItem.Name = "cutToolStripMenuItem"
			self.@cutToolStripMenuItem.ShortcutKeys = (((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)))
			self.@cutToolStripMenuItem.Size = System.Drawing.Size.new(192, 22)
			self.@cutToolStripMenuItem.Text = "Cut"
			# 
			# copyToolStripMenuItem
			# 
			self.@copyToolStripMenuItem.Name = "copyToolStripMenuItem"
			self.@copyToolStripMenuItem.ShortcutKeys = (((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)))
			self.@copyToolStripMenuItem.Size = System.Drawing.Size.new(192, 22)
			self.@copyToolStripMenuItem.Text = "Copy"
			# 
			# pasteToolStripMenuItem
			# 
			self.@pasteToolStripMenuItem.Name = "pasteToolStripMenuItem"
			self.@pasteToolStripMenuItem.ShortcutKeys = (((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)))
			self.@pasteToolStripMenuItem.Size = System.Drawing.Size.new(192, 22)
			self.@pasteToolStripMenuItem.Text = "Paste"
			# 
			# toolStripSeparator7
			# 
			self.@toolStripSeparator7.Name = "toolStripSeparator7"
			self.@toolStripSeparator7.Size = System.Drawing.Size.new(189, 6)
			# 
			# selectAllToolStripMenuItem
			# 
			self.@selectAllToolStripMenuItem.Name = "selectAllToolStripMenuItem"
			self.@selectAllToolStripMenuItem.ShortcutKeys = (((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)))
			self.@selectAllToolStripMenuItem.Size = System.Drawing.Size.new(192, 22)
			self.@selectAllToolStripMenuItem.Text = "Select All"
			# 
			# clearAllToolStripMenuItem
			# 
			self.@clearAllToolStripMenuItem.Name = "clearAllToolStripMenuItem"
			self.@clearAllToolStripMenuItem.ShortcutKeys = ((((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) | System.Windows.Forms.Keys.C)))
			self.@clearAllToolStripMenuItem.Size = System.Drawing.Size.new(192, 22)
			self.@clearAllToolStripMenuItem.Text = "Clear All"
			# 
			# toolStripDropDownButton2
			# 
			self.@toolStripDropDownButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
			self.@toolStripDropDownButton2.DropDownItems.AddRange(Array[System::Windows::Forms::ToolStripItem].new([self.@fontToolStripMenuItem, self.@backColorToolStripMenuItem]))
			self.@toolStripDropDownButton2.Image = ((resources.GetObject("toolStripDropDownButton2.Image")))
			self.@toolStripDropDownButton2.ImageTransparentColor = System.Drawing.Color.Magenta
			self.@toolStripDropDownButton2.Name = "toolStripDropDownButton2"
			self.@toolStripDropDownButton2.Size = System.Drawing.Size.new(58, 22)
			self.@toolStripDropDownButton2.Text = "Format"
			# 
			# fontToolStripMenuItem
			# 
			self.@fontToolStripMenuItem.Name = "fontToolStripMenuItem"
			self.@fontToolStripMenuItem.Size = System.Drawing.Size.new(152, 22)
			self.@fontToolStripMenuItem.Text = "Font"
			# 
			# backColorToolStripMenuItem
			# 
			self.@backColorToolStripMenuItem.Name = "backColorToolStripMenuItem"
			self.@backColorToolStripMenuItem.Size = System.Drawing.Size.new(152, 22)
			self.@backColorToolStripMenuItem.Text = "BackColor"
			# 
			# toolStripSplitButton1
			# 
			self.@toolStripSplitButton1.Items.AddRange(Array[System::Windows::Forms::ToolStripItem].new([self.@newToolStripMenuItem, self.@openToolStripMenuItem, self.@toolStripSeparator1, self.@saveToolStripMenuItem, self.@saveAsToolStripMenuItem, self.@toolStripSeparator2, self.@importToolStripMenuItem, self.@toolStripSeparator3, self.@exitToolStripMenuItem]))
			self.@toolStripSplitButton1.Name = "toolStripSplitButton1"
			self.@toolStripSplitButton1.Size = System.Drawing.Size.new(206, 154)
			self.@toolStripSplitButton1.Text = "File"
			# 
			# newToolStripMenuItem
			# 
			self.@newToolStripMenuItem.Name = "newToolStripMenuItem"
			self.@newToolStripMenuItem.Size = System.Drawing.Size.new(205, 22)
			self.@newToolStripMenuItem.Text = "New"
			# 
			# openToolStripMenuItem
			# 
			self.@openToolStripMenuItem.Name = "openToolStripMenuItem"
			self.@openToolStripMenuItem.Size = System.Drawing.Size.new(205, 22)
			self.@openToolStripMenuItem.Text = "Open"
			# 
			# toolStripSeparator1
			# 
			self.@toolStripSeparator1.Name = "toolStripSeparator1"
			self.@toolStripSeparator1.Size = System.Drawing.Size.new(202, 6)
			# 
			# saveToolStripMenuItem
			# 
			self.@saveToolStripMenuItem.Name = "saveToolStripMenuItem"
			self.@saveToolStripMenuItem.Size = System.Drawing.Size.new(205, 22)
			self.@saveToolStripMenuItem.Text = "Save"
			# 
			# saveAsToolStripMenuItem
			# 
			self.@saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem"
			self.@saveAsToolStripMenuItem.Size = System.Drawing.Size.new(205, 22)
			self.@saveAsToolStripMenuItem.Text = "Save As"
			# 
			# toolStripSeparator2
			# 
			self.@toolStripSeparator2.Name = "toolStripSeparator2"
			self.@toolStripSeparator2.Size = System.Drawing.Size.new(202, 6)
			# 
			# importToolStripMenuItem
			# 
			self.@importToolStripMenuItem.Name = "importToolStripMenuItem"
			self.@importToolStripMenuItem.Size = System.Drawing.Size.new(205, 22)
			self.@importToolStripMenuItem.Text = "Import From Main Editor"
			# 
			# toolStripSeparator3
			# 
			self.@toolStripSeparator3.Name = "toolStripSeparator3"
			self.@toolStripSeparator3.Size = System.Drawing.Size.new(202, 6)
			# 
			# exitToolStripMenuItem
			# 
			self.@exitToolStripMenuItem.Name = "exitToolStripMenuItem"
			self.@exitToolStripMenuItem.Size = System.Drawing.Size.new(205, 22)
			self.@exitToolStripMenuItem.Text = "Exit"
			# 
			# MainForm
			# 
			self.@ClientSize = System.Drawing.Size.new(284, 343)
			self.@Controls.Add(self.@txtmain)
			self.@Controls.Add(self.@toolStrip1)
			self.@Font = System.Drawing.Font.new("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((0)))
			self.@Name = "MainForm"
			self.@ShowHint = WeifenLuo.WinFormsUI.Docking.DockState.DockRight
			self.@Text = "Quick Note"
			self.@toolStrip1.ResumeLayout(false)
			self.@toolStrip1.PerformLayout()
			self.@toolStripSplitButton1.ResumeLayout(false)
			self.ResumeLayout(false)
			self.PerformLayout()
		end
	end
end