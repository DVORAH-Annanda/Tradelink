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

namespace TTI2_WF
{
    public partial class frmTLADMGardAdditional : Form
    {
        DataGridViewComboBoxColumn    pk; //0
        DataGridViewComboBoxColumn    size; //1
        DataGridViewComboBoxColumn    color; //2
        DataGridViewTextBoxColumn     stdCost; //3
        DataGridViewTextBoxColumn     stdCostRefund; //4
        Util core;
        int SFK;
        bool formloaded;

        public frmTLADMGardAdditional( int StyleFK, int SizePN)
        {
            InitializeComponent();
            SetUp(StyleFK, SizePN);
            SFK = StyleFK;
        }

        void SetUp(int sfk, int sizepn)
        {
            formloaded = false;
            core = new Util();
            IList<int> powerN;
            IList<TLADM_Sizes> sizeList = new List<TLADM_Sizes>();
            DataGridView oDgv;
            dataGridView12.AutoGenerateColumns = false;

            dataGridView12.EditingControlShowing += new DataGridViewEditingControlShowingEventHandler(dataGridView1_EditingControlShowing);
            dataGridView12.RowsAdded += new DataGridViewRowsAddedEventHandler(dataGridView1_RowsAdded);

            pk = new DataGridViewComboBoxColumn();
            pk.Visible = false;

            size = new DataGridViewComboBoxColumn();
            size.HeaderText = "Please select a size";
            
            color = new DataGridViewComboBoxColumn();
            color.HeaderText = "Please select a colour";
            
            stdCost = new DataGridViewTextBoxColumn();
            stdCost.HeaderText = "Please enter a standard cost";
            stdCost.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
           
            stdCostRefund = new DataGridViewTextBoxColumn();
            stdCostRefund.HeaderText = "Please enter a refund cost";
            stdCostRefund.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            
            dataGridView12.Columns.Add(pk);
            dataGridView12.Columns.Add(size);
            dataGridView12.Columns.Add(color);
            dataGridView12.Columns.Add(stdCost);
            dataGridView12.Columns.Add(stdCostRefund);

            //--------------------------------------------------------------------------
            // 
            //------------------------------------------------------------------------------
            using (var context = new TTI2Entities())
            {
                
                powerN = core.ExtrapNumber(sizepn, context.TLADM_Sizes.Count());
                foreach (var nbr in powerN)
                {
                    var tst = context.TLADM_Sizes.Where(x => x.SI_PowerN == nbr).FirstOrDefault();
                    sizeList.Add(tst); 
                }

                size.DataSource = sizeList;  // context.TLADM_Sizes.OrderBy(x=>x.SI_Description).ToList();
                size.DisplayMember = "SI_Description";
                size.ValueMember = "SI_id";

                color.DataSource = context.TLADM_Colours.OrderBy(x=>x.Col_Description).ToList();;
                color.DisplayMember = "Col_Description";
                color.ValueMember = "Col_Id";
          

                oDgv = dataGridView12;

                var ExistingData = context.TLADM_StylesAdditional
                                   .Where(x => x.AddSty_Style_FK == sfk).ToList();

                if (ExistingData != null)
                {
                    foreach (var ExistingRow in ExistingData)
                    {
                        var index = oDgv.Rows.Add();
                        oDgv.Rows[index].Cells[0].Value = ExistingRow.AddSty_Pk;
                        oDgv.Rows[index].Cells[1].Value = ExistingRow.AddSty_Size_FK;
                        oDgv.Rows[index].Cells[2].Value = ExistingRow.AddSty_Colour_FK;
                        oDgv.Rows[index].Cells[3].Value = Math.Round(ExistingRow.AddSty_StandardCost,2).ToString();
                        oDgv.Rows[index].Cells[4].Value = Math.Round(ExistingRow.AddSty_RefundCost,2).ToString();
                    }
                }

                formloaded = true;
            }
       
        }
        private void dataGridView1_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            var oDgv = sender as DataGridView;
            try
            {
                if (oDgv != null && formloaded)
                {
                    try
                    {
                        // dataGridView12.Rows[e.RowIndex].Cells[3].Value = 0;
                        // dataGridView12.Rows[e.RowIndex].Cells[4].Value = 0;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            DataGridView oDgv = sender as DataGridView;
            if (oDgv.Focused && oDgv.CurrentCell is DataGridViewTextBoxCell)
             {
                var currentCell = oDgv.CurrentCell;
                if (currentCell.ColumnIndex == 2 || currentCell.ColumnIndex == 3)
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

        private void btnSave_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;
            bool lAdd;
            bool NoProb = true;

            if (oBtn != null)
            {
                DataGridView oDgv = dataGridView12;
                using (var Context = new TTI2Entities())
                {
                    foreach (DataGridViewRow row in dataGridView12.Rows)
                    {
                        lAdd = false;
                        
                        if (row.Cells[3].Value == null)
                            continue;

                        TLADM_StylesAdditional clrs = null;

                        if (row.Cells[0].Value != null)
                        {
                            int index = Convert.ToInt32(row.Cells[0].Value.ToString());
                            clrs = Context.TLADM_StylesAdditional.Find(index);

                        }
                        else
                        {
                            clrs = new TLADM_StylesAdditional();
                            lAdd = true;
                        }


                        clrs.AddSty_Style_FK = SFK;
                        clrs.AddSty_Size_FK      = Convert.ToInt32(row.Cells[1].Value.ToString());
                        clrs.AddSty_Colour_FK    = Convert.ToInt32(row.Cells[2].Value.ToString());
                        clrs.AddSty_StandardCost = Convert.ToDecimal(row.Cells[3].Value.ToString());
                        clrs.AddSty_RefundCost   = Convert.ToDecimal(row.Cells[4].Value.ToString());
                        
                        if (lAdd)
                        {
                            Context.TLADM_StylesAdditional.Add(clrs);
                        }

                        try
                        {
                            Context.SaveChanges();
                          
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                            NoProb = false;
                        }
                    }
                }
                if (NoProb)
                {
                    MessageBox.Show("Records Stored to Database correctly");
                    dataGridView12.Rows.Clear();
                    this.Close();
                }
            }
        }
       
    }
}
