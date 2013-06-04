require "mscorlib"

require "mscorlib"
require "System, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
require "System.IO, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
require "System.Drawing, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
require "System.Windows.Forms, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
require "System.ComponentModel, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
require "System.Drawing.Printing, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
require "SS.Ynote.Engine.Framework, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"

module XMLTreeView
	# <summary>
	# Summary description for InnerTextBox.
	# </summary>
	class InnerTextBox < TextBox, IXmlControl
		def initialize(container)
			@parent = nil
			@draggedFile = System::String.Empty
			@printDocument1 = PrintDocument.new()
			@printFont = Font.new("Arial", 10)
			@parent = container
			ReadOnly = true
			Multiline = true
			Dock = DockStyle.Fill
			self.@BackColor = Color.White
		end

		def InitializeComponent()
			resources = System.ComponentModel.ComponentResourceManager.new(InnerTextBox.to_clr_type)
			((self)).BeginInit()
			self.SuspendLayout()
			# 
			# InnerTextBox
			# 
			resources.ApplyResources(self, "$this")
			self.@BackColor = Color.White
			self.@Name = "InnerTextBox"
			((self)).EndInit()
			self.ResumeLayout(false)
		end

		def Search(sender, e)
			if Text.Length > 0 then
				SearchDlg.new(self).Show()
			end
		end

		# <summary>
		# 
		# </summary>
		# <param name="criterion"></param>
		# <param name="caseSensitive"></param>
		def StartSearch(criterion, caseSensitive)
			@startPos = 0
			self.@caseSensitive = caseSensitive
			self.@criterion = caseSensitive ? criterion : criterion.ToUpper()
		end

		# <summary>
		# 
		# </summary>
		def Next()
			foundPos = 0
			SelectionLength = 0
			internalText = @caseSensitive ? Text : Text.ToUpper()
			if internalText.Length > 0 then
				if (foundPos = internalText.IndexOf(@criterion, @startPos)) > -1 then
					SelectionStart = foundPos
					SelectionLength = @criterion.Length
					@startPos = foundPos + @criterion.Length
					self.Focus()
				else
					MessageBox.Show("End of text reached !", XmlTreeView.MessageBoxTitle)
					@startPos = 0
				end
			end
		end

		def Print(sender, e)
			if Text.Length > 0 and @parent.PrintDialog.ShowDialog() == DialogResult.OK then
				@printDocument1.Print()
			end
		end

		def PrintPage(sender, e)
			linesPrinted = 0
			yPos = 0
			endOfPageReached = false
			textToDraw = System::String.Empty
			if @fontHeight == 0.0 then # nothing initialized yet
				@fontHeight = @printFont.GetHeight(e.Graphics)
				@linesPerPage = e.MarginBounds.Height / @fontHeight
				@printableWidth = Text.Length / ((e.Graphics.MeasureString(Text, @printFont).Width / e.MarginBounds.Width))
			end
			while textToDraw.Length > 0 and not endOfPageReached
				if @xPos < Text.Length - 1 then
					textToDraw = @xPos + @printableWidth < Text.Length - 1 ? Text.Substring(@xPos, @printableWidth) : Text.Substring(@xPos)
					e.Graphics.DrawString(textToDraw, @printFont, Brushes.Black, e.MarginBounds.Left, yPos, StringFormat.new())
					yPos += @fontHeight
					@xPos += @printableWidth
					linesPrinted += 1
				end
				if @xPos >= Text.Length or linesPrinted >= @linesPerPage then
					endOfPageReached = true
				end
			end
			e.HasMorePages = (@xPos < Text.Length - 1)
		end

		def PrintDocument
			return @printDocument1
		end

		def Edit(sender, e)
			if not @closingHandlerAssigned then
				parentForm = self.FindForm()
				parentForm.Closing { @parent.form_Closing() }
				@closingHandlerAssigned = true
			end
			ReadOnly = false
		end

		def Delete(sender, e)
			Text = Text.Remove(SelectionStart, SelectionLength)
		end

		def Copy(sender, e)
			self.Copy()
		end

		def Paste(sender, e)
			dataObject = Clipboard.GetDataObject()
			if dataObject.GetDataPresent(DataFormats.Text) then
				@parent.Xml = dataObject.GetData(DataFormats.Text)
			end
		end

		def Save(sender, e)
			if Text.Length > 0 and @parent.SaveDialog.ShowDialog() == DialogResult.OK then
				sw = StreamWriter.new(@parent.SaveDialog.FileName)
				sw.Write(Xml)
				@parent.fileXml = Xml
				@parent.filePath = @parent.SaveDialog.FileName
			end
		end

		# <summary>
		# 
		# </summary>
		def Xml
			return Text
		end

		def Xml=(value)
			Text = value
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
	end
end