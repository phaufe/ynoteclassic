

namespace SS.Ynote.Classic
{
    #region Using
    using System.Text;
    using System.Windows.Forms;
    using System.Drawing;
    #endregion

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
