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
using System.IO;

namespace Spinning
{
    public partial class frmCottonBales : Form
    {
        TLSPN_CottonTransactions _CTrns;

        DataGridViewTextBoxColumn oTxtBoxA;
        DataGridViewTextBoxColumn oTxtBoxB;
        DataGridViewTextBoxColumn oTxtBoxC;
        DataGridViewTextBoxColumn oTxtBoxD;
        DataGridViewTextBoxColumn oTxtBoxE;
        DataGridViewTextBoxColumn oTxtBoxF;
        int LotN;
        int SeqN;
        bool formLoaded;
        Util core;
        //-------------------------------------------------------------------------------------------------
        public frmCottonBales(TLSPN_CottonTransactions cts)
        {
            InitializeComponent();
            _CTrns = cts;
            SetUp();
        }
        //-------------------------------------------------------------------------------------------------------
        void SetUp()
        {
            formLoaded = true;

            core = new Util();

            oTxtBoxA = new DataGridViewTextBoxColumn();
            oTxtBoxA.Visible = false;

            oTxtBoxB = new DataGridViewTextBoxColumn();
            oTxtBoxB.HeaderText = "Bale No";
            oTxtBoxB.ValueType = typeof(int);
            oTxtBoxB.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            oTxtBoxC = new DataGridViewTextBoxColumn();
            oTxtBoxC.HeaderText = "Mic";
            oTxtBoxC.ValueType = typeof(Decimal);
            oTxtBoxC.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            oTxtBoxD = new DataGridViewTextBoxColumn();
            oTxtBoxD.HeaderText = "Kgs (NETT)";
            oTxtBoxD.ValueType = typeof(Decimal);
            oTxtBoxD.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            oTxtBoxE = new DataGridViewTextBoxColumn();
            oTxtBoxE.HeaderText = "Staple";
            oTxtBoxE.ValueType = typeof(Decimal);
            oTxtBoxE.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            oTxtBoxF = new DataGridViewTextBoxColumn();
            oTxtBoxF.HeaderText = "Kgs (GROSS)";
            oTxtBoxF.ValueType = typeof(Decimal);
            oTxtBoxF.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dataGridView1.Columns.Add(oTxtBoxA);    // 0 
            dataGridView1.Columns.Add(oTxtBoxB);    // 1 Bales Numeric 
            dataGridView1.Columns.Add(oTxtBoxC);    // 2 MIC Decimal
            dataGridView1.Columns.Add(oTxtBoxD);    // 3 kgs (NETT) Decimal 
            dataGridView1.Columns.Add(oTxtBoxE);    // 4 Staple Decimal
            dataGridView1.Columns.Add(oTxtBoxF);    // 5 Kgs (GROSS) Decimal

            dataGridView1.EditingControlShowing += new DataGridViewEditingControlShowingEventHandler(dataGridView1_EditingControlShowing);
            dataGridView1.RowsAdded += new DataGridViewRowsAddedEventHandler(dataGridView1_RowsAdded);

        }
        
        private void dataGridView1_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            /*
            try
            {
                dataGridView1.Rows[dataGridView1.NewRowIndex].Cells[1].Value = 0;     //Bale No 
                dataGridView1.Rows[dataGridView1.NewRowIndex].Cells[2].Value = 0.00;  // MIC Decimal
                dataGridView1.Rows[dataGridView1.NewRowIndex].Cells[3].Value = _CTrns.cottrx_NettAveBaleWeight;  // Kgs (NETT)decimal 
                dataGridView1.Rows[dataGridView1.NewRowIndex].Cells[4].Value = 0.00;  // staple decimal 
                dataGridView1.Rows[dataGridView1.NewRowIndex].Cells[5].Value = _CTrns.cotrx_GrossAveBaleWeight;  // kgs (GROSS) decimal
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
             * */

        }
        void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            DataGridView oDgv = sender as DataGridView;

            if (formLoaded)
            {
                if (oDgv.Focused && oDgv.CurrentCell is DataGridViewTextBoxCell)
                {
                    var Cell = oDgv.CurrentCell;
                    if (Cell.ColumnIndex == 1)
                    {
                        e.Control.KeyDown -= new KeyEventHandler(core.txtWin_KeyDown);
                        e.Control.KeyDown += new KeyEventHandler(core.txtWin_KeyDown);
                        e.Control.KeyPress -= new KeyPressEventHandler(core.txtWin_KeyPress);
                        e.Control.KeyPress += new KeyPressEventHandler(core.txtWin_KeyPress);
                    }
                    else if (Cell.ColumnIndex > 1)
                    {
                        e.Control.KeyDown -= new KeyEventHandler(core.txtWin_KeyDownOEM);
                        e.Control.KeyDown += new KeyEventHandler(core.txtWin_KeyDownOEM);
                        e.Control.KeyPress -= new KeyPressEventHandler(core.txtWin_KeyPress);
                        e.Control.KeyPress += new KeyPressEventHandler(core.txtWin_KeyPress);
                    }
                    else
                    {
                        e.Control.KeyDown -= new KeyEventHandler(core.txtWin_KeyDownOEM);
                        e.Control.KeyDown -= new KeyEventHandler(core.txtWin_KeyDown);
                        e.Control.KeyPress -= new KeyPressEventHandler(core.txtWin_KeyPress);
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
                    foreach (DataGridViewRow dr in dataGridView1.Rows)
                    {
                        if((int)dr.Cells[1].Value == 0)
                            continue;

                        TLSPN_CottonReceivedBales bales = new TLSPN_CottonReceivedBales();
                        //---------------------------------------------------------------------------
                        bales.CotBales_BaleNo = (int)dr.Cells[1].Value;
                        bales.CotBales_Mic = (decimal)dr.Cells[2].Value;
                        bales.CotBales_Weight_Nett = (decimal)dr.Cells[3].Value;
                        bales.CotBales_Staple = (decimal)dr.Cells[4].Value;
                        bales.CotBales_LotNo = _CTrns.cotrx_LotNo;
                        bales.CotBales_Weight_Gross = (decimal)dr.Cells[5].Value;
                        bales.CoBales_CottonSequence = _CTrns.cotrx_Return_No;
                        //------------------------------------------------------------------------------
                        context.TLSPN_CottonReceivedBales.Add(bales);

                       
                    }

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

                if (success)
                {
                    MessageBox.Show("Rocords stored to database successfully");
                    this.Close();
                }
            }
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;
            int RowCount = 0;
            if (oBtn != null)
            {
                // Create an instance of the open file dialog box.
                OpenFileDialog openFileDialog1 = new OpenFileDialog();

                // Set filter options and filter index.
                openFileDialog1.Filter = "Text Files (.txt)|*.txt|All Files (*.*)|*.*";
                openFileDialog1.FilterIndex = 1;

                openFileDialog1.Multiselect = false;

                // Call the ShowDialog method to show the dialog box.
                DialogResult res = openFileDialog1.ShowDialog(this);

                // Process input if the user clicked OK.
                if (res == DialogResult.OK)
                {
                    // Open the selected file to read.
                    try
                    {
                        System.IO.Stream fileStream = openFileDialog1.OpenFile();
                        using (System.IO.StreamReader reader = new System.IO.StreamReader(fileStream))
                        {
                            using (var context = new TTI2Entities())
                            {
                                // Read the first line from the file and write it the textbox.
                                string[] input = reader.ReadToEnd().Split('\r');
                                foreach (var line in input)
                                {
                                    string[] row = line.Split(',');

                                    if (row[0].Contains("Bale"))
                                        continue;

                                

                                    TLSPN_CottonReceivedBales BalesRec = new TLSPN_CottonReceivedBales();
                                    //-------------------------------------------------------------------------------------                 

                                    int Bale = Convert.ToInt32(row[0]);
                                    decimal Micro = Math.Round(Convert.ToDecimal(row[1]), 1);
                                    decimal Staple = Math.Round(Convert.ToDecimal(row[3]), 1);
                                    //-----------------------------------------------------------------------------
                                    BalesRec.CotBales_BaleNo = Bale;
                                    // *20250926
                                    //--CotBales_CotReceived_FK = 0-- >> CotBales_Sample_BaleNo
                                    //BalesRec.CotBales_CotReceived_FK = _CTrns.cotrx_pk;
                                    BalesRec.CotBales_Sample_BaleNo = "Test";
                                    BalesRec.CotBales_LotNo = _CTrns.cotrx_LotNo;
                                    BalesRec.CoBales_CottonSequence = _CTrns.cotrx_Return_No;
                                    BalesRec.CotBales_Mic = Micro;
                                    BalesRec.CotBales_Staple = Staple;
                                    BalesRec.CotBales_Weight_Nett = _CTrns.cottrx_NettAveBaleWeight;
                                    BalesRec.CotBales_Weight_Gross = _CTrns.cotrx_GrossAveBaleWeight;

                                    var index = dataGridView1.Rows.Add();
                                    dataGridView1.Rows[index].Cells[1].Value = Bale;     //Bale No 
                                    dataGridView1.Rows[index].Cells[2].Value = Micro;  // MIC Decimal
                                    dataGridView1.Rows[index].Cells[3].Value = _CTrns.cottrx_NettAveBaleWeight;  // Kgs (NETT)decimal 
                                    dataGridView1.Rows[index].Cells[4].Value = Staple;  // staple decimal 
                                    dataGridView1.Rows[index].Cells[5].Value = _CTrns.cotrx_GrossAveBaleWeight;  // kgs (GROSS) decimal

                                    RowCount += 1;
                                }

                                dataGridView1.Refresh();

                            }

                        }
                        fileStream.Close();

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        return;
                    }

                    if (RowCount != _CTrns.cotrx_NoBales)
                    {
                        MessageBox.Show("The number of records read in does not correspond to the number entered" + Environment.NewLine + "Please investigate" + Environment.NewLine + "Number of Rows read in " + RowCount.ToString());
                        btnSave.Enabled = false;
                    }
                }
            }
        }

       
    }
}
