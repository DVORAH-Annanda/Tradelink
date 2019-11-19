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
    public partial class frmPurchaseOrderConfig : Form
    {
        DataTable dt;
        TLCSV_PurchaseOrder PO;
        String BoxId;
        bool FormLoaded;
        Util core;
     
        int _PurchaseKey;

        IList<TLCSV_PuchaseOrderDetail> PODetails = null; 

        public string Number;
        public string PurchaseOrderNumber;

        int StylePk;
        int ColourPk;

        CustomerServices.Repository repo;
        CustomerServices.CustomerServicesParameters QueryParms;
       
        public frmPurchaseOrderConfig(int PurchaseOrder)
        {
            InitializeComponent();

            _PurchaseKey = PurchaseOrder;
          
            dt = new DataTable();
            core = new Util();
            repo = new Repository();
            QueryParms = new CustomerServicesParameters();
            
            txtPONumber.KeyPress += core.txtWin_KeyPress;
            txtPONumber.KeyDown  += core.txtWin_KeyDownJI;
        }

        private void frmPurchaseOrderConfig_Load(object sender, EventArgs e)
        {
            FormLoaded = false;
            using (var context = new TTI2Entities())
            {
               txtPONumber.Text = string.Empty;

               var POrder = context.TLCSV_PurchaseOrder.Find(_PurchaseKey);
               if (POrder != null)
               {
                     txtPONumber.Text = POrder.TLCSVPO_PurchaseOrder;
                     PODetails = context.TLCSV_PuchaseOrderDetail.Where(x => x.TLCUSTO_PurchaseOrder_FK == _PurchaseKey).ToList();

                     if (PODetails != null)
                     {
                            StylePk = PODetails.FirstOrDefault().TLCUSTO_Style_FK;
                            ColourPk = PODetails.FirstOrDefault().TLCUSTO_Colour_FK;

                            txtStyle.Text = context.TLADM_Styles.Find(StylePk).Sty_Description;
                            txtColour.Text = context.TLADM_Colours.Find(PODetails.FirstOrDefault().TLCUSTO_Colour_FK).Col_Display;
                            
                            DataColumn column;
                            column = new DataColumn();
                            column.DataType = typeof(string);
                            column.ColumnName = "Col0";
                            dt.Columns.Add(column);

                            dataGridViewx.DataSource = dt;

                            var ColDesc = dataGridViewx.Columns[0];
                            ColDesc.HeaderText = "Box Config Id";
                            ColDesc.ReadOnly = true;


                            foreach (var SZ in PODetails)
                            {
                                var SizePk = SZ.TLCUSTO_Size_FK;
                                var Size = context.TLADM_Sizes.Find(SizePk);
                                if (Size != null)
                                {
                                    column = new DataColumn();
                                    column.DataType = typeof(int);
                                    column.ColumnName = Size.SI_id.ToString();
                                    dt.Columns.Add(column);
                                    var ColumnCount = dt.Columns.Count;
                                    dt.Columns[-1 + ColumnCount].DefaultValue = 0;

                                    ColDesc = dataGridViewx.Columns[-1 + ColumnCount];
                                    ColDesc.HeaderText = Size.SI_Description;
                                }
                            }

                            column = new DataColumn();
                            column.DataType = typeof(Int32);
                            column.ColumnName = "Col999";
                            column.DefaultValue = 0;
                            dt.Columns.Add(column);

                            ColDesc = dataGridViewx.Columns[-1 + dataGridViewx.Columns.Count];
                            ColDesc.HeaderText = "Total Boxes";

                            DataRow Row = dt.NewRow();
                            BoxId = txtPONumber.Text.Substring(-4 + txtPONumber.Text.TrimEnd().Length, 4);
                            BoxId = BoxId + "-" + (dt.Rows.Count + 1).ToString().PadLeft(3, '0');
                            Row[0] = BoxId;

                            dt.Rows.Add(Row);
                         
                     }
               }
               btnSave.Text = "Save";

                
            }
            FormLoaded = true;
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

                    if (Cell.ColumnIndex > 0)
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

        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView oDgv = (DataGridView)sender;
            if (oDgv != null)
            {
                string PurchaseNumber = txtPONumber.Text.TrimEnd();
                BoxId = txtPONumber.Text.Substring(-4 + PurchaseNumber.Length, 4);
                BoxId = BoxId + "-" + (oDgv.NewRowIndex + 1).ToString().PadLeft(3, '0');
                oDgv.Rows[oDgv.NewRowIndex].Cells[0].Value = BoxId;
            }
      
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Button oBtn = (Button)sender;
            if (oBtn != null && FormLoaded)
            {
                Number = txtPONumber.Text.Substring(-4 + txtPONumber.Text.TrimEnd().Length, 4);
                //===========================================================================
                // We have to put in some form of logic to sum the various columns
                // and conmpare to PODetails and if there is a difference warn the user and abort
                //===============================================================================================
                using (var context = new TTI2Entities())
                {
                    foreach (DataColumn dc in dt.Columns)
                    {
                        if (dc.Ordinal > 0 && dc.Ordinal < -1 + dt.Columns.Count)
                        {
                            var ColTotal = 0;

                            try
                            {
                                foreach (DataRow dr in dt.Rows)
                                {
                                    ColTotal += dr.Field<int>(dc.Ordinal) * dr.Field<int>(-1 + dt.Columns.Count);
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message.ToString());
                                return;
                            }
                            
                            var SizePk = Convert.ToInt32(dc.ColumnName);

                            var SizeDetail = PODetails.Where(x => x.TLCUSTO_Size_FK == SizePk).FirstOrDefault();
                            if (SizeDetail != null)
                            {
                                if (ColTotal != SizeDetail.TLCUSTO_Qty)
                                {
                                    var TLSizes = context.TLADM_Sizes.Find(SizePk);
                                    if (TLSizes != null)
                                    {
                                        var Message = "There is a difference between what was ordered and what was captured for Size " + TLSizes.SI_Description;
                                        var TopMessage = "Ordered " + SizeDetail.TLCUSTO_Qty.ToString() + " vs a Repack value " + ColTotal.ToString();
                                        using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                                        {
                                           MessageBox.Show(Message, TopMessage, MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        }
                                        return;
                                    }
                                }
                            }
                        }
                    }
                    
                    PO = context.TLCSV_PurchaseOrder.Find(_PurchaseKey);
                    if (PO != null)
                        PO.TLCSVPO_RepackTransaction = true;

                    foreach (DataRow Row in dt.Rows)
                    {
                        int CellTotals = 0;
                        foreach (DataColumn dc in dt.Columns)
                        {
                            if (dc.DataType == typeof(String))
                                continue;

                            CellTotals += Row.Field<int>(dc.Ordinal);
                        }

                        if (CellTotals == 0)
                            continue;

                   
                        foreach (DataColumn dc in dt.Columns)
                        {
                            try
                            {
                                var SizeSelected = Convert.ToInt32(dc.ColumnName);
                                var SizeObject = context.TLADM_Sizes.Find(SizeSelected);
                                if (SizeObject != null)
                                {
                                    TLCSV_RePackConfig RePac = new TLCSV_RePackConfig();
                                    RePac.PORConfig_PONumber_Fk = _PurchaseKey;
                                    RePac.PORConfig_Colour_FK = ColourPk;
                                    RePac.PORConfig_Style_FK = StylePk;
                                    RePac.PORConfig_Size_FK = SizeObject.SI_id;
                                    RePac.PORConfig_SizeBoxQty = Row.Field<int>(dc.Ordinal);
                                    RePac.PORConfig_TotalBoxes = Row.Field<int>(dt.Columns.Count - 1);
                                    RePac.PORConfig_BoxNumber = Row.Field<string>(0);
                                    RePac.PORConfig_Display = Row.Field<String>(0) + " : " + SizeObject.SI_Description;
                                    RePac.PORConfig_BoxNumber_Key = Convert.ToInt32(Number);
                                    context.TLCSV_RePackConfig.Add(RePac);
                                }
                            }
                            catch (Exception ex)
                            {
                                continue;
                            }
                        }
                    }

                    try
                    {
                        context.SaveChanges();
                        MessageBox.Show("Data successfully saved to the database");
                        this.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }

        private void cmboSizes_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = (ComboBox)sender;
            if (oCmbo != null && FormLoaded && !oCmbo.DroppedDown)
            {
                oCmbo.DroppedDown = true;
            }
        }

        private void txtPONumber_Leave(object sender, EventArgs e)
        {
            TextBox oTxt = (TextBox)sender;
            if (oTxt != null && FormLoaded)
            {
                Number = oTxt.Text.TrimEnd().Substring(-4 + oTxt.Text.TrimEnd().Length, 4);
                PurchaseOrderNumber = oTxt.Text;
            }
        }

        private void dataGridViewx_KeyDown(object sender, KeyEventArgs e)
        {
            DataGridView oDgv = sender as DataGridView;
            if (oDgv != null && e.KeyCode == Keys.Enter)
            {
                dataGridViewx.CurrentCell = core.GetNextCell(oDgv, oDgv.CurrentCell);
                e.Handled = true;
            }
        }

        private void dataGridViewx_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void dataGridViewx_KeyUp(object sender, KeyEventArgs e)
        {
            DataGridView oDgv = sender as DataGridView;
            if (oDgv != null && e.KeyCode == Keys.Tab)
            {
                if (dataGridViewx.CurrentCell.ReadOnly)
                    dataGridViewx.CurrentCell = core.GetNextCell(oDgv, oDgv.CurrentCell);
                e.Handled = true;
            }
            else if (oDgv != null)
            {
                if (e.KeyCode == Keys.Left || e.KeyCode == Keys.Right)
                {
                    if (dataGridViewx.CurrentCell.ReadOnly)
                        dataGridViewx.CurrentCell = core.GetNextCell(oDgv, oDgv.CurrentCell);
                    e.Handled = true;
                }
            }
        }

        private void dataGridViewx_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            DataGridView oDgv = (DataGridView)sender;
            //==============================================
            if (oDgv != null && FormLoaded)
            {
                if (e.ColumnIndex == -1 + oDgv.Columns.Count)
                {
                   String CellValue = oDgv.CurrentRow.Cells[e.ColumnIndex].EditedFormattedValue.ToString();
                   if (String.IsNullOrEmpty(CellValue) || Convert.ToInt32(CellValue) == 0)
                   {
                       using (DialogCenteringService centeringService = new DialogCenteringService(this)) // center message box
                       {
                           MessageBox.Show("Please enter a value greater than 0", "Error", MessageBoxButtons.OK, MessageBoxIcon.Question);
                       }
                       e.Cancel = true;
                   }
                }
            }
        }

        private void dataGridViewx_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView oDgv = (DataGridView)sender;
            if (oDgv != null)
            {
                string PurchaseNumber = txtPONumber.Text.TrimEnd();
                BoxId = txtPONumber.Text.Substring(-4 + PurchaseNumber.Length, 4);
                BoxId = BoxId + "-" + (oDgv.NewRowIndex + 1).ToString().PadLeft(3, '0');
                oDgv.Rows[oDgv.NewRowIndex].Cells[0].Value = BoxId;
            }
        }
    }
}
