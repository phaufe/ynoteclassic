using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace SS.Ynote.Classic
{
    public partial class About : Form
    {
        public About()
        {
            InitializeComponent();
            this.Focus();
            this.textBox1.ScrollBars = ScrollBars.Vertical;
   
            this.textBox1.ReadOnly = true;
            this.Select();
            try
            {

                this.textBox1.Text = System.IO.File.ReadAllText("License.txt");
            }
            catch (Exception ex)
            {
            }
        }

        private void About_Load(object sender, EventArgs e)
        {
            this.Focus();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Close();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("http://sscorps.webs.com");
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("http://sscorps.webs.com/apps/auth/login?try=1&next=");
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("http://www.facebook.com/sscorpscom");
        }

        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("http://sscorps.webs.com/Downloads");
        }
    }
}
