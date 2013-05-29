namespace SS.Ynote.Classic
{
    partial class Editor
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
            this.codebox = new FastColoredTextBoxNS.FastColoredTextBox();
            this.EditorContext = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.undoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.redoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.findToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.replaceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.goToToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.foldingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.foldSelectedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.unFoldSelectedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.markToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.markLineToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.usingDefaultToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.usingRedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.usingYellowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.usingGreenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.usingGrayToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.unmarkLineToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.markCharToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.usingYellowStyleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.usingRedStyleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.usingGreenStyleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.usingBlueStyleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.usingGrayStyleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.autoCompleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.selectAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.TabContext = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeAllButThisToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.copyFileNameToClipboardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyFilePathToClipboardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.openContainingFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.unmarkSelectionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.codebox)).BeginInit();
            this.EditorContext.SuspendLayout();
            this.TabContext.SuspendLayout();
            this.SuspendLayout();
            // 
            // codebox
            // 
            this.codebox.AutoScrollMinSize = new System.Drawing.Size(45, 14);
            this.codebox.BackBrush = null;
            this.codebox.BackColor = global::SS.Ynote.Classic.Properties.Settings.Default.EditorBackColor;
            this.codebox.CharHeight = 14;
            this.codebox.CharWidth = 8;
            this.codebox.ContextMenuStrip = this.EditorContext;
            this.codebox.CurrentLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.codebox.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.codebox.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.codebox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.codebox.ForeColor = System.Drawing.Color.Black;
            this.codebox.IndentBackColor = global::SS.Ynote.Classic.Properties.Settings.Default.IndentBackColor;
            this.codebox.IsReplaceMode = false;
            this.codebox.LeftBracket = '(';
            this.codebox.LeftPadding = global::SS.Ynote.Classic.Properties.Settings.Default.PaddingWidth;
            this.codebox.Location = new System.Drawing.Point(0, 0);
            this.codebox.Name = "codebox";
            this.codebox.Paddings = new System.Windows.Forms.Padding(0);
            this.codebox.RightBracket = ')';
            this.codebox.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.codebox.ShowFoldingLines = true;
            this.codebox.Size = new System.Drawing.Size(775, 469);
            this.codebox.TabIndex = 0;
            this.codebox.Zoom = 100;
            this.codebox.TextChangedDelayed += new System.EventHandler<FastColoredTextBoxNS.TextChangedEventArgs>(this.fctb_textchangeddelayed);
            // 
            // EditorContext
            // 
            this.EditorContext.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cutToolStripMenuItem,
            this.copyToolStripMenuItem,
            this.pToolStripMenuItem,
            this.toolStripSeparator1,
            this.undoToolStripMenuItem,
            this.redoToolStripMenuItem,
            this.toolStripSeparator2,
            this.findToolStripMenuItem,
            this.replaceToolStripMenuItem,
            this.goToToolStripMenuItem,
            this.toolStripSeparator5,
            this.foldingToolStripMenuItem,
            this.markToolStripMenuItem,
            this.toolStripSeparator6,
            this.autoCompleteToolStripMenuItem,
            this.toolStripSeparator7,
            this.selectAllToolStripMenuItem});
            this.EditorContext.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.EditorContext.Name = "EditorContext";
            this.EditorContext.Size = new System.Drawing.Size(156, 320);
            // 
            // cutToolStripMenuItem
            // 
            this.cutToolStripMenuItem.Name = "cutToolStripMenuItem";
            this.cutToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.cutToolStripMenuItem.Text = "Cut  ";
            this.cutToolStripMenuItem.Click += new System.EventHandler(this.cutToolStripMenuItem_Click);
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.copyToolStripMenuItem.Text = "Copy ";
            this.copyToolStripMenuItem.Click += new System.EventHandler(this.copyToolStripMenuItem_Click);
            // 
            // pToolStripMenuItem
            // 
            this.pToolStripMenuItem.Name = "pToolStripMenuItem";
            this.pToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.pToolStripMenuItem.Text = "Paste";
            this.pToolStripMenuItem.Click += new System.EventHandler(this.pToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(152, 6);
            // 
            // undoToolStripMenuItem
            // 
            this.undoToolStripMenuItem.Name = "undoToolStripMenuItem";
            this.undoToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.undoToolStripMenuItem.Text = "Undo";
            this.undoToolStripMenuItem.Click += new System.EventHandler(this.undoToolStripMenuItem_Click);
            // 
            // redoToolStripMenuItem
            // 
            this.redoToolStripMenuItem.Name = "redoToolStripMenuItem";
            this.redoToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.redoToolStripMenuItem.Text = "Redo";
            this.redoToolStripMenuItem.Click += new System.EventHandler(this.redoToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(152, 6);
            // 
            // findToolStripMenuItem
            // 
            this.findToolStripMenuItem.Name = "findToolStripMenuItem";
            this.findToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.findToolStripMenuItem.Text = "Find ";
            this.findToolStripMenuItem.Click += new System.EventHandler(this.findToolStripMenuItem_Click);
            // 
            // replaceToolStripMenuItem
            // 
            this.replaceToolStripMenuItem.Name = "replaceToolStripMenuItem";
            this.replaceToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.replaceToolStripMenuItem.Text = "Replace";
            this.replaceToolStripMenuItem.Click += new System.EventHandler(this.replaceToolStripMenuItem_Click);
            // 
            // goToToolStripMenuItem
            // 
            this.goToToolStripMenuItem.Name = "goToToolStripMenuItem";
            this.goToToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.goToToolStripMenuItem.Text = "Go To";
            this.goToToolStripMenuItem.Click += new System.EventHandler(this.goToToolStripMenuItem_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(152, 6);
            // 
            // foldingToolStripMenuItem
            // 
            this.foldingToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.foldSelectedToolStripMenuItem,
            this.unFoldSelectedToolStripMenuItem});
            this.foldingToolStripMenuItem.Name = "foldingToolStripMenuItem";
            this.foldingToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.foldingToolStripMenuItem.Text = "Folding";
            // 
            // foldSelectedToolStripMenuItem
            // 
            this.foldSelectedToolStripMenuItem.Name = "foldSelectedToolStripMenuItem";
            this.foldSelectedToolStripMenuItem.Size = new System.Drawing.Size(261, 22);
            this.foldSelectedToolStripMenuItem.Text = "Fold Selected    [Ctrl + Shift  + F]";
            this.foldSelectedToolStripMenuItem.Click += new System.EventHandler(this.foldSelectedToolStripMenuItem_Click);
            // 
            // unFoldSelectedToolStripMenuItem
            // 
            this.unFoldSelectedToolStripMenuItem.Name = "unFoldSelectedToolStripMenuItem";
            this.unFoldSelectedToolStripMenuItem.Size = new System.Drawing.Size(261, 22);
            this.unFoldSelectedToolStripMenuItem.Text = "UnFold Selected     [Ctrl + Shift + U]";
            this.unFoldSelectedToolStripMenuItem.Click += new System.EventHandler(this.unFoldSelectedToolStripMenuItem_Click);
            // 
            // markToolStripMenuItem
            // 
            this.markToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.markLineToolStripMenuItem,
            this.unmarkLineToolStripMenuItem,
            this.toolStripSeparator8,
            this.markCharToolStripMenuItem,
            this.unmarkSelectionToolStripMenuItem});
            this.markToolStripMenuItem.Name = "markToolStripMenuItem";
            this.markToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.markToolStripMenuItem.Text = "Mark/Unmark";
            // 
            // markLineToolStripMenuItem
            // 
            this.markLineToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.usingDefaultToolStripMenuItem,
            this.usingRedToolStripMenuItem,
            this.usingYellowToolStripMenuItem,
            this.usingGreenToolStripMenuItem,
            this.usingGrayToolStripMenuItem});
            this.markLineToolStripMenuItem.Name = "markLineToolStripMenuItem";
            this.markLineToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.markLineToolStripMenuItem.Text = "Mark Line";
            // 
            // usingDefaultToolStripMenuItem
            // 
            this.usingDefaultToolStripMenuItem.Name = "usingDefaultToolStripMenuItem";
            this.usingDefaultToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
            this.usingDefaultToolStripMenuItem.Text = "Using Default";
            this.usingDefaultToolStripMenuItem.Click += new System.EventHandler(this.usingDefaultToolStripMenuItem_Click);
            // 
            // usingRedToolStripMenuItem
            // 
            this.usingRedToolStripMenuItem.Name = "usingRedToolStripMenuItem";
            this.usingRedToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
            this.usingRedToolStripMenuItem.Text = "Using Red";
            this.usingRedToolStripMenuItem.Click += new System.EventHandler(this.usingRedToolStripMenuItem_Click);
            // 
            // usingYellowToolStripMenuItem
            // 
            this.usingYellowToolStripMenuItem.Name = "usingYellowToolStripMenuItem";
            this.usingYellowToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
            this.usingYellowToolStripMenuItem.Text = "Using Yellow";
            this.usingYellowToolStripMenuItem.Click += new System.EventHandler(this.usingYellowToolStripMenuItem_Click);
            // 
            // usingGreenToolStripMenuItem
            // 
            this.usingGreenToolStripMenuItem.Name = "usingGreenToolStripMenuItem";
            this.usingGreenToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
            this.usingGreenToolStripMenuItem.Text = "Using Green";
            this.usingGreenToolStripMenuItem.Click += new System.EventHandler(this.usingGreenToolStripMenuItem_Click);
            // 
            // usingGrayToolStripMenuItem
            // 
            this.usingGrayToolStripMenuItem.Name = "usingGrayToolStripMenuItem";
            this.usingGrayToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
            this.usingGrayToolStripMenuItem.Text = "Using Gray";
            this.usingGrayToolStripMenuItem.Click += new System.EventHandler(this.usingGrayToolStripMenuItem_Click);
            // 
            // unmarkLineToolStripMenuItem
            // 
            this.unmarkLineToolStripMenuItem.Name = "unmarkLineToolStripMenuItem";
            this.unmarkLineToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.unmarkLineToolStripMenuItem.Text = "Unmark Line";
            this.unmarkLineToolStripMenuItem.Click += new System.EventHandler(this.unmarkLineToolStripMenuItem_Click);
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(164, 6);
            // 
            // markCharToolStripMenuItem
            // 
            this.markCharToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.usingYellowStyleToolStripMenuItem,
            this.usingRedStyleToolStripMenuItem,
            this.usingGreenStyleToolStripMenuItem,
            this.usingBlueStyleToolStripMenuItem,
            this.usingGrayStyleToolStripMenuItem});
            this.markCharToolStripMenuItem.Name = "markCharToolStripMenuItem";
            this.markCharToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.markCharToolStripMenuItem.Text = "Mark Selection";
            // 
            // usingYellowStyleToolStripMenuItem
            // 
            this.usingYellowStyleToolStripMenuItem.Name = "usingYellowStyleToolStripMenuItem";
            this.usingYellowStyleToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.usingYellowStyleToolStripMenuItem.Text = "Using Yellow Style";
            this.usingYellowStyleToolStripMenuItem.Click += new System.EventHandler(this.usingYellowStyleToolStripMenuItem_Click);
            // 
            // usingRedStyleToolStripMenuItem
            // 
            this.usingRedStyleToolStripMenuItem.Name = "usingRedStyleToolStripMenuItem";
            this.usingRedStyleToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.usingRedStyleToolStripMenuItem.Text = "Using Red Style";
            this.usingRedStyleToolStripMenuItem.Click += new System.EventHandler(this.usingRedStyleToolStripMenuItem_Click);
            // 
            // usingGreenStyleToolStripMenuItem
            // 
            this.usingGreenStyleToolStripMenuItem.Name = "usingGreenStyleToolStripMenuItem";
            this.usingGreenStyleToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.usingGreenStyleToolStripMenuItem.Text = "Using Green Style";
            this.usingGreenStyleToolStripMenuItem.Click += new System.EventHandler(this.usingGreenStyleToolStripMenuItem_Click);
            // 
            // usingBlueStyleToolStripMenuItem
            // 
            this.usingBlueStyleToolStripMenuItem.Name = "usingBlueStyleToolStripMenuItem";
            this.usingBlueStyleToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.usingBlueStyleToolStripMenuItem.Text = "Using Blue Style";
            this.usingBlueStyleToolStripMenuItem.Click += new System.EventHandler(this.usingBlueStyleToolStripMenuItem_Click);
            // 
            // usingGrayStyleToolStripMenuItem
            // 
            this.usingGrayStyleToolStripMenuItem.Name = "usingGrayStyleToolStripMenuItem";
            this.usingGrayStyleToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.usingGrayStyleToolStripMenuItem.Text = "Using Gray Style";
            this.usingGrayStyleToolStripMenuItem.Click += new System.EventHandler(this.usingGrayStyleToolStripMenuItem_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(152, 6);
            // 
            // autoCompleteToolStripMenuItem
            // 
            this.autoCompleteToolStripMenuItem.Name = "autoCompleteToolStripMenuItem";
            this.autoCompleteToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.autoCompleteToolStripMenuItem.Text = "Auto Complete";
            this.autoCompleteToolStripMenuItem.Click += new System.EventHandler(this.autoCompleteToolStripMenuItem_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(152, 6);
            // 
            // selectAllToolStripMenuItem
            // 
            this.selectAllToolStripMenuItem.Name = "selectAllToolStripMenuItem";
            this.selectAllToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.selectAllToolStripMenuItem.Text = "Select All ";
            this.selectAllToolStripMenuItem.Click += new System.EventHandler(this.selectAllToolStripMenuItem_Click);
            // 
            // TabContext
            // 
            this.TabContext.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.closeToolStripMenuItem,
            this.closeAllButThisToolStripMenuItem,
            this.closeAllToolStripMenuItem,
            this.toolStripSeparator3,
            this.copyFileNameToClipboardToolStripMenuItem,
            this.copyFilePathToClipboardToolStripMenuItem,
            this.toolStripSeparator4,
            this.openContainingFolderToolStripMenuItem});
            this.TabContext.Name = "TabContext";
            this.TabContext.Size = new System.Drawing.Size(242, 148);
            // 
            // closeToolStripMenuItem
            // 
            this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            this.closeToolStripMenuItem.Size = new System.Drawing.Size(241, 22);
            this.closeToolStripMenuItem.Text = "Close";
            this.closeToolStripMenuItem.Click += new System.EventHandler(this.closeToolStripMenuItem_Click);
            // 
            // closeAllButThisToolStripMenuItem
            // 
            this.closeAllButThisToolStripMenuItem.Name = "closeAllButThisToolStripMenuItem";
            this.closeAllButThisToolStripMenuItem.Size = new System.Drawing.Size(241, 22);
            this.closeAllButThisToolStripMenuItem.Text = "Close All But this";
            this.closeAllButThisToolStripMenuItem.Click += new System.EventHandler(this.closeAllButThisToolStripMenuItem_Click);
            // 
            // closeAllToolStripMenuItem
            // 
            this.closeAllToolStripMenuItem.Name = "closeAllToolStripMenuItem";
            this.closeAllToolStripMenuItem.Size = new System.Drawing.Size(241, 22);
            this.closeAllToolStripMenuItem.Text = "Close All";
            this.closeAllToolStripMenuItem.Click += new System.EventHandler(this.closeAllToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(238, 6);
            // 
            // copyFileNameToClipboardToolStripMenuItem
            // 
            this.copyFileNameToClipboardToolStripMenuItem.Name = "copyFileNameToClipboardToolStripMenuItem";
            this.copyFileNameToClipboardToolStripMenuItem.Size = new System.Drawing.Size(241, 22);
            this.copyFileNameToClipboardToolStripMenuItem.Text = "Copy File Name to Clipboard";
            this.copyFileNameToClipboardToolStripMenuItem.Click += new System.EventHandler(this.copyFileNameToClipboardToolStripMenuItem_Click);
            // 
            // copyFilePathToClipboardToolStripMenuItem
            // 
            this.copyFilePathToClipboardToolStripMenuItem.Name = "copyFilePathToClipboardToolStripMenuItem";
            this.copyFilePathToClipboardToolStripMenuItem.Size = new System.Drawing.Size(241, 22);
            this.copyFilePathToClipboardToolStripMenuItem.Text = "Copy Full File Path to Clipboard";
            this.copyFilePathToClipboardToolStripMenuItem.Click += new System.EventHandler(this.copyFilePathToClipboardToolStripMenuItem_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(238, 6);
            // 
            // openContainingFolderToolStripMenuItem
            // 
            this.openContainingFolderToolStripMenuItem.Name = "openContainingFolderToolStripMenuItem";
            this.openContainingFolderToolStripMenuItem.Size = new System.Drawing.Size(241, 22);
            this.openContainingFolderToolStripMenuItem.Text = "Open Containing Folder";
            this.openContainingFolderToolStripMenuItem.Click += new System.EventHandler(this.openContainingFolderToolStripMenuItem_Click);
            // 
            // unmarkSelectionToolStripMenuItem
            // 
            this.unmarkSelectionToolStripMenuItem.Name = "unmarkSelectionToolStripMenuItem";
            this.unmarkSelectionToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.unmarkSelectionToolStripMenuItem.Text = "Unmark Selection";
            this.unmarkSelectionToolStripMenuItem.Click += new System.EventHandler(this.unmarkSelectionToolStripMenuItem_Click);
            // 
            // Editor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(775, 469);
            this.Controls.Add(this.codebox);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "Editor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.TabPageContextMenuStrip = this.TabContext;
            this.Text = "New";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Editor_Closing);
            ((System.ComponentModel.ISupportInitialize)(this.codebox)).EndInit();
            this.EditorContext.ResumeLayout(false);
            this.TabContext.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        public FastColoredTextBoxNS.FastColoredTextBox codebox;
        private System.Windows.Forms.ContextMenuStrip EditorContext;
        private System.Windows.Forms.ContextMenuStrip TabContext;
        private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem undoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem redoToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem closeAllButThisToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closeAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem copyFileNameToClipboardToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyFilePathToClipboardToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem openContainingFolderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem findToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem replaceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem goToToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem foldingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem autoCompleteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem foldSelectedToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem unFoldSelectedToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem markToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem markLineToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem markCharToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripMenuItem selectAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem usingDefaultToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem usingRedToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem usingYellowToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem usingGreenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem usingGrayToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem unmarkLineToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        private System.Windows.Forms.ToolStripMenuItem usingYellowStyleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem usingRedStyleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem usingGreenStyleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem usingBlueStyleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem usingGrayStyleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem unmarkSelectionToolStripMenuItem;
    }
}