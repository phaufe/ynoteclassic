require "mscorlib"
require "System, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
require "System.IO, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
require "System.Xml, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
require "System.Data, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
require "System.Drawing, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
require "System.Collections, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
require "System.Windows.Forms, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
require "System.ComponentModel, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
require "System.Drawing.Printing, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"

module XMLTreeView
	# <summary>
	# Summary description for XmlTreeView.
	# </summary>
	class XmlTreeView < System::Windows::Forms::UserControl
		# <summary>
		# Required designer variable.
		# </summary>
		def initialize()
			@content = System::String.Empty
			@fileXml = System::String.Empty
			@filePath = System::String.Empty
			@MessageBoxTitle = "XmlTreeView"
			@components = nil
			# This call is required by the Windows.Forms Form Designer.
			self.InitializeComponent()
		end
 # <summary>
		# Clean up any resources being used.
		# </summary>
		def Dispose(disposing)
			if disposing then
				if @components != nil then
					@components.Dispose()
				end
			end
			self.Dispose(disposing)
		end
 # <summary>
		# Required method for Designer support - do not modify
		# the contents of this method with the code editor.
		# </summary>
		def InitializeComponent()
			self.@popUpMenu = System.Windows.Forms.ContextMenu.new()
			self.@printMenuItem = System.Windows.Forms.MenuItem.new()
			self.@searchMenuItem = System.Windows.Forms.MenuItem.new()
			self.@deleteMenuItem = System.Windows.Forms.MenuItem.new()
			self.@saveMenuItem = System.Windows.Forms.MenuItem.new()
			self.@pasteMenuItem = System.Windows.Forms.MenuItem.new()
			self.@copyMenuItem = System.Windows.Forms.MenuItem.new()
			self.@editMenuItem = System.Windows.Forms.MenuItem.new()
			self.@printDialog1 = System.Windows.Forms.PrintDialog.new()
			self.@saveFileDialog1 = System.Windows.Forms.SaveFileDialog.new()
			self.SuspendLayout()
			# 
			# popUpMenu
			# 
			self.@popUpMenu.MenuItems.AddRange(Array[System::Windows::Forms::MenuItem].new([self.@printMenuItem, self.@searchMenuItem, self.@deleteMenuItem, self.@saveMenuItem, self.@pasteMenuItem, self.@copyMenuItem, self.@editMenuItem]))
			# 
			# printMenuItem
			# 
			self.@printMenuItem.Index = 0
			self.@printMenuItem.Text = "&Print"
			# 
			# searchMenuItem
			# 
			self.@searchMenuItem.Index = 1
			self.@searchMenuItem.Text = "&Search"
			# 
			# deleteMenuItem
			# 
			self.@deleteMenuItem.Index = 2
			self.@deleteMenuItem.Text = "&Delete"
			# 
			# saveMenuItem
			# 
			self.@saveMenuItem.Index = 3
			self.@saveMenuItem.Text = "S&ave As..."
			# 
			# pasteMenuItem
			# 
			self.@pasteMenuItem.Index = 4
			self.@pasteMenuItem.Text = "Past&e"
			# 
			# copyMenuItem
			# 
			self.@copyMenuItem.Index = 5
			self.@copyMenuItem.Text = "&Copy"
			# 
			# editMenuItem
			# 
			self.@editMenuItem.Index = 6
			self.@editMenuItem.Text = "Ed&it"
			# 
			# saveFileDialog1
			# 
			self.@saveFileDialog1.Filter = "XML-Files|*.xml"
			# 
			# XmlTreeView
			# 
			self.@BackColor = System.Drawing.Color.White
			self.@Name = "XmlTreeView"
			self.@Size = System.Drawing.Size.new(232, 321)
			self.ResumeLayout(false)
		end

		def DeregisterEvents(control)
			if nil != control then
				self.@printMenuItem.Click.remove(OnSystem.EventHandler.new(control.Print))
				self.@searchMenuItem.Click.remove(OnSystem.EventHandler.new(control.Search))
				self.@deleteMenuItem.Click.remove(OnSystem.EventHandler.new(control.Delete))
				self.@saveMenuItem.Click.remove(OnSystem.EventHandler.new(control.Save))
				self.@pasteMenuItem.Click.remove(OnSystem.EventHandler.new(control.Paste))
				self.@copyMenuItem.Click.remove(OnSystem.EventHandler.new(control.Copy))
				self.@editMenuItem.Click.remove(OnSystem.EventHandler.new(control.Edit))
				self.@printDialog1.Document.PrintPage.remove(OnSystem.Drawing.Printing.PrintPageEventHandler.new(control.PrintPage))
			end
		end

		def RegisterEvents(control)
			if nil != control then
				self.@printMenuItem.Click { control.Print() }
				self.@searchMenuItem.Click { control.Search() }
				self.@deleteMenuItem.Click { control.Delete() }
				self.@saveMenuItem.Click { control.Save() }
				self.@pasteMenuItem.Click { control.Paste() }
				self.@copyMenuItem.Click { control.Copy() }
				self.@editMenuItem.Click { control.Edit() }
				self.@printDialog1.Document = control.PrintDocument
				self.@printDialog1.Document.PrintPage { control.PrintPage() }
			end
		end

		def form_Closing(sender, e)
			if not @content.Equals(Xml) then
				if MessageBox.Show("Save changes?", XmlTreeView.MessageBoxTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes then
					if @saveFileDialog1.ShowDialog() == DialogResult.OK then
						sw = StreamWriter.new(@saveFileDialog1.FileName)
						sw.Write(Xml)
					end
				end
			end
		end
 # <summary>
		# Sets or gets XML-data in a string form.
		# </summary>
		def Xml
			result = System::String.Empty
			if Controls.Count > 0 then
				result = (Controls[0]).Xml
			end
			return result
		end

		def Xml=(value)
			Controls.Clear()
			begin
				self.DeregisterEvents(@xmlControl)
				@xmlControl = InnerXmlTreeView.new(self)
				@xmlControl.Xml = value
				@content = @xmlControl.Xml
				self.RegisterEvents(@xmlControl)
			rescue  => 
				Controls.Clear()
				self.DeregisterEvents(@xmlControl)
				@xmlControl = InnerTextBox.new(self)
				@xmlControl.Xml = value
				self.RegisterEvents(@xmlControl)
			ensure
			end
			(@xmlControl).ContextMenu = self.@popUpMenu
			Controls.Add(@xmlControl)
		end

		# <summary>
		# Sets or gets the fully qualified path to a file containing XML-data.
		# </summary>
		def XmlFile
			if @fileXml != self.Xml then
				@filePath = System::String.Empty
			end
			return @filePath
		end

		def XmlFile=(value)
			if nil != value and value.Length > 0 then
				fs = StreamReader.new(value)
				@fileXml = fs.ReadToEnd()
				self.Xml = @fileXml
				@filePath = value
			end
		end

		def PrintDialog
			return @printDialog1
		end

		def SaveDialog
			return @saveFileDialog1
		end

		def LabelEdit
			# <summary>
			# 
			# </summary>
			return @deleteMenuItem.Enabled and @editMenuItem.Enabled
		end

		def LabelEdit=(value)
			@editMenuItem.Enabled = @deleteMenuItem.Enabled = value
		end
	end
end