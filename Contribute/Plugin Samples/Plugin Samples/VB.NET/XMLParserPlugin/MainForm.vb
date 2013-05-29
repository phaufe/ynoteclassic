Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Linq
Imports System.Text
Imports System.Windows.Forms
Imports SS.Ynote.Engine.Framework.Plugins.Interface
Imports WeifenLuo.WinFormsUI.Docking
Imports System.Xml.Linq

Public Partial Class MainForm
	Inherits DockContent
	Implements IFormPlugin
	Public Sub New()
		InitializeComponent()
		Me.ShowHint = DockState.DockRight
	End Sub
	#Region "IPluginMembers"

	'IFormPlugin Inherits the IPlugin Interface

	Public ReadOnly Property Title() As String Implements IPlugin.Title
		Get
			Return "XMLParserPlugin"
		End Get
	End Property
	Public ReadOnly Property Description() As String Implements IPlugin.Description
		Get
			Return "Sample XML Parser Plugin"
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

	Private m_configuration As New XElement("XMLParserPluginConfig")
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




	Private Sub toolStripButton1_Click(sender As Object, e As EventArgs)
		XmlTree.Xml = SS.Ynote.Classic.Main.ActiveEditor.codebox.Text
	End Sub
End Class
