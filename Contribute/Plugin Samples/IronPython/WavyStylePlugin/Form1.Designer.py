class Form1(object):
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
		self.SuspendLayout()
		# 
		# Form1
		# 
		self._AutoScaleDimensions = System.Drawing.SizeF(6f, 13f)
		self._AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		self._ClientSize = System.Drawing.Size(532, 383)
		self._Font = System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((0)))
		self._Name = "Form1"
		self._ShowHint = WeifenLuo.WinFormsUI.Docking.DockState.Float
		self._ShowInTaskbar = False
		self._Text = "Form1"
		self.ResumeLayout(False)