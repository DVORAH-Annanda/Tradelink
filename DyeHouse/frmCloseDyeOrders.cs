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
    public partial class frmCloseDyeOrders : Form
    {
        DataGridViewTextBoxColumn oTxtA = new DataGridViewTextBoxColumn();
        DataGridViewCheckBoxColumn oChkA = new DataGridViewCheckBoxColumn();
        DataGridViewTextBoxColumn oTxtB = new DataGridViewTextBoxColumn();
        DataGridViewTextBoxColumn oTxtC = new DataGridViewTextBoxColumn();
        DataGridViewTextBoxColumn oTxtD = new DataGridViewTextBoxColumn();
        DataGridViewTextBoxColumn oTxtE = new DataGridViewTextBoxColumn();
        DataGridViewTextBoxColumn oTxtF = new DataGridViewTextBoxColumn();
        DataGridViewTextBoxColumn oTxtG = new DataGridViewTextBoxColumn();
        DataGridViewTextBoxColumn oTxtH = new DataGridViewTextBoxColumn();
        Util core;

        Boolean FormLoaded;

        public frmCloseDyeOrders()
        {
            InitializeComponent();

            oTxtA = new DataGridViewTextBoxColumn();
            oTxtA.Visible = false;
            oTxtA.ValueType = typeof(int);
            oTxtA.ReadOnly = true;
            dataGridView1.Columns.Add(oTxtA);

            oChkA = new DataGridViewCheckBoxColumn();
            oChkA.Visible = true;
            oChkA.ValueType = typeof(Boolean);
            oChkA.HeaderText = "Select";
            dataGridView1.Columns.Add(oChkA);

            oTxtB = new DataGridViewTextBoxColumn();
            oTxtB.Visible = true;
            oTxtB.HeaderText = "DyeOrder No";
            oTxtB.ReadOnly = true;
            oTxtB.ValueType = typeof(string);
            oTxtB.Width = 100;
            dataGridView1.Columns.Add(oTxtB);

            oTxtC = new DataGridViewTextBoxColumn();
            oTxtC.Visible = true;
            oTxtC.HeaderText = "Quality";
            oTxtC.ReadOnly = true;
            oTxtC.ValueType = typeof(string);
            oTxtC.Width = 150;
            dataGridView1.Columns.Add(oTxtC);

            oTxtD = new DataGridViewTextBoxColumn();
            oTxtD.Visible = true;
            oTxtD.HeaderText = "Styles";
            oTxtD.ReadOnly = true;
            oTxtD.ValueType = typeof(string);
            oTxtD.Width = 150;
            dataGridView1.Columns.Add(oTxtD);

            oTxtE = new DataGridViewTextBoxColumn();
            oTxtE.Visible = true;
            oTxtE.HeaderText = "Colours";
            oTxtE.ReadOnly = true;
            oTxtE.ValueType = typeof(string);
            oTxtE.Width = 100;
            dataGridView1.Columns.Add(oTxtE);

            oTxtF = new DataGridViewTextBoxColumn();
            oTxtF.Visible = true;
            oTxtF.HeaderText = "Req Week";
            oTxtF.ReadOnly = true;
            oTxtF.ValueType = typeof(int);
            oTxtF.Width = 100;
            dataGridView1.Columns.Add(oTxtF);
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.AllowUserToAddRows = false;

            core = new Util();
        }

        private void frmCloseDyeOrders_Load(object sender, EventArgs e)
        {
            FormLoaded = false;

            rbGarments.Checked = true;

            using (var context = new TTI2Entities())
            {
                dataGridView1.Rows.Clear();

                var DyeOrders = (from T1 in context.TLDYE_DyeOrder
                                 join T2 in context.TLADM_CustomerFile
                                 on T1.TLDYO_Customer_FK equals T2.Cust_Pk
                                 where !T1.TLDYO_Closed && !T2.Cust_FabricCustomer
                                 select T1).ToList();

                    // context.TLDYE_DyeOrder.Where(x => !x.TLDYO_Closed).ToList();

                foreach (var DyeOrder in DyeOrders)
                {
                    var index = dataGridView1.Rows.Add();
                    dataGridView1.Rows[index].Cells[0].Value = DyeOrder.TLDYO_Pk;
                    dataGridView1.Rows[index].Cells[1].Value = false;
                    dataGridView1.Rows[index].Cells[2].Value = DyeOrder.TLDYO_OrderNum;
                    dataGridView1.Rows[index].Cells[3].Value = context.TLADM_Griege.Find(DyeOrder.TLDYO_Greige_FK).TLGreige_Description;
                    dataGridView1.Rows[index].Cells[4].Value = context.TLADM_Styles.Find(DyeOrder.TLDYO_Style_FK).Sty_Description;
                    dataGridView1.Rows[index].Cells[5].Value = context.TLADM_Colours.Find(DyeOrder.TLDYO_Colour_FK).Col_Display ;
                    dataGridView1.Rows[index].Cells[6].Value = DyeOrder.TLDYO_CutReqWeek;
                }
            }

            FormLoaded = true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Button oBtn = (Button)sender;
            DataGridView oDgv = dataGridView1;

            if (oBtn != null && FormLoaded)
            {
                using (var context = new TTI2Entities())
                {

                    if (rbGarments.Checked)
                    {
                        foreach (DataGridViewRow Row in dataGridView1.Rows)
                        {
                            if (!(bool)Row.Cells[1].Value)
                                continue;

                            var DyeOrder_Pk = (int)Row.Cells[0].Value;

                            var DOrder = context.TLDYE_DyeOrder.Find(DyeOrder_Pk);
                            if (DOrder != null)
                            {
                                DOrder.TLDYO_Closed = true;
                            }
                        }
                    }
                    else
                    {
                        foreach (DataGridViewRow Row in dataGridView1.Rows)
                        {
                            if (!(bool)Row.Cells[1].Value)
                                continue;

                            var DyeOrderF_Pk = (int)Row.Cells[0].Value;

                            var DOrderF = context.TLDYE_DyeOrderFabric.Find(DyeOrderF_Pk);
                            if (DOrderF != null)
                            {
                                DOrderF.TLDYEF_Closed = true;
                            }
                        }
                    }

                    try
                    {
                        context.SaveChanges();
                        MessageBox.Show("Data successfuly saved to the database");
                        this.dataGridView1.Rows.Clear();
                        this.frmCloseDyeOrders_Load(this, null);

                        

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }

        private void rbGarments_CheckedChanged(object sender, EventArgs e)
        {
            var RadioB = sender as RadioButton;
            if(RadioB != null && FormLoaded)
            {
                if(RadioB.Checked)
                {
                    frmCloseDyeOrders_Load(this, null); 
                }
            }
        }

        private void rbFabricSales_CheckedChanged(object sender, EventArgs e)
        {
            var RadioB = sender as RadioButton;
            if (RadioB != null && FormLoaded)
            {
                if (RadioB.Checked)
                {
                    using (var context = new TTI2Entities())
                    {
                        dataGridView1.Rows.Clear();

                        var DyeOrders = (from T1 in context.TLDYE_DyeOrderFabric
                                         join T2 in context.TLADM_CustomerFile
                                         on T1.TLDYEF_Customer_FK equals T2.Cust_Pk
                                         where !T1.TLDYEF_Closed && T2.Cust_FabricCustomer
                                         select T1).ToList();

                        // context.TLDYE_DyeOrder.Where(x => !x.TLDYO_Closed).ToList();

                        foreach (var DyeOrder in DyeOrders)
                        {
                            var index = dataGridView1.Rows.Add();
                            dataGridView1.Rows[index].Cells[0].Value = DyeOrder.TLDYEF_Pk;
                            dataGridView1.Rows[index].Cells[1].Value = false;
                            dataGridView1.Rows[index].Cells[2].Value = DyeOrder.TLDYEF_DyeOrderNo;
                            dataGridView1.Rows[index].Cells[3].Value = context.TLADM_Griege.Find(DyeOrder.TLDYEF_Greige_FK).TLGreige_Description;
                            dataGridView1.Rows[index].Cells[4].Value = ""; //  context.TLADM_Styles.Find(DyeOrder.TLDYO_Style_FK).Sty_Description;
                            dataGridView1.Rows[index].Cells[5].Value = context.TLADM_Colours.Find(DyeOrder.TLDYEF_Colours_FK).Col_Display;
                            dataGridView1.Rows[index].Cells[6].Value = DyeOrder.TLDYEF_DyeWeek;
                        }
                    }
                }
            }
        }
    }
}
