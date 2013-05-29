Imports System.IO
Imports System.Xml
Imports System.Drawing
Imports System.Threading
Imports System.Collections
Imports System.Windows.Forms
Imports System.ComponentModel
Imports System.Drawing.Printing
Imports SS.Ynote.Engine.Controls

Namespace XMLTreeView
	Friend Class InnerXmlTreeView
		Inherits SSTreeView
		Implements IXmlControl
		#Region "Fields"
		Private document As XmlDocument = Nothing
		Private parent As XmlTreeView = Nothing
		Private draggedFile As String = String.Empty
		Private Shared CurrentNode As TreeNode = Nothing
		Private foundList As New ArrayList(100)
		Private foundNode As Boolean
		Private searchText As String
		Private caseSensitive As Boolean
		Private printFont As New Font("Arial", 10)
		Private linesPerPage As Single = 0
		Private yPos As Single = 0
		Private count As Integer = 0
		Private indent As String = String.Empty
		Private leftMargin As Single = 0
		Private topMargin As Single = 0
		Private endOfPageReached As Boolean = False
		Private printedNodes As New ArrayList(1000)
		Private eventArgs As PrintPageEventArgs = Nothing
		Private editBox As TextBox
		Private editingNode As XmlTreeNode
		Private printDocument1 As New PrintDocument()
		Private closingHandlerAssigned As Boolean
		#End Region

		#Region "Constructors"
		''' <summary>
		''' 
		''' </summary>
		Public Sub New(container As XmlTreeView)
			parent = container
			AllowDrop = True
			Dock = System.Windows.Forms.DockStyle.Fill
			ImageIndex = -1
			SelectedImageIndex = -1
			Size = New System.Drawing.Size(150, 130)
			TabIndex = 0
			document = New XmlDocument()
			ShowLines = False
		End Sub
		#End Region

		#Region "Implementation IXmlControl"

		Public Sub Search(sender As Object, e As EventArgs) Implements IXmlControl.Search
			If Nodes.Count > 0 Then
				New SearchDlg(Me).Show()
			End If
		End Sub

		''' <summary>
		''' 
		''' </summary>
		''' <param name="criterion"></param>
		''' <param name="caseSensitive"></param>
		Public Sub StartSearch(criterion As String, caseSensitive As Boolean) Implements IXmlControl.StartSearch
			foundList.Clear()
			Me.caseSensitive = caseSensitive
			searchText = If(caseSensitive, criterion, criterion.ToUpper())
		End Sub

		''' <summary>
		''' 
		''' </summary>
		Public Sub [Next]() Implements IXmlControl.[Next]
			SelectedNode.BackColor = Color.Empty
			foundNode = False

			RecurseTreeNodes(Nodes)

			If Not foundNode Then
				MessageBox.Show("End of tree reached !", XmlTreeView.MessageBoxTitle)
				foundList.Clear()
			End If
		End Sub

		Public Sub Print(sender As Object, e As EventArgs) Implements IXmlControl.Print
			If Nodes.Count > 0 AndAlso parent.PrintDialog.ShowDialog() = DialogResult.OK Then
				printedNodes.Clear()
				printDocument1.Print()
			End If
		End Sub

		Public Sub PrintPage(sender As Object, e As PrintPageEventArgs) Implements IXmlControl.PrintPage
			yPos = 0
			count = 0
			endOfPageReached = False
			eventArgs = e
			eventArgs.HasMorePages = False
			leftMargin = eventArgs.MarginBounds.Left
			topMargin = eventArgs.MarginBounds.Top
			linesPerPage = eventArgs.MarginBounds.Height / printFont.GetHeight(eventArgs.Graphics)

			' Iterate over the file, printing each line.
			RecursePrintTreeNodes(Nodes)
		End Sub

		Public ReadOnly Property PrintDocument() As PrintDocument Implements IXmlControl.PrintDocument
			Get
				Return printDocument1
			End Get
		End Property

		Public Sub Edit(sender As Object, e As System.EventArgs) Implements IXmlControl.Edit
			If Nodes.Count > 0 AndAlso SelectedNode IsNot Nothing AndAlso parent.LabelEdit Then
				editingNode = DirectCast(SelectedNode, XmlTreeNode)

				If editingNode.ConnectedXmlElement IsNot Nothing Then
					If Not closingHandlerAssigned Then
						Dim parentForm As Form = FindForm()
						AddHandler parentForm.Closing, New CancelEventHandler(AddressOf parent.form_Closing)
						closingHandlerAssigned = True
					End If

					Dim height As Integer = editingNode.Bounds.Height
					Dim width__1 As Integer = editingNode.Bounds.Width
					Dim left As Integer = editingNode.Bounds.Left
					Dim top As Integer = editingNode.Bounds.Top

					editingNode.ExpandAll()

					If editingNode.ConnectedXmlElement.HasChildNodes AndAlso editingNode.ConnectedXmlElement.FirstChild.NodeType <> XmlNodeType.Text Then
						height = editingNode.NextNode.Bounds.Bottom - editingNode.Bounds.Top
						width__1 = Width - left
					End If

					editBox = New TextBox()
					editBox.Multiline = True
					editBox.BorderStyle = BorderStyle.FixedSingle
					AddHandler editBox.Leave, New EventHandler(AddressOf editBox_Leave)
					AddHandler editBox.KeyUp, New KeyEventHandler(AddressOf editBox_KeyUp)
					editBox.SetBounds(left, top, width__1, height)
					editingNode.RecurseSubNodes(editingNode.Parent)
					editBox.Text = editingNode.SelfAndChildren
					Controls.Add(editBox)
					editBox.Focus()
				End If
			End If
		End Sub

		Public Sub Copy(sender As Object, e As System.EventArgs) Implements IXmlControl.Copy
			If Nodes.Count > 0 Then
				Clipboard.SetDataObject(Xml, True)
			End If
		End Sub

		Public Sub Paste(sender As Object, e As System.EventArgs) Implements IXmlControl.Paste
			Dim dataObject As IDataObject = Clipboard.GetDataObject()

			If dataObject.GetDataPresent(DataFormats.Text) Then
				parent.Xml = DirectCast(dataObject.GetData(DataFormats.Text), String)
			End If
		End Sub

		Public Sub Delete(sender As Object, e As System.EventArgs) Implements IXmlControl.Delete
			If Nodes.Count > 0 AndAlso SelectedNode IsNot Nothing AndAlso parent.LabelEdit Then
				Dim tmp As String = Xml

				Try
					Dim elemToRemove As XmlNode = DirectCast(SelectedNode, XmlTreeNode).ConnectedXmlElement

					If elemToRemove IsNot Nothing Then
						elemToRemove.ParentNode.RemoveChild(elemToRemove)

						If SelectedNode.NextNode.Text.Equals("</" & elemToRemove.Name & ">") Then
							SelectedNode.NextNode.Remove()
						End If

						SelectedNode.Remove()
					End If
				Catch
					MessageBox.Show("Cannot delete this node ! Rolling back...", XmlTreeView.MessageBoxTitle)
					parent.Xml = tmp
				End Try
			End If
		End Sub

		Public Sub Save(sender As Object, e As System.EventArgs) Implements IXmlControl.Save
			If Nodes.Count > 0 AndAlso parent.SaveDialog.ShowDialog() = DialogResult.OK Then
				Using sw As New StreamWriter(parent.SaveDialog.FileName)
					sw.Write(Xml)
					parent.fileXml = Xml
					parent.filePath = parent.SaveDialog.FileName
				End Using
			End If
		End Sub

		''' <summary>
		''' Takes XML-data as a value.
		''' </summary>
		Public Property Xml() As String Implements IXmlControl.Xml
			Get
				Return document.OuterXml
			End Get
			Set
				If value.Length > 0 Then
					LoadXml(value)
				End If
			End Set
		End Property
		#End Region

		#Region "Event handlers for EditBox"

		Private Sub editBox_Leave(sender As Object, e As EventArgs)
			If Controls.Contains(editBox) Then
				RemoveHandler editBox.Leave, New EventHandler(AddressOf editBox_Leave)
				RemoveHandler editBox.KeyUp, New KeyEventHandler(AddressOf editBox_KeyUp)
				Controls.Remove(editBox)
				editingNode.Text = editBox.Text
				editBox.Dispose()
			End If
		End Sub

		Private Sub editBox_KeyUp(sender As Object, e As KeyEventArgs)
			If e.KeyCode = Keys.Escape Then
				RemoveHandler editBox.Leave, New EventHandler(AddressOf editBox_Leave)
				RemoveHandler editBox.KeyUp, New KeyEventHandler(AddressOf editBox_KeyUp)
				Controls.Remove(editBox)
				editBox.Dispose()
			End If
		End Sub
		#End Region

		#Region "Private methods"
		Private Sub RecursePrintTreeNodes(coll As TreeNodeCollection)
			If Not endOfPageReached Then
				For Each node As TreeNode In coll
					If endOfPageReached Then
						Exit For
					End If

					If Not printedNodes.Contains(node) Then
						Dim textToDraw As String = indent & node.Text
						Dim textWidthPx As Single = 0

						yPos = topMargin + (count * printFont.GetHeight())

						If (InlineAssignHelper(textWidthPx, eventArgs.Graphics.MeasureString(textToDraw, printFont).Width)) > eventArgs.MarginBounds.Width Then
							Dim startPos As Integer = 0
							Dim pixPerChar As Single = textWidthPx / textToDraw.Length
							Dim maxCharsPerLine As Integer = CInt(Math.Truncate(eventArgs.MarginBounds.Width / pixPerChar))

							While (startPos + maxCharsPerLine) < textToDraw.Length
								eventArgs.Graphics.DrawString(textToDraw.Substring(startPos, maxCharsPerLine), printFont, Brushes.Black, leftMargin, yPos, New StringFormat())
								startPos += maxCharsPerLine
								yPos += printFont.GetHeight()
								count += 1
							End While

							eventArgs.Graphics.DrawString(textToDraw.Substring(startPos), printFont, Brushes.Black, leftMargin, yPos, New StringFormat())
						Else
							eventArgs.Graphics.DrawString(textToDraw, printFont, Brushes.Black, leftMargin, yPos, New StringFormat())
						End If

						count += 1
						printedNodes.Add(node)
					End If

					If InlineAssignHelper(endOfPageReached, (count >= linesPerPage)) Then
						eventArgs.HasMorePages = True
						Exit For
					End If

					If node.Nodes.Count > 0 Then
						indent += "    "
						RecursePrintTreeNodes(node.Nodes)
					End If
				Next
			End If

			If indent.Length > 0 Then
				indent = indent.Substring(0, indent.Length - 4)
			End If
		End Sub

		Private Sub RecurseTreeNodes(nodes As TreeNodeCollection)
			If Not foundNode Then
				Dim nodeText As String = Nothing

				For Each node As TreeNode In nodes
					If foundNode Then
						Exit For
					End If

					nodeText = If(caseSensitive, node.Text, node.Text.ToUpper())

					If nodeText.IndexOf(searchText) > -1 AndAlso Not foundList.Contains(node) Then
						SelectedNode = node
						SelectedNode.BackColor = Color.Blue
						foundList.Add(node)
						foundNode = True
						Exit For
					End If

					If node.Nodes.Count > 0 Then
						RecurseTreeNodes(node.Nodes)
					End If
				Next
			End If
		End Sub

		Private Sub LoadXml(xml As String)
			SuspendLayout()
			document.LoadXml(xml)

			Nodes.Clear()

			If xml.StartsWith("<?") Then
				Nodes.Add(New XmlTreeNode(xml.Substring(0, xml.IndexOf("?>") + 2), Nothing))
			End If

			RecurseAndAssignNodes(document.DocumentElement)

			ExpandAll()
			ResumeLayout(False)
		End Sub

		Private Sub RecurseAndAssignNodes(elem As XmlNode)
			Dim attrs As String = String.Empty
			Dim addedNode As XmlTreeNode = Nothing

			If elem.NodeType = XmlNodeType.Element Then
				For Each attr As XmlAttribute In elem.Attributes
					attrs += " " & attr.Name & "=""" & attr.Value & """"
				Next
			End If

			If elem.Equals(document.DocumentElement) Then
				addedNode = New XmlTreeNode("<" & elem.Name & attrs & ">", elem)
				Nodes.Add(addedNode)
				InnerXmlTreeView.CurrentNode = addedNode
				Nodes.Add(New XmlTreeNode("</" & elem.Name & ">", Nothing))
			ElseIf elem.HasChildNodes AndAlso elem.ChildNodes(0).NodeType = XmlNodeType.Text Then
				addedNode = New XmlTreeNode("<" & elem.Name & attrs & ">" & elem.InnerText & "</" & elem.Name & ">", elem)
				InnerXmlTreeView.CurrentNode.Nodes.Add(addedNode)
				InnerXmlTreeView.CurrentNode = addedNode
			ElseIf TypeOf elem Is XmlElement AndAlso DirectCast(elem, XmlElement).IsEmpty Then
				addedNode = New XmlTreeNode("<" & elem.Name & attrs & "/>", elem)
				InnerXmlTreeView.CurrentNode.Nodes.Add(addedNode)
				InnerXmlTreeView.CurrentNode = addedNode
			Else
				addedNode = New XmlTreeNode("<" & elem.Name & attrs & ">", elem)
				InnerXmlTreeView.CurrentNode.Nodes.Add(addedNode)
				InnerXmlTreeView.CurrentNode = addedNode
				InnerXmlTreeView.CurrentNode.Parent.Nodes.Add(New XmlTreeNode("</" & elem.Name & ">", Nothing))
			End If

			For Each child As XmlNode In elem.ChildNodes
				If child.NodeType = XmlNodeType.Element Then
					RecurseAndAssignNodes(child)
				ElseIf child.NodeType = XmlNodeType.Comment Then
					InnerXmlTreeView.CurrentNode.Nodes.Add(New XmlTreeNode(child.OuterXml, child))
				End If
			Next

			If InnerXmlTreeView.CurrentNode.Parent IsNot Nothing Then
				InnerXmlTreeView.CurrentNode = InnerXmlTreeView.CurrentNode.Parent
			End If
		End Sub
		#End Region

		#Region "Event handlers for InnerXmlTreeView"
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

		Protected Overrides Sub OnDoubleClick(e As EventArgs)
			If SelectedNode.Parent IsNot Nothing AndAlso DirectCast(SelectedNode, XmlTreeNode).ConnectedXmlElement IsNot Nothing Then
				Edit(Nothing, Nothing)

			Else
			End If
		End Sub
		Private Shared Function InlineAssignHelper(Of T)(ByRef target As T, value As T) As T
			target = value
			Return value
		End Function
		#End Region
	End Class
End Namespace
