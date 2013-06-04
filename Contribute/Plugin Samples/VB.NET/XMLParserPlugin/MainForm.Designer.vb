Partial Class MainForm
	''' <summary>
	''' Required designer variable.
	''' </summary>
	Private components As System.ComponentModel.IContainer = Nothing

	''' <summary>
	''' Clean up any resources being used.
	''' </summary>
	''' <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
	Protected Overrides Sub Dispose(disposing As Boolean)
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
		Me.XmlTree = New XMLTreeView.XmlTreeView()
		Me.toolStrip1 = New System.Windows.Forms.ToolStrip()
		Me.toolStripButton1 = New System.Windows.Forms.ToolStripButton()
		Me.toolStrip1.SuspendLayout()
		Me.SuspendLayout()
		' 
		' XmlTree
		' 
		Me.XmlTree.BackColor = System.Drawing.Color.White
		Me.XmlTree.Dock = System.Windows.Forms.DockStyle.Fill
		Me.XmlTree.Location = New System.Drawing.Point(0, 25)
		Me.XmlTree.Name = "XmlTree"
		Me.XmlTree.Size = New System.Drawing.Size(441, 435)
		Me.XmlTree.TabIndex = 0
		Me.XmlTree.Xml = ""
		Me.XmlTree.XmlFile = ""
		' 
		' toolStrip1
		' 
		Me.toolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.toolStripButton1})
		Me.toolStrip1.Location = New System.Drawing.Point(0, 0)
		Me.toolStrip1.Name = "toolStrip1"
		Me.toolStrip1.Size = New System.Drawing.Size(441, 25)
		Me.toolStrip1.TabIndex = 1
		Me.toolStrip1.Text = "toolStrip1"
		' 
		' toolStripButton1
		' 
		Me.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
		Me.toolStripButton1.Image = DirectCast(resources.GetObject("toolStripButton1.Image"), System.Drawing.Image)
		Me.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta
		Me.toolStripButton1.Name = "toolStripButton1"
		Me.toolStripButton1.Size = New System.Drawing.Size(141, 22)
		Me.toolStripButton1.Text = "Parse Current Document"
		AddHandler Me.toolStripButton1.Click, New System.EventHandler(AddressOf Me.toolStripButton1_Click)
		' 
		' MainForm
		' 
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6F, 13F)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.ClientSize = New System.Drawing.Size(441, 460)
		Me.Controls.Add(Me.XmlTree)
		Me.Controls.Add(Me.toolStrip1)
		Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(0))
		Me.Name = "MainForm"
		Me.Text = "MainForm"
		Me.toolStrip1.ResumeLayout(False)
		Me.toolStrip1.PerformLayout()
		Me.ResumeLayout(False)
		Me.PerformLayout()

	End Sub

	#End Region

	Private XmlTree As XMLTreeView.XmlTreeView
	Private toolStrip1 As System.Windows.Forms.ToolStrip
	Private toolStripButton1 As System.Windows.Forms.ToolStripButton
End Class
