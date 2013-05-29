
namespace SS.Ynote.Classic
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using System.Windows.Forms;
    using System.Diagnostics;
    using System.Drawing;

    class RunDialog : Form
    {

        #region Designer
        private Label label1;
        private Button button1;
        private GroupBox groupBox1;
        private Button button2;
        private TextBox pgname;
        private Button button3;
        private void InitializeComponent()  {
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.pgname = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Program : ";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(301, 32);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(27, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "...";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.pgname);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(336, 100);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Select ";
            // 
            // pgname
            // 
            this.pgname.Location = new System.Drawing.Point(77, 34);
            this.pgname.Name = "pgname";
            this.pgname.Size = new System.Drawing.Size(208, 20);
            this.pgname.TabIndex = 3;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(202, 131);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(82, 22);
            this.button2.TabIndex = 4;
            this.button2.Text = "Run";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(287, 131);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(82, 22);
            this.button3.TabIndex = 5;
            this.button3.Text = "Cancel";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // RunDialog
            // 
            this.ClientSize = new System.Drawing.Size(381, 167);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "RunDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Run";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }
        #endregion

        #region Constructor
        bool Started = false;
        string AppDataDir = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/SS/Ynote/" ;
        public RunDialog() {
              InitializeComponent();
              Bitmap bmp = SS.Ynote.Classic.Properties.Resources.script_code;
              this.Icon = Icon.FromHandle(bmp.GetHicon());
             if (Directory.Exists(AppDataDir)) { }
             else { Directory.CreateDirectory(AppDataDir); }
        }
        #endregion
         
        public bool Running
        {
            get {return Started;}
            set {Started = value;}
        }
        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog o = new OpenFileDialog();
            o.Filter = "Programs (*.exe) ,(*.cmd),(*.bat)|*.exe;*.bat;*.cmd";
            o.ShowDialog();
            if(!string.IsNullOrEmpty(o.FileName))
             pgname.Text = o.FileName;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Started = true;
            Start();
            Close();
        }
       
       public void Start()
        {
            Process.Start(pgname.Text, AppDataDir + "app.run");
        }
    }
}
