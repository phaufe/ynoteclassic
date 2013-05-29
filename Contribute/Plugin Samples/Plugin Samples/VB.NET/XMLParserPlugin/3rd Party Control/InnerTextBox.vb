Imports System.IO
Imports System.Drawing
Imports System.Windows.Forms
Imports System.ComponentModel
Imports System.Drawing.Printing
Imports SS.Ynote.Engine.Framework

Namespace XMLTreeView
	''' <summary>
	''' Summary description for InnerTextBox.
	''' </summary>
	Friend Class InnerTextBox
		Inherits TextBox
		Implements IXmlControl
		#Region "Fields"
		Private parent As XmlTreeView = Nothing
		Private draggedFile As String = String.Empty
		Private printDocument1 As New PrintDocument()
		Private startPos As Integer
		Private xPos As Integer
		Private printFont As New Font("Arial", 10)
		Private caseSensitive As Boolean
		Private criterion As String
		Private printableWidth As Integer
		Private fontHeight As Single
		Private linesPerPage As Single
		Private closingHandlerAssigned As Boolean
		#End Region

		#Region "Constructors"
		Friend Sub New(container As XmlTreeView)
			parent = container
			[ReadOnly] = True
			Multiline = True
			Dock = DockStyle.Fill
			Me.BackColor = Color.White
		End Sub
		Private Sub InitializeComponent()
			Dim resources As New System.ComponentModel.ComponentResourceManager(GetType(InnerTextBox))
			DirectCast(Me, System.ComponentModel.ISupportInitialize).BeginInit()
			Me.SuspendLayout()
			' 
			' InnerTextBox
			' 
			resources.ApplyResources(Me, "$this")
			Me.BackColor = Color.White
			Me.Name = "InnerTextBox"
			DirectCast(Me, System.ComponentModel.ISupportInitialize).EndInit()
			Me.ResumeLayout(False)

		End Sub
		#End Region

		#Region "Implementation IXmlControl"
		Public Sub Search(sender As Object, e As EventArgs) Implements IXmlControl.Search
			If Text.Length > 0 Then
				New SearchDlg(Me).Show()
			End If
		End Sub

		''' <summary>
		''' 
		''' </summary>
		''' <param name="criterion"></param>
		''' <param name="caseSensitive"></param>
		Public Sub StartSearch(criterion As String, caseSensitive As Boolean) Implements IXmlControl.StartSearch
			startPos = 0
			Me.caseSensitive = caseSensitive
			Me.criterion = If(caseSensitive, criterion, criterion.ToUpper())
		End Sub

		''' <summary>
		''' 
		''' </summary>
		Public Sub [Next]() Implements IXmlControl.[Next]
			Dim foundPos As Integer = 0
			SelectionLength = 0
			Dim internalText As String = If(caseSensitive, Text, Text.ToUpper())

			If internalText.Length > 0 Then
				If (InlineAssignHelper(foundPos, internalText.IndexOf(criterion, startPos))) > -1 Then
					SelectionStart = foundPos
					SelectionLength = criterion.Length
					startPos = foundPos + criterion.Length
					Focus()
				Else
					MessageBox.Show("End of text reached !", XmlTreeView.MessageBoxTitle)
					startPos = 0
				End If
			End If
		End Sub

		Public Sub Print(sender As Object, e As EventArgs) Implements IXmlControl.Print
			If Text.Length > 0 AndAlso parent.PrintDialog.ShowDialog() = DialogResult.OK Then
				printDocument1.Print()
			End If
		End Sub

		Public Sub PrintPage(sender As Object, e As PrintPageEventArgs) Implements IXmlControl.PrintPage
			Dim linesPrinted As Integer = 0
			Dim yPos As Single = 0
			Dim endOfPageReached As Boolean = False
			Dim textToDraw As String = String.Empty

			If fontHeight = 0.0 Then
				' nothing initialized yet
				fontHeight = printFont.GetHeight(e.Graphics)
				linesPerPage = e.MarginBounds.Height / fontHeight
				printableWidth = Text.Length \ CInt(Math.Truncate(e.Graphics.MeasureString(Text, printFont).Width / e.MarginBounds.Width))
			End If

			Do
				If xPos < Text.Length - 1 Then
					textToDraw = If(xPos + printableWidth < Text.Length - 1, Text.Substring(xPos, printableWidth), Text.Substring(xPos))
					e.Graphics.DrawString(textToDraw, printFont, Brushes.Black, e.MarginBounds.Left, yPos, New StringFormat())
					yPos += fontHeight
					xPos += printableWidth
					linesPrinted += 1
				End If

				If xPos >= Text.Length OrElse linesPrinted >= linesPerPage Then
					endOfPageReached = True
				End If
			Loop While textToDraw.Length > 0 AndAlso Not endOfPageReached

			e.HasMorePages = (xPos < Text.Length - 1)
		End Sub

		Public ReadOnly Property PrintDocument() As PrintDocument Implements IXmlControl.PrintDocument
			Get
				Return printDocument1
			End Get
		End Property

		Public Sub Edit(sender As Object, e As System.EventArgs) Implements IXmlControl.Edit
			If Not closingHandlerAssigned Then
				Dim parentForm As Form = FindForm()
				AddHandler parentForm.Closing, New CancelEventHandler(AddressOf parent.form_Closing)
				closingHandlerAssigned = True
			End If

			[ReadOnly] = False
		End Sub

		Public Sub Delete(sender As Object, e As System.EventArgs) Implements IXmlControl.Delete
			Text = Text.Remove(SelectionStart, SelectionLength)
		End Sub

		Public Overloads Sub Copy(sender As Object, e As System.EventArgs) Implements IXmlControl.Copy
			Copy()
		End Sub

		Public Overloads Sub Paste(sender As Object, e As System.EventArgs) Implements IXmlControl.Paste
			Dim dataObject As IDataObject = Clipboard.GetDataObject()

			If dataObject.GetDataPresent(DataFormats.Text) Then
				parent.Xml = DirectCast(dataObject.GetData(DataFormats.Text), String)
			End If
		End Sub

		Public Sub Save(sender As Object, e As System.EventArgs) Implements IXmlControl.Save
			If Text.Length > 0 AndAlso parent.SaveDialog.ShowDialog() = DialogResult.OK Then
				Using sw As New StreamWriter(parent.SaveDialog.FileName)
					sw.Write(Xml)
					parent.fileXml = Xml
					parent.filePath = parent.SaveDialog.FileName
				End Using
			End If
		End Sub

		''' <summary>
		''' 
		''' </summary>
		Public Property Xml() As String Implements IXmlControl.Xml
			Get
				Return Text
			End Get
			Set
				Text = value
			End Set
		End Property
		#End Region

		#Region "Event handlers for InnerTextBox"
		Protected Overrides Sub OnKeyUp(e As KeyEventArgs)
			MyBase.OnKeyUp(e)

			If e.Control AndAlso e.KeyCode = Keys.C Then
				Copy(Nothing, Nothing)
			ElseIf e.Control AndAlso e.KeyCode = Keys.V Then
				Paste(Nothing, Nothing)
			ElseIf e.Control AndAlso e.KeyCode = Keys.F Then
				Search(Nothing, Nothing)
			ElseIf e.Control AndAlso e.KeyCode = Keys.P Then
				Print(Nothing, Nothing)
			ElseIf e.Control AndAlso e.KeyCode = Keys.S Then
				Save(Nothing, Nothing)
			ElseIf e.KeyCode = Keys.Delete Then
				Delete(Nothing, Nothing)
			ElseIf e.KeyCode = Keys.Insert Then
				Edit(Nothing, Nothing)
			End If
		End Sub

		Protected Overrides Sub OnDragEnter(e As DragEventArgs)
			MyBase.OnDragEnter(e)

			If e.Data.GetDataPresent(DataFormats.FileDrop) Then
				e.Effect = DragDropEffects.Copy
				draggedFile = DirectCast(DirectCast(e.Data.GetData(DataFormats.FileDrop), Object())(0), String)
			End If
		End Sub

		Protected Overrides Sub OnDragDrop(e As DragEventArgs)
			MyBase.OnDragDrop(e)

			If draggedFile.Length > 0 Then
				Using fs As New StreamReader(draggedFile)
					parent.fileXml = fs.ReadToEnd()
					parent.Xml = parent.fileXml
					parent.filePath = draggedFile
				End Using

				draggedFile = String.Empty
			End If
		End Sub
		Private Shared Function InlineAssignHelper(Of T)(ByRef target As T, value As T) As T
			target = value
			Return value
		End Function
		#End Region

	End Class
End Namespace
