from System import *
from System.Xml import *
from System.Windows.Forms import *
from SS.Ynote.Engine.Framework import *

class XmlTreeNode(TreeNode):
	""" <summary>
	 Summary description for XmlTreeNode.
	 </summary>
	"""
	def __init__(self, text, elem):
		self._elem = None
		self._childrenXml = str.Empty
		self._indent = str.Empty
		self._hitEnd = False
		self._hitStart = False
		self._elem = elem

	def get_ConnectedXmlElement(self):
		return elem

	ConnectedXmlElement = property(fget=get_ConnectedXmlElement)

	def get_SelfAndChildren(self):
		return self._childrenXml

	SelfAndChildren = property(fget=get_SelfAndChildren)

	def get_Text(self):
		return self.Text

	def set_Text(self, value):
		if value != None and value.Length > 0:
			try:
				if value != self._childrenXml:
					frag = elem.OwnerDocument.CreateDocumentFragment()
					frag.InnerXml = value
					elem.ParentNode.ReplaceChild(frag, elem)
					innerView = TreeView
					innerView.Xml = innerView.Xml
			except XmlException, xEx:
				MessageBox.Show(xEx.Message, XmlTreeView.MessageBoxTitle)
			finally:
				self._childrenXml = str.Empty
				self._hitEnd = False

	Text = property(fget=get_Text, fset=set_Text)

	def RecurseSubNodes(self, entryNode):
		enumerator = entryNode.Nodes.GetEnumerator()
		while enumerator.MoveNext():
			node = enumerator.Current
			if self.Equals(node):
				self._hitStart = True
			if not self._hitEnd and self._hitStart:
				self._hitEnd = (node.Text.EndsWith("</" + self._elem.Name + ">") and self._hitStart) or ((node).ConnectedXmlElement != None and (node).ConnectedXmlElement.NodeType == XmlNodeType.Comment)
				self._childrenXml += self._indent + node.Text + Environment.NewLine
				if node.Nodes.Count > 0:
					self._indent += "    "
					self.RecurseSubNodes(node)
		if self._indent.Length > 3:
			self._indent = self._indent.Substring(0, self._indent.Length - 4)