using System;
using System.Xml;
using System.Windows.Forms;
using SS.Ynote.Engine.Framework;

namespace XMLTreeView
{
	/// <summary>
	/// Summary description for XmlTreeNode.
	/// </summary>
	internal class XmlTreeNode : TreeNode
	{
		#region Fields
		private XmlNode elem = null;
		private string childrenXml = string.Empty;
		private string indent = string.Empty;
		private bool hitEnd = false;
		private bool hitStart = false;
		#endregion // Fields

		#region Constructors
		internal XmlTreeNode( string text, XmlNode elem ) : base( text )
		{
			this.elem = elem;
		}
		#endregion // Constructors

		#region Properties
		internal XmlNode ConnectedXmlElement
		{
			get
			{
				return elem;
			}
		}

		internal string SelfAndChildren
		{
			get
			{
				return childrenXml;
			}
		}

		public new string Text
		{
			get
			{
				return base.Text;
			}
			set
			{
				if ( value != null && value.Length > 0 )
				{
					try
					{
						if ( value != childrenXml )
						{
							XmlDocumentFragment frag = elem.OwnerDocument.CreateDocumentFragment();
							frag.InnerXml = value;
							elem.ParentNode.ReplaceChild( frag, elem );
							InnerXmlTreeView innerView = (InnerXmlTreeView) TreeView;
							innerView.Xml = innerView.Xml;
						}
					}
					catch (XmlException xEx) 
					{
						MessageBox.Show( xEx.Message, XmlTreeView.MessageBoxTitle );
					}
					finally
					{
						childrenXml = string.Empty;
						hitEnd = false;
					}
				}
			}
		}
		#endregion // Properties

		#region Internal methods
		internal void RecurseSubNodes( TreeNode entryNode )
		{
			foreach( TreeNode node in entryNode.Nodes )
			{
				if ( this.Equals( node ) )
				{
					hitStart = true;
				}

				if( !hitEnd && hitStart )
				{
					hitEnd = ( node.Text.EndsWith( "</" + elem.Name + ">" ) && hitStart ) || ( ( (XmlTreeNode) node).ConnectedXmlElement != null && ( (XmlTreeNode) node).ConnectedXmlElement.NodeType == XmlNodeType.Comment );

					childrenXml += indent + node.Text + Environment.NewLine;

					if ( node.Nodes.Count > 0 )
					{
						indent += "    ";
						RecurseSubNodes( node );
					}
				}
			}

			if ( indent.Length > 3 )
			{
				indent = indent.Substring( 0, indent.Length - 4 );
			}
		}
		#endregion // Internal methods
	}
}
