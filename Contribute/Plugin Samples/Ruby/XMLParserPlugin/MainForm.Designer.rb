require "mscorlib"

module XMLParserPlugin
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
			self.@XmlTree = XMLTreeView.XmlTreeView.new()
			self.@toolStrip1 = System.Windows.Forms.ToolStrip.new()
			self.@toolStripButton1 = System.Windows.Forms.ToolStripButton.new()
			self.@toolStrip1.SuspendLayout()
			self.SuspendLayout()
			# 
			# XmlTree
			# 
			self.@XmlTree.BackColor = System.Drawing.Color.White
			self.@XmlTree.Dock = System.Windows.Forms.DockStyle.Fill
			self.@XmlTree.Location = System.Drawing.Point.new(0, 25)
			self.@XmlTree.Name = "XmlTree"
			self.@XmlTree.Size = System.Drawing.Size.new(441, 435)
			self.@XmlTree.TabIndex = 0
			self.@XmlTree.Xml = ""
			self.@XmlTree.XmlFile = ""
			# 
			# toolStrip1
			# 
			self.@toolStrip1.Items.AddRange(Array[System::Windows::Forms::ToolStripItem].new([self.@toolStripButton1]))
			self.@toolStrip1.Location = System.Drawing.Point.new(0, 0)
			self.@toolStrip1.Name = "toolStrip1"
			self.@toolStrip1.Size = System.Drawing.Size.new(441, 25)
			self.@toolStrip1.TabIndex = 1
			self.@toolStrip1.Text = "toolStrip1"
			# 
			# toolStripButton1
			# 
			self.@toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
			self.@toolStripButton1.Image = ((resources.GetObject("toolStripButton1.Image")))
			self.@toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta
			self.@toolStripButton1.Name = "toolStripButton1"
			self.@toolStripButton1.Size = System.Drawing.Size.new(141, 22)
			self.@toolStripButton1.Text = "Parse Current Document"
			self.@toolStripButton1.Click { |sender, e| self.@toolStripButton1_Click(sender, e) }
			# 
			# MainForm
			# 
			self.@AutoScaleDimensions = System.Drawing.SizeF.new(6f, 13f)
			self.@AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
			self.@ClientSize = System.Drawing.Size.new(441, 460)
			self.@Controls.Add(self.@XmlTree)
			self.@Controls.Add(self.@toolStrip1)
			self.@Font = System.Drawing.Font.new("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((0)))
			self.@Name = "MainForm"
			self.@Text = "MainForm"
			self.@toolStrip1.ResumeLayout(false)
			self.@toolStrip1.PerformLayout()
			self.ResumeLayout(false)
			self.PerformLayout()
		end
	end
end