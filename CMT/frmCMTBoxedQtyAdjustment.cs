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


namespace CMT
{
    public partial class frmCMTBoxedQtyAdjustment : Form
    {
        bool FormLoaded;
        private DataTable dt;
        Util core;

        public frmCMTBoxedQtyAdjustment()
        {
            InitializeComponent();
            core = new Util();

            dt = new DataTable();

            DataColumn column = new DataColumn();

            //------------------------------------------------------
            // Create column 1. // This is Index Position of the measurement in the TLCMT_AuditMeasurent Recorded Table
            //----------------------------------------------
            column = new DataColumn();
            column.DataType = typeof(Int32);
            column.ColumnName = "CompWork_Pk";
            dt.Columns.Add(column);

            //-----------------------------------------------------------
            // Create column 2. //  
            //----------------------------------------------
            column = new DataColumn();
            column.DataType = typeof(bool);
            column.ColumnName = "Select";
            column.Caption = "Select Record";
            dt.Columns.Add(column);

            //-----------------------------------------------------------
            // Create column 2. //  
            //----------------------------------------------
            column = new DataColumn();
            column.DataType = typeof(String);
            column.ColumnName = "BoxNumber";
            column.Caption = "Box Number";
            column.ReadOnly = true;
            dt.Columns.Add(column);

            //-----------------------------------------------------------
            // Create column 3. //  
            //----------------------------------------------
            column = new DataColumn();
            column.DataType = typeof(String);
            column.ColumnName = "Size";
            column.Caption = "Size";
            column.ReadOnly = true;
            dt.Columns.Add(column);

            //-----------------------------------------------------------
            // Create column 4. //  
            //----------------------------------------------
            column = new DataColumn();
            column.DataType = typeof(Int32);
            column.ColumnName = "BoxQty";
            column.Caption = "Box Qty";
            dt.Columns.Add(column);

            dataGridView1.DataSource = dt;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToOrderColumns = false;

            int idx = -1;

            foreach (DataColumn col in dt.Columns)
            {
                if (++idx == 0)
                {
                    dataGridView1.Columns[idx].Visible = false;
                }
                else
                {
                    dataGridView1.Columns[col.ColumnName].HeaderText = col.Caption;
                    dataGridView1.Columns[col.ColumnName].Width = 120;
                }
            }

        }

        private void frmCMTBoxedQtyAdjustment_Load(object sender, EventArgs e)
        {
            dt.Rows.Clear();

            using (var context = new TTI2Entities())
            {
                FormLoaded = false;
                var CutSheets = (from CSheets in context.TLCUT_CutSheet
                                 join CWork in context.TLCMT_CompletedWork on CSheets.TLCutSH_Pk equals CWork.TLCMTWC_CutSheet_FK
                                 where CWork.TLCMTWC_Picked && CWork.TLCMTWC_Despatched && !CWork.TLCMTWC_BoxReceiptedWhse
                                 select CSheets).Distinct().ToList();

                cmboCutSheets.DataSource = CutSheets;
                cmboCutSheets.DisplayMember = "TLCutSH_No";
                cmboCutSheets.ValueMember = "TLCutSH_Pk";
                cmboCutSheets.SelectedValue = -1;
                FormLoaded = true;
            }
        }

        private void cmboCutSheets_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = (ComboBox)sender;
            if(oCmbo != null & FormLoaded)
            {
                var Selected = (TLCUT_CutSheet)oCmbo.SelectedItem;
                if (Selected != null)
                {
                    using (var context = new TTI2Entities())
                    {
                        dt.Rows.Clear();

                        var CompleteWork = context.TLCMT_CompletedWork.Where(x => x.TLCMTWC_CutSheet_FK == Selected.TLCutSH_Pk).ToList();
                        foreach(var CWork in CompleteWork)
                        {
                            DataRow NewRow = dt.NewRow();

                            NewRow[0] = CWork.TLCMTWC_Pk;
                            NewRow[1] = false;
                            NewRow[2] = CWork.TLCMTWC_BoxNumber;
                            NewRow[3] = context.TLADM_Sizes.Find(CWork.TLCMTWC_Size_FK).SI_Description;
                            NewRow[4] = CWork.TLCMTWC_Qty;

                            dt.Rows.Add(NewRow);

                        }
                    }
                }
            }
        }

        private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            DataGridView oDgv = sender as DataGridView;
            ComboBox combo = e.Control as ComboBox;

            if (FormLoaded)
            {
                if (oDgv.Focused && oDgv.CurrentCell is DataGridViewTextBoxCell)
                {
                    var Cell = oDgv.CurrentCell;

                    if (Cell.ColumnIndex == 4)
                    {
                        e.Control.KeyDown -= new KeyEventHandler(core.txtWin_KeyDownJI);
                        e.Control.KeyDown += new KeyEventHandler(core.txtWin_KeyDownJI);
                        e.Control.KeyPress -= new KeyPressEventHandler(core.txtWin_KeyPress);
                        e.Control.KeyPress += new KeyPressEventHandler(core.txtWin_KeyPress);
                    }
                    else
                    {
                        e.Control.KeyDown -= new KeyEventHandler(core.txtWin_KeyDown);
                        e.Control.KeyPress -= new KeyPressEventHandler(core.txtWin_KeyPress);
                    }
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Button oBtn = (Button)sender;
            if(oBtn != null && FormLoaded)
            {
                var selected = (TLCUT_CutSheet)cmboCutSheets.SelectedItem;
                if(selected == null)
                {
                    MessageBox.Show("Please select a cut sheet from the drop down list");
                    return;
                }

                using (var context = new TTI2Entities())
                {
                    foreach(DataRow Row in dt.Rows)
                    {
                        if (!Row.Field<bool>(1))
                            continue;

                        var ComWork_FK = Row.Field<int>(0);
                        var CompletedWork = context.TLCMT_CompletedWork.Find(ComWork_FK);
                        if(CompletedWork != null)
                        {
                            TLCMT_HistoryBoxedQty History = new TLCMT_HistoryBoxedQty();
                            History.BoxHist_CompletedWork_FK = CompletedWork.TLCMTWC_Pk;
                            History.BoxHist_DateTime = DateTime.Now;
                            History.BoxHist_New_Qty = Row.Field<int>(4);
                            History.BoxHist_Orignal_Qty = CompletedWork.TLCMTWC_Qty;
                            History.BoxHist_Size_FK = CompletedWork.TLCMTWC_Size_FK;
                            History.BoxHist_CutSheet_FK = selected.TLCutSH_Pk;
                            History.BoxHist_No = CompletedWork.TLCMTWC_BoxNumber;

                            context.TLCMT_HistoryBoxedQty.Add(History);

                            CompletedWork.TLCMTWC_Qty = Row.Field<int>(4);

                        }

                    }

                    try
                    {
                        context.SaveChanges();
                        MessageBox.Show("Data saved successfully to database");

                        frmCMTViewRep vRep = new frmCMTViewRep(34, selected.TLCutSH_Pk);
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
                    catch(Exception ex)
                    {
                        MessageBox.Show(ex.InnerException.ToString());
                    }
                    finally
                    {
                        this.frmCMTBoxedQtyAdjustment_Load(this, null);
                    }

                }
            }
        }
    }
}
