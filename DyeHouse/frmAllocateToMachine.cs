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

namespace DyeHouse
{
    public partial class frmAllocateToMachine : Form
    {
        bool formloaded;
        Util core;
        bool Add;

        string[][] MandatoryFields;
        bool[] MandSelected;

        protected readonly TTI2Entities _context;

        public frmAllocateToMachine()
        {

            InitializeComponent();
            _context = new TTI2Entities();

            MandatoryFields = new string[][]
                {   new string[] {"dtpAllocationDate", "Please select an allocation date", "0", "10", "D"},
                    new string[] {"cmboDyeBatch", "Please select a dye batch", "1", "10", ""},
                    new string[] {"cmboDyeMachines", "Please select a machine", "2", "", ""}
                 };

            core = new Util();
            SetUp(true);


        }

        void SetUp(bool status)
        {
            formloaded = false;

            txtBatchColour.Text = string.Empty;
            txtBatchQuality.Text = string.Empty;
            txtBatchWeight.Text = string.Empty;

            List<TLADM_MachineDefinitions> Machines = new List<TLADM_MachineDefinitions>();

            using (var context = new TTI2Entities())
            {
                cmboDyeBatch.DataSource = _context.TLDYE_DyeBatch.Where(x => !x.DYEB_Closed && x.DYEB_Transfered && !x.DYEB_Allocated).OrderBy(x => x.DYEB_BatchNo).ToList();
                cmboDyeBatch.DisplayMember = "DYEB_BatchNo";
                cmboDyeBatch.ValueMember = "DYEB_Pk";
                cmboDyeBatch.SelectedIndex = -1;
                cmboDyeBatch.Text = string.Empty;

                var Dept = _context.TLADM_Departments.Where(x => x.Dep_ShortCode.Contains("DYE")).FirstOrDefault();
                if (Dept != null)
                {
                    cmboDyeMachines.DataSource = _context.TLADM_MachineDefinitions.Where(x => x.MD_Department_FK == Dept.Dep_Id).ToList();
                    cmboDyeMachines.DisplayMember = "MD_AlternateDesc";
                    cmboDyeMachines.ValueMember = "MD_Pk";
                    cmboDyeMachines.SelectedIndex = -1;
                    cmboDyeMachines.Text = string.Empty;
                }
            }

            if (status)
                MandSelected = core.PopulateArray(MandatoryFields.Length, false);
            Add = true;

            txtFabricType.Text = string.Empty;
            txtFabricType.ReadOnly = true;

            formloaded = true;
        }

        private void cmboDyeMachines_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = sender as ComboBox;
            if (oCmbo != null && formloaded)
            {
                var selected = (TLADM_MachineDefinitions)cmboDyeMachines.SelectedItem;
                if (selected != null)
                {
                    label4.Text = selected.MD_AlternateDesc;

                    using (var context = new TTI2Entities())
                    {
                        formloaded = false;
                        txtFabricType.Text = context.TLADM_FabricProduct.Find(selected.MD_FabricType_FK).FP_Description;
                        formloaded = true;
                    }

                    var result = (from u in MandatoryFields
                                  where u[0] == oCmbo.Name
                                  select u).FirstOrDefault();

                    if (result != null)
                    {
                        int nbr = Convert.ToInt32(result[2].ToString());
                        MandSelected[nbr] = true;
                    }

                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;
            if (oBtn != null && formloaded)
            {
                var errorM = core.returnMessage(MandSelected, true, MandatoryFields);
                if (!string.IsNullOrEmpty(errorM))
                {
                    MessageBox.Show(errorM);
                    return;
                }

                var Selected = (TLADM_MachineDefinitions)cmboDyeMachines.SelectedItem;
                if (Selected == null)
                {
                    MessageBox.Show("Please select a machine from the drop down box provided");
                    return;
                }

                TLDYE_DyeBatchAllocated dba = new TLDYE_DyeBatchAllocated();

                dba.TLDYEA_AllocateDate = dtpAllocationDate.Value;
                dba.TLDYEA_DyeBatch_FK = (int)cmboDyeBatch.SelectedValue;
                dba.TLDYEA_FabricType_FK = Selected.MD_FabricType_FK;
                dba.TLDYEA_MachCode_FK = (int)cmboDyeMachines.SelectedValue;

                if (Add)
                {
                    _context.TLDYE_DyeBatchAllocated.Add(dba);

                    var db = _context.TLDYE_DyeBatch.Find((int)cmboDyeBatch.SelectedValue);
                    if (db != null)
                    {
                        db.DYEB_Allocated = true;
                    }

                    try
                    {
                        UpdateDyeKitchen_Balance(db.DYEB_Colour_FK, db.DYEB_BatchKG);
                        _context.SaveChanges();

                        MessageBox.Show("Data saved to database successfully");
                        SetUp(false);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }

        private void cmboDyeBatch_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = sender as ComboBox;
            if (oCmbo != null && formloaded)
            {
                var result = (from u in MandatoryFields
                              where u[0] == oCmbo.Name
                              select u).FirstOrDefault();

                if (result != null)
                {
                    int nbr = Convert.ToInt32(result[2].ToString());
                    MandSelected[nbr] = true;
                }

                var selected = (TLDYE_DyeBatch)oCmbo.SelectedItem;

                using (var context = new TTI2Entities())
                {
                    txtBatchColour.Text = context.TLADM_Colours.Find(selected.DYEB_Colour_FK).Col_Display;
                    txtBatchWeight.Text = ((TLDYE_DyeBatch)oCmbo.SelectedItem).DYEB_BatchKG.ToString();
                    var BD = context.TLDYE_DyeBatchDetails.Where(x => x.DYEBD_DyeBatch_FK == selected.DYEB_Pk && x.DYEBD_BodyTrim).FirstOrDefault();
                    if (BD != null)
                    {
                        txtBatchQuality.Text = context.TLADM_Griege.Find(BD.DYEBD_QualityKey).TLGreige_Description;
                    }

                }
            }
        }

        private void dtpAllocationDate_ValueChanged(object sender, EventArgs e)
        {
            DateTimePicker oDtp = sender as DateTimePicker;
            if (oDtp != null && formloaded)
            {
                var result = (from u in MandatoryFields
                              where u[0] == oDtp.Name
                              select u).FirstOrDefault();

                if (result != null)
                {
                    int nbr = Convert.ToInt32(result[2].ToString());
                    MandSelected[nbr] = true;
                }
            }
        }

        private void cmboFabricType_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = sender as ComboBox;
            if (oCmbo != null && formloaded)
            {
                var result = (from u in MandatoryFields
                              where u[0] == oCmbo.Name
                              select u).FirstOrDefault();

                if (result != null)
                {
                    int nbr = Convert.ToInt32(result[2].ToString());
                    MandSelected[nbr] = true;
                }
            }
        }

        private void UpdateDyeKitchen_Balance(int ColorPk, decimal TotalWeight)
        {

            var ReceipeDefinition = _context.TLDYE_RecipeDefinition.Where(x => x.TLDYE_ColorChart_FK == ColorPk).FirstOrDefault();
            if (ReceipeDefinition != null)
            {
                var ReceipeDetails = _context.TLDYE_DefinitionDetails.Where(x => x.TLDYED_Receipe_FK == ReceipeDefinition.TLDYE_DefinePk).ToList();
                foreach (var ReceipeRecord in ReceipeDetails)
                {
                    var ConsDC = _context.TLADM_ConsumablesDC.Find(ReceipeRecord.TLDYED_Cosumables_FK);

                    if (ConsDC == null || (bool)ConsDC.ConsDC_Discontinued)
                        continue;

                    Decimal ExpectedConsumption = 0.00M;

                    if (!ReceipeRecord.TLDYED_LiqCalc)
                    {
                        //--------------------------------------------------------
                        // The MELFC is a percentage of the receipe Program Load 
                        //-------------------------------------------------------------------------------------
                        ExpectedConsumption = (ReceipeRecord.TLDYED_MELFC * TotalWeight) / 100;
                    }
                    else
                    {
                        //----------------------------------------------------------
                        // Now use Liquidity factor 
                        //-------------------------------------------------------------
                        ExpectedConsumption = (ReceipeRecord.TLDYED_MELFC * TotalWeight * ReceipeRecord.TLDYED_LiqRatio) / 1000;
                    }

                    //-----------------------------------
                    //Find this Record in the SOH
                    //----------------------------------------------
                    var SOH = _context.TLDYE_ConsumableSOH.Where(x => x.DYCSH_Consumable_FK == ReceipeRecord.TLDYED_Cosumables_FK).FirstOrDefault();
                    if (SOH != null)
                    {
                        if ((SOH.DYCSH_SOHKitchen - ExpectedConsumption) >= ExpectedConsumption)
                        {
                            SOH.DYCSH_SOHKitchen -= ExpectedConsumption;
                        }
                        else
                        {
                            SOH.DYCSH_SOHKitchen = 0.00M;
                        }
                    }
                }
            }
        }

        private void frmAllocateToMachine_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!e.Cancel)
            {
                if (_context != null)
                {
                    _context.Dispose();
                }
            }
        }
    }
}
