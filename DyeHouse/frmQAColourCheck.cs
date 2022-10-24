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
    public partial class frmQAColourCheck : Form
    {
        Util core;
        bool formloaded;

        DataGridViewTextBoxColumn  oTxtA0 = new DataGridViewTextBoxColumn();
        DataGridViewComboBoxColumn oCmbA = new DataGridViewComboBoxColumn();
        DataGridViewTextBoxColumn  oTxtA1 = new DataGridViewTextBoxColumn();
       
        public frmQAColourCheck()
        {
            InitializeComponent();
            core = new Util();
            SetUp();

        }

        void SetUp()
        {
            formloaded = false;
            rbBtnNo.Checked = true;

            using ( var context = new TTI2Entities())
            {
                var Existing = from T1 in context.TLDYE_DyeBatchAllocated
                               join T2 in context.TLDYE_DyeBatch on T1.TLDYEA_DyeBatch_FK equals T2.DYEB_Pk 
                               where T2.DYEB_Allocated && !T2.DYEB_OutProcess && !T2.DYEB_Stage1  
                               orderby T2.DYEB_BatchNo 
                               select T2;

                cmboBatchNo.DataSource = Existing.ToList();
                cmboBatchNo.ValueMember = "DYEB_Pk";
                cmboBatchNo.DisplayMember = "DYEB_BatchNo";
                cmboBatchNo.SelectedValue = -1;

                oCmbA.DataSource = context.TLADM_NonStockItems.OrderBy(x => x.NSI_Description).ToList();

                var Dept = context.TLADM_Departments.Where(x=>x.Dep_ShortCode.Contains("DYE")).FirstOrDefault();
                if (Dept != null)
                {
                    cmboOperator.DataSource = context.TLADM_MachineOperators.Where(x => x.MachOp_Department_FK == Dept.Dep_Id && !x.MachOp_Discontinued).ToList();
                    cmboOperator.ValueMember = "MachOp_Pk";
                    cmboOperator.DisplayMember = "MachOp_Description";
                    cmboOperator.SelectedValue = -1;
                }
           }

            formloaded = true;
        }

        private void cmboBatchNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = sender as ComboBox;
            if (oCmbo != null && formloaded)
            {
                var selected = (TLDYE_DyeBatch)cmboBatchNo.SelectedItem;
                if(selected != null)
                {
                    using (var context = new TTI2Entities())
                    {
                        if (!selected.DYEB_CommissinCust)
                        {
                            var DO = context.TLDYE_DyeOrder.Find(selected.DYEB_DyeOrder_FK);
                            if (DO != null)
                            {
                                var color = context.TLADM_Colours.Find(selected.DYEB_Colour_FK);
                                if (color != null)
                                {
                                    txtColour.Text = color.Col_Display;
                                }

                                var DOD = context.TLDYE_DyeOrderDetails.Where(x => x.TLDYOD_DyeOrder_Fk == DO.TLDYO_Pk);
                                if (DOD != null)
                                {
                                    var count = 0;
                                    foreach (var record in DOD)
                                    {
                                        var Qual = context.TLADM_Griege.Find(record.TLDYOD_Greige_FK);
                                        if (Qual != null)
                                        {
                                            if (record.TLDYOD_BodyOrTrim)
                                                txtBodyQuality.Text = Qual.TLGreige_Description;
                                            else
                                            {
                                                if (++count == 1)
                                                    txtTrim1Qual.Text = Qual.TLGreige_Description;
                                                else if (count == 2)
                                                    txtTrim2Qual.Text = Qual.TLGreige_Description;
                                                else if (count == 3)
                                                    txtTrim3Qual.Text = Qual.TLGreige_Description;
                                                else
                                                    txtTrim4Qual.Text = Qual.TLGreige_Description;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            txtColour.Text = context.TLADM_Colours.Find(selected.DYEB_Colour_FK).Col_Display;
                            var Existing = context.TLDYE_DyeBatchDetails.Where(x => x.DYEBD_DyeBatch_FK == selected.DYEB_Pk).GroupBy(x=> new { x.DYEBD_QualityKey, x.DYEBO_GVRowNumber});
                            var count = 0;
                            foreach (var row in Existing)
                            {

                                if (row.FirstOrDefault().DYEBD_BodyTrim)
                                    txtBodyQuality.Text = context.TLADM_Griege.Find(row.FirstOrDefault().DYEBD_QualityKey).TLGreige_Description;
                                else
                                {
                                    if(++count == 1)
                                    {
                                        txtTrim1Qual.Text = context.TLADM_Griege.Find(row.FirstOrDefault().DYEBD_QualityKey).TLGreige_Description;
                                    }
                                    else if (count == 2)
                                    {
                                        txtTrim2Qual.Text = context.TLADM_Griege.Find(row.FirstOrDefault().DYEBD_QualityKey).TLGreige_Description;
                                    }
                                    else if (count == 3)
                                    {
                                        txtTrim3Qual.Text = context.TLADM_Griege.Find(row.FirstOrDefault().DYEBD_QualityKey).TLGreige_Description;
                                    }
                                    else
                                    {
                                        txtTrim4Qual.Text = context.TLADM_Griege.Find(row.FirstOrDefault().DYEBD_QualityKey).TLGreige_Description;
                                    }
                                }
      
                            }
                        }
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;
            IList<TLDYE_DyeBatchDetails> DBDetails = new List<TLDYE_DyeBatchDetails>();
            IList<TLADM_Departments> Depts = new List<TLADM_Departments>();

            bool Add = true;
            if (oBtn != null && formloaded)
            {
               
                var Selected = (TLDYE_DyeBatch)cmboBatchNo.SelectedItem;
                if (Selected == null)
                {
                    MessageBox.Show("Please select a dyebatch from the drop down box");
                    return;
                }

                var SelectedOp = (TLADM_MachineOperators)cmboOperator.SelectedItem;
                if (SelectedOp == null)
                {
                    MessageBox.Show("Please select an operator from the drop down list");
                    return;
                }

                using (var context = new TTI2Entities())
                {
                    Depts = context.TLADM_Departments.ToList();

                    var DB = context.TLDYE_DyeBatch.Find(Selected.DYEB_Pk);
                    if (DB != null)
                    {
                        //-------------------------------------------------
                        // This does the dye batch
                        //------------------------------------------------------------
                        if (rbBtnNo.Checked)
                            DB.DYEB_Stage1 = true;

                        var Dept = Depts.Where(x => x.Dep_ShortCode.Contains("DYE")).FirstOrDefault();
                        if (Dept != null)
                        {
                            TLDYE_DyeTransactions trns = new TLDYE_DyeTransactions();
                            trns = context.TLDYE_DyeTransactions.Where(x => x.TLDYET_Batch_FK == DB.DYEB_Pk && x.TLDYET_Stage == 3).FirstOrDefault();
                            if (trns == null)
                            {
                                trns = new TLDYE_DyeTransactions();
                            }
                            else
                            {
                                Add = false;
                            }

                            trns.TLDYET_BatchNo = DB.DYEB_BatchNo;
                            trns.TLDYET_BatchWeight = DB.DYEB_BatchKG;
                            trns.TLDYET_Date = DateTime.Now;
                            trns.TLDYET_SequenceNo = DB.DYEB_SequenceNo;
                            trns.TLDYET_Batch_FK = DB.DYEB_Pk;
                            trns.TLDYET_Stage = 3;
                            if (rbBtnNo.Checked)
                                trns.TLDYET_Rejected = false;
                            else
                            {
                                trns.TLDYET_Rejected = true;
                                trns.TLDYET_RejectDate = DateTime.Now;
                            }

                            var TT = context.TLADM_TranactionType.Where(x => x.TrxT_Department_FK == Dept.Dep_Id && x.TrxT_Number == 400).FirstOrDefault();
                            if (TT != null)
                            {
                                trns.TLDYET_TransactionType = TT.TrxT_Pk;
                                trns.TLDYET_CurrentStore_FK = (int)TT.TrxT_ToWhse_FK;
                            }

                            if (Add)
                                context.TLDYE_DyeTransactions.Add(trns);

                            DBDetails = context.TLDYE_DyeBatchDetails.Where(x => x.DYEBD_DyeBatch_FK == DB.DYEB_Pk).ToList();
                            foreach (var Record in DBDetails)
                            {
                                Record.DYEBO_CurrentStore_FK = trns.TLDYET_CurrentStore_FK;
                            }

                        }
                        //-------------------------------------------------------------------------
                        // This code assign operators 
                        //----------------------------------------------------------------
                        var selected = (TLADM_MachineOperators)cmboOperator.SelectedItem;
                        if(selected != null)
                        {
                            var allop = context.TLDYE_AllocatedOperator.Where(x => x.DYEOP_BatchNo_FK == DB.DYEB_Pk).FirstOrDefault();
                            if (allop == null)
                            {
                                allop = new TLDYE_AllocatedOperator();
                                allop.DYEOP_BatchDate = DateTime.Now;
                                allop.DYEOP_Operator_FK = selected.MachOp_Pk;
                                allop.DYEOP_BatchNo_FK = DB.DYEB_Pk;

                                context.TLDYE_AllocatedOperator.Add(allop);
                            }
                            else
                            {
                                allop.DYEOP_BatchDate = DateTime.Now;
                                allop.DYEOP_Operator_FK = selected.MachOp_Pk;
                            }
                        }

                        //if (DB.DYEB_SequenceNo == 0)
                        //{  
                        //---------------------------------------------------------------------
                        // 1st thing get the colour of the batch
                        //---------------------------------------------------------------------
                            var Colour = context.TLADM_Colours.Find(DB.DYEB_Colour_FK);
                            //----------------------------------------------------------------
                            // Now get the Receipe for this colour
                            //----------------------------------------------------------------
                            var Receipe = context.TLDYE_RecipeDefinition.Where(x => x.TLDYE_ColorChart_FK == DB.DYEB_Colour_FK).FirstOrDefault();
                            if (Receipe != null)
                            {
                                var ReceipeDefinition = context.TLDYE_DefinitionDetails.Where(x => x.TLDYED_Receipe_FK == Receipe.TLDYE_DefinePk).ToList();
                                foreach (var Definition in ReceipeDefinition)
                                {
                                    var ConsDC = context.TLADM_ConsumablesDC.Find(Definition.TLDYED_Cosumables_FK);
                                    if (ConsDC.ConsDC_Discontinued)
                                        continue;

                                    if (!Definition.TLDYED_LiqCalc)
                                    {
                                        var Kgs = (Definition.TLDYED_MELFC * DB.DYEB_BatchKG) / 100;
                                        //-----------------------------------
                                        //Find this Record in the SOH
                                        //----------------------------------------------
                                        if (Dept != null)
                                        {
                                            var SOH = context.TLDYE_ConsumableSOH.Where(x => x.DYCSH_Consumable_FK == Definition.TLDYED_Cosumables_FK
                                                && x.DYCSH_DyeKitchen).FirstOrDefault();
                                            //------------------------
                                            // Now there might be NO stock on hand for this item
                                            //--------------------------------------------------------
                                            if (SOH == null)
                                            {
                                                /* var Whse = context.TLADM_WhseStore.Where(x => x.WhStore_DepartmentFK == Dept.Dep_Id && x.WhStore_DyeKitchen).FirstOrDefault();
 
                                                TLDYE_ConsumableSOH cons = new TLDYE_ConsumableSOH();
                                                cons.DYCSH_Consumable_FK = Definition.TLDYED_Cosumables_FK;
                                                cons.DYCSH_StockOnHand -= Kgs;
                                                cons.DYCSH_WhseStore_FK = Whse.WhStore_Id;

                                                context.TLDYE_ConsumableSOH.Add(cons);
                                                try
                                                {
                                                    context.SaveChanges();
                                                }
                                                catch (Exception ex)
                                                {
                                                    MessageBox.Show("Unable to adjust consummable Stock on hand table" + ex.Message);
                                                    continue;
                                                } */

                                            }
                                            else
                                            {
                                                SOH.DYCSH_K_Used += Kgs;
                                                
                                            }
                                        }
                                    }
                                    else
                                    {
                                        //-------------------------------------------
                                        // We Now use the the liquidity factor 
                                        //------------------------------------------------------------
                                        var Kgs = (Definition.TLDYED_MELFC * DB.DYEB_BatchKG * Definition.TLDYED_LiqRatio) / 1000;
                                        //-----------------------------------
                                        //Find this Record in the SOH
                                        //----------------------------------------------
                                        var SOH = context.TLDYE_ConsumableSOH.Where(x => x.DYCSH_Consumable_FK == Definition.TLDYED_Cosumables_FK && x.DYCSH_DyeKitchen).FirstOrDefault();
                                        //------------------------
                                        // Now there might be NO stock on hand for this item in the consumables stock on hand 
                                        //--------------------------------------------------------
                                        if (SOH == null)
                                        {
                                            /*
                                            var Whse = context.TLADM_WhseStore.Where(x => x.WhStore_DepartmentFK == Dept.Dep_Id && x.WhStore_DyeKitchen).FirstOrDefault();

                                            TLDYE_ConsumableSOH cons = new TLDYE_ConsumableSOH();
                                            cons.DYCSH_Consumable_FK = Definition.TLDYED_Cosumables_FK;
                                            cons.DYCSH_StockOnHand -= Kgs;
                                            cons.DYCSH_WhseStore_FK = Whse.WhStore_Id;

                                            context.TLDYE_ConsumableSOH.Add(cons);
                                            try
                                            {
                                                context.SaveChanges();
                                            }
                                            catch (Exception ex)
                                            {
                                                MessageBox.Show("Unable to adjust consummable Stock on hand table" + ex.Message);
                                                continue;
                                            }
                                            */
                                        }
                                        else
                                        {
                                            SOH.DYCSH_K_Used += Kgs;
                                            
                                        }
                                    }
                                }

                                try
                                {
                                    context.SaveChanges();
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show("Unable to adjust consummable Stock on hand table" + ex.Message);
                                }
                          //  }
                        }
                    }

                    try
                    {
                        context.SaveChanges();
                        SetUp();
                        MessageBox.Show("Data saved to database successfully");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }

        private void cmboOperator_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void rbBtnYes_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton oRad = sender as RadioButton;
            if (oRad != null && formloaded)
            {
                if (oRad.Checked)
                {
                    if(!rbPortA.Checked && !rbPortB.Checked && !rbBothPorts.Checked )
                    {
                        MessageBox.Show("Please select a Port status");
                        rbBtnNo.Checked = true;
                        return;
                    }
                    var selected = (TLDYE_DyeBatch)cmboBatchNo.SelectedItem;
                    if (selected != null)
                    {
                        var machop = (TLADM_MachineOperators)cmboOperator.SelectedItem;
                        if (machop == null)
                        {
                            MessageBox.Show("Please select an operator");
                            rbBtnNo.Checked = true;
                            return;
                        }

                        var PortStatus = 0;
                        if(rbPortA.Checked)
                        {
                            PortStatus = 1;
                        }
                        else if(rbPortB.Checked)
                        {
                            PortStatus = 2;                        }
                        else
                        {
                            PortStatus = 3;
                        }
                        frmDye_NonCompliance nonCom = new frmDye_NonCompliance(selected.DYEB_Pk, machop.MachOp_Pk, 1, PortStatus);
                        nonCom.ShowDialog(this);
                    }
                    else
                    {
                        MessageBox.Show("Please select a dye batch");
                        rbBtnNo.Checked = true;
                    }
                }
            }
        }
    }
}
