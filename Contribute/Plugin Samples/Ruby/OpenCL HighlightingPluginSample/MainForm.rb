#CL Syntax Highlighter Plugin
require "mscorlib"
require "System, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
require "System.Collections.Generic, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
require "System.Linq, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
require "System.Text, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
require "WeifenLuo.WinFormsUI.Docking, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
require "SS.Ynote.Engine.Framework.Plugins.Interface, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
require "System.Xml.Linq, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
require "FastColoredTextBoxNS, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
require "SS.Ynote.Classic, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
require "System.Windows.Forms, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"

module HighlightingPluginSample
    class MainForm < DockContent, IFormPlugin
        def initialize()
            #IFormPlugin Inherits the IPlugin Interface
            @configuration = XElement.new("HighlighterPluginConfig")
            self.InitializeComponent()
            self.@ShowHint = DockState.DockLeft
        end

        def Title
            return "HighlighterPlugin"
        end

        def Description
            return "Sample Highlighter Plugin"
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

        def codebox_TextChangedDelayed(sender, e)
            Custom = List[ExplorerItem].new()
            HighlightingPluginSample.CLSyntaxHighlighter.Highlight(e.ChangedRange, Custom)
        end

        def InitializeComponent()
            self.@button1 = System.Windows.Forms.Button.new()
            self.@button2 = System.Windows.Forms.Button.new()
            self.SuspendLayout()
            # 
            # button1
            # 
            self.@button1.Dock = System.Windows.Forms.DockStyle.Fill
            self.@button1.Font = System.Drawing.Font.new("Microsoft Sans Serif", 50f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((0)))
            self.@button1.Location = System.Drawing.Point.new(0, 0)
            self.@button1.Name = "button1"
            self.@button1.Size = System.Drawing.Size.new(358, 257)
            self.@button1.TabIndex = 0
            self.@button1.Text = "Highlight"
            self.@button1.UseVisualStyleBackColor = true
            self.@button1.Click { |sender, e| self.@button1_Click(sender, e) }
            # 
            # button2
            # 
            self.@button2.Dock = System.Windows.Forms.DockStyle.Bottom
            self.@button2.Font = System.Drawing.Font.new("Microsoft Sans Serif", 8.25f)
            self.@button2.Location = System.Drawing.Point.new(0, 257)
            self.@button2.Name = "button2"
            self.@button2.Size = System.Drawing.Size.new(358, 249)
            self.@button2.TabIndex = 1
            self.@button2.Text = "Insert Sample Text"
            self.@button2.UseVisualStyleBackColor = true
            self.@button2.Click { |sender, e| self.@button2_Click(sender, e) }
            # 
            # MainForm
            # 
            self.@ClientSize = System.Drawing.Size.new(358, 506)
            self.@Controls.Add(self.@button1)
            self.@Controls.Add(self.@button2)
            self.@Font = System.Drawing.Font.new("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((0)))
            self.@Name = "MainForm"
            self.@ShowInTaskbar = false
            self.@StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            self.@Text = "OpenCL Highlighter Plugin"
            self.@Load { |sender, e| self.@MainForm_Load(sender, e) }
            self.ResumeLayout(false)
        end

        def MainForm_Load(sender, e)
        end

        def button1_Click(sender, e)
            begin
            Main.ActiveEditor.codebox.TextChangedDelayed { |sender, e| codebox_TextChangedDelayed(sender, e) }
                MessageBox.Show("Highlighter Initialized. Just Type in Text To Highlight")
            rescue Exception => ex
                Console.WriteLine(ex.Message)
            ensure
            end
        end

        def button2_Click(sender, e)
            begin
            Main.ActiveEditor.codebox.InsertText("kernel void Mono(\n_global uchar4 *input,\nconst float2 file\n)\n\nint Main()\n{\nint coord=(int2)(get_global_id(0));\n}")
            rescue  => 
            ensure
            end
        end
    end
end