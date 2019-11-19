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

namespace Spinning
{
    public partial class frmCottonQA : Form
    {
        DataGridViewCheckBoxColumn oChk;
        DataGridViewTextBoxColumn oTxtBoxA;
        DataGridViewTextBoxColumn oTxtBoxC;
        DataGridViewTextBoxColumn oTxtBoxD;
        DataGridViewTextBoxColumn oTxtBoxE;
        DataGridViewTextBoxColumn oTxtBoxF;
        DataGridViewTextBoxColumn oTxtBoxG;
        bool formloaded;
        Util core;
        public frmCottonQA()
        {
            InitializeComponent();
            core = new Util();
            
            txtFromMicr.Text = "0.00";
            txtFromMicr.KeyDown += core.txtWin_KeyDownOEM;
            txtFromMicr.KeyPress += core.txtWin_KeyPress;

            txtToMicra.Text = "0.00";
            txtToMicra.KeyDown += core.txtWin_KeyDownOEM;
            txtToMicra.KeyPress += core.txtWin_KeyPress;

            txtFromStaple.Text = "0.00";
            txtFromStaple.KeyDown += core.txtWin_KeyDownOEM;
            txtFromStaple.KeyPress += core.txtWin_KeyPress;

            txtToStaple.Text = "0.00";
            txtToStaple.KeyDown += core.txtWin_KeyDownOEM;
            txtToStaple.KeyPress += core.txtWin_KeyPress;

            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AutoGenerateColumns = false;

            SetUp();
        }

        void SetUp()
        {

            using (var context = new TTI2Entities())
            {
                IList<TLSPN_CottonTransactions> cotreceived = new List<TLSPN_CottonTransactions>();

               
                var existing = context.AltSelectCottonRecords().ToList();
                foreach (var row in existing)
                {
                    TLSPN_CottonTransactions cr = new TLSPN_CottonTransactions();
                    cr.cotrx_ContractNo_Fk = row.cotrx_ContractNo_Fk;
                    cr.cotrx_Customer_FK = row.cotrx_Customer_FK;
                    cr.cotrx_GrossAveBaleWeight = row.cotrx_GrossAveBaleWeight;
                    cr.cotrx_GrossWeight = row.cotrx_GrossWeight;
                    cr.cotrx_LotNo = row.cotrx_LotNo;
                    cr.cotrx_NettPerWB = row.cotrx_NettPerWB;
                    cr.cotrx_NetWeight = row.cotrx_NetWeight;
                    cr.cotrx_NoBales = row.cotrx_NoBales;
                    cr.cotrx_Notes = row.cotrx_Notes;
                    cr.cotrx_pk = row.cotrx_pk;
                    cr.cotrx_Return_No = row.cotrx_Return_No;
                    cr.cotrx_Supplier_FK = row.cotrx_Supplier_FK;
                    cr.cotrx_TransDate = row.cotrx_TransDate;
                    cr.cotrx_TranType = row.cotrx_TranType;
                    //cr.cotrx_VehReg_FK = row.cotrx_VehReg_FK;
                    cr.cotrx_WeighBridgeEmpty = row.cotrx_WeighBridgeEmpty;
                    cr.cotrx_WeighBridgeFull = row.cotrx_WeighBridgeFull;
                    cr.cotrx_WriteOff = row.cotrx_WriteOff;
                    cr.cottrx_NettAveBaleWeight = row.cottrx_NettAveBaleWeight;
                    cotreceived.Add(cr);
                }

                cmbLotNo.DataSource = cotreceived.OrderBy(x=>x.cotrx_LotNo).ToList();
                cmbLotNo.ValueMember = "cotrx_LotNo";
                cmbLotNo.DisplayMember = "cotrx_LotNo";
                cmbLotNo.SelectedValue = 0;

                oChk = new DataGridViewCheckBoxColumn();              //1
                oChk.HeaderText = "Confirmed";
                oChk.ValueType = typeof(Boolean);

                oTxtBoxA = new DataGridViewTextBoxColumn();
                oTxtBoxA.Visible = false;

               
                oTxtBoxC = new DataGridViewTextBoxColumn();          //3
                oTxtBoxC.HeaderText = "Bale No";
                oTxtBoxC.ValueType = typeof(int);
                oTxtBoxC.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                oTxtBoxC.ReadOnly = true;

                oTxtBoxD = new DataGridViewTextBoxColumn();          //4
                oTxtBoxD.HeaderText = "Mic";
                oTxtBoxD.ValueType = typeof(Decimal);
                oTxtBoxD.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                oTxtBoxD.ReadOnly = true;

                oTxtBoxE = new DataGridViewTextBoxColumn();          //5
                oTxtBoxE.HeaderText = "Kgs (NETT)";
                oTxtBoxE.ValueType = typeof(Decimal);
                oTxtBoxE.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                oTxtBoxE.ReadOnly = true;

                oTxtBoxF = new DataGridViewTextBoxColumn();         //6
                oTxtBoxF.HeaderText = "Staple";
                oTxtBoxF.ValueType = typeof(Decimal);
                oTxtBoxF.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                oTxtBoxF.ReadOnly = true;

                oTxtBoxG = new DataGridViewTextBoxColumn();         //6
                oTxtBoxG.HeaderText = "Kgs (GROSS)";
                oTxtBoxG.ValueType = typeof(Decimal);
                oTxtBoxG.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                oTxtBoxG.ReadOnly = true;
                oTxtBoxG.Visible = false;

                dataGridView1.EditingControlShowing += new DataGridViewEditingControlShowingEventHandler(dataGridView1_EditingControlShowing);

                dataGridView1.Columns.Add(oTxtBoxA);    // 0 Index Pk value
                dataGridView1.Columns.Add(oChk);        // 1 Selected
                dataGridView1.Columns.Add(oTxtBoxC);    // 2 Bales Numeric 
                dataGridView1.Columns.Add(oTxtBoxD);    // 3 MIC Decimal
                dataGridView1.Columns.Add(oTxtBoxE);    // 4 kgs (NETT) Decimal 
                dataGridView1.Columns.Add(oTxtBoxF);    // 5 Staple Decimal
                dataGridView1.Columns.Add(oTxtBoxG);    // 6 Kgs Gross

                formloaded = true;

                groupBox1.Enabled = false;
            }
        }

        void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            DataGridView oDgv = sender as DataGridView;
        }

        private void cmbLotNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = sender as ComboBox;
            if (oCmbo != null & formloaded)
            {
                dataGridView1.Rows.Clear();
                var selectedRecord = (TLSPN_CottonTransactions)oCmbo.SelectedItem;
                if (selectedRecord != null)
                {
                    groupBox1.Enabled = true;
                    using (var context = new TTI2Entities())
                    {
                        var ExistingData = context.TLSPN_CottonReceivedBales.Where(x => x.CotBales_LotNo == selectedRecord.cotrx_LotNo
                                           && !x.CoBales_IssuedToProd 
                                           && !x.CoBales_CottonSold 
                                           && !x.CoBales_CottonReturned 
                                           && !x.CotBales_ConfirmedByQA).ToList();

                        if (ExistingData.Count != 0)
                        {
                            txtFromMicr.Text = Math.Round(ExistingData.Min(x => x.CotBales_Mic),2).ToString();
                            txtToMicra.Text = Math.Round(ExistingData.Max(x => x.CotBales_Mic), 2).ToString();
                            txtFromStaple.Text = Math.Round(ExistingData.Min(x => x.CotBales_Staple),2).ToString();
                            txtToStaple.Text = Math.Round(ExistingData.Max(x => x.CotBales_Staple), 2).ToString();
                            foreach (var row in ExistingData)
                            {
                                var index = dataGridView1.Rows.Add();
                                dataGridView1.Rows[index].Cells[0].Value = row.CotBales_Pk;
                                dataGridView1.Rows[index].Cells[1].Value = false;
                                dataGridView1.Rows[index].Cells[2].Value = row.CotBales_BaleNo;
                                dataGridView1.Rows[index].Cells[3].Value = Math.Round(row.CotBales_Mic, 2);
                                dataGridView1.Rows[index].Cells[4].Value = Math.Round(row.CotBales_Weight_Nett, 2);
                                dataGridView1.Rows[index].Cells[5].Value = Math.Round(row.CotBales_Staple, 2);
                                dataGridView1.Rows[index].Cells[6].Value = Math.Round(row.CotBales_Weight_Gross, 2);
                            }
                        }
                    }
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;
            bool success = true;
            if (oBtn != null)
            {
                using (var context = new TTI2Entities())
                {
                    foreach (DataGridViewRow rw in dataGridView1.Rows)
                    {
                        if ((bool)rw.Cells[1].Value == false)
                            continue;

                        TLSPN_CottonReceivedBales bales = context.TLSPN_CottonReceivedBales.Find((int)rw.Cells[0].Value);
                        if (bales != null)
                        {
                            bales.CotBales_ConfirmedByQA = true;

                            try
                            {
                                context.SaveChanges();
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                                success = false;
                            }
                        }
                    }
                }

                if (success)
                {
                    dataGridView1.Rows.Clear();
                    MessageBox.Show("Records stored successfully to database");
                }
            }
        }

        private void cbGlobal_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox oChk = sender as CheckBox;
            if (oChk != null && oChk.Checked)
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    row.Cells[1].Value = true;
                }

                dataGridView1.Refresh();
            }
            else
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    row.Cells[1].Value = false;
                }
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox oChk = sender as CheckBox;
            decimal avgMicra = 0.00M;
            decimal avgStaple = 0.00M;
            decimal fromMicra = 0.00M;
            decimal toMicra = 0.00M;
            decimal fromStaple = 0.00M;
            decimal toStaple = 0.00M;

            if (oChk != null && formloaded)
            {
                if (oChk.Checked)
                {
                    fromMicra = Convert.ToDecimal(txtFromMicr.Text);
                    toMicra = Convert.ToDecimal(txtToMicra.Text);

                    fromStaple = Convert.ToDecimal(txtFromStaple.Text);
                    toStaple = Convert.ToDecimal(txtToStaple.Text);

                    if (fromMicra != 0 && toMicra != 0)
                    {
                        avgMicra = Math.Round(toMicra + fromMicra / 2, 2);

                    }

                    if (fromStaple != 0 && toStaple != 0)
                    {
                        avgStaple = Math.Round(toStaple + fromStaple / 2, 2);

                    }

                    if (avgMicra != 0 && avgStaple != 0)
                    {
                        using (var context = new TTI2Entities())
                        {
                            foreach (DataGridViewRow row in dataGridView1.Rows)
                            {
                                dataGridView1.Rows[row.Index].Cells[3].Value = avgMicra;
                                dataGridView1.Rows[row.Index].Cells[5].Value = avgStaple;

                                int index = (int)row.Cells[0].Value;
                                var record = context.TLSPN_CottonReceivedBales.Find(index);
                                if (record != null)
                                {
                                    record.CotBales_Mic = avgMicra;
                                    record.CotBales_Staple = avgStaple;
                                }

                            }

                            try
                            {
                                context.SaveChanges();
                                formloaded = false;
                                checkBox1.Checked = false;
                                formloaded = true;
                                MessageBox.Show("Records amended successfully");
                                
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
