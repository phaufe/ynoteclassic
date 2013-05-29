require "mscorlib"
require "System, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
require "System.Xml, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
require "System.Windows.Forms, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
require "SS.Ynote.Engine.Framework, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"

module XMLTreeView
	# <summary>
	# Summary description for XmlTreeNode.
	# </summary>
	class XmlTreeNode < TreeNode
		def initialize(text, elem)
			@elem = nil
			@childrenXml = System::String.Empty
			@indent = System::String.Empty
			@hitEnd = false
			@hitStart = false
			self.@elem = elem
		end

		def ConnectedXmlElement
			return elem
		end

		def SelfAndChildren
			return @childrenXml
		end

		def Text
			return self.Text
		end

		def Text=(value)
			if value != nil and value.Length > 0 then
				begin
					if value != @childrenXml then
						frag = elem.OwnerDocument.CreateDocumentFragment()
						frag.InnerXml = value
						elem.ParentNode.ReplaceChild(frag, elem)
						innerView = TreeView
						innerView.Xml = innerView.Xml
					end
				rescue XmlException => xEx
					MessageBox.Show(xEx.Message, XmlTreeView.MessageBoxTitle)
				ensure
					@childrenXml = System::String.Empty
					@hitEnd = false
				end
			end
		end

		def RecurseSubNodes(entryNode)
			enumerator = entryNode.Nodes.GetEnumerator()
			while enumerator.MoveNext()
				node = enumerator.Current
				if self.Equals(node) then
					@hitStart = true
				end
				if not @hitEnd and @hitStart then
					@hitEnd = (node.Text.EndsWith("</" + @elem.Name + ">") and @hitStart) or ((node).ConnectedXmlElement != nil and (node).ConnectedXmlElement.NodeType == XmlNodeType.Comment)
					@childrenXml += @indent + node.Text + Environment.NewLine
					if node.Nodes.Count > 0 then
						@indent += "    "
						self.RecurseSubNodes(node)
					end
				end
			end
			if @indent.Length > 3 then
				@indent = @indent.Substring(0, @indent.Length - 4)
			end
		end
	end
end