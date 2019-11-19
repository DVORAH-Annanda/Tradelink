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
    public partial class frmTLADM_ConsumablesDC : Form
    {
        bool formloaded;
        bool nonNumeric;
        bool addRecord;
        string[][] FldNames;
        bool[] FldEntered;
        Util core;
        public frmTLADM_ConsumablesDC()
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
                new string[] {"cmbPreferedSupplier", "Please select a preferred supplier", "15"}
            };
     

            Setup(true);

        }

        void Setup(bool update)
        {
            formloaded = false;
            FldEntered = core.PopulateArray(FldNames.Length, false);

            using (var context = new TTI2Entities())
            {
                cmbCodeSelection.DataSource = context.TLADM_ConsumablesDC.OrderBy(x => x.ConsDC_Description).ToList();
                cmbCodeSelection.DisplayMember = "ConsDC_Description";
                cmbCodeSelection.ValueMember = "ConsDC_Pk";

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

                    cmbPreferedSupplier.DataSource = context.TLADM_Suppliers.OrderBy(X => X.Sup_Code).ToList();
                    cmbPreferedSupplier.DisplayMember = "Sup_Description";
                    cmbPreferedSupplier.ValueMember = "Sup_Pk";
                }
            }

            txtCode.Text = string.Empty;
            txtDescription.Text = string.Empty;

            txtEconReOrderQty.Text = "0";
            txtMinimumreOrderQty.Text = "0";
            txtreOrderLevel.Text = "0";
            txtDeliveryLeadTime.Text = "0";
            txtConvF.Text = "0";

            txtBoxStdCost.KeyDown += core.txtWin_KeyDownOEM;
            txtBoxStdCost.KeyPress += core.txtWin_KeyPress;

            rbShowQtyNo.Checked = true;
            rbConsignmentNo.Checked = true;
            rbShowStdCostNo.Checked = true;
            rbHazardousNo.Checked = true;
            rbDiscontinued.Checked = false;

            richTextBox1.Text = string.Empty;
            richTextBox2.Text = string.Empty;

            cmbAUOM.SelectedIndex = 0;
            cmbConsumableGroups.SelectedIndex = 0;
            cmbPreferedSupplier.SelectedIndex = 0;
            cmbStockTakeFreq.SelectedIndex = 0;
            cmbStockType.SelectedIndex = 0;
            cmbStoreType.SelectedIndex = 0;
            cmbUOM.SelectedIndex = 0;

            txtBoxStdCost.Visible = true;
            txtBoxUnits.Visible = true;

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
                    if (oTxt.TextLength > 0 && oTxt.TextLength < length)
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
                            var exists = context.TLADM_ConsumablesDC
                                             .Where(x => x.ConsDC_Code == txtCode.Text).FirstOrDefault();
                            if (exists != null)
                            {
                                FldEntered[nbr] = false;
                                txtCode.Text = string.Empty;
                                MessageBox.Show("A record already exists for the code entered"); 
                            }
                        }
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
            if(oCmb != null && formloaded)
            {
                var Selected = (TLADM_ConsumablesDC)oCmb.SelectedItem;
                if (Selected != null)
                {
                    txtCode.Text = Selected.ConsDC_Code;
                    txtDescription.Text = Selected.ConsDC_Description;
                    txtConvF.Text = Selected.ConsDC_ConsFactors.ToString();
                    txtBoxUnits.Text = Selected.ConsDC_Units.ToString();
                    txtBoxStdCost.Text = Math.Round(Selected.ConsDC_StandardCost, 2).ToString();
                    txtreOrderLevel.Text = Selected.ConsDC_ReOrderLevel.ToString();
                    txtEconReOrderQty.Text = Selected.ConsDC_Economic_ReOrderQty.ToString();
                    txtMinimumreOrderQty.Text = Selected.ConsDC_MinReorderQty.ToString();
                    txtDeliveryLeadTime.Text = Selected.ConsDC_DelLeadTime.ToString();
                    richTextBox1.Text = Selected.ConsDC_Notes;
                    richTextBox2.Text = Selected.ConsDC_HazChem;
                    cmbAUOM.SelectedValue = Selected.ConsDC_AUOM_FK;
                    cmbUOM.SelectedValue = Selected.ConsDC_UOM_Fk;
                    cmbPreferedSupplier.SelectedValue = Selected.ConsDC_PreferedSupplier_FK;
                    cmbStockTakeFreq.SelectedValue = Selected.ConsDC_StockTake_FK;
                    cmbStockType.SelectedValue = Selected.ConsDC_StockType_FK;
                    cmbStoreType.SelectedValue = Selected.ConsDC_StoreCode_FK;
                    cmbConsumableGroups.SelectedValue = Selected.ConsDC_ConsGroup_FK;

                    if (Selected.ConsDC_Discontinued)
                    {
                        rbDiscontinued.Checked = true;
                        dtpDiscontinuedDate.Value = (DateTime)Selected.ConsDC_DiscontinuedDate;
                    }
                    else
                    {
                        rbActive.Checked = true;
                        dtpDiscontinuedDate.Enabled = false;
                    }

                    if (Selected.ConsDC_Consignment == true)
                        rbConsignmentYes.Checked = true;
                    else
                        rbConsignmentNo.Checked = true;

                    if (Selected.ConsDC_ShowQty)
                        rbShowQtyYes.Checked = true;
                    else
                        rbShowQtyNo.Checked = true;

                    if ((bool)Selected.ConsDC_ShowStdCost)
                        rbShowStdCostYes.Checked = true;
                    else
                        rbShowStdCostNo.Checked = true;


                    FldEntered = core.PopulateArray(FldNames.Length, true);
                    addRecord = false;

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
                FldEntered = core.PopulateArray(FldNames.Length, false);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;
            if (oBtn != null && formloaded)
            {
                var errorM = returnMessage(FldEntered);
                if (!String.IsNullOrEmpty(errorM))
                {
                    MessageBox.Show(errorM);
                    return;
                }

                TLADM_ConsumablesDC Selected = new TLADM_ConsumablesDC();
                using (var context = new TTI2Entities())
                {
                    if (!addRecord)
                    {
                        var previous = (TLADM_ConsumablesDC)cmbCodeSelection.SelectedItem;
                        if (previous != null)
                            Selected = context.TLADM_ConsumablesDC.Find(previous.ConsDC_Pk);
                        else
                            addRecord = true;
                    }
                    try
                    {
                        Selected.ConsDC_Code = txtCode.Text;
                        Selected.ConsDC_Description = txtDescription.Text;
                        Selected.ConsDC_ConsFactors = Convert.ToDecimal(txtConvF.Text);
                        Selected.ConsDC_Units = Convert.ToInt32(txtBoxUnits.Text);
                        Selected.ConsDC_StandardCost = Convert.ToDecimal(txtBoxStdCost.Text);
                        Selected.ConsDC_ReOrderLevel = Convert.ToInt32(txtreOrderLevel.Text);
                        Selected.ConsDC_Economic_ReOrderQty = Convert.ToInt32(txtEconReOrderQty.Text);
                        Selected.ConsDC_MinReorderQty = Convert.ToInt32(txtMinimumreOrderQty.Text);
                        Selected.ConsDC_DelLeadTime = Convert.ToInt32(txtDeliveryLeadTime.Text);
                        Selected.ConsDC_Notes = richTextBox1.Text;
                        Selected.ConsDC_HazChem = richTextBox2.Text;
                        Selected.ConsDC_AUOM_FK = Convert.ToInt32(cmbAUOM.SelectedValue.ToString());
                        Selected.ConsDC_UOM_Fk = Convert.ToInt32(cmbUOM.SelectedValue.ToString());
                        Selected.ConsDC_PreferedSupplier_FK = Convert.ToInt32(cmbPreferedSupplier.SelectedValue.ToString());
                        Selected.ConsDC_StockTake_FK = Convert.ToInt32(cmbStockTakeFreq.SelectedValue.ToString());
                        Selected.ConsDC_StockType_FK = Convert.ToInt32(cmbStockType.SelectedValue.ToString());
                        Selected.ConsDC_StoreCode_FK = Convert.ToInt32(cmbStoreType.SelectedValue.ToString());
                        Selected.ConsDC_ConsGroup_FK = Convert.ToInt32(cmbConsumableGroups.SelectedValue.ToString());

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        return;
                    }

                    if (rbDiscontinued.Checked)
                    {
                        Selected.ConsDC_Discontinued = true;
                        Selected.ConsDC_DiscontinuedDate = dtpDiscontinuedDate.Value;
                    }
                    else
                    {
                        Selected.ConsDC_Discontinued = false;
                        Selected.ConsDC_DiscontinuedDate = null;
                    }

                    if (rbConsignmentYes.Checked)
                        Selected.ConsDC_Consignment = true;
                    else
                        Selected.ConsDC_Consignment = false;

                    if (rbShowStdCostYes.Checked)
                        Selected.ConsDC_ShowStdCost = true;
                    else
                        Selected.ConsDC_ShowStdCost = false;

                    if (rbShowQtyYes.Checked)
                        Selected.ConsDC_ShowQty = true;
                    else
                        Selected.ConsDC_ShowQty = false;

                    if (rbHazardousYes.Checked)
                        Selected.ConsDC_Hazardous = true;
                    else
                        Selected.ConsDC_Hazardous = false;

                    if (addRecord)
                        context.TLADM_ConsumablesDC.Add(Selected);

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

        private void rbActive_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton oRad = sender as RadioButton;
            if (oRad != null && formloaded)
            {
                if (oRad.Checked)
                {
                    rbDiscontinued.Checked = false;
                    dtpDiscontinuedDate.Value = DateTime.Now;

                    if (dtpDiscontinuedDate.Enabled)
                        dtpDiscontinuedDate.Enabled = false;
                }
            }
        }

        private void rbDiscontinued_CheckedChanged(object sender, EventArgs e)
        {
              RadioButton oRad = sender as RadioButton;
              if (oRad != null && formloaded)
              {
                  if (oRad.Checked)
                  {
                      if (!dtpDiscontinuedDate.Enabled)
                          dtpDiscontinuedDate.Enabled = true;
                  }
              }
        }
  
    }
}
