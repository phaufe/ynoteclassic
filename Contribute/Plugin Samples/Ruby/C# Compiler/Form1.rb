require "mscorlib"
require "System, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
require "System.Drawing, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
require "System.Collections, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
require "System.ComponentModel, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
require "System.Windows.Forms, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
require "System.Data, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
require "Microsoft.CSharp, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
require "System.CodeDom.Compiler, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
require "System.Reflection, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
require "System.Diagnostics, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
require "SS.Ynote.Engine.Framework.Plugins.Interface, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
require "System.Xml.Linq, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
#-------------------------------------------------
#
#Simple C# Compiler Plugin
#
#Make a Compiler Plugin For Any languge using Process Info
#
#--------------------------------------------
module CSCompiler
    class Form1 < WeifenLuo::WinFormsUI::Docking::DockContent, IFormPlugin
        def initialize() # <summary>
            # Required method for Designer support - do not modify
            # the contents of this method with the code editor.
            # </summary>
            # 
            # button1
            # 
            # 
            # label2
            # 
            # 
            # appName
            # 
            # 
            # label1
            # 
            # 
            # mainClass
            # 
            # 
            # includeDebug
            # 
            # 
            # Form1
            # 
            @configuration = XElement.new("HighlighterPluginConfig")
            self.InitializeComponent()
        end

        def Dispose(disposing)
            if disposing then
                if @components != nil then
                    @components.Dispose()
                end
            end
            self.Dispose(disposing)
        end

        def InitializeComponent()
            self.@button1 = System.Windows.Forms.Button.new()
            self.@label2 = System.Windows.Forms.Label.new()
            self.@appName = System.Windows.Forms.TextBox.new()
            self.@label1 = System.Windows.Forms.Label.new()
            self.@mainClass = System.Windows.Forms.TextBox.new()
            self.@includeDebug = System.Windows.Forms.CheckBox.new()
            self.SuspendLayout()
            self.@button1.BackColor = System.Drawing.SystemColors.Control
            self.@button1.Location = System.Drawing.Point.new(114, 179)
            self.@button1.Name = "button1"
            self.@button1.Size = System.Drawing.Size.new(160, 24)
            self.@button1.TabIndex = 1
            self.@button1.Text = "&Compile and Execute"
            self.@button1.UseVisualStyleBackColor = false
            self.@button1.Click { |sender, e| self.@button1_Click(sender, e) }
            self.@label2.Location = System.Drawing.Point.new(31, 65)
            self.@label2.Name = "label2"
            self.@label2.Size = System.Drawing.Size.new(104, 23)
            self.@label2.TabIndex = 5
            self.@label2.Text = "Main Class Name"
            self.@appName.Location = System.Drawing.Point.new(137, 26)
            self.@appName.Name = "appName"
            self.@appName.Size = System.Drawing.Size.new(152, 20)
            self.@appName.TabIndex = 2
            self.@appName.Text = "Application.exe"
            self.@label1.Location = System.Drawing.Point.new(31, 29)
            self.@label1.Name = "label1"
            self.@label1.Size = System.Drawing.Size.new(100, 23)
            self.@label1.TabIndex = 4
            self.@label1.Text = "OutputFileName"
            self.@mainClass.Location = System.Drawing.Point.new(137, 68)
            self.@mainClass.Name = "mainClass"
            self.@mainClass.Size = System.Drawing.Size.new(152, 20)
            self.@mainClass.TabIndex = 3
            self.@mainClass.Text = "SS.Ynote.Classic.Main"
            self.@includeDebug.Location = System.Drawing.Point.new(34, 111)
            self.@includeDebug.Name = "includeDebug"
            self.@includeDebug.Size = System.Drawing.Size.new(160, 24)
            self.@includeDebug.TabIndex = 7
            self.@includeDebug.Text = "Include Debug Info"
            self.@AutoScaleBaseSize = System.Drawing.Size.new(5, 13)
            self.@ClientSize = System.Drawing.Size.new(316, 233)
            self.@Controls.Add(self.@includeDebug)
            self.@Controls.Add(self.@label2)
            self.@Controls.Add(self.@label1)
            self.@Controls.Add(self.@mainClass)
            self.@Controls.Add(self.@appName)
            self.@Controls.Add(self.@button1)
            self.@Font = System.Drawing.Font.new("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((0)))
            self.@FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
            self.@Name = "Form1"
            self.@StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            self.@Text = "C# Compiler Plugin"
            self.ResumeLayout(false)
            self.PerformLayout()
        end

        def Title
            return "C# Compiler Plugin"
        end

        def Description
            return "Sample Compiler Plugin"
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
            #eg : C:\\PluginIcon.ico
            return System::String.Empty
        end

        def Content
            return self
        end

        def ShowAs
            return self.ShowAs.Dialog
        end

        def menuItem4_Click(sender, e)
            self.Dispose()
            Application.Exit()
        end

        def menuItem3_Click(sender, e)
            System.Windows.Forms.MessageBox.Show(self, "CSharp sample compiler :)", "CodeProject Rulez")
        end

        def button1_Click(sender, e)
            codeProvider = CSharpCodeProvider.new()
            # For Visual Basic Compiler 
            #Microsoft.VisualBasic.VBCodeProvider
            compiler = codeProvider.CreateCompiler()
            parameters = CompilerParameters.new()
            parameters.GenerateExecutable = true
            if @appName.Text == "" then
                System.Windows.Forms.MessageBox.Show(self, "Application name cannot be empty")
                return 
            end
            parameters.OutputAssembly = @appName.Text.ToString()
            if @mainClass.Text.ToString() == "" then
                System.Windows.Forms.MessageBox.Show(self, "Main Class Name cannot be empty")
                return 
            end
            parameters.MainClass = @mainClass.Text.ToString()
            parameters.IncludeDebugInformation = @includeDebug.Checked
            enumerator = AppDomain.CurrentDomain.GetAssemblies().GetEnumerator()
            while enumerator.MoveNext()
                asm = enumerator.Current
                parameters.ReferencedAssemblies.Add(asm.Location)
            end
            code = SS.Ynote.Classic.Main.ActiveEditor.codebox.Text
            results = compiler.CompileAssemblyFromSource(parameters, code)
            if results.Errors.Count > 0 then
                errors = "Compilation failed:\n"
                enumerator = results.Errors.GetEnumerator()
                while enumerator.MoveNext()
                    err = enumerator.Current
                    errors += err.ToString() + "\n"
                end
                System.Windows.Forms.MessageBox.Show(self, errors, "There were compilation errors")
            else # try to execute application
                begin
                    if not System.IO.File.Exists(@appName.Text.ToString()) then
                        MessageBox.Show(String.Format("Can't find {0}", @appName), "Can't execute.", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        return 
                    end
                    pInfo = ProcessStartInfo.new(@appName.Text.ToString())
                    Process.Start(pInfo)
                rescue Exception => ex
                    MessageBox.Show(String.Format("Error while executing {0}", @appName) + ex.ToString(), "Can't execute.", MessageBoxButtons.OK, MessageBoxIcon.Error)
                ensure
                end
            end
        end
    end
end