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
 
namespace Cutting
{
    public partial class frmCutOnOffHold : Form
    {
        bool FormLoaded;

        DataGridViewTextBoxColumn selecta;    // index of the main record 
        DataGridViewCheckBoxColumn oChkA;     // Is On or Off Hold;
        DataGridViewCheckBoxColumn oChkB;     // Is On or Off Priority;
        DataGridViewTextBoxColumn selectb;    // CutSheet Number  
        DataGridViewTextBoxColumn selectc;    // Style 
        DataGridViewTextBoxColumn selectd;    // Colour  
        Util core;
   
        public frmCutOnOffHold()
        {
            InitializeComponent();

            selecta = new DataGridViewTextBoxColumn();
            selecta.ValueType = typeof(System.Int32);
            selecta.Visible = false;

            oChkA = new DataGridViewCheckBoxColumn();
            oChkA.ValueType = typeof(bool);
            oChkA.HeaderText = "On Hold";

            oChkB = new DataGridViewCheckBoxColumn();
            oChkB.ValueType = typeof(bool);
            oChkB.HeaderText = "Priority";

            selectb = new DataGridViewTextBoxColumn();
            selectb.HeaderText = " Cutsheet Number";

            selectc = new DataGridViewTextBoxColumn();
            selectc.HeaderText = "Style";
            selectc.Width = 175;

            selectd = new DataGridViewTextBoxColumn(); 
            selectd.HeaderText = "Colour";
            selectd.Width = 175;

            dataGridView1.Columns.Add(selecta);

            dataGridView1.Columns.Add(oChkA);
            dataGridView1.Columns.Add(oChkB);

            dataGridView1.Columns.Add(selectb);
            dataGridView1.Columns.Add(selectc);
            dataGridView1.Columns.Add(selectd);

            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToOrderColumns = false;

            core = new Util();

           
        }

        private void frmCutOnOffHold_Load(object sender, EventArgs e)
        {
            rbOnHold.Checked = true;
            rbOffHold.Checked = false;
            txtReasons.Text = string.Empty;
 
            FormLoaded = false;

            dataGridView1.Rows.Clear();

            PlaceOnHold();

            FormLoaded = true;

        }

        private void PlaceOnHold()
        {
            using (var context = new TTI2Entities())
            {
                var CutSheetsOnHold = context.TLCUT_CutSheet.Where(x => !x.TLCutSH_Closed && !x.TLCUTSH_OnHold && !x.TLCutSH_WIPComplete && x.TLCutSH_Accepted).ToList();
                foreach (var CutSheet in CutSheetsOnHold)
                {
                    var index = dataGridView1.Rows.Add();
                    dataGridView1.Rows[index].Cells[0].Value = CutSheet.TLCutSH_Pk;
                    dataGridView1.Rows[index].Cells[1].Value = false;
                    dataGridView1.Rows[index].Cells[2].Value = (bool)CutSheet.TLCUTSH_Priority; 
                    dataGridView1.Rows[index].Cells[3].Value = CutSheet.TLCutSH_No;
                    dataGridView1.Rows[index].Cells[4].Value = context.TLADM_Styles.Find(CutSheet.TLCutSH_Styles_FK).Sty_Description;
                    dataGridView1.Rows[index].Cells[5].Value = context.TLADM_Colours.Find(CutSheet.TLCutSH_Colour_FK).Col_Display;
                }
            }
        }


        private void PlaceOffHold()
        {
            using (var context = new TTI2Entities())
            {
                var CutSheetsOnHold = context.TLCUT_CutSheet.Where(x => !x.TLCutSH_Closed && x.TLCUTSH_OnHold && !x.TLCutSH_WIPComplete).ToList();
                foreach (var CutSheet in CutSheetsOnHold)
                {
                    var index = dataGridView1.Rows.Add();
                    dataGridView1.Rows[index].Cells[0].Value = CutSheet.TLCutSH_Pk;
                    dataGridView1.Rows[index].Cells[1].Value = true;
                    dataGridView1.Rows[index].Cells[2].Value = (bool)CutSheet.TLCUTSH_Priority;
                    dataGridView1.Rows[index].Cells[3].Value = CutSheet.TLCutSH_No;
                    dataGridView1.Rows[index].Cells[4].Value = context.TLADM_Styles.Find(CutSheet.TLCutSH_Styles_FK).Sty_Description;
                    dataGridView1.Rows[index].Cells[5].Value = context.TLADM_Colours.Find(CutSheet.TLCutSH_Colour_FK).Col_Display;
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
                    label2.Visible = true;
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
                     label2.Visible = false;
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

                            var CS = context.TLCUT_CutSheet.Find(PrimeKey);

                            if (CS != null)
                            {
                                CS.TLCUTSH_OnHold = true;
                                CS.TLCUTSH_OnHold_Reasons = txtReasons.Text;
                                CS.TLCUTSH_OnHoldDate = dtpOnHold.Value;
                                
                                

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

                            var CS = context.TLCUT_CutSheet.Find(PrimeKey);

                            if (CS != null)
                            {
                                CS.TLCUTSH_OnHold = false;
                                CS.TLCUTSH_OnHold_Reasons = string.Empty;
                                CS.TLCUTSH_OnHoldDate = null;
                                
                            }
                        }
                    }

                    try
                    {
                        context.SaveChanges();
                        MessageBox.Show("Data successfully saved to database");

                        frmCutOnOffHold_Load(this, null);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.InnerException.Message);
                    }
                }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView oDgv = sender as DataGridView;
            if (FormLoaded)
            {
                if (oDgv.Focused && oDgv.CurrentCell is DataGridViewCheckBoxCell)
                {
                    var Cell = oDgv.CurrentCell;
                    if (Cell.ColumnIndex == 2)
                    {
                        bool isChecked = (bool)Cell.EditedFormattedValue;
                                               
                        using (var context = new TTI2Entities())
                        {
                            var PrimeKey = (int)oDgv.CurrentRow.Cells[0].Value;

                             var CS = context.TLCUT_CutSheet.Find(PrimeKey);

                             if (CS != null)
                             {
                                   if (isChecked)
                                        CS.TLCUTSH_Priority = true;
                                   else
                                        CS.TLCUTSH_Priority = false;

                                    try
                                    {
                                        context.SaveChanges();
                                    }
                                    catch (Exception ex)
                                    {

                                    }
                             }
                        }
                       
                    }
                }
            }
        }
    }
}
