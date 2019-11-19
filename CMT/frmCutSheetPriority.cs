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
    public partial class frmCutSheetPriority : Form
    {
        bool FormLoaded;
        Util core;

        DataGridViewTextBoxColumn oTxtBoxA;
        DataGridViewTextBoxColumn oTxtBoxB;
        
        DataGridViewCheckBoxColumn oChkA;

        public frmCutSheetPriority()
        {
            InitializeComponent();
            
            core = new Util();
            oTxtBoxA = new DataGridViewTextBoxColumn();
            oTxtBoxA.ReadOnly = true;
            oTxtBoxA.Visible = false;
            oTxtBoxA.HeaderText = "Primary Key";
            dataGridView1.Columns.Add(oTxtBoxA);

            oChkA = new DataGridViewCheckBoxColumn();
            oChkA.HeaderText = "Select";
            dataGridView1.Columns.Add(oChkA);

            oTxtBoxB = new DataGridViewTextBoxColumn();
            oTxtBoxB.ReadOnly = true;
            oTxtBoxB.Visible = true;
            oTxtBoxB.HeaderText = "CutSheet Number";
            dataGridView1.Columns.Add(oTxtBoxB);

            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToOrderColumns = false;
        }

        private void frmCutSheetPriority_Load(object sender, EventArgs e)
        {
            FormLoaded = false;
            using (var context = new TTI2Entities())
            {
                var Entries = (from CutSheet in context.TLCUT_CutSheet
                               join CmtLineIssue in context.TLCMT_LineIssue on CutSheet.TLCutSH_Pk equals CmtLineIssue.TLCMTLI_CutSheet_FK
                               where !CmtLineIssue.TLCMTLI_Priority && !CmtLineIssue.TLCMTLI_WorkCompleted
                               select new { CutSheet, CmtLineIssue}).ToList();
                
                foreach (var Entry in Entries)
                {
                    var index = dataGridView1.Rows.Add();
                    dataGridView1.Rows[index].Cells[0].Value = Entry.CmtLineIssue.TLCMTLI_Pk;
                    dataGridView1.Rows[index].Cells[1].Value = false;
                    dataGridView1.Rows[index].Cells[2].Value = Entry.CutSheet.TLCutSH_No;
                }
            }
            FormLoaded = true;

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Button oBtn = (Button)sender;
            if (oBtn != null && FormLoaded)
            {
                using (var context = new TTI2Entities())
                {
                    foreach (DataGridViewRow Row in dataGridView1.Rows)
                    {
                        if (!(bool)Row.Cells[1].Value)
                            continue;

                        int Pk = (int)Row.Cells[0].Value;

                        var LineIssue = context.TLCMT_LineIssue.Find(Pk);
                        if (LineIssue != null)
                        {
                            LineIssue.TLCMTLI_Priority = true;
                            LineIssue.TLCMTLI_Priority_Date = dtDate.Value;
                        }
                    }

                    try
                    {
                        context.SaveChanges();
                        MessageBox.Show("Data saved successfully to database");
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
