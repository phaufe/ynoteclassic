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
require "System.Xml.Linq, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
require "FastColoredTextBoxNS, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
require "SS.Ynote.Classic, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
#========================================
#
#Wavy Style can be use in error checking
#you can make a plugin for error checking for any language
#
#This is just a sample
#
#========================================
module WavyStylePlugin
    # <summary>
    # Simple Spell Checker
    # </summary>
    class Form1 < WeifenLuo::WinFormsUI::Docking::DockContent, IFormPlugin
        def initialize()
            @RedWavy = WavyLineStyle.new(100, Color.Red)
            @configuration = XElement.new("WavyStylePluginConfig")
            self.InitializeComponent()
            self.Hide()
            self.@Visible = false
            Main.ActiveEditor.codebox.TextChangedDelayed { |sender, e| codebox_TextChangedDelayed(sender, e) }
            MessageBox.Show("Spell Checker Initialized!")
        end

        def Title
            return "WavyStylePlugin"
        end

        def Description
            return "Wavy Style Sample Plugin"
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
            #get { return "C:\\Icons\\youricon.ico"; }
            return System::String.Empty
        end

        def Content
            return self
        end

        def ShowAs
            return self.ShowAs.Normal
        end

        def codebox_TextChangedDelayed(sender, e)
            SpellChecker = SpellChecker.new(Main.ActiveFastColoredTextBox, @"Dictionary.dic")
            SpellChecker.SpellCheck(sender)
        end
    end
end