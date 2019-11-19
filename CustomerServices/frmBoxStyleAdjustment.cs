using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Utilities;

namespace CustomerServices
{
    public partial class frmBoxStyleAdjustment : Form
    {
        TLCSV_StockOnHand oStockOH;
        bool FormLoaded;

        public frmBoxStyleAdjustment()
        {
            InitializeComponent();
        }

        private void frmBoxStyleAdjustment_Load(object sender, EventArgs e)
        {
            FormLoaded = false;
            using (var context = new TTI2Entities())
            {
                cmboStyle.DataSource = context.TLADM_Styles.OrderBy(x => x.Sty_Description).ToList();
                cmboStyle.ValueMember = "Sty_Id";
                cmboStyle.DisplayMember = "Sty_Description";
                cmboStyle.SelectedIndex = -1;
            }
            FormLoaded = true;
            oStockOH = null;

            radStyle.Checked = true;
        }

        private void oBoxNumber_TextChanged(object sender, EventArgs e)
        {
            TextBox oTBox = sender as TextBox;
            if (oTBox != null && FormLoaded)
            {
                using (var context = new TTI2Entities())
                {
                    var SOH = context.TLCSV_StockOnHand.Where(x => x.TLSOH_BoxNumber == oTBox.Text).FirstOrDefault();
                    if (SOH != null)
                    {
                        FormLoaded = false;
                        oStockOH = SOH;
                        txtStyle.Text = context.TLADM_Styles.Find(oStockOH.TLSOH_Style_FK).Sty_Description;
                        txtColour.Text = context.TLADM_Colours.Find(oStockOH.TLSOH_Colour_FK).Col_Description;
                        txtSize.Text = context.TLADM_Sizes.Find(oStockOH.TLSOH_Size_FK).SI_Description;
                        cmboStyle.SelectedValue = oStockOH.TLSOH_Style_FK;
                        FormLoaded = true;
                    }
                }
            }
        }

        private void cmboStyle_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = sender as ComboBox;
            if (oCmbo != null && FormLoaded)
            {
               
                if (radStyle.Checked)
                {
                    if (oStockOH == null)
                    {
                        MessageBox.Show("Please enter a box number");
                        return;
                    }

                    var Selected = (TLADM_Styles)cmboStyle.SelectedItem;
                    if (Selected == null)
                    {
                        MessageBox.Show("Please select an alternative style");
                        return;

                    }

                    using (var context = new TTI2Entities())
                    {
                        if (oStockOH != null)
                        {
                            var SOH = context.TLCSV_StockOnHand.Find(oStockOH.TLSOH_Pk);
                            if (SOH != null)
                            {
                                SOH.TLSOH_Style_FK = Selected.Sty_Id;

                                try
                                {
                                    context.SaveChanges();
                                    MessageBox.Show("Data wrritten successfully to database");

                                    txtColour.Text = string.Empty;
                                    txtSize.Text = string.Empty;
                                    txtStyle.Text = string.Empty;

                                    FormLoaded = false;
                                    cmboStyle.SelectedIndex = -1;
                                    oBoxNumber.Text = String.Empty;
                                    oBoxNumber.Focus();
                                    FormLoaded = true;
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message);
                                }
                            }
                        }
                    }
                }
                else
                {
                    
                    using (var context = new TTI2Entities())
                    {
                        var Selected = (TLADM_Sizes)cmboStyle.SelectedItem;
                        if (Selected == null)
                        {
                            MessageBox.Show("Please select an alternative size");
                            return;

                        }

                        if (oStockOH != null)
                        {
                            var SOH = context.TLCSV_StockOnHand.Find(oStockOH.TLSOH_Pk);
                            if (SOH != null)
                            {
                                SOH.TLSOH_Size_FK = Selected.SI_id;

                                try
                                {
                                    context.SaveChanges();
                                    MessageBox.Show("Data wrritten successfully to database");

                                    txtColour.Text = string.Empty;
                                    txtSize.Text = string.Empty;
                                    txtStyle.Text = string.Empty;

                                    FormLoaded = false;
                                    cmboStyle.SelectedIndex = -1;
                                    oBoxNumber.Text = String.Empty;
                                    oBoxNumber.Focus();
                                    FormLoaded = true;
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message);
                                }
                            }
                        }

                    }
                }
            }
        }

        private void radSizes_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton oRad = (RadioButton)sender;
            if (oRad != null && FormLoaded && oRad.Checked)
            {
                FormLoaded = false;
                using (var context = new TTI2Entities())
                {
                    label5.Text = "Sizes";
                    cmboStyle.DataSource = null;
                    cmboStyle.DataSource = context.TLADM_Sizes.Where(x => !(bool)x.SI_Discontinued).OrderBy(x => x.SI_DisplayOrder).ToList();
                    cmboStyle.ValueMember = "SI_id";
                    cmboStyle.DisplayMember = "SI_Description";
                    cmboStyle.SelectedValue = -1;
                }
                FormLoaded = true;
            }
        }

        private void radStyle_CheckedChanged(object sender, EventArgs e)
        {
             RadioButton oRad = (RadioButton)sender;
             if (oRad != null && oRad.Checked && FormLoaded)
             {
                 using (var context = new TTI2Entities())
                 {
                     FormLoaded = false;

                     label5.Text = "Styles";
                     cmboStyle.DataSource = null;
                     cmboStyle.DataSource = context.TLADM_Styles.Where(x => !(bool)x.Sty_Discontinued).OrderBy(x => x.Sty_Description).ToList();
                     cmboStyle.ValueMember = "Sty_Id";
                     cmboStyle.DisplayMember = "Sty_Description";
                     cmboStyle.SelectedValue = -1;

                     FormLoaded = true;
                 }
             }
        }
    }
}
