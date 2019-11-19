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

namespace Cutting
{
    public partial class frmReprintBIFTransfer : Form
    {
        bool FormLoaded;
        bool _Reprint;

        DataGridViewTextBoxColumn oTxtA = new DataGridViewTextBoxColumn();   // Primary Key File Record         0
        DataGridViewTextBoxColumn oTxtB = new DataGridViewTextBoxColumn();   // Primary Key in CutSheetReceipt  1
        DataGridViewCheckBoxColumn oChkA = new DataGridViewCheckBoxColumn(); // Check Box to select
        DataGridViewTextBoxColumn oTxtC = new DataGridViewTextBoxColumn();   // Primary Key File Record         0
        DataGridViewTextBoxColumn oTxtD = new DataGridViewTextBoxColumn();  
        
        public frmReprintBIFTransfer(bool Reprint)
        {
            InitializeComponent();
            if (Reprint)
            {
                this.Text = "Reprint Picking Lists Bought In Fabric";
                dataGridView1.Enabled = false;
                btnSave.Enabled = false;
                dataGridView1.Visible = false;
                btnSave.Visible = false;
                rbPast.Visible = true;
                rbPending.Visible = true;
                groupBox1.Visible = true;
            }
            else
            {
                this.Text = "Picking Lists Confirmation";
                
                rbPast.Visible = false;
                rbPending.Visible = false;
                groupBox1.Visible = false;

                btnSave.Enabled = true;
                dataGridView1.Enabled = true;
                btnSave.Visible = true;
                dataGridView1.Visible = true;

                oTxtA.HeaderText = "XXXX";
                oTxtA.ValueType = typeof(System.Int32);
                oTxtA.Visible = false;
                dataGridView1.Columns.Add(oTxtA);

                oTxtB.HeaderText = "XXXX";
                oTxtB.ValueType = typeof(System.Int32);
                oTxtB.Visible = false;
                dataGridView1.Columns.Add(oTxtB);

                oChkA.HeaderText = "select";
                oChkA.ValueType = typeof(System.Boolean);
                oChkA.Visible = true;
                dataGridView1.Columns.Add(oChkA);


                oTxtC.HeaderText = "Piece No";
                oTxtC.ValueType = typeof(System.String);
                oTxtC.Visible = true;
                dataGridView1.Columns.Add(oTxtC);

                dataGridView1.AllowUserToAddRows = false;
                dataGridView1.AllowUserToOrderColumns = false;

            }

            _Reprint = Reprint;

        }

        private void frmReprintBIFTransfer_Load(object sender, EventArgs e)
        {
            FormLoaded = false;
            
            using (var context = new TTI2Entities())
            {
                if (rbPending.Checked && _Reprint)
                {
                    cmboPickingLists.DataSource = context.TLDYE_BIFInTransit.Where(x => x.BIFT_PickingList && !x.BIFT_Despatched).OrderBy(x => x.BIFT_PickingList_Number).ToList();
                    cmboPickingLists.ValueMember = "BIFT_Pk";
                    cmboPickingLists.DisplayMember = "BIFT_PickingList_Number";
                    cmboPickingLists.SelectedValue = -1;
                }
            }

            FormLoaded = true;

        }

        private void cmboPickingLists_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = (ComboBox)sender;
            if (oCmbo != null && FormLoaded)
            {
                var BIFTransit = (TLDYE_BIFInTransit)oCmbo.SelectedItem;
                if (BIFTransit != null)
                {
                    if (_Reprint)
                    {
                        Cutting.frmCutViewRep vRep = new Cutting.frmCutViewRep(16, BIFTransit.BIFT_Pk);
                        int h = Screen.PrimaryScreen.WorkingArea.Height;
                        int w = Screen.PrimaryScreen.WorkingArea.Width;
                        vRep.ClientSize = new Size(w, h);
                        vRep.ShowDialog();
                    }
                    else
                    {
                        dataGridView1.Rows.Clear();

                        using (var context = new TTI2Entities())
                        {
                            var Entries = context.TLDYE_BIFInTransitDetails.Where(x => x.BIFD_Intransit_FK == BIFTransit.BIFT_Pk).ToList();
                            foreach (var Entry in Entries)
                            {
                                var index = dataGridView1.Rows.Add();
                                dataGridView1.Rows[index].Cells[0].Value = Entry.BIFD_Pk;
                                dataGridView1.Rows[index].Cells[1].Value = Entry.BIFD_DyeBatchDetail_FK;
                                dataGridView1.Rows[index].Cells[2].Value = true;
                                var GreigePk = Entry.BIFD_Greige_FK;
                                dataGridView1.Rows[index].Cells[3].Value = context.TLKNI_GreigeProduction.Find(GreigePk).GreigeP_PieceNo;
                            }

                        }

                    }
                }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
             var oDgv = sender as DataGridView;
             if (oDgv.Focused && oDgv.CurrentCell is DataGridViewCheckBoxCell)
             {
                 bool isChecked = (bool)oDgv[e.ColumnIndex, e.RowIndex].EditedFormattedValue;
                 if (!isChecked)
                 {
                     DialogResult Res = MessageBox.Show("Please confirm this transaction", "Confirmation Required", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                     if (Res == DialogResult.Yes)
                     {
                         using (var context = new TTI2Entities())
                         {
                             var DetailKey = (int)oDgv.CurrentRow.Cells[0].Value;

                             var Detail = context.TLDYE_BIFInTransitDetails.Find(DetailKey);
                             if (Detail != null)
                             {
                                 var DyeBatchDetail = context.TLDYE_DyeBatchDetails.Find(Detail.BIFD_DyeBatchDetail_FK);
                                 if (DyeBatchDetail != null)
                                 {
                                     DyeBatchDetail.DYEBO_BIFInTransit = false;
                                 }
                             }

                             context.TLDYE_BIFInTransitDetails.Remove(Detail);

                             try
                             {
                                 context.SaveChanges();
                                 MessageBox.Show("Data Sucessfully updated to database");
                                 oDgv.Rows.RemoveAt(this.dataGridView1.Rows[e.RowIndex].Index);
                                 return;
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

        private void rbPast_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton oRad = (RadioButton)sender;
            if (oRad != null && oRad.Checked && _Reprint)
            {
                using (var context = new TTI2Entities())
                {
                    FormLoaded = false;
                    cmboPickingLists.DataSource = context.TLDYE_BIFInTransit.Where(x => x.BIFT_PickingList && x.BIFT_Despatched).OrderBy(x => x.BIFT_PickingList_Number).ToList();
                    cmboPickingLists.ValueMember = "BIFT_Pk";
                    cmboPickingLists.DisplayMember = "BIFT_PickingList_Number";
                    cmboPickingLists.SelectedValue = -1;
                    FormLoaded = true;

                }
            }
        }

        private void rbPending_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton oRad = (RadioButton)sender;
            if (oRad != null && oRad.Checked && _Reprint)
            {
                FormLoaded = false;
                using (var context = new TTI2Entities())
                {
                    FormLoaded = false;
                    cmboPickingLists.DataSource = context.TLDYE_BIFInTransit.Where(x => x.BIFT_PickingList && !x.BIFT_Despatched).OrderBy(x => x.BIFT_PickingList_Number).ToList();
                    cmboPickingLists.ValueMember = "BIFT_Pk";
                    cmboPickingLists.DisplayMember = "BIFT_PickingList_Number";
                    cmboPickingLists.SelectedValue = -1;
                    FormLoaded = true;
                }
                FormLoaded = true;
            }
        }
    }
}
