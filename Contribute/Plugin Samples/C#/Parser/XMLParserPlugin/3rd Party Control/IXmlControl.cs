using System;
using System.Drawing.Printing;

namespace XMLTreeView
{
	/// <summary>
	/// Summary description for IXmlControl.
	/// </summary>
	internal interface IXmlControl
	{
		#region Content
		string Xml { get; set; }
		#endregion // Content

		#region Searching
		void Search( object sender, EventArgs e );
		void StartSearch( string criterion, bool caseSensitive );
		void Next();
		#endregion // Searching

		#region Printing
		void Print( object sender, EventArgs e );
		void PrintPage( object sender, PrintPageEventArgs e );
		PrintDocument PrintDocument { get; }
		#endregion // Printing

		#region Editing, Deleting, Copying, Pasting
		void Edit(object sender, System.EventArgs e);
		void Delete(object sender, System.EventArgs e);
		void Copy(object sender, System.EventArgs e);
		void Paste(object sender, System.EventArgs e);
		#endregion // Edititing, Deleting, Copying, Pasting

		#region Saving, Opening
		void Save(object sender, System.EventArgs e);
		#endregion // Saving, Opening
	}
}
