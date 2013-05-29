Imports System.Xml
Imports System.Windows.Forms
Imports SS.Ynote.Engine.Framework

Namespace XMLTreeView
	''' <summary>
	''' Summary description for XmlTreeNode.
	''' </summary>
	Friend Class XmlTreeNode
		Inherits TreeNode
		#Region "Fields"
		Private elem As XmlNode = Nothing
		Private childrenXml As String = String.Empty
		Private indent As String = String.Empty
		Private hitEnd As Boolean = False
		Private hitStart As Boolean = False
		#End Region

		#Region "Constructors"
		Friend Sub New(text As String, elem As XmlNode)
			MyBase.New(text)
			Me.elem = elem
		End Sub
		#End Region

		#Region "Properties"
		Friend ReadOnly Property ConnectedXmlElement() As XmlNode
			Get
				Return elem
			End Get
		End Property

		Friend ReadOnly Property SelfAndChildren() As String
			Get
				Return childrenXml
			End Get
		End Property

		Public Shadows Property Text() As String
			Get
				Return MyBase.Text
			End Get
			Set
				If value IsNot Nothing AndAlso value.Length > 0 Then
					Try
						If value <> childrenXml Then
							Dim frag As XmlDocumentFragment = elem.OwnerDocument.CreateDocumentFragment()
							frag.InnerXml = value
							elem.ParentNode.ReplaceChild(frag, elem)
							Dim innerView As InnerXmlTreeView = DirectCast(TreeView, InnerXmlTreeView)
							innerView.Xml = innerView.Xml
						End If
					Catch xEx As XmlException
						MessageBox.Show(xEx.Message, XmlTreeView.MessageBoxTitle)
					Finally
						childrenXml = String.Empty
						hitEnd = False
					End Try
				End If
			End Set
		End Property
		#End Region

		#Region "Internal methods"
		Friend Sub RecurseSubNodes(entryNode As TreeNode)
			For Each node As TreeNode In entryNode.Nodes
				If Me.Equals(node) Then
					hitStart = True
				End If

				If Not hitEnd AndAlso hitStart Then
					hitEnd = (node.Text.EndsWith("</" & elem.Name & ">") AndAlso hitStart) OrElse (DirectCast(node, XmlTreeNode).ConnectedXmlElement IsNot Nothing AndAlso DirectCast(node, XmlTreeNode).ConnectedXmlElement.NodeType = XmlNodeType.Comment)

					childrenXml += indent & node.Text & Environment.NewLine

					If node.Nodes.Count > 0 Then
						indent += "    "
						RecurseSubNodes(node)
					End If
				End If
			Next

			If indent.Length > 3 Then
				indent = indent.Substring(0, indent.Length - 4)
			End If
		End Sub
		#End Region
	End Class
End Namespace
