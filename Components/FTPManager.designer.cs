
namespace SS.Ynote.Classic
{
	partial class FTPManager
	{
		private System.ComponentModel.IContainer components = null;
		
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FTPManager));
            this.label1 = new System.Windows.Forms.Label();
            this.txtHost = new System.Windows.Forms.TextBox();
            this.btnConnect = new System.Windows.Forms.Button();
            this.txtMessages = new System.Windows.Forms.TextBox();
            this.mnuLocal = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.uploadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuRemote = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.downloadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.lblRemotePath = new System.Windows.Forms.Label();
            this.lvwRemote = new System.Windows.Forms.ListView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.btnRemoteUp = new System.Windows.Forms.Button();
            this.mnuLocal.SuspendLayout();
            this.mnuRemote.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(6, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 22);
            this.label1.TabIndex = 0;
            this.label1.Text = "Host:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtHost
            // 
            this.txtHost.Location = new System.Drawing.Point(48, 14);
            this.txtHost.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtHost.Name = "txtHost";
            this.txtHost.Size = new System.Drawing.Size(294, 22);
            this.txtHost.TabIndex = 1;
            this.txtHost.Tag = "ftp://ftp.au.debian.org/debian";
            this.txtHost.Text = "ftp://localhost";
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(792, 13);
            this.btnConnect.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(104, 34);
            this.btnConnect.TabIndex = 2;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.BtnConnectClick);
            // 
            // txtMessages
            // 
            this.txtMessages.Location = new System.Drawing.Point(28, 300);
            this.txtMessages.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtMessages.Multiline = true;
            this.txtMessages.Name = "txtMessages";
            this.txtMessages.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtMessages.Size = new System.Drawing.Size(887, 152);
            this.txtMessages.TabIndex = 3;
            // 
            // mnuLocal
            // 
            this.mnuLocal.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.uploadToolStripMenuItem});
            this.mnuLocal.Name = "mnuLocal";
            this.mnuLocal.Size = new System.Drawing.Size(113, 26);
            // 
            // uploadToolStripMenuItem
            // 
            this.uploadToolStripMenuItem.Name = "uploadToolStripMenuItem";
            this.uploadToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.uploadToolStripMenuItem.Text = "Upload";
            // 
            // mnuRemote
            // 
            this.mnuRemote.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.downloadToolStripMenuItem});
            this.mnuRemote.Name = "mnuRemote";
            this.mnuRemote.Size = new System.Drawing.Size(129, 26);
            // 
            // downloadToolStripMenuItem
            // 
            this.downloadToolStripMenuItem.Name = "downloadToolStripMenuItem";
            this.downloadToolStripMenuItem.Size = new System.Drawing.Size(128, 22);
            this.downloadToolStripMenuItem.Text = "Download";
            // 
            // txtUsername
            // 
            this.txtUsername.Location = new System.Drawing.Point(453, 14);
            this.txtUsername.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(138, 22);
            this.txtUsername.TabIndex = 9;
            this.txtUsername.Text = "prahlad";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(402, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 22);
            this.label2.TabIndex = 8;
            this.label2.Text = "User:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(634, 14);
            this.txtPassword.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(138, 22);
            this.txtPassword.TabIndex = 11;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(583, 14);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 22);
            this.label3.TabIndex = 10;
            this.label3.Text = "Pass:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblRemotePath
            // 
            this.lblRemotePath.Location = new System.Drawing.Point(77, 48);
            this.lblRemotePath.Name = "lblRemotePath";
            this.lblRemotePath.Size = new System.Drawing.Size(319, 25);
            this.lblRemotePath.TabIndex = 13;
            this.lblRemotePath.Text = "Not Connected";
            // 
            // lvwRemote
            // 
            this.lvwRemote.ContextMenuStrip = this.mnuRemote;
            this.lvwRemote.Location = new System.Drawing.Point(23, 77);
            this.lvwRemote.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lvwRemote.Name = "lvwRemote";
            this.lvwRemote.Size = new System.Drawing.Size(868, 204);
            this.lvwRemote.SmallImageList = this.imageList1;
            this.lvwRemote.TabIndex = 14;
            this.lvwRemote.UseCompatibleStateImageBehavior = false;
            this.lvwRemote.View = System.Windows.Forms.View.SmallIcon;
            this.lvwRemote.SelectedIndexChanged += new System.EventHandler(this.lvwRemote_SelectedIndexChanged);
            this.lvwRemote.DoubleClick += new System.EventHandler(this.lvwRemote_DoubleClick);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "48px-Gnome-text-x-generic.svg.png");
            this.imageList1.Images.SetKeyName(1, "48px-Gnome-folder.svg.png");
            // 
            // btnRemoteUp
            // 
            this.btnRemoteUp.Location = new System.Drawing.Point(28, 44);
            this.btnRemoteUp.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnRemoteUp.Name = "btnRemoteUp";
            this.btnRemoteUp.Size = new System.Drawing.Size(43, 29);
            this.btnRemoteUp.TabIndex = 17;
            this.btnRemoteUp.Text = "Up";
            this.btnRemoteUp.UseVisualStyleBackColor = true;
            this.btnRemoteUp.Click += new System.EventHandler(this.btnRemoteUp_Click);
            // 
            // FTPManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(903, 541);
            this.Controls.Add(this.btnRemoteUp);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.lblRemotePath);
            this.Controls.Add(this.lvwRemote);
            this.Controls.Add(this.txtUsername);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtMessages);
            this.Controls.Add(this.txtHost);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnConnect);
            this.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "FTPManager";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FTP Manager";
            this.Load += new System.EventHandler(this.MainFormLoad);
            this.mnuLocal.ResumeLayout(false);
            this.mnuRemote.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

		}
        private System.Windows.Forms.Label lblRemotePath;
		private System.Windows.Forms.TextBox txtUsername;
		private System.Windows.Forms.TextBox txtPassword;
		private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox txtHost;
        private System.Windows.Forms.Button btnConnect;
		private System.Windows.Forms.TextBox txtMessages;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ContextMenuStrip mnuRemote;
        private System.Windows.Forms.ToolStripMenuItem downloadToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip mnuLocal;
        private System.Windows.Forms.ToolStripMenuItem uploadToolStripMenuItem;
        private System.Windows.Forms.ListView lvwRemote;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Button btnRemoteUp;
	}
}
