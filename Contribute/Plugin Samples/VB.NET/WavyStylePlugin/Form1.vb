Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Linq
Imports System.Text
Imports System.Windows.Forms
Imports SS.Ynote.Engine.Framework.Plugins.Interface
Imports System.Xml.Linq
Imports FastColoredTextBoxNS
Imports SS.Ynote.Classic

'========================================
'
'Wavy Style can be use in error checking
'you can make a plugin for error checking for any language
'
'This is just a sample
'
'========================================

''' <summary>
''' Simple Spell Checker
''' </summary>
Public Partial Class Form1
	Inherits WeifenLuo.WinFormsUI.Docking.DockContent
	Implements IFormPlugin
	#Region "Constants"

	Private RedWavy As New WavyLineStyle(100, Color.Red)

	#End Region

	#Region "Constructor"
	Public Sub New()
		InitializeComponent()
		Hide()
		Me.Visible = False
		AddHandler Main.ActiveFastColoredTextBox.TextChangedDelayed, New EventHandler(Of TextChangedEventArgs)(AddressOf ActiveFastColoredTextBox_TextChangedDelayed)
		MessageBox.Show("Spell Checker Initialized!")
	End Sub
	#End Region

	#Region "IPlugin Members"

	Public ReadOnly Property Title() As String Implements IPlugin.Title
		Get
			Return "WavyStylePlugin"
		End Get
	End Property
	Public ReadOnly Property Description() As String Implements IPlugin.Description
		Get
			Return "Wavy Style Sample Plugin"
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

	Private m_configuration As New XElement("WavyStylePluginConfig")
	Public Property Configuration() As XElement Implements IPlugin.Configuration
		Get
			Return m_configuration
		End Get
		Set
			m_configuration = value
		End Set
	End Property

	Public ReadOnly Property Icon() As String Implements IPlugin.Icon
		'get { return "C:\\Icons\\youricon.ico"; }
		Get
			Return String.Empty
		End Get
	End Property

	#End Region

	#Region "IFormPluginMembers"

	Public ReadOnly Property Content() As WeifenLuo.WinFormsUI.Docking.DockContent Implements IFormPlugin.Content
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

    Private Sub ActiveFastColoredTextBox_TextChangedDelayed(ByVal sender As Object, ByVal e As TextChangedEventArgs)
        Dim SpellChecker As New SpellChecker(Main.ActiveEditor.codebox, "Dictionary.dic")
        SpellChecker.SpellCheck(sender)
    End Sub
End Class
