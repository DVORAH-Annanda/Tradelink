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

namespace Knitting
{
   

    public partial class frmGreigeReceived3P : Form
    {
        bool formloaded;
        Util core;

        string[][] MandatoryFields;
        bool[] MandatorySelected;

        DataGridViewTextBoxColumn oTxtA = new DataGridViewTextBoxColumn();   // Piece No     0
        DataGridViewTextBoxColumn oTxtB = new DataGridViewTextBoxColumn();   // TTS Number   1
        DataGridViewComboBoxColumn oCmbA = new DataGridViewComboBoxColumn();  // Fabric Type  2
        DataGridViewTextBoxColumn oTxtC = new DataGridViewTextBoxColumn();   // Grade        3
        DataGridViewTextBoxColumn oTxtD = new DataGridViewTextBoxColumn();   // Customer Order 4 
        DataGridViewTextBoxColumn oTxtE = new DataGridViewTextBoxColumn();   // Nett Weight    5
        
      
        public frmGreigeReceived3P()
        {
            InitializeComponent();
            core = new Util();

            MandatoryFields = new string[][]
            {   new string[] {"cmbCommissionC", "Please select a Commission Customer", "0"},
                new string[] {"txtCustDeliveryDoc", "Please enter a delivery document number", "1"},
                new string[] {"cmbStore", "Please enter a store number", "2"},
                new string[] {"cmbFabricType", "Please select a fabric type", "3"},
                new string[] {"txtGrade", "Please enter a Grade", "4"},
                new string[] {"txtComments", "Please enter the customers comments", "5"},
                new string[] {"txtNoOfPieces", "Please enter the number of pieces", "6"},
            };

            txtNoOfPieces.KeyPress += core.txtWin_KeyPress;
            txtNoOfPieces.KeyDown += core.txtWin_KeyDown;
            txtNoOfPieces.TextAlign = HorizontalAlignment.Right;

            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AutoGenerateColumns = false;


            SetUp(true);

            dataGridView1.AutoGenerateColumns = false;

            dataGridView1.EditingControlShowing += new DataGridViewEditingControlShowingEventHandler(dataGridView1_EditingControlShowing);

        }


        void SetUp(bool Ind)
        {
            formloaded = false;
            using (var context = new TTI2Entities())
            {
                var LNU = context.TLADM_LastNumberUsed.Find(2);
                if (LNU != null)
                {
                    txtGRNNumber.Text = "CG" + LNU.col8.ToString().PadLeft(6, '0'); 
                }

                if (Ind)
                {
                    oTxtA = new DataGridViewTextBoxColumn();
                    oTxtA.ValueType = typeof(string);
                    oTxtA.HeaderText = "Piece No";
                    oTxtA.Visible = true;

                    oTxtB = new DataGridViewTextBoxColumn();
                    oTxtB.ValueType = typeof(string);
                    oTxtB.HeaderText = "TTS";
                    oTxtB.Visible = true;
                    oTxtB.ReadOnly = true;

                    oCmbA = new DataGridViewComboBoxColumn();
                    oCmbA.DataSource = context.TLADM_Griege.ToList();
                    oCmbA.HeaderText = "Fabric Type";
                    oCmbA.ValueMember = "TLGreige_Id";
                    oCmbA.DisplayMember = "TLGreige_Description";

                    oTxtC = new DataGridViewTextBoxColumn();
                    oTxtC.ValueType = typeof(string);
                    oTxtC.HeaderText = "Grade";
                    oTxtC.Visible = true;

                    oTxtD = new DataGridViewTextBoxColumn();
                    oTxtD.ValueType = typeof(string);
                    oTxtD.HeaderText = "Cust Order";
                    oTxtD.Visible = true;

                    oTxtE = new DataGridViewTextBoxColumn();
                    oTxtE.ValueType = typeof(Decimal);
                    oTxtE.HeaderText = "Nett Weight";
                    oTxtE.Visible = true;


                    dataGridView1.Columns.Add(oTxtA);
                    dataGridView1.Columns.Add(oTxtB);
                    dataGridView1.Columns.Add(oCmbA);
                    dataGridView1.Columns.Add(oTxtC);
                    dataGridView1.Columns.Add(oTxtD);
                    dataGridView1.Columns.Add(oTxtE);

                }
                cmbCommissionC.DataSource = context.TLADM_CustomerFile.Where(x => x.Cust_CommissionCust).ToList();
                cmbCommissionC.ValueMember = "Cust_Pk";
                cmbCommissionC.DisplayMember = "Cust_Description";
                cmbCommissionC.SelectedValue = 0;

                cmbFabricType.DataSource = context.TLADM_Griege.OrderBy(x => x.TLGreige_Description).ToList();
                cmbFabricType.DisplayMember = "TLGreige_Description";
                cmbFabricType.ValueMember = "TLGreige_Id";
                cmbFabricType.SelectedValue = 0;

                var dept = context.TLADM_Departments.Where(x => x.Dep_ShortCode.Contains("KNIT")).FirstOrDefault();
                if (dept != null)
                {
                    cmbStore.DataSource = context.TLADM_WhseStore.Where(x => x.WhStore_DepartmentFK == dept.Dep_Id).ToList();
                    cmbStore.ValueMember = "WhStore_Id";
                    cmbStore.DisplayMember = "WhStore_Description";
                    cmbStore.SelectedValue = 0;
                }
                
                txtComments.Text        = string.Empty;
                txtCustDeliveryDoc.Text = string.Empty;
                txtComments.Text        = string.Empty;
                txtGrade.Text = string.Empty;
                txtNoOfPieces.Text = "0";

            }
            MandatorySelected = core.PopulateArray(MandatoryFields.Length, false);
            formloaded = true;
        }

        void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            DataGridView oDgv = sender as DataGridView;

            if (formloaded)
            {
                if (oDgv.Focused && oDgv.CurrentCell is DataGridViewTextBoxCell)
                {
                    var Cell = oDgv.CurrentCell;
                    if (Cell.ColumnIndex == 5)
                    {
                        e.Control.KeyDown -= new KeyEventHandler(core.txtWin_KeyDownOEM);
                        e.Control.KeyDown += new KeyEventHandler(core.txtWin_KeyDownOEM);
                        e.Control.KeyPress -= new KeyPressEventHandler(core.txtWin_KeyPress);
                        e.Control.KeyPress += new KeyPressEventHandler(core.txtWin_KeyPress);
                    }
                    else
                    {
                        e.Control.KeyDown -= new KeyEventHandler(core.txtWin_KeyDown);
                        e.Control.KeyDown -= new KeyEventHandler(core.txtWin_KeyDownOEM);
                        e.Control.KeyPress -= new KeyPressEventHandler(core.txtWin_KeyPress);
                    }
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
                if (oTxtBx.TextLength > 0)
                    MandatorySelected[nbr] = true;
                else
                {
                    MandatorySelected[nbr] = false;
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;
            int CLNU = 0;

            if (oBtn != null && formloaded)
            {
                var ErrorM = core.returnMessage(MandatorySelected, false, MandatoryFields);
                if (!String.IsNullOrEmpty(ErrorM))
                {
                    MessageBox.Show(ErrorM);
                    return;
                }
                using (var context = new TTI2Entities())
                {
                    var LNU = context.TLADM_LastNumberUsed.Find(2);
                    if (LNU != null)
                    {
                        LNU.col8 += 1;
                    }

                    var custSelected = (TLADM_CustomerFile)cmbCommissionC.SelectedItem;
                    var store = (TLADM_WhseStore)cmbStore.SelectedItem;

                    if (custSelected != null)
                    {
                        CLNU = custSelected.Cust_LastNumberUsed;

                        custSelected = context.TLADM_CustomerFile.Find(custSelected.Cust_Pk);
                        custSelected.Cust_LastNumberUsed = CLNU + 1;

                    }

                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        if ( row.Cells[0].Value == null)
                            continue;

                        TLKNI_GreigeCommissionTransctions comTrans = new TLKNI_GreigeCommissionTransctions();
                        comTrans.GreigeCom_Custdoc = txtCustDeliveryDoc.Text;
                        comTrans.GreigeCom_Customer_FK = custSelected.Cust_Pk;
                        comTrans.GreigeCom_Comments = txtComments.Text;
                        comTrans.GreigeCom_CustOrderNo = (string)row.Cells[4].Value;
                        comTrans.GreigeCom_Grade = (string)row.Cells[3].Value;
                        comTrans.GreigeCom_GrnNo = CLNU;
                        comTrans.GreigeCom_NettWeight = (decimal)row.Cells[5].Value;
                        comTrans.GreigeCom_PieceNo = row.Cells[0].Value.ToString();
                        comTrans.GreigeCom_ProductType_FK = (int)row.Cells[2].Value;
                        comTrans.GreigeCom_Transdate = dateTimePicker1.Value;
                        comTrans.GreigeCom_TTSNo = (string)row.Cells[1].Value;
                        comTrans.GreigeCom_Store_FK = store.WhStore_Id;
                        comTrans.GreigeCom_Display = CLNU.ToString() + " " + txtCustDeliveryDoc.Text;
                        context.TLKNI_GreigeCommissionTransctions.Add(comTrans);

                        try
                        {
                            context.SaveChanges();
                        }
                        catch (Exception ex)
                        {

                        }
                        //we need to store further information in a separate file for the processing in the Dye Module

                        TLKNI_GreigeProduction gp = new TLKNI_GreigeProduction();
                        gp.GreigeP_Captured = true;
                        //-----------------------------------------------------------
                        // gp.GreigeP_KnitO_Fk = CLNU;
                        //------------------------------------------------------
                        gp.GreigeP_KnitO_Fk = comTrans.GreigeCom_PK;
                        gp.GreigeP_CommisionCust = true;
                        gp.GreigeP_CommissionCust_FK = custSelected.Cust_Pk;
                        gp.GreigeP_Grade = (string)row.Cells[3].Value;
                        gp.GreigeP_InspDate = DateTime.Now;
                        gp.GreigeP_Inspected = true;
                        gp.GreigeP_Meas1 = 0;
                        gp.GreigeP_Meas2 = 0;
                        gp.GreigeP_Meas3 = 0;
                        gp.GreigeP_Meas4 = 0;
                        gp.GreigeP_Meas5 = 0;
                        gp.GreigeP_Meas6 = 0;
                        gp.GreigeP_Meas7 = 0;
                        gp.GreigeP_Meas8 = 0;
                        gp.GreigeP_PDate = DateTime.Now;
                        gp.GreigeP_PieceNo = (string)row.Cells[0].Value;
                        gp.GreigeP_weight = (decimal)row.Cells[5].Value;
                        gp.GreigeP_weightAvail = (decimal)row.Cells[5].Value;
                        gp.GreigeP_Store_FK = store.WhStore_Id;
                        gp.GreigeP_Greige_Fk = (int)row.Cells[2].Value;
                        gp.GreigeP_CommissionGrn = CLNU;


                        context.TLKNI_GreigeProduction.Add(gp);
                    }
                    try
                    {
                        context.SaveChanges();
                        MessageBox.Show("Data successfully saved to the database");
                        frmKnitViewRep vRep = new frmKnitViewRep(13, CLNU);
                        int h = Screen.PrimaryScreen.WorkingArea.Height;
                        int w = Screen.PrimaryScreen.WorkingArea.Width;
                        vRep.ClientSize = new Size(w, h);
                        vRep.ShowDialog(this);
                        if (vRep != null)
                        {
                            vRep.Close();
                            vRep.Dispose();

                        }
                        dataGridView1.Rows.Clear();
                        SetUp(false);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }

        private void TxtNoofPieces_OnLeave(object sender, EventArgs e)
        {
            TextBox oTxt = sender as TextBox;
            if (oTxt != null && formloaded)
            {
                var NoOfPieces = Convert.ToInt32(txtNoOfPieces.Text);
                if (NoOfPieces > 0)
                {
                    var Product = (TLADM_Griege)cmbFabricType.SelectedItem;

                    var Customer = (TLADM_CustomerFile)cmbCommissionC.SelectedItem;
                    if (Customer != null)
                    {
                        var LNU = Customer.Cust_LastNumberUsed;
                      
                        var TotalPieces = Convert.ToInt32(txtNoOfPieces.Text);
                        var I = 1;

                        do
                        {
                            var index = dataGridView1.Rows.Add();
                            dataGridView1.Rows[index].Cells[0].Value = string.Empty;
                            dataGridView1.Rows[index].Cells[1].Value = Customer.Cust_GreigePrefix.Trim() + LNU.ToString().PadLeft(5,'0');
                            dataGridView1.Rows[index].Cells[2].Value = Product.TLGreige_Id;
                            dataGridView1.Rows[index].Cells[3].Value = txtGrade.Text.ToUpper();
                            dataGridView1.Rows[index].Cells[4].Value = txtCustomerOrder.Text; 
                            dataGridView1.Rows[index].Cells[5].Value = 0.00M;
                            
                            I++;
                            ++LNU;

                         } while (I <= TotalPieces);

                        Customer.Cust_LastNumberUsed = LNU;
                        //dataGridView1.Focus();
                        //dataGridView1.CurrentCell = dataGridView1[0, 0];

                    }
                    else
                    {
                        MessageBox.Show("Please select a commission customer");
                    }
                }
                else
                {
                    MessageBox.Show("Please enter of pieces being receipted");
                }

            }
        }

        private void cmbStore_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = sender as ComboBox;
            if (oCmbo != null && formloaded)
            {
                var result = (from u in MandatoryFields
                              where u[0] == oCmbo.Name
                              select u).FirstOrDefault();

                int nbr = Convert.ToInt32(result[2].ToString());
                MandatorySelected[nbr] = true;
                
            }
        }

        private void cmbFabricType_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = sender as ComboBox;
            if (oCmbo != null && formloaded)
            {
                var result = (from u in MandatoryFields
                              where u[0] == oCmbo.Name
                              select u).FirstOrDefault();

                int nbr = Convert.ToInt32(result[2].ToString());
                MandatorySelected[nbr] = true;
            }
        }

        private void cmbCommissionC_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = sender as ComboBox;
            if (oCmbo != null && formloaded)
            {
                var result = (from u in MandatoryFields
                              where u[0] == oCmbo.Name
                              select u).FirstOrDefault();

                int nbr = Convert.ToInt32(result[2].ToString());
                MandatorySelected[nbr] = true;
            }
        }
    }
}
