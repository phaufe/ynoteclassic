using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Text;

namespace SS.Ynote.Classic.Components
{
    public partial class Clipboard : WeifenLuo.WinFormsUI.Docking.DockContent
    {
        FastColoredTextBoxNS.FastColoredTextBox f;
        public Clipboard(FastColoredTextBoxNS.FastColoredTextBox tb)
        {
            f = tb;
            InitializeComponent();
            if (File.Exists(@"Configurations/Clipboard.cl")) { }
            else { File.WriteAllText(@"Configurations/Clipboard.cl", System.Windows.Forms.Clipboard.GetText(), System.Text.Encoding.ASCII); }
            Bitmap bmp = SS.Ynote.Classic.Properties.Resources.page_paste;
            this.Icon = Icon.FromHandle(bmp.GetHicon());
            string LineText = File.ReadAllText(@"Configurations/Clipboard.cl", Encoding.ASCII);
            this.Click +=new EventHandler(Clipboard_Click);
            foreach (string line in File.ReadAllLines(@"Configurations/Clipboard.cl"))
             {
                 listBox1.Items.Add(line);
             }
            
  
            this.listBox1.DoubleClick += new EventHandler(listBox1_DoubleClick);
        }
        private void Clipboard_Click(object sender, EventArgs e){
            foreach (string line in File.ReadAllLines(@"Configurations/Clipboard.cl"))
            {
                listBox1.Items.Add(line);
            }
         }
        public void Insert(FastColoredTextBoxNS.FastColoredTextBox tb)
        {
            try { tb.InsertText(this.listBox1.SelectedItem.ToString()); }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            Insert(f);
        }
      

     
    }

}
