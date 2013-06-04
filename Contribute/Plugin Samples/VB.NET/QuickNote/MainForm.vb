Imports System.IO
Imports System.ComponentModel
Imports System.Text
Imports SS.Ynote.Classic
Imports SS.Ynote.Engine.Framework.Plugins
Imports SS.Ynote.Engine.Framework.Plugins.Controls
Imports SS.Ynote.Engine.Framework.Plugins.Interface
Imports WeifenLuo.WinFormsUI.Docking

Namespace QuickNotePlugin
    Partial Public Class MainForm
        Inherits DockContent
        Implements IFormPlugin

        #Region "Constructor"

        Public Sub New()
            InitializeComponent()
            AddHandler newToolStripMenuItem1.Click, AddressOf menuItem2_Click
            AddHandler openToolStripMenuItem1.Click, AddressOf menuItem3_Click
            AddHandler importFromMainEditorToolStripMenuItem.Click, AddressOf menuItem18_Click
            AddHandler saveAsToolStripMenuItem1.Click, AddressOf menuItem5_Click
            AddHandler clearAllToolStripMenuItem.Click, AddressOf menuItem2_Click
            AddHandler cutToolStripMenuItem.Click, AddressOf menuItem8_Click
            AddHandler copyToolStripMenuItem.Click, AddressOf menuItem9_Click
            AddHandler pasteToolStripMenuItem.Click, AddressOf menuItem10_Click
            AddHandler selectAllToolStripMenuItem.Click, AddressOf menuItem11_Click
            AddHandler undoToolStripMenuItem.Click, AddressOf menuItem7_Click
            AddHandler fontToolStripMenuItem.Click, AddressOf menuItem14_Click
            AddHandler backColorToolStripMenuItem.Click, AddressOf menuItem15_Click
        End Sub

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

        #Region "IPlugin Members"

        Public ReadOnly Property Title() As String Implements SS.Ynote.Engine.Framework.Plugins.Interface.IPlugin.Title
            Get
                Return "QuickNotePlugin"
            End Get
        End Property
        Public ReadOnly Property Description() As String Implements SS.Ynote.Engine.Framework.Plugins.Interface.IPlugin.Description
            Get
                Return "This is  a Quick Note Plugin for Ynote Classic"
            End Get
        End Property
        Public ReadOnly Property Group() As String Implements SS.Ynote.Engine.Framework.Plugins.Interface.IPlugin.Group
            Get
                Return "Plugins"
            End Get
        End Property
        Public ReadOnly Property SubGroup() As String Implements SS.Ynote.Engine.Framework.Plugins.Interface.IPlugin.SubGroup
            Get
                Return "Note"
            End Get
        End Property

        Private configuration_Renamed As New XElement("QuickNote")
        Public Property Configuration() As XElement Implements SS.Ynote.Engine.Framework.Plugins.Interface.IPlugin.Configuration
            Get
                Return configuration_Renamed
            End Get
            Set(ByVal value As XElement)
                configuration_Renamed = value
            End Set
        End Property

        Public ReadOnly Overloads Property Icon() As String Implements SS.Ynote.Engine.Framework.Plugins.Interface.IPlugin.Icon
            'get { return "C:\\Icons\\Folder.ico"; }
            Get
                Return String.Empty
            End Get
        End Property
        #End Region

        #Region "Events"

        Private Sub menuItem7_Click(ByVal sender As Object, ByVal e As EventArgs)
            Me.txtmain.Undo()
        End Sub

        Private Sub menuItem18_Click(ByVal sender As Object, ByVal e As EventArgs)
            Me.txtmain.Text = Main.ActiveEditor.codebox.Text
        End Sub

        Private Sub menuItem2_Click(ByVal sender As Object, ByVal e As EventArgs)
            Me.txtmain.Clear()
        End Sub

        Private Sub menuItem3_Click(ByVal sender As Object, ByVal e As EventArgs)
            Dim o As New System.Windows.Forms.OpenFileDialog()
            o.Filter = "Text Files (*.txt)|*.txt|All Files(*.*)|*.*"
            o.ShowDialog()
            txtmain.Text = File.ReadAllText(o.FileName, Encoding.Default)

        End Sub

        Private Sub menuItem8_Click(ByVal sender As Object, ByVal e As EventArgs)
            Me.txtmain.Cut()
        End Sub

        Private Sub menuItem9_Click(ByVal sender As Object, ByVal e As EventArgs)
            Me.txtmain.Copy()
        End Sub

        Private Sub menuItem10_Click(ByVal sender As Object, ByVal e As EventArgs)
            Me.txtmain.Paste()
        End Sub

        Private Sub menuItem11_Click(ByVal sender As Object, ByVal e As EventArgs)
            Me.txtmain.SelectAll()
        End Sub
        Private Sub menuItem14_Click(ByVal sender As Object, ByVal e As EventArgs)
            Dim f As New System.Windows.Forms.FontDialog()
            f.ShowEffects = True
            f.ShowColor = True
            f.ShowDialog()
            txtmain.Font = f.Font
            txtmain.ForeColor = f.Color

        End Sub

        Private Sub menuItem15_Click(ByVal sender As Object, ByVal e As EventArgs)
            Dim c As New System.Windows.Forms.ColorDialog()
            c.ShowDialog()
            Me.txtmain.BackColor = c.Color
        End Sub

        Private Sub menuItem5_Click(ByVal sender As Object, ByVal e As EventArgs)
            Dim s As New System.Windows.Forms.SaveFileDialog()
            s.Filter = "All Files (*.*)|*.*"
            s.ShowDialog()
            If s.FileName <> "" Then
                File.WriteAllText(s.FileName, txtmain.Text, Encoding.Default)
            End If
        End Sub

        Private Sub menuItem19_Click(ByVal sender As Object, ByVal e As EventArgs)
            Close()
        End Sub

        Private Sub exitToolStripMenuItem1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles exitToolStripMenuItem1.Click
            Application.Exit()
        End Sub
        #End Region
    End Class
End Namespace
