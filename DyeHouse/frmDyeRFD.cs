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
    public partial class frmDyeRFD : Form
    {
        bool FormLoaded;
        protected readonly TTI2Entities _context;
        DataTable DataT;
        BindingSource BindS;
        TLADM_LastNumberUsed LastNUsed; 
        public frmDyeRFD()
        {
            InitializeComponent();
            
            _context = new TTI2Entities();
            DataT = new DataTable();
            BindS = new BindingSource();
            DataColumn Column;
            
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.AllowUserToAddRows = false;

            //==========================================================================================
            // 1st task is to create the data table
            // Col 0
            //=====================================================================
            Column = new DataColumn();
            Column.DataType = typeof(int);
            Column.ColumnName = "RFD_Pk";
            Column.Caption = "RFD Key";
            Column.DefaultValue = 0;
            DataT.Columns.Add(Column);

            //==========================================================================================
            // Col 1
            //=====================================================================
            Column = new DataColumn();
            Column.DataType = typeof(bool);
            Column.ColumnName = "RFD_Select";
            Column.Caption = "Select";
            Column.DefaultValue = false;
            DataT.Columns.Add(Column);

            //==========================================================================================
            // Col 2
            //=====================================================================
            Column = new DataColumn();
            Column.DataType = typeof(string);
            Column.ColumnName = "RFD_CS";
            Column.Caption = "CutSheet";
            Column.DefaultValue = string.Empty;
            DataT.Columns.Add(Column);

            //==========================================================================================
            // Col 3
            //=====================================================================
            Column = new DataColumn();
            Column.DataType = typeof(string);
            Column.ColumnName = "BoxNo";
            Column.Caption = "Box Number";
            Column.DefaultValue = string.Empty;
            DataT.Columns.Add(Column);

            //==========================================================================================
            // Col 4
            //=====================================================================
            Column = new DataColumn();
            Column.DataType = typeof(int);
            Column.ColumnName = "RFD_BoxedQty";
            Column.Caption = "Boxed Qty";
            Column.DefaultValue = 0;
            DataT.Columns.Add(Column);

            //======================================================
            // DataGridView 
            //==========================================================
            DataGridViewTextBoxColumn selecta = new DataGridViewTextBoxColumn();
            selecta.Name = "PrRatingIndex";
            selecta.ValueType = typeof(Int32);
            selecta.DataPropertyName = DataT.Columns[0].ColumnName;
            selecta.HeaderText = "Rating Index";
            selecta.Visible = false;
            selecta.ReadOnly = true;
            dataGridView1.Columns.Add(selecta);
            dataGridView1.Columns[0].DisplayIndex = 0;

            DataGridViewCheckBoxColumn oChkA = new DataGridViewCheckBoxColumn();
            oChkA.Name = "Select";
            oChkA.ValueType = typeof(bool);
            oChkA.HeaderText = "Select";
            oChkA.DataPropertyName = DataT.Columns[1].ColumnName;
            oChkA.Visible = true;
            dataGridView1.Columns.Add(oChkA);
            dataGridView1.Columns[1].DisplayIndex = 1;

            DataGridViewTextBoxColumn selectb = new DataGridViewTextBoxColumn();
            selectb.Name = "BoxNumber";
            selectb.ValueType = typeof(string);
            selectb.DataPropertyName = DataT.Columns[2].ColumnName;
            selectb.HeaderText = "Box Number";
            selectb.ReadOnly = true;
            selectb.Visible = true;
            dataGridView1.Columns.Add(selectb);
            dataGridView1.Columns[2].DisplayIndex = 2;

            DataGridViewTextBoxColumn selectd = new DataGridViewTextBoxColumn();
            selectd.Name = "BoxedQty";
            selectd.ValueType = typeof(Int32);
            selectd.DataPropertyName = DataT.Columns[3].ColumnName;
            selectd.HeaderText = "Boxed Qty";
            selectd.ReadOnly = true;
            selectd.Visible = true;
            dataGridView1.Columns.Add(selectd);
            dataGridView1.Columns[3].DisplayIndex = 3;

            BindS.DataSource = DataT;
            dataGridView1.DataSource = BindS;

        }

        private void frmDyeRFD_Load(object sender, EventArgs e)
        {
            FormLoaded = false;

            DataT.Rows.Clear();

            LastNUsed = _context.TLADM_LastNumberUsed.Find(3);
            if(LastNUsed != null)
            {
                if(LastNUsed.col14 == 0)
                {
                    LastNUsed.col14 = 1;
                }

                label6.Text = LastNUsed.col14.ToString().PadLeft(5, '0');
            }
            cmboColours.DataSource = _context.TLADM_Colours.Where(x => !(bool)x.Col_Discontinued).OrderBy(x => x.Col_Display).ToList();
            cmboColours.DisplayMember = "Col_Display";
            cmboColours.ValueMember = "Col_Id";
            cmboColours.SelectedValue = -1;

            cmboStyles.DataSource = _context.TLADM_Styles.Where(x => x.Sty_PFD).OrderBy(x => x.Sty_Description).ToList();
            cmboStyles.DisplayMember = "Sty_Description";
            cmboStyles.ValueMember = "Sty_Id";
            cmboStyles.SelectedValue = -1;

            FormLoaded = true; 
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;
            if(oBtn != null && FormLoaded)
            {
                TLADM_Colours ColourSelected = (TLADM_Colours)cmboColours.SelectedItem;
                if (ColourSelected == null)
                {
                    using (DialogCenteringService centeringService = new DialogCenteringService(this))
                    {
                        MessageBox.Show("Please select a colour");
                    }

                    return;
                }

                foreach(DataRow Row in DataT.Rows)
                {
                    if(!Row.Field<bool>(1))
                    {
                        continue;
                    }

                    var Pk = Row.Field<int>(0);
                    var StockOnHand = _context.TLCSV_StockOnHand.Find(Pk);

                    try
                    {
                        if (StockOnHand != null)
                        {
                            StockOnHand.TLSOH_RFD_NotYetDyed = true;

                            var History = _context.TLDYE_RFDHistory.FirstOrDefault(x => x.DyeRFD_StockOnHand_Fk == Pk);
                            if (History == null)
                            {
                                History = new TLDYE_RFDHistory();
                                History.DyeRFD_CurrentStyle = StockOnHand.TLSOH_Style_FK;
                                History.DyeRFD_BeginDyeDate = dtpDateDyed.Value.Date; ;
                                History.DyeRFD_DyeToColour = ColourSelected.Col_Id;
                                History.DyeRFD_StockOnHand_Fk = StockOnHand.TLSOH_Pk;
                                History.DyeRFD_Transaction_No = LastNUsed.col14;

                                _context.TLDYE_RFDHistory.Add(History);
                            }
                        }
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show(ex.InnerException.Message);

                    }
                }
            }

            LastNUsed.col14 += 1;

            try
            {
                _context.SaveChanges();
                MessageBox.Show("Data successfully saved to database");
                this.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.InnerException.Message);
            }
        }

        private void frmDyeRFD_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(!e.Cancel)
            {
                if(_context != null)
                {
                    _context.Dispose();
                }
            }
        }

        private void cmboStyles_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox oCmbo = sender as ComboBox;
            if(oCmbo != null && FormLoaded)
            {
                var StySelected = (TLADM_Styles)oCmbo.SelectedItem;

                if(StySelected != null)
                {
                    var StockAvail = _context.TLCSV_StockOnHand.Where(x => x.TLSOH_Style_FK == StySelected.Sty_Id && !x.TLSOH_RFD_NotYetDyed).OrderBy(x => x.TLSOH_BoxNumber).ToList();
               
                    DataT.Rows.Clear();

                    foreach (var SAvail in StockAvail)
                    {
                        DataRow Row = DataT.NewRow();
                        Row[0] = SAvail.TLSOH_Pk;
                        Row[1] = false;
                        Row[2] = SAvail.TLSOH_BoxNumber;
                        Row[3] = SAvail.TLSOH_BoxedQty;

                        DataT.Rows.Add(Row);
                    }
                }

            }
        }
    }
}
