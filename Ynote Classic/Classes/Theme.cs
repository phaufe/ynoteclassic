

namespace SS.Ynote.Classic
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Drawing;
    using System.Windows.Forms;

 class OfficeXPRenderer : ToolStripProfessionalRenderer{
     public OfficeXPRenderer()
         :base(new OfficeXP())
     {
     }
     }
 class VSRenderer : ProfessionalColorTable
 {
     public override Color ButtonSelectedHighlight
     {
         get { return ButtonSelectedGradientMiddle; }
     }
     public override Color ButtonSelectedHighlightBorder
     {
         get { return ButtonSelectedBorder; }
     }
     public override Color ButtonPressedHighlight
     {
         get { return ButtonPressedGradientMiddle; }
     }
     public override Color ButtonPressedHighlightBorder
     {
         get { return ButtonPressedBorder; }
     }
     public override Color ButtonCheckedHighlight
     {
         get { return ButtonCheckedGradientMiddle; }
     }
     public override Color ButtonCheckedHighlightBorder
     {
         get { return ButtonSelectedBorder; }
     }
     public override Color ButtonPressedBorder
     {
         get { return ButtonSelectedBorder; }
     }
     public override Color ButtonSelectedBorder
     {
         get { return Color.FromArgb(255, 239, 239, 242); }
     }
     public override Color ButtonCheckedGradientBegin
     {
         get { return Color.FromArgb(255, 254, 254, 254); }
     }
     public override Color ButtonCheckedGradientMiddle
     {
         get { return Color.FromArgb(255, 254, 254, 254); }
     }
     public override Color ButtonCheckedGradientEnd
     {
         get { return Color.FromArgb(255, 254, 254, 254); }
     }
     public override Color ButtonSelectedGradientBegin
     {
         get { return Color.FromArgb(255, 254, 254, 254); }
     }
     public override Color ButtonSelectedGradientMiddle
     {
         get { return Color.FromArgb(255, 254, 254, 254); }
     }
     public override Color ButtonSelectedGradientEnd
     {
         get { return Color.FromArgb(255, 254, 254, 254); }
     }
     public override Color ButtonPressedGradientBegin
     {
         get { return Color.FromArgb(255, 32, 172, 232); }
     }
     public override Color ButtonPressedGradientMiddle
     {
         get { return Color.FromArgb(255, 32, 172, 232); }
     }
     public override Color ButtonPressedGradientEnd
     {
         get { return Color.FromArgb(255, 32, 172, 232); }
     }
     public override Color CheckBackground
     {
         get { return Color.FromArgb(255, 254, 254, 254); }
     }
     public override Color CheckSelectedBackground
     {
         get { return Color.FromArgb(255, 254, 254, 254); }
     }
     public override Color CheckPressedBackground
     {
         get { return Color.FromArgb(255, 32, 172, 232); }
     }
     public override Color GripDark
     {
         get { return Color.FromArgb(255, 221, 226, 236); }
     }
     public override Color GripLight
     {
         get { return Color.FromArgb(255, 204, 204, 219); }
     }
     public override Color ImageMarginGradientBegin
     {
         get { return Color.FromArgb(255, 231, 232, 236); }
     }
     public override Color ImageMarginGradientMiddle
     {
         get { return Color.FromArgb(255, 231, 232, 236); }
     }
     public override Color ImageMarginGradientEnd
     {
         get { return Color.FromArgb(255, 231, 232, 236); }
     }
     public override Color ImageMarginRevealedGradientBegin
     {
         get { return Color.FromArgb(255, 231, 232, 236); }
     }
     public override Color ImageMarginRevealedGradientMiddle
     {
         get { return Color.FromArgb(255, 231, 232, 236); }
     }
     public override Color ImageMarginRevealedGradientEnd
     {
         get { return Color.FromArgb(255, 231, 232, 236); }
     }
     public override Color MenuStripGradientBegin
     {
         get { return Color.FromArgb(255, 239, 239, 242); }
     }
     public override Color MenuStripGradientEnd
     {
         get { return Color.FromArgb(255, 239, 239, 242); }
     }
     public override Color MenuItemSelected
     {
         get { return Color.FromArgb(255, 248, 249, 250); }
     }
     public override Color MenuItemBorder
     {
         get { return Color.FromArgb(255, 231, 232, 236); }
     }
     public override Color MenuBorder
     {
         get { return Color.FromArgb(255, 204, 206, 219); }
     }
     public override Color MenuItemSelectedGradientBegin
     {
         get { return Color.FromArgb(255, 254, 254, 254); }
     }
     public override Color MenuItemSelectedGradientEnd
     {
         get { return Color.FromArgb(255, 254, 254, 254); }
     }
     public override Color MenuItemPressedGradientBegin
     {
         get { return Color.FromArgb(255, 231, 232, 236); }
     }
     public override Color MenuItemPressedGradientMiddle
     {
         get { return Color.FromArgb(255, 231, 232, 236); }
     }
     public override Color MenuItemPressedGradientEnd
     {
         get { return Color.FromArgb(255, 231, 232, 236); }
     }
     public override Color RaftingContainerGradientBegin
     {
         get { return Color.FromArgb(255, 186, 192, 201); }
     }
     public override Color RaftingContainerGradientEnd
     {
         get { return Color.FromArgb(255, 186, 192, 201); }
     }
     public override Color SeparatorDark
     {
         get { return Color.FromArgb(255, 204, 206, 219); }
     }
     public override Color SeparatorLight
     {
         get { return Color.FromArgb(255, 246, 246, 246); }
     }
     public override Color StatusStripGradientBegin
     {
         get { return Color.FromArgb(255, 234, 234, 234); }
     }
     public override Color StatusStripGradientEnd
     {
         get { return Color.FromArgb(255, 209, 209, 209); }
     }
     public override Color ToolStripBorder
     {
         get { return Color.FromArgb(0, 0, 0, 0); }
     }
     public override Color ToolStripDropDownBackground
     {
         get { return Color.FromArgb(255, 231, 232, 236); }
     }
     public override Color ToolStripGradientBegin
     {
         get { return Color.FromArgb(255, 239, 239, 242); }
     }
     public override Color ToolStripGradientMiddle
     {
         get { return Color.FromArgb(255, 239, 239, 242); }
     }
     public override Color ToolStripGradientEnd
     {
         get { return Color.FromArgb(255, 239, 239, 242); }
     }
     public override Color ToolStripContentPanelGradientBegin
     {
         get { return Color.FromArgb(255, 239, 239, 242); }
     }
     public override Color ToolStripContentPanelGradientEnd
     {
         get { return Color.FromArgb(255, 239, 239, 242); }
     }
     public override Color ToolStripPanelGradientBegin
     {
         get { return Color.FromArgb(255, 239, 239, 242); }
     }
     public override Color ToolStripPanelGradientEnd
     {
         get { return Color.FromArgb(255, 239, 239, 242); }
     }
     public override Color OverflowButtonGradientBegin
     {
         get { return Color.FromArgb(255, 239, 239, 242); }
     }
     public override Color OverflowButtonGradientMiddle
     {
         get { return Color.FromArgb(255, 239, 239, 242); }
     }
     public override Color OverflowButtonGradientEnd
     {
         get { return Color.FromArgb(255, 239, 239, 242); }
     }

 }
 class VS2010Renderer : ToolStripProfessionalRenderer 
 {
     public VS2010Renderer()
         :base(new VSRenderer())
     {
     }
    
 }
    ///<summary>
    ///Dark Theme
    ///</summary>
    class DarkTheme : ProfessionalColorTable
    {
        public override Color ButtonSelectedHighlight
        {
            get { return ButtonSelectedGradientMiddle; }
        }
        public override Color ButtonSelectedHighlightBorder
        {
            get { return ButtonSelectedBorder; }
        }
        public override Color ButtonPressedHighlight
        {
            get { return ButtonPressedGradientMiddle; }
        }
        public override Color ButtonPressedHighlightBorder
        {
            get { return ButtonPressedBorder; }
        }
        public override Color ButtonCheckedHighlight
        {
            get { return ButtonCheckedGradientMiddle; }
        }
        public override Color ButtonCheckedHighlightBorder
        {
            get { return ButtonSelectedBorder; }
        }
        public override Color ButtonPressedBorder
        {
            get { return ButtonSelectedBorder; }
        }
        public override Color ButtonSelectedBorder
        {
            get { return Color.FromArgb(255, 98, 98, 98); }
        }
        public override Color ButtonCheckedGradientBegin
        {
            get { return Color.FromArgb(255, 144, 144, 144); }
        }
        public override Color ButtonCheckedGradientMiddle
        {
            get { return Color.FromArgb(255, 170, 170, 170); }
        }
        public override Color ButtonCheckedGradientEnd
        {
            get { return Color.FromArgb(255, 170, 170, 170); }
        }
        public override Color ButtonSelectedGradientBegin
        {
            get { return Color.FromArgb(255, 170, 170, 170); }
        }
        public override Color ButtonSelectedGradientMiddle
        {
            get { return Color.FromArgb(255, 170, 170, 170); }
        }
        public override Color ButtonSelectedGradientEnd
        {
            get { return Color.FromArgb(255, 170, 170, 170); }
        }
        public override Color ButtonPressedGradientBegin
        {
            get { return Color.FromArgb(255, 170, 170, 170); }
        }
        public override Color ButtonPressedGradientMiddle
        {
            get { return Color.FromArgb(255, 170, 170, 170); }
        }
        public override Color ButtonPressedGradientEnd
        {
            get { return Color.FromArgb(255, 170, 170, 170); }
        }
        public override Color CheckBackground
        {
            get { return Color.FromArgb(255, 173, 173, 173); }
        }
        public override Color CheckSelectedBackground
        {
            get { return Color.FromArgb(255, 173, 173, 173); }
        }
        public override Color CheckPressedBackground
        {
            get { return Color.FromArgb(255, 140, 140, 140); }
        }
        public override Color GripDark
        {
            get { return Color.FromArgb(255, 22, 22, 22); }
        }
        public override Color GripLight
        {
            get { return Color.FromArgb(255, 83, 83, 83); }
        }
        public override Color ImageMarginGradientBegin
        {
            get { return Color.FromArgb(255, 85, 85, 85); }
        }
        public override Color ImageMarginGradientMiddle
        {
            get { return Color.FromArgb(255, 68, 68, 68); }
        }
        public override Color ImageMarginGradientEnd
        {
            get { return Color.FromArgb(255, 68, 68, 68); }
        }
        public override Color ImageMarginRevealedGradientBegin
        {
            get { return Color.FromArgb(255, 68, 68, 68); }
        }
        public override Color ImageMarginRevealedGradientMiddle
        {
            get { return Color.FromArgb(255, 68, 68, 68); }
        }
        public override Color ImageMarginRevealedGradientEnd
        {
            get { return Color.FromArgb(255, 68, 68, 68); }
        }
        public override Color MenuStripGradientBegin
        {
            get { return Color.FromArgb(255, 138, 138, 138); }
        }
        public override Color MenuStripGradientEnd
        {
            get { return Color.FromArgb(255, 103, 103, 103); }
        }
        public override Color MenuItemSelected
        {
            get { return Color.FromArgb(255, 170, 170, 170); }
        }
        public override Color MenuItemBorder
        {
            get { return Color.FromArgb(255, 170, 170, 170); }
        }
        public override Color MenuBorder
        {
            get { return Color.FromArgb(255, 22, 22, 22); }
        }
        public override Color MenuItemSelectedGradientBegin
        {
            get { return Color.FromArgb(255, 170, 170, 170); }
        }
        public override Color MenuItemSelectedGradientEnd
        {
            get { return Color.FromArgb(255, 170, 170, 170); }
        }
        public override Color MenuItemPressedGradientBegin
        {
            get { return Color.FromArgb(255, 125, 125, 125); }
        }
        public override Color MenuItemPressedGradientMiddle
        {
            get { return Color.FromArgb(255, 125, 125, 125); }
        }
        public override Color MenuItemPressedGradientEnd
        {
            get { return Color.FromArgb(255, 125, 125, 125); }
        }
        public override Color RaftingContainerGradientBegin
        {
            get { return Color.FromArgb(255, 170, 170, 170); }
        }
        public override Color RaftingContainerGradientEnd
        {
            get { return Color.FromArgb(255, 170, 170, 170); }
        }
        public override Color SeparatorDark
        {
            get { return Color.FromArgb(255, 22, 22, 22); }
        }
        public override Color SeparatorLight
        {
            get { return Color.FromArgb(255, 62, 62, 62); }
        }
        public override Color StatusStripGradientBegin
        {
            get { return Color.FromArgb(255, 112, 112, 112); }
        }
        public override Color StatusStripGradientEnd
        {
            get { return Color.FromArgb(255, 97, 97, 97); }
        }
        public override Color ToolStripBorder
        {
            get { return Color.FromArgb(255, 22, 22, 22); }
        }
        public override Color ToolStripDropDownBackground
        {
            get { return Color.FromArgb(255, 125, 125, 125); }
        }
        public override Color ToolStripGradientBegin
        {
            get { return Color.FromName("DimGray"); }
        }
        public override Color ToolStripGradientMiddle
        {
            get { return Color.FromArgb(255, 89, 89, 89); }
        }
        public override Color ToolStripGradientEnd
        {
            get { return Color.FromArgb(255, 88, 88, 88); }
        }
        public override Color ToolStripContentPanelGradientBegin
        {
            get { return Color.FromArgb(255, 68, 68, 68); }
        }
        public override Color ToolStripContentPanelGradientEnd
        {
            get { return Color.FromArgb(255, 68, 68, 68); }
        }
        public override Color ToolStripPanelGradientBegin
        {
            get { return Color.FromArgb(255, 103, 103, 103); }
        }
        public override Color ToolStripPanelGradientEnd
        {
            get { return Color.FromArgb(255, 103, 103, 103); }
        }
        public override Color OverflowButtonGradientBegin
        {
            get { return Color.FromArgb(255, 103, 103, 103); }
        }
        public override Color OverflowButtonGradientMiddle
        {
            get { return Color.FromArgb(255, 103, 103, 103); }
        }
        public override Color OverflowButtonGradientEnd
        {
            get { return Color.FromArgb(255, 79, 79, 79); }
        }
    }

    /// <summary>
    /// Office XP Renderer
    /// </summary>
    public class OfficeXP : ProfessionalColorTable
    {
        public override Color ButtonSelectedHighlight
        {
            get { return ButtonSelectedGradientMiddle; }
        }
        public override Color ButtonSelectedHighlightBorder
        {
            get { return ButtonSelectedBorder; }
        }
        public override Color ButtonPressedHighlight
        {
            get { return ButtonPressedGradientMiddle; }
        }
        public override Color ButtonPressedHighlightBorder
        {
            get { return ButtonPressedBorder; }
        }
        public override Color ButtonCheckedHighlight
        {
            get { return ButtonCheckedGradientMiddle; }
        }
        public override Color ButtonCheckedHighlightBorder
        {
            get { return ButtonSelectedBorder; }
        }
        public override Color ButtonPressedBorder
        {
            get { return ButtonSelectedBorder; }
        }
        public override Color ButtonSelectedBorder
        {
            get { return Color.FromArgb(255, 51, 94, 168); }
        }
        public override Color ButtonCheckedGradientBegin
        {
            get { return Color.FromArgb(255, 226, 229, 238); }
        }
        public override Color ButtonCheckedGradientMiddle
        {
            get { return Color.FromArgb(255, 226, 229, 238); }
        }
        public override Color ButtonCheckedGradientEnd
        {
            get { return Color.FromArgb(255, 226, 229, 238); }
        }
        public override Color ButtonSelectedGradientBegin
        {
            get { return Color.FromArgb(255, 194, 207, 229); }
        }
        public override Color ButtonSelectedGradientMiddle
        {
            get { return Color.FromArgb(255, 194, 207, 229); }
        }
        public override Color ButtonSelectedGradientEnd
        {
            get { return Color.FromArgb(255, 194, 207, 229); }
        }
        public override Color ButtonPressedGradientBegin
        {
            get { return Color.FromArgb(255, 153, 175, 212); }
        }
        public override Color ButtonPressedGradientMiddle
        {
            get { return Color.FromArgb(255, 153, 175, 212); }
        }
        public override Color ButtonPressedGradientEnd
        {
            get { return Color.FromArgb(255, 153, 175, 212); }
        }
        public override Color CheckBackground
        {
            get { return Color.FromArgb(255, 226, 229, 238); }
        }
        public override Color CheckSelectedBackground
        {
            get { return Color.FromArgb(255, 51, 94, 168); }
        }
        public override Color CheckPressedBackground
        {
            get { return Color.FromArgb(255, 51, 94, 168); }
        }
        public override Color GripDark
        {
            get { return Color.FromArgb(255, 189, 188, 191); }
        }
        public override Color GripLight
        {
            get { return Color.FromArgb(255, 255, 255, 255); }
        }
        public override Color ImageMarginGradientBegin
        {
            get { return Color.FromArgb(255, 252, 252, 252); }
        }
        public override Color ImageMarginGradientMiddle
        {
            get { return Color.FromArgb(255, 245, 244, 246); }
        }
        public override Color ImageMarginGradientEnd
        {
            get { return Color.FromArgb(255, 235, 233, 237); }
        }
        public override Color ImageMarginRevealedGradientBegin
        {
            get { return Color.FromArgb(255, 247, 246, 248); }
        }
        public override Color ImageMarginRevealedGradientMiddle
        {
            get { return Color.FromArgb(255, 241, 240, 242); }
        }
        public override Color ImageMarginRevealedGradientEnd
        {
            get { return Color.FromArgb(255, 228, 226, 230); }
        }
        public override Color MenuStripGradientBegin
        {
            get { return Color.FromArgb(255, 235, 233, 237); }
        }
        public override Color MenuStripGradientEnd
        {
            get { return Color.FromArgb(255, 251, 250, 251); }
        }
        public override Color MenuItemSelected
        {
            get { return Color.FromArgb(255, 194, 207, 229); }
        }
        public override Color MenuItemBorder
        {
            get { return Color.FromArgb(255, 51, 94, 168); }
        }
        public override Color MenuBorder
        {
            get { return Color.FromArgb(255, 134, 133, 136); }
        }
        public override Color MenuItemSelectedGradientBegin
        {
            get { return Color.FromArgb(255, 194, 207, 229); }
        }
        public override Color MenuItemSelectedGradientEnd
        {
            get { return Color.FromArgb(255, 194, 207, 229); }
        }
        public override Color MenuItemPressedGradientBegin
        {
            get { return Color.FromArgb(255, 252, 252, 252); }
        }
        public override Color MenuItemPressedGradientMiddle
        {
            get { return Color.FromArgb(255, 241, 240, 242); }
        }
        public override Color MenuItemPressedGradientEnd
        {
            get { return Color.FromArgb(255, 245, 244, 246); }
        }
        public override Color RaftingContainerGradientBegin
        {
            get { return Color.FromArgb(255, 235, 233, 237); }
        }
        public override Color RaftingContainerGradientEnd
        {
            get { return Color.FromArgb(255, 251, 250, 251); }
        }
        public override Color SeparatorDark
        {
            get { return Color.FromArgb(255, 193, 193, 196); }
        }
        public override Color SeparatorLight
        {
            get { return Color.FromArgb(255, 255, 255, 255); }
        }
        public override Color StatusStripGradientBegin
        {
            get { return Color.FromArgb(255, 235, 233, 237); }
        }
        public override Color StatusStripGradientEnd
        {
            get { return Color.FromArgb(255, 251, 250, 251); }
        }
        public override Color ToolStripBorder
        {
            get { return Color.FromArgb(255, 238, 237, 240); }
        }
        public override Color ToolStripDropDownBackground
        {
            get { return Color.FromArgb(255, 252, 252, 252); }
        }
        public override Color ToolStripGradientBegin
        {
            get { return Color.FromArgb(255, 252, 252, 252); }
        }
        public override Color ToolStripGradientMiddle
        {
            get { return Color.FromArgb(255, 245, 244, 246); }
        }
        public override Color ToolStripGradientEnd
        {
            get { return Color.FromArgb(255, 235, 233, 237); }
        }
        public override Color ToolStripContentPanelGradientBegin
        {
            get { return Color.FromArgb(255, 235, 233, 237); }
        }
        public override Color ToolStripContentPanelGradientEnd
        {
            get { return Color.FromArgb(255, 251, 250, 251); }
        }
        public override Color ToolStripPanelGradientBegin
        {
            get { return Color.FromArgb(255, 235, 233, 237); }
        }
        public override Color ToolStripPanelGradientEnd
        {
            get { return Color.FromArgb(255, 251, 250, 251); }
        }
        public override Color OverflowButtonGradientBegin
        {
            get { return Color.FromArgb(255, 242, 242, 242); }
        }
        public override Color OverflowButtonGradientMiddle
        {
            get { return Color.FromArgb(255, 224, 224, 225); }
        }
        public override Color OverflowButtonGradientEnd
        {
            get { return Color.FromArgb(255, 167, 166, 170); }
        }


        /// <summary>
        /// Visual Studio 2010 Style Renderer
        /// </summary>
        class vS2012 : ProfessionalColorTable
        {
           
        }

    }
}
