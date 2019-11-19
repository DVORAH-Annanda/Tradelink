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
    public partial class frmTLADM_ConsumablesOther : Form
    {
        bool formloaded;
        bool nonNumeric;
        bool addRecord;
        string[][] FldNames;
        bool[] FldEntered;
        Util core;

        public frmTLADM_ConsumablesOther()
        {
            InitializeComponent();
            core = new Util();

            FldNames = new string[][]
            {   new string[] {"txtCode", "Please enter a valid code", "0", "10"},
                new string[] {"txtDescription", "Please enter a description", "1", "50"}, 
                new string[] {"txtConvF", "Please enter  a valid conversion factor", "2", "500"},
                new string[] {"txtBoxUnits", "Please the number of units", "3", "500"},
                new string[] {"txtBoxStdCost", "Please enter a valid standard cost", "4", "500"},
                new string[] {"txtreOrderLevel", "Please enter a reorder level", "5", "500"}, 
                new string[] {"txtEconReOrderQty", "Please enter a economic reorder qty", "6", "500"},
                new string[] {"txtMinimumreOrderQty", "Please enter a standard cost amount", "7", "500"},
                new string[] {"txtDeliveryLeadTime", "Please enter a valid delivery time", "8", "500"},
                new string[] {"cmbUOM", "Please select a unit of measure", "9"},
                new string[] {"cmbAUOM", "Please select an alternative unit of measure", "10"},
                new string[] {"cmbStockType", "Please select a stock type", "11"},
                new string[] {"cmbStoreType", "Please select a store type", "12"},
                new string[] {"cmbStockTakeFreq", "Please select a stock take frequency", "13"},
                new string[] {"cmbConsumableGroups", "Please select a consumable group", "14"},
                new string[] {"cmbSizeCode", "Please select a size", "15"}};
           
            Setup(true);
            
        }
        
        void Setup(bool update)
        {
            formloaded = false;
            using ( var context = new TTI2Entities())
            {
                cmbCodeSelection.DataSource = context.TLADM_ConsumablesOther.OrderBy(x => x.ConsOther_Description).ToList();
                cmbCodeSelection.DisplayMember = "ConsOther_Description";
                cmbCodeSelection.ValueMember = "ConsOther_Pk";
                
                if (update)
                {
                    cmbUOM.DataSource = context.TLADM_UOM.OrderBy(x => x.UOM_Description).ToList();
                    cmbUOM.DisplayMember = "UOM_Description";
                    cmbUOM.ValueMember = "UOM_Pk";

                    cmbAUOM.DataSource = context.TLADM_UOM.OrderBy(x => x.UOM_Description).ToList();
                    cmbAUOM.DisplayMember = "UOM_Description";
                    cmbAUOM.ValueMember = "UOM_Pk";

                    cmbStockType.DataSource = context.TLADM_StockTypes.OrderBy(x => x.ST_Description).ToList();
                    cmbStockType.DisplayMember = "ST_Description";
                    cmbStockType.ValueMember = "ST_Id";

                    cmbStoreType.DataSource = context.TLADM_StoreTypes.OrderBy(x => x.StoreT_Description).ToList();
                    cmbStoreType.DisplayMember = "StoreT_Description";
                    cmbStoreType.ValueMember = "StoreT_Id";

                    cmbStockTakeFreq.DataSource = context.TLADM_StockTakeFreq.OrderBy(x => x.STF_ShortCode).ToList();
                    cmbStockTakeFreq.DisplayMember = "STF_Description";
                    cmbStockTakeFreq.ValueMember = "STF_Pk";

                    cmbConsumableGroups.DataSource = context.TLADM_ConsumableGroups.OrderBy(x => x.ConG_ShortCode).ToList();
                    cmbConsumableGroups.DisplayMember = "ConG_Description";
                    cmbConsumableGroups.ValueMember = "ConG_Pk";

                    cmbSizeCode.DataSource = context.TLADM_Sizes.OrderBy(x => x.SI_id).ToList();
                    cmbSizeCode.DisplayMember = "SI_Description";
                    cmbSizeCode.ValueMember = "SI_Id";

                    cmbPreferedSupplier.DataSource = context.TLADM_Suppliers.OrderBy(x => x.Sup_Code).ToList();
                    cmbPreferedSupplier.DisplayMember = "Sup_Description";
                    cmbPreferedSupplier.ValueMember = "Sup_Pk"; 
                }
            }

            txtCode.Text = string.Empty;
            txtDescription.Text = string.Empty;
            addRecord = false;

            txtEconReOrderQty.Text    = "0";
            txtMinimumreOrderQty.Text = "0";
            txtreOrderLevel.Text      = "0";
            txtDeliveryLeadTime.Text  = "0";
            txtConvF.Text             = "0";

            rbShowQtyNo.Checked     = true;
            rbConsignmentNo.Checked = true;
            rbShowStdCostNo.Checked = true;

            txtBoxStdCost.Visible = true;
            txtBoxUnits.Visible   = true;

            cmbUOM.SelectedIndex =   0;
            cmbAUOM.SelectedIndex =   0;
            cmbStockType.SelectedIndex = 0;
            cmbStoreType.SelectedIndex = 0;
            cmbStockTakeFreq.SelectedIndex = 0;
            cmbConsumableGroups.SelectedIndex = 0;
            cmbSizeCode.SelectedIndex = 0;
            cmbPreferedSupplier.SelectedIndex = 0;

            richTextBox1.Text = string.Empty;
           
            FldEntered = core.PopulateArray(FldNames.Length, false);

            formloaded = true;

        }
        private void txtWin_KeyDownOem(object sender, KeyEventArgs e)
        {
            nonNumeric = false;
            TextBox oTxt = sender as TextBox;

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

        private void txtWin_KeyDown(object sender, KeyEventArgs e)
        {
            nonNumeric = false;

            // Determine whether the keystroke is a number from the top of the keyboard. 
            if (e.KeyCode < Keys.D0 || e.KeyCode > Keys.D9)
            {
                // Determine whether the keystroke is a number from the keypad. 
                if (e.KeyCode < Keys.NumPad0 || e.KeyCode > Keys.NumPad9)
                {
                    // Determine whether the keystroke is a backspace or a period. 
                    if (e.KeyCode != Keys.Back)
                    {
                        // A non-numerical keystroke was pressed. 
                        // Set the flag to true and evaluate in KeyPress event.
                        nonNumeric = true;
                    }
                }

            }

            //If shift key was pressed, it's not a number. 
            if (Control.ModifierKeys == Keys.Shift)
            {
                nonNumeric = true;
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

        private void txt_ValueChanged(object sender, EventArgs e)
        {
            TextBox oTxt = sender as TextBox;
            if (oTxt != null && formloaded)
            {
                var result = (from u in FldNames
                              where u[0] == oTxt.Name
                              select u).FirstOrDefault();
                if (result != null)
                {
                    int nbr = Convert.ToInt32(result[2].ToString());
                    int length = Convert.ToInt32(result[3].ToString());

                    if (oTxt.TextLength > 0 && oTxt.TextLength <= length)
                        FldEntered[nbr] = true;
                    else
                    {
                        FldEntered[nbr] = false;
                        if (oTxt.TextLength > length)
                            MessageBox.Show("Value entered exceeds allowable length");
                    }

                    if (nbr == 0 && addRecord)
                    {
                        using (var context = new TTI2Entities())
                        {
                            var Existing = context.TLADM_ConsumablesOther
                                               .Where(x => x.ConsOther_Code == txtCode.Text).FirstOrDefault();
                            if (Existing != null)
                            {
                                FldEntered[nbr] = false;
                                txtCode.Text = String.Empty;
                                MessageBox.Show("A record already exists for the code entered");

                            }
                        }
                    }
                }
            }
        }

        

        private void btnNew_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;
            if (oBtn != null)
            {
                Setup(false);
                addRecord = true;
                             
            }

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;
            if (oBtn != null)
            {
                var errorM = returnMessage(FldEntered);
                if (!String.IsNullOrEmpty(errorM))
                {
                    MessageBox.Show(errorM);
                    return;
                }

                TLADM_ConsumablesOther Selected =  new TLADM_ConsumablesOther();
                using (var context = new TTI2Entities())
                {
                    if (!addRecord)
                    {
                        var previous = (TLADM_ConsumablesOther)cmbCodeSelection.SelectedItem;
                        if (previous != null)
                            Selected = context.TLADM_ConsumablesOther.Find(previous.ConsOther_Pk);
                        else
                            addRecord = true;
                    }

                    Selected.ConsOther_Code = txtCode.Text;
                    Selected.ConsOther_Description = txtDescription.Text;
                    Selected.ConsOther_ConvFactor = Convert.ToDecimal(txtConvF.Text);
                    Selected.ConsOther_Unit = Convert.ToInt32(txtBoxUnits.Text);
                    Selected.ConsOther_StdCost = Convert.ToDecimal(txtBoxStdCost.Text);
                    Selected.ConsOther_ReOrderLevel = Convert.ToInt32(txtreOrderLevel.Text);
                    Selected.ConsOther_EconomicReOrderQty = Convert.ToInt32(txtEconReOrderQty.Text);
                    Selected.ConsOther_MinReOrderQty = Convert.ToInt32(txtMinimumreOrderQty.Text);
                    Selected.ConsOther_DeliveryLeadTime = Convert.ToInt32(txtDeliveryLeadTime.Text);
                    Selected.ConsOther_Notes = richTextBox1.Text;

                    Selected.ConsOther_AUOM_FK = Convert.ToInt32(cmbAUOM.SelectedValue.ToString());
                    Selected.ConsOther_UOM_FK = Convert.ToInt32(cmbUOM.SelectedValue.ToString());
                    Selected.ConsOther_PreferredSupplier_FK = Convert.ToInt32(cmbPreferedSupplier.SelectedValue.ToString());
                    Selected.ConsOther_SizeCode_FK = Convert.ToInt32(cmbSizeCode.SelectedValue.ToString());
                    Selected.ConsOther_StockTakeFrequency_Fk = Convert.ToInt32(cmbStockTakeFreq.SelectedValue.ToString());
                    Selected.ConsOther_StockType_Fk = Convert.ToInt32(cmbStockType.SelectedValue.ToString());
                    Selected.ConsOther_Store_FK = Convert.ToInt32(cmbStoreType.SelectedValue.ToString());
                    Selected.ConsOther_ConsGroup_FK = Convert.ToInt32(cmbConsumableGroups.SelectedValue.ToString());
                    
                    if (rbConsignmentYes.Checked)
                        Selected.ConsOther_Consignment = true;
                    else
                        Selected.ConsOther_Consignment = false;

                    if (rbShowStdCostYes.Checked)
                        Selected.ConsOther_ShowStdCost = true;
                    else
                        Selected.ConsOther_ShowStdCost = false;

                    if (rbShowQtyYes.Checked)
                        Selected.ConsOther_ShowQty = true;
                    else
                        Selected.ConsOther_ShowQty = false;

                    if (addRecord)
                        context.TLADM_ConsumablesOther.Add(Selected);

                    try
                    {
                        context.SaveChanges();
                        Setup(false);
                        MessageBox.Show("Record stored to database successfully");
                       
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }

            }

        }

        private string returnMessage(bool[] selectedarray)
        {
            int Cnt = 0;
            StringBuilder Mess = new StringBuilder();

            foreach (var ArrayElement in selectedarray)
            {
                if (bool.FalseString == ArrayElement.ToString())
                {
                    var result = (from u in FldNames
                                  where u[2] == Cnt.ToString()
                                  select u).FirstOrDefault();

                    Mess.Append(result[1] + Environment.NewLine);
                }

                Cnt += 1;
            }
            return Mess.ToString();
        }

        private void Cmb_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmb = sender as ComboBox;
            if (oCmb != null && formloaded)
            {
                var result = (from u in FldNames
                              where u[0] == oCmb.Name
                              select u).FirstOrDefault();
                if (result != null)
                {
                    int nbr = Convert.ToInt32(result[2].ToString());
                    FldEntered[nbr] = true;
                }
            }
        }

        private void cmbCodeSelection_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmb = sender as ComboBox;
            if (oCmb != null && formloaded)
            {
                var Selected = (TLADM_ConsumablesOther)oCmb.SelectedItem;
                if (Selected != null)
                {
                    addRecord = false;

                    txtCode.Text = Selected.ConsOther_Code;
                    txtDescription.Text = Selected.ConsOther_Description;
                    txtConvF.Text = Selected.ConsOther_ConvFactor.ToString();
                    txtBoxUnits.Text = Selected.ConsOther_Unit.ToString();
                    txtBoxStdCost.Text = Math.Round(Selected.ConsOther_StdCost, 2).ToString();
                    txtreOrderLevel.Text = Selected.ConsOther_ReOrderLevel.ToString();
                    txtEconReOrderQty.Text = Selected.ConsOther_EconomicReOrderQty.ToString();
                    txtMinimumreOrderQty.Text = Selected.ConsOther_MinReOrderQty.ToString();
                    txtDeliveryLeadTime.Text = Selected.ConsOther_DeliveryLeadTime.ToString();
                    richTextBox1.Text = Selected.ConsOther_Notes;

                    cmbAUOM.SelectedValue = Selected.ConsOther_AUOM_FK;
                    cmbUOM.SelectedValue = Selected.ConsOther_UOM_FK;
                    cmbPreferedSupplier.SelectedValue = Selected.ConsOther_PreferredSupplier_FK;
                    cmbSizeCode.SelectedValue = Selected.ConsOther_SizeCode_FK;
                    cmbStockTakeFreq.SelectedValue = Selected.ConsOther_StockTakeFrequency_Fk;
                    cmbStockType.SelectedValue = Selected.ConsOther_StockType_Fk;
                    cmbStoreType.SelectedValue = Selected.ConsOther_Store_FK;
                    cmbConsumableGroups.SelectedValue = Selected.ConsOther_ConsGroup_FK;

                    if (Selected.ConsOther_Consignment == true)
                        rbConsignmentYes.Checked = true;
                    else
                        rbConsignmentNo.Checked = true;

                    if (Selected.ConsOther_ShowQty)
                        rbShowQtyYes.Checked = true;
                    else
                        rbShowQtyNo.Checked = true;

                    if (Selected.ConsOther_ShowStdCost)
                        rbShowStdCostYes.Checked = true;
                    else
                        rbShowStdCostNo.Checked = true;


                    FldEntered = core.PopulateArray(FldNames.Length, true);
                  
                    

                }
            }

        }
    }
}
