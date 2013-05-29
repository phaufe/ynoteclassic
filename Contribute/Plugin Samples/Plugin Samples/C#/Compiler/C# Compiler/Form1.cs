


using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using Microsoft.CSharp;
using System.CodeDom.Compiler;
using System.Reflection;
using System.Diagnostics;
using SS.Ynote.Engine.Framework.Plugins.Interface;
using System.Xml.Linq;

//-------------------------------------------------
//
//Simple C# Compiler Plugin
//
//Make a Compiler Plugin For Any languge using Process Info
//
//--------------------------------------------
namespace CSCompiler
{
	public class Form1 : WeifenLuo.WinFormsUI.Docking.DockContent, IFormPlugin
    {
        #region Variables

        private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox appName;
		private System.Windows.Forms.TextBox mainClass;
        private System.Windows.Forms.CheckBox includeDebug;
        private IContainer components;
        #endregion

        #region Constructor
        public Form1()
		{
			InitializeComponent();


        }

        protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
        }
        #endregion

        #region Windows Form Designer generated code
        /// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.button1 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.appName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.mainClass = new System.Windows.Forms.TextBox();
            this.includeDebug = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.SystemColors.Control;
            this.button1.Location = new System.Drawing.Point(114, 179);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(160, 24);
            this.button1.TabIndex = 1;
            this.button1.Text = "&Compile and Execute";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(31, 65);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(104, 23);
            this.label2.TabIndex = 5;
            this.label2.Text = "Main Class Name";
            // 
            // appName
            // 
            this.appName.Location = new System.Drawing.Point(137, 26);
            this.appName.Name = "appName";
            this.appName.Size = new System.Drawing.Size(152, 20);
            this.appName.TabIndex = 2;
            this.appName.Text = "Application.exe";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(31, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 23);
            this.label1.TabIndex = 4;
            this.label1.Text = "OutputFileName";
            // 
            // mainClass
            // 
            this.mainClass.Location = new System.Drawing.Point(137, 68);
            this.mainClass.Name = "mainClass";
            this.mainClass.Size = new System.Drawing.Size(152, 20);
            this.mainClass.TabIndex = 3;
            this.mainClass.Text = "SS.Ynote.Classic.Main";
            // 
            // includeDebug
            // 
            this.includeDebug.Location = new System.Drawing.Point(34, 111);
            this.includeDebug.Name = "includeDebug";
            this.includeDebug.Size = new System.Drawing.Size(160, 24);
            this.includeDebug.TabIndex = 7;
            this.includeDebug.Text = "Include Debug Info";
            // 
            // Form1
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(316, 233);
            this.Controls.Add(this.includeDebug);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.mainClass);
            this.Controls.Add(this.appName);
            this.Controls.Add(this.button1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "C# Compiler Plugin";
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

        #region Plugin Members
        public string Title
        {
            get { return "C# Compiler Plugin"; }
        }
        public string Description
        {
            get { return "Sample Compiler Plugin"; }
        }
        public string Group
        {
            get { return "Plugins"; }
        }
        public string SubGroup
        {
            get { return "Samples"; }
        }

        private XElement configuration = new XElement("HighlighterPluginConfig");
        public XElement Configuration
        {
            get { return configuration; }
            set { configuration = value; }
        }

        public string Icon
        {
            //eg : C:\\PluginIcon.ico
            get { return string.Empty; }
        }
        #region IFormPlugin Members

        public WeifenLuo.WinFormsUI.Docking.DockContent Content
        {
            get { return this; }
        }
        public ShowAs ShowAs
        {
            get { return ShowAs.Dialog; }
        }
        #endregion

        #endregion

        private void menuItem4_Click(object sender, System.EventArgs e)
		{
			Dispose();
			Application.Exit();
		}

		private void menuItem3_Click(object sender, System.EventArgs e)
		{
			System.Windows.Forms.MessageBox.Show(this, "CSharp sample compiler :)", "CodeProject Rulez");
		}

		private void button1_Click(object sender, System.EventArgs e)
		{
			CSharpCodeProvider codeProvider = new CSharpCodeProvider();

			// For Visual Basic Compiler 
			//Microsoft.VisualBasic.VBCodeProvider

			ICodeCompiler compiler = codeProvider.CreateCompiler();
			CompilerParameters parameters = new CompilerParameters();

			parameters.GenerateExecutable = true;
			if (appName.Text == "")	
			{
				System.Windows.Forms.MessageBox.Show(this, "Application name cannot be empty");
				return ;
			}

			parameters.OutputAssembly = appName.Text.ToString();

			if (mainClass.Text.ToString() == "")
			{
				System.Windows.Forms.MessageBox.Show(this, "Main Class Name cannot be empty");
				return ;
			}

			parameters.MainClass = mainClass.Text.ToString();

			parameters.IncludeDebugInformation = includeDebug.Checked;

			foreach (Assembly asm in AppDomain.CurrentDomain.GetAssemblies()) 
			{
				parameters.ReferencedAssemblies.Add(asm.Location);
			}

            String code = SS.Ynote.Classic.Main.ActiveFastColoredTextBox.Text;

			CompilerResults results = compiler.CompileAssemblyFromSource(parameters, code);
			
			if (results.Errors.Count > 0) 
			{
				string errors = "Compilation failed:\n";
				foreach (CompilerError err in results.Errors) 
				{
					errors += err.ToString() + "\n";
				}
				System.Windows.Forms.MessageBox.Show(this, errors, "There were compilation errors");
			}
			else	
			{
				#region Executing generated executable
				// try to execute application

				try 
				{
					if (!System.IO.File.Exists(appName.Text.ToString())) 
					{
						MessageBox.Show(String.Format("Can't find {0}", appName), "Can't execute.", MessageBoxButtons.OK, MessageBoxIcon.Error);
						return;
					}
					ProcessStartInfo pInfo = new ProcessStartInfo(appName.Text.ToString());
					Process.Start(pInfo);
				} 
				catch (Exception ex) 
				{
					MessageBox.Show(String.Format("Error while executing {0}", appName) + ex.ToString(),
							"Can't execute.", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}

				#endregion
				
			}
			
		}
	}
}
