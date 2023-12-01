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
    public partial class frmRFDCompleted : Form
    {
        bool FormLoaded;
        protected readonly TTI2Entities _context;
        DataTable DataT;
        BindingSource BindS;

        public frmRFDCompleted()
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
            Column.ColumnName = "RFD_TransNo";
            Column.Caption = "TransNumber";
            Column.DefaultValue = string.Empty;
            DataT.Columns.Add(Column);

            //==========================================================================================
            // Col 3
            //=====================================================================
            Column = new DataColumn();
            Column.DataType = typeof(string);
            Column.ColumnName = "New_Colour";
            Column.Caption = "New Colour";
            Column.DefaultValue = string.Empty;
            DataT.Columns.Add(Column);

            //==========================================================================================
            // Col 4
            //=====================================================================
            Column = new DataColumn();
            Column.DataType = typeof(int);
            Column.ColumnName = "No_OfGradeA";
            Column.Caption = "No of Grade A";
            Column.DefaultValue = 0;
            DataT.Columns.Add(Column);

            //==========================================================================================
            // Col 5
            //=====================================================================
            Column = new DataColumn();
            Column.DataType = typeof(int);
            Column.ColumnName = "No_OfGradeB";
            Column.Caption = "No of Grade B";
            Column.DefaultValue = 0;
            DataT.Columns.Add(Column);

            //==========================================================================================
            // Col 6
            //=====================================================================
            Column = new DataColumn();
            Column.DataType = typeof(int);
            Column.ColumnName = "No_OfGradeC";
            Column.Caption = "No of Other Grades";
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
            selectb.Name = "TransNumber";
            selectb.ValueType = typeof(string);
            selectb.DataPropertyName = DataT.Columns[2].ColumnName;
            selectb.HeaderText = "Trans Number";
            selectb.ReadOnly = true;
            selectb.Visible = true;
            dataGridView1.Columns.Add(selectb);
            dataGridView1.Columns[2].DisplayIndex = 2;

            DataGridViewTextBoxColumn selectd = new DataGridViewTextBoxColumn();
            selectd.Name = "ToColour";
            selectd.ValueType = typeof(string);
            selectd.DataPropertyName = DataT.Columns[3].ColumnName;
            selectd.HeaderText = "To Colour";
            selectd.ReadOnly = true;
            selectd.Visible = true;
            dataGridView1.Columns.Add(selectd);
            dataGridView1.Columns[3].DisplayIndex = 3;

            DataGridViewTextBoxColumn selecte = new DataGridViewTextBoxColumn();
            selecte.Name = "GradeA";
            selecte.ValueType = typeof(int);
            selecte.DataPropertyName = DataT.Columns[4].ColumnName;
            selecte.HeaderText = "Grade A";
            selecte.Visible = true;
            dataGridView1.Columns.Add(selecte);
            dataGridView1.Columns[4].DisplayIndex = 4;

            DataGridViewTextBoxColumn selectf = new DataGridViewTextBoxColumn();
            selectf.Name = "GradeA";
            selectf.ValueType = typeof(int);
            selectf.DataPropertyName = DataT.Columns[5].ColumnName;
            selectf.HeaderText = "Grade B";
            selectf.Visible = true;
            dataGridView1.Columns.Add(selectf);
            dataGridView1.Columns[5].DisplayIndex = 5;

            DataGridViewTextBoxColumn selectg = new DataGridViewTextBoxColumn();
            selectg.Name = "GradeOther";
            selectg.ValueType = typeof(int);
            selectg.DataPropertyName = DataT.Columns[6].ColumnName;
            selectg.HeaderText = "Grade Other";
            selectg.Visible = true;
            dataGridView1.Columns.Add(selectg);
            dataGridView1.Columns[6].DisplayIndex = 6;

            BindS.DataSource = DataT;
            dataGridView1.DataSource = BindS;
        }

        private void frmRFDCompleted_Load(object sender, EventArgs e)
        {
            FormLoaded = false;

            DataT.Rows.Clear();

            var CurrentEntries = _context.TLDYE_RFDHistory.Where(x => !x.DyeRFD_Completed).GroupBy(x => x.DyeRFD_Transaction_No).ToList(); 
            foreach(var CurrentEntry in CurrentEntries)
            {
                DataRow NewRow = DataT.NewRow();
                NewRow[0] = CurrentEntry.FirstOrDefault().DyeRFD_Transaction_No;
                NewRow[1] = false;
                NewRow[2] = CurrentEntry.FirstOrDefault().DyeRFD_Transaction_No.ToString().PadLeft(5, '0');
                var ToColour = CurrentEntry.FirstOrDefault().DyeRFD_DyeToColour;
                NewRow[3] = _context.TLADM_Colours.Find(ToColour).Col_Display;
                NewRow[4] = CurrentEntry.FirstOrDefault().DyeRFD_NoumberOfAGrades; ;
                NewRow[5] = CurrentEntry.FirstOrDefault().DyeRFD_NumberOfBGrades;
                NewRow[6] = CurrentEntry.FirstOrDefault().DyeRFD_NumberOfOtherGrades;

                DataT.Rows.Add(NewRow);
            }

            FormLoaded = true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;

            if(oBtn != null && FormLoaded)
            {
                foreach(DataRow Row in DataT.Rows)
                {
                    if(!Row.Field<bool>(1))
                    {
                        continue;
                    }

                    var TrnsNum = Row.Field<int>(0);

                    var Entries = _context.TLDYE_RFDHistory.Where(x => x.DyeRFD_Transaction_No == TrnsNum && !x.DyeRFD_Completed).ToList();
                    foreach(var Entry in Entries)
                    {
                        Entry.DyeRFD_Completed = true;
                        Entry.DyeRFD_NoumberOfAGrades = Row.Field<int>(4);
                        Entry.DyeRFD_NumberOfBGrades = Row.Field<int>(5);
                        Entry.DyeRFD_NumberOfOtherGrades = Row.Field<int>(6);

                        var StockOnHand = _context.TLCSV_StockOnHand.Find(Entry.DyeRFD_StockOnHand_Fk);
                        if(StockOnHand != null)
                        {
                            StockOnHand.TLSOH_Style_FK = (int)(from T1 in _context.TLADM_Styles
                                      join T2 in _context.TLADM_StyAssoc
                                      on T1.Sty_Id equals T2.StyAssoc_StyPk
                                      select T2).FirstOrDefault().StyAssoc_StyOther;

                            StockOnHand.TLSOH_Colour_FK = Entry.DyeRFD_DyeToColour;
                            if (Entry.DyeRFD_NoumberOfAGrades != 0)
                            {
                                StockOnHand.TLSOH_BoxedQty = Entry.DyeRFD_NoumberOfAGrades;
                            }
                        }
                    }
                }

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
        }

        private void frmRFDCompleted_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(!e.Cancel)
            {
                if(_context != null)
                {
                    _context.Dispose();
                }
            }
        }
    }
}
