Imports System.IO
Imports System.Drawing
Imports FastColoredTextBoxNS
Imports System.Collections.Generic

Public Class SpellChecker
	Private wavyStyle As Style = New WavyLineStyle(255, Color.Red)
	Private words As HashSet(Of String)
	Private tb As FastColoredTextBox
	Private Dictionary As String
	Public Sub New(fctb As FastColoredTextBox, dictionarypath__1 As String)
		Dim List = File.ReadAllLines(dictionarypath__1)
		DictionaryPath = dictionarypath__1
		words = New HashSet(Of String)(List, System.StringComparer.InvariantCultureIgnoreCase)
		Me.tb = fctb
		fctb = Me.tb
		AddHandler Me.tb.TextChangedDelayed, New System.EventHandler(Of TextChangedEventArgs)(AddressOf tb_TextChangedDelayed)
	End Sub
	Public Property DictionaryPath() As String
		Get
			Return Dictionary
		End Get
		Set
			Dictionary = value
		End Set
	End Property
	Public Sub SpellCheck(sender As Object)
		tb_TextChangedDelayed(sender, New TextChangedEventArgs(Me.tb.Range))
	End Sub
	Private Sub tb_TextChangedDelayed(sender As Object, e As TextChangedEventArgs)
		System.Threading.ThreadPool.QueueUserWorkItem(Function(s) 
		Dim list = New List(Of FastColoredTextBoxNS.Range)()
		For Each word As var In e.ChangedRange.GetRanges("[\w']+")
			If Not words.Contains(word.Text) Then
				list.Add(word)
			End If
		Next
		'
		e.ChangedRange.ClearStyle(wavyStyle)
		For Each word As var In list
			word.SetStyle(wavyStyle)
		Next

End Function)
	End Sub
	Public Sub Clear()
		Me.tb.Range.ClearStyle(wavyStyle)
	End Sub
End Class
