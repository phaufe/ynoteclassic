Imports System.IO
Imports System.Xml
Imports System.Data
Imports System.Drawing
Imports System.Collections
Imports System.Windows.Forms
Imports System.ComponentModel
Imports System.Drawing.Printing

Namespace XMLTreeView
	''' <summary>
	''' Summary description for XmlTreeView.
	''' </summary>
	Public Class XmlTreeView
		Inherits System.Windows.Forms.UserControl
		#Region "Fields"
		Private popUpMenu As System.Windows.Forms.ContextMenu
		Private printMenuItem As System.Windows.Forms.MenuItem
		Private searchMenuItem As System.Windows.Forms.MenuItem
		Private deleteMenuItem As System.Windows.Forms.MenuItem
		Private printDialog1 As System.Windows.Forms.PrintDialog
		Private saveFileDialog1 As System.Windows.Forms.SaveFileDialog
		Private saveMenuItem As System.Windows.Forms.MenuItem
		Private pasteMenuItem As System.Windows.Forms.MenuItem
		Private copyMenuItem As System.Windows.Forms.MenuItem
		Private editMenuItem As System.Windows.Forms.MenuItem
		Private xmlControl As XMLTreeView.IXmlControl
		Private content As String = String.Empty
		Friend fileXml As String = String.Empty
		Friend filePath As String = String.Empty
		Public Const MessageBoxTitle As String = "XmlTreeView"

		''' <summary> 
		''' Required designer variable.
		''' </summary>
		Private components As System.ComponentModel.Container = Nothing
		#End Region

		#Region "Constructors"
		Public Sub New()
			' This call is required by the Windows.Forms Form Designer.
			InitializeComponent()
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

		#Region "Component Designer generated code"
		''' <summary> 
		''' Required method for Designer support - do not modify 
		''' the contents of this method with the code editor.
		''' </summary>
		Private Sub InitializeComponent()
			Me.popUpMenu = New System.Windows.Forms.ContextMenu()
			Me.printMenuItem = New System.Windows.Forms.MenuItem()
			Me.searchMenuItem = New System.Windows.Forms.MenuItem()
			Me.deleteMenuItem = New System.Windows.Forms.MenuItem()
			Me.saveMenuItem = New System.Windows.Forms.MenuItem()
			Me.pasteMenuItem = New System.Windows.Forms.MenuItem()
			Me.copyMenuItem = New System.Windows.Forms.MenuItem()
			Me.editMenuItem = New System.Windows.Forms.MenuItem()
			Me.printDialog1 = New System.Windows.Forms.PrintDialog()
			Me.saveFileDialog1 = New System.Windows.Forms.SaveFileDialog()
			Me.SuspendLayout()
			' 
			' popUpMenu
			' 
			Me.popUpMenu.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.printMenuItem, Me.searchMenuItem, Me.deleteMenuItem, Me.saveMenuItem, Me.pasteMenuItem, Me.copyMenuItem, _
				Me.editMenuItem})
			' 
			' printMenuItem
			' 
			Me.printMenuItem.Index = 0
			Me.printMenuItem.Text = "&Print"
			' 
			' searchMenuItem
			' 
			Me.searchMenuItem.Index = 1
			Me.searchMenuItem.Text = "&Search"
			' 
			' deleteMenuItem
			' 
			Me.deleteMenuItem.Index = 2
			Me.deleteMenuItem.Text = "&Delete"
			' 
			' saveMenuItem
			' 
			Me.saveMenuItem.Index = 3
			Me.saveMenuItem.Text = "S&ave As..."
			' 
			' pasteMenuItem
			' 
			Me.pasteMenuItem.Index = 4
			Me.pasteMenuItem.Text = "Past&e"
			' 
			' copyMenuItem
			' 
			Me.copyMenuItem.Index = 5
			Me.copyMenuItem.Text = "&Copy"
			' 
			' editMenuItem
			' 
			Me.editMenuItem.Index = 6
			Me.editMenuItem.Text = "Ed&it"
			' 
			' saveFileDialog1
			' 
			Me.saveFileDialog1.Filter = "XML-Files|*.xml"
			' 
			' XmlTreeView
			' 
			Me.BackColor = System.Drawing.Color.White
			Me.Name = "XmlTreeView"
			Me.Size = New System.Drawing.Size(232, 321)
			Me.ResumeLayout(False)

		End Sub
		#End Region

		#Region "Private methods"
		Private Sub DeregisterEvents(control As IXmlControl)
			If control IsNot Nothing Then
				RemoveHandler Me.printMenuItem.Click, New System.EventHandler(AddressOf control.Print)
				RemoveHandler Me.searchMenuItem.Click, New System.EventHandler(AddressOf control.Search)
				RemoveHandler Me.deleteMenuItem.Click, New System.EventHandler(AddressOf control.Delete)
				RemoveHandler Me.saveMenuItem.Click, New System.EventHandler(AddressOf control.Save)
				RemoveHandler Me.pasteMenuItem.Click, New System.EventHandler(AddressOf control.Paste)
				RemoveHandler Me.copyMenuItem.Click, New System.EventHandler(AddressOf control.Copy)
				RemoveHandler Me.editMenuItem.Click, New System.EventHandler(AddressOf control.Edit)
				RemoveHandler Me.printDialog1.Document.PrintPage, New System.Drawing.Printing.PrintPageEventHandler(AddressOf control.PrintPage)
			End If
		End Sub

		Private Sub RegisterEvents(control As IXmlControl)
			If control IsNot Nothing Then
				AddHandler Me.printMenuItem.Click, New System.EventHandler(AddressOf control.Print)
				AddHandler Me.searchMenuItem.Click, New System.EventHandler(AddressOf control.Search)
				AddHandler Me.deleteMenuItem.Click, New System.EventHandler(AddressOf control.Delete)
				AddHandler Me.saveMenuItem.Click, New System.EventHandler(AddressOf control.Save)
				AddHandler Me.pasteMenuItem.Click, New System.EventHandler(AddressOf control.Paste)
				AddHandler Me.copyMenuItem.Click, New System.EventHandler(AddressOf control.Copy)
				AddHandler Me.editMenuItem.Click, New System.EventHandler(AddressOf control.Edit)
				Me.printDialog1.Document = control.PrintDocument
				AddHandler Me.printDialog1.Document.PrintPage, New System.Drawing.Printing.PrintPageEventHandler(AddressOf control.PrintPage)
			End If
		End Sub
		#End Region

		#Region "Event handler for form closing"
		Friend Sub form_Closing(sender As Object, e As CancelEventArgs)
			If Not content.Equals(Xml) Then
				If MessageBox.Show("Save changes?", XmlTreeView.MessageBoxTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = DialogResult.Yes Then
					If saveFileDialog1.ShowDialog() = DialogResult.OK Then
						Using sw As New StreamWriter(saveFileDialog1.FileName)
							sw.Write(Xml)
						End Using
					End If
				End If
			End If
		End Sub
		#End Region

		#Region "Properties"
		''' <summary>
		''' Sets or gets XML-data in a string form.
		''' </summary>
		Public Property Xml() As String
			Get
				Dim result As String = String.Empty

				If Controls.Count > 0 Then
					result = DirectCast(Controls(0), IXmlControl).Xml
				End If

				Return result
			End Get
			Set
				Controls.Clear()

				Try
					DeregisterEvents(xmlControl)

					xmlControl = New InnerXmlTreeView(Me)
					xmlControl.Xml = value
					content = xmlControl.Xml

					RegisterEvents(xmlControl)
				Catch
					Controls.Clear()

					DeregisterEvents(xmlControl)

					xmlControl = New InnerTextBox(Me)
					xmlControl.Xml = value

					RegisterEvents(xmlControl)
				End Try

				DirectCast(xmlControl, Control).ContextMenu = Me.popUpMenu

				Controls.Add(DirectCast(xmlControl, Control))
			End Set
		End Property

		''' <summary>
		''' Sets or gets the fully qualified path to a file containing XML-data.
		''' </summary>
		Public Property XmlFile() As String
			Get
				If fileXml <> Xml Then
					filePath = String.Empty
				End If

				Return filePath
			End Get

			Set
				If value IsNot Nothing AndAlso value.Length > 0 Then
					Using fs As New StreamReader(value)
						fileXml = fs.ReadToEnd()
						Xml = fileXml
						filePath = value
					End Using
				End If
			End Set
		End Property

		Friend ReadOnly Property PrintDialog() As PrintDialog
			Get
				Return printDialog1
			End Get
		End Property

		Friend ReadOnly Property SaveDialog() As SaveFileDialog
			Get
				Return saveFileDialog1
			End Get
		End Property

		''' <summary>
		''' 
		''' </summary>
		<DefaultValue(True)> _
		Public Property LabelEdit() As Boolean
			Get
				Return deleteMenuItem.Enabled AndAlso editMenuItem.Enabled
			End Get
			Set
				editMenuItem.Enabled = InlineAssignHelper(deleteMenuItem.Enabled, value)
			End Set
		End Property
		Private Shared Function InlineAssignHelper(Of T)(ByRef target As T, value As T) As T
			target = value
			Return value
		End Function
		#End Region
	End Class
End Namespace
