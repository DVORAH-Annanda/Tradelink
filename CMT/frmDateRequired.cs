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

namespace CMT
{
    public partial class frmDateRequired : Form
    {
        CMTQueryParameters QueryParms = null;
        CMTRepository repo = null;
        bool FormLoaded;
        CMTReportOptions repOptions = null;
        bool _Mode = false;

        Util core;

        DataGridViewTextBoxColumn oTxtBoxA;
        DataGridViewTextBoxColumn oTxtBoxB;
        DataGridViewTextBoxColumn oTxtBoxC;

        DataGridViewCheckBoxColumn oChkA;

        public frmDateRequired(bool Mode)
        {
            // Mode is either True or False
            // if Mode True then it is in Reporting Mode
            // else it is in Edit Mode to allow for adjustment of Required Dates 
            InitializeComponent();

            repo = new CMTRepository();

            _Mode = Mode;
            if (Mode)
            {
                this.Text = "Required Dates Reporting Facility";
                this.dataGridView1.Visible = false;

            }
            else
            {
                this.Text = "Required Dates Editting facility";
                this.label2.Visible = false;
                this.dtpToDate.Visible = false;
                this.dataGridView1.Visible = true;

                core = new Util();
                oTxtBoxA = new DataGridViewTextBoxColumn();
                oTxtBoxA.ReadOnly = true;
                oTxtBoxA.Visible = false;
                oTxtBoxA.HeaderText = "Primary Key";
                dataGridView1.Columns.Add(oTxtBoxA);

                oChkA = new DataGridViewCheckBoxColumn();
                oChkA.ValueType = typeof(bool);
                oChkA.HeaderText = "Select";
                dataGridView1.Columns.Add(oChkA);

                oTxtBoxB = new DataGridViewTextBoxColumn();
                oTxtBoxB.ReadOnly = true;
                oTxtBoxB.Visible = true;
                oTxtBoxB.HeaderText = "CutSheet Number";
                dataGridView1.Columns.Add(oTxtBoxB);

                oTxtBoxC = new DataGridViewTextBoxColumn();
                oTxtBoxC.ReadOnly = true;
                oTxtBoxC.Visible = true;
                oTxtBoxC.HeaderText = "Current Required Date";
                dataGridView1.Columns.Add(oTxtBoxC);

                dataGridView1.AllowUserToAddRows = false;
                dataGridView1.AllowUserToOrderColumns = false;
            }

        }

        private void frmDateRequired_Load(object sender, EventArgs e)
        {
            FormLoaded = false;
           
            using (var context = new TTI2Entities())
            {
                if (!_Mode)
                {
                    dataGridView1.Rows.Clear();

                    var Entries = (from CutSheet in context.TLCUT_CutSheet
                                   join CmtLineIssue in context.TLCMT_LineIssue on CutSheet.TLCutSH_Pk equals CmtLineIssue.TLCMTLI_CutSheet_FK
                                   where !CmtLineIssue.TLCMTLI_WorkCompleted
                                   select new { CutSheet, CmtLineIssue }).ToList();

                    foreach (var Entry in Entries)
                    {
                        var index = dataGridView1.Rows.Add();
                        dataGridView1.Rows[index].Cells[0].Value = Entry.CmtLineIssue.TLCMTLI_Pk;
                        dataGridView1.Rows[index].Cells[1].Value = false;
                        dataGridView1.Rows[index].Cells[2].Value = Entry.CutSheet.TLCutSH_No;
                        if(Entry.CmtLineIssue.TLCMTLI_Required_Date != null)
                           dataGridView1.Rows[index].Cells[3].Value = (DateTime)Entry.CmtLineIssue.TLCMTLI_Required_Date;
                    }
                 }
            }
            QueryParms = new CMTQueryParameters();
            FormLoaded = true;
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            Button oBtn = (Button)sender;
            if(oBtn != null && FormLoaded)
            {
                if (_Mode)
                {
                    if (chkProductionResults.Checked)
                        QueryParms.ProductionResults = true;

                    repOptions = new CMTReportOptions();

                    DateTime FromDate = Convert.ToDateTime(dtpFromDate.Value.ToShortDateString());
                    DateTime ToDate = Convert.ToDateTime(dtpToDate.Value.ToShortDateString());

                    ToDate = ToDate.AddHours(23);

                    repOptions.fromDate = FromDate;
                    repOptions.toDate = ToDate;

                    frmCMTViewRep vRep = new frmCMTViewRep(30, QueryParms, repOptions);
                    int h = Screen.PrimaryScreen.WorkingArea.Height;
                    int w = Screen.PrimaryScreen.WorkingArea.Width;
                    vRep.ClientSize = new Size(w, h);
                    vRep.ShowDialog(this);

                    frmDateRequired_Load(this, null);

                }
                else
                {
                    using ( var context = new TTI2Entities())
                    {
                        foreach (DataGridViewRow Row in dataGridView1.Rows)
                        {
                            if (!(bool)Row.Cells[1].Value)
                                continue;

                            var Pk = (int)Row.Cells[0].Value;
                            var LineIssue = context.TLCMT_LineIssue.Find(Pk);
                            if (LineIssue != null)
                            {
                                LineIssue.TLCMTLI_Required_Date = dtpFromDate.Value;
                            }
                        }

                        try
                        {
                            context.SaveChanges();
                            MessageBox.Show("Data successfully saved to the database");
                            
                            this.frmDateRequired_Load(this, null);

                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }
                }
            }
        }
    }
}
