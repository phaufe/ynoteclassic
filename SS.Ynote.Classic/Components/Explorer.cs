using System.Drawing;
using System;
using System.Windows.Forms;
namespace SS.Ynote.Classic.Components
{
    public partial class Explorer : WeifenLuo.WinFormsUI.Docking.DockContent
    {
        private string mRootPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        string recentfilename;
        string RootPath
        {
            get { return mRootPath; }
            set { mRootPath = value; }
        }
        public Explorer()
        {
            InitializeComponent();
            Bitmap bmp = Properties.Resources.folder;
            this.Icon = Icon.FromHandle(bmp.GetHicon());
            TreeNode mRootNode = new TreeNode();
            mRootNode.Text = RootPath;
            mRootNode.Tag = RootPath;
        }
    }
    
}
