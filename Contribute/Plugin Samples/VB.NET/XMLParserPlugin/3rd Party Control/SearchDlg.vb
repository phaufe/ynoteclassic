Imports System.Drawing
Imports System.Threading
Imports System.ComponentModel
Imports System.Windows.Forms

Namespace XMLTreeView
	Friend Class SearchDlg
		Inherits System.Windows.Forms.Form
		#Region "Fields"
		Private searchTextBox As System.Windows.Forms.TextBox
		Private goBtn As System.Windows.Forms.Button
		Private cancelBtn As System.Windows.Forms.Button
		Private label1 As System.Windows.Forms.Label
		Private iterator As IXmlControl
		Private caseSensitiveRBtn As System.Windows.Forms.RadioButton
		Private caseInsensitiveRBtn As System.Windows.Forms.RadioButton

		''' <summary>
		''' Required designer variable.
		''' </summary>
		Private components As System.ComponentModel.Container = Nothing
		#End Region

		#Region "Constructors"
		Friend Sub New(iterator As IXmlControl)
			Me.iterator = iterator
			'
			' Required for Windows Form Designer support
			'
			InitializeComponent()
				'
				' TODO: Add any constructor code after InitializeComponent call
				'
			searchTextBox.Focus()
		End Sub
		#End Region

		#Region "Dispose"
		''' <summary>
		''' Clean up any resources being used.
		''' </summary>
		Protected Overrides Sub Dispose(disposing As Boolean)
			If disposing Then
				If components IsNot Nothing Then
					components.Dispose()
				End If
			End If
			MyBase.Dispose(disposing)
		End Sub
		#End Region

		#Region "Windows Form Designer generated code"
		''' <summary>
		''' Required method for Designer support - do not modify
		''' the contents of this method with the code editor.
		''' </summary>
		Private Sub InitializeComponent()
			Me.searchTextBox = New System.Windows.Forms.TextBox()
			Me.goBtn = New System.Windows.Forms.Button()
			Me.cancelBtn = New System.Windows.Forms.Button()
			Me.label1 = New System.Windows.Forms.Label()
			Me.caseSensitiveRBtn = New System.Windows.Forms.RadioButton()
			Me.caseInsensitiveRBtn = New System.Windows.Forms.RadioButton()
			Me.SuspendLayout()
			' 
			' searchTextBox
			' 
			Me.searchTextBox.Location = New System.Drawing.Point(24, 24)
			Me.searchTextBox.Name = "searchTextBox"
			Me.searchTextBox.Size = New System.Drawing.Size(240, 20)
			Me.searchTextBox.TabIndex = 0
			AddHandler Me.searchTextBox.TextChanged, New System.EventHandler(AddressOf Me.searchTextBox_TextChanged)
			AddHandler Me.searchTextBox.KeyUp, New System.Windows.Forms.KeyEventHandler(AddressOf Me.searchTextBox_KeyUp)
			' 
			' goBtn
			' 
			Me.goBtn.Location = New System.Drawing.Point(102, 79)
			Me.goBtn.Name = "goBtn"
			Me.goBtn.Size = New System.Drawing.Size(75, 23)
			Me.goBtn.TabIndex = 1
			Me.goBtn.Text = "Search"
			AddHandler Me.goBtn.Click, New System.EventHandler(AddressOf Me.goBtn_Click)
			' 
			' cancelBtn
			' 
			Me.cancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel
			Me.cancelBtn.Location = New System.Drawing.Point(183, 80)
			Me.cancelBtn.Name = "cancelBtn"
			Me.cancelBtn.Size = New System.Drawing.Size(75, 23)
			Me.cancelBtn.TabIndex = 2
			Me.cancelBtn.Text = "Cancel"
			AddHandler Me.cancelBtn.Click, New System.EventHandler(AddressOf Me.cancelBtn_Click)
			' 
			' label1
			' 
			Me.label1.Location = New System.Drawing.Point(24, 8)
			Me.label1.Name = "label1"
			Me.label1.Size = New System.Drawing.Size(64, 16)
			Me.label1.TabIndex = 3
			Me.label1.Text = "Search for :"
			' 
			' caseSensitiveRBtn
			' 
			Me.caseSensitiveRBtn.Location = New System.Drawing.Point(154, 48)
			Me.caseSensitiveRBtn.Name = "caseSensitiveRBtn"
			Me.caseSensitiveRBtn.Size = New System.Drawing.Size(104, 24)
			Me.caseSensitiveRBtn.TabIndex = 4
			Me.caseSensitiveRBtn.Text = "case sensitive"
			AddHandler Me.caseSensitiveRBtn.CheckedChanged, New System.EventHandler(AddressOf Me.caseSensitiveRBtn_CheckedChanged)
			' 
			' caseInsensitiveRBtn
			' 
			Me.caseInsensitiveRBtn.Checked = True
			Me.caseInsensitiveRBtn.Location = New System.Drawing.Point(30, 48)
			Me.caseInsensitiveRBtn.Name = "caseInsensitiveRBtn"
			Me.caseInsensitiveRBtn.Size = New System.Drawing.Size(104, 24)
			Me.caseInsensitiveRBtn.TabIndex = 5
			Me.caseInsensitiveRBtn.TabStop = True
			Me.caseInsensitiveRBtn.Text = "case insensitive"
			' 
			' SearchDlg
			' 
			Me.AcceptButton = Me.goBtn
			Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
			Me.CancelButton = Me.cancelBtn
			Me.ClientSize = New System.Drawing.Size(280, 114)
			Me.Controls.Add(Me.caseInsensitiveRBtn)
			Me.Controls.Add(Me.caseSensitiveRBtn)
			Me.Controls.Add(Me.label1)
			Me.Controls.Add(Me.cancelBtn)
			Me.Controls.Add(Me.goBtn)
			Me.Controls.Add(Me.searchTextBox)
			Me.MaximizeBox = False
			Me.MaximumSize = New System.Drawing.Size(296, 152)
			Me.MinimizeBox = False
			Me.MinimumSize = New System.Drawing.Size(296, 152)
			Me.Name = "SearchDlg"
			Me.ShowIcon = False
			Me.ShowInTaskbar = False
			Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
			Me.Text = "Search for XML Node - SS Ynote Classic"
			Me.TopMost = True
			Me.ResumeLayout(False)
			Me.PerformLayout()

		End Sub
		#End Region

		#Region "Event handlers for SearchDlg"
		Private Sub cancelBtn_Click(sender As Object, e As System.EventArgs)
			Close()
		End Sub

		Private Sub goBtn_Click(sender As Object, e As System.EventArgs)
			iterator.[Next]()
			searchTextBox.Focus()
		End Sub

		Private Sub searchTextBox_TextChanged(sender As Object, e As System.EventArgs)
			iterator.StartSearch(searchTextBox.Text, caseSensitiveRBtn.Checked)
		End Sub

		Private Sub caseSensitiveRBtn_CheckedChanged(sender As Object, e As System.EventArgs)
			iterator.StartSearch(searchTextBox.Text, caseSensitiveRBtn.Checked)
		End Sub

		Private Sub searchTextBox_KeyUp(sender As Object, e As System.Windows.Forms.KeyEventArgs)
			If e.KeyCode = Keys.F3 Then
				goBtn_Click(Nothing, Nothing)
			End If
		End Sub
		#End Region
	End Class
End Namespace
