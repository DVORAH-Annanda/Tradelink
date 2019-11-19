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

namespace TTI2_WF
{
    public partial class frmTLADMNonStockItems : Form
    {
        bool formloaded;
        bool nonNumeric;
        bool AddRecord;
        Util core; 
        string[][] MandatoryFields;
        bool[] MandSelected;

          

        public frmTLADMNonStockItems()
        {
            InitializeComponent();
            core = new Util();
           
            MandatoryFields = new string[][]
            {   new string[] {"txtCode", "Please enter a valid code", "0", "10"},
                new string[] {"txtDescription", "Please enter a valid description", "1", "50"}, 
                new string[] {"txtBoxUnitCost", "Please enter a valid stndard cost amount", "2", "500"},
                new string[] {"cmbUOM", "Please select a unit of measure", "3"},
                new string[] {"cmbCategories", "Please select a category", "4"},
                new string[] {"cmbStockTypes", "Please select a stock type", "5"}
            };

            Setup(true);
        
        }

      

        private void Setup(bool Update)
        {
            formloaded = false;
            using (var context = new TTI2Entities())
            {

                cmbNSI.DataSource    = context.TLADM_NonStockItems.OrderBy(x => x.NSI_Code).ToList();
                cmbNSI.ValueMember   = "NSI_Pk";
                cmbNSI.DisplayMember = "NSI_Description";
     
                if (Update)
                {
                    cmbUOM.DataSource = context.TLADM_UOM.OrderBy(x => x.UOM_Description).ToList();
                    cmbUOM.ValueMember = "UOM_Pk";
                    cmbUOM.DisplayMember = "UOM_Description";
                 

                    cmbCategories.DataSource = context.TLADM_NonStockCat.OrderBy(x => x.NSC_ShortCode).ToList();
                    cmbCategories.DisplayMember = "NSC_Description";
                    cmbCategories.ValueMember = "NSC_Pk";
                 

                    cmbStockTypes.DataSource = context.TLADM_StockTypes.OrderBy(x => x.ST_ShortCode).ToList();
                    cmbStockTypes.DisplayMember = "ST_Description";
                    cmbStockTypes.ValueMember = "ST_Id";
               }
            }

            txtCode.Text = string.Empty;
            txtDescription.Text = string.Empty;

            rbNo.Checked = true;
            txtBoxUnitCost.Text = "0.00";
            txtBoxUnitCost.Visible = true;
            AddRecord = false;

            btnDept.Text = "0";

            MandSelected = core.PopulateArray(MandatoryFields.Length, false);

            formloaded = true;
        }

        private void txtWin_KeyDownOem(object sender, KeyEventArgs e)
        {
            nonNumeric = false;
            TextBox oTxt = sender as TextBox;
            
            if (oTxt != null)
            {
                // Determine whether the keystroke is a number from the top of the keyboard. 
                if (e.KeyCode < Keys.D0 || e.KeyCode > Keys.D9)
                {
                    // Determine whether the keystroke is a number from the keypad. 
                    if (e.KeyCode < Keys.NumPad0 || e.KeyCode > Keys.NumPad9)
                    {
                        // Determine whether the keystroke is a backspace or a period. 
                        if (e.KeyCode != Keys.Back && e.KeyCode != Keys.OemPeriod)
                        {
                            // A non-numerical keystroke was pressed. 
                            // Set the flag to true and evaluate in KeyPress event.
                            nonNumeric = true;
                        }
                    }

                    if (e.KeyCode == Keys.OemPeriod)
                    {
                        int nonperiod = oTxt.Text.Count(x => x == '.');

                        if (nonperiod > 0)
                            nonNumeric = true;
                    }
                }

                //If shift key was pressed, it's not a number. 
                if (Control.ModifierKeys == Keys.Shift)
                {
                    nonNumeric = true;
                }
            }
        }

        private void txtWin_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (nonNumeric)
            {
                // Stop the character from being entered into the control since it is non-numerical.
                e.Handled = true;
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;
            if (oBtn != null && formloaded)
            {
                Setup(false);
                AddRecord = true;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;
            if (oBtn != null && formloaded)
            {
                TLADM_NonStockItems nsi = new TLADM_NonStockItems();

                string errorM = core.returnMessage(MandSelected, true, MandatoryFields);
                if (errorM != string.Empty)
                {
                    MessageBox.Show("The following Errors were encountered " + Environment.NewLine + errorM);
                    return;
                }

                var uom = (TLADM_UOM)cmbUOM.SelectedItem;
                var nscat = (TLADM_NonStockCat)cmbCategories.SelectedItem;
                var stocktypes = (TLADM_StockTypes)cmbStockTypes.SelectedItem;

                using (var context = new TTI2Entities())
                {
                    if (AddRecord)
                    {
                        nsi.NSI_Code = txtCode.Text;
                        nsi.NSI_Description = txtDescription.Text;
                        nsi.NSI_Category_FK = nscat.NSC_Pk;
                        nsi.NSI_StockType_FK = stocktypes.ST_Id;
                        nsi.NSI_UOM_FK = uom.UOM_Pk;
                        nsi.NSI_Department_PWN = Convert.ToInt32(btnDept.Text);
                        nsi.NSI_UnitCost = Convert.ToDecimal(txtBoxUnitCost.Text);
                        if(core.IsValueDidgit(btnDept.Text))
                           nsi.NSI_Department_PWN = Convert.ToInt32(btnDept.Text);
                        if (rbYes.Checked)
                            nsi.NSI_ShowUnitCost = true;

                        context.TLADM_NonStockItems.Add(nsi);
                    }
                    else
                    {
                        var test = (TLADM_NonStockItems)cmbNSI.SelectedItem;
                        nsi = context.TLADM_NonStockItems.Find(test.NSI_Pk);
                        nsi.NSI_Code = txtCode.Text;
                        nsi.NSI_Description = txtDescription.Text;
                        nsi.NSI_Category_FK = nscat.NSC_Pk;
                        nsi.NSI_StockType_FK = stocktypes.ST_Id;
                        nsi.NSI_UOM_FK = uom.UOM_Pk;
                        if (core.IsValueDidgit(btnDept.Text))
                            nsi.NSI_Department_PWN = Convert.ToInt32(btnDept.Text);
                        nsi.NSI_UnitCost = Convert.ToDecimal(txtBoxUnitCost.Text);
                        if (rbYes.Checked)
                            nsi.NSI_ShowUnitCost = true;
                    }

                    try
                    {
                        context.SaveChanges();
                        MessageBox.Show("records successfully updated to database");
                        cmbCategories.SelectedIndex = 0;
                        cmbNSI.SelectedIndex = 0;
                        cmbStockTypes.SelectedIndex = 0;
                        cmbUOM.SelectedIndex = 0;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        return;
                    }

                }
                Setup(false);
            }
        }

       

        private void cmb_SelectedIndexChanged(object sender, EventArgs e)
        {
            int nbr; 
            ComboBox oCmb = sender as ComboBox;
            if (oCmb != null && formloaded)
            {

                var result = (from u in MandatoryFields
                              where u[0] == oCmb.Name
                              select u).FirstOrDefault();

                if (result != null)
                {
                    nbr = Convert.ToInt32(result[2].ToString());
                    MandSelected[nbr] = true;
                }

                if (oCmb.TabIndex == 0)
                {
                    AddRecord = false;

                    TLADM_NonStockItems nsi = new TLADM_NonStockItems();
                    var test = (TLADM_NonStockItems)oCmb.SelectedItem;
                    using (var context = new TTI2Entities())
                    {
                        nsi = context.TLADM_NonStockItems.Find(test.NSI_Pk);
                        if (nsi != null)
                        {
                            txtCode.Text = nsi.NSI_Code;
                            txtDescription.Text = nsi.NSI_Description;
                            txtBoxUnitCost.Text = Math.Round(nsi.NSI_UnitCost, 2).ToString();
                            cmbUOM.SelectedValue = nsi.NSI_UOM_FK;
                            cmbStockTypes.SelectedValue = nsi.NSI_StockType_FK;
                            cmbCategories.SelectedValue = nsi.NSI_Category_FK;
                            btnDept.Text = nsi.NSI_Department_PWN.ToString();
                            txtBoxUnitCost.Visible = true;

                            if (nsi.NSI_ShowUnitCost)
                            {
                                rbYes.Checked = true;
                               
                            }
                            else
                            {
                                rbNo.Checked = true;
                              
                            }

                            MandSelected = core.PopulateArray(MandatoryFields.Length, true);
                        }
                    }
                }
            }
        }

     

        private void btnDept_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;
            if (oBtn != null)
            {
                frmTLADMGardProp depts = new frmTLADMGardProp(5000, Convert.ToInt32(oBtn.Text));
                depts.ShowDialog(this);
                oBtn.Text = depts.TotalPN.ToString();
               
            }

        }

        private void Txt_TextChanged(object sender, EventArgs e)
        {
            TextBox oTxt = sender as TextBox;
            if (oTxt != null && formloaded)
            {
                var result = (from u in MandatoryFields
                              where u[0] == oTxt.Name
                              select u).FirstOrDefault();
                if (result != null)
                {
                    int nbr    = Convert.ToInt32(result[2].ToString());
                    int length = Convert.ToInt32(result[3].ToString());

                    if (oTxt.TextLength > 0 && oTxt.TextLength <= length)
                        MandSelected[nbr] = true;
                    else
                    {
                        MandSelected[nbr] = false;

                        if (oTxt.TextLength > length)
                            MessageBox.Show("Value entered exceeds allowable length"); 
                    }

                    if (nbr == 0 && AddRecord)
                    {
                        using (var context = new TTI2Entities())
                        {
                            var Existing = context.TLADM_NonStockItems.Where(x => x.NSI_Code == txtCode.Text).FirstOrDefault();
                            if (Existing != null)
                            {
                                MandSelected[nbr] = false;
                                txtCode.Text = string.Empty;
                                MessageBox.Show("A record already exists for the code as entered");
                            }
                        }
                    }
 
                }
            }
        }
    }
}
