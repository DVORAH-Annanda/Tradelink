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
    public partial class frmFabricOnOffHold : Form
    {
        bool FormLoaded;

        DataGridViewTextBoxColumn selecta;    // index of the main record 
        DataGridViewCheckBoxColumn oChkA;     // Is On or Off Hold;
        DataGridViewTextBoxColumn selectb;    // CutSheet Number  
        DataGridViewTextBoxColumn selectc;    // Style 
        DataGridViewTextBoxColumn selectd;    // Colour  

        public frmFabricOnOffHold()
        {
            InitializeComponent();
            selecta = new DataGridViewTextBoxColumn();
            selecta.ValueType = typeof(System.Int32);
            selecta.Visible = false;

            oChkA = new DataGridViewCheckBoxColumn();
            oChkA.ValueType = typeof(bool);
            oChkA.HeaderText = "On Hold";

            selectb = new DataGridViewTextBoxColumn();
            selectb.HeaderText = " DyeBatch Number";

            selectc = new DataGridViewTextBoxColumn();
            selectc.HeaderText = "Quality";
            selectc.Width = 175;

            selectd = new DataGridViewTextBoxColumn();
            selectd.HeaderText = "Colour";
            selectd.Width = 175;

            dataGridView1.Columns.Add(selecta);

            dataGridView1.Columns.Add(oChkA);

            dataGridView1.Columns.Add(selectb);
            dataGridView1.Columns.Add(selectc);
            dataGridView1.Columns.Add(selectd);

            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToOrderColumns = false;
        }

        private void frmFabricOnOffHold_Load(object sender, EventArgs e)
        {
            FormLoaded = false;

            rbOnHold.Checked = true;
            rbOffHold.Checked = false;
            
            txtReasons.Text = string.Empty;

   
            dataGridView1.Rows.Clear();

            PlaceOnHold();

   
            FormLoaded = true; 
        }

        private void PlaceOnHold()
        {
            using (var context = new TTI2Entities())
            {
                var DyeBatches = context.TLDYE_DyeBatch.Where(x =>  !x.DYEB_Closed
                                                                 && !x.DYEB_OnHold 
                                                                 &&  x.DYEB_Stage1
                                                                 && !x.DYEB_CommissinCust
                                                                 && !x.DYEB_QAInspected).ToList();
                foreach (var DyeBatch in DyeBatches)
                {
                    var index = dataGridView1.Rows.Add();
                    dataGridView1.Rows[index].Cells[0].Value = DyeBatch.DYEB_Pk;
                    dataGridView1.Rows[index].Cells[1].Value = false;
                    dataGridView1.Rows[index].Cells[2].Value = DyeBatch.DYEB_BatchNo;
                    dataGridView1.Rows[index].Cells[3].Value = context.TLADM_Griege.Find(DyeBatch.DYEB_Greige_FK).TLGreige_Description;
                    dataGridView1.Rows[index].Cells[4].Value = context.TLADM_Colours.Find(DyeBatch.DYEB_Colour_FK).Col_Display;
                }
            }
        }

        private void PlaceOffHold()
        {
            using (var context = new TTI2Entities())
            {
                var DyeBatchOnHold = context.TLDYE_DyeBatch.Where(x => x.DYEB_OnHold).ToList();
                foreach (var DyeBatch in DyeBatchOnHold)
                {
                    var index = dataGridView1.Rows.Add();
                    dataGridView1.Rows[index].Cells[0].Value = DyeBatch.DYEB_Pk;
                    dataGridView1.Rows[index].Cells[1].Value = true;
                    dataGridView1.Rows[index].Cells[2].Value = DyeBatch.DYEB_BatchNo;
                    dataGridView1.Rows[index].Cells[3].Value = context.TLADM_Griege.Find(DyeBatch.DYEB_Greige_FK).TLGreige_Description;
                    dataGridView1.Rows[index].Cells[4].Value = context.TLADM_Colours.Find(DyeBatch.DYEB_Colour_FK).Col_Display;
                }
            }
        }

        private void rbOnHold_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton oRad = (RadioButton)sender;
            if (oRad != null && FormLoaded && oRad.Checked)
            {
                if (!txtReasons.Visible)
                {
                    txtReasons.Visible = true;
                    label1.Visible = true;
                }
                dataGridView1.Rows.Clear();
                PlaceOnHold();

            }

        }

        private void rbOffHold_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton oRad = (RadioButton)sender;
            if (oRad != null && FormLoaded && oRad.Checked)
            {
                if (txtReasons.Visible)
                {
                    txtReasons.Visible = false;
                    label1.Visible = false;
                }
                dataGridView1.Rows.Clear();
                PlaceOffHold();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
             Button oBtn = (Button)sender;
            if (oBtn != null && FormLoaded)
            {
                using ( var context = new TTI2Entities())
                {
                    if (rbOnHold.Checked)
                    {
                        if (txtReasons.Text.Length == 0)
                        {
                            MessageBox.Show("Please give a reason for placing a box on hold");
                            return;
                        }

                        foreach (DataGridViewRow row in dataGridView1.Rows)
                        {
                            if (!(bool)row.Cells[1].Value)
                                continue;

                            var PrimeKey = (int)row.Cells[0].Value;

                            var DB = context.TLDYE_DyeBatch.Find(PrimeKey);

                            if (DB != null)
                            {
                                DB.DYEB_OnHold = true;
                                DB.DYEB_OnHold_Reason = txtReasons.Text;
                                DB.DYEB_OnHold_Date =  DateTime.Now;
                            }

                        }
                    }
                    else
                    {
                        foreach (DataGridViewRow row in dataGridView1.Rows)
                        {
                            if ((bool)row.Cells[1].Value)
                                continue;

                            var PrimeKey = (int)row.Cells[0].Value;

                            var  DB = context.TLDYE_DyeBatch.Find(PrimeKey);

                            if (DB != null)
                            {
                                DB.DYEB_OnHold = false;
                                DB.DYEB_OnHold_Reason = string.Empty;
                                DB.DYEB_OnHold_Date = null;
                            }

                        }
                    }

                    try
                    {
                        context.SaveChanges();
                        MessageBox.Show("Data successfully saved to database");

                        frmFabricOnOffHold_Load(this, null);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.InnerException.Message);
                    }
                }
            }
      
        }
    }
}
