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
    public partial class frmTLADMDeptdef : Form
    {
        DataGridViewTextBoxColumn selecta;    // index of the main record 
        DataGridViewTextBoxColumn selectb;    // Department short code 
        DataGridViewTextBoxColumn selectc;    // Department description
        DataGridViewComboBoxColumn selectd;   // UOM 
        DataGridViewComboBoxColumn selecte;   // Product type 
        DataGridViewTextBoxColumn selectf;    // Department power no
        DataGridViewCheckBoxColumn oChkA;     // Is Cmt Yes or No;
        DataGridViewCheckBoxColumn oChkB;     // Is Cutting Department Yes or No;
        DataGridViewCheckBoxColumn oChkC;     // Is Department a quarantine store Yes or No;
        public frmTLADMDeptdef()
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
            selectb.HeaderText = " Dept Short Code";

            selectc = new DataGridViewTextBoxColumn();
            selectc.HeaderText = "Dept Description";
            selectc.Width = 175;

            selectd = new DataGridViewComboBoxColumn();
            selecte = new DataGridViewComboBoxColumn();

            selectf = new DataGridViewTextBoxColumn();
            selectf.HeaderText = "power Number";
            selectf.Visible = false;

            oChkA = new DataGridViewCheckBoxColumn();
            oChkA.ValueType = typeof(bool);
            oChkA.HeaderText = "CMT";

            oChkB = new DataGridViewCheckBoxColumn();
            oChkB.ValueType = typeof(bool);
            oChkB.HeaderText = "Cut";
                        

            dataGridView1.Columns.Add(selecta);
            dataGridView1.Columns.Add(selectb);
            dataGridView1.Columns.Add(selectc);

            dataGridView1.Width = 620;

            
            using (var context = new TTI2Entities())
            {
                selectd.DataSource = context.TLADM_UOM.OrderBy(x => x.UOM_ShortCode).ToList();
                selectd.DisplayMember = "UOM_Description";
                selectd.ValueMember = "UOM_Pk";
                selectd.HeaderText = "UOM";

                selecte.DataSource = context.TLADM_ProductTypes.OrderBy(x => x.PT_ShortCode).ToList();
                selecte.DisplayMember = "PT_Description";
                selecte.ValueMember = "PT_Pk";
                selecte.HeaderText = "Product Type";

                dataGridView1.Columns.Add(selectd);
                dataGridView1.Columns.Add(selecte);
                dataGridView1.Columns.Add(selectf);
                dataGridView1.Columns.Add(oChkA);
                dataGridView1.Columns.Add(oChkB);
           
                var ExistingData = context.TLADM_Departments.OrderBy(x => x.Dep_Description).ToList();

                foreach (var row in ExistingData)
                {
                    var index = dataGridView1.Rows.Add();
                    dataGridView1.Rows[index].Cells[0].Value = row.Dep_Id;
                    dataGridView1.Rows[index].Cells[1].Value = row.Dep_ShortCode;
                    dataGridView1.Rows[index].Cells[2].Value = row.Dep_Description;
                    dataGridView1.Rows[index].Cells[3].Value = row.Dep_UOM;
                    dataGridView1.Rows[index].Cells[4].Value = row.Dep_ProductType_FK;
                    dataGridView1.Rows[index].Cells[5].Value = row.Dep_PowerN;
                    if (row.Dep_IsCMT)
                        dataGridView1.Rows[index].Cells[6].Value = true;
                    else
                        dataGridView1.Rows[index].Cells[6].Value = false;

                    if (row.Dep_IsCut)
                        dataGridView1.Rows[index].Cells[7].Value = true;
                    else
                        dataGridView1.Rows[index].Cells[7].Value = false;

                    
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Button oBtn = sender as Button;
            bool lAdd = false;
            bool lCMT = false;
            bool lQuarantine = false;
            bool lSuccess = true;
          
            if (oBtn != null)
            {
                using ( var context = new TTI2Entities())
                {
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        if (row.Cells[1].Value == null)
                            continue;

                        TLADM_Departments dpt = new TLADM_Departments();

                        if (row.Cells[0].Value == null)
                        {
                            lAdd = true;
                        }
                        else
                        {
                            int pk = Convert.ToInt32(row.Cells[0].Value.ToString());
                            dpt = context.TLADM_Departments.Find(pk);
                        }

                        dpt.Dep_ShortCode   = row.Cells[1].Value.ToString();
                        dpt.Dep_Description = row.Cells[2].Value.ToString();
                        dpt.Dep_UOM         = Convert.ToInt32(row.Cells[3].Value.ToString());
                        dpt.Dep_ProductType_FK = Convert.ToInt32(row.Cells[4].Value.ToString());
                        
                        if (row.Cells[5].Value == null && lAdd)
                        {
                            dpt.Dep_PowerN = (int)Math.Pow(2.00D, (double)row.Index);
                        }
                        else
                            dpt.Dep_PowerN = Convert.ToInt32(row.Cells[5].Value.ToString());

                        if (row.Cells[6].Value != null)
                        {
                            if ((row.Cells[6].Value.ToString() == Boolean.TrueString))
                                dpt.Dep_IsCMT = true;
                        }
                        else
                            dpt.Dep_IsCMT = false;

                        if (row.Cells[7].Value != null)
                        {
                            if ((row.Cells[7].Value.ToString() == Boolean.TrueString))
                                dpt.Dep_IsCut = true;
                        }
                        else
                            dpt.Dep_IsCut = false;

                        
                        if (lAdd)
                            context.TLADM_Departments.Add(dpt);

                        lCMT = dpt.Dep_IsCMT;

                        try
                        {
                            context.SaveChanges();
                            // We need to add a record to the TLADM_LastNumberUsed  table 

                            if (lAdd)
                            {
                                TLADM_LastNumberUsed LstNumberUsed = new TLADM_LastNumberUsed();
                                LstNumberUsed.LUN_Department_FK = dpt.Dep_Id;
                                LstNumberUsed.col1 = 1;
                                LstNumberUsed.col2 = 1;
                                LstNumberUsed.col3 = 1;
                                LstNumberUsed.col4 = 1;
                                LstNumberUsed.col5 = 1;
                                LstNumberUsed.col6 = 1;
                                LstNumberUsed.col7 = 1;
                                LstNumberUsed.col8 = 1;
                                LstNumberUsed.col9 = 1;
                                LstNumberUsed.col10 = 1;
                                LstNumberUsed.col11 = 1;
                                LstNumberUsed.col12 = 1;
                                LstNumberUsed.col13 = 1;

                                var LstNumber = context.TLADM_LastNumberUsed.Where(x => x.LUN_Department_FK == dpt.Dep_Id).FirstOrDefault();
                                if (LstNumber == null)
                                {
                                    context.TLADM_LastNumberUsed.Add(LstNumberUsed);

                                    context.SaveChanges();
                                }
                            }
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

                    string Mess = string.Empty;

                    if (lCMT)
                    {
                        Mess = "A new CMT has just been added" + Environment.NewLine;
                        Mess += "Please proceed to tranaction types to add a tranaction 100 to the relevant CMT's" + Environment.NewLine;
                        Mess += "If unsure please contact system developer for assistance" + Environment.NewLine;

                    }

                    Mess += "Records successfully stored to database";

                    MessageBox.Show(Mess);
                }
            }
        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            var loc = e.Location;
            DataGridView oDgv = sender as DataGridView;
            if (oDgv != null && e.Button.ToString() == "Right")
            {
                if (oDgv.SelectedRows.Count > 0)
                {
                    DialogResult res = MessageBox.Show("Please confirm this transaction", "Confirmation Required", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                    using (var context = new TTI2Entities())
                    {
                        if (res == DialogResult.OK)
                        {
                            var Pk = (int)oDgv.CurrentRow.Cells[0].Value;
                            TLADM_Departments shifts = context.TLADM_Departments.Find(Pk);
                            if (shifts != null)
                            {
                                context.TLADM_Departments.Remove(shifts);

                                try
                                {
                                    context.SaveChanges();
                                    oDgv.Rows.Clear();
                                    MessageBox.Show("Data successfully saved to database");

                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message);
                                    return;
                                }
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Please select a row in the datagrid", "Information");
                }
            }
        }
    }
}
