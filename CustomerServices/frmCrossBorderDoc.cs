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
    public partial class frmCrossBorderDoc : Form
    {
        bool FormLoaded;

        DataGridViewTextBoxColumn oTxtA;
        DataGridViewCheckBoxColumn oChkA;
        DataGridViewTextBoxColumn oTxtB;
        DataGridViewTextBoxColumn oTxtC;
        DataGridViewTextBoxColumn oTxtD;
        // DataGridViewTextBoxColumn oTxtE;

        CustomerServices.Repository Repo;
        CustomerServices.CustomerServicesParameters QueryParms;

        public frmCrossBorderDoc()
        {
            InitializeComponent();

            oTxtA = new DataGridViewTextBoxColumn();
            oTxtA.Visible = false;
            oTxtA.ReadOnly = true;
            oTxtA.ValueType = typeof(int);
            dataGridView1.Columns.Add(oTxtA);

            oChkA = new DataGridViewCheckBoxColumn();
            oChkA.Visible = true;
            oChkA.HeaderText = "Select";
            oChkA.ValueType = typeof(bool);
            dataGridView1.Columns.Add(oChkA);

            oTxtB = new DataGridViewTextBoxColumn();
            oTxtB.ReadOnly = true;
            oTxtB.Visible = true;
            oTxtB.ValueType = typeof(string);
            oTxtB.HeaderText = "Purchase Number";
            dataGridView1.Columns.Add(oTxtB);

            oTxtC = new DataGridViewTextBoxColumn();
            oTxtC.ReadOnly = true;
            oTxtC.Visible = true;
            oTxtC.ValueType = typeof(string);
            oTxtC.HeaderText = "PL Number";
            dataGridView1.Columns.Add(oTxtC);


            oTxtD = new DataGridViewTextBoxColumn();
            oTxtD.ReadOnly = true;
            oTxtD.Visible = true;
            oTxtD.ValueType = typeof(DateTime);
            oTxtD.HeaderText = "Transaction Date";
            dataGridView1.Columns.Add(oTxtD);

           /* oTxtE = new DataGridViewTextBoxColumn();
            oTxtE.ReadOnly = true;
            oTxtE.Visible = true;
            oTxtE.ValueType = typeof(int);
            dataGridView1.Columns.Add(oTxtE);*/

            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AutoGenerateColumns = false;

            Repo = new Repository();

        }

        private void frmCrossBorderDoc_Load(object sender, EventArgs e)
        {
            FormLoaded = false;
            dataGridView1.Rows.Clear();

            using (var context = new TTI2Entities())
            {
                var OrderAllocated = context.TLCSV_OrderAllocated.Where(x => !x.TLORDA_Delivered).OrderBy(x => x.TLORDA_TransNumber).ToList();
                foreach (var Order in OrderAllocated)
                {
                    var index = dataGridView1.Rows.Add();
                    dataGridView1.Rows[index].Cells[0].Value = (int)Order.TLORDA_Pk;
                    dataGridView1.Rows[index].Cells[1].Value = false;
                    dataGridView1.Rows[index].Cells[2].Value = context.TLCSV_PurchaseOrder.Find(Order.TLORDA_POOrder_FK).TLCSVPO_PurchaseOrder;
                    dataGridView1.Rows[index].Cells[3].Value = "PL" + Order.TLORDA_TransNumber.ToString().PadLeft(5, '0');
                    dataGridView1.Rows[index].Cells[4].Value = Convert.ToDateTime(Order.TLORDA_TransDate.ToShortDateString());
                }
            }

            QueryParms = new CustomerServicesParameters();

            FormLoaded = true;


        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            Button oBtn = (Button)sender;
            if (oBtn != null && FormLoaded)
            {

                var SingleRow = (from Rows in dataGridView1.Rows.Cast<DataGridViewRow>()
                                 where (bool)Rows.Cells[1].Value == true
                                 select Rows).FirstOrDefault();

                if (SingleRow == null)
                {
                    MessageBox.Show("Please select at least one row to process");
                    return;
                }

                foreach (DataGridViewRow Row in dataGridView1.Rows)
                {
                    if (!(bool)Row.Cells[1].Value)
                        continue;

                    QueryParms.OrdersAllocated.Add(Repo.LoadOrderAllocated((int)Row.Cells[0].Value));
                }

                var services = new CSVServices();

                frmCSViewRep vRep = new frmCSViewRep(24, QueryParms, services);
                int h = Screen.PrimaryScreen.WorkingArea.Height;
                int w = Screen.PrimaryScreen.WorkingArea.Width;
                vRep.ClientSize = new Size(w, h);
                vRep.ShowDialog(this);

                QueryParms = new CustomerServicesParameters();

                foreach (DataGridViewRow Row in dataGridView1.Rows)
                {
                    if (!(bool)Row.Cells[1].Value)
                        continue;

                    Row.Cells[1].Value = false;
                }
            }
        }
    }
}
