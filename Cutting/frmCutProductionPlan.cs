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
    public partial class frmCutProductionPlan : Form
    {
        CuttingRepository repo;
        CuttingQueryParameters QueryParms;
        bool _ProductionResults;

        //datgridview1  
        DataGridViewTextBoxColumn oTxtA;    // 0 Pk
        DataGridViewCheckBoxColumn oChkA;   // 1 Select
        DataGridViewTextBoxColumn oTxtB;    // 2 CutSheet Number
        DataGridViewTextBoxColumn oTxtC;    // 3 Current Required Date
        
        public frmCutProductionPlan(bool ProductionResults)
        {
            // if Production Results equals true then produce a report to show the results
            // from a From and To Date 
            // Otherwise show the data gridview where current records may have altered 
            InitializeComponent();
            _ProductionResults = ProductionResults;

            if (ProductionResults)
            {
                this.Text = "Production Planning Results";
                
                this.dataGridView1.Visible = false;
                
                this.label1.Text = "From Date";
                this.label2.Text = "To Date";
                this.chkProductionResults.Visible = true;

                this.dtpFromDate.Visible = true;
                this.dtpToDate.Visible = true;
                this.label1.Visible = true;
                this.label2.Visible = true;

            }
            else
            {
                this.Text = "Production Planning Required Dates Edit ";

                this.label1.Text = "Expected required Date";
                this.dtpFromDate.Visible = true;
                this.chkProductionResults.Visible = false;

                this.dataGridView1.Visible = true;

                this.label2.Visible = false;
                this.dtpToDate.Visible = false;

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
                oTxtB.HeaderText = "Cut Sheet Number";
                oTxtB.ValueType = typeof(string);
                dataGridView1.Columns.Add(oTxtB);

                oTxtC = new DataGridViewTextBoxColumn();
                oTxtC.ReadOnly = true;
                oTxtC.HeaderText = "Date Required";
                oTxtC.ValueType = typeof(DateTime);
                dataGridView1.Columns.Add(oTxtC);
                dataGridView1.Columns[3].DefaultCellStyle.Format = "dd-MM-yyyy"; 

                dataGridView1.AllowUserToAddRows = false;
                dataGridView1.AutoGenerateColumns = false;
                dataGridView1.AllowUserToOrderColumns = false;

            }
        
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            Button oBtn = (Button)sender;
            QueryParms = new CuttingQueryParameters();
            repo = new CuttingRepository();

            if (oBtn != null)
            {
                DateTime FromDate = Convert.ToDateTime(dtpFromDate.Value.ToShortDateString());
                DateTime ToDate = Convert.ToDateTime(dtpToDate.Value.ToShortDateString());
                ToDate = ToDate.AddHours(23);

                QueryParms.FromDate = FromDate;
                QueryParms.ToDate = ToDate;

                if (_ProductionResults)
                {
                    if (chkProductionResults.Checked)
                        QueryParms.ProductionResults = true;

                    frmCutViewRep vRep = new frmCutViewRep(18, QueryParms);
                    int h = Screen.PrimaryScreen.WorkingArea.Height;
                    int w = Screen.PrimaryScreen.WorkingArea.Width;
                    vRep.ClientSize = new Size(w, h);
                    vRep.ShowDialog(this);
                    if (vRep != null)
                    {
                        vRep.Close();
                        vRep.Dispose();
                    }
                }
                else
                {
                    using (var context = new TTI2Entities())
                    {
                        foreach (DataGridViewRow Row in dataGridView1.Rows)
                        {
                            if ((bool)Row.Cells[1].Value == false)
                                continue;

                            var Pk = (int)Row.Cells[0].Value;
                            var Cutsheet = context.TLCUT_CutSheet.Find(Pk);

                            if (Cutsheet != null)
                                Cutsheet.TLCUTSH_RequiredDate = dtpFromDate.Value;
                        }

                        try
                        {
                            context.SaveChanges();
                            MessageBox.Show("Data successfully saved to database");

                        }
                        catch(Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                            return;
                        }
                    }
                }

            }
        }

        private void frmCutProductionPlan_Load(object sender, EventArgs e)
        {
            using (var context = new TTI2Entities())
            {
                if (!_ProductionResults)
                {
                    var Entries = context.TLCUT_CutSheet.Where(x => !x.TLCutSH_Closed && !x.TLCutSH_WIPComplete && x.TLCUTSH_RequiredDate != null).ToList();
                    foreach (var Entry in Entries)
                    {
                        var index = dataGridView1.Rows.Add();
                        dataGridView1.Rows[index].Cells[0].Value = Entry.TLCutSH_Pk;
                        dataGridView1.Rows[index].Cells[1].Value = false;
                        dataGridView1.Rows[index].Cells[2].Value = Entry.TLCutSH_No;
                        dataGridView1.Rows[index].Cells[3].Value = Entry.TLCUTSH_RequiredDate;
                    }
                }
            }
        }
    }
}
