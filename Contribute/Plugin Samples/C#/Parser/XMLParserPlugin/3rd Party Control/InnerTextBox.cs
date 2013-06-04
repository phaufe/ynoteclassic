using System;
using System.IO;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing.Printing;
using SS.Ynote.Engine.Framework;

namespace XMLTreeView
{
	/// <summary>
	/// Summary description for InnerTextBox.
	/// </summary>
	internal class InnerTextBox :TextBox, IXmlControl
	{
		#region Fields
		private XmlTreeView parent = null;
		private string draggedFile = string.Empty;
		private PrintDocument printDocument1 = new PrintDocument();
		private int startPos;
		private int xPos;
		private Font printFont = new Font( "Arial", 10 );
		private bool caseSensitive;
		private string criterion;
		private int printableWidth;
		private float fontHeight;
		private float linesPerPage;
		private bool closingHandlerAssigned;
		#endregion // Fields

		#region Constructors
		internal InnerTextBox(XmlTreeView container)
		{
			parent = container;
			ReadOnly = true;
			Multiline = true;
			Dock = DockStyle.Fill;
            this.BackColor = Color.White;
		}
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InnerTextBox));
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // InnerTextBox
            // 
            resources.ApplyResources(this, "$this");
            this.BackColor = Color.White;
            this.Name = "InnerTextBox";
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }
		#endregion // Constructors

		#region Implementation IXmlControl
		public void Search( object sender, EventArgs e )
		{
			if ( Text.Length > 0 )
			{
				new SearchDlg( this ).Show();
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="criterion"></param>
		/// <param name="caseSensitive"></param>
		public void StartSearch( string criterion, bool caseSensitive )
		{
			startPos = 0;
			this.caseSensitive = caseSensitive;
			this.criterion = caseSensitive ? criterion : criterion.ToUpper();
		}

		/// <summary>
		/// 
		/// </summary>
		public void Next()
		{
			int foundPos = 0;
			SelectionLength = 0;
			string internalText = caseSensitive ? Text : Text.ToUpper();

			if ( internalText.Length > 0 )
			{
				if ( ( foundPos = internalText.IndexOf( criterion, startPos ) ) > -1 )
				{
					SelectionStart = foundPos;
					SelectionLength = criterion.Length;
					startPos = foundPos + criterion.Length;
					Focus();
				}
				else
				{
					MessageBox.Show( "End of text reached !", XmlTreeView.MessageBoxTitle );
					startPos = 0;
				}
			}
		}

		public void Print( object sender, EventArgs e )
		{
			if ( Text.Length > 0 && parent.PrintDialog.ShowDialog() == DialogResult.OK )
			{
				printDocument1.Print();
			}
		}
		
		public void PrintPage( object sender, PrintPageEventArgs e )
		{
			int linesPrinted = 0;
			float yPos = 0;
			bool endOfPageReached = false;
			string textToDraw = string.Empty;

			if (fontHeight == 0.0)
			{	// nothing initialized yet
				fontHeight = printFont.GetHeight( e.Graphics );
				linesPerPage = e.MarginBounds.Height / fontHeight;
				printableWidth = Text.Length / ((int) (e.Graphics.MeasureString( Text, printFont ).Width / e.MarginBounds.Width));
			}

			do 
			{
				if ( xPos < Text.Length - 1 )
				{
					textToDraw = xPos + printableWidth < Text.Length - 1 ? Text.Substring( xPos, printableWidth ) : Text.Substring( xPos );
					e.Graphics.DrawString( textToDraw, printFont, Brushes.Black, e.MarginBounds.Left, yPos, new StringFormat() );
					yPos += fontHeight;
					xPos += printableWidth;
					++linesPrinted;
				}

				if ( xPos >= Text.Length || linesPrinted >= linesPerPage )
				{
					endOfPageReached = true;
				}
			} while ( textToDraw.Length > 0 && !endOfPageReached );

			e.HasMorePages = ( xPos < Text.Length - 1 );
		}

		public PrintDocument PrintDocument
		{
			get
			{
				return printDocument1;
			}
		}

		public void Edit(object sender, System.EventArgs e)
		{
			if ( !closingHandlerAssigned )
			{
				Form parentForm = FindForm();
				parentForm.Closing += new CancelEventHandler( parent.form_Closing );
				closingHandlerAssigned = true;
			}

			ReadOnly = false;
		}

		public void Delete(object sender, System.EventArgs e)
		{
			Text = Text.Remove( SelectionStart, SelectionLength );
		}

		public void Copy(object sender, System.EventArgs e)
		{
			Copy();
		}

		public void Paste(object sender, System.EventArgs e)
		{
			IDataObject dataObject = Clipboard.GetDataObject();

			if ( dataObject.GetDataPresent( DataFormats.Text ) )
			{
				parent.Xml = (string) dataObject.GetData( DataFormats.Text );
			}
		}

		public void Save(object sender, System.EventArgs e)
		{
			if ( Text.Length > 0 && parent.SaveDialog.ShowDialog() == DialogResult.OK )
			{
				using ( StreamWriter sw = new StreamWriter( parent.SaveDialog.FileName ) )
				{
					sw.Write( Xml );
					parent.fileXml = Xml;
					parent.filePath = parent.SaveDialog.FileName;
				}
			}
		}

		/// <summary>
		/// 
		/// </summary>
		public string Xml
		{
			get
			{
				return Text;
			}
			set
			{
				Text = value;
			}
		}
		#endregion // IXmlControl

		#region Event handlers for InnerTextBox
		protected override void OnKeyUp( KeyEventArgs e )
		{
			base.OnKeyUp( e );

			if ( e.Control && e.KeyCode == Keys.C )
			{
				Copy( null, null );
			}	
			else if ( e.Control && e.KeyCode == Keys.V )
			{
				Paste( null, null );
			}
			else if ( e.Control && e.KeyCode == Keys.F )
			{
				Search( null, null );
			}
			else if ( e.Control && e.KeyCode == Keys.P )
			{
				Print( null, null );
			}
			else if ( e.Control && e.KeyCode == Keys.S )
			{
				Save( null, null );
			}
			else if ( e.KeyCode == Keys.Delete )
			{
				Delete( null, null );
			}
			else if ( e.KeyCode == Keys.Insert )
			{
				Edit( null, null );
			}
		}

		protected override void OnDragEnter( DragEventArgs e )
		{
			base.OnDragEnter( e );

			if ( e.Data.GetDataPresent( DataFormats.FileDrop ) )
			{
				e.Effect = DragDropEffects.Copy;
				draggedFile = (string) ( (object[]) e.Data.GetData( DataFormats.FileDrop ) )[0];
			}
		}

		protected override void OnDragDrop( DragEventArgs e )
		{
			base.OnDragDrop( e );

			if ( draggedFile.Length > 0 )
			{
				using ( StreamReader fs = new StreamReader( draggedFile ) )
				{
					parent.fileXml = fs.ReadToEnd();
					parent.Xml = parent.fileXml;
					parent.filePath = draggedFile;
				}

				draggedFile = string.Empty;
			}
		}
		#endregion // InnerTextBox

	}
}
