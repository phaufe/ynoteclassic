


Imports System.Drawing
Imports System.Collections
Imports System.ComponentModel
Imports System.Windows.Forms
Imports System.Data
Imports Microsoft.CSharp
Imports System.CodeDom.Compiler
Imports System.Reflection
Imports System.Diagnostics
Imports SS.Ynote.Engine.Framework.Plugins.Interface
Imports System.Xml.Linq

'-------------------------------------------------
'
'Simple C# Compiler Plugin
'
'Make a Compiler Plugin For Any languge using Process Info
'
'--------------------------------------------
Public Class Form1
    Inherits WeifenLuo.WinFormsUI.Docking.DockContent
    Implements IFormPlugin
    #Region "Variables"

    Private button1 As System.Windows.Forms.Button
    Private label1 As System.Windows.Forms.Label
    Private label2 As System.Windows.Forms.Label
    Private appName As System.Windows.Forms.TextBox
    Private mainClass As System.Windows.Forms.TextBox
    Private includeDebug As System.Windows.Forms.CheckBox
    Private components As IContainer
    #End Region

    #Region "Constructor"
    Public Sub New()


        InitializeComponent()
    End Sub

    Protected Overrides Sub Dispose(disposing As Boolean)
        If disposing Then
            If components IsNot Nothing Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub
    #End Region

    #Region "Windows Form Designer generated code"
    ''' <summary>
    ''' Required method for Designer support - do not modify
    ''' the contents of this method with the code editor.
    ''' </summary>
    Private Sub InitializeComponent()
        Me.button1 = New System.Windows.Forms.Button()
        Me.label2 = New System.Windows.Forms.Label()
        Me.appName = New System.Windows.Forms.TextBox()
        Me.label1 = New System.Windows.Forms.Label()
        Me.mainClass = New System.Windows.Forms.TextBox()
        Me.includeDebug = New System.Windows.Forms.CheckBox()
        Me.SuspendLayout()
        ' 
        ' button1
        ' 
        Me.button1.BackColor = System.Drawing.SystemColors.Control
        Me.button1.Location = New System.Drawing.Point(114, 179)
        Me.button1.Name = "button1"
        Me.button1.Size = New System.Drawing.Size(160, 24)
        Me.button1.TabIndex = 1
        Me.button1.Text = "&Compile and Execute"
        Me.button1.UseVisualStyleBackColor = False
        AddHandler Me.button1.Click, New System.EventHandler(AddressOf Me.button1_Click)
        ' 
        ' label2
        ' 
        Me.label2.Location = New System.Drawing.Point(31, 65)
        Me.label2.Name = "label2"
        Me.label2.Size = New System.Drawing.Size(104, 23)
        Me.label2.TabIndex = 5
        Me.label2.Text = "Main Class Name"
        ' 
        ' appName
        ' 
        Me.appName.Location = New System.Drawing.Point(137, 26)
        Me.appName.Name = "appName"
        Me.appName.Size = New System.Drawing.Size(152, 20)
        Me.appName.TabIndex = 2
        Me.appName.Text = "Application.exe"
        ' 
        ' label1
        ' 
        Me.label1.Location = New System.Drawing.Point(31, 29)
        Me.label1.Name = "label1"
        Me.label1.Size = New System.Drawing.Size(100, 23)
        Me.label1.TabIndex = 4
        Me.label1.Text = "OutputFileName"
        ' 
        ' mainClass
        ' 
        Me.mainClass.Location = New System.Drawing.Point(137, 68)
        Me.mainClass.Name = "mainClass"
        Me.mainClass.Size = New System.Drawing.Size(152, 20)
        Me.mainClass.TabIndex = 3
        Me.mainClass.Text = "SS.Ynote.Classic.Main"
        ' 
        ' includeDebug
        ' 
        Me.includeDebug.Location = New System.Drawing.Point(34, 111)
        Me.includeDebug.Name = "includeDebug"
        Me.includeDebug.Size = New System.Drawing.Size(160, 24)
        Me.includeDebug.TabIndex = 7
        Me.includeDebug.Text = "Include Debug Info"
        ' 
        ' Form1
        ' 
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(316, 233)
        Me.Controls.Add(Me.includeDebug)
        Me.Controls.Add(Me.label2)
        Me.Controls.Add(Me.label1)
        Me.Controls.Add(Me.mainClass)
        Me.Controls.Add(Me.appName)
        Me.Controls.Add(Me.button1)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(0))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "Form1"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "C# Compiler Plugin"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    #End Region

    #Region "Plugin Members"
    Public ReadOnly Property Title() As String Implements IPlugin.Title
        Get
            Return "C# Compiler Plugin"
        End Get
    End Property
    Public ReadOnly Property Description() As String Implements IPlugin.Description
        Get
            Return "Sample Compiler Plugin"
        End Get
    End Property
    Public ReadOnly Property Group() As String Implements IPlugin.Group
        Get
            Return "Plugins"
        End Get
    End Property
    Public ReadOnly Property SubGroup() As String Implements IPlugin.SubGroup
        Get
            Return "Samples"
        End Get
    End Property

    Private m_configuration As New XElement("HighlighterPluginConfig")
    Public Property Configuration() As XElement Implements IPlugin.Configuration
        Get
            Return m_configuration
        End Get
        Set
            m_configuration = value
        End Set
    End Property

    Public ReadOnly Property Icon() As String Implements IPlugin.Icon
        'eg : C:\\PluginIcon.ico
        Get
            Return String.Empty
        End Get
    End Property
    #Region "IFormPlugin Members"

    Public ReadOnly Property Content() As WeifenLuo.WinFormsUI.Docking.DockContent Implements IFormPlugin.Content
        Get
            Return Me
        End Get
    End Property
    Public ReadOnly Property ShowAs() As ShowAs Implements IFormPlugin.ShowAs
        Get
            Return ShowAs.Dialog
        End Get
    End Property
    #End Region

    #End Region

    Private Sub menuItem4_Click(sender As Object, e As System.EventArgs)
        Dispose()
        Application.[Exit]()
    End Sub

    Private Sub menuItem3_Click(sender As Object, e As System.EventArgs)
        System.Windows.Forms.MessageBox.Show(Me, "CSharp sample compiler :)", "CodeProject Rulez")
    End Sub

    Private Sub button1_Click(sender As Object, e As System.EventArgs)
        Dim codeProvider As New CSharpCodeProvider()

        ' For Visual Basic Compiler 
        'Microsoft.VisualBasic.VBCodeProvider

        Dim compiler As ICodeCompiler = codeProvider.CreateCompiler()
        Dim parameters As New CompilerParameters()

        parameters.GenerateExecutable = True
        If appName.Text = "" Then
            System.Windows.Forms.MessageBox.Show(Me, "Application name cannot be empty")
            Return
        End If

        parameters.OutputAssembly = appName.Text.ToString()

        If mainClass.Text.ToString() = "" Then
            System.Windows.Forms.MessageBox.Show(Me, "Main Class Name cannot be empty")
            Return
        End If

        parameters.MainClass = mainClass.Text.ToString()

        parameters.IncludeDebugInformation = includeDebug.Checked

        For Each asm As Assembly In AppDomain.CurrentDomain.GetAssemblies()
            parameters.ReferencedAssemblies.Add(asm.Location)
        Next

        Dim code As String = SS.Ynote.Classic.Main.ActiveEditor.codebox.Text

        Dim results As CompilerResults = compiler.CompileAssemblyFromSource(parameters, code)

        If results.Errors.Count > 0 Then
            Dim errors As String = "Compilation failed:" & vbLf
            For Each err As CompilerError In results.Errors
                errors += err.ToString() & vbLf
            Next
            System.Windows.Forms.MessageBox.Show(Me, errors, "There were compilation errors")
        Else
            '#Region "Executing generated executable"
            ' try to execute application

            Try
                If Not System.IO.File.Exists(appName.Text.ToString()) Then
                    MessageBox.Show([String].Format("Can't find {0}", appName), "Can't execute.", MessageBoxButtons.OK, MessageBoxIcon.[Error])
                    Return
                End If
                Dim pInfo As New ProcessStartInfo(appName.Text.ToString())
                Process.Start(pInfo)
            Catch ex As Exception
                MessageBox.Show([String].Format("Error while executing {0}", appName) & ex.ToString(), "Can't execute.", MessageBoxButtons.OK, MessageBoxIcon.[Error])

                '#End Region

            End Try
        End If

    End Sub
End Class
