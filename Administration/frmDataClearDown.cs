using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq.Expressions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Utilities;
using EntityFramework.Extensions;
using System.Threading;


using LinqKit;


namespace Administration
{
    public partial class frmDataClearDown : Form
    {
        protected readonly TTI2Entities _context;
        DateTime DSelected;

        private BackgroundWorker backgroundWorker1;
        int ProgressState = 0;

        int PendingCuts = 0;
        bool lStage1 = false;
        bool lStage2 = false;
        public frmDataClearDown()
        {
            InitializeComponent();
            this._context = new TTI2Entities();
            backgroundWorker1 = new BackgroundWorker();

            this.backgroundWorker1.DoWork += new DoWorkEventHandler(this.BackGroundWork1_DoWork);
            this.backgroundWorker1.WorkerReportsProgress = true;
            this.backgroundWorker1.WorkerSupportsCancellation = true;
            
            this.backgroundWorker1.ProgressChanged += new ProgressChangedEventHandler(Worker_ProgressChanged);
            this.backgroundWorker1.RunWorkerCompleted += new RunWorkerCompletedEventHandler(Worker_RunWorkerCompleted);
        }

        private void frmDataClearDown_Load(object sender, EventArgs e)
        {
            dtpPriorDate.Value = DateTime.Now.AddDays(-1 * DateTime.Now.DayOfYear);
            this.toolStripStatusLabel1.Text = string.Empty; 
            
        }
      
        private void btnCommence_Click(object sender, EventArgs e)
        {
            DSelected = Convert.ToDateTime(dtpPriorDate.Value.ToShortDateString());

            //===========================================================================
                     
            _context.TLCSV_StockOnHand.Where(x => x.TLSOH_Sold && x.TLSOH_CutSheet_FK == 0 && x.TLSOH_SoldDate <= DSelected).Delete();

            try
            {
                _context.Configuration.AutoDetectChangesEnabled = false;
                _context.TLCUT_CutSheet.Update(x => new TLCUT_CutSheet() { TLCUTSH_MarkedForDeletion = false });

            }
            finally
            {
                _context.Configuration.AutoDetectChangesEnabled = true;
            }
            
            this.toolStripStatusLabel1.Text = "Commencing Customer Clear Down";
            backgroundWorker1.RunWorkerAsync(2000);
            
        }

        private void BackGroundWork1_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker helperBW = sender as BackgroundWorker;
            int arg = (int)e.Argument;
                
            e.Result = ProcessLogicMethod(helperBW, arg);

            if (helperBW.CancellationPending)
            {
                    e.Cancel = true;
            }
        }

        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.backgroundWorker1.CancelAsync();
            if (e.Cancelled)
            {
                this.toolStripStatusLabel1.Text = "Run cancelled";
            }
            else
            {
                this.toolStripStatusLabel1.Text = "Run completed";
            }
        }
        private void Worker_ProgressChanged(object sender,
                                     ProgressChangedEventArgs e)
        {
            if(lStage1)
            {
                lStage1 = !lStage1;
                this.toolStripStatusLabel1.Text = "Begining Stage 1";
            }
            else if(lStage2)
            {
                lStage2 = !lStage2;
                this.toolStripStatusLabel1.Text = "Beginning Stage 2";
            }
            
           // this.toolStripProgressBar1.Value = e.ProgressPercentage;
            
        }

      
        private int ProcessLogicMethod(BackgroundWorker bw, int a)
        {
            int result = 0;
            var BoxSold = _context.TLCSV_StockOnHand.Where(x => x.TLSOH_Sold && (DateTime)x.TLSOH_SoldDate <= DSelected).GroupBy(x => x.TLSOH_CutSheet_FK).ToList();
            PendingCuts = BoxSold.Count();
            lStage1 = true;
                      
            foreach (var BoxGroup in BoxSold)
            {
                var CutSheet_Pk = BoxGroup.FirstOrDefault().TLSOH_CutSheet_FK;
                var WcCount = _context.TLCMT_CompletedWork.Where(x => x.TLCMTWC_CutSheet_FK == CutSheet_Pk).Count();
                var QtySold = _context.TLCSV_StockOnHand.Where(x => x.TLSOH_CutSheet_FK == CutSheet_Pk).Count();

                _context.TLCUT_CutSheet.Where(c => c.TLCutSH_Pk == CutSheet_Pk && ((QtySold >= WcCount) || (WcCount - QtySold < 2))).Update(x=> new TLCUT_CutSheet { TLCUTSH_MarkedForDeletion = true });

                ProgressState += 1;
                bw.ReportProgress((ProgressState * 100) / (PendingCuts - 1));

            }
            

            try
            {
                lStage1 = false;
                lStage2 = true;
             
                
                //Commencing Stage 2
                //==============================================
                var PendingCutSheets = _context.TLCUT_CutSheet.Where(x => x.TLCUTSH_MarkedForDeletion).Select(x=>x.TLCutSH_Pk).ToList();
                PendingCuts = PendingCutSheets.Count();
                ProgressState = 0;
                
                foreach(var PCutSh in PendingCutSheets)
                {
                    //*************************************************
                    // This is Customer Services
                    //**************************************************
                    _context.TLCSV_StockOnHand.Where(x => x.TLSOH_CutSheet_FK == PCutSh).Delete();

                    //*************************************************
                    // This is CMT 
                    //**************************************************
                    var LineIssue = _context.TLCMT_LineIssue.Where(x => x.TLCMTLI_CutSheet_FK == PCutSh).FirstOrDefault();
                    if(LineIssue != null)
                    {
                        _context.TLCMT_Statistics.Where(x => x.CMTS_PanelIssue_FK == LineIssue.TLCMTLI_Pk).Delete();
                        _context.TLCMT_ProductionFaults.Where(x => x.TLCMTPF_LineIssue_FK == LineIssue.TLCMTLI_Pk).Delete();
                    }
                    
                    _context.TLCMT_LineIssue.Where(x => x.TLCMTLI_CutSheet_FK == PCutSh).Delete();
                    _context.TLCMT_AuditMeasureRecorded.Where(x => x.TLBFAR_CutSheet_FK == PCutSh).Delete();
                    _context.TLCMT_CompletedWork.Where(x => x.TLCMTWC_CutSheet_FK == PCutSh).Delete();
                    _context.TLCMT_LineFeederBundleCheck.Where(x => x.TLCMTLF_CutSheet_FK == PCutSh).Delete();
                    _context.TLCMT_NonCompliance.Where(x => x.CMTNCD_CutSheet_Fk == PCutSh).Delete();

                    ProgressState += 1;
                    bw.ReportProgress((ProgressState * 100) / (PendingCuts - 1));
                }
               
            }
            catch (Exception ex)
            {
                MessageBox.Show("Message " + ex.InnerException);

            }

            return result;
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
          this.backgroundWorker1.CancelAsync();
        }

        private void BackGroundProcessLogicMethod(object sender, RunWorkerCompletedEventArgs e)
        {
            if(e.Cancelled)
            {
               MessageBox.Show("Operation was cancelled");
            }
        }

        private void frmDataClearDown_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!e.Cancel)
            {
                if(_context != null)
                {
                    _context.Dispose();
                }
                
                if(backgroundWorker1.IsBusy)
                {
                    this.backgroundWorker1.CancelAsync();
                }
            }
        }
        
    }
    
}
