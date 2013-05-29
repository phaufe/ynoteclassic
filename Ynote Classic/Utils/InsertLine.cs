using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SS.Ynote.Classic.Utils
{
    public enum InsertType
    {
        Line,
        Column,
        Macro
    }
    public partial class InsertLine : Form
    {
        FastColoredTextBoxNS.FastColoredTextBox fctb;
        InsertType it;
        public InsertLine(FastColoredTextBoxNS.FastColoredTextBox tb, InsertType t)
        {
            InitializeComponent();
            it = t;
            fctb = tb;
            if (t == InsertType.Line)
            {
                this.Text = "Insert Lines";
                label1.Text = "Number of Lines To Insert :";
                button1.Text = "Insert";
            }
            else if (t == InsertType.Column)
            {
                this.Text = "Insert Column";
                label1.Text = "No. of Columns to Insert :";
                button1.Text = "Insert";
            }
            else if (t == InsertType.Macro)
            {
                this.Text = "Run Macro Multiple Times";
                label1.Text = "No. of Times to Run :";
                button1.Text = "Run";
            }

        }
        public int Lines
        {
            get { return this.numericTextBox1.IntValue; }
            set { this.numericTextBox1.Text = value.ToString(); }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (this.it == InsertType.Line)
            {
                while (Lines > 0)
                {
                    fctb.InsertText("\n\r");
                    Lines--;
                }
            }
            else if (this.it == InsertType.Column)
            {
                while (Lines > 0)
                {
                    fctb.InsertText(" ");
                    Lines--;
                }
            }
            else if (this.it == InsertType.Macro)
            {
                while (Lines > 0)
                {
                    fctb.MacrosManager.ExecuteMacros();
                    Lines--;
                }
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
