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

namespace CustomerServices
{
    public partial class frmWareHouseReceipt : Form
    {
        bool formloaded;

        DataGridViewTextBoxColumn oTxtA;
        DataGridViewCheckBoxColumn oChkA;
        DataGridViewTextBoxColumn oTxtB;
        DataGridViewTextBoxColumn oTxtC;
        DataGridViewTextBoxColumn oTxtD;

        public frmWareHouseReceipt()
        {
            InitializeComponent();
        }

        private void frmWareHouseReceipt_Load(object sender, EventArgs e)
        {
            formloaded = false;
            using (var context = new TTI2Entities())
            {
                try
                {
                    cmboWareHouse.DataSource = context.TLCSV_BoxSelected.Where(x => !x.TLCSV_Receipted).GroupBy(g => new { g.TLCSV_DNTransNumber}).Select(s=>s.FirstOrDefault()).ToList();
                    cmboWareHouse.ValueMember = "TLCSV_Pk";
                    cmboWareHouse.DisplayMember = "TLCSV_DNDeails";
                    cmboWareHouse.SelectedValue = -1;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

                oTxtA = new DataGridViewTextBoxColumn();
                oTxtA.Visible = false;
                oTxtA.HeaderText = string.Empty;
                oTxtA.ValueType = typeof(int);
                dataGridView1.Columns.Add(oTxtA);

                oChkA = new DataGridViewCheckBoxColumn();
                oChkA.HeaderText = "Select";
                oChkA.Visible = true;
                oChkA.ValueType = typeof(bool);
                dataGridView1.Columns.Add(oChkA);

                oTxtB = new DataGridViewTextBoxColumn();
                oTxtB.ValueType = typeof(string);
                oTxtB.ReadOnly = true;
                oTxtB.HeaderText = "Box Number";
                dataGridView1.Columns.Add(oTxtB);

                oTxtC = new DataGridViewTextBoxColumn();
                oTxtC.ValueType = typeof(string);
                oTxtC.HeaderText = "Grade";
                oTxtC.ReadOnly = true;
                dataGridView1.Columns.Add(oTxtC);

                oTxtC = new DataGridViewTextBoxColumn();
                oTxtC.ValueType = typeof(string);
                oTxtC.HeaderText = "Notes";
                dataGridView1.Columns.Add(oTxtC);

                dataGridView1.AllowUserToAddRows = false;
                dataGridView1.AutoGenerateColumns = false;

            }
            formloaded = true;
        }

        private void cmboWareHouse_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = sender as ComboBox;
            if (oCmbo != null && formloaded)
            {
                var selected = (TLCSV_BoxSelected)oCmbo.SelectedItem;
                if (selected != null)
                {
                    dataGridView1.Rows.Clear();

                    using (var context = new TTI2Entities())
                    {
                        txtToWhse.Text = context.TLADM_WhseStore.Find(selected.TLCSV_To_FK).WhStore_Description;

                        var Existing = context.TLCMT_CompletedWork.Where(x => x.TLCMTWC_DepatchedList_FK == selected.TLCSV_Pk).ToList();
                        foreach (var row in Existing)
                        {
                            var index = dataGridView1.Rows.Add();
                            dataGridView1.Rows[index].Cells[0].Value = row.TLCMTWC_Pk;
                            dataGridView1.Rows[index].Cells[1].Value = true;
                            dataGridView1.Rows[index].Cells[2].Value = row.TLCMTWC_BoxNumber;
                            dataGridView1.Rows[index].Cells[3].Value = row.TLCMTWC_Grade;
                            dataGridView1.Rows[index].Cells[4].Value = String.Empty;
                        }
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;
            if (oBtn != null && formloaded)
            {
                var selected = (TLCSV_BoxSelected)cmboWareHouse.SelectedItem;
                if (selected == null)
                {
                    MessageBox.Show("Please select a Delivery Note from the drop down box");
                    return;
                }

                var RecCount = dataGridView1.Rows.Cast<DataGridViewRow>().Where(x => (bool)x.Cells[1].Value == true).Count();

                if (RecCount == 0)
                {
                    MessageBox.Show("Please select a least one record to process");
                    return;
                }

                using (var context = new TTI2Entities())
                {
                    TLCSV_Movement move = new TLCSV_Movement();
                    move.TLMV_FromCMT_Fk = selected.TLCSV_From_FK;
                    move.TLMV_ToWhse_FK  = selected.TLCSV_To_FK;
                    move.TLMV_TransactionNumber = 1;
                    move.TLMV_TransDate = dtpTransDate.Value;
                    move.TLMV_BoxSelected_FK = selected.TLCSV_Pk;
                    move.TLMV_NoOfBoxes = 0;
                    move.TLMV_BoxedQty = 0;
                    move.TLMV_OriginalNumber = selected.TLCSV_DNDeails;
                    var moveDetails = context.TLCMT_CompletedWork.Where(x => x.TLCMTWC_DepatchedList_FK == selected.TLCSV_Pk).ToList();
                    if (moveDetails != null)
                    {
                        move.TLMV_NoOfBoxes = moveDetails.Count;
                        move.TLMV_BoxedQty = moveDetails.Sum(x => x.TLCMTWC_Qty);
                    }

                    context.TLCSV_Movement.Add(move);

                    try
                    {
                        context.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        return;
                    }

                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        bool isChecked = (bool)row.Cells[1].Value;
                        if (isChecked)
                        {
                            TLCSV_StockOnHand OnHand = new TLCSV_StockOnHand();
                            OnHand.TLSOH_WareHouse_FK = selected.TLCSV_To_FK;
                            OnHand.TLSOH_CMT_FK = (int)row.Cells[0].Value;
                            OnHand.TLSOH_BoxSelected_FK = selected.TLCSV_Pk;
                            OnHand.TLSOH_Movement_FK = move.TLMV_Pk;
                            OnHand.TLSOH_DateIntoStock = dtpTransDate.Value;

                            TLCMT_CompletedWork comWork = context.TLCMT_CompletedWork.Find((int)row.Cells[0].Value);
                            if (comWork != null)
                            {
                                comWork.TLCMTWC_BoxReceiptedWhse = true;
                                
                                var Already = context.TLCSV_StockOnHand.Where(x => x.TLSOH_BoxNumber == comWork.TLCMTWC_BoxNumber).FirstOrDefault();
                                if (Already != null)
                                    continue;

                                OnHand.TLSOH_BoxedQty = comWork.TLCMTWC_Qty;
                                OnHand.TLSOH_Style_FK = comWork.TLCMTWC_Style_FK;
                                OnHand.TLSOH_Colour_FK = comWork.TLCMTWC_Colour_FK;
                                OnHand.TLSOH_Size_FK = comWork.TLCMTWC_Size_FK;
                                OnHand.TLSOH_BoxNumber = comWork.TLCMTWC_BoxNumber;
                                OnHand.TLSOH_Weight = comWork.TLCMTWC_Weight;
                                OnHand.TLSOH_PastelNumber = comWork.TLCMTWC_PastelNumber;
                                OnHand.TLSOH_CutSheet_FK = comWork.TLCMTWC_CutSheet_FK;

                                if (comWork.TLCMTWC_PastelNumber.Length == 0)
                                {
                                    OnHand.TLSOH_PastelNumber = context.TLADM_Styles.Find(comWork.TLCMTWC_Style_FK).Sty_PastelNo.ToString();
                                    OnHand.TLSOH_PastelNumber += context.TLADM_Colours.Find(comWork.TLCMTWC_Colour_FK).Col_FinishedCode;
                                    OnHand.TLSOH_PastelNumber += "NG";
                                    OnHand.TLSOH_PastelNumber += context.TLADM_Sizes.Find(comWork.TLCMTWC_Size_FK).SI_PastelNo; 
                                }

                                OnHand.TLSOH_BoxType = comWork.TLCMTWC_BoxType_FK;
                                OnHand.TLSOH_Grade = comWork.TLCMTWC_Grade;
                                if (!comWork.TLCMTWC_Grade.Contains("A"))
                                {
                                    var Assoc = context.TLADM_WareHouseAssociation.Where(X => X.TLWA_PrimaryWareHouse == selected.TLCSV_To_FK).FirstOrDefault();
                                    if (Assoc != null)
                                    {
                                        OnHand.TLSOH_WareHouse_FK = (int)Assoc.TLWA_SecondaryWareHouse;
                                    }
                                }
                                else
                                {
                                    OnHand.TLSOH_Is_A = true;
                                }

                            }
                            context.TLCSV_StockOnHand.Add(OnHand);
                        }
                     }

                    var Existing = context.TLCSV_BoxSelected.Where(x => x.TLCSV_DNTransNumber == selected.TLCSV_DNTransNumber).ToList();
                    foreach (var row in Existing)
                    {
                        TLCSV_BoxSelected Box = new TLCSV_BoxSelected();
                        Box = context.TLCSV_BoxSelected.Find(row.TLCSV_Pk);
                        if (Box != null)
                        {
                            Box.TLCSV_Receipted     = true;
                            Box.TLCSV_DateReceipted = dtpTransDate.Value;
                        }
                    }

                    try
                    {
                        context.SaveChanges();
                        MessageBox.Show("Data saved successfully to database");
                        dataGridView1.Rows.Clear();

                        cmboWareHouse.DataSource = null;
                        cmboWareHouse.DataSource = context.TLCSV_BoxSelected.Where(x => !x.TLCSV_Receipted).GroupBy(g => new { g.TLCSV_DNTransNumber }).Select(s => s.FirstOrDefault()).ToList();
                        cmboWareHouse.ValueMember = "TLCSV_Pk";
                        cmboWareHouse.DisplayMember = "TLCSV_DNDeails";
                        cmboWareHouse.SelectedValue = -1;

                        frmCSViewRep vRep = new frmCSViewRep(3, selected.TLCSV_Pk);
                        int h = Screen.PrimaryScreen.WorkingArea.Height;
                        int w = Screen.PrimaryScreen.WorkingArea.Width;
                        vRep.ClientSize = new Size(w, h);
                        vRep.ShowDialog(this);
                    }
                    catch (Exception ex)
                    {
                        var exceptionMessages = new StringBuilder();
                        do
                        {
                            exceptionMessages.Append(ex.Message);
                            ex = ex.InnerException;
                        }
                        while (ex != null);

                        MessageBox.Show(exceptionMessages.ToString());
                    }                  
                }

            }
        }
    }
}
