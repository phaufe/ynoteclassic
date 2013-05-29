'CL Syntax Highlighter Plugin

Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports WeifenLuo.WinFormsUI.Docking
Imports SS.Ynote.Engine.Framework.Plugins.Interface
Imports System.Xml.Linq
Imports FastColoredTextBoxNS
Imports SS.Ynote.Classic
Imports System.Windows.Forms

Public Class MainForm
	Inherits DockContent
	Implements IFormPlugin
	#Region "Constructor"

	Public Sub New()
		InitializeComponent()
		Me.ShowHint = DockState.DockLeft
	End Sub
	#End Region

	Private button1 As Button
	Private button2 As Button

	#Region "IPluginMembers"

	'IFormPlugin Inherits the IPlugin Interface

	Public ReadOnly Property Title() As String Implements IPlugin.Title
		Get
			Return "HighlighterPlugin"
		End Get
	End Property
	Public ReadOnly Property Description() As String Implements IPlugin.Description
		Get
			Return "Sample Highlighter Plugin"
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
		Get
			Return String.Empty
		End Get
	End Property

	#End Region

	#Region "IFormPlugin Members"

	Public ReadOnly Property Content() As DockContent Implements IFormPlugin.Content
		Get
			Return Me
		End Get
	End Property
	Public ReadOnly Property ShowAs() As ShowAs Implements IFormPlugin.ShowAs
		Get
			Return ShowAs.Normal
		End Get
	End Property
	#End Region

	Private Sub ActiveFastColoredTextBox_TextChangedDelayed(sender As Object, e As TextChangedEventArgs)
		Dim [Custom] As New List(Of ExplorerItem)()
		HighlightingPluginSample.CLSyntaxHighlighter.Highlight(e.ChangedRange, [Custom])
	End Sub

	Private Sub InitializeComponent()
		Me.button1 = New System.Windows.Forms.Button()
		Me.button2 = New System.Windows.Forms.Button()
		Me.SuspendLayout()
		' 
		' button1
		' 
		Me.button1.Dock = System.Windows.Forms.DockStyle.Fill
		Me.button1.Font = New System.Drawing.Font("Microsoft Sans Serif", 50F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(0))
		Me.button1.Location = New System.Drawing.Point(0, 0)
		Me.button1.Name = "button1"
		Me.button1.Size = New System.Drawing.Size(358, 257)
		Me.button1.TabIndex = 0
		Me.button1.Text = "Highlight"
		Me.button1.UseVisualStyleBackColor = True
		AddHandler Me.button1.Click, New System.EventHandler(AddressOf Me.button1_Click)
		' 
		' button2
		' 
		Me.button2.Dock = System.Windows.Forms.DockStyle.Bottom
		Me.button2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25F)
		Me.button2.Location = New System.Drawing.Point(0, 257)
		Me.button2.Name = "button2"
		Me.button2.Size = New System.Drawing.Size(358, 249)
		Me.button2.TabIndex = 1
		Me.button2.Text = "Insert Sample Text"
		Me.button2.UseVisualStyleBackColor = True
		AddHandler Me.button2.Click, New System.EventHandler(AddressOf Me.button2_Click)
		' 
		' MainForm
		' 
		Me.ClientSize = New System.Drawing.Size(358, 506)
		Me.Controls.Add(Me.button1)
		Me.Controls.Add(Me.button2)
		Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CByte(0))
		Me.Name = "MainForm"
		Me.ShowInTaskbar = False
		Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
		Me.Text = "OpenCL Highlighter Plugin"
		AddHandler Me.Load, New System.EventHandler(AddressOf Me.MainForm_Load)
		Me.ResumeLayout(False)

	End Sub

	Private Sub MainForm_Load(sender As Object, e As EventArgs)

	End Sub

	Private Sub button1_Click(sender As Object, e As EventArgs)
		Try
			AddHandler Main.ActiveFastColoredTextBox.TextChangedDelayed, New EventHandler(Of TextChangedEventArgs)(AddressOf ActiveFastColoredTextBox_TextChangedDelayed)
			MessageBox.Show("Highlighter Initialized. Just Type in Text To Highlight")
		Catch ex As Exception
			Console.WriteLine(ex.Message)
		End Try
	End Sub

	Private Sub button2_Click(sender As Object, e As EventArgs)
		Try
			Main.ActiveFastColoredTextBox.InsertText("kernel void Mono(" & vbLf & "_global uchar4 *input," & vbLf & "const float2 file" & vbLf & ")" & vbLf & vbLf & "int Main()" & vbLf & "{" & vbLf & "int coord=(int2)(get_global_id(0));" & vbLf & "}")
		Catch
		End Try
	End Sub
End Class
