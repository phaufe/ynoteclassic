module WavyStylePlugin
	class Form1
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
			self.SuspendLayout()
			# 
			# Form1
			# 
			self.@AutoScaleDimensions = System.Drawing.SizeF.new(6f, 13f)
			self.@AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
			self.@ClientSize = System.Drawing.Size.new(532, 383)
			self.@Font = System.Drawing.Font.new("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((0)))
			self.@Name = "Form1"
			self.@ShowHint = WeifenLuo.WinFormsUI.Docking.DockState.Float
			self.@ShowInTaskbar = false
			self.@Text = "Form1"
			self.ResumeLayout(false)
		end
	end
end