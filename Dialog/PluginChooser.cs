using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using SS.Ynote.Engine.Framework.Extensions.Core;
using SS.Ynote.Engine.Framework.Plugins.Utilities;
using SS.Ynote.Classic.Properties;

namespace SS.Ynote.Classic
{
    public class PluginChooser : Form
    {
        private IDictionary<string, string> availablePlugins;
        private Engine.Controls.A1Panel a1Panel1;
        private Engine.Controls.Button button2;
        private Engine.Controls.Button button1;
    
        public IDictionary<string, string> SelectedPlugins { get; private set; }

        private CheckedListBox clbAvailablePlugins;
    
        public PluginChooser()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.clbAvailablePlugins = new System.Windows.Forms.CheckedListBox();
            this.a1Panel1 = new SS.Ynote.Engine.Controls.A1Panel();
            this.button1 = new SS.Ynote.Engine.Controls.Button();
            this.button2 = new SS.Ynote.Engine.Controls.Button();
            this.a1Panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // clbAvailablePlugins
            // 
            this.clbAvailablePlugins.Dock = System.Windows.Forms.DockStyle.Top;
            this.clbAvailablePlugins.FormattingEnabled = true;
            this.clbAvailablePlugins.Location = new System.Drawing.Point(0, 0);
            this.clbAvailablePlugins.Name = "clbAvailablePlugins";
            this.clbAvailablePlugins.Size = new System.Drawing.Size(284, 349);
            this.clbAvailablePlugins.TabIndex = 0;
            // 
            // a1Panel1
            // 
            this.a1Panel1.BorderColor = System.Drawing.Color.Gray;
            this.a1Panel1.Controls.Add(this.button2);
            this.a1Panel1.Controls.Add(this.button1);
            this.a1Panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.a1Panel1.GradientEndColor = System.Drawing.SystemColors.Control;
            this.a1Panel1.GradientStartColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.a1Panel1.Image = null;
            this.a1Panel1.ImageLocation = new System.Drawing.Point(4, 4);
            this.a1Panel1.Location = new System.Drawing.Point(0, 355);
            this.a1Panel1.Name = "a1Panel1";
            this.a1Panel1.Size = new System.Drawing.Size(284, 77);
            this.a1Panel1.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.button1.Location = new System.Drawing.Point(27, 24);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(94, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "OK";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.button2.Location = new System.Drawing.Point(147, 24);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(106, 23);
            this.button2.TabIndex = 1;
            this.button2.Text = "Cancel";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // PluginChooser
            // 
            this.ClientSize = new System.Drawing.Size(284, 432);
            this.Controls.Add(this.a1Panel1);
            this.Controls.Add(this.clbAvailablePlugins);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "PluginChooser";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Choose Plugins";
            this.Load += new System.EventHandler(this.PluginChooser_Load);
            this.a1Panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        private void PluginChooser_Load(object sender, EventArgs e)
        {
            availablePlugins = PluginHelper.FindPlugins();
            clbAvailablePlugins.Items.AddRange(availablePlugins.Keys.ToArray());

            SelectedPlugins = (from p in AppContext.ConfigurationFile.Startup.Plugins
                               select p).ToDictionary(key => key.Title, value => value.AssemblyPath);

            for (int i = 0; i < clbAvailablePlugins.Items.Count; i++)
            {
                if (clbAvailablePlugins.Items[i].ToString().In(SelectedPlugins.Keys))
                {
                    clbAvailablePlugins.SetItemChecked(i, true);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SelectedPlugins = (from p in availablePlugins
                               where p.Key.In(clbAvailablePlugins.CheckedItems)
                               select p).ToDictionary(key => key.Key, value => value.Value);

            if (SelectedPlugins != null && SelectedPlugins.Count > 0)
            {
                AppContext.ConfigurationFile.Startup.Plugins.Clear();
                foreach (KeyValuePair<string, string> kv in SelectedPlugins)
                {
                    if (!AppContext.ConfigurationFile.Startup.Plugins.Contains(kv.Key))
                    {
                        StartupPlugin plugin = new StartupPlugin();
                        plugin.Title = kv.Key;
                        plugin.AssemblyPath = kv.Value;
                        AppContext.ConfigurationFile.Startup.Plugins.Add(plugin);
                    }
                }
            }

            AppContext.ConfigurationFile.Save(Settings.Default.PluginConfigFile);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
