using System;
using System.IO;
using System.Xml;
using System.Drawing;
using System.Threading;
using System.Collections;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing.Printing;
using SS.Ynote.Engine.Controls;

namespace XMLTreeView
{
	internal class InnerXmlTreeView : SSTreeView, IXmlControl
	{
		#region Fields
		private XmlDocument document = null;
		private XmlTreeView parent = null;
		private string draggedFile = string.Empty;
		private static TreeNode CurrentNode = null;
		private ArrayList foundList = new ArrayList( 100 );
		private bool foundNode;
		private string searchText;
		private bool caseSensitive;
		private Font printFont = new Font( "Arial", 10 );
		private float linesPerPage = 0;
		private float yPos =  0;
		private int count = 0;
		private string indent = string.Empty;
		private float leftMargin = 0;
		private float topMargin = 0;
		private bool endOfPageReached = false;
		private ArrayList printedNodes = new ArrayList( 1000 );
		private PrintPageEventArgs eventArgs = null;
		private TextBox editBox;
		private XmlTreeNode editingNode;
		private PrintDocument printDocument1 = new PrintDocument();
		private bool closingHandlerAssigned;
		#endregion // Fields

		#region Constructors
		/// <summary>
		/// 
		/// </summary>
		public InnerXmlTreeView(XmlTreeView container)
		{
			parent = container;
			AllowDrop = true;
			Dock = System.Windows.Forms.DockStyle.Fill;
			ImageIndex = -1;
			SelectedImageIndex = -1;
			Size = new System.Drawing.Size(150, 130);
			TabIndex = 0;
			document = new XmlDocument();
			ShowLines = false;
		}
		#endregion // Constructors

		#region Implementation IXmlControl

		public void Search( object sender, EventArgs e )
		{
			if ( Nodes.Count > 0 )
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
			foundList.Clear();
			this.caseSensitive = caseSensitive;
			searchText = caseSensitive ? criterion : criterion.ToUpper();
		}

		/// <summary>
		/// 
		/// </summary>
		public void Next()
		{
			SelectedNode.BackColor = Color.Empty;
			foundNode = false;

			RecurseTreeNodes( Nodes );

			if ( !foundNode )
			{
				MessageBox.Show( "End of tree reached !", XmlTreeView.MessageBoxTitle );
				foundList.Clear();
			}
		}

		public void Print( object sender, EventArgs e )
		{
			if ( Nodes.Count > 0 && parent.PrintDialog.ShowDialog() == DialogResult.OK )
			{
				printedNodes.Clear();
				printDocument1.Print();
			}
		}

		public void PrintPage( object sender, PrintPageEventArgs e ) 
		{
			yPos = 0;
			count = 0;
			endOfPageReached = false;
			eventArgs = e;
			eventArgs.HasMorePages = false;
			leftMargin = eventArgs.MarginBounds.Left;
			topMargin = eventArgs.MarginBounds.Top;
			linesPerPage = eventArgs.MarginBounds.Height / printFont.GetHeight( eventArgs.Graphics );

			// Iterate over the file, printing each line.
			RecursePrintTreeNodes( Nodes );
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
			if ( Nodes.Count > 0 && SelectedNode != null && parent.LabelEdit )
			{
				editingNode = (XmlTreeNode) SelectedNode;

				if ( editingNode.ConnectedXmlElement != null )
				{
					if ( !closingHandlerAssigned )
					{
						Form parentForm = FindForm();
						parentForm.Closing += new CancelEventHandler( parent.form_Closing );
						closingHandlerAssigned = true;
					}

					int height = editingNode.Bounds.Height;
					int width =  editingNode.Bounds.Width;
					int left = editingNode.Bounds.Left;
					int top = editingNode.Bounds.Top;

					editingNode.ExpandAll();

					if ( editingNode.ConnectedXmlElement.HasChildNodes && editingNode.ConnectedXmlElement.FirstChild.NodeType != XmlNodeType.Text )
					{
						height = editingNode.NextNode.Bounds.Bottom - editingNode.Bounds.Top;
						width = Width - left;
					}

                    editBox = new TextBox();
					editBox.Multiline = true;
					editBox.BorderStyle = BorderStyle.FixedSingle;
					editBox.Leave += new EventHandler( editBox_Leave );
					editBox.KeyUp += new KeyEventHandler( editBox_KeyUp );
					editBox.SetBounds( left, top, width, height );
					editingNode.RecurseSubNodes( editingNode.Parent );
					editBox.Text = editingNode.SelfAndChildren;
					Controls.Add( editBox );
					editBox.Focus();
				}
			}
		}

		public void Copy(object sender, System.EventArgs e)
		{
			if ( Nodes.Count > 0 )
			{
				Clipboard.SetDataObject( Xml, true );
			}
		}

		public void Paste(object sender, System.EventArgs e)
		{
			IDataObject dataObject = Clipboard.GetDataObject();

			if ( dataObject.GetDataPresent( DataFormats.Text ) )
			{
				parent.Xml = (string) dataObject.GetData( DataFormats.Text );
			}
		}

		public void Delete(object sender, System.EventArgs e)
		{
			if ( Nodes.Count > 0 && null != SelectedNode && parent.LabelEdit )
			{
				string tmp = Xml;

				try
				{
					XmlNode elemToRemove = ( (XmlTreeNode) SelectedNode ).ConnectedXmlElement;

					if ( null != elemToRemove )
					{
						elemToRemove.ParentNode.RemoveChild( elemToRemove );
					
						if ( SelectedNode.NextNode.Text.Equals( "</" + elemToRemove.Name + ">" ) )
						{
							SelectedNode.NextNode.Remove();
						}

						SelectedNode.Remove();
					}
				}
				catch 
				{
					MessageBox.Show( "Cannot delete this node ! Rolling back...", XmlTreeView.MessageBoxTitle );
					parent.Xml = tmp;
				}
			}	 
		}

		public void Save(object sender, System.EventArgs e)
		{
			if ( Nodes.Count > 0 && parent.SaveDialog.ShowDialog() == DialogResult.OK )
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
		/// Takes XML-data as a value.
		/// </summary>
		public string Xml
		{
			get
			{
				return document.OuterXml;
			}
			set
			{
				if ( value.Length > 0 )
				{
					LoadXml( value );
				}
			}
		}
		#endregion // IXmlControl

		#region Event handlers for EditBox

		private void editBox_Leave( object sender, EventArgs e )
		{
			if ( Controls.Contains( editBox ) )
			{
				editBox.Leave -= new EventHandler( editBox_Leave ); 
				editBox.KeyUp -= new KeyEventHandler( editBox_KeyUp ); 
				Controls.Remove( editBox );
				editingNode.Text = editBox.Text;
				editBox.Dispose();
			}
		}

		private void editBox_KeyUp( object sender, KeyEventArgs e )
		{
			if ( e.KeyCode == Keys.Escape )
			{
				editBox.Leave -= new EventHandler( editBox_Leave ); 
				editBox.KeyUp -= new KeyEventHandler( editBox_KeyUp ); 
				Controls.Remove( editBox );
				editBox.Dispose();
			}
		}
		#endregion // EditBox

		#region Private methods
		private void RecursePrintTreeNodes( TreeNodeCollection coll )
		{
			if ( !endOfPageReached )
			{
				foreach ( TreeNode node in coll )
				{
					if ( endOfPageReached )
					{
						break;
					}

					if ( !printedNodes.Contains( node ) )
					{
						string textToDraw = indent + node.Text;
						float textWidthPx = 0;

						yPos = topMargin + ( count * printFont.GetHeight() );

						if ( ( textWidthPx = eventArgs.Graphics.MeasureString( textToDraw, printFont ).Width ) > eventArgs.MarginBounds.Width )
						{
							int startPos = 0;
							float pixPerChar = textWidthPx / textToDraw.Length;
							int maxCharsPerLine = (int)( eventArgs.MarginBounds.Width / pixPerChar );

							while ( ( startPos + maxCharsPerLine ) < textToDraw.Length )
							{
								eventArgs.Graphics.DrawString( textToDraw.Substring( startPos, maxCharsPerLine ), printFont, Brushes.Black, leftMargin, yPos, new StringFormat() );
								startPos += maxCharsPerLine;
								yPos += printFont.GetHeight();
								++count;
							}

							eventArgs.Graphics.DrawString( textToDraw.Substring( startPos ), printFont, Brushes.Black, leftMargin, yPos, new StringFormat() );
						}
						else
						{
							eventArgs.Graphics.DrawString( textToDraw, printFont, Brushes.Black, leftMargin, yPos, new StringFormat() );
						}

						++count;
						printedNodes.Add( node );
					}
					
					if ( endOfPageReached = ( count >= linesPerPage ) )
					{
						eventArgs.HasMorePages = true;
						break;
					}

					if ( node.Nodes.Count > 0 )
					{
						indent += "    ";
						RecursePrintTreeNodes( node.Nodes );
					}
				}
			}

			if ( indent.Length > 0 )
			{
				indent = indent.Substring( 0, indent.Length - 4 );
			}
		}

		private void RecurseTreeNodes( TreeNodeCollection nodes )
		{
			if ( !foundNode )
			{
				string nodeText = null;

				foreach ( TreeNode node in nodes )
				{
					if ( foundNode )
					{
						break;
					}

					nodeText = caseSensitive ? node.Text : node.Text.ToUpper();

					if ( nodeText.IndexOf( searchText ) > -1  && !foundList.Contains( node ) )
					{
						SelectedNode = node;
						SelectedNode.BackColor = Color.Blue;
						foundList.Add( node );
						foundNode = true;
						break;
					}

					if ( node.Nodes.Count > 0 )
					{
						RecurseTreeNodes( node.Nodes );
					}
				}
			}
		}

		private void LoadXml( string xml )
		{
			SuspendLayout();
			document.LoadXml( xml );

			Nodes.Clear();

			if ( xml.StartsWith( "<?" ) )
			{
				Nodes.Add( new XmlTreeNode( xml.Substring( 0, xml.IndexOf( "?>" ) + 2 ), null ) );
			}

			RecurseAndAssignNodes( document.DocumentElement );

			ExpandAll();
			ResumeLayout( false );
		}

		private void RecurseAndAssignNodes( XmlNode elem )
		{
			string attrs = string.Empty;
			XmlTreeNode addedNode = null;

			if ( elem.NodeType == XmlNodeType.Element )
			{
				foreach ( XmlAttribute attr in elem.Attributes )
				{
					attrs += " " + attr.Name + "=\"" + attr.Value + "\"";
				}
			}

			if ( elem.Equals( document.DocumentElement ) )
			{
				addedNode = new XmlTreeNode( "<" + elem.Name + attrs + ">", elem );
				Nodes.Add( addedNode );
				InnerXmlTreeView.CurrentNode = addedNode;
				Nodes.Add( new XmlTreeNode( "</" + elem.Name + ">", null ) );
			}
			else if ( elem.HasChildNodes && elem.ChildNodes[0].NodeType == XmlNodeType.Text )
			{
				addedNode = new XmlTreeNode( "<" + elem.Name + attrs + ">" + elem.InnerText + "</" + elem.Name + ">", elem );
				InnerXmlTreeView.CurrentNode.Nodes.Add( addedNode );
				InnerXmlTreeView.CurrentNode = addedNode;
			}
			else if ( elem is XmlElement && ( (XmlElement) elem ).IsEmpty )
			{
				addedNode = new XmlTreeNode( "<" + elem.Name + attrs + "/>", elem );
				InnerXmlTreeView.CurrentNode.Nodes.Add( addedNode );
				InnerXmlTreeView.CurrentNode = addedNode;
			}
			else
			{
				addedNode = new XmlTreeNode( "<" + elem.Name + attrs + ">", elem );
				InnerXmlTreeView.CurrentNode.Nodes.Add( addedNode );
				InnerXmlTreeView.CurrentNode = addedNode;
				InnerXmlTreeView.CurrentNode.Parent.Nodes.Add( new XmlTreeNode( "</" + elem.Name + ">", null ) );
			}

			foreach ( XmlNode child in elem.ChildNodes )
			{
				if ( child.NodeType == XmlNodeType.Element )
				{
					RecurseAndAssignNodes( child );
				}
				else if ( child.NodeType == XmlNodeType.Comment )
				{
					InnerXmlTreeView.CurrentNode.Nodes.Add( new XmlTreeNode( child.OuterXml, child ) );
				}
			}

			if ( InnerXmlTreeView.CurrentNode.Parent != null )
			{
				InnerXmlTreeView.CurrentNode = InnerXmlTreeView.CurrentNode.Parent;
			}
		}
		#endregion // Private methods

		#region Event handlers for InnerXmlTreeView
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

		protected override void OnDoubleClick( EventArgs e )
		{
			if ( SelectedNode.Parent != null && ( (XmlTreeNode) SelectedNode ).ConnectedXmlElement != null )
			{
				Edit( null, null );
			}
			else
			{

			}
		}
		#endregion // InnerXmlTreeView
	}
}
