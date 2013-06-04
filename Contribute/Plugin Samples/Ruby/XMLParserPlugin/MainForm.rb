require "mscorlib"
require "System, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
require "System.Collections.Generic, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
require "System.ComponentModel, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
require "System.Data, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
require "System.Drawing, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
require "System.Linq, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
require "System.Text, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
require "System.Windows.Forms, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
require "SS.Ynote.Engine.Framework.Plugins.Interface, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
require "WeifenLuo.WinFormsUI.Docking, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
require "System.Xml.Linq, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"

module XMLParserPlugin
	class MainForm < DockContent, IFormPlugin
		def initialize()
			#IFormPlugin Inherits the IPlugin Interface
			@configuration = XElement.new("XMLParserPluginConfig")
			self.InitializeComponent()
			self.@ShowHint = DockState.DockRight
		end

		def Title
			return "XMLParserPlugin"
		end

		def Description
			return "Sample XML Parser Plugin"
		end

		def Group
			return "Plugins"
		end

		def SubGroup
			return "Samples"
		end

		def Configuration
			return @configuration
		end

		def Configuration=(value)
			@configuration = value
		end

		def Icon
			return System::String.Empty
		end

		def Content
			return self
		end

		def ShowAs
			return self.ShowAs.Normal
		end

		def toolStripButton1_Click(sender, e)
			XmlTree.Xml = SS.Ynote.Classic.Main.ActiveEditor.codebox.Text
		end
	end
end