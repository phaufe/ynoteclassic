require "mscorlib"
require "System, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
require "System.Drawing, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
require "System.Threading, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
require "System.ComponentModel, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
require "System.Windows.Forms, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"

module XMLTreeView
	class SearchDlg < System::Windows::Forms::Form
		# <summary>
		# Required designer variable.
		# </summary>
		def initialize(iterator)
			@components = nil
			self.@iterator = iterator
			#
			# Required for Windows Form Designer support
			#
			self.InitializeComponent()
			@searchTextBox.Focus()
		end

		#
		# TODO: Add any constructor code after InitializeComponent call
		# # <summary>
		# Clean up any resources being used.
		# </summary>
		def Dispose(disposing)
			if disposing then
				if @components != nil then
					@components.Dispose()
				end
			end
			self.Dispose(disposing)
		end
 # <summary>
		# Required method for Designer support - do not modify
		# the contents of this method with the code editor.
		# </summary>
		def InitializeComponent()
			self.@searchTextBox = System.Windows.Forms.TextBox.new()
			self.@goBtn = System.Windows.Forms.Button.new()
			self.@cancelBtn = System.Windows.Forms.Button.new()
			self.@label1 = System.Windows.Forms.Label.new()
			self.@caseSensitiveRBtn = System.Windows.Forms.RadioButton.new()
			self.@caseInsensitiveRBtn = System.Windows.Forms.RadioButton.new()
			self.SuspendLayout()
			# 
			# searchTextBox
			# 
			self.@searchTextBox.Location = System.Drawing.Point.new(24, 24)
			self.@searchTextBox.Name = "searchTextBox"
			self.@searchTextBox.Size = System.Drawing.Size.new(240, 20)
			self.@searchTextBox.TabIndex = 0
			self.@searchTextBox.TextChanged { |sender, e| self.@searchTextBox_TextChanged(sender, e) }
			self.@searchTextBox.KeyUp { |sender, e| self.@searchTextBox_KeyUp(sender, e) }
			# 
			# goBtn
			# 
			self.@goBtn.Location = System.Drawing.Point.new(102, 79)
			self.@goBtn.Name = "goBtn"
			self.@goBtn.Size = System.Drawing.Size.new(75, 23)
			self.@goBtn.TabIndex = 1
			self.@goBtn.Text = "Search"
			self.@goBtn.Click { |sender, e| self.@goBtn_Click(sender, e) }
			# 
			# cancelBtn
			# 
			self.@cancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel
			self.@cancelBtn.Location = System.Drawing.Point.new(183, 80)
			self.@cancelBtn.Name = "cancelBtn"
			self.@cancelBtn.Size = System.Drawing.Size.new(75, 23)
			self.@cancelBtn.TabIndex = 2
			self.@cancelBtn.Text = "Cancel"
			self.@cancelBtn.Click { |sender, e| self.@cancelBtn_Click(sender, e) }
			# 
			# label1
			# 
			self.@label1.Location = System.Drawing.Point.new(24, 8)
			self.@label1.Name = "label1"
			self.@label1.Size = System.Drawing.Size.new(64, 16)
			self.@label1.TabIndex = 3
			self.@label1.Text = "Search for :"
			# 
			# caseSensitiveRBtn
			# 
			self.@caseSensitiveRBtn.Location = System.Drawing.Point.new(154, 48)
			self.@caseSensitiveRBtn.Name = "caseSensitiveRBtn"
			self.@caseSensitiveRBtn.Size = System.Drawing.Size.new(104, 24)
			self.@caseSensitiveRBtn.TabIndex = 4
			self.@caseSensitiveRBtn.Text = "case sensitive"
			self.@caseSensitiveRBtn.CheckedChanged { |sender, e| self.@caseSensitiveRBtn_CheckedChanged(sender, e) }
			# 
			# caseInsensitiveRBtn
			# 
			self.@caseInsensitiveRBtn.Checked = true
			self.@caseInsensitiveRBtn.Location = System.Drawing.Point.new(30, 48)
			self.@caseInsensitiveRBtn.Name = "caseInsensitiveRBtn"
			self.@caseInsensitiveRBtn.Size = System.Drawing.Size.new(104, 24)
			self.@caseInsensitiveRBtn.TabIndex = 5
			self.@caseInsensitiveRBtn.TabStop = true
			self.@caseInsensitiveRBtn.Text = "case insensitive"
			# 
			# SearchDlg
			# 
			self.@AcceptButton = self.@goBtn
			self.@AutoScaleBaseSize = System.Drawing.Size.new(5, 13)
			self.@CancelButton = self.@cancelBtn
			self.@ClientSize = System.Drawing.Size.new(280, 114)
			self.@Controls.Add(self.@caseInsensitiveRBtn)
			self.@Controls.Add(self.@caseSensitiveRBtn)
			self.@Controls.Add(self.@label1)
			self.@Controls.Add(self.@cancelBtn)
			self.@Controls.Add(self.@goBtn)
			self.@Controls.Add(self.@searchTextBox)
			self.@MaximizeBox = false
			self.@MaximumSize = System.Drawing.Size.new(296, 152)
			self.@MinimizeBox = false
			self.@MinimumSize = System.Drawing.Size.new(296, 152)
			self.@Name = "SearchDlg"
			self.@ShowIcon = false
			self.@ShowInTaskbar = false
			self.@StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
			self.@Text = "Search for XML Node - SS Ynote Classic"
			self.@TopMost = true
			self.ResumeLayout(false)
			self.PerformLayout()
		end

		def cancelBtn_Click(sender, e)
			self.Close()
		end

		def goBtn_Click(sender, e)
			@iterator.Next()
			@searchTextBox.Focus()
		end

		def searchTextBox_TextChanged(sender, e)
			@iterator.StartSearch(@searchTextBox.Text, @caseSensitiveRBtn.Checked)
		end

		def caseSensitiveRBtn_CheckedChanged(sender, e)
			@iterator.StartSearch(@searchTextBox.Text, @caseSensitiveRBtn.Checked)
		end

		def searchTextBox_KeyUp(sender, e)
			if e.KeyCode == Keys.F3 then
				self.goBtn_Click(nil, nil)
			end
		end
	end
end