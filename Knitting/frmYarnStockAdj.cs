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
using Spinning;

namespace Knitting
{
    public partial class frmYarnStockAdj : Form
    {
        DataGridViewTextBoxColumn oTxtA;    // Key    0
        DataGridViewCheckBoxColumn oChkA;   // Select Y/N  1
        DataGridViewTextBoxColumn oTxtB;    // Pallet No  2
        DataGridViewTextBoxColumn oTxtC;    // Yarn Description    3
        DataGridViewTextBoxColumn oTxtD;    // Tex             4
        DataGridViewTextBoxColumn oTxtE;    // Twist   5 
        DataGridViewTextBoxColumn oTxtF;    // Identification       6
        DataGridViewTextBoxColumn oTxtG;    // No Of Cones       7
        DataGridViewTextBoxColumn oTxtH;    // Nett Weight      8
        DataGridViewCheckBoxColumn oChkB;   // Write Off        9  Y / N   

        Util core;

        string[][] MandatoryFields;
   
        bool[] rowTicked;

        List<DATA> fieldEntered = new List<DATA>();
        bool[] MandRows;

        string[][] MandatoryRows;
        
        bool[] MandatorySelected;

        bool formloaded;

        public frmYarnStockAdj()
        {
            InitializeComponent();
            core = new Util();

            MandatoryFields = new string[][]
                {   new string[] {"txtApprovedBy", "Please enter the approved by details", "0", "10"},
                    new string[] {"rtbNotes", "Please the details in notes", "1", "10"}
                };

            MandatoryRows = new string[][]
                {   new string[] {"5", "Please enter the number of cones", "0"},
                    new string[] {"6", "Please enter the nett weight", "1"}
                 };

            oTxtA = new DataGridViewTextBoxColumn();
            oTxtA.HeaderText = "Yarn Key No";
            oTxtA.ValueType = typeof(int);
            oTxtA.Visible = false;

            oTxtB = new DataGridViewTextBoxColumn();
            oTxtB.HeaderText = "Pallet Number";
            oTxtB.ValueType = typeof(int);
            oTxtB.Visible = true;
            oTxtB.ReadOnly = true;

            oTxtC = new DataGridViewTextBoxColumn();
            oTxtC.HeaderText = "Yarn Description";
            oTxtC.ValueType = typeof(string);
            oTxtC.Visible = true;
            oTxtC.ReadOnly = true;

            oTxtD = new DataGridViewTextBoxColumn();
            oTxtD.HeaderText = "Tex Count";
            oTxtD.ValueType = typeof(string);
            oTxtD.Visible = true;
            oTxtD.ReadOnly = true;

            oTxtE = new DataGridViewTextBoxColumn();
            oTxtE.HeaderText = "Twist";
            oTxtE.ValueType = typeof(string);
            oTxtE.ReadOnly = true;
            oTxtE.Visible = true;

            oTxtF = new DataGridViewTextBoxColumn();
            oTxtF.HeaderText = "Identification";
            oTxtF.ValueType = typeof(string);
            oTxtF.Visible = true;

            oTxtG = new DataGridViewTextBoxColumn();
            oTxtG.HeaderText = "No of Cones";
            oTxtG.ValueType = typeof(int);
            oTxtG.Visible = true;

            oTxtH = new DataGridViewTextBoxColumn();
            oTxtH.HeaderText = "Nett Weight";
            oTxtH.ValueType = typeof(int);
            oTxtH.Visible = true;

            oChkA = new DataGridViewCheckBoxColumn();
            oChkA.HeaderText = "Select";
            oChkA.ValueType = typeof(bool);

            oChkB = new DataGridViewCheckBoxColumn();
            oChkB.HeaderText = "Write On / Off (Tick for Off)";
            oChkB.ValueType = typeof(bool);

            dataGridView1.Columns.Add(oTxtA); // 0
            dataGridView1.Columns.Add(oChkA); // 1
            dataGridView1.Columns.Add(oTxtB); // 2
            dataGridView1.Columns.Add(oTxtC); // 3 
            dataGridView1.Columns.Add(oTxtD); // 4
            dataGridView1.Columns.Add(oTxtE); // 5
            dataGridView1.Columns.Add(oTxtF); // 6
            dataGridView1.Columns.Add(oTxtG); // 7
            dataGridView1.Columns.Add(oTxtH); // 8
            dataGridView1.Columns.Add(oChkB); // 9

            this.dataGridView1.EditingControlShowing += new DataGridViewEditingControlShowingEventHandler(dataGridView1_EditingControlShowing);
            this.dataGridView1.RowsAdded += new DataGridViewRowsAddedEventHandler(dataGridView1_RowsAdded);
            this.dataGridView1.CellContentClick += new DataGridViewCellEventHandler(Check_Changed);
            this.dataGridView1.CellValidating += new DataGridViewCellValidatingEventHandler(dataGridView1_CellValidating);
            this.dataGridView1.RowsAdded += new DataGridViewRowsAddedEventHandler(dataGridView1_RowsAdded);

            SetUp();
        }

        void SetUp()
        {
            rbCommission.Checked = true;
            formloaded = false;
            using (var context = new TTI2Entities())
            {
                var LastNumberUsed = context.TLADM_LastNumberUsed.Find(2);
                if(LastNumberUsed != null)
                    txtAdjustmentNo.Text = "YA" + LastNumberUsed.col5.ToString().PadLeft(4, ' ').Replace(' ', '0');

            }
            MandatorySelected = core.PopulateArray(MandatoryFields.Length, false);
            formloaded = true;

        }

        private struct DATA
        {
            public int rownumber;
            public bool[] fieldComplete;

            public DATA(int rownumber, bool[] fieldComplete)
            {
                this.rownumber = rownumber;
                this.fieldComplete = fieldComplete;
            }
        }

        void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            DataGridView oDgv = sender as DataGridView;
            ComboBox oCombo = e.Control as ComboBox;
            CheckBox oChk = e.Control as CheckBox;

            if (formloaded)
            {
                if (oDgv.Focused && oDgv.CurrentCell is DataGridViewTextBoxCell)
                {
                    var Cell = oDgv.CurrentCell;
                    if (Cell.ColumnIndex == 7)
                    {
                        e.Control.KeyDown -= new KeyEventHandler(core.txtWin_KeyDown);
                        e.Control.KeyDown += new KeyEventHandler(core.txtWin_KeyDown);
                        e.Control.KeyPress -= new KeyPressEventHandler(core.txtWin_KeyPress);
                        e.Control.KeyPress += new KeyPressEventHandler(core.txtWin_KeyPress);
                    }
                    else if (Cell.ColumnIndex == 8)
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

        private void Check_Changed(object sender,  DataGridViewCellEventArgs e)
        {
           DataGridView oDgv = sender as DataGridView;
           if (oDgv.Focused && oDgv.CurrentCell is DataGridViewCheckBoxCell)
           {
               var Cell = oDgv.CurrentCell;
               if (Cell.ColumnIndex == 9)
               {
                   if ((bool)Cell.EditedFormattedValue)
                   {
                       var ActiveRow = oDgv.CurrentRow;
                       if (ActiveRow != null)
                       {

                           dataGridView1.Rows[ActiveRow.Index].Cells[7].Value = -1 * Convert.ToDecimal(dataGridView1.Rows[ActiveRow.Index].Cells[7].Value.ToString());
                           dataGridView1.Rows[ActiveRow.Index].Cells[8].Value = -1 * Convert.ToDecimal(dataGridView1.Rows[ActiveRow.Index].Cells[8].Value.ToString());
                           rowTicked[ActiveRow.Index] = true;
                       }

                   }
                   else
                   {
                       var ActiveRow = oDgv.CurrentRow;
                       if (rowTicked[ActiveRow.Index])
                       {
                           dataGridView1.Rows[ActiveRow.Index].Cells[7].Value = -1 * Convert.ToDecimal(dataGridView1.Rows[ActiveRow.Index].Cells[7].Value.ToString());
                           dataGridView1.Rows[ActiveRow.Index].Cells[8].Value = -1 * Convert.ToDecimal(dataGridView1.Rows[ActiveRow.Index].Cells[8].Value.ToString());
                           rowTicked[ActiveRow.Index] = false;
                       }
                   }
               }
            
           }
        }
       
        private void dataGridView1_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            DataGridView oDgv = sender as DataGridView;
           
        }

        private void dataGridView1_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            /*
           if (e.ColumnIndex == 5 ||
                e.ColumnIndex == 6)
            {
                if (e.ColumnIndex == 5)
                {
                    if (Convert.ToInt32(e.FormattedValue.ToString()) == 0)
                    {
                        var result = (from u in MandatoryRows
                                      where u[0] == e.ColumnIndex.ToString()
                                      select u).FirstOrDefault();

                        if (result != null)
                        {
                            MessageBox.Show(result[1]);
                        }
                        e.Cancel = true;
                    }
                    else
                    {
                        var record = fieldEntered.Find(x => x.rownumber == e.RowIndex);
                        if (record.fieldComplete != null)
                        {
                            var index = fieldEntered.IndexOf(record);

                            var result = (from u in MandatoryRows
                                          where u[0] == e.ColumnIndex.ToString()
                                          select u).FirstOrDefault();

                            if (result != null)
                            {
                                int a = Convert.ToInt32(result[2]);
                                record.fieldComplete[a] = true;
                            }

                            fieldEntered[index] = record;
                        }
                    }
                    
                }
                if (e.ColumnIndex == 6)
                {
                    if (Convert.ToInt32(e.FormattedValue.ToString()) == 0)
                    {
                        var result = (from u in MandatoryRows
                                      where u[0] == e.ColumnIndex.ToString()
                                      select u).FirstOrDefault();

                        if (result != null)
                        {
                            MessageBox.Show(result[1]);
                        }
                        e.Cancel = true;
                    }
                    else
                    {
                        var record = fieldEntered.Find(x => x.rownumber == e.RowIndex);
                        if (record.fieldComplete != null)
                        {
                            var index = fieldEntered.IndexOf(record);

                            var result = (from u in MandatoryRows
                                          where u[0] == e.ColumnIndex.ToString()
                                          select u).FirstOrDefault();

                            if (result != null)
                            {
                                int a = Convert.ToInt32(result[2]);
                                record.fieldComplete[a] = true;
                            }

                            fieldEntered[index] = record;
                        }
                    }

                }
               
            }
           
            */

        }

        private void rbCommission_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton oRadb = sender as RadioButton;
            if (oRadb != null && formloaded)
            {
                if (oRadb.Checked)
                {
                    dataGridView1.Rows.Clear();
                    fieldEntered.Clear();
                  
                    using (var context = new TTI2Entities())
                    {
                        formloaded = false;
                        oCmboTransactions.DataSource = null;
                        oCmboTransactions.DataSource = context.TLKNI_YarnTransaction.Where(x => !x.KnitY_ThirdParty && !x.KnitY_RTS).ToList();
                        oCmboTransactions.ValueMember = "KnitY_Pk";
                        oCmboTransactions.DisplayMember = "KnitY_GRNNumber";
                        oCmboTransactions.SelectedValue = 0;
                        oCmboTransactions.Text = string.Empty;
                        formloaded = true;
                    }
                  
                }
            }
        }

        private void rb3rdParty_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton oRadb = sender as RadioButton;
            if (oRadb != null && formloaded)
            {
                if (oRadb.Checked)
                {
                    dataGridView1.Rows.Clear();
                    fieldEntered.Clear();
                   
                    using (var context = new TTI2Entities())
                    {
                        formloaded = false;
                        oCmboTransactions.DataSource = null;
                        oCmboTransactions.DataSource = context.TLKNI_YarnTransaction.Where(x => x.KnitY_ThirdParty).ToList();
                        oCmboTransactions.ValueMember = "KnitY_Pk";
                        oCmboTransactions.DisplayMember = "KnitY_GRNNumber";
                        oCmboTransactions.SelectedValue = 0;
                        oCmboTransactions.Text = string.Empty;
                        formloaded = true;
                    }
                   
                }
            }
        }

        private void oCmboTransactions_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = sender as ComboBox;
            if (oCmbo != null && formloaded)
            {
                if (rb3rdParty.Checked || rbCommission.Checked)
                {
                    var OrderSelected = (TLKNI_YarnTransaction)oCmboTransactions.SelectedItem;
                    if (OrderSelected != null)
                    {
                        using (var context = new TTI2Entities())
                        {
                            var Pallets = context.TLKNI_YarnOrderPallets.Where(x => x.TLKNIOP_HeaderRecord_FK == OrderSelected.KnitY_Pk && x.TLKNIOP_Cones > 0).ToList();
                            if (Pallets != null)
                            {
                                    dataGridView1.Rows.Clear();

                                    foreach (var Pallet in Pallets)
                                    {
                                        var index = dataGridView1.Rows.Add();
                                        dataGridView1.Rows[index].Cells[0].Value = Pallet.TLKNIOP_Pk;
                                        dataGridView1.Rows[index].Cells[1].Value = false;
                                        dataGridView1.Rows[index].Cells[2].Value = Pallet.TLKNIOP_PalletNo;

                                        var YarnType = context.TLADM_Yarn.Where(x => x.YA_Id == Pallet.TLKNIOP_YarnType_FK).FirstOrDefault();
                                        if (YarnType != null)
                                        {
                                            dataGridView1.Rows[index].Cells[3].Value = YarnType.YA_Description;
                                            dataGridView1.Rows[index].Cells[4].Value = YarnType.YA_TexCount.ToString();
                                            dataGridView1.Rows[index].Cells[5].Value = YarnType.YA_Twist.ToString();
                                            dataGridView1.Rows[index].Cells[6].Value = YarnType.YA_ConeColour;
                                        }

                                        dataGridView1.Rows[index].Cells[7].Value = 0;     // Pallet.YarnOP_NoOfConesSpun;
                                        dataGridView1.Rows[index].Cells[8].Value = 0.00M; // Math.Round(Pallet.YarnOP_NettWeight,2);
                                        dataGridView1.Rows[index].Cells[9].Value = false;
                                    }

                                    rowTicked = core.PopulateArray(dataGridView1.Rows.Count, false);


                            }
                            

                        }

                    }
                }
                else
                {
                    var OrderSelected = (TLSPN_YarnOrder)oCmboTransactions.SelectedItem;
                    if (OrderSelected != null)
                    {
                        using (var context = new TTI2Entities())
                        {

                            var Pallets = context.TLSPN_YarnOrderPallets.Where(x => x.YarnOP_YarnOrder_FK == OrderSelected.YarnO_Pk).ToList();
                            if (Pallets != null)
                            {
                                dataGridView1.Rows.Clear();

                                foreach (var Pallet in Pallets)
                                {
                                    if (Pallet.YarnOP_RTS)
                                        continue;

                                    var index = dataGridView1.Rows.Add();
                                    dataGridView1.Rows[index].Cells[0].Value = Pallet.YarnOP_Pk;
                                    dataGridView1.Rows[index].Cells[1].Value = false;
                                    dataGridView1.Rows[index].Cells[2].Value = Pallet.YarnOP_PalletNo;

                                    var YarnType = context.TLADM_Yarn.Where(x => x.YA_Id == Pallet.YarnOP_YarnType_FK).FirstOrDefault();
                                    if (YarnType != null)
                                    {
                                        dataGridView1.Rows[index].Cells[3].Value = YarnType.YA_Description;
                                        dataGridView1.Rows[index].Cells[4].Value = YarnType.YA_TexCount.ToString();
                                        dataGridView1.Rows[index].Cells[5].Value = YarnType.YA_Twist.ToString();
                                        dataGridView1.Rows[index].Cells[6].Value = YarnType.YA_ConeColour;
                                    }

                                    dataGridView1.Rows[index].Cells[7].Value = 0;     // Pallet.YarnOP_NoOfConesSpun;
                                    dataGridView1.Rows[index].Cells[8].Value = 0.00M; // Math.Round(Pallet.YarnOP_NettWeight,2);
                                    dataGridView1.Rows[index].Cells[9].Value = false;
                                }

                                rowTicked = core.PopulateArray(dataGridView1.Rows.Count, false);


                            }

                        }

                    }
                }
            }
        }

        private void text_Changed(object sender, EventArgs e)
        {
            TextBox oTxt = sender as TextBox;
            RichTextBox oRtb = sender as RichTextBox;
            if (oTxt != null)
            {
                if (oTxt.TextLength > 0)
                {
                    var result = (from u in MandatoryFields
                                  where u[0] == oTxt.Name
                                  select u).FirstOrDefault();

                    if (result != null)
                    {
                        int nbr = Convert.ToInt32(result[2].ToString());
                        if (oTxt.TextLength != 0)
                            MandatorySelected[nbr] = true;
                        else
                            MandatorySelected[nbr] = false;

                    }
                }
            }
            else
            {
                if (oRtb.TextLength > 0)
                {
                    var result = (from u in MandatoryFields
                                  where u[0] == oRtb.Name
                                  select u).FirstOrDefault();

                    if (result != null)
                    {
                        int nbr = Convert.ToInt32(result[2].ToString());
                        if (oRtb.TextLength != 0)
                            MandatorySelected[nbr] = true;
                        else
                            MandatorySelected[nbr] = false;

                    }
                }

            }
        }


        private void btnSave_Click(object sender, EventArgs e)
        {

            Button oBtn = sender as Button;

            TLKNI_YarnTransactionDetails yarnTD;
            TLSPN_YarnOrderPallets palletStore;
            TLADM_TranactionType TranType = null;
            int LastNumberUsed = 0;

            bool success = true;

            if (oBtn != null && formloaded)
            {
                var errorM = core.returnMessage(MandatorySelected, true, MandatoryFields);
                if (!string.IsNullOrEmpty(errorM))
                {
                    MessageBox.Show(errorM);
                    return;
                }

                //-------------------------------------------------------------
                // must write out a header record 
                //------------------------------------------------------------------
                if (rbCommission.Checked || rb3rdParty.Checked)
                {
                    var YarnTrx = (TLKNI_YarnTransaction)oCmboTransactions.SelectedItem;
                    if (YarnTrx != null)
                    {

                        using (var context = new TTI2Entities())
                        {
                            var LastNumber = context.TLADM_LastNumberUsed.Find(2);
                            if (LastNumber != null)
                            {
                                LastNumberUsed = LastNumber.col5;
                                LastNumber.col5 += 1;
                            }

                            var Dept = context.TLADM_Departments.Where(x => x.Dep_ShortCode.Contains("KNIT")).FirstOrDefault();
                            if (Dept != null)
                            {
                                if (rbCommission.Checked)
                                    TranType = context.TLADM_TranactionType.Where(x => x.TrxT_Department_FK == Dept.Dep_Id && x.TrxT_Number == 600).FirstOrDefault();
                                else
                                    TranType = context.TLADM_TranactionType.Where(x => x.TrxT_Department_FK == Dept.Dep_Id && x.TrxT_Number == 500).FirstOrDefault();
                            }


                            try
                            {
                                context.SaveChanges();

                                var yt = (TLKNI_YarnTransaction)oCmboTransactions.SelectedItem;

                                foreach (DataGridViewRow dr in dataGridView1.Rows)
                                {
                                    if (!(bool)dr.Cells[1].Value)
                                        continue;

                                    yarnTD = new TLKNI_YarnTransactionDetails();
                                    yarnTD.KnitYD_KnitY_FK = YarnTrx.KnitY_Pk;

                                    var TypeOfYarn = dr.Cells[3].Value;
                                    var YarnDetails = context.TLADM_Yarn.Where(x => x.YA_Description.Contains(TypeOfYarn.ToString())).FirstOrDefault();
                                    if (YarnDetails != null)
                                        yarnTD.KnitYD_YarnType_FK = YarnDetails.YA_Id;

                                    yarnTD.KnitYD_PalletNo_FK = (int)dr.Cells[0].Value;
                                    yarnTD.KnitYD_NoOfCones = Convert.ToInt32(dr.Cells[7].Value.ToString());
                                    yarnTD.KnitYD_NettWeight = Convert.ToDecimal(dr.Cells[8].Value.ToString());
                                    yarnTD.KnitYD_TransactionNumber = LastNumberUsed;
                                    yarnTD.KnitYD_TransactionDate = dateTimePicker1.Value;
                                    yarnTD.KnitYD_ApprovedBy = txtApprovedBy.Text;
                                    yarnTD.KnitYD_Notes = rtbNotes.Text;
                                    yarnTD.KnitYD_RTS = false;

                                    if (yt != null)
                                        yarnTD.KnitYD_OriginalOrderNo = yt.KnitY_GRNNumber;

                                    if (TranType != null)
                                        yarnTD.KnitYD_TransactionType = TranType.TrxT_Pk;

                                    context.TLKNI_YarnTransactionDetails.Add(yarnTD);

                                    //--------------------------------------------------------------------
                                    // Now we have to keep the Status file uptodate
                                    //--------------------------------------------------------------------------
                                    var _key = (int)dr.Cells[0].Value;
                                    var palletStorex = context.TLKNI_YarnOrderPallets.Find(_key);
                                    if (palletStorex != null)
                                    {
                                        if ((bool)dr.Cells[9].Value == true)
                                        {
                                            palletStorex.TLKNIOP_Cones       -= yarnTD.KnitYD_NoOfCones;
                                            palletStorex.TLKNIOP_NettWeight  -= yarnTD.KnitYD_NettWeight;
                                            palletStorex.TLKNIOP_GrossWeight -= yarnTD.KnitYD_GrossWeight;
                                         }
                                         else
                                         {
                                            palletStorex.TLKNIOP_Cones       += yarnTD.KnitYD_NoOfCones;
                                            palletStorex.TLKNIOP_NettWeight  += yarnTD.KnitYD_NettWeight;
                                            palletStorex.TLKNIOP_GrossWeight += yarnTD.KnitYD_GrossWeight;
                                         }

                                        if (core.CalculatePalletNett(palletStorex) <= 0.00M)
                                            palletStorex.TLKNIOP_PalletAllocated = true;
                                        else
                                            palletStorex.TLKNIOP_PalletAllocated = false;
                                    
                                      }
                                }

                                context.SaveChanges();
                                MessageBox.Show("Data successfully stored to database");
                                frmKnitViewRep vRep = new frmKnitViewRep(6, LastNumberUsed);
                                int h = Screen.PrimaryScreen.WorkingArea.Height;
                                int w = Screen.PrimaryScreen.WorkingArea.Width;
                                vRep.ClientSize = new Size(w, h);
                                vRep.ShowDialog(this);
                                if (vRep != null)
                                {
                                    vRep.Close();
                                    vRep.Dispose();
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                                success = false;
                            }
                        }

                        if (success)
                        {
                            dataGridView1.Rows.Clear();
                            SetUp();
                        }

                    }
                }
                else
                {
                    

                    
                }
            }
        }

        private void rbOwnYarn_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton oRadb = sender as RadioButton;
            if (oRadb != null && formloaded)
            {
                if (oRadb.Checked)
                {
                    try
                    {
                      frmYarnAdjustment yarnA = new frmYarnAdjustment(false);
                        yarnA.ShowDialog(this);
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
