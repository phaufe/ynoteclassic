using System;
using System.Drawing;
using System.Threading;
using System.ComponentModel;
using System.Windows.Forms;

namespace XMLTreeView
{
	internal class SearchDlg : System.Windows.Forms.Form
	{
		#region Fields
		private System.Windows.Forms.TextBox searchTextBox;
		private System.Windows.Forms.Button goBtn;
		private System.Windows.Forms.Button cancelBtn;
		private System.Windows.Forms.Label label1;
		private IXmlControl iterator;
		private System.Windows.Forms.RadioButton caseSensitiveRBtn;
		private System.Windows.Forms.RadioButton caseInsensitiveRBtn;

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		#endregion // Fields

		#region Constructors
		internal SearchDlg( IXmlControl iterator )
		{
			this.iterator = iterator;
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			searchTextBox.Focus();
			//
			// TODO: Add any constructor code after InitializeComponent call
			//
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

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.searchTextBox = new System.Windows.Forms.TextBox();
            this.goBtn = new System.Windows.Forms.Button();
            this.cancelBtn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.caseSensitiveRBtn = new System.Windows.Forms.RadioButton();
            this.caseInsensitiveRBtn = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // searchTextBox
            // 
            this.searchTextBox.Location = new System.Drawing.Point(24, 24);
            this.searchTextBox.Name = "searchTextBox";
            this.searchTextBox.Size = new System.Drawing.Size(240, 20);
            this.searchTextBox.TabIndex = 0;
            this.searchTextBox.TextChanged += new System.EventHandler(this.searchTextBox_TextChanged);
            this.searchTextBox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.searchTextBox_KeyUp);
            // 
            // goBtn
            // 
            this.goBtn.Location = new System.Drawing.Point(102, 79);
            this.goBtn.Name = "goBtn";
            this.goBtn.Size = new System.Drawing.Size(75, 23);
            this.goBtn.TabIndex = 1;
            this.goBtn.Text = "Search";
            this.goBtn.Click += new System.EventHandler(this.goBtn_Click);
            // 
            // cancelBtn
            // 
            this.cancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelBtn.Location = new System.Drawing.Point(183, 80);
            this.cancelBtn.Name = "cancelBtn";
            this.cancelBtn.Size = new System.Drawing.Size(75, 23);
            this.cancelBtn.TabIndex = 2;
            this.cancelBtn.Text = "Cancel";
            this.cancelBtn.Click += new System.EventHandler(this.cancelBtn_Click);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(24, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 16);
            this.label1.TabIndex = 3;
            this.label1.Text = "Search for :";
            // 
            // caseSensitiveRBtn
            // 
            this.caseSensitiveRBtn.Location = new System.Drawing.Point(154, 48);
            this.caseSensitiveRBtn.Name = "caseSensitiveRBtn";
            this.caseSensitiveRBtn.Size = new System.Drawing.Size(104, 24);
            this.caseSensitiveRBtn.TabIndex = 4;
            this.caseSensitiveRBtn.Text = "case sensitive";
            this.caseSensitiveRBtn.CheckedChanged += new System.EventHandler(this.caseSensitiveRBtn_CheckedChanged);
            // 
            // caseInsensitiveRBtn
            // 
            this.caseInsensitiveRBtn.Checked = true;
            this.caseInsensitiveRBtn.Location = new System.Drawing.Point(30, 48);
            this.caseInsensitiveRBtn.Name = "caseInsensitiveRBtn";
            this.caseInsensitiveRBtn.Size = new System.Drawing.Size(104, 24);
            this.caseInsensitiveRBtn.TabIndex = 5;
            this.caseInsensitiveRBtn.TabStop = true;
            this.caseInsensitiveRBtn.Text = "case insensitive";
            // 
            // SearchDlg
            // 
            this.AcceptButton = this.goBtn;
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.CancelButton = this.cancelBtn;
            this.ClientSize = new System.Drawing.Size(280, 114);
            this.Controls.Add(this.caseInsensitiveRBtn);
            this.Controls.Add(this.caseSensitiveRBtn);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cancelBtn);
            this.Controls.Add(this.goBtn);
            this.Controls.Add(this.searchTextBox);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(296, 152);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(296, 152);
            this.Name = "SearchDlg";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Search for XML Node - SS Ynote Classic";
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		#region Event handlers for SearchDlg
		private void cancelBtn_Click(object sender, System.EventArgs e)
		{
			Close();
		}

		private void goBtn_Click(object sender, System.EventArgs e)
		{
			iterator.Next();
			searchTextBox.Focus();
		}

		private void searchTextBox_TextChanged(object sender, System.EventArgs e)
		{
			iterator.StartSearch( searchTextBox.Text, caseSensitiveRBtn.Checked );
		}

		private void caseSensitiveRBtn_CheckedChanged(object sender, System.EventArgs e)
		{
			iterator.StartSearch( searchTextBox.Text, caseSensitiveRBtn.Checked );
		}

		private void searchTextBox_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if ( e.KeyCode == Keys.F3 )
			{
				goBtn_Click( null, null );
			}
		}
		#endregion // SearchDlg
	}
}
