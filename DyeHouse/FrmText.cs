using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DyeHouse
{
    public partial class FrmText : Form
    {
        public class ComboBoxCustom : ComboBox
        {
            public ComboBoxCustom()
            {
                this.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            }

            protected override void OnDrawItem(DrawItemEventArgs e)
            {
                base.OnDrawItem(e);
                if (e.Index < 0) { return; }
                e.DrawBackground();
                ComboBoxItem item = (ComboBoxItem)this.Items[e.Index];
                Brush brush = new SolidBrush(item.ForeColor);
                if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
                { brush = Brushes.Yellow; }
                e.Graphics.DrawString(item.Text,
                    this.Font, brush, e.Bounds.X, e.Bounds.Y);
            }
            object selectedValue = null;
            public new Object SelectedValue
            {
                get
                {
                    object ret = null;
                    if (this.SelectedIndex >= 0)
                    {
                        ret = ((ComboBoxItem)this.SelectedItem).Value;
                    }
                    return ret;
                }
                set { selectedValue = value; }
            }
            string selectedText = "";
            public new String SelectedText
            {
                get
                {
                    return ((ComboBoxItem)this.SelectedItem).Text;
                }
                set { selectedText = value; }
            }
        }

        public class ComboBoxItem
        {
            public ComboBoxItem() { }

            public ComboBoxItem(string pText, object pValue)
            {
                text = pText; val = pValue;
            }

            public ComboBoxItem(string pText, object pValue, Color pColor)
            {
                text = pText; val = pValue; foreColor = pColor;
            }

            string text = "";
            public string Text
            {
                get { return text; }
                set { text = value; }
            }

            object val;
            public object Value
            {
                get { return val; }
                set { val = value; }
            }

            Color foreColor = Color.Black;
            public Color ForeColor
            {
                get { return foreColor; }
                set { foreColor = value; }
            }

            public override string ToString()
            {
                return text;
            }
        }


        public FrmText()
        {
            InitializeComponent();
        }

        private void FrmText_Load(object sender, EventArgs e)
        {
            ComboBoxCustom cus = new ComboBoxCustom();

            comboBox1.Items.Add(new ComboBoxItem("México", "0", Color.Green));
            comboBox1.Items.Add(new ComboBoxItem("USA", "1", Color.Blue));
            comboBox1.Items.Add(new ComboBoxItem("China", "2", Color.Red));

            

        }

    }
}
