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
using TTI2_WF;

namespace Administration
{
    public partial class frmCottonHauliersVehicles : Form
    {
        int Haulier_FK;
        DataGridViewTextBoxColumn oTxtBoxA;
        DataGridViewTextBoxColumn oTxtBoxB;
        DataGridViewTextBoxColumn oTxtBoxC;

        public frmCottonHauliersVehicles(int FK, string Description)
        {
            InitializeComponent();
          
            Haulier_FK = FK;
         
            oTxtBoxA = new DataGridViewTextBoxColumn();
            oTxtBoxA.Visible = false;
            
            oTxtBoxB = new DataGridViewTextBoxColumn();
            oTxtBoxB.HeaderText = "Vehicle Registration No";
            oTxtBoxB.Width = 150;
           
            oTxtBoxC = new DataGridViewTextBoxColumn();
            oTxtBoxC.HeaderText = "Vehicle Description";
            oTxtBoxC.Width = 215;

            dataGridView1.Columns.Add(oTxtBoxA);
            dataGridView1.Columns.Add(oTxtBoxB);
            dataGridView1.Columns.Add(oTxtBoxC);

            Setup(Description);

        }

        void Setup(string desc)
        {
           
            label1.Text = desc;

            using (var context = new TTI2Entities())
            {
                var Existing = context.TLADM_CottonHauliersVehicles.Where(x => x.HaulVeh_Haulier_FK == Haulier_FK).ToList();
                foreach (var record in Existing)
                {
                    var index = dataGridView1.Rows.Add();
                    dataGridView1.Rows[index].Cells[0].Value = record.HaulVeh_Pk;
                    dataGridView1.Rows[index].Cells[1].Value = record.HaulVeh_RegNo;
                    dataGridView1.Rows[index].Cells[2].Value = record.HaulVeh_Description;
                }
            }
           
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;
            bool success = true;
            bool addRec;

            if (oBtn != null)
            {
                DataGridView oDgv = dataGridView1;
                foreach (DataGridViewRow rw in oDgv.Rows)
                {
                    if (rw.Cells[1].Value == null)
                        continue;

                    addRec = true;

                    TLADM_CottonHauliersVehicles hv = new TLADM_CottonHauliersVehicles();

                   
                    using (var context = new TTI2Entities())
                    {
                        if(rw.Cells[0].Value != null)
                        {
                            int Val = Convert.ToInt32(rw.Cells[0].Value.ToString());
                            hv = context.TLADM_CottonHauliersVehicles.Find(Val);
                            if(hv != null)
                                 addRec = false;
                        }

                        hv.HaulVeh_RegNo = rw.Cells[1].Value.ToString();
                        hv.HaulVeh_Description = rw.Cells[2].Value.ToString();
                        hv.HaulVeh_Haulier_FK = Haulier_FK;

                        if(addRec)
                            context.TLADM_CottonHauliersVehicles.Add(hv);

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
                }
                if (success)
                {
                    MessageBox.Show("Records store successfully to database");
                    this.Close();
                }

            }
        }
    }
}
