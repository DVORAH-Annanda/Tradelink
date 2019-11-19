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
    public partial class frmTLADM_UOM : Form
    {
        DataGridViewTextBoxColumn selecta;   // index of the main record 
        DataGridViewTextBoxColumn selectb;   // UOM short code 
        DataGridViewTextBoxColumn selectc;   // UOM description
        public frmTLADM_UOM()
        {
            InitializeComponent();
            SetUp();
        }

        void SetUp()
        {
            this.Width = 600;

            selecta = new DataGridViewTextBoxColumn();
            selecta.ValueType = typeof(System.Int32);
            selecta.Visible = false;

            selectb = new DataGridViewTextBoxColumn();
            selectb.HeaderText = " UOM Short Code";

            selectc = new DataGridViewTextBoxColumn();
            selectc.HeaderText = "UOM Description";
            selectc.Width = 175;

            dataGridView1.Columns.Add(selecta);
            dataGridView1.Columns.Add(selectb);
            dataGridView1.Columns.Add(selectc);

            dataGridView1.Width = 325;
        
            using (var context = new TTI2Entities())
            {
                var ExistingData = context.TLADM_UOM.OrderBy(x => x.UOM_Description).ToList();

                foreach (var row in ExistingData)
                {
                    var index = dataGridView1.Rows.Add();
                    dataGridView1.Rows[index].Cells[0].Value = row.UOM_Pk;
                    dataGridView1.Rows[index].Cells[1].Value = row.UOM_ShortCode;
                    dataGridView1.Rows[index].Cells[2].Value = row.UOM_Description;
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;
            bool lAdd;
            bool lSuccess = true;

            if (oBtn != null)
            {
               
                using ( var context = new TTI2Entities())
                {
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        if (row.Cells[1].Value == null)
                            continue;

                        lAdd = false;

                       TLADM_UOM   uom = new TLADM_UOM();

                        if (row.Cells[0].Value == null)
                        {
                            lAdd = true;
                        }
                        else
                        {
                            int pk = Convert.ToInt32(row.Cells[0].Value.ToString());
                            uom = context.TLADM_UOM.Find(pk);
                        }

                        uom.UOM_ShortCode = row.Cells[1].Value.ToString();
                        uom.UOM_Description = row.Cells[2].Value.ToString();

                        if (lAdd)
                            context.TLADM_UOM.Add(uom);

                        try
                        {
                            context.SaveChanges();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                            lSuccess = false;
                        }


                    }
                }
                if (lSuccess)
                {
                    dataGridView1.Rows.Clear();
                    MessageBox.Show("Record's successfully stored");
                }
            
            }

        }
    }
}
