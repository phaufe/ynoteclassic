
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using WeifenLuo.WinFormsUI.Docking;
using SS.Web.Tools.Clients.FTP;
using SS.Web.Tools.Clients;


namespace SS.Ynote.Classic
{
	public partial class FTPManager : DockContent
	{
		Ftper testftp=new Ftper();
		
		public FTPManager()
		{
			InitializeComponent();
		
            testftp.ftpobject.statusChange += new EventHandler<statusChangeEventArgs>(ftpobject_statusChange);
		}

        void ftpobject_statusChange(object sender, statusChangeEventArgs e)
        {
            txtMessages.Text += "STATUS: " + e.message + " db:" + e.bytesDownloaded + " up:" + e.bytesUploaded + System.Environment.NewLine;
        }

		
		void MainFormLoad(object sender, EventArgs e)
		{
            this.txtHost.Text = Properties.Settings.Default.Host;
            txtUsername.Text = Properties.Settings.Default.Username;
            txtPassword.Text = Properties.Settings.Default.Password;
            this.Text += " " + ReflectionUtils.GetVersion();
            if (Properties.Settings.Default.Host == "")
            { }else{BtnConnectClick(sender,e);}
            
		}
		
		void BtnConnectClick(object sender, EventArgs e)
		{
            if(txtHost.Text.Substring(0,6) == "ftp://"){
            }
            else { txtHost.Text = "ftp://" + txtHost.Text; }

			List<ftpinfo> files= testftp.connect(txtHost.Text,txtUsername.Text,txtPassword.Text);
            listRemoteFiles(files);
            lblRemotePath.Text = new Uri(txtHost.Text).AbsolutePath; 
            if (savedata.Checked == true) {
                Properties.Settings.Default.Host = this.txtHost.Text;
                Properties.Settings.Default.Username = txtUsername.Text;
                Properties.Settings.Default.Password = txtPassword.Text;
            }
		}

        private void listRemoteFiles(List<ftpinfo> files)
        {
            lvwRemote.Items.Clear();
            if (files == null)
                return;
            foreach (ftpinfo file in files)
            {
                ListViewItem it = new ListViewItem(file.filename, file.fileType == directoryEntryTypes.directory ? (int)directoryEntryTypes.directory : (int)directoryEntryTypes.file);
                lvwRemote.Items.Add( it);
            }
        }

        private void btnAddUpload_Click(object sender, EventArgs e)
        {

        }

        private void lstRemote_SelectedIndexChanged(object sender, EventArgs e)
        {
            byte[] pdfBytes = File.ReadAllBytes("c:\foo.pdf");
            int fileSize = pdfBytes.Length / 5; //assuming foo is 40MB filesize will be abt 8MB
            MemoryStream m = new MemoryStream(pdfBytes);
            for (int i = 0; i < 4; i++)
            {
                byte[] tbytes = new byte[fileSize];
                m.Read(tbytes,i*fileSize,fileSize);
                File.WriteAllBytes("C:\foo" + i + ".part",tbytes);
            }
            
        }

        private void btnAddDownload_Click(object sender, EventArgs e)
        {

        }
        void refreshRemote()
        {
            List<ftpinfo> files = testftp.browse(txtHost.Text + lblRemotePath.Text);
            listRemoteFiles(files);
        }

        private void lvwRemote_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void lvwRemote_DoubleClick(object sender, EventArgs e)
        {
            if (lvwRemote.SelectedItems.Count == 1)
            {
                if (lvwRemote.SelectedItems[0].ImageIndex == (int)directoryEntryTypes.directory) //directory listing
                {
                    lblRemotePath.Text += lvwRemote.SelectedItems[0].Text;
                    refreshRemote();
                }
            }
        }

        private void btnRemoteUp_Click(object sender, EventArgs e)
        {
           
        }

        private void lvwLocal_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (lblRemotePath.Text == "/")
            {
                //already in root dir.
                MessageBox.Show("Already in root directory.");
            }
            else if (lblRemotePath.Text == "Not Connected")
            {
                MessageBox.Show("Not connected");
            }
            else
            {
                lblRemotePath.Text = StringUtils.ExtractFolderFromPath(lblRemotePath.Text, "/", false);
                if (lblRemotePath.Text.Length == 0)
                    lblRemotePath.Text = "/";

                refreshRemote();
                //string currpath = lblRemotePath.Text.Substring(0, lblRemotePath.Text.Length - 1);
                //string newpath = lblRemotePath.Text.Substring(0, currpath.LastIndexOf("/"));
                //List<ftpinfo> files = testftp.browse(txtHost.Text + newpath);
                //listRemoteFiles(files);
            }
        }
	}
}
