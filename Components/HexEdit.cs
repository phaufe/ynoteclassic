using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SS.Ynote.Classic
{
    public partial class HexEdit : Form
    {
        public HexEdit()
        {
            InitializeComponent();
            Bitmap bmp = SS.Ynote.Classic.Properties.Resources.computer_edit;
            this.Icon = Icon.FromHandle(bmp.GetHicon());
        }
    }
}
