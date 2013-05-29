from System import *
from System.Drawing import *
from System.Threading import *
from System.ComponentModel import *
from System.Windows.Forms import *

class SearchDlg(System.Windows.Forms.Form):
	# <summary>
	# Required designer variable.
	# </summary>
	def __init__(self, iterator):
		self._components = None
		self._iterator = iterator
		#
		# Required for Windows Form Designer support
		#
		self.InitializeComponent()
		self._searchTextBox.Focus()

	#
	# TODO: Add any constructor code after InitializeComponent call
	#
	def Dispose(self, disposing):
		""" <summary>
		 Clean up any resources being used.
		 </summary>
		"""
		if disposing:
			if self._components != None:
				self._components.Dispose()
		self.Dispose(disposing)

	def InitializeComponent(self):
		""" <summary>
		 Required method for Designer support - do not modify
		 the contents of this method with the code editor.
		 </summary>
		"""
		self._searchTextBox = System.Windows.Forms.TextBox()
		self._goBtn = System.Windows.Forms.Button()
		self._cancelBtn = System.Windows.Forms.Button()
		self._label1 = System.Windows.Forms.Label()
		self._caseSensitiveRBtn = System.Windows.Forms.RadioButton()
		self._caseInsensitiveRBtn = System.Windows.Forms.RadioButton()
		self.SuspendLayout()
		# 
		# searchTextBox
		# 
		self._searchTextBox.Location = System.Drawing.Point(24, 24)
		self._searchTextBox.Name = "searchTextBox"
		self._searchTextBox.Size = System.Drawing.Size(240, 20)
		self._searchTextBox.TabIndex = 0
		self._searchTextBox.TextChanged += self._searchTextBox_TextChanged
		self._searchTextBox.KeyUp += self._searchTextBox_KeyUp
		# 
		# goBtn
		# 
		self._goBtn.Location = System.Drawing.Point(102, 79)
		self._goBtn.Name = "goBtn"
		self._goBtn.Size = System.Drawing.Size(75, 23)
		self._goBtn.TabIndex = 1
		self._goBtn.Text = "Search"
		self._goBtn.Click += self._goBtn_Click
		# 
		# cancelBtn
		# 
		self._cancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel
		self._cancelBtn.Location = System.Drawing.Point(183, 80)
		self._cancelBtn.Name = "cancelBtn"
		self._cancelBtn.Size = System.Drawing.Size(75, 23)
		self._cancelBtn.TabIndex = 2
		self._cancelBtn.Text = "Cancel"
		self._cancelBtn.Click += self._cancelBtn_Click
		# 
		# label1
		# 
		self._label1.Location = System.Drawing.Point(24, 8)
		self._label1.Name = "label1"
		self._label1.Size = System.Drawing.Size(64, 16)
		self._label1.TabIndex = 3
		self._label1.Text = "Search for :"
		# 
		# caseSensitiveRBtn
		# 
		self._caseSensitiveRBtn.Location = System.Drawing.Point(154, 48)
		self._caseSensitiveRBtn.Name = "caseSensitiveRBtn"
		self._caseSensitiveRBtn.Size = System.Drawing.Size(104, 24)
		self._caseSensitiveRBtn.TabIndex = 4
		self._caseSensitiveRBtn.Text = "case sensitive"
		self._caseSensitiveRBtn.CheckedChanged += self._caseSensitiveRBtn_CheckedChanged
		# 
		# caseInsensitiveRBtn
		# 
		self._caseInsensitiveRBtn.Checked = True
		self._caseInsensitiveRBtn.Location = System.Drawing.Point(30, 48)
		self._caseInsensitiveRBtn.Name = "caseInsensitiveRBtn"
		self._caseInsensitiveRBtn.Size = System.Drawing.Size(104, 24)
		self._caseInsensitiveRBtn.TabIndex = 5
		self._caseInsensitiveRBtn.TabStop = True
		self._caseInsensitiveRBtn.Text = "case insensitive"
		# 
		# SearchDlg
		# 
		self._AcceptButton = self._goBtn
		self._AutoScaleBaseSize = System.Drawing.Size(5, 13)
		self._CancelButton = self._cancelBtn
		self._ClientSize = System.Drawing.Size(280, 114)
		self._Controls.Add(self._caseInsensitiveRBtn)
		self._Controls.Add(self._caseSensitiveRBtn)
		self._Controls.Add(self._label1)
		self._Controls.Add(self._cancelBtn)
		self._Controls.Add(self._goBtn)
		self._Controls.Add(self._searchTextBox)
		self._MaximizeBox = False
		self._MaximumSize = System.Drawing.Size(296, 152)
		self._MinimizeBox = False
		self._MinimumSize = System.Drawing.Size(296, 152)
		self._Name = "SearchDlg"
		self._ShowIcon = False
		self._ShowInTaskbar = False
		self._StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
		self._Text = "Search for XML Node - SS Ynote Classic"
		self._TopMost = True
		self.ResumeLayout(False)
		self.PerformLayout()

	def cancelBtn_Click(self, sender, e):
		self.Close()

	def goBtn_Click(self, sender, e):
		self._iterator.Next()
		self._searchTextBox.Focus()

	def searchTextBox_TextChanged(self, sender, e):
		self._iterator.StartSearch(self._searchTextBox.Text, self._caseSensitiveRBtn.Checked)

	def caseSensitiveRBtn_CheckedChanged(self, sender, e):
		self._iterator.StartSearch(self._searchTextBox.Text, self._caseSensitiveRBtn.Checked)

	def searchTextBox_KeyUp(self, sender, e):
		if e.KeyCode == Keys.F3:
			self.goBtn_Click(None, None)