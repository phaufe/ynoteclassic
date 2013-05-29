require "mscorlib"
require "System, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
require "System.IO, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
require "System.Collections.Generic, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
require "System.ComponentModel, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
require "System.Data, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
require "System.Drawing, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
require "System.Linq, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
require "System.Text, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
require "System.Windows.Forms, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
require "System.Xml.Linq, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
require "SS.Ynote.Classic, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
require "SS.Ynote.Engine.Framework.Plugins, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
require "SS.Ynote.Engine.Framework.Plugins.Controls, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
require "SS.Ynote.Engine.Framework.Plugins.Interface, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
require "WeifenLuo.WinFormsUI.Docking, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"

module QuickNotePlugin
	class MainForm < DockContent, IFormPlugin
		def initialize()
			@configuration = XElement.new("ThisFormConfig")
			self.InitializeComponent()
			self.@newToolStripMenuItem1.Click { |sender, e| self.menuItem2_Click(sender, e) }
			self.@openToolStripMenuItem1.Click { |sender, e| self.menuItem3_Click(sender, e) }
			self.@importFromMainEditorToolStripMenuItem.Click { |sender, e| self.menuItem18_Click(sender, e) }
			self.@saveAsToolStripMenuItem1.Click { |sender, e| self.menuItem5_Click(sender, e) }
			self.@clearAllToolStripMenuItem.Click { |sender, e| self.menuItem2_Click(sender, e) }
			self.@cutToolStripMenuItem.Click { |sender, e| self.menuItem8_Click(sender, e) }
			self.@copyToolStripMenuItem.Click { |sender, e| self.menuItem9_Click(sender, e) }
			self.@pasteToolStripMenuItem.Click { |sender, e| self.menuItem10_Click(sender, e) }
			self.@selectAllToolStripMenuItem.Click { |sender, e| self.menuItem11_Click(sender, e) }
			self.@undoToolStripMenuItem.Click { |sender, e| self.menuItem7_Click(sender, e) }
			self.@fontToolStripMenuItem.Click { |sender, e| self.menuItem14_Click(sender, e) }
			self.@backColorToolStripMenuItem.Click { |sender, e| self.menuItem15_Click(sender, e) }
		end

		def Content
			return self
		end

		def ShowAs
			return self.ShowAs.Normal
		end

		def Title
			return "QuickNotePlugin"
		end

		def Description
			return "This is  a Quick Note Plugin for Ynote Classic"
		end

		def Group
			return "Plugins"
		end

		def SubGroup
			return "Note"
		end

		def Configuration
			return @configuration
		end

		def Configuration=(value)
			@configuration = value
		end

		def Icon
			#get { return "C:\\Icons\\Folder.ico"; }
			return System::String.Empty
		end

		def menuItem7_Click(sender, e)
			self.@txtmain.Undo()
		end

		def menuItem18_Click(sender, e)
			self.@txtmain.Text = Main.UserText
		end

		def menuItem2_Click(sender, e)
			self.@txtmain.Clear()
		end

		def menuItem3_Click(sender, e)
			o = System.Windows.Forms.OpenFileDialog.new()
			o.Filter = "Text Files (*.txt)|*.txt|All Files(*.*)|*.*"
			o.ShowDialog()
			txtmain.Text = File.ReadAllText(o.FileName, Encoding.Default)
		end

		def menuItem8_Click(sender, e)
			self.@txtmain.Cut()
		end

		def menuItem9_Click(sender, e)
			self.@txtmain.Copy()
		end

		def menuItem10_Click(sender, e)
			self.@txtmain.Paste()
		end

		def menuItem11_Click(sender, e)
			self.@txtmain.SelectAll()
		end

		def menuItem14_Click(sender, e)
			f = System.Windows.Forms.FontDialog.new()
			f.ShowEffects = true
			f.ShowColor = true
			f.ShowDialog()
			txtmain.Font = f.Font
			txtmain.ForeColor = f.Color
		end

		def menuItem15_Click(sender, e)
			c = System.Windows.Forms.ColorDialog.new()
			c.ShowDialog()
			self.@txtmain.BackColor = c.Color
		end

		def menuItem5_Click(sender, e)
			s = System.Windows.Forms.SaveFileDialog.new()
			s.Filter = "All Files (*.*)|*.*"
			s.ShowDialog()
			if s.FileName != "" then
				File.WriteAllText(s.FileName, txtmain.Text, Encoding.Default)
			end
		end

		def menuItem19_Click(sender, e)
			self.Close()
		end

		def exitToolStripMenuItem1_Click(sender, e)
			Application.Exit()
		end
	end
end