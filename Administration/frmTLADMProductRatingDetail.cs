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
    public partial class frmTLADMProductRatingDetail : Form
    {
        DataGridViewTextBoxColumn selecta;   // index of the this record 
        DataGridViewTextBoxColumn selectb;   // foreign key 
        DataGridViewTextBoxColumn selectc;   // Sizes fk
        DataGridViewTextBoxColumn selectd;   // Sizes Details
        DataGridViewTextBoxColumn selecte;   // Marker Details

        Util core;

        public decimal ProductRatio { get; set;} 
        
        int _PowerNo;
        int MKey;

        public frmTLADMProductRatingDetail(int PowerNo, int MasterKey)
        {
            InitializeComponent();
            _PowerNo = PowerNo;
            SetUp(MasterKey);
            dataGridView1.AllowUserToAddRows = false;

        }

        void SetUp(int MK)
        {
            DataGridView oDgv;
       
            dataGridView1.AutoGenerateColumns = false;

            core = new Util();

            MKey = MK;

            selecta = new DataGridViewTextBoxColumn();
            selecta.Visible = false;
            selecta.ValueType = typeof(int);
            
            selectb = new DataGridViewTextBoxColumn();
            selectb.Visible = false;
            selectb.ValueType = typeof(int);

            selectc = new DataGridViewTextBoxColumn();
            selectc.HeaderText = "Sizes FK";
            selectc.ValueType = typeof(int);
            selectc.Visible = false;

            selectd = new DataGridViewTextBoxColumn();
            selectd.HeaderText = "Size Description";

            selecte = new DataGridViewTextBoxColumn();
            selecte.HeaderText = "Marker";
            selecte.ValueType = typeof(Decimal);
          
            dataGridView1.Columns.Add(selecta); //0 record index
            dataGridView1.Columns.Add(selectb); //1 Parent Foreign key
            dataGridView1.Columns.Add(selectc); //2 Size FK 
            dataGridView1.Columns.Add(selectd); //3 Sizes Description 
            dataGridView1.Columns.Add(selecte); //4 Marker Ratio's 

            using ( var context = new TTI2Entities())
            {
                List<int> xx = core.ExtrapNumber(_PowerNo, context.TLADM_Sizes.Count());
                xx.Sort();
                foreach (var rw in xx)
                {
                    var Sizes = context.TLADM_Sizes.FirstOrDefault(x => x.SI_PowerN == rw);
                    
                    if(Sizes != null)
                    {
                        var index = dataGridView1.Rows.Add();
                        dataGridView1.Rows[index].Cells[2].Value = Sizes.SI_id;
                        dataGridView1.Rows[index].Cells[3].Value = Sizes.SI_Description;

                        var Existing = context.TLADM_ProductRating_Detail.Where(x => x.prd_Parent_FK == MK && x.Prd_SizePN == Sizes.SI_id).FirstOrDefault();
                        if (Existing != null)
                        {
                            dataGridView1.Rows[index].Cells[0].Value = Existing.prd_Id;
                            dataGridView1.Rows[index].Cells[4].Value = Existing.Prd_MarkerRatio;
                        }
                        else
                            dataGridView1.Rows[index].Cells[4].Value = 0;

                    }
                }
            }

            dataGridView1.EditingControlShowing += new DataGridViewEditingControlShowingEventHandler(dataGridView1_EditingControlShowing);
            oDgv = new DataGridView();
            oDgv = dataGridView1;
        
        }

        private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            DataGridView oDgv = sender as DataGridView;
            ComboBox oCmb = e.Control as ComboBox;
                       
            if(oDgv.Focused && oDgv.CurrentCell is DataGridViewComboBoxCell && oCmb != null)
            {
                if (oDgv.CurrentCell.ColumnIndex == 4)
                {
                    e.Control.KeyDown -= new KeyEventHandler(core.txtWin_KeyDownOEM);
                    e.Control.KeyDown += new KeyEventHandler(core.txtWin_KeyDownOEM);
                    e.Control.KeyPress -= new KeyPressEventHandler(core.txtWin_KeyPress);
                    e.Control.KeyPress += new KeyPressEventHandler(core.txtWin_KeyPress);
                }
            }
        }
       
         private void btnSave_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;
            bool ladd = true; 
            decimal total = 0.00M;
           
            if (oBtn != null)
            {
                using (var context = new TTI2Entities())
                {
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        if (row.Cells[3].Value == null)
                        {
                            continue;
                        }

                        ladd = true;
                        TLADM_ProductRating_Detail prd = new TLADM_ProductRating_Detail();
                        
                        if (row.Cells[0].Value != null)
                        {
                            prd = context.TLADM_ProductRating_Detail.Find(Convert.ToInt32(row.Cells[0].Value.ToString()));
                            ladd = false;
                        }
                        else
                        {
                            prd.prd_Parent_FK = MKey;
                            prd.Prd_SizePN = (Convert.ToInt32(row.Cells[2].Value.ToString()));
                        }

                        prd.Prd_MarkerRatio = Convert.ToDecimal(row.Cells[4].Value.ToString());
                        total += Convert.ToDecimal(row.Cells[4].Value.ToString());

                        if (ladd)
                            context.TLADM_ProductRating_Detail.Add(prd);

                    }
                    try
                    {
                        context.SaveChanges();
                        if(total > 0 && dataGridView1.Rows.Count > 0)
                            ProductRatio = Math.Round(total,4);
                     
                     }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }

                }
               this.Close();
            }
        }

        private void dataGridView1_EditingControlShowing_1(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            DataGridView oDgv = sender as DataGridView;
            if (oDgv.Focused && oDgv.CurrentCell is DataGridViewTextBoxCell)
            {
               e.Control.KeyDown -= new KeyEventHandler(core.txtWin_KeyDownOEM);
               e.Control.KeyDown += new KeyEventHandler(core.txtWin_KeyDownOEM);
               e.Control.KeyPress -= new KeyPressEventHandler(core.txtWin_KeyPress);
               e.Control.KeyPress += new KeyPressEventHandler(core.txtWin_KeyPress);
            }
        }
    }
}
