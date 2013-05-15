using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace SS.Ynote.Classic
{
    #region DarkTheme

    public class DarkThemeRenderer : ToolStripProfessionalRenderer
    {
      public DarkThemeRenderer()
		: base(new DarkTheme())
	{
	}
    }

    #endregion

}
