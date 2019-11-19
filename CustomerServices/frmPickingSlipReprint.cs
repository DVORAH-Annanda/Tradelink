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
    public partial class frmPickingSlipReprint : Form
    {
        DataGridViewTextBoxColumn oTxtA;    // 0 Pk
        DataGridViewCheckBoxColumn oChkA;   // 1 Select
        DataGridViewTextBoxColumn oTxtB;    // 2 PickSlip Number
        DataGridViewTextBoxColumn oTxtC;    // 3 Customer
        DataGridViewTextBoxColumn oTxtD;    // 4 WareHouse 
        DataGridViewTextBoxColumn oTxtE;    // 5 Created Date

        public frmPickingSlipReprint()
        {
            InitializeComponent();
        }

        private void frmPickingSlipReprint_Load(object sender, EventArgs e)
        {
            oTxtA = new DataGridViewTextBoxColumn();
            oTxtA.Visible = false;
            oTxtA.ValueType = typeof(int);
            dataGridView1.Columns.Add(oTxtA);

            oChkA = new DataGridViewCheckBoxColumn();
            oChkA.HeaderText = "Selected";
            oChkA.ValueType = typeof(bool);
            dataGridView1.Columns.Add(oChkA);

            oTxtB = new DataGridViewTextBoxColumn();
            oTxtB.ReadOnly = true;
            oTxtB.HeaderText = "Pick Slip";
            oTxtB.ValueType = typeof(string);
            dataGridView1.Columns.Add(oTxtB);

            oTxtC = new DataGridViewTextBoxColumn();
            oTxtC.ReadOnly = true;
            oTxtC.HeaderText = "Customer";
            oTxtC.ValueType = typeof(string);
            dataGridView1.Columns.Add(oTxtC);

            oTxtD = new DataGridViewTextBoxColumn();
            oTxtD.ReadOnly = true;
            oTxtD.HeaderText = "Warehouse";
            oTxtD.ValueType = typeof(string);
            dataGridView1.Columns.Add(oTxtD);

            oTxtE = new DataGridViewTextBoxColumn();
            oTxtE.ReadOnly = true;
            oTxtE.HeaderText = "Created Date";
            oTxtE.ValueType = typeof(string);
            dataGridView1.Columns.Add(oTxtE);

            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToOrderColumns = false;

            using (var context = new TTI2Entities())
            {
                var Existing = context.TLCSV_OrderAllocated.Where(x => x.TLORDA_PickListPrint && !x.TLORDA_Delivered).ToList();
                foreach (var row in Existing)
                {
                    var index = dataGridView1.Rows.Add();
                    dataGridView1.Rows[index].Cells[0].Value = row.TLORDA_Pk;
                    dataGridView1.Rows[index].Cells[1].Value = false;
                    dataGridView1.Rows[index].Cells[2].Value = "PL" + row.TLORDA_TransNumber.ToString().PadLeft(5, '0');
                    dataGridView1.Rows[index].Cells[3].Value = context.TLADM_CustomerFile.Find(row.TLORDA_Customer_FK).Cust_Description;
                    dataGridView1.Rows[index].Cells[4].Value = " "; // context.TLADM_WhseStore.Find(row.TLORDA_WareHouse_FK).WhStore_Description;
                    dataGridView1.Rows[index].Cells[5].Value = row.TLORDA_PLPrintDate.ToString();
                }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView oDgv = sender as DataGridView;
            if (oDgv.Focused && oDgv.CurrentCell is DataGridViewCheckBoxCell)
            {
                if ((bool)oDgv.CurrentCell.EditedFormattedValue)
                {
                    var AllReadyTicked = (from Rows in dataGridView1.Rows.Cast<DataGridViewRow>()
                                          where (bool)Rows.Cells[1].Value == true
                                          select Rows).ToList();

                    var CurrentRow = oDgv.CurrentRow;

                    foreach (DataGridViewRow Row in AllReadyTicked)
                    {
                        if (Row.Index == CurrentRow.Index)
                            continue;

                        dataGridView1.Rows[Row.Index].Cells[1].Value = false;
                    }
                }
            }
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;
            if (oBtn != null)
            {
                CSVServices svces = new CSVServices();
                svces.PLReprint = true;

                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if ((bool)row.Cells[1].Value == true)
                    {
                        var Pk = Convert.ToInt32(row.Cells[2].Value.ToString().Remove(0,2));
                        frmCSViewRep vRep = new frmCSViewRep(4, svces, Pk);
                        int h = Screen.PrimaryScreen.WorkingArea.Height;
                        int w = Screen.PrimaryScreen.WorkingArea.Width;
                        vRep.ClientSize = new Size(w, h);
                        vRep.ShowDialog(this);
                    }
                }
            }
        }
    }
}
