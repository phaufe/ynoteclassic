using System.Drawing;
using SS.Ynote.Engine.Framework.Plugins.Controls;
using SS.Ynote.Engine.Framework.Plugins.Interface;
using SS.Ynote.Engine.Framework.Plugins.Utilities;
using System.Windows.Forms;

namespace SS.Ynote.Classic
{
    public partial class PluginTree : WeifenLuo.WinFormsUI.Docking.DockContent
    {

        public PluginTree()
        {
            InitializeComponent();
            Bitmap bmp = Properties.Resources.plugin;
            this.Icon = Icon.FromHandle(bmp.GetHicon());
        }
        public PluginTreeView Tree
        {
            get { return this.PluginView; }
        }

        private void PluginView_AfterSelect(object sender, System.Windows.Forms.TreeViewEventArgs e)
        {
            if (e.Node.Tag == null)
            { return; }

            PluginInfo pluginInfo = e.Node.Tag as PluginInfo;

            if (pluginInfo.Plugin is IUserControlPlugin)
            {
                System.Windows.Forms.UserControl control = ((IUserControlPlugin)pluginInfo.Plugin).Content;
                panel1.Controls.Add(control); panel1.Visible = true;
                control.Dock = DockStyle.Fill;
            }
            else if (pluginInfo.Plugin is IFormPlugin)
            {
                IFormPlugin formPlugin = (IFormPlugin)pluginInfo.Plugin;
                Form form = formPlugin.Content;

                if (form.IsDisposed)
                {
                    form = PluginHelper.CreateNewInstance<Form>(pluginInfo.AssemblyPath);
                }

                if (formPlugin.ShowAs == ShowAs.Dialog)
                {
                    form.ShowDialog();
                }
                else
                {
                    form.Show();
                }
            }
        }
        
    }
}
