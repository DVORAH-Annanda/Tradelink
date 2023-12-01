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
    public partial class frmAllocatedOperator : Form
    {
        bool formloaded;

        public frmAllocatedOperator()
        {
            InitializeComponent();
            SetUp();
        }

        void SetUp()
        {
            formloaded = false;
            using (var context = new TTI2Entities())
            {
                cmboDyeBatch.DataSource = context.TLDYE_DyeBatch.Where(x=>x.DYEB_Transfered).OrderBy(x => x.DYEB_BatchNo).ToList();
                cmboDyeBatch.ValueMember = "DYEB_BatchNo";
                cmboDyeBatch.DisplayMember = "DYEB_BatchNo";
                cmboDyeBatch.SelectedValue = 0;

                var Dept = context.TLADM_Departments.Where(x => x.Dep_ShortCode.Contains("DYE")).FirstOrDefault();
                if (Dept != null)
                {
                    cmboOperator.DataSource = context.TLADM_MachineOperators.Where(x => x.MachOp_Department_FK == Dept.Dep_Id && !x.MachOp_Discontinued).ToList();
                    cmboOperator.ValueMember = "MachOp_Pk";
                    cmboOperator.DisplayMember = "MachOp_Description";
                    cmboOperator.SelectedValue = 0;
                }
            }

            txtMachineCode.Text = string.Empty;
            txtMachineDescription.Text = string.Empty;
            txtSubCode.Text = string.Empty;

            formloaded = true;
        }

        private void cmboDyeBatch_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = sender as ComboBox;
            if (oCmbo != null)
            {
                var selected = (TLDYE_DyeBatch)cmboDyeBatch.SelectedItem;
                if (selected != null)
                {
                    using (var context = new TTI2Entities())
                    {
                        var DBA = context.TLDYE_DyeBatchAllocated.Where(x=>x.TLDYEA_DyeBatch_FK == selected.DYEB_Pk).FirstOrDefault();
                        if (DBA != null)
                        {
                            var Machine = context.TLADM_MachineDefinitions.Find(DBA.TLDYEA_MachCode_FK);
                            if (Machine != null)
                            {
                                txtMachineCode.Text = Machine.MD_MachineCode;
                                txtMachineDescription.Text = Machine.MD_Description;

                                var FT = context.TLADM_FabricProduct.Find(Machine.MD_FabricType_FK);
                                if (FT != null)
                                {
                                    txtSubCode.Text = FT.FP_Description;
                                }
                            }
                        }
                    }
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;
            if (oBtn != null && formloaded)
            {
                var selected = (TLDYE_DyeBatch)cmboDyeBatch.SelectedItem;
                if (selected == null)
                {
                    MessageBox.Show("Please select a dye batch from the combobox");
                    return;
                }

                var Operator = (TLADM_MachineOperators)cmboOperator.SelectedItem;
                if (Operator == null)
                {
                    MessageBox.Show("Please select an operator the combobox");
                    return;
                }

                TLDYE_AllocatedOperator allop = new TLDYE_AllocatedOperator();

                using (var context = new TTI2Entities())
                {
                   allop = context.TLDYE_AllocatedOperator.Where(x => x.DYEOP_BatchNo_FK == selected.DYEB_Pk).FirstOrDefault();
                    if (allop == null)
                    {
                        allop = new TLDYE_AllocatedOperator();
                        allop.DYEOP_BatchDate = dtpDateAllocated.Value;
                        allop.DYEOP_Operator_FK = Operator.MachOp_Pk;
                        allop.DYEOP_BatchNo_FK = selected.DYEB_Pk;

                        context.TLDYE_AllocatedOperator.Add(allop);
                    }
                    else
                    {
                        allop.DYEOP_BatchDate = dtpDateAllocated.Value;
                        allop.DYEOP_Operator_FK = Operator.MachOp_Pk;
                    }

                    try
                    {
                        context.SaveChanges();
                        MessageBox.Show("Data saved to database successfully");
                        SetUp();
                    }
                    catch (System.Data.Entity.Validation.DbEntityValidationException en)
                    {
                        foreach (var eve in en.EntityValidationErrors)
                        {
                            MessageBox.Show("following validation errors: Type" + eve.Entry.Entity.GetType().Name.ToString() + "State " + eve.Entry.State.ToString());
                            foreach (var ve in eve.ValidationErrors)
                            {
                                MessageBox.Show("- Property" + ve.PropertyName + " Message " + ve.ErrorMessage);
                            }
                        }
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
