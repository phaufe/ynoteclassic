using System;
using System.IO;
using System.Xml;
using System.Data;
using System.Drawing;
using System.Collections;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing.Printing;

namespace XMLTreeView
{
	/// <summary>
	/// Summary description for XmlTreeView.
	/// </summary>
	public class XmlTreeView : System.Windows.Forms.UserControl
	{
		#region Fields
		private System.Windows.Forms.ContextMenu popUpMenu;
		private System.Windows.Forms.MenuItem printMenuItem;
		private System.Windows.Forms.MenuItem searchMenuItem;
		private System.Windows.Forms.MenuItem deleteMenuItem;
		private System.Windows.Forms.PrintDialog printDialog1;
		private System.Windows.Forms.SaveFileDialog saveFileDialog1;
		private System.Windows.Forms.MenuItem saveMenuItem;
		private System.Windows.Forms.MenuItem pasteMenuItem;
		private System.Windows.Forms.MenuItem copyMenuItem;
		private System.Windows.Forms.MenuItem editMenuItem;
		private XMLTreeView.IXmlControl xmlControl;
		private string content = string.Empty;
		internal string fileXml = string.Empty;
		internal string filePath = string.Empty;
		public const string MessageBoxTitle = "XmlTreeView";

		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		#endregion // Fields

		#region Constructors
		public XmlTreeView()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();
		}
		#endregion // Constructors

		#region Dispose
		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}
		#endregion // Dispose

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.popUpMenu = new System.Windows.Forms.ContextMenu();
            this.printMenuItem = new System.Windows.Forms.MenuItem();
            this.searchMenuItem = new System.Windows.Forms.MenuItem();
            this.deleteMenuItem = new System.Windows.Forms.MenuItem();
            this.saveMenuItem = new System.Windows.Forms.MenuItem();
            this.pasteMenuItem = new System.Windows.Forms.MenuItem();
            this.copyMenuItem = new System.Windows.Forms.MenuItem();
            this.editMenuItem = new System.Windows.Forms.MenuItem();
            this.printDialog1 = new System.Windows.Forms.PrintDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.SuspendLayout();
            // 
            // popUpMenu
            // 
            this.popUpMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.printMenuItem,
            this.searchMenuItem,
            this.deleteMenuItem,
            this.saveMenuItem,
            this.pasteMenuItem,
            this.copyMenuItem,
            this.editMenuItem});
            // 
            // printMenuItem
            // 
            this.printMenuItem.Index = 0;
            this.printMenuItem.Text = "&Print";
            // 
            // searchMenuItem
            // 
            this.searchMenuItem.Index = 1;
            this.searchMenuItem.Text = "&Search";
            // 
            // deleteMenuItem
            // 
            this.deleteMenuItem.Index = 2;
            this.deleteMenuItem.Text = "&Delete";
            // 
            // saveMenuItem
            // 
            this.saveMenuItem.Index = 3;
            this.saveMenuItem.Text = "S&ave As...";
            // 
            // pasteMenuItem
            // 
            this.pasteMenuItem.Index = 4;
            this.pasteMenuItem.Text = "Past&e";
            // 
            // copyMenuItem
            // 
            this.copyMenuItem.Index = 5;
            this.copyMenuItem.Text = "&Copy";
            // 
            // editMenuItem
            // 
            this.editMenuItem.Index = 6;
            this.editMenuItem.Text = "Ed&it";
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.Filter = "XML-Files|*.xml";
            // 
            // XmlTreeView
            // 
            this.BackColor = System.Drawing.Color.White;
            this.Name = "XmlTreeView";
            this.Size = new System.Drawing.Size(232, 321);
            this.ResumeLayout(false);

		}
		#endregion

		#region Private methods
		private void DeregisterEvents(IXmlControl control)
		{
			if ( null != control )
			{
				this.printMenuItem.Click -= new System.EventHandler(control.Print);
				this.searchMenuItem.Click -= new System.EventHandler(control.Search);
				this.deleteMenuItem.Click -= new System.EventHandler(control.Delete);
				this.saveMenuItem.Click -= new System.EventHandler(control.Save);
				this.pasteMenuItem.Click -= new System.EventHandler(control.Paste);
				this.copyMenuItem.Click -= new System.EventHandler(control.Copy);
				this.editMenuItem.Click -= new System.EventHandler(control.Edit);
				this.printDialog1.Document.PrintPage -= new System.Drawing.Printing.PrintPageEventHandler(control.PrintPage);
			}
		}

		private void RegisterEvents(IXmlControl control)
		{
			if ( null != control )
			{
				this.printMenuItem.Click += new System.EventHandler(control.Print);
				this.searchMenuItem.Click += new System.EventHandler(control.Search);
				this.deleteMenuItem.Click += new System.EventHandler(control.Delete);
				this.saveMenuItem.Click += new System.EventHandler(control.Save);
				this.pasteMenuItem.Click += new System.EventHandler(control.Paste);
				this.copyMenuItem.Click += new System.EventHandler(control.Copy);
				this.editMenuItem.Click += new System.EventHandler(control.Edit);
				this.printDialog1.Document = control.PrintDocument;
				this.printDialog1.Document.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(control.PrintPage);
			}
		}
		#endregion // Private methods

		#region Event handler for form closing
		internal void form_Closing( object sender, CancelEventArgs e )
		{
			if ( !content.Equals( Xml ) )
			{
				if ( MessageBox.Show( "Save changes?", XmlTreeView.MessageBoxTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1 ) == DialogResult.Yes )
				{
					if ( saveFileDialog1.ShowDialog() == DialogResult.OK )
					{
						using ( StreamWriter sw = new StreamWriter( saveFileDialog1.FileName ) )
						{
							sw.Write( Xml );
						}
					}
				}
			}
		}
		#endregion // Event handler

		#region Properties
		/// <summary>
		/// Sets or gets XML-data in a string form.
		/// </summary>
		public string Xml
		{
			get
			{
				string result = string.Empty;

				if ( Controls.Count > 0 )
				{
					result = ((IXmlControl) Controls[0]).Xml;
				}

				return result;
			}
			set
			{
				Controls.Clear();

				try
				{
					DeregisterEvents( xmlControl );

					xmlControl = new InnerXmlTreeView(this);
					xmlControl.Xml = value;
					content = xmlControl.Xml;

					RegisterEvents( xmlControl );
				}
				catch
				{
					Controls.Clear();
					
					DeregisterEvents( xmlControl );

					xmlControl = new InnerTextBox(this);
					xmlControl.Xml = value;

					RegisterEvents( xmlControl );
				}

				( (Control) xmlControl ).ContextMenu = this.popUpMenu;

				Controls.Add( (Control) xmlControl );
			}
		}

		/// <summary>
		/// Sets or gets the fully qualified path to a file containing XML-data.
		/// </summary>
		public string XmlFile
		{
			get
			{
				if ( fileXml != Xml )
				{
					filePath = string.Empty;
				}

				return filePath;
			}

			set
			{
				if ( null != value && value.Length > 0 )
				{
					using ( StreamReader fs = new StreamReader( value ) )
					{
						fileXml = fs.ReadToEnd();
						Xml = fileXml;
						filePath = value;
					}
				}
			}
		}

		internal PrintDialog PrintDialog
		{
			get
			{
				return printDialog1;
			}
		}

		internal SaveFileDialog SaveDialog
		{
			get
			{
				return saveFileDialog1;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		[DefaultValue(true)]
		public bool LabelEdit
		{
			get
			{
				return deleteMenuItem.Enabled && editMenuItem.Enabled;
			}
			set
			{
				editMenuItem.Enabled = deleteMenuItem.Enabled = value;
			}
		}
		#endregion // Properties
	}
}
