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
    public partial class frmWhseToWhseDelivery : Form
    {
        bool FormLoaded;
        bool InterWhseDel;

        DataGridViewTextBoxColumn oTxtBoxA;
        DataGridViewTextBoxColumn oTxtBoxB;
        DataGridViewTextBoxColumn oTxtBoxC;
        DataGridViewTextBoxColumn oTxtBoxD;
        DataGridViewTextBoxColumn oTxtBoxE;
        DataGridViewCheckBoxColumn oChkA;

        DataGridViewTextBoxColumn oTxtBoxZA;
        DataGridViewTextBoxColumn oTxtBoxZB;
        DataGridViewTextBoxColumn oTxtBoxZC;
        DataGridViewTextBoxColumn oTxtBoxZD;
        DataGridViewTextBoxColumn oTxtBoxZE;
        DataGridViewTextBoxColumn oTxtBoxZF;
        DataGridViewTextBoxColumn oTxtBoxZG;
        DataGridViewCheckBoxColumn oChkZA;

       
        public frmWhseToWhseDelivery(bool IsDelivery)
        {
            //Note - If IsDelivery True then the transaction is a Whse Delivery Note Creation 
            //       Else it is a Whse Receipt Transaction 
            //==================================================================

            InitializeComponent();
            InterWhseDel = IsDelivery;

            if (IsDelivery)
            {
                this.Text = "Inter Warehouse Delivery Note Creation ";
                this.label1.Text = "Current PickLists";
                this.label2.Text = "Current PickList Details";
            }
            else
            {
                this.Text = "Inter WareHouse Delivery Note Receipt";
                this.label1.Text = "Current Delivery Note Lists";
                this.label2.Text = "Current Delivery Note List Details";
            }

            oTxtBoxA = new DataGridViewTextBoxColumn();
            oTxtBoxA.ReadOnly = true;
            oTxtBoxA.Visible = false;
            oTxtBoxA.HeaderText = "Primary Key";
            dataGridView1.Columns.Add(oTxtBoxA);

            oChkA = new DataGridViewCheckBoxColumn();
            oChkA.ValueType = typeof(bool);
            oChkA.HeaderText = "Select";
            dataGridView1.Columns.Add(oChkA);

            oTxtBoxB = new DataGridViewTextBoxColumn();
            oTxtBoxB.HeaderText = "Trans Number";
            oTxtBoxB.ValueType = typeof(Int32);
            dataGridView1.Columns.Add(oTxtBoxB);

            oTxtBoxC = new DataGridViewTextBoxColumn();
            oTxtBoxC.HeaderText = "Date of Transaction";
            oTxtBoxC.ValueType = typeof(DateTime);
            dataGridView1.Columns.Add(oTxtBoxC);

            oTxtBoxD = new DataGridViewTextBoxColumn();
            oTxtBoxD.HeaderText = "From WareHouse";
            oTxtBoxD.ValueType = typeof(String);
            dataGridView1.Columns.Add(oTxtBoxD);

            oTxtBoxE = new DataGridViewTextBoxColumn();
            oTxtBoxE.HeaderText = "To WareHouse";
            oTxtBoxE.ValueType = typeof(String);
            dataGridView1.Columns.Add(oTxtBoxE);

            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.AllowUserToOrderColumns = false;

            //================================================================

            oTxtBoxZA = new DataGridViewTextBoxColumn();
            oTxtBoxZA.ReadOnly = true;
            oTxtBoxZA.Visible = false;
            oTxtBoxZA.HeaderText = "Primary Key";
            dataGridView2.Columns.Add(oTxtBoxZA);

            oChkZA = new DataGridViewCheckBoxColumn();
            oChkZA.ValueType = typeof(bool);
            oChkZA.HeaderText = "Select";
            dataGridView2.Columns.Add(oChkZA);

            oTxtBoxZB = new DataGridViewTextBoxColumn();
            oTxtBoxZB.HeaderText = "Box Number";
            oTxtBoxZB.ValueType = typeof(String);
            dataGridView2.Columns.Add(oTxtBoxZB);

            oTxtBoxZC = new DataGridViewTextBoxColumn();
            oTxtBoxZC.HeaderText = "Style";
            oTxtBoxZC.ValueType = typeof(string);
            dataGridView2.Columns.Add(oTxtBoxZC);

            oTxtBoxZD = new DataGridViewTextBoxColumn();
            oTxtBoxZD.HeaderText = "Colour";
            oTxtBoxZD.ValueType = typeof(String);
            dataGridView2.Columns.Add(oTxtBoxZD);

            oTxtBoxZE = new DataGridViewTextBoxColumn();
            oTxtBoxZE.HeaderText = "Size";
            oTxtBoxZE.ValueType = typeof(String);
            dataGridView2.Columns.Add(oTxtBoxZE);

            oTxtBoxZF = new DataGridViewTextBoxColumn();
            oTxtBoxZF.HeaderText = "Boxed Qty";
            oTxtBoxZF.ValueType = typeof(Int32);
            dataGridView2.Columns.Add(oTxtBoxZF);

            oTxtBoxZG = new DataGridViewTextBoxColumn();
            oTxtBoxZG.HeaderText = "Weighte";
            oTxtBoxZG.ValueType = typeof(Decimal);
            dataGridView2.Columns.Add(oTxtBoxZG);

            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.AllowUserToOrderColumns = false;

            dataGridView2.AllowUserToAddRows = false;
            dataGridView2.AllowUserToDeleteRows = false;
            dataGridView2.AllowUserToOrderColumns = false;

        }

        private void frmWhseToWhseDelivery_Load(object sender, EventArgs e)
        {
            FormLoaded = false;
            IList<TLCSV_WhseTransfer> Entries = null;

            oChkConfirmation.Checked = true;

            using (var context = new TTI2Entities())
            {
                if (InterWhseDel)
                {
                    Entries = context.TLCSV_WhseTransfer.Where(x => !x.TLCSVWHT_DeliveryNote).ToList();
                }
                else
                {
                    Entries = context.TLCSV_WhseTransfer.Where(x => x.TLCSVWHT_DeliveryNote && !x.TLCSVWHT_Receipted).ToList();
                }

                foreach (var Entry in Entries)
                {
                        var index = dataGridView1.Rows.Add();
                        dataGridView1.Rows[index].Cells[0].Value = Entry.TLCSVWHT_Pk;
                        dataGridView1.Rows[index].Cells[1].Value = false;
                        if (InterWhseDel)
                        {
                            dataGridView1.Rows[index].Cells[2].Value = Entry.TLCSVWHT_PickListNo;
                            dataGridView1.Rows[index].Cells[3].Value = Convert.ToDateTime(Entry.TLCSVWHT_PickListDate).ToString("dd/MM/yyyy");
                        }
                        else
                        {
                            dataGridView1.Rows[index].Cells[2].Value = Entry.TLCSVWHT_DeliveryNo;
                            dataGridView1.Rows[index].Cells[3].Value = Convert.ToDateTime(Entry.TLCSVWHT_PickListDate).ToString("dd/MM/yyyy");
                        }

                        dataGridView1.Rows[index].Cells[4].Value = context.TLADM_WhseStore.Find(Entry.TLCSVWHT_FromWhse_Fk).WhStore_Description;
                        dataGridView1.Rows[index].Cells[5].Value = context.TLADM_WhseStore.Find(Entry.TLCSVWHT_ToWhse_Fk).WhStore_Description;
                }
            }

            FormLoaded = true;

        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView oDgv = (DataGridView)sender;
            IList<TLCSV_WhseTransferDetail> WhseDetails = null;
            if (oDgv != null && FormLoaded)
            {
                if (e.ColumnIndex == 1)
                {
                    var CurrentRow = oDgv.CurrentRow;
                    foreach (DataGridViewRow Row in dataGridView1.Rows)
                    {
                        if (Row.Index == CurrentRow.Index)
                            continue;

                        Row.Cells[1].Value = false;
                    }

                    var IsChecked = (bool)CurrentRow.Cells[1].EditedFormattedValue;
                    if (IsChecked)
                    {
                        dataGridView2.Rows.Clear();
                        using (var context = new TTI2Entities())
                        {
                            var Pk = (int)CurrentRow.Cells[0].Value;
                            WhseDetails = context.TLCSV_WhseTransferDetail.Where(x => x.TLCSVWHTD_WhseTranfer_FK == Pk).ToList();
                            foreach (var WhseDetail in WhseDetails)
                            {
                                var index = dataGridView2.Rows.Add();
                                dataGridView2.Rows[index].Cells[0].Value = WhseDetail.TLCSVWHTD_Pk;
                                dataGridView2.Rows[index].Cells[1].Value = true;
                                var SOH = context.TLCSV_StockOnHand.Find(WhseDetail.TLCSVWHTD_TLSOH_Fk);
                                if(SOH != null)
                                {
                                    dataGridView2.Rows[index].Cells[2].Value = SOH.TLSOH_BoxNumber;
                                    dataGridView2.Rows[index].Cells[3].Value = context.TLADM_Styles.Find(SOH.TLSOH_Style_FK).Sty_Description;
                                    dataGridView2.Rows[index].Cells[4].Value = context.TLADM_Colours.Find(SOH.TLSOH_Colour_FK).Col_Display;
                                    dataGridView2.Rows[index].Cells[5].Value = context.TLADM_Sizes.Find(SOH.TLSOH_Size_FK).SI_Description;
                                    dataGridView2.Rows[index].Cells[6].Value = SOH.TLSOH_BoxedQty;
                                    dataGridView2.Rows[index].Cells[7].Value = Math.Round(SOH.TLSOH_Weight, 2);
                                }

                            }
                        }
                    }
                }
            }
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Button oBtn = (Button)sender;
            Boolean IsTicked;
            TLCSV_WhseTransfer WhseTrans = null;
            frmCSViewRep vRep = null;
            TLADM_LastNumberUsed LNU = null;

            if (oBtn != null && FormLoaded)
            {
                // Check that the user ie the blonde has checked at least one record 
                //--------------------------------------------------------------------
                var SingleRow = (from Rows in dataGridView1.Rows.Cast<DataGridViewRow>()
                                 where (bool)Rows.Cells[1].Value == true 
                                 select Rows).FirstOrDefault();
                
                if (SingleRow == null)
                {
                    MessageBox.Show("Please select at least one record");
                    return;
                }

                using (var context = new TTI2Entities())
                {
                    foreach (DataGridViewRow Row in dataGridView2.Rows)
                    {
                        IsTicked = (bool)Row.Cells[1].Value;
                        
                        var Pk = (int)Row.Cells[0].Value;
                        TLCSV_WhseTransferDetail WhseDetail = context.TLCSV_WhseTransferDetail.Find(Pk);

                        if (!IsTicked && WhseDetail != null)
                        {
                            context.TLCSV_WhseTransferDetail.Remove(WhseDetail);
                        }
                        else if (WhseDetail != null)
                        {
                            if (WhseTrans == null)
                                WhseTrans = context.TLCSV_WhseTransfer.Find(WhseDetail.TLCSVWHTD_WhseTranfer_FK);

                            var SOH = context.TLCSV_StockOnHand.Find(WhseDetail.TLCSVWHTD_TLSOH_Fk);
                            if (SOH != null && WhseTrans != null)
                            {
                                if (InterWhseDel && oChkConfirmation.Checked)
                                {
                                    SOH.TLSOH_WareHouse_FK = WhseTrans.TLCSVWHT_ToWhse_Fk;
                                    SOH.TLSOH_InTransit = false;
                                }
                                else
                                {
                                    if (!InterWhseDel)
                                    {
                                        SOH.TLSOH_WareHouse_FK = WhseTrans.TLCSVWHT_ToWhse_Fk;
                                        SOH.TLSOH_InTransit = false;
                                    }
                                    else
                                    {
                                        SOH.TLSOH_InTransit = true;
                                    }
                                }
                            }
                            

                            if (InterWhseDel)
                                WhseDetail.TLCSVWHTD_DeliveryNote = true;
                            else
                                WhseDetail.TLCSVWHTD_Receipted = true;
                       }

                    }

                    var Dept = context.TLADM_Departments.Where(x => x.Dep_ShortCode == "CSV").FirstOrDefault();
                    if (Dept != null)
                    {
                        LNU = context.TLADM_LastNumberUsed.Where(x => x.LUN_Department_FK == Dept.Dep_Id).FirstOrDefault();

                        if (InterWhseDel)
                        {
                            WhseTrans.TLCSVWHT_DeliveryNo = LNU.col7;
                            LNU.col7 += 1;
                            WhseTrans.TLCSVWHT_DeliveryNote = true;
                            WhseTrans.TLCSVWHT_Date = DateTime.Now;
                            if (oChkConfirmation.Checked)
                            {
                                WhseTrans.TLCSVWHT_ReceiptNo = LNU.col8;
                                LNU.col8 += 1;
                                WhseTrans.TLCSVWHT_Receipted = true;
                                WhseTrans.TLCSVWHT_Receipt_Date = DateTime.Now;
                               
                            }
                        }
                        else
                        {
                            WhseTrans.TLCSVWHT_ReceiptNo = LNU.col8;
                            LNU.col8 += 1;
                            WhseTrans.TLCSVWHT_Receipted = true;
                            WhseTrans.TLCSVWHT_Receipt_Date = DateTime.Now;
                        }
                    }

                    try
                    {
                        context.SaveChanges();
                        MessageBox.Show("data successfully saved to database");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message.ToString());
                    }
                }
                
                if(oChkConfirmation.Checked)
                    InterWhseDel = false;

                if (InterWhseDel)
                    vRep = new frmCSViewRep(19, WhseTrans.TLCSVWHT_Pk);
                else
                    vRep = new frmCSViewRep(20, WhseTrans.TLCSVWHT_Pk);

                int h = Screen.PrimaryScreen.WorkingArea.Height;
                int w = Screen.PrimaryScreen.WorkingArea.Width;
                vRep.ClientSize = new Size(w, h);
                vRep.ShowDialog(this);

                this.Close();
            }
        }

        private void oChkConfirmation_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox oChk = (CheckBox)sender;
            if (oChk != null && FormLoaded && oChk.Checked)
            {

            }
        }

       
    }
}
