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
using log4net.Util.TypeConverters;
using System.Data.Entity;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;
using Microsoft.Office.Interop.Excel;
using System.Reflection;
using System.Runtime.Remoting.Contexts;

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
        bool lStage3 = false;
        bool lStage4 = false;

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
            // Display the ProgressBar control.
            progressBar1.Visible = true;
            progressBar1.Minimum = 0;
            // Set the Step property to a value of 1 to represent PPS Record processed
            progressBar1.Step = 1;

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
                _context.TLDYE_DyeBatchDetails.Update(x => new TLDYE_DyeBatchDetails() { DYEBO_MarkedForDeletion = false });
                _context.TLKNI_GreigeProduction.Update(x => new TLKNI_GreigeProduction() { GreigeP_MarkedForDeletion = false });
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
            this.progressBar1.Visible = false;

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
            if (lStage1)
            {
                lStage1 = !lStage1;
                this.toolStripStatusLabel1.Text = "Begining Stage 1";
            }
            else if (lStage2)
            {
                lStage2 = !lStage2;
                this.toolStripStatusLabel1.Text = "Beginning Stage 2";
            }
            else if (lStage3)
            {
                lStage3 = !lStage3;
                this.toolStripStatusLabel1.Text = "Beginning Stage 3";
            }
            else if (lStage4)
            {
                lStage4 = !lStage4;
                this.toolStripStatusLabel1.Text = "Beginning Stage 4";
            }
            this.progressBar1.Value = e.ProgressPercentage;

        }

        private int ProcessLogicMethod(BackgroundWorker bw, int a)
        {
            int result = 0;
            ProgressState = 0;
            lStage1 = true;
            lStage2 = false;
            lStage3 = false;
            lStage4 = false;


            _context.Cutsheet_Cleanup(DSelected);

            lStage2 = true;
            lStage1 = false;

            //Commencing Stage 2
            //==============================================
            var PendingCutSheets = _context.TLCUT_CutSheet.Where(x => x.TLCUTSH_MarkedForDeletion).Select(x => x.TLCutSH_Pk).ToList();
            PendingCuts = PendingCutSheets.Count();
            ProgressState = 0;

            foreach (var PCutSh in PendingCutSheets)
            {
                //*************************************************
                // This is Customer Services
                //**************************************************

                _context.TLCSV_StockOnHand.Where(x => x.TLSOH_CutSheet_FK == PCutSh).Delete();

                //*************************************************
                // This is CMT 
                //**************************************************
                var LineIssue = _context.TLCMT_LineIssue.Where(x => x.TLCMTLI_CutSheet_FK == PCutSh).FirstOrDefault();
                if (LineIssue != null)
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

            lStage2 = false;
            lStage1 = false;
            lStage3 = true;
            lStage4 = false;

            //Commencing Stage 3
            //==============================================
            ProgressState = 0;
            var CS = _context.TLCUT_CutSheet.Where(x => x.TLCUTSH_MarkedForDeletion).Count();
            IList<TLCUT_CutSheet> CSMarkedforDeletion = _context.TLCUT_CutSheet.Where(x => x.TLCUTSH_MarkedForDeletion).ToList();

            foreach (var CutSh in CSMarkedforDeletion)
            {
                var CutShR = _context.TLCUT_CutSheetReceipt.Where(x => x.TLCUTSHR_CutSheet_FK == CutSh.TLCutSH_Pk).FirstOrDefault();
                if (CutShR != null)
                {
                    _context.TLCUT_TrimsOnCut.Where(x => x.TLCUTTOC_CutSheet_FK == CutShR.TLCUTSHR_Pk).Delete();
                    var CutShR_Detail = _context.TLCUT_CutSheetReceiptDetail.Where(x => x.TLCUTSHRD_CutSheet_FK == CutShR.TLCUTSHR_Pk).FirstOrDefault();
                    if (CutShR_Detail != null)
                    {
                        _context.TLCUT_QAResults.Where(x=>x.TLCUTQA_Bundle_FK == CutShR_Detail.TLCUTSHRD_Pk).Delete();
                        _context.TLCUT_CutSheetReceiptDetail.Where(x => x.TLCUTSHRD_CutSheet_FK == CutShR.TLCUTSHR_Pk).Delete();
                        _context.TLCUT_QCBerrie.Where(x => x.TLQCFB_CutSheetReceipt_FK == CutShR.TLCUTSHR_Pk).Delete();
                        _context.TLCUT_CutSheetReceipt.Where(x => x.TLCUTSHR_CutSheet_FK == CutShR.TLCUTSHR_Pk).Delete();
                        _context.TLCMT_PanelIssueDetail.Where(x => x.CMTPID_CutSheet_FK == CutShR.TLCUTSHR_Pk).Delete();
                    }
                    _context.TLCUT_ExpectedUnits.Where(x => x.TLCUTE_CutSheet_FK == CutSh.TLCutSH_Pk).Delete();
                    _context.TLCUT_CutSheet.Where(x => x.TLCutSH_Pk == CutSh.TLCutSH_Pk).Delete();
                    List<int> DBDetPk = (from T1 in _context.TLCUT_CutSheetDetail
                                         join T2 in _context.TLDYE_DyeBatchDetails
                                         on T1.TLCutSHD_DyeBatchDet_FK equals T2.DYEBD_Pk
                                         where T1.TLCutSHD_CutSheet_FK == CutSh.TLCutSH_Pk
                                         select T2.DYEBD_Pk).ToList();
                    if (DBDetPk != null)
                    {
                        foreach (var n in DBDetPk)
                        {
                            _context.TLDYE_DyeBatchDetails.Where(x => x.DYEBD_Pk == n).Update(x => new TLDYE_DyeBatchDetails { DYEBO_MarkedForDeletion = true });
                        }
                    }
                }

                ProgressState += 1;
                bw.ReportProgress((ProgressState * 100) / (CS - 1));

            }
            //Commencing Stage 4
            lStage2 = false;
            lStage1 = false;
            lStage3 = false;
            lStage4 = true;
            //==============================================
            // Involved are the Dye Orders, DyeBatch, DyeBatchDetails 
            // plus all the Statistical info associated with the dye batch 
            //===================================================================
            ProgressState = 0;

            var MarkedForD = _context.TLDYE_DyeBatchDetails.Where(x => x.DYEBO_MarkedForDeletion).Count();
            var GrpBDetails = _context.TLDYE_DyeBatchDetails.Where(x => x.DYEBO_MarkedForDeletion).GroupBy(x => x.DYEBD_DyeBatch_FK).ToList();

            foreach (var DyeBDet in GrpBDetails)
            {
                foreach (var DBD in DyeBDet)
                {
                    _context.TLKNI_GreigeProduction.Where(x => x.GreigeP_Pk == DBD.DYEBD_GreigeProduction_FK).Update((x => new TLKNI_GreigeProduction { GreigeP_MarkedForDeletion = true }));
                }

                ProgressState += 1;
                bw.ReportProgress((ProgressState * 100) / (GrpBDetails.Count - 1));
            }
            
            // Remember to do the non compliance details first before this 
            //===============================================================
            // _context.TLDYE_NonCompliance;
            (from T1 in _context.TLKNI_GreigeProduction
            join T2 in _context.TLDYE_DyeBatchDetails
            on T1.GreigeP_Pk equals T2.DYEBD_GreigeProduction_FK
            join T3 in _context.TLDYE_NonCompliance
            on T2.DYEBD_DyeBatch_FK equals T3.TLDYE_NcrBatchNo_FK
            where T1.GreigeP_MarkedForDeletion
            select T3).Delete();

            // _context.TLDYE_NonComplianceAnalysis
            (from T1 in _context.TLKNI_GreigeProduction
             join T2 in _context.TLDYE_DyeBatchDetails
             on T1.GreigeP_Pk equals T2.DYEBD_GreigeProduction_FK
             join T3 in _context.TLDYE_NonComplianceAnalysis
             on T2.DYEBD_DyeBatch_FK equals T3.TLDYEDC_BatchNo
             where T1.GreigeP_MarkedForDeletion
             select T3).Delete();
            
            // _context.TLDYE_NonComplianceConsDetail
            (from T1 in _context.TLKNI_GreigeProduction
             join T2 in _context.TLDYE_DyeBatchDetails
             on T1.GreigeP_Pk equals T2.DYEBD_GreigeProduction_FK
             join T3 in _context.TLDYE_NonComplianceConsDetail
             on T2.DYEBD_DyeBatch_FK equals T3.DYENCCON_BatchNo_FK
             where T1.GreigeP_MarkedForDeletion
             select T3).Delete();

            //_context.TLDYE_NonComplianceDetail
            (from T1 in _context.TLKNI_GreigeProduction
             join T2 in _context.TLDYE_DyeBatchDetails
             on T1.GreigeP_Pk equals T2.DYEBD_GreigeProduction_FK
             join T3 in _context.TLDYE_NonComplianceDetail
             on T2.DYEBD_DyeBatch_FK equals T3.DYENCRD_BatchNo_Fk
             where T1.GreigeP_MarkedForDeletion
             select T3).Delete();
            
            // Last Step
            //===================================================================
            // Delete all the records in the detail file
            //=====================================================================
            _context.TLDYE_DyeBatchDetails.Where(x => x.DYEBO_MarkedForDeletion).Delete();
         
            var DBatch = _context.TLDYE_DyeBatch;
            var DBatchDetail = _context.TLDYE_DyeBatchDetails;

            var orphanedIds = DBatch.Select(x => x.DYEB_Pk)
                                 .Except(DBatchDetail.Select(x => x.DYEBD_DyeBatch_FK));

            var orphaned = from l in DBatch
                           join id in orphanedIds on l.DYEB_Pk equals id
                           select l;

            DBatch.RemoveRange(orphaned);
            _context.SaveChanges();

            //===================================================================
            // Delete all the orders in the Order file
            //=================================================================
            
            var DOrder = _context.TLDYE_DyeOrder;
       
            var orphanedOrderIds = DOrder.Select(x => x.TLDYO_Pk)
                                 .Except(DBatch.Select(x => x.DYEB_Pk));

            var orphanedOrders = from l in DOrder
                           join id in orphanedOrderIds on l.TLDYO_Pk equals id
                           select l;
            
            DOrder.RemoveRange(orphanedOrders);
            
            _context.SaveChanges();

            var GriegeP = _context.TLKNI_GreigeProduction.Where(x => x.GreigeP_MarkedForDeletion).GroupBy(x => x.GreigeP_KnitO_Fk);
            foreach(var Griege in GriegeP)
            {

            }
            // AllocTrans.TLKYT_YOP_FK = Pallet.TLKNIOP_Pk;
            // KnitOrder.KnitO_YarnO_FK = Pallet.TLKNIOP_YarnOrder_FK;

            return result;
        }
       

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.backgroundWorker1.CancelAsync();
        }

        private void BackGroundProcessLogicMethod(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                MessageBox.Show("Operation was cancelled");
            }
        }

        private void frmDataClearDown_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!e.Cancel)
            {
                if (_context != null)
                {
                    _context.Dispose();
                }

                if (backgroundWorker1.IsBusy)
                {
                    this.backgroundWorker1.CancelAsync();
                }
            }
        }

    }

}
