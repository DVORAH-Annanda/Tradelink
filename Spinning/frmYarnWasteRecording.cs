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
    public partial class frmYarnWasteRecording : Form
    {
        DataGridViewTextBoxColumn oTxtBoxA;
        DataGridViewTextBoxColumn oTxtBoxB;
        DataGridViewTextBoxColumn oTxtBoxC;
        DataGridViewTextBoxColumn oTxtBoxD;
        DataGridViewCheckBoxColumn oChkA;
        bool Edit;
        Util core;
        bool formloaded;

        string[][] MandatoryRows;
        bool[] MandRows;

        List<DATA> fieldEntered = new List<DATA>();

        public frmYarnWasteRecording()
        {
            InitializeComponent();
            dataGridView1.AutoGenerateColumns = false;

            MandatoryRows = new string[][]
                {   new string[] {"1", "Please enter a pallet number", "0"},
                    new string[] {"2", "Please enter the gross weight", "1"},
                    new string[] {"3", "Please enter the nett weight", "2"}
                };
            oTxtBoxA = new DataGridViewTextBoxColumn();
            oTxtBoxA.Visible = false;

            oTxtBoxB = new DataGridViewTextBoxColumn();
            oTxtBoxB.HeaderText = "Waste Bale No";
            oTxtBoxB.ValueType = typeof(string);
            oTxtBoxB.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            oTxtBoxB.Width = 100;

            oTxtBoxC = new DataGridViewTextBoxColumn();
            oTxtBoxC.HeaderText = "Gross Weight KG";
            oTxtBoxC.ValueType = typeof(decimal);
            oTxtBoxC.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            oTxtBoxD = new DataGridViewTextBoxColumn();
            oTxtBoxD.HeaderText = "Nett Weight KG";
            oTxtBoxD.ValueType = typeof(decimal);
            oTxtBoxD.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            oChkA = new DataGridViewCheckBoxColumn();
            oChkA.Visible = false;
            oChkA.ValueType = typeof(Boolean);

            this.dataGridView1.Columns.Add(oTxtBoxA);       // 0 pk
            this.dataGridView1.Columns.Add(oTxtBoxB);       // 1 Bale No
            this.dataGridView1.Columns.Add(oTxtBoxC);       // 2 Gross Weight
            this.dataGridView1.Columns.Add(oTxtBoxD);       // 3 Nett Weight
      
            this.dataGridView1.EditingControlShowing += new DataGridViewEditingControlShowingEventHandler(dataGridView1_EditingControlShowing);
            this.dataGridView1.CellValidating += new DataGridViewCellValidatingEventHandler(dataGridView1_CellValidating);
            this.dataGridView1.RowsAdded += new DataGridViewRowsAddedEventHandler(dataGridView1_RowsAdded);
            SetUp();

        }

        public void SetUp()
        {
            formloaded = false;
            core = new Util();
            formloaded = true;
            Edit = false;

           
        }

        
        private void dataGridView1_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            var oDgv = sender as DataGridView;
            if (oDgv != null)
            {
                var ActiveRow = oDgv.CurrentRow;
                if (ActiveRow != null)
                {
                    MandRows = core.PopulateArray(MandatoryRows.Length, false);

                    fieldEntered.Add(new DATA(ActiveRow.Index, MandRows));
                }
                else
                {
                    oDgv.Rows[oDgv.NewRowIndex].Cells[2].Value = 0.00;
                    oDgv.Rows[oDgv.NewRowIndex].Cells[3].Value = 0.00;
                }

            }
            if (Edit)
            {
                try
                {
                    dataGridView1.CurrentCell = dataGridView1[1, this.dataGridView1.Rows.Count - 1];
                }
                catch
                {

                }
            }
           
        }

        private void dataGridView1_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            DataGridView oDgv = sender as DataGridView;
            var CurrentCell = oDgv.CurrentCell;
            bool Error = false;
            StringBuilder sb = new StringBuilder();
            if (!Edit)
            {
                if (e.ColumnIndex == 1 && !String.IsNullOrEmpty(CurrentCell.EditedFormattedValue.ToString()))
                {
                    foreach (DataGridViewRow row in oDgv.Rows)
                    {
                        if (row.Cells[0] == null || row.Cells[1].Value == null)
                            continue;

                        if (CurrentCell.EditedFormattedValue.ToString() == row.Cells[1].Value.ToString())
                        {
                           sb.Append("Bale number already entered into grid" + Environment.NewLine);
                           Error = true;

                        }

                    }

                    using (var context = new TTI2Entities())
                    {
                        var data = context.TLSPN_YarnWaste.Where(x => x.TLYW_BaleNo.Contains(CurrentCell.EditedFormattedValue.ToString())).FirstOrDefault();
                        if (data != null)
                        {
                            sb.Append("Bale number already entered on file" + Environment.NewLine);
                            Error = true;

                        }
                    }

                    if (Error)
                    {
                        MessageBox.Show(sb.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        e.Cancel = true;
                        return;

                    }
                   
                   
                     if (e.ColumnIndex == 2 || e.ColumnIndex == 3)
                     {
                         if (Convert.ToDecimal(e.FormattedValue.ToString()) == 0)
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
                else if (e.ColumnIndex == 2 || e.ColumnIndex == 3)
                {
                    if (Convert.ToDecimal(e.FormattedValue.ToString()) == 0)
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
            
        }
        void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            DataGridView oDgv = sender as DataGridView;
            if (oDgv != null && oDgv.Focused)
            {
                var Cell = oDgv.CurrentCell;
                if (Cell.ColumnIndex == 2 || Cell.ColumnIndex == 3)
                {
                    e.Control.KeyDown  -= core.txtWin_KeyDownOEM;
                    e.Control.KeyPress -= core.txtWin_KeyPress;
                    e.Control.KeyDown  += core.txtWin_KeyDownOEM;
                    e.Control.KeyPress += core.txtWin_KeyPress;
                }
                else
                {
                    e.Control.KeyDown  -= core.txtWin_KeyDownOEM;
                    e.Control.KeyPress -= core.txtWin_KeyPress;
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            TLSPN_YarnWaste YarnW;
            Button oBtn = sender as Button;
            bool Add;
            bool success = false;

            if (oBtn != null)
            {
                using (var context = new TTI2Entities())
                {
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        if (row.Cells[0] == null || row.Cells[1].Value == null)
                            continue;
                        /*
                        var tst = fieldEntered.Find(x => x.rownumber == row.Index);
                        if (tst.fieldComplete == null)
                            continue;

                        var cnt = tst.fieldComplete.Where(x => x == false).Count();
                        if (cnt != 0)
                            continue;

                        */

                        var Express = row.Cells[1].Value.ToString();
                        YarnW = context.TLSPN_YarnWaste.Where(x=>x.TLYW_BaleNo.Contains(Express)).FirstOrDefault();
                        if (YarnW != null)
                        {
                             YarnW = context.TLSPN_YarnWaste.Find(YarnW.TLYW_Pk);
                             Add = false;
                        }
                        else
                        {
                             YarnW = new TLSPN_YarnWaste();
                             Add = true;
                        }

                        var Dept = context.TLADM_Departments.Where(x => x.Dep_ShortCode.Contains("SPIN")).FirstOrDefault();
                        if (Dept != null)
                        {
                            var TranType = context.TLADM_TranactionType.Where(x => x.TrxT_Department_FK == Dept.Dep_Id && x.TrxT_Number == 1200).FirstOrDefault();
                            if (TranType != null)
                            {
                                YarnW.TLYW_TransactionType_In = TranType.TrxT_Pk;
                            }
                        }

                        YarnW.TLYW_BaleNo          = Express;
                        YarnW.TLYW_BaleGrossWeight = Convert.ToDecimal(row.Cells[2].Value.ToString());
                        YarnW.TLYW_BaleNettWeight  = Convert.ToDecimal(row.Cells[3].Value.ToString());
                        YarnW.TLYW_Date = dateTimePicker1.Value;

                        try
                        {
                             if (Add)
                                 context.TLSPN_YarnWaste.Add(YarnW);
                             
                             context.SaveChanges();
                             success = true;

                        }
                         catch (Exception ex)
                         {
                             MessageBox.Show(ex.Message);
                             success = false;
                             break;
                         }
                    }
                }
                if (success)
                {
                    dataGridView1.Rows.Clear();
                    MessageBox.Show("Data successfully stored to database");
                }
            }
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

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            DateTimePicker oDtp = sender as DateTimePicker;
            if (oDtp != null && formloaded)
            {
                using (var context = new TTI2Entities())
                {
                    DateTime dt = Convert.ToDateTime(oDtp.Value.ToShortDateString());
                    var Existing = context.TLSPN_YarnWaste.Where(x => x.TLYW_Date == dt).ToList();

                    if (Existing.Count > 0)
                    {

                        Edit = true;

                        foreach (var row in Existing)
                        {
                            var index = dataGridView1.Rows.Add();
                            dataGridView1.Rows[index].Cells[0].Value = row.TLYW_Pk;
                            dataGridView1.Rows[index].Cells[1].Value = row.TLYW_BaleNo;
                            dataGridView1.Rows[index].Cells[2].Value = Math.Round(row.TLYW_BaleGrossWeight, 1);
                            dataGridView1.Rows[index].Cells[3].Value = Math.Round(row.TLYW_BaleNettWeight, 1);
                           
                        }

                        Edit = false;
                        
                    }
                }
            }
        }

        private void btnReports_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;
            if (oBtn != null)
            {
                frmYarnWasteDateSelection wasteDate = new frmYarnWasteDateSelection();
                wasteDate.ShowDialog(this);
            }
        }
    }
}
