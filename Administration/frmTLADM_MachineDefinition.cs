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
    public partial class frmTLADM_MachineDefinition : Form
    {
        string[][] MeasElements;
        bool[] MeasSelected;
      
        string[][] MandatoryFields;
        bool[] MandSelected;
     
        bool nonNumeric;
        bool formloaded;
        bool addRecord;
        Util core;

        public frmTLADM_MachineDefinition()
        {
            InitializeComponent();

            core = new Util();
          
            MandatoryFields = new string[][]
            {   new string[] {"txtMachineCode", "Please enter a machine code", "0","F", "10"},
                new string[] {"cmbDepartmentSelect", "Please select a department", "1","F"}, 
                new string[] {"cmbFabricProdType", "Please select a fabric product type", "2","T"},
                new string[] {"txtMacineDescription", "Please enter a machine description ", "3","F", "50"},
                new string[] {"txtMaxCapacity", "Please enter a maximum capacity for this machine", "4", "F", "500"},
                new string[] {"txtGLCostCentre", "Please enter a GL Cost Centre", "5", "F", "10"}, 
                new string[] {"txtRealistic", "Please enter a realistic value of which to measure", "6", "F", "500"},
                new string[] {"txtSerialNo", "Please enter a serial number for this machine", "7", "F", "15"},
                new string[] {"cmbFinishedGoods", "Please select a finished goods product", "8", "F"},
                new string[] {"cmbFirstM", "Please select at least one measurement", "9", "F"},
                new string[] {"txtAssetRegNo", "Please enter a assest register number", "10", "F", "10"},
            };
            MandSelected = core.PopulateArray(MandatoryFields.Length, false);

            MeasElements = new string[][]
            {   new string[] {"cmbFirstM", "", "0"},
                new string[] {"cmbSecondM", "", "1"}, 
                new string[] {"cmbThirdM", "", "2"},
                new string[] {"cmbFourthM", "", "3"},
                new string[] {"cmbFifthM", "", "4"},
                new string[] {"cmbSixM", "", "5"}, 
                new string[] {"cmbSeventhM", "", "6"},
                new string[] {"cmbEighthM", "", "7"}
            };
            MeasSelected = core.PopulateArray(MeasElements.Length, false);

            txtRealistic.KeyDown += core.txtWin_KeyDownOEM;
            txtRealistic.KeyPress += core.txtWin_KeyPress;
            txtMaxCapacity.KeyDown += core.txtWin_KeyDownOEM;
            txtMaxCapacity.KeyPress += core.txtWin_KeyPress;
            
            txtMeasure1Qty.KeyDown += core.txtWin_KeyDownOEM;
            txtMeasure1Qty.KeyPress += core.txtWin_KeyPress;

            txtMeasure2Qty.KeyDown += core.txtWin_KeyDownOEM;
            txtMeasure2Qty.KeyPress += core.txtWin_KeyPress;

            txtMeasure3Qty.KeyDown += core.txtWin_KeyDownOEM;
            txtMeasure3Qty.KeyPress += core.txtWin_KeyPress;

            txtMeasure4Qty.KeyDown += core.txtWin_KeyDownOEM;
            txtMeasure4Qty.KeyPress += core.txtWin_KeyPress;

            txtMeasure5Qty.KeyDown += core.txtWin_KeyDownOEM;
            txtMeasure5Qty.KeyPress += core.txtWin_KeyPress;

            txtMeasure6Qty.KeyDown += core.txtWin_KeyDownOEM;
            txtMeasure6Qty.KeyPress += core.txtWin_KeyPress;

            txtMeasure7Qty.KeyDown += core.txtWin_KeyDownOEM;
            txtMeasure7Qty.KeyPress += core.txtWin_KeyPress;

            txtMeasure8Qty.KeyDown += core.txtWin_KeyDownOEM;
            txtMeasure8Qty.KeyPress += core.txtWin_KeyPress;

            txtLastNumberUsed.KeyDown += core.txtWin_KeyDown;
            txtLastNumberUsed.KeyPress += core.txtWin_KeyPress;

            //--------------------------------------------------------
            // wire up the check state changed event
            //--------------------------------------------------------------------------------------------------------
            this.cmbGreigeProductType.CheckStateChanged += new System.EventHandler(this.cmboGreigeProductType_CheckStateChanged);

            SetUp(true);
        }

        void SetUp(bool updt)
        {
            formloaded = false;
            IList<TLADM_Griege> GreigeItems = new List<TLADM_Griege>();

            


            using (var context = new TTI2Entities())
            {
                cmbCodeSelection.DataSource = context.TLADM_MachineDefinitions.OrderBy(x => x.MD_MachineCode).GroupBy(x=>x.MD_MachineCode).Select(grp=>grp.FirstOrDefault()).ToList();
                cmbCodeSelection.DisplayMember = "MD_MachineCode";
                cmbCodeSelection.ValueMember = "MD_Pk";

                if (updt)
                {
                    cmbDepartmentSelect.DataSource = context.TLADM_Departments.OrderBy(x => x.Dep_Description).ToList();
                    cmbDepartmentSelect.DisplayMember = "Dep_Description";
                    cmbDepartmentSelect.ValueMember = "Dep_Id";

                    cmbFabricProdType.DataSource = context.TLADM_FabricProduct.OrderBy(x => x.FP_Description).ToList();
                    cmbFabricProdType.DisplayMember = "FP_Description";
                    cmbFabricProdType.ValueMember = "FP_Id";

                  
                    cmbFinishedGoods.DataSource = context.TLADM_FinishedGoods.OrderBy(x => x.Fin_Description).ToList();
                    cmbFinishedGoods.DisplayMember = "Fin_Description";
                    cmbFinishedGoods.ValueMember = "Fin_Pk";

                    
                    // GreigeItems = context.TLADM_Griege.OrderBy(x => x.TLGreige_Description).ToList();
                    // cmbGreigeProductType.DataSource = context.TLADM_Griege.Where(x => !(bool)x.TLGriege_Discontinued).OrderBy(x => x.TLGreige_Description).ToList();
                    // cmbGreigeProductType.DataSource = GreigeItems;
                    // cmbGreigeProductType.DisplayMember = "TLGreige_Description";
                    // cmbGreigeProductType.ValueMember = "TLGreige_Id";

                    
                    var Existing = context.TLADM_Griege.OrderBy(x => x.TLGreige_Description).ToList();
                    foreach (var row in Existing)
                    {
                        cmbGreigeProductType.Items.Add(new Administration.CheckComboBoxItem(row.TLGreige_Id, row.TLGreige_Description, false));
                    }
                     

                    cmbFirstM.DataSource = context.TLADM_NonStockItems.OrderBy(x => x.NSI_Description).ToList();
                    cmbFirstM.DisplayMember = "NSI_Description";
                    cmbFirstM.ValueMember = "NSI_Pk";

                    cmbSecondM.DataSource = context.TLADM_NonStockItems.OrderBy(x => x.NSI_Description).ToList();
                    cmbSecondM.DisplayMember = "NSI_Description";
                    cmbSecondM.ValueMember = "NSI_Pk";

                    cmbThirdM.DataSource = context.TLADM_NonStockItems.OrderBy(x => x.NSI_Description).ToList();
                    cmbThirdM.DisplayMember = "NSI_Description";
                    cmbThirdM.ValueMember = "NSI_Pk";

                    cmbFourthM.DataSource = context.TLADM_NonStockItems.OrderBy(x => x.NSI_Description).ToList();
                    cmbFourthM.DisplayMember = "NSI_Description";
                    cmbFourthM.ValueMember = "NSI_Pk";
                    
                    cmbFifthM.DataSource = context.TLADM_NonStockItems.OrderBy(x => x.NSI_Description).ToList();
                    cmbFifthM.DisplayMember = "NSI_Description";
                    cmbFifthM.ValueMember = "NSI_Pk";
                    
                    cmbSixM.DataSource = context.TLADM_NonStockItems.OrderBy(x => x.NSI_Description).ToList();
                    cmbSixM.DisplayMember = "NSI_Description";
                    cmbSixM.ValueMember = "NSI_Pk";

                    cmbSeventhM.DataSource = context.TLADM_NonStockItems.OrderBy(x => x.NSI_Description).ToList();
                    cmbSeventhM.DisplayMember = "NSI_Description";
                    cmbSeventhM.ValueMember = "NSI_Pk";

                    cmbEighthM.DataSource = context.TLADM_NonStockItems.OrderBy(x => x.NSI_Description).ToList();
                    cmbEighthM.DisplayMember = "NSI_Description";
                    cmbEighthM.ValueMember = "NSI_Pk"; 

                   
                }
            }

            txtAssetRegNo.Text = string.Empty;
            txtAssetRegNo.MaxLength = 10;
            txtGLCostCentre.Text = string.Empty;
            txtGLCostCentre.MaxLength = 10;
            txtMachineCode.Text = string.Empty;
            txtMachineCode.MaxLength = 10;
            txtMacineDescription.Text = string.Empty;
            txtMaxCapacity.Text = "100.00";
            txtRealistic.Text = "100.00";
            txtSerialNo.Text = string.Empty;
            txtSerialNo.MaxLength = 15;
            txtLastNumberUsed.Text = "0";

            txtMeasure1Qty.Text = "0.00";
            txtMeasure2Qty.Text = "0.00";
            txtMeasure3Qty.Text = "0.00";
            txtMeasure4Qty.Text = "0.00";
            txtMeasure5Qty.Text = "0.00";
            txtMeasure6Qty.Text = "0.00"; 
            txtMeasure7Qty.Text = "0.00";
            txtMeasure8Qty.Text = "0.00";

          

            btnMaintenance.Text = "0";
            InitialiseConsumption();
           
            cmbCodeSelection.SelectedValue = 0;
            cmbFinishedGoods.SelectedValue = 0;
            cmbFabricProdType.SelectedValue = 0;

            label5.Visible = false;
            cmbGreigeProductType.Visible = false;
           

            foreach (Administration.CheckComboBoxItem item in cmbGreigeProductType.Items)
            {
                item.CheckState = false;
            }
                       
            if(cmbCodeSelection.Items.Count == 0)
               addRecord = true;
            else
                addRecord = false;

            formloaded = true;
        }


        //-------------------------------------------------------------------------------------
        // this message handler gets called when the user checks/unchecks an item the combo box
        //----------------------------------------------------------------------------------------
        private void cmboGreigeProductType_CheckStateChanged(object sender, EventArgs e)
        {
            Boolean DoesExist = false;
            if (sender is Administration.CheckComboBoxItem && formloaded)
            {
                Administration.CheckComboBoxItem item = (Administration.CheckComboBoxItem)sender;
               
                if (item.CheckState)
                {
                     DialogResult res = MessageBox.Show("Please confirm wanting to add this record to the database", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                     if (res == DialogResult.Yes)
                     {
                          InitialiseConsumption();
                          MandSelected[9] = false;
                          MeasSelected = core.PopulateArray(MeasElements.Length, false);
                          addRecord = true;
                     }
                     else
                     {
                         item.CheckState = false;
                     }
                }
                else
                {
                    var selectedCode = (TLADM_MachineDefinitions)cmbCodeSelection.SelectedItem;
                    if (selectedCode == null)
                    {
                        MessageBox.Show("Please select a machine code");
                        return;
                    }

                    using (var context = new TTI2Entities())
                    {
                        var Md = context.TLADM_MachineDefinitions.Where(x => x.MD_MachineCode == selectedCode.MD_MachineCode && x.MD_GreigeType_FK == item._Pk).FirstOrDefault();
                        if (Md != null)
                            DoesExist = true;
                    }

                    if (DoesExist)
                    {
                        DialogResult res = MessageBox.Show("Please confirm wanting to delete this record", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (res == DialogResult.Yes)
                        {
                            using (var context = new TTI2Entities())
                            {
                                var MachineRecord = context.TLADM_MachineDefinitions.Where(x => x.MD_MachineCode == selectedCode.MD_MachineCode && x.MD_GreigeType_FK == item._Pk).FirstOrDefault();
                                if (MachineRecord != null)
                                {
                                    var Greige = context.TLADM_Griege.Where(x => x.TLGreige_Machine_FK == MachineRecord.MD_Pk).FirstOrDefault();
                                    if (Greige != null)
                                    {
                                        MessageBox.Show("There is a existing Greige record for this machine" + Environment.NewLine + "Amend the record as shown. Delete not allowed ", "Greige quality " + Greige.TLGreige_Description);
                                        return;
  
                                    }
                                    context.TLADM_MachineDefinitions.Remove(MachineRecord);

                                    try
                                    {
                                        context.SaveChanges();
                                        MessageBox.Show("Record successfully removed from database");
                                        SetUp(false);
                                    }
                                    catch (Exception ex)
                                    {
                                        MessageBox.Show(ex.Message);
                                    }
                                }
                            }
                        }
                        else
                        {
                            item.CheckState = true;
                        }
                    }
                }
              
            }
        }
        private void txtWin_KeyDown(object sender, KeyEventArgs e)
        {
            nonNumeric = false;
            TextBox oTxtBox = sender as TextBox;
            ComboBox oCmbo = sender as ComboBox;

            // Determine whether the keystroke is a number from the top of the keyboard. 
            if (oTxtBox != null)
            {
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
            }
            else
            {
                if (e.KeyCode == Keys.Back && oCmbo != null)
                {
                    var result = (from u in MeasElements
                                  where u[0] == oCmbo.Name
                                  select u).FirstOrDefault();

                    int nbr = Convert.ToInt32(result[2].ToString());
                    MeasSelected[nbr] = false;
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

        }

        private void cmb_MeasurementSelectedIndexchanged(object sender, EventArgs e)
        {
            ComboBox oCmb = sender as ComboBox;
            if (oCmb != null && formloaded)
            {
                var result = (from u in MeasElements
                              where u[0] == oCmb.Name
                              select u).FirstOrDefault();

                if (result != null)
                {
                    int nbr = Convert.ToInt32(result[2].ToString());
                    MeasSelected[nbr] = true;

                    result = (from u in MandatoryFields
                              where u[0] == oCmb.Name
                              select u).FirstOrDefault();

                    if (result != null)
                    {
                        int mnbr = Convert.ToInt32(result[2].ToString());
                        MandSelected[mnbr] = true;
                    }
                }
            }
        }

        private void cmb_MandatorySelectedIndexChanged(object sender, EventArgs e)
        {
             ComboBox oCmb = sender as ComboBox;
             bool IsKnitting = false;
            TLADM_MachineDefinitions ExistingRecord = null;

             if (oCmb != null && formloaded)
             {
                 var result = (from u in MandatoryFields
                               where u[0] == oCmb.Name
                               select u).FirstOrDefault();

                 int nbr = Convert.ToInt32(result[2].ToString());
                 MandSelected[nbr] = true;

                 if (result[3] == "T" && MandSelected[0] && MandSelected[2])
                 {
                     //--------------------------------------------------------------------------
                     var Department = (TLADM_Departments)cmbDepartmentSelect.SelectedItem;
                     if (Department != null)
                     {
                         if (Department.Dep_ShortCode.Contains("KNIT"))
                         {
                             label5.Visible = true;
                             cmbGreigeProductType.Visible = true;
                             IsKnitting = true;
                         }
                         else 
                         {
                             label5.Visible = false;
                             cmbGreigeProductType.Visible = false;
                         }



                    }
                     using (var context = new TTI2Entities())
                     {
                         var ft = (TLADM_FabricProduct)cmbFabricProdType.SelectedItem;

                         if (ft != null)
                         {
                             ExistingRecord = context.TLADM_MachineDefinitions
                                                  .Where(m => m.MD_MachineCode == txtMachineCode.Text
                                                         && m.MD_FabricType_FK == ft.FP_Id).FirstOrDefault();
                             if (ExistingRecord != null)
                             {
                                 InitialiseConsumption();

                                 txtAssetRegNo.Text = ExistingRecord.MD_AssetRegNo;
                                 txtGLCostCentre.Text = ExistingRecord.MD_GLCostCentre;
                                 txtMachineCode.Text = ExistingRecord.MD_MachineCode;
                                 txtMacineDescription.Text = ExistingRecord.MD_Description;
                                 txtMaxCapacity.Text = ExistingRecord.MD_MaxCapacity.ToString();
                                 txtRealistic.Text = ExistingRecord.MD_Realistic.ToString();
                                 txtSerialNo.Text = ExistingRecord.MD_SerialNo;

                                 btnMaintenance.Text = ExistingRecord.MD_Maintenance_TotalPN.ToString();
                                 
                                 try
                                 {
                                     cmbDepartmentSelect.SelectedValue = ExistingRecord.MD_Department_FK;
                                     cmbFinishedGoods.SelectedValue = ExistingRecord.MD_FinishGoods_FK;
                                     cmbFabricProdType.SelectedValue = ExistingRecord.MD_FabricType_FK;
                                 }
                                 catch (Exception ex)
                                 {
                                     MessageBox.Show(ex.Message);
                                 }

                                 if (!string.IsNullOrEmpty(ExistingRecord.MD_FirstMeasure_FK.ToString()))
                                 {
                                     cmbFirstM.SelectedValue = ExistingRecord.MD_FirstMeasure_FK;
                                     txtMeasure1Qty.Text = ExistingRecord.MD_FirstMeasure_Qty.ToString();

                                 }
                                 else
                                     cmbFirstM.SelectedValue = 0;

                                 if (!string.IsNullOrEmpty(ExistingRecord.MD_SecMeasure_FK.ToString()))
                                 {
                                     cmbSecondM.SelectedValue = ExistingRecord.MD_SecMeasure_FK;
                                     txtMeasure2Qty.Text = ExistingRecord.MD_SecMeasure_Qty.ToString();
                                 }
                                 else
                                     cmbSecondM.SelectedValue = 0;

                                 if (!string.IsNullOrEmpty(ExistingRecord.MD_ThirdMeasure_FK.ToString()))
                                 {
                                     cmbThirdM.SelectedValue = ExistingRecord.MD_ThirdMeasure_FK;
                                     txtMeasure3Qty.Text = ExistingRecord.MD_ThirdMeasure_Qty.ToString();

                                 }
                                 else

                                     cmbThirdM.SelectedValue = 0;

                                 if (!string.IsNullOrEmpty(ExistingRecord.MD_FourthMeasure_FK.ToString()))
                                 {
                                     cmbFourthM.SelectedValue = ExistingRecord.MD_FourthMeasure_FK;
                                     txtMeasure4Qty.Text = ExistingRecord.MD_FourthMeasure_Qty.ToString();

                                 }
                                 else
                                     cmbFourthM.SelectedValue = 0;

                                 if (!string.IsNullOrEmpty(ExistingRecord.MD_FifthMeasure_FK.ToString()))
                                 {
                                     cmbFifthM.SelectedValue = ExistingRecord.MD_FifthMeasure_FK;
                                     txtMeasure5Qty.Text = ExistingRecord.MD_FifthMeasure_Qty.ToString();
                                 }
                                 else
                                     cmbFifthM.SelectedValue = 0;

                                 if (!string.IsNullOrEmpty(ExistingRecord.MD_SixMeasure_FK.ToString()))
                                 {
                                     cmbSixM.SelectedValue = ExistingRecord.MD_SixMeasure_FK;
                                     txtMeasure6Qty.Text = ExistingRecord.MD_SixMeasure_Qty.ToString();
                                 }
                                 else
                                     cmbSixM.SelectedValue = 0;

                                 if (!string.IsNullOrEmpty(ExistingRecord.MD_SevenMeasure_FK.ToString()))
                                 {
                                     cmbSeventhM.SelectedValue = ExistingRecord.MD_SevenMeasure_FK;
                                     txtMeasure7Qty.Text = ExistingRecord.MD_SevenMeasure_Qty.ToString();
                                 }
                                 else
                                     cmbSeventhM.SelectedValue = 0;

                                 if (!string.IsNullOrEmpty(ExistingRecord.MD_EightMeasure_FK.ToString()))
                                 {
                                     cmbEighthM.SelectedValue = ExistingRecord.MD_EightMeasure_FK;
                                     txtMeasure8Qty.Text = ExistingRecord.MD_EightMeasure_Qty.ToString();
                                 }
                                 else
                                     cmbEighthM.SelectedValue = 0;

                                 MandSelected = core.PopulateArray(MandatoryFields.Length, true);
                             }
                             else
                             {
                                 if (!addRecord &&!IsKnitting)
                                 {
                                     DialogResult res = MessageBox.Show("Record does not exist for selection made.Would you like to create one?", "Create new record", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                                     if (res == System.Windows.Forms.DialogResult.OK)
                                     {
                                         InitialiseConsumption();
                                         MandSelected[9] = false;
                                         MeasSelected = core.PopulateArray(MeasElements.Length, false);
                                     }
                                     else
                                     {
                                     }
                                 }
                             }
                         }
                     }
                     //-----------------------------------------------------
                 }
             }
        }

        private void txt(object sender, EventArgs e)
        {
            TextBox oTxtBx = sender as TextBox;
            if (oTxtBx != null && formloaded)
            {
               

                var result = (from u in MandatoryFields
                              where u[0] == oTxtBx.Name
                              select u).FirstOrDefault();

                int nbr = Convert.ToInt32(result[2].ToString());
                int length = Convert.ToInt32(result[4].ToString());
                if (oTxtBx.TextLength > 0 && oTxtBx.TextLength <= length)
                    MandSelected[nbr] = true;
                else
                {
                    MandSelected[nbr] = false;
                    if (oTxtBx.TextLength > length)
                        MessageBox.Show("Value entered exceeds allowable length");
                }

                if (nbr == 0 && addRecord)
                {
                    using (var context = new TTI2Entities())
                    {
                        /*
                        var Existing = context.TLADM_MachineDefinitions.Where(x => x.MD_MachineCode == txtMachineCode.Text).FirstOrDefault();
                        if (Existing != null)
                        {
                            MandSelected[nbr] = false;
                            txtMachineCode.Text = string.Empty;
                            MessageBox.Show("A record already exists fo the machine code entered"); 
                        }
                         */ 
                    }
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Util core = new Util();
            bool isKnitting = false;
            Button oBtn = sender as Button;
            TLADM_MachineDefinitions machdef;
            TLADM_MachineDefinitions MD = null;
            if (oBtn != null && formloaded)
            {
                var errorM = core.returnMessage(MandSelected, addRecord, MandatoryFields);

                if (!string.IsNullOrEmpty(errorM))
                {
                    MessageBox.Show(errorM);
                    return;
                }

                machdef = new TLADM_MachineDefinitions();

                if (!addRecord)
                {
                    MD = (TLADM_MachineDefinitions)cmbCodeSelection.SelectedItem;
                    if(MD == null)
                    {
                         MessageBox.Show("Please select a machine from the drop down provided");
                         return;
                    }
                }
                
                var FT = (TLADM_FabricProduct)cmbFabricProdType.SelectedItem;
                if (FT == null)
                {
                         MessageBox.Show("Please select a Fabric Product type");
                         return;
                }
                             
                using (var context = new TTI2Entities())
                {
                    var DeptDetail = (TLADM_Departments)cmbDepartmentSelect.SelectedItem;

                    if (DeptDetail.Dep_ShortCode.Contains("KNIT"))
                    {
                        isKnitting = true;

                        var GT = (Administration.CheckComboBoxItem)cmbGreigeProductType.SelectedItem;
                        if (GT == null)
                        {
                            MessageBox.Show("Please select a Greige Product type");
                            return;
                        }

                        if (!addRecord)
                        {
                            machdef = context.TLADM_MachineDefinitions.Where(x => x.MD_MachineCode == MD.MD_MachineCode && x.MD_GreigeType_FK == GT._Pk).FirstOrDefault();
                            if (machdef == null)
                                machdef = new TLADM_MachineDefinitions();
                        }

                    }
                    else
                    {
                        if (!addRecord)
                        {
                            machdef = context.TLADM_MachineDefinitions.Where(x => x.MD_MachineCode == MD.MD_MachineCode && x.MD_FabricType_FK == FT.FP_Id).FirstOrDefault();
                            if (machdef == null)
                                machdef = new TLADM_MachineDefinitions();
                        }
                    }
                     
                    
                    //-----------------------------------------------------
                    machdef.MD_AssetRegNo = txtAssetRegNo.Text;
                    machdef.MD_Department_FK = Convert.ToInt32(cmbDepartmentSelect.SelectedValue.ToString());
                    machdef.MD_Description = txtMacineDescription.Text;
                    if(cmbFinishedGoods.SelectedValue != null)
                        machdef.MD_FinishGoods_FK = Convert.ToInt32(cmbFinishedGoods.SelectedValue.ToString());
                    machdef.MD_GLCostCentre = txtGLCostCentre.Text;
                    machdef.MD_MachineCode = txtMachineCode.Text;
                    machdef.MD_MaxCapacity = Convert.ToDecimal(txtMaxCapacity.Text);
                    machdef.MD_Operators_TotalPN = 0;
                    machdef.MD_FabricType_FK = Convert.ToInt32(cmbFabricProdType.SelectedValue.ToString());
                    machdef.MD_Realistic = Convert.ToDecimal(txtRealistic.Text);
                    machdef.MD_SerialNo = txtSerialNo.Text;
                    machdef.MD_LastNumberUsed = Int32.Parse(txtLastNumberUsed.Text);
                   
                    if (isKnitting)
                    {
                        var gt = (Administration.CheckComboBoxItem)cmbGreigeProductType.SelectedItem;
                        if (gt != null)
                        {
                            machdef.MD_AlternateDesc = txtMachineCode.Text + " " + gt.Text;
                            machdef.MD_GreigeType_FK = gt._Pk;
                        }
                    }
                    else
                    {
                        var ft = (TLADM_FabricProduct)cmbFabricProdType.SelectedItem;
                        machdef.MD_AlternateDesc = txtMachineCode.Text + " " + ft.FP_Description;
                    }

                    if(core.IsValueDidgit(btnMaintenance.Text))
                        machdef.MD_Maintenance_TotalPN = Convert.ToInt32(btnMaintenance.Text);
                    
                    //--------------------------------------------------------------------------
                    machdef.MD_FirstMeasure_FK = null;
                    machdef.MD_SecMeasure_FK = null;
                    machdef.MD_ThirdMeasure_FK = null;
                    machdef.MD_FourthMeasure_FK = null;
                    machdef.MD_SixMeasure_FK = null;
                    machdef.MD_SevenMeasure_FK = null;
                    machdef.MD_EightMeasure_FK = null;
                    //------------------------------------------------------------------------------
                    
                    if (MeasSelected[0])
                    {
                        machdef.MD_FirstMeasure_FK = Convert.ToInt32(cmbFirstM.SelectedValue.ToString());
                        if(txtMeasure1Qty.TextLength != 0)
                             machdef.MD_FirstMeasure_Qty = Convert.ToDecimal(txtMeasure1Qty.Text);

                    }

                    if (MeasSelected[1])
                    {
                        machdef.MD_SecMeasure_FK = Convert.ToInt32(cmbSecondM.SelectedValue.ToString());
                        if (txtMeasure2Qty.TextLength != 0)
                             machdef.MD_SecMeasure_Qty = Convert.ToDecimal(txtMeasure2Qty.Text);
                    }

                    if (MeasSelected[2])
                    {
                        machdef.MD_ThirdMeasure_FK = Convert.ToInt32(cmbThirdM.SelectedValue.ToString());
                        if (txtMeasure3Qty.TextLength != 0)
                            machdef.MD_ThirdMeasure_Qty = Convert.ToDecimal(txtMeasure3Qty.Text);
                    }

                    if (MeasSelected[3])
                    {
                        machdef.MD_FourthMeasure_FK = Convert.ToInt32(cmbFourthM.SelectedValue.ToString());
                        if (txtMeasure4Qty.TextLength != 0)
                            machdef.MD_FourthMeasure_Qty = Convert.ToDecimal(txtMeasure4Qty.Text);
                    }

                    if (MeasSelected[4])
                    {
                        machdef.MD_FifthMeasure_FK = Convert.ToInt32(cmbFifthM.SelectedValue.ToString());
                        if (txtMeasure5Qty.TextLength != 0)
                           machdef.MD_FifthMeasure_Qty = Convert.ToDecimal(txtMeasure5Qty.Text);
                    }

                    if (MeasSelected[5])
                    {
                        machdef.MD_SixMeasure_FK = Convert.ToInt32(cmbSeventhM.SelectedValue.ToString());
                        if (txtMeasure6Qty.TextLength != 0)
                            machdef.MD_SixMeasure_Qty = Convert.ToDecimal(txtMeasure6Qty.Text);
                    }

                    if (MeasSelected[6])
                    {
                        machdef.MD_SevenMeasure_FK = Convert.ToInt32(cmbSeventhM.SelectedValue.ToString());
                        if (txtMeasure7Qty.TextLength != 0)
                            machdef.MD_SevenMeasure_Qty = Convert.ToDecimal(txtMeasure7Qty.Text);
                    }

                    if (MeasSelected[7])
                    {
                        machdef.MD_EightMeasure_FK = Convert.ToInt32(cmbEighthM.SelectedValue.ToString());
                        if (txtMeasure8Qty.TextLength != 0)
                            machdef.MD_EightMeasure_Qty = Convert.ToDecimal(txtMeasure8Qty.Text);
                    }

                    if (addRecord)
                        context.TLADM_MachineDefinitions.Add(machdef);
                    try
                    {
                        context.SaveChanges();
                        formloaded = false;
                        cmbCodeSelection.DataSource = context.TLADM_MachineDefinitions.OrderBy(x => x.MD_MachineCode).GroupBy(x => x.MD_MachineCode).Select(grp => grp.FirstOrDefault()).ToList();

                        if(addRecord)
                        {
                            var Dept = context.TLADM_Departments.Find(machdef.MD_Department_FK);
                            if (Dept != null && Dept.Dep_ShortCode.Contains("KNIT"))
                            {
                                TLKNI_MachineLastNumber LastN = new TLKNI_MachineLastNumber();
                                LastN.TLMDD_Machine_FK = machdef.MD_Pk;
                                LastN.TLMDD_MachineCode = machdef.MD_MachineCode;

                                context.TLKNI_MachineLastNumber.Add(LastN);

                                try
                                {
                                    context.SaveChanges();
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message);
                                }
                            }
                        }
                        formloaded = true;

                        MeasSelected = core.PopulateArray(MeasElements.Length, false);
                        MessageBox.Show(core.SuccessFullTransAction());
                        SetUp(false);

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                }
                //--------------------------------------------------------------------------
            }
        }

       
        private void InitialiseConsumption()
        {
            MeasSelected = core.PopulateArray(MeasElements.Length, false);

            cmbFirstM.SelectedValue = 0;
            cmbSecondM.SelectedValue = 0;
            cmbThirdM.SelectedValue = 0;
            cmbFourthM.SelectedValue = 0;
            cmbFifthM.SelectedValue = 0;
            cmbSixM.SelectedValue = 0;
            cmbSeventhM.SelectedValue = 0;
            cmbEighthM.SelectedValue = 0;

            txtMeasure1Qty.Text = "0.00";
            txtMeasure2Qty.Text = "0.00";
            txtMeasure3Qty.Text = "0.00";
            txtMeasure4Qty.Text = "0.00";
            txtMeasure5Qty.Text = "0.00";
            txtMeasure6Qty.Text = "0.00";
            txtMeasure7Qty.Text = "0.00";
            txtMeasure8Qty.Text = "0.00";

        }

        private void cmbCodeSelection_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmb = sender as ComboBox;
         

            if (oCmb != null && formloaded)
            {
                InitialiseConsumption();
               

                var Selected = (TLADM_MachineDefinitions)cmbCodeSelection.SelectedItem;
                if (Selected != null)
                {
                    MandSelected[0] = true;
                    MandSelected[2] = true;
                    txtMachineCode.Text = Selected.MD_MachineCode;
                    MeasSelected = core.PopulateArray(MeasElements.Length, false);
                    
                    try
                    {
                        cmbDepartmentSelect.SelectedValue = Selected.MD_Department_FK;
                        cmbFinishedGoods.SelectedValue = Selected.MD_FinishGoods_FK;
                        cmbFabricProdType.SelectedValue = Selected.MD_FabricType_FK;
                        if (Selected.MD_GreigeType_FK != null)
                            cmbGreigeProductType.SelectedValue = Selected.MD_GreigeType_FK;

                        if (Selected.MD_FirstMeasure_FK != null)
                        {
                            cmbFirstM.SelectedValue = Selected.MD_FirstMeasure_FK;
                            txtMeasure1Qty.Text = Selected.MD_FirstMeasure_Qty.ToString();
                            MeasSelected[0] = true;

                        }


                        if (Selected.MD_SecMeasure_FK != null)
                        {
                            cmbSecondM.SelectedValue = Selected.MD_SecMeasure_FK;
                            txtMeasure2Qty.Text = Selected.MD_SecMeasure_Qty.ToString();
                            MeasSelected[1] = true;
                        }


                        if (Selected.MD_ThirdMeasure_FK != null)
                        {
                            cmbThirdM.SelectedValue = Selected.MD_ThirdMeasure_FK;
                            txtMeasure3Qty.Text = Selected.MD_ThirdMeasure_Qty.ToString();
                            MeasSelected[2] = true;
                        }


                        if (Selected.MD_FourthMeasure_FK != null)
                        {
                            cmbFourthM.SelectedValue = Selected.MD_FourthMeasure_FK;
                            txtMeasure4Qty.Text = Selected.MD_FourthMeasure_Qty.ToString();
                            MeasSelected[3] = true;
                        }


                        if (Selected.MD_FifthMeasure_FK != null)
                        {
                            cmbFifthM.SelectedValue = Selected.MD_FifthMeasure_FK;
                            txtMeasure5Qty.Text = Selected.MD_FifthMeasure_Qty.ToString();
                            MeasSelected[4] = true;
                        }


                        if (Selected.MD_SixMeasure_FK != null)
                        {
                            cmbSixM.SelectedValue = Selected.MD_SixMeasure_FK;
                            txtMeasure1Qty.Text = Selected.MD_SixMeasure_Qty.ToString();
                            MeasSelected[5] = true;
                        }


                        if (Selected.MD_SevenMeasure_FK != null)
                        {
                            cmbSeventhM.SelectedValue = Selected.MD_SevenMeasure_FK;
                            txtMeasure7Qty.Text = Selected.MD_SevenMeasure_Qty.ToString();
                            MeasSelected[6] = true;
                        }


                        if (Selected.MD_EightMeasure_FK != null)
                        {
                            cmbEighthM.SelectedValue = Selected.MD_EightMeasure_FK;
                            txtMeasure8Qty.Text = Selected.MD_EightMeasure_Qty.ToString();
                            MeasSelected[7] = true;
                        }

                        if (Selected.MD_GreigeType_FK != null)
                        {
                            using (var context = new TTI2Entities())
                            {
                                var Existing = context.TLADM_MachineDefinitions.Where(x => x.MD_MachineCode == Selected.MD_MachineCode).ToList();
                                foreach (var Row in Existing)
                                {
                                    var index = cmbGreigeProductType.Items.IndexOf(Row.MD_Pk);
                                }
                            }
                        }

                        
                    }
                    catch (Exception ex)
                    {
                         MessageBox.Show(ex.Message);
                    }
                  
                    MandSelected = core.PopulateArray(MandatoryFields.Length, true);

                    if (Selected.MD_GreigeType_FK != null)
                    {
                        using (var context = new TTI2Entities())
                        {
                           // var Existing = cmbCodeSelection.Items.Cast<TLADM_MachineDefinitions>().Where(x => x.MD_MachineCode == Selected.MD_MachineCode).ToList();
                            var Existing = context.TLADM_MachineDefinitions.Where(x => x.MD_MachineCode == Selected.MD_MachineCode).ToList(); 
                            foreach (var Row in Existing)
                            {
                                var selectedObject = cmbGreigeProductType.Items.Cast<Administration.CheckComboBoxItem>().SingleOrDefault(i => i._Pk.Equals(Row.MD_GreigeType_FK));
                                if (selectedObject != null)
                                {
                                    selectedObject.CheckState = true;
                                }
                            }
                        }
                    }
                }
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;
            if (oBtn != null && formloaded)
            {
                SetUp(false);
                addRecord = true;
                MandSelected = core.PopulateArray(MandatoryFields.Length, false);
                MeasSelected = core.PopulateArray(MeasElements.Length, false);
            }
        }

        private void cmb_DisplayMemberChanged(object sender, EventArgs e)
        {
            ComboBox oCmb = sender as ComboBox;
            if (oCmb != null && formloaded)
            {
                
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;
            if (oBtn != null)
            {
                
                frmTLADMGardProp depts = new frmTLADMGardProp(6000, Convert.ToInt32(oBtn.Text));
                depts.ShowDialog(this);

                oBtn.Text = depts.TotalPN.ToString();
            }
        }

      

        private void txt_KeyDownOEM(object sender, KeyEventArgs e)
        {

        }

        private void cmbGreigeProductType_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = sender as ComboBox;
            if (oCmbo != null && formloaded)
            {
                var SelectedObject = (Administration.CheckComboBoxItem)oCmbo.SelectedItem;
                if (SelectedObject.CheckState)
                {

                    using (var context = new TTI2Entities())
                    {
                        var MachineRecord = context.TLADM_MachineDefinitions
                                            .Where(m => m.MD_MachineCode == txtMachineCode.Text
                                                       && m.MD_GreigeType_FK == SelectedObject._Pk).FirstOrDefault();
                        if (MachineRecord != null)
                        {
                            InitialiseConsumption();

                            txtAssetRegNo.Text = MachineRecord.MD_AssetRegNo;
                            txtGLCostCentre.Text = MachineRecord.MD_GLCostCentre;
                            txtMachineCode.Text = MachineRecord.MD_MachineCode;
                            txtMacineDescription.Text = MachineRecord.MD_Description;
                            txtMaxCapacity.Text = MachineRecord.MD_MaxCapacity.ToString();
                            txtRealistic.Text = MachineRecord.MD_Realistic.ToString();
                            txtSerialNo.Text = MachineRecord.MD_SerialNo;
                            txtLastNumberUsed.Text = MachineRecord.MD_LastNumberUsed.ToString();

                            btnMaintenance.Text = MachineRecord.MD_Maintenance_TotalPN.ToString();

                            try
                            {
                                cmbDepartmentSelect.SelectedValue = MachineRecord.MD_Department_FK;
                                cmbFinishedGoods.SelectedValue = MachineRecord.MD_FinishGoods_FK;
                                cmbFabricProdType.SelectedValue = MachineRecord.MD_FabricType_FK;
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }

                            if (!string.IsNullOrEmpty(MachineRecord.MD_FirstMeasure_FK.ToString()))
                            {
                                cmbFirstM.SelectedValue = MachineRecord.MD_FirstMeasure_FK;
                                txtMeasure1Qty.Text = MachineRecord.MD_FirstMeasure_Qty.ToString();

                            }
                            else
                                cmbFirstM.SelectedValue = 0;

                            if (!string.IsNullOrEmpty(MachineRecord.MD_SecMeasure_FK.ToString()))
                            {
                                cmbSecondM.SelectedValue = MachineRecord.MD_SecMeasure_FK;
                                txtMeasure2Qty.Text = MachineRecord.MD_SecMeasure_Qty.ToString();
                            }
                            else
                                cmbSecondM.SelectedValue = 0;

                            if (!string.IsNullOrEmpty(MachineRecord.MD_ThirdMeasure_FK.ToString()))
                            {
                                cmbThirdM.SelectedValue = MachineRecord.MD_ThirdMeasure_FK;
                                txtMeasure3Qty.Text = MachineRecord.MD_ThirdMeasure_Qty.ToString();

                            }
                            else

                                cmbThirdM.SelectedValue = 0;

                            if (!string.IsNullOrEmpty(MachineRecord.MD_FourthMeasure_FK.ToString()))
                            {
                                cmbFourthM.SelectedValue = MachineRecord.MD_FourthMeasure_FK;
                                txtMeasure4Qty.Text = MachineRecord.MD_FourthMeasure_Qty.ToString();

                            }
                            else
                                cmbFourthM.SelectedValue = 0;

                            if (!string.IsNullOrEmpty(MachineRecord.MD_FifthMeasure_FK.ToString()))
                            {
                                cmbFifthM.SelectedValue = MachineRecord.MD_FifthMeasure_FK;
                                txtMeasure5Qty.Text = MachineRecord.MD_FifthMeasure_Qty.ToString();
                            }
                            else
                                cmbFifthM.SelectedValue = 0;

                            if (!string.IsNullOrEmpty(MachineRecord.MD_SixMeasure_FK.ToString()))
                            {
                                cmbSixM.SelectedValue = MachineRecord.MD_SixMeasure_FK;
                                txtMeasure6Qty.Text = MachineRecord.MD_SixMeasure_Qty.ToString();
                            }
                            else
                                cmbSixM.SelectedValue = 0;

                            if (!string.IsNullOrEmpty(MachineRecord.MD_SevenMeasure_FK.ToString()))
                            {
                                cmbSeventhM.SelectedValue = MachineRecord.MD_SevenMeasure_FK;
                                txtMeasure7Qty.Text = MachineRecord.MD_SevenMeasure_Qty.ToString();
                            }
                            else
                                cmbSeventhM.SelectedValue = 0;

                            if (!string.IsNullOrEmpty(MachineRecord.MD_EightMeasure_FK.ToString()))
                            {
                                cmbEighthM.SelectedValue = MachineRecord.MD_EightMeasure_FK;
                                txtMeasure8Qty.Text = MachineRecord.MD_EightMeasure_Qty.ToString();
                            }
                            else
                                cmbEighthM.SelectedValue = 0;

                            MandSelected = core.PopulateArray(MandatoryFields.Length, true);

                        }
                    }
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            TLADM_MachineDefinitions MachDef = null;
            Button oBtn = sender as Button;
            if (oBtn != null && formloaded)
            {
                DialogResult res = MessageBox.Show("Please confirm this action ", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (res == DialogResult.Yes)
                {
                    var Mach = (TLADM_MachineDefinitions)cmbCodeSelection.SelectedItem;
                    if (Mach == null)
                    {
                        MessageBox.Show("Please select a machine from the drop down box");
                        return;
                    }
                    using ( var context = new TTI2Entities())
                    {
                         MachDef = context.TLADM_MachineDefinitions.Find(Mach.MD_Pk);

                         if(MachDef != null)
                         {
                             context.TLADM_MachineDefinitions.Remove(MachDef);
                             try
                             {
                                 context.SaveChanges();
                                 MessageBox.Show("Data successfully deleted from the database");
                                 SetUp(false);
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
}
