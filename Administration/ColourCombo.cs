using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Administration
{
    public class ColourCombo : ComboBox
    {
        public ColourCombo() :  base()
        {
            // set draw mode to OwnerDrawFixed to allow custom drawing of the combo-box items.
            this.DrawMode = DrawMode.OwnerDrawFixed;
            this.DrawItem += new DrawItemEventHandler(ColourCombo_DrawItem);
        }

        /// <summary>
        /// handle the DrawItem event to provide a custom draw method.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ColourCombo_DrawItem(object sender, DrawItemEventArgs e)
        {
            // draw the background in the specified background colour.
            e.DrawBackground();

            // draw the text:
            e.Graphics.DrawString(((ComboBox)sender).Items[e.Index].ToString(), e.Font, new SolidBrush(e.ForeColor), e.Bounds);
        }

        /// <summary>
        /// set the back colour to the currently selected colour, and set the fore colour to black or white, depending on the
        /// brightness of the selected colour.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnSelectedIndexChanged(EventArgs e)
        {
            base.OnSelectedIndexChanged(e);
            this.BackColor = this.SelectedColour;
            if (this.BackColor.GetBrightness() < 0.5)
                this.ForeColor = Color.White;
            else
                this.ForeColor = Color.Black;
        }

        /// <summary>
        /// override the OnDrawItem method to generate custom DrawItemEventArgs.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            // fetch the list-item to be drawn:
            var item = this.Items[e.Index];

            // is this a colour struct?
            if (item is Color)
            {
                // generate a new DrawItemEventArgs, with the selected colour as the back-colour.
                DrawItemEventArgs de = new DrawItemEventArgs(e.Graphics, e.Font, e.Bounds, e.Index, e.State, e.ForeColor, (Color)item);
                base.OnDrawItem(de);
            }
            else
                base.OnDrawItem(e);
        }

        /// <summary>
        /// load the standard ROYGIBV colour wheel into the colour combo.
        /// </summary>
        public void LoadRainbow()
        {
            this.Items.Add(Color.Red);
            this.Items.Add(Color.Orange);
            this.Items.Add(Color.Yellow);
            this.Items.Add(Color.Green);
            this.Items.Add(Color.Blue);
            this.Items.Add(Color.Indigo);
            this.Items.Add(Color.Violet);
        }

        /// <summary>
        /// load up a darker version of the ROYGBIV colour wheel.
        /// </summary>
        public void LoadDarkRainbow()
        {
            this.Items.Add(Color.DarkRed);
            this.Items.Add(Color.DarkOrange);
            this.Items.Add(Color.Brown);
            this.Items.Add(Color.DarkGreen);
            this.Items.Add(Color.DarkBlue);
            this.Items.Add(Color.DarkMagenta);
            this.Items.Add(Color.DarkViolet);
        }

        /// <summary>
        /// gets or sets the selected colour.
        /// </summary>
        public Color SelectedColour
        {
            get
            {
                if (SelectedItem != null && SelectedItem is Color)
                    return (Color)SelectedItem;
                else
                    return this.BackColor;
            }
            set
            {
                this.SelectedItem = value;
            }
        }
    }
}
