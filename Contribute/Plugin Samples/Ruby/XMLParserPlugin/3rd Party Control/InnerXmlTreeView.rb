require "mscorlib"
require "System, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
require "System.IO, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
require "System.Xml, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
require "System.Drawing, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
require "System.Threading, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
require "System.Collections, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
require "System.Windows.Forms, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
require "System.ComponentModel, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
require "System.Drawing.Printing, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
require "SS.Ynote.Engine.Controls, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"

module XMLTreeView
	class InnerXmlTreeView < SSTreeView, IXmlControl # <summary>
		# 
		# </summary>
		def initialize(container)
			@document = nil
			@parent = nil
			@draggedFile = System::String.Empty
			@CurrentNode = nil
			@foundList = ArrayList.new(100)
			@printFont = Font.new("Arial", 10)
			@linesPerPage = 0
			@yPos = 0
			@count = 0
			@indent = System::String.Empty
			@leftMargin = 0
			@topMargin = 0
			@endOfPageReached = false
			@printedNodes = ArrayList.new(1000)
			@eventArgs = nil
			@printDocument1 = PrintDocument.new()
			@parent = container
			AllowDrop = true
			Dock = System.Windows.Forms.DockStyle.Fill
			ImageIndex = -1
			SelectedImageIndex = -1
			Size = System.Drawing.Size.new(150, 130)
			TabIndex = 0
			@document = XmlDocument.new()
			ShowLines = false
		end

		def Search(sender, e)
			if Nodes.Count > 0 then
				SearchDlg.new(self).Show()
			end
		end

		# <summary>
		# 
		# </summary>
		# <param name="criterion"></param>
		# <param name="caseSensitive"></param>
		def StartSearch(criterion, caseSensitive)
			@foundList.Clear()
			self.@caseSensitive = caseSensitive
			@searchText = caseSensitive ? criterion : criterion.ToUpper()
		end

		# <summary>
		# 
		# </summary>
		def Next()
			SelectedNode.BackColor = Color.Empty
			@foundNode = false
			self.RecurseTreeNodes(Nodes)
			if not @foundNode then
				MessageBox.Show("End of tree reached !", XmlTreeView.MessageBoxTitle)
				@foundList.Clear()
			end
		end

		def Print(sender, e)
			if Nodes.Count > 0 and @parent.PrintDialog.ShowDialog() == DialogResult.OK then
				@printedNodes.Clear()
				@printDocument1.Print()
			end
		end

		def PrintPage(sender, e)
			@yPos = 0
			@count = 0
			@endOfPageReached = false
			@eventArgs = e
			@eventArgs.HasMorePages = false
			@leftMargin = @eventArgs.MarginBounds.Left
			@topMargin = @eventArgs.MarginBounds.Top
			@linesPerPage = @eventArgs.MarginBounds.Height / @printFont.GetHeight(@eventArgs.Graphics)
			# Iterate over the file, printing each line.
			self.RecursePrintTreeNodes(Nodes)
		end

		def PrintDocument
			return @printDocument1
		end

		def Edit(sender, e)
			if Nodes.Count > 0 and SelectedNode != nil and @parent.LabelEdit then
				@editingNode = SelectedNode
				if @editingNode.ConnectedXmlElement != nil then
					if not @closingHandlerAssigned then
						parentForm = self.FindForm()
						parentForm.Closing { @parent.form_Closing() }
						@closingHandlerAssigned = true
					end
					height = @editingNode.Bounds.Height
					width = @editingNode.Bounds.Width
					left = @editingNode.Bounds.Left
					top = @editingNode.Bounds.Top
					@editingNode.ExpandAll()
					if @editingNode.ConnectedXmlElement.HasChildNodes and @editingNode.ConnectedXmlElement.FirstChild.NodeType != XmlNodeType.Text then
						height = @editingNode.NextNode.Bounds.Bottom - @editingNode.Bounds.Top
						width = Width - left
					end
					@editBox = TextBox.new()
					@editBox.Multiline = true
					@editBox.BorderStyle = BorderStyle.FixedSingle
					@editBox.Leave { |sender, e| self.editBox_Leave(sender, e) }
					@editBox.KeyUp { |sender, e| self.editBox_KeyUp(sender, e) }
					@editBox.SetBounds(left, top, width, height)
					@editingNode.RecurseSubNodes(@editingNode.Parent)
					@editBox.Text = @editingNode.SelfAndChildren
					Controls.Add(@editBox)
					@editBox.Focus()
				end
			end
		end

		def Copy(sender, e)
			if Nodes.Count > 0 then
				Clipboard.SetDataObject(Xml, true)
			end
		end

		def Paste(sender, e)
			dataObject = Clipboard.GetDataObject()
			if dataObject.GetDataPresent(DataFormats.Text) then
				@parent.Xml = dataObject.GetData(DataFormats.Text)
			end
		end

		def Delete(sender, e)
			if Nodes.Count > 0 and nil != SelectedNode and @parent.LabelEdit then
				tmp = Xml
				begin
					elemToRemove = (SelectedNode).ConnectedXmlElement
					if nil != elemToRemove then
						elemToRemove.ParentNode.RemoveChild(elemToRemove)
						if SelectedNode.NextNode.Text.Equals("</" + elemToRemove.Name + ">") then
							SelectedNode.NextNode.Remove()
						end
						SelectedNode.Remove()
					end
				rescue  => 
					MessageBox.Show("Cannot delete this node ! Rolling back...", XmlTreeView.MessageBoxTitle)
					@parent.Xml = tmp
				ensure
				end
			end
		end

		def Save(sender, e)
			if Nodes.Count > 0 and @parent.SaveDialog.ShowDialog() == DialogResult.OK then
				sw = StreamWriter.new(@parent.SaveDialog.FileName)
				sw.Write(Xml)
				@parent.fileXml = Xml
				@parent.filePath = @parent.SaveDialog.FileName
			end
		end

		# <summary>
		# Takes XML-data as a value.
		# </summary>
		def Xml
			return @document.OuterXml
		end

		def Xml=(value)
			if value.Length > 0 then
				self.LoadXml(value)
			end
		end

		def editBox_Leave(sender, e)
			if Controls.Contains(@editBox) then
				@editBox.Leave.remove(OnEventHandler.new(editBox_Leave))
				@editBox.KeyUp.remove(OnKeyEventHandler.new(editBox_KeyUp))
				Controls.Remove(@editBox)
				@editingNode.Text = @editBox.Text
				@editBox.Dispose()
			end
		end

		def editBox_KeyUp(sender, e)
			if e.KeyCode == Keys.Escape then
				@editBox.Leave.remove(OnEventHandler.new(editBox_Leave))
				@editBox.KeyUp.remove(OnKeyEventHandler.new(editBox_KeyUp))
				Controls.Remove(@editBox)
				@editBox.Dispose()
			end
		end

		def RecursePrintTreeNodes(coll)
			if not @endOfPageReached then
				enumerator = coll.GetEnumerator()
				while enumerator.MoveNext()
					node = enumerator.Current
					if @endOfPageReached then
						break
					end
					if not @printedNodes.Contains(node) then
						textToDraw = @indent + node.Text
						textWidthPx = 0
						@yPos = @topMargin + (@count * @printFont.GetHeight())
						if (textWidthPx = @eventArgs.Graphics.MeasureString(textToDraw, @printFont).Width) > @eventArgs.MarginBounds.Width then
							startPos = 0
							pixPerChar = textWidthPx / textToDraw.Length
							maxCharsPerLine = (@eventArgs.MarginBounds.Width / pixPerChar)
							while (startPos + maxCharsPerLine) < textToDraw.Length
								@eventArgs.Graphics.DrawString(textToDraw.Substring(startPos, maxCharsPerLine), @printFont, Brushes.Black, @leftMargin, @yPos, StringFormat.new())
								startPos += maxCharsPerLine
								@yPos += @printFont.GetHeight()
								@count += 1
							end
							@eventArgs.Graphics.DrawString(textToDraw.Substring(startPos), @printFont, Brushes.Black, @leftMargin, @yPos, StringFormat.new())
						else
							@eventArgs.Graphics.DrawString(textToDraw, @printFont, Brushes.Black, @leftMargin, @yPos, StringFormat.new())
						end
						@count += 1
						@printedNodes.Add(node)
					end
					if @endOfPageReached = (@count >= @linesPerPage) then
						@eventArgs.HasMorePages = true
						break
					end
					if node.Nodes.Count > 0 then
						@indent += "    "
						self.RecursePrintTreeNodes(node.Nodes)
					end
				end
			end
			if @indent.Length > 0 then
				@indent = @indent.Substring(0, @indent.Length - 4)
			end
		end

		def RecurseTreeNodes(nodes)
			if not @foundNode then
				nodeText = nil
				enumerator = nodes.GetEnumerator()
				while enumerator.MoveNext()
					node = enumerator.Current
					if @foundNode then
						break
					end
					nodeText = @caseSensitive ? node.Text : node.Text.ToUpper()
					if nodeText.IndexOf(@searchText) > -1 and not @foundList.Contains(node) then
						SelectedNode = node
						SelectedNode.BackColor = Color.Blue
						@foundList.Add(node)
						@foundNode = true
						break
					end
					if node.Nodes.Count > 0 then
						self.RecurseTreeNodes(node.Nodes)
					end
				end
			end
		end

		def LoadXml(xml)
			self.SuspendLayout()
			@document.LoadXml(xml)
			Nodes.Clear()
			if xml.StartsWith("<?") then
				Nodes.Add(XmlTreeNode.new(xml.Substring(0, xml.IndexOf("?>") + 2), nil))
			end
			self.RecurseAndAssignNodes(@document.DocumentElement)
			self.ExpandAll()
			self.ResumeLayout(false)
		end

		def RecurseAndAssignNodes(elem)
			attrs = System::String.Empty
			addedNode = nil
			if elem.NodeType == XmlNodeType.Element then
				enumerator = elem.Attributes.GetEnumerator()
				while enumerator.MoveNext()
					attr = enumerator.Current
					attrs += " " + attr.Name + "=\"" + attr.Value + "\""
				end
			end
			if elem.Equals(@document.DocumentElement) then
				addedNode = XmlTreeNode.new("<" + elem.Name + attrs + ">", elem)
				Nodes.Add(addedNode)
				InnerXmlTreeView.CurrentNode = addedNode
				Nodes.Add(XmlTreeNode.new("</" + elem.Name + ">", nil))
			elsif elem.HasChildNodes and elem.ChildNodes[0].NodeType == XmlNodeType.Text then
				addedNode = XmlTreeNode.new("<" + elem.Name + attrs + ">" + elem.InnerText + "</" + elem.Name + ">", elem)
				InnerXmlTreeView.CurrentNode.Nodes.Add(addedNode)
				InnerXmlTreeView.CurrentNode = addedNode
			elsif elem and (elem).IsEmpty then
				addedNode = XmlTreeNode.new("<" + elem.Name + attrs + "/>", elem)
				InnerXmlTreeView.CurrentNode.Nodes.Add(addedNode)
				InnerXmlTreeView.CurrentNode = addedNode
			else
				addedNode = XmlTreeNode.new("<" + elem.Name + attrs + ">", elem)
				InnerXmlTreeView.CurrentNode.Nodes.Add(addedNode)
				InnerXmlTreeView.CurrentNode = addedNode
				InnerXmlTreeView.CurrentNode.Parent.Nodes.Add(XmlTreeNode.new("</" + elem.Name + ">", nil))
			end
			enumerator = elem.ChildNodes.GetEnumerator()
			while enumerator.MoveNext()
				child = enumerator.Current
				if child.NodeType == XmlNodeType.Element then
					self.RecurseAndAssignNodes(child)
				elsif child.NodeType == XmlNodeType.Comment then
					InnerXmlTreeView.CurrentNode.Nodes.Add(XmlTreeNode.new(child.OuterXml, child))
				end
			end
			if InnerXmlTreeView.CurrentNode.Parent != nil then
				InnerXmlTreeView.CurrentNode = InnerXmlTreeView.CurrentNode.Parent
			end
		end

		def OnKeyUp(e)
			self.OnKeyUp(e)
			if e.Control and e.KeyCode == Keys.C then
				self.Copy(nil, nil)
			elsif e.Control and e.KeyCode == Keys.V then
				self.Paste(nil, nil)
			elsif e.Control and e.KeyCode == Keys.F then
				self.Search(nil, nil)
			elsif e.Control and e.KeyCode == Keys.P then
				self.Print(nil, nil)
			elsif e.Control and e.KeyCode == Keys.S then
				self.Save(nil, nil)
			elsif e.KeyCode == Keys.Delete then
				self.Delete(nil, nil)
			elsif e.KeyCode == Keys.Insert then
				self.Edit(nil, nil)
			end
		end

		def OnDragEnter(e)
			self.OnDragEnter(e)
			if e.Data.GetDataPresent(DataFormats.FileDrop) then
				e.Effect = DragDropEffects.Copy
				@draggedFile = (e.Data.GetData(DataFormats.FileDrop))[0]
			end
		end

		def OnDragDrop(e)
			self.OnDragDrop(e)
			if @draggedFile.Length > 0 then
				fs = StreamReader.new(@draggedFile)
				@parent.fileXml = fs.ReadToEnd()
				@parent.Xml = @parent.fileXml
				@parent.filePath = @draggedFile
				@draggedFile = System::String.Empty
			end
		end

		def OnDoubleClick(e)
			if SelectedNode.Parent != nil and (SelectedNode).ConnectedXmlElement != nil then
				self.Edit(nil, nil)
			else
			end
		end
	end
end