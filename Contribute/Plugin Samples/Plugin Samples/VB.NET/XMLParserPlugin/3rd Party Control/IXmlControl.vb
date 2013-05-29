Imports System.Drawing.Printing

Namespace XMLTreeView
	''' <summary>
	''' Summary description for IXmlControl.
	''' </summary>
	Friend Interface IXmlControl
		#Region "Content"
		Property Xml() As String
		#End Region

		#Region "Searching"
		Sub Search(sender As Object, e As EventArgs)
		Sub StartSearch(criterion As String, caseSensitive As Boolean)
		Sub [Next]()
		#End Region

		#Region "Printing"
		Sub Print(sender As Object, e As EventArgs)
		Sub PrintPage(sender As Object, e As PrintPageEventArgs)
		ReadOnly Property PrintDocument() As PrintDocument
		#End Region

		#Region "Editing, Deleting, Copying, Pasting"
		Sub Edit(sender As Object, e As System.EventArgs)
		Sub Delete(sender As Object, e As System.EventArgs)
		Sub Copy(sender As Object, e As System.EventArgs)
		Sub Paste(sender As Object, e As System.EventArgs)
		#End Region

		#Region "Saving, Opening"
		Sub Save(sender As Object, e As System.EventArgs)
		#End Region
	End Interface
End Namespace
