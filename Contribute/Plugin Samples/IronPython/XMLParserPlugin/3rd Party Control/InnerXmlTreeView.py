from System import *
from System.IO import *
from System.Xml import *
from System.Drawing import *
from System.Threading import *
from System.Collections import *
from System.Windows.Forms import *
from System.ComponentModel import *
from System.Drawing.Printing import *
from SS.Ynote.Engine.Controls import *

class InnerXmlTreeView(SSTreeView, IXmlControl):
	def __init__(self, container):
		""" <summary>
		 
		 </summary>
		"""
		self._document = None
		self._parent = None
		self._draggedFile = str.Empty
		self._CurrentNode = None
		self._foundList = ArrayList(100)
		self._printFont = Font("Arial", 10)
		self._linesPerPage = 0
		self._yPos = 0
		self._count = 0
		self._indent = str.Empty
		self._leftMargin = 0
		self._topMargin = 0
		self._endOfPageReached = False
		self._printedNodes = ArrayList(1000)
		self._eventArgs = None
		self._printDocument1 = PrintDocument()
		self._parent = container
		AllowDrop = True
		Dock = System.Windows.Forms.DockStyle.Fill
		ImageIndex = -1
		SelectedImageIndex = -1
		Size = System.Drawing.Size(150, 130)
		TabIndex = 0
		self._document = XmlDocument()
		ShowLines = False

	def Search(self, sender, e):
		if Nodes.Count > 0:
			SearchDlg(self).Show()

	def StartSearch(self, criterion, caseSensitive):
		""" <summary>
		 
		 </summary>
		 <param name="criterion"></param>
		 <param name="caseSensitive"></param>
		"""
		self._foundList.Clear()
		self._caseSensitive = caseSensitive
		self._searchText = criterion if caseSensitive else criterion.ToUpper()

	def Next(self):
		""" <summary>
		 
		 </summary>
		"""
		SelectedNode.BackColor = Color.Empty
		self._foundNode = False
		self.RecurseTreeNodes(Nodes)
		if not self._foundNode:
			MessageBox.Show("End of tree reached !", XmlTreeView.MessageBoxTitle)
			self._foundList.Clear()

	def Print(self, sender, e):
		if Nodes.Count > 0 and self._parent.PrintDialog.ShowDialog() == DialogResult.OK:
			self._printedNodes.Clear()
			self._printDocument1.Print()

	def PrintPage(self, sender, e):
		self._yPos = 0
		self._count = 0
		self._endOfPageReached = False
		self._eventArgs = e
		self._eventArgs.HasMorePages = False
		self._leftMargin = self._eventArgs.MarginBounds.Left
		self._topMargin = self._eventArgs.MarginBounds.Top
		self._linesPerPage = self._eventArgs.MarginBounds.Height / self._printFont.GetHeight(self._eventArgs.Graphics)
		# Iterate over the file, printing each line.
		self.RecursePrintTreeNodes(Nodes)

	def get_PrintDocument(self):
		return self._printDocument1

	PrintDocument = property(fget=get_PrintDocument)

	def Edit(self, sender, e):
		if Nodes.Count > 0 and SelectedNode != None and self._parent.LabelEdit:
			self._editingNode = SelectedNode
			if self._editingNode.ConnectedXmlElement != None:
				if not self._closingHandlerAssigned:
					parentForm = self.FindForm()
					parentForm.Closing += self._parent.form_Closing
					self._closingHandlerAssigned = True
				height = self._editingNode.Bounds.Height
				width = self._editingNode.Bounds.Width
				left = self._editingNode.Bounds.Left
				top = self._editingNode.Bounds.Top
				self._editingNode.ExpandAll()
				if self._editingNode.ConnectedXmlElement.HasChildNodes and self._editingNode.ConnectedXmlElement.FirstChild.NodeType != XmlNodeType.Text:
					height = self._editingNode.NextNode.Bounds.Bottom - self._editingNode.Bounds.Top
					width = Width - left
				self._editBox = TextBox()
				self._editBox.Multiline = True
				self._editBox.BorderStyle = BorderStyle.FixedSingle
				self._editBox.Leave += self.editBox_Leave
				self._editBox.KeyUp += self.editBox_KeyUp
				self._editBox.SetBounds(left, top, width, height)
				self._editingNode.RecurseSubNodes(self._editingNode.Parent)
				self._editBox.Text = self._editingNode.SelfAndChildren
				Controls.Add(self._editBox)
				self._editBox.Focus()

	def Copy(self, sender, e):
		if Nodes.Count > 0:
			Clipboard.SetDataObject(Xml, True)

	def Paste(self, sender, e):
		dataObject = Clipboard.GetDataObject()
		if dataObject.GetDataPresent(DataFormats.Text):
			self._parent.Xml = dataObject.GetData(DataFormats.Text)

	def Delete(self, sender, e):
		if Nodes.Count > 0 and None != SelectedNode and self._parent.LabelEdit:
			tmp = Xml
			try:
				elemToRemove = (SelectedNode).ConnectedXmlElement
				if None != elemToRemove:
					elemToRemove.ParentNode.RemoveChild(elemToRemove)
					if SelectedNode.NextNode.Text.Equals("</" + elemToRemove.Name + ">"):
						SelectedNode.NextNode.Remove()
					SelectedNode.Remove()
			except , :
				MessageBox.Show("Cannot delete this node ! Rolling back...", XmlTreeView.MessageBoxTitle)
				self._parent.Xml = tmp
			finally:

	def Save(self, sender, e):
		if Nodes.Count > 0 and self._parent.SaveDialog.ShowDialog() == DialogResult.OK:

	# <summary>
	# Takes XML-data as a value.
	# </summary>
	def get_Xml(self):
		return self._document.OuterXml

	def set_Xml(self, value):
		if value.Length > 0:
			self.LoadXml(value)

	Xml = property(fget=get_Xml, fset=set_Xml)

	def editBox_Leave(self, sender, e):
		if Controls.Contains(self._editBox):
			self._editBox.Leave -= self.editBox_Leave
			self._editBox.KeyUp -= self.editBox_KeyUp
			Controls.Remove(self._editBox)
			self._editingNode.Text = self._editBox.Text
			self._editBox.Dispose()

	def editBox_KeyUp(self, sender, e):
		if e.KeyCode == Keys.Escape:
			self._editBox.Leave -= self.editBox_Leave
			self._editBox.KeyUp -= self.editBox_KeyUp
			Controls.Remove(self._editBox)
			self._editBox.Dispose()

	def RecursePrintTreeNodes(self, coll):
		if not self._endOfPageReached:
			enumerator = coll.GetEnumerator()
			while enumerator.MoveNext():
				node = enumerator.Current
				if self._endOfPageReached:
					break
				if not self._printedNodes.Contains(node):
					textToDraw = self._indent + node.Text
					textWidthPx = 0
					self._yPos = self._topMargin + (self._count * self._printFont.GetHeight())
					if (textWidthPx = self._eventArgs.Graphics.MeasureString(textToDraw, self._printFont).Width) > self._eventArgs.MarginBounds.Width:
						startPos = 0
						pixPerChar = textWidthPx / textToDraw.Length
						maxCharsPerLine = (self._eventArgs.MarginBounds.Width / pixPerChar)
						while (startPos + maxCharsPerLine) < textToDraw.Length:
							self._eventArgs.Graphics.DrawString(textToDraw.Substring(startPos, maxCharsPerLine), self._printFont, Brushes.Black, self._leftMargin, self._yPos, StringFormat())
							startPos += maxCharsPerLine
							self._yPos += self._printFont.GetHeight()
							self._count += 1
						self._eventArgs.Graphics.DrawString(textToDraw.Substring(startPos), self._printFont, Brushes.Black, self._leftMargin, self._yPos, StringFormat())
					else:
						self._eventArgs.Graphics.DrawString(textToDraw, self._printFont, Brushes.Black, self._leftMargin, self._yPos, StringFormat())
					self._count += 1
					self._printedNodes.Add(node)
				if self._endOfPageReached = (self._count >= self._linesPerPage):
					self._eventArgs.HasMorePages = True
					break
				if node.Nodes.Count > 0:
					self._indent += "    "
					self.RecursePrintTreeNodes(node.Nodes)
		if self._indent.Length > 0:
			self._indent = self._indent.Substring(0, self._indent.Length - 4)

	def RecurseTreeNodes(self, nodes):
		if not self._foundNode:
			nodeText = None
			enumerator = nodes.GetEnumerator()
			while enumerator.MoveNext():
				node = enumerator.Current
				if self._foundNode:
					break
				nodeText = node.Text if self._caseSensitive else node.Text.ToUpper()
				if nodeText.IndexOf(self._searchText) > -1 and not self._foundList.Contains(node):
					SelectedNode = node
					SelectedNode.BackColor = Color.Blue
					self._foundList.Add(node)
					self._foundNode = True
					break
				if node.Nodes.Count > 0:
					self.RecurseTreeNodes(node.Nodes)

	def LoadXml(self, xml):
		self.SuspendLayout()
		self._document.LoadXml(xml)
		Nodes.Clear()
		if xml.StartsWith("<?"):
			Nodes.Add(XmlTreeNode(xml.Substring(0, xml.IndexOf("?>") + 2), None))
		self.RecurseAndAssignNodes(self._document.DocumentElement)
		self.ExpandAll()
		self.ResumeLayout(False)

	def RecurseAndAssignNodes(self, elem):
		attrs = str.Empty
		addedNode = None
		if elem.NodeType == XmlNodeType.Element:
			enumerator = elem.Attributes.GetEnumerator()
			while enumerator.MoveNext():
				attr = enumerator.Current
				attrs += " " + attr.Name + "=\"" + attr.Value + "\""
		if elem.Equals(self._document.DocumentElement):
			addedNode = XmlTreeNode("<" + elem.Name + attrs + ">", elem)
			Nodes.Add(addedNode)
			InnerXmlTreeView.CurrentNode = addedNode
			Nodes.Add(XmlTreeNode("</" + elem.Name + ">", None))
		elif elem.HasChildNodes and elem.ChildNodes[0].NodeType == XmlNodeType.Text:
			addedNode = XmlTreeNode("<" + elem.Name + attrs + ">" + elem.InnerText + "</" + elem.Name + ">", elem)
			InnerXmlTreeView.CurrentNode.Nodes.Add(addedNode)
			InnerXmlTreeView.CurrentNode = addedNode
		elif  and (elem).IsEmpty:
			addedNode = XmlTreeNode("<" + elem.Name + attrs + "/>", elem)
			InnerXmlTreeView.CurrentNode.Nodes.Add(addedNode)
			InnerXmlTreeView.CurrentNode = addedNode
		else:
			addedNode = XmlTreeNode("<" + elem.Name + attrs + ">", elem)
			InnerXmlTreeView.CurrentNode.Nodes.Add(addedNode)
			InnerXmlTreeView.CurrentNode = addedNode
			InnerXmlTreeView.CurrentNode.Parent.Nodes.Add(XmlTreeNode("</" + elem.Name + ">", None))
		enumerator = elem.ChildNodes.GetEnumerator()
		while enumerator.MoveNext():
			child = enumerator.Current
			if child.NodeType == XmlNodeType.Element:
				self.RecurseAndAssignNodes(child)
			elif child.NodeType == XmlNodeType.Comment:
				InnerXmlTreeView.CurrentNode.Nodes.Add(XmlTreeNode(child.OuterXml, child))
		if InnerXmlTreeView.CurrentNode.Parent != None:
			InnerXmlTreeView.CurrentNode = InnerXmlTreeView.CurrentNode.Parent

	def OnKeyUp(self, e):
		self.OnKeyUp(e)
		if e.Control and e.KeyCode == Keys.C:
			self.Copy(None, None)
		elif e.Control and e.KeyCode == Keys.V:
			self.Paste(None, None)
		elif e.Control and e.KeyCode == Keys.F:
			self.Search(None, None)
		elif e.Control and e.KeyCode == Keys.P:
			self.Print(None, None)
		elif e.Control and e.KeyCode == Keys.S:
			self.Save(None, None)
		elif e.KeyCode == Keys.Delete:
			self.Delete(None, None)
		elif e.KeyCode == Keys.Insert:
			self.Edit(None, None)

	def OnDragEnter(self, e):
		self.OnDragEnter(e)
		if e.Data.GetDataPresent(DataFormats.FileDrop):
			e.Effect = DragDropEffects.Copy
			self._draggedFile = (e.Data.GetData(DataFormats.FileDrop))[0]

	def OnDragDrop(self, e):
		self.OnDragDrop(e)
		if self._draggedFile.Length > 0:
			self._draggedFile = str.Empty

	def OnDoubleClick(self, e):
		if SelectedNode.Parent != None and (SelectedNode).ConnectedXmlElement != None:
			self.Edit(None, None)
		else: