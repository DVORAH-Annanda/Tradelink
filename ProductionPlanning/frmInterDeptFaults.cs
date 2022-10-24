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
namespace ProductionPlanning
{
    public partial class frmInterDeptFaults : Form
    {
        Util core = null;
        bool FormLoaded;
        PPSRepository repo;
        ProdQueryParameters QueryParms;

        public frmInterDeptFaults()
        {
            InitializeComponent();
            core = new Util();

            repo = new PPSRepository();
        }

        private void frmInterDeptFaults_Load(object sender, EventArgs e)
        {
            FormLoaded = false;
        
            using (var context = new TTI2Entities())
            {
                cmboInterDepartmentalOption.DataSource = context.TLPPS_InterDept.OrderBy(x => x.TLInter_Description).ToList();
                cmboInterDepartmentalOption.DisplayMember = "TLInter_Description";
                cmboInterDepartmentalOption.ValueMember = "TLInter_Pk";
                cmboInterDepartmentalOption.SelectedValue = -1;
            }
            txtCutSheet.Text = string.Empty;
            rbCutSheet.Checked = true; 
            QueryParms = new ProdQueryParameters();

            FormLoaded = true;
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            Button oBtn = (Button)sender;
            //===================================================== 
            // 0 = Piece Number
            // 1 = All Dye Batches for Period Selected 
            // 2 = Dye Batch 
            // 3 = Cut Sheet 
            //====================================================
            int[] RecordKey = new int[4];
            bool[] SelectedOption = new bool[4];

            if (oBtn != null && FormLoaded)
            {
                var SelectedChoice = (TLPPS_InterDept)cmboInterDepartmentalOption.SelectedItem;

                if(SelectedChoice == null)
                {
                    MessageBox.Show("Please select a comparision option from the list provided");
                    return;
                }

                QueryParms.InterDeptOption = SelectedChoice.TLInter_Pk;

                using (var context = new TTI2Entities())
                {
                    QueryParms.FromDate = DateTime.Now;
                    QueryParms.ToDate = DateTime.Now;

                    if (rbCutSheet.Checked)
                    {
                        var CutSht = context.TLCUT_CutSheet.FirstOrDefault(x => x.TLCutSH_No == txtCutSheet.Text);
                        if (CutSht == null)
                        {
                            MessageBox.Show("Cut Sheet Number Not Found");
                            return;
                        }

                        var CmtSheet = context.TLCMT_LineIssue.FirstOrDefault(x => x.TLCMTLI_CutSheet_FK == CutSht.TLCutSH_Pk);
                        
                        if (CmtSheet == null)
                        {
                            MessageBox.Show("Cut Sheet Not Found at CMT");
                            return;
                        }
                        else
                        {
                            if(!CmtSheet.TLCMTLI_WorkCompleted)
                            {
                                MessageBox.Show("This Cut Sheet is still work in progress");
                                return;
                            }
                        }
                        RecordKey[3] = CmtSheet.TLCMTLI_Pk;
                        SelectedOption[3] = true;
                    }
                    else if(rbDyeButton.Checked)
                    {
                        var DyeBatch = context.TLDYE_DyeBatch.FirstOrDefault(x=>x.DYEB_BatchNo == txtCutSheet.Text);
                        if (DyeBatch == null)
                        {
                            MessageBox.Show("Dye Batch Number Not Found");
                            return;
                        }

                        RecordKey[2] = DyeBatch.DYEB_Pk;
                 
                        SelectedOption[2] = true;
                    }
                    else
                    {
                        SelectedOption[1] = true;
                        QueryParms.FromDate = dtpFromDate.Value;
                        QueryParms.ToDate = dtpToDate.Value;
                    }
                }

                QueryParms.RecordKeys = RecordKey;
                QueryParms.SelectedOptions = SelectedOption;

                frmPPSViewRep vRep = new frmPPSViewRep(7, QueryParms);
                int h = Screen.PrimaryScreen.WorkingArea.Height;
                int w = Screen.PrimaryScreen.WorkingArea.Width;
                vRep.ClientSize = new Size(w, h);
                vRep.ShowDialog(this);

                frmInterDeptFaults_Load(this, null);
            }
            
        }
    }
}
