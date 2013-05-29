//Color Combobox.cs
//Copyright (C) 2013 Samarjeet Singh
namespace SS.Ynote.Classic
{
      #region Using Directives
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Windows.Forms;
    using System.Drawing;
    using System.Reflection;
    #endregion

    class ColorComboBox : ComboBox
    {
        #region Constructor

        public ColorComboBox()
        {
            FillColors();
            this.DrawMode = DrawMode.OwnerDrawFixed;
            this.DropDownStyle = ComboBoxStyle.DropDownList;
        }
        private void FillColors()
        {
            this.Items.Clear();
            foreach (Color color in typeof(Color).GetProperties(BindingFlags.Static | BindingFlags.Public).Where(c => c.PropertyType == typeof(Color)).Select(c => (Color)c.GetValue(c, null)))
            {
                this.Items.Add(color);
            }
        }
        #endregion

        #region Colouring

        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            try
            {
                if (e.Index >= 0)
                {
                    Color color = (Color)this.Items[e.Index];

                    int nextX = 0;

                    e.Graphics.FillRectangle(new SolidBrush(e.BackColor), e.Bounds);
                    DrawColor(e, color, ref nextX);
                    DrawText(e, color, nextX);
                }
                else
                    base.OnDrawItem(e);
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
        }

        private void DrawColor(DrawItemEventArgs e, Color color, ref int nextX)
        {
            int width = e.Bounds.Height * 2 - 8;
            Rectangle rectangle = new Rectangle(e.Bounds.X + 3, e.Bounds.Y + 3, width, e.Bounds.Height - 6);
            e.Graphics.FillRectangle(new SolidBrush(color), rectangle);

            nextX = width + 8;
        }
        private void DrawText(DrawItemEventArgs e, Color color, int nextX)
        {
            e.Graphics.DrawString(color.Name, e.Font, new SolidBrush(e.ForeColor), new PointF(nextX, e.Bounds.Y + (e.Bounds.Height - e.Font.Height) / 2));
        }
        #endregion

        #region Properties

        public Color Color
        {
            get
            {
                if (this.SelectedItem != null)
                    return (Color)this.SelectedItem;

                return Color.Black;
            }
            set
            {
                int ix = this.Items.IndexOf(value);
                if (ix >= 0)
                    this.SelectedIndex = ix;
            }
        }
        #endregion
    }
}
