namespace SS.Ynote.Classic
{
    partial class PluginTree
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.PluginView = new SS.Ynote.Engine.Framework.Plugins.Controls.PluginTreeView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // PluginView
            // 
            this.PluginView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PluginView.HotTracking = true;
            this.PluginView.ImageIndex = 0;
            this.PluginView.ImageListImageSize = new System.Drawing.Size(16, 16);
            this.PluginView.Location = new System.Drawing.Point(0, 0);
            this.PluginView.Name = "PluginView";
            this.PluginView.SelectedImageIndex = 0;
            this.PluginView.ShowLines = false;
            this.PluginView.Size = new System.Drawing.Size(592, 387);
            this.PluginView.TabIndex = 0;
            this.PluginView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.PluginView_AfterSelect);
            // 
            // panel1
            // 
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(170, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(422, 387);
            this.panel1.TabIndex = 1;
            this.panel1.Visible = false;
            // 
            // PluginTree
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(592, 387);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.PluginView);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "PluginTree";
            this.Text = "PluginTree";
            this.ResumeLayout(false);

        }

        #endregion

        private Engine.Framework.Plugins.Controls.PluginTreeView PluginView;
        private System.Windows.Forms.Panel panel1;
    }
}