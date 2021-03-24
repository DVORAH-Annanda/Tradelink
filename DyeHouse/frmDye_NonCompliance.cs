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
    public partial class frmDye_NonCompliance : Form
    {
        int _DyeBatch;
        int _Operator;
        int _NCStage;
        int _PortStatus;

        bool formloaded;

        DataGridViewTextBoxColumn oTxtA0 = new DataGridViewTextBoxColumn();
        DataGridViewCheckBoxColumn oChkA = new DataGridViewCheckBoxColumn();
        DataGridViewTextBoxColumn oTxtA1 = new DataGridViewTextBoxColumn();
        DataGridViewTextBoxColumn oTxtA2 = new DataGridViewTextBoxColumn();


        public frmDye_NonCompliance(int db, int Operator, int NCStage, int PortStatus)
        {
            InitializeComponent();

            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.AllowUserToAddRows = false;

            oTxtA0.ReadOnly = true;
            oTxtA0.Visible = false;
            oTxtA0.ValueType = typeof(int);
            dataGridView1.Columns.Add(oTxtA0);

            oChkA.HeaderText = "Selected";
            oChkA.ValueType = typeof(bool);
            dataGridView1.Columns.Add(oChkA);

            oTxtA1.HeaderText = "Fault Code";
            oTxtA1.ReadOnly = true;
            oTxtA1.ValueType = typeof(string);
            dataGridView1.Columns.Add(oTxtA1);

            oTxtA2.HeaderText = "Fault Description";
            oTxtA2.ValueType = typeof(string);
            oTxtA2.ReadOnly = true;
            oTxtA2.Width = 200;

            dataGridView1.Columns.Add(oTxtA2);

            _DyeBatch = db;
            _Operator = Operator;
            _NCStage = NCStage;
            _PortStatus = PortStatus;


            SetUp();
        }

        void SetUp()
        {
            using (var context = new TTI2Entities())
            {
                formloaded = false;

                var DB = context.TLDYE_DyeBatch.Find(_DyeBatch);
                if (DB != null)
                {
                    var LNU = context.TLADM_LastNumberUsed.Find(3);
                    if (LNU != null)
                    {
                        txtNCRNumber.Text = LNU.col5.ToString();
                        txtNCRNumber.ReadOnly = true;
                    }

                    txtColour.Text = context.TLADM_Colours.Find(DB.DYEB_Colour_FK).Col_Description;
                  
                    if(!DB.DYEB_FabricMode)
                    {
                        var DO = context.TLDYE_DyeBatchDetails.Where(x => x.DYEBD_DyeBatch_FK == DB.DYEB_Pk).FirstOrDefault();
                        if (DO != null)
                        {
                            // var DO = context.TLDYE_DyeOrder.Find(DB.DYEB_DyeOrder_FK);
                            var Qual = context.TLADM_Griege.Find(DO.DYEBD_QualityKey);
                            if (Qual != null)
                            {
                                txtQual.Text = Qual.TLGreige_Description;
                            }
                        }
                    }
                    else
                    {
                        var DO = context.TLDYE_DyeOrderFabric.Find(DB.DYEB_DyeOrder_FK);
                        var Qual = context.TLADM_Griege.Find(DO.TLDYEF_Greige_FK);
                        if (Qual != null)
                        {
                            txtQual.Text = Qual.TLGreige_Description;
                        }
                    }

                    var Allocated = context.TLDYE_DyeBatchAllocated.Where(x => x.TLDYEA_DyeBatch_FK == _DyeBatch).FirstOrDefault();
                    if (Allocated != null)
                    {
                        var Machine = context.TLADM_MachineDefinitions.Find(Allocated.TLDYEA_MachCode_FK);
                        if (Machine != null)
                        {
                            txtMachineCode.Text = Machine.MD_AlternateDesc;
                        }
                    }

                    txtBatchNo.Text = DB.DYEB_BatchNo;
                    txtTotalKgs.Text = DB.DYEB_BatchKG.ToString();

                    var QDC = context.TLADM_DyeQDCodes.OrderBy(x => x.QDF_Code).ToList();
                    foreach (var record in QDC)
                    {
                        var index = dataGridView1.Rows.Add();
                        dataGridView1.Rows[index].Cells[0].Value = record.QDF_Pk;
                        dataGridView1.Rows[index].Cells[1].Value = false;
                        dataGridView1.Rows[index].Cells[2].Value = record.QDF_Code;
                        dataGridView1.Rows[index].Cells[3].Value = record.QDF_Description;
                    }
                }

                formloaded = true;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;
            if (oBtn != null && formloaded)
            {
                using (var context = new TTI2Entities())
                {

                    var Dept = context.TLADM_Departments.Where(x => x.Dep_ShortCode.Contains("DYE")).FirstOrDefault();
                    if (Dept != null)
                    {
                        TLDYE_NonCompliance nonC = new TLDYE_NonCompliance();
                        var LNU = context.TLADM_LastNumberUsed.Find(3);
                        if (LNU != null)
                        {
                            nonC.TLDYE_ComplainceDate = dtpComplDate.Value;
                            nonC.TLDYE_NcrBatchNo_FK = _DyeBatch;
                            nonC.TLDYE_NcrNotes = rtbNotes.Text;
                            nonC.TLDYE_NcrNumber = LNU.col5;
                            nonC.TLDYE_Department_FK = Dept.Dep_Id;
                            nonC.TLDYE_Operator_FK = _Operator;
                            nonC.TLDYE_NCStage = _NCStage;
                            nonC.TLDYE_PortStatus = _PortStatus;
                            var DBAllocated = context.TLDYE_DyeBatchAllocated.Where(x => x.TLDYEA_DyeBatch_FK == _DyeBatch).FirstOrDefault();
                            if (DBAllocated != null)
                                nonC.TLDYE_Machine_FK = DBAllocated.TLDYEA_MachCode_FK;

                            context.TLDYE_NonCompliance.Add(nonC);

                            
                        }

                        foreach (DataGridViewRow row in dataGridView1.Rows)
                        {
                            if (row.Cells[1].Value.ToString() == bool.FalseString)
                                continue;

                            TLDYE_NonComplianceDetail nonD = new TLDYE_NonComplianceDetail();
                            nonD.DYENCRD_BatchNo_Fk = _DyeBatch;
                            nonD.DYENCRD_FR = true;
                            nonD.DYENCRD_Code_FK = (int)row.Cells[0].Value;
                            nonD.DYENCRD_ComNumber = LNU.col5;
                            context.TLDYE_NonComplianceDetail.Add(nonD);
                        }

                        try
                        {
                            // Last Number used 
                            //------------------------------------------------
                            LNU.col5 += 1;

                            context.SaveChanges();
                            MessageBox.Show("Data saved to database successfully");
                            frmDyeViewReport vRep = new frmDyeViewReport(7, nonC.TLDYE_NcrNumber);
                            int h = Screen.PrimaryScreen.WorkingArea.Height;
                            int w = Screen.PrimaryScreen.WorkingArea.Width;
                            vRep.ClientSize = new Size(w, h);
                            vRep.ShowDialog(this);
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
