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
		self._XmlTree = XMLTreeView.XmlTreeView()
		self._toolStrip1 = System.Windows.Forms.ToolStrip()
		self._toolStripButton1 = System.Windows.Forms.ToolStripButton()
		self._toolStrip1.SuspendLayout()
		self.SuspendLayout()
		# 
		# XmlTree
		# 
		self._XmlTree.BackColor = System.Drawing.Color.White
		self._XmlTree.Dock = System.Windows.Forms.DockStyle.Fill
		self._XmlTree.Location = System.Drawing.Point(0, 25)
		self._XmlTree.Name = "XmlTree"
		self._XmlTree.Size = System.Drawing.Size(441, 435)
		self._XmlTree.TabIndex = 0
		self._XmlTree.Xml = ""
		self._XmlTree.XmlFile = ""
		# 
		# toolStrip1
		# 
		self._toolStrip1.Items.AddRange(Array[System.Windows.Forms.ToolStripItem]((self._toolStripButton1)))
		self._toolStrip1.Location = System.Drawing.Point(0, 0)
		self._toolStrip1.Name = "toolStrip1"
		self._toolStrip1.Size = System.Drawing.Size(441, 25)
		self._toolStrip1.TabIndex = 1
		self._toolStrip1.Text = "toolStrip1"
		# 
		# toolStripButton1
		# 
		self._toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
		self._toolStripButton1.Image = ((resources.GetObject("toolStripButton1.Image")))
		self._toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta
		self._toolStripButton1.Name = "toolStripButton1"
		self._toolStripButton1.Size = System.Drawing.Size(141, 22)
		self._toolStripButton1.Text = "Parse Current Document"
		self._toolStripButton1.Click += self._toolStripButton1_Click
		# 
		# MainForm
		# 
		self._AutoScaleDimensions = System.Drawing.SizeF(6f, 13f)
		self._AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		self._ClientSize = System.Drawing.Size(441, 460)
		self._Controls.Add(self._XmlTree)
		self._Controls.Add(self._toolStrip1)
		self._Font = System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((0)))
		self._Name = "MainForm"
		self._Text = "MainForm"
		self._toolStrip1.ResumeLayout(False)
		self._toolStrip1.PerformLayout()
		self.ResumeLayout(False)
		self.PerformLayout()