import clr

from System import *
from System.IO import *
from System.Drawing import *
from System.Windows.Forms import *
from System.ComponentModel import *
from System.Drawing.Printing import *
from SS.Ynote.Engine.Framework import *

class InnerTextBox(TextBox, IXmlControl):
	""" <summary>
	 Summary description for InnerTextBox.
	 </summary>
	"""
	def __init__(self, container):
		self._parent = None
		self._draggedFile = str.Empty
		self._printDocument1 = PrintDocument()
		self._printFont = Font("Arial", 10)
		self._parent = container
		ReadOnly = True
		Multiline = True
		Dock = DockStyle.Fill
		self._BackColor = Color.White

	def InitializeComponent(self):
		resources = System.ComponentModel.ComponentResourceManager(clr.GetClrType(InnerTextBox))
		((self)).BeginInit()
		self.SuspendLayout()
		# 
		# InnerTextBox
		# 
		resources.ApplyResources(self, "$this")
		self._BackColor = Color.White
		self._Name = "InnerTextBox"
		((self)).EndInit()
		self.ResumeLayout(False)

	def Search(self, sender, e):
		if Text.Length > 0:
			SearchDlg(self).Show()

	def StartSearch(self, criterion, caseSensitive):
		""" <summary>
		 
		 </summary>
		 <param name="criterion"></param>
		 <param name="caseSensitive"></param>
		"""
		self._startPos = 0
		self._caseSensitive = caseSensitive
		self._criterion = criterion if caseSensitive else criterion.ToUpper()

	def Next(self):
		""" <summary>
		 
		 </summary>
		"""
		foundPos = 0
		SelectionLength = 0
		internalText = Text if self._caseSensitive else Text.ToUpper()
		if internalText.Length > 0:
			if (foundPos = internalText.IndexOf(self._criterion, self._startPos)) > -1:
				SelectionStart = foundPos
				SelectionLength = self._criterion.Length
				self._startPos = foundPos + self._criterion.Length
				self.Focus()
			else:
				MessageBox.Show("End of text reached !", XmlTreeView.MessageBoxTitle)
				self._startPos = 0

	def Print(self, sender, e):
		if Text.Length > 0 and self._parent.PrintDialog.ShowDialog() == DialogResult.OK:
			self._printDocument1.Print()

	def PrintPage(self, sender, e):
		linesPrinted = 0
		yPos = 0
		endOfPageReached = False
		textToDraw = str.Empty
		if self._fontHeight == 0.0: # nothing initialized yet
			self._fontHeight = self._printFont.GetHeight(e.Graphics)
			self._linesPerPage = e.MarginBounds.Height / self._fontHeight
			self._printableWidth = Text.Length / ((e.Graphics.MeasureString(Text, self._printFont).Width / e.MarginBounds.Width))
		while textToDraw.Length > 0 and not endOfPageReached:
			if self._xPos < Text.Length - 1:
				textToDraw = Text.Substring(self._xPos, self._printableWidth) if self._xPos + self._printableWidth < Text.Length - 1 else Text.Substring(self._xPos)
				e.Graphics.DrawString(textToDraw, self._printFont, Brushes.Black, e.MarginBounds.Left, yPos, StringFormat())
				yPos += self._fontHeight
				self._xPos += self._printableWidth
				linesPrinted += 1
			if self._xPos >= Text.Length or linesPrinted >= self._linesPerPage:
				endOfPageReached = True
		e.HasMorePages = (self._xPos < Text.Length - 1)

	def get_PrintDocument(self):
		return self._printDocument1

	PrintDocument = property(fget=get_PrintDocument)

	def Edit(self, sender, e):
		if not self._closingHandlerAssigned:
			parentForm = self.FindForm()
			parentForm.Closing += self._parent.form_Closing
			self._closingHandlerAssigned = True
		ReadOnly = False

	def Delete(self, sender, e):
		Text = Text.Remove(SelectionStart, SelectionLength)

	def Copy(self, sender, e):
		self.Copy()

	def Paste(self, sender, e):
		dataObject = Clipboard.GetDataObject()
		if dataObject.GetDataPresent(DataFormats.Text):
			self._parent.Xml = dataObject.GetData(DataFormats.Text)

	def Save(self, sender, e):
		if Text.Length > 0 and self._parent.SaveDialog.ShowDialog() == DialogResult.OK:

	# <summary>
	# 
	# </summary>
	def get_Xml(self):
		return Text

	def set_Xml(self, value):
		Text = value

	Xml = property(fget=get_Xml, fset=set_Xml)

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