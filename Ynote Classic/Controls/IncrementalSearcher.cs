namespace FastColoredTextBoxNS
{
    using System;
    using System.Windows.Forms;
    using System.Text.RegularExpressions;
    using System.Collections.Generic;
    using System.Drawing;
    public partial class IncrementalSearcher : UserControl
    {
        bool firstSearch = true;
        Place startPlace;
        FastColoredTextBox tb;
        public IncrementalSearcher(FastColoredTextBox tb)
        {
            InitializeComponent();
            this.tb = tb;
        }
        public void FindNext(string pattern)
        {
            try
            {
                tbFind.BackColor = Color.White;
                RegexOptions opt = cbMatchCase.Checked ? RegexOptions.None : RegexOptions.IgnoreCase;
                if (!cbRegex.Checked)
                    pattern = Regex.Escape(pattern);
                if (cbWholeWord.Checked)
                    pattern = "\\b" + pattern + "\\b";
                //
                Range range = tb.Selection.Clone();
                range.Normalize();
                //
                if (firstSearch)
                {
                    startPlace = range.Start;
                    firstSearch = false;
                }
                //
                range.Start = range.End;
                if (range.Start >= startPlace)
                    range.End = new Place(tb.GetLineLength(tb.LinesCount - 1), tb.LinesCount - 1);
                else
                    range.End = startPlace;
                //
                foreach (var r in range.GetRangesByLines(pattern, opt))
                {
                    tb.Selection = r;
                    tb.DoSelectionVisible();
                    tb.Invalidate();
                    return;
                }
                //
                if (range.Start >= startPlace && startPlace > Place.Empty)
                {
                    tb.Selection.Start = new Place(0, 0);
                    FindNext(pattern);
                    return;
                }
                tbFind.BackColor = Color.LightCoral;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void tbFind_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                FindNext(tbFind.Text);
                
                e.Handled = true;
                return;
            }
            if (e.KeyChar == '\x1b')
            {
                Hide();
                e.Handled = true;
                return;
            }
        }

        private void FindForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Hide();
            }
            this.tb.Focus();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }


        void ResetSerach()
        {
            firstSearch = true;
        }

        private void cbMatchCase_CheckedChanged(object sender, EventArgs e)
        {
            ResetSerach();
        }

        private void tbFind_TextChanged(object sender, EventArgs e)
        {
            FindNext(tbFind.Text);
        }

        private void FindForm_Load(object sender, EventArgs e)
        {
            tbFind.Focus();
            ResetSerach();
        }
        private void button1_Click_1(object sender, EventArgs e)
        {
            FindNext(tbFind.Text);
        }
        private void button2_Click(object sender, EventArgs e)
        {
            this.Visible = false;
        }
    }
}
