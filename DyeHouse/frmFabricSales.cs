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
    public partial class frmFabricSales : Form
    {
        bool formloaded;
        bool[] MandSelected;
        string[][] MandatoryFields;

        private DataTable dt;

        Util core;

        public frmFabricSales()
        {
            InitializeComponent();

            MandatoryFields = new string[][]
            {   new string[] {"dtTransDate", "Please select a transaction date", "0"},
                new string[] {"cmboCustomers", "Please enter a customer", "1"},
                new string[] {"txtOrderNumber", "Please a customer order number", "2"}

            };
            core = new Util();

            DataGridViewTextBoxColumn oTxtIndex = new DataGridViewTextBoxColumn();
            oTxtIndex.HeaderText = "Index";
            oTxtIndex.ValueType = typeof(int);
            oTxtIndex.ReadOnly = true;
            oTxtIndex.Visible = false;

            DataGridViewTextBoxColumn oTxtA = new DataGridViewTextBoxColumn();
            oTxtA.HeaderText = "Piece No";
            oTxtA.ReadOnly = true;

            DataGridViewTextBoxColumn oTxtB = new DataGridViewTextBoxColumn();
            oTxtB.HeaderText = "Quality";
            oTxtB.ReadOnly = true;

            DataGridViewTextBoxColumn oTxtC = new DataGridViewTextBoxColumn();
            oTxtC.HeaderText = "Colour";
            oTxtC.ReadOnly = true;

            DataGridViewTextBoxColumn oTxtD = new DataGridViewTextBoxColumn();
            oTxtD.HeaderText = "Gross Weight";
            oTxtD.ReadOnly = true;
            oTxtD.ValueType = typeof(decimal);

            DataGridViewTextBoxColumn oTxtE = new DataGridViewTextBoxColumn();
            oTxtE.HeaderText = "Nett Weight";
            oTxtE.ValueType = typeof(decimal);

            DataGridViewTextBoxColumn oTxtF = new DataGridViewTextBoxColumn();
            oTxtF.HeaderText = "Disk";
            oTxtF.ReadOnly = true;
            oTxtF.ValueType = typeof(decimal);

            DataGridViewTextBoxColumn oTxtG = new DataGridViewTextBoxColumn();
            oTxtG.HeaderText = "Length %";
            oTxtG.ValueType = typeof(decimal);
            oTxtG.ReadOnly = true;

            DataGridViewTextBoxColumn oTxtH = new DataGridViewTextBoxColumn();
            oTxtH.HeaderText = "Width %";
            oTxtH.ValueType = typeof(decimal);
            oTxtH.ReadOnly = true;

            DataGridViewTextBoxColumn oTxtJ = new DataGridViewTextBoxColumn();
            oTxtJ.HeaderText = "Spirality %";
            oTxtJ.ValueType = typeof(decimal);
            oTxtJ.ReadOnly = true;

            DataGridViewTextBoxColumn oTxtK = new DataGridViewTextBoxColumn();
            oTxtK.HeaderText = "Index position of Batch";
            oTxtK.ValueType = typeof(decimal);
            oTxtK.Visible = false;
            oTxtK.ReadOnly = true;

            DataGridViewCheckBoxColumn oChk = new DataGridViewCheckBoxColumn();
            oChk.HeaderText = "Select";
            oChk.ValueType = typeof(bool);

            dataGridView1.Columns.Add(oTxtIndex);  //0 Index
            dataGridView1.Columns.Add(oTxtA);      //1 Piece No
            dataGridView1.Columns.Add(oTxtB);      //2 Quality
            dataGridView1.Columns.Add(oTxtC);      //3 Color
            dataGridView1.Columns.Add(oTxtD);      //4 Gross Weight
            dataGridView1.Columns.Add(oTxtE);      //5 Nett Weight
            dataGridView1.Columns.Add(oTxtF);      //6 Disk
            dataGridView1.Columns.Add(oTxtG);      //7 Length %
            dataGridView1.Columns.Add(oTxtH);      //8 Width %
            dataGridView1.Columns.Add(oTxtJ);      //9 Spirality
            dataGridView1.Columns.Add(oChk);       //10 Check
            dataGridView1.Columns.Add(oTxtK);      //11 Batch Foreign Key
            
            dataGridView1.AllowUserToOrderColumns = false;
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.AllowUserToAddRows = false;
            
       

        }

        private void frmFabricSales_Load(object sender, EventArgs e)
        {
            formloaded = false;
            
            using (var context = new TTI2Entities())
            {
                var LNU = context.TLADM_LastNumberUsed.Find(3);
                if (LNU != null)
                {
                    txtTransNumber.Text = "FD" + LNU.col10.ToString().PadLeft(6, '0');
                }

                cmboGreige.DataSource = context.TLADM_Griege.Where(x => !(bool)x.TLGriege_Discontinued).ToList(); 
                cmboGreige.ValueMember = "TLGreige_id";
                cmboGreige.DisplayMember = "TLGreige_Description";
                cmboGreige.SelectedValue = -1;

                cmboCustomers.DataSource = context.TLADM_CustomerFile.OrderBy(x => x.Cust_Description).Where(x=>!x.Cust_CommissionCust).OrderBy(x=>x.Cust_Description).ToList();
                cmboCustomers.ValueMember = "Cust_Pk";
                cmboCustomers.DisplayMember = "Cust_Description";
                cmboCustomers.SelectedValue = -1;

            }
            MandSelected = core.PopulateArray(MandatoryFields.Length, false);

            formloaded = true;
            rbFabricStore.Checked = false;

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;
            IList<TLDYE_DyeBatch> DB = new List<TLDYE_DyeBatch>();
            Decimal Nett = 0.00M;
            TLADM_TranactionType TranType = null;
            Decimal BatchWeight = 0.00M;
           

            if (oBtn != null && formloaded)
            {
                var errorM = core.returnMessage(MandSelected, true, MandatoryFields);
                if (!string.IsNullOrEmpty(errorM))
                {
                    MessageBox.Show(errorM);
                    return;
                }

                using (var context = new TTI2Entities())
                {
                    var Dept = context.TLADM_Departments.Where(x => x.Dep_ShortCode == "DYE").FirstOrDefault();
                    if (Dept != null)
                    {
                        if (rbFabricStore.Checked)
                        {
                            TranType = context.TLADM_TranactionType.Where(x => x.TrxT_Department_FK == Dept.Dep_Id && x.TrxT_Number == 1100).FirstOrDefault();
                            
                        }
                        else
                        {
                            TranType = context.TLADM_TranactionType.Where(x => x.TrxT_Department_FK == Dept.Dep_Id && x.TrxT_Number == 1200).FirstOrDefault();
                        }
                    }

                    var LNU = context.TLADM_LastNumberUsed.Find(3);
                    if (LNU != null)
                    {
                        LNU.col10 += 1; ;
                    }

                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        if ((bool)row.Cells[10].Value == true)
                        {
                            
                            var index = Int32.Parse(row.Cells[0].Value.ToString());
                            var DBD = context.TLDYE_DyeBatchDetails.Find(index);
                            if (DBD != null)
                            {
                                DBD.DYEBO_CurrentStore_FK = TranType.TrxT_Pk;
                                DBD.DYEBO_TransactionNo = txtTransNumber.Text;
                                DBD.DYEBO_DateSold = dtTransDate.Value;
                                DBD.DYEBO_Sold = true;

                                BatchWeight += DBD.DYEBD_GreigeProduction_Weight;
                                Nett += DBD.DYEBO_Nett;
                            }

                           
                       }
                    }

                   
                    TLDYE_DyeTransactions tt = new TLDYE_DyeTransactions();
                    tt.TLDYET_BatchNo = "0";
                    tt.TLDYET_BatchWeight = BatchWeight;
                    tt.TLDYET_SequenceNo = 0;
                    tt.TLDYET_Batch_FK = 0;
                    tt.TLDYET_Date = dtTransDate.Value;
                    tt.TLDYET_TransactionWeight = Nett;
                    tt.TLDYET_TransactionNumber = txtTransNumber.Text;
                    tt.TLDYET_TransactionType = TranType.TrxT_Pk;
                    tt.TLDYET_CurrentStore_FK = (int)TranType.TrxT_ToWhse_FK;
                    tt.TLDYET_Customer_FK = ((TLADM_CustomerFile)cmboCustomers.SelectedItem).Cust_Pk;
                    tt.TLDYET_FabricSales = true;
                    tt.TLDYET_CustomerOrderNo = txtOrderNumber.Text;
 
                    context.TLDYE_DyeTransactions.Add(tt);
                    try
                    {
                        context.SaveChanges();
                        formloaded = false;
                        dataGridView1.Rows.Clear();
                        cmboCustomers.SelectedValue = -1;
                        cmboGreige.SelectedValue = -1;
                        formloaded = true;
                        MessageBox.Show("Data succesfully saved to database");
                    }
                    catch (Exception ex)
                    {
                            MessageBox.Show(ex.Message);
                    }
                    
                }


            }
        }

        private void dtTransDate_ValueChanged(object sender, EventArgs e)
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

        private void cmboCustomers_SelectedIndexChanged(object sender, EventArgs e)
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

                /*
                using (var context = new TTI2Entities())
                {
                    if (rbFabricStore.Checked)
                        dbDetails = context.TLDYE_DyeBatchDetails.Where(x => x.DYEBO_QAApproved && !x.DYEBO_Sold && !x.DYEBO_CutSheet).ToList();
                    else
                        dbDetails = context.TLDYE_DyeBatchDetails.Where(x => x.DYEBO_Rejected && !x.DYEBO_Sold && !x.DYEBO_CutSheet).ToList();

                    foreach (var row in dbDetails)
                    {
                        var index = dataGridView1.Rows.Add();
                        dataGridView1.Rows[index].Cells[0].Value = row.DYEBD_Pk;
                        var GreigeProd = context.TLKNI_GreigeProduction.Find(row.DYEBD_GreigeProduction_FK);
                        if (GreigeProd == null)
                        {
                            dataGridView1.Rows[index].Cells[1].Value = "Unknown";
                        }
                        else
                            dataGridView1.Rows[index].Cells[1].Value = GreigeProd.GreigeP_PieceNo;
                        dataGridView1.Rows[index].Cells[2].Value = context.TLADM_Griege.Find(row.DYEBD_QualityKey).TLGreige_Description;
                        var BD = context.TLDYE_DyeBatch.Find(row.DYEBD_DyeBatch_FK);
                        if (BD != null)
                            dataGridView1.Rows[index].Cells[3].Value = context.TLADM_Colours.Find(BD.DYEB_Colour_FK).Col_Display;
                        dataGridView1.Rows[index].Cells[4].Value = row.DYEBD_GreigeProduction_Weight;
                        dataGridView1.Rows[index].Cells[5].Value = row.DYEBO_Nett;
                        dataGridView1.Rows[index].Cells[6].Value = row.DYEBO_DiskWeight;
                        
                        dataGridView1.Rows[index].Cells[7].Value = 0;
                        dataGridView1.Rows[index].Cells[8].Value = 0;
                        dataGridView1.Rows[index].Cells[9].Value = 0;

                        dataGridView1.Rows[index].Cells[10].Value = false;
                        dataGridView1.Rows[index].Cells[11].Value = row.DYEBD_DyeBatch_FK;

                    }
                    
                }
                 */ 
            }
        }

        private void txtOrderNumber_Validated(object sender, EventArgs e)
        {

        }

        private void txtOrderNumber_TextChanged(object sender, EventArgs e)
        {
            TextBox oTxt = sender as TextBox;
            if (oTxt != null && formloaded)
            {
                var result = (from u in MandatoryFields
                              where u[0] == oTxt.Name
                              select u).FirstOrDefault();

                if (result != null)
                {
                    int nbr = Convert.ToInt32(result[2].ToString());
                    MandSelected[nbr] = true;
                }
            }
        }

        private void rbFabricStore_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton oRad = sender as RadioButton;
             // IList<TLDYE_DyeBatchDetails> dbDetails = null;
            if (oRad != null && formloaded && oRad.Checked)
            {
                var GreigeSelected = (TLADM_Griege)cmboGreige.SelectedItem;
                if (GreigeSelected != null)
                {
                    using (var context = new TTI2Entities())
                    {
                        dataGridView1.Rows.Clear();

                        var dbDetails = (from GP in context.TLKNI_GreigeProduction
                                         join DBD in context.TLDYE_DyeBatchDetails
                                         on GP.GreigeP_Pk equals DBD.DYEBD_GreigeProduction_FK
                                         join ADMGriege in context.TLADM_Griege
                                         on GP.GreigeP_Greige_Fk equals ADMGriege.TLGreige_Id
                                         where !GP.GreigeP_BoughtIn && DBD.DYEBO_QAApproved 
                                         && !DBD.DYEBO_Sold 
                                         && !DBD.DYEBO_CutSheet 
                                         && !DBD.DYEBO_WriteOff
                                         && DBD.DYEBD_QualityKey == GreigeSelected.TLGreige_Id
                                         orderby GP.GreigeP_PieceNo
                                         select new { DBD, GP, ADMGriege }).ToList();

                        if (dbDetails.Count == 0)
                        {
                            using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                            {
                                MessageBox.Show("There are no records found pertaining to selection made", "No Records Found", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }
                        }

                        foreach (var row in dbDetails)
                        {
                            var index = dataGridView1.Rows.Add();
                            dataGridView1.Rows[index].Cells[0].Value = row.DBD.DYEBD_Pk;
                            dataGridView1.Rows[index].Cells[1].Value = row.GP.GreigeP_PieceNo;
                            dataGridView1.Rows[index].Cells[2].Value = row.ADMGriege.TLGreige_Description;
                            var BD = context.TLDYE_DyeBatch.Find(row.DBD.DYEBD_DyeBatch_FK);
                            if (BD != null)
                                dataGridView1.Rows[index].Cells[3].Value = context.TLADM_Colours.Find(BD.DYEB_Colour_FK).Col_Display;
                            dataGridView1.Rows[index].Cells[4].Value = row.DBD.DYEBD_GreigeProduction_Weight;
                            dataGridView1.Rows[index].Cells[5].Value = row.DBD.DYEBO_Nett;
                            dataGridView1.Rows[index].Cells[6].Value = row.DBD.DYEBO_DiskWeight;

                            dataGridView1.Rows[index].Cells[7].Value = 0;
                            dataGridView1.Rows[index].Cells[8].Value = 0;
                            dataGridView1.Rows[index].Cells[9].Value = 0;

                            dataGridView1.Rows[index].Cells[10].Value = false;
                            dataGridView1.Rows[index].Cells[11].Value = row.DBD.DYEBD_DyeBatch_FK;

                        }
                    }
                }
                else
                {
                    using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                    {
                        MessageBox.Show("Please select a quality from the drop down box");
                    }
                }
            }
        }

        private void rbRejectStore_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton oRad = sender as RadioButton;
            if (oRad != null && formloaded && oRad.Checked)
            {
                var GreigeSelected = (TLADM_Griege)cmboGreige.SelectedItem;
                if (GreigeSelected != null)
                {

                    using (var context = new TTI2Entities())
                    {
                        dataGridView1.Rows.Clear();

                        var dbDetails = (from GP in context.TLKNI_GreigeProduction
                                         join DBD in context.TLDYE_DyeBatchDetails
                                         on GP.GreigeP_Pk equals DBD.DYEBD_GreigeProduction_FK
                                         join ADMGriege in context.TLADM_Griege
                                         on GP.GreigeP_Greige_Fk equals ADMGriege.TLGreige_Id
                                         where !GP.GreigeP_BoughtIn && 
                                               !DBD.DYEBO_QAApproved && 
                                               DBD.DYEBO_Rejected && 
                                               !DBD.DYEBO_WriteOff &&
                                               !DBD.DYEBO_Sold && 
                                               !DBD.DYEBO_CutSheet && 
                                               DBD.DYEBD_QualityKey == GreigeSelected.TLGreige_Id
                                         orderby GP.GreigeP_PieceNo
                                         select new { DBD, GP, ADMGriege }).ToList();


                        if (dbDetails.Count == 0)
                        {
                            using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                            {
                                MessageBox.Show("There are no records found pertaining to selection made", "No Records Found", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }
                        }

                        foreach (var row in dbDetails)
                        {
                            var index = dataGridView1.Rows.Add();
                            dataGridView1.Rows[index].Cells[0].Value = row.DBD.DYEBD_Pk;
                            dataGridView1.Rows[index].Cells[1].Value = row.GP.GreigeP_PieceNo;
                            dataGridView1.Rows[index].Cells[2].Value = row.ADMGriege.TLGreige_Description;
                            var BD = context.TLDYE_DyeBatch.Find(row.DBD.DYEBD_DyeBatch_FK);
                            if (BD != null)
                                dataGridView1.Rows[index].Cells[3].Value = context.TLADM_Colours.Find(BD.DYEB_Colour_FK).Col_Display;
                            dataGridView1.Rows[index].Cells[4].Value = row.DBD.DYEBD_GreigeProduction_Weight;
                            dataGridView1.Rows[index].Cells[5].Value = row.DBD.DYEBO_Nett;
                            dataGridView1.Rows[index].Cells[6].Value = row.DBD.DYEBO_DiskWeight;

                            dataGridView1.Rows[index].Cells[7].Value = 0;
                            dataGridView1.Rows[index].Cells[8].Value = 0;
                            dataGridView1.Rows[index].Cells[9].Value = 0;

                            dataGridView1.Rows[index].Cells[10].Value = false;
                            dataGridView1.Rows[index].Cells[11].Value = row.DBD.DYEBD_DyeBatch_FK;
                        }
                    }
                }
                else
                {
                    using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                    {
                        MessageBox.Show("Please select a quality from the drop down box");
                    }
                }
            }
        }

       
    }
}
