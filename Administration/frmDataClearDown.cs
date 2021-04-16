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
using LinqKit;


namespace Administration
{
    public partial class frmDataClearDown : Form
    {
        public frmDataClearDown()
        {
            InitializeComponent();
        }

        private void frmDataClearDown_Load(object sender, EventArgs e)
        {
            dtpPriorDate.Value = DateTime.Now.AddDays(-1 * DateTime.Now.DayOfYear);
            pBar1.Visible = false;
  
        }

        private void btnCommence_Click(object sender, EventArgs e)
        {
            DateTime DSelected = Convert.ToDateTime(dtpPriorDate.Value.ToShortDateString());

            using (var context = new TTI2Entities())
            {
                //================================================================================
                // 1st Step is to clear down all the TLCSV_stockOnHand Records 
                //========================================================================

                // Display the ProgressBar control.
                pBar1.Visible = true;
                // Set Minimum to 1 
                pBar1.Minimum = 1;
                // Set the initial value of ProgessBar
                pBar1.Value = 1;
                // Set the Step property to a value of 1 to represent PPS Record processed
                pBar1.Step = 1;
                //===========================================================================

                textBox1.Text = "Commencing Customer Services Clear Down";
                this.Refresh();

                //=====================================
                //1st Step - are there any old records ie in CSV_StockOnHand
                //           that have 0 value in the Cut Sheet FK ...
                //=============================================================

                var Delete = context.TLCSV_StockOnHand.Where(x => x.TLSOH_Sold && x.TLSOH_CutSheet_FK == 0 && x.TLSOH_SoldDate <= DSelected).Delete();



                //==============================================================================
                // 1st Step - We have a situation whereby a box has been sold 
                // We need to ensure that it is removed 
                //====================================================
                var BoxSold = context.TLCSV_StockOnHand.Where(x => x.TLSOH_Sold && x.TLSOH_CutSheet_FK != 0 && x.TLSOH_SoldDate <= DSelected).GroupBy(x => x.TLSOH_CutSheet_FK).ToList();
                
                pBar1.Maximum = BoxSold.Count();
                foreach (var BoxGroup in BoxSold)
                {
                    var SoldQty = BoxGroup.Count();
                    var CutSheet_Pk = BoxGroup.FirstOrDefault().TLSOH_CutSheet_FK;

                    var CompletedW = context.TLCMT_CompletedWork.Where(x => x.TLCMTWC_CutSheet_FK == CutSheet_Pk).Count();
                                        
                    if (SoldQty == CompletedW)
                    {    // this means that the whole cut 
                        /*
                        var PODPrimaryKey = BoxGroup.FirstOrDefault().TLSOH_POOrderDetail_FK;
                        var POPrimaryKey = BoxGroup.FirstOrDefault().TLSOH_POOrder_FK;

                        var BoxedQty = BoxGroup.Sum(x => (int?)x.TLSOH_BoxedQty) ?? 0;

                        var PODetail = context.TLCSV_PuchaseOrderDetail.Find(PODPrimaryKey);
                        if (PODetail != null && !PODetail.TLCUSTO_Closed)
                        {
                            if (BoxedQty < PODetail.TLCUSTO_Qty)
                            {
                                continue;
                            }

                            context.TLCSV_PurchaseOrder.Where(x => x.TLCSVPO_Pk == POPrimaryKey).Delete();
                            context.TLCSV_PuchaseOrderDetail.Where(x => x.TLCUSTO_Pk == PODPrimaryKey).Delete();

                            context.TLCSV_OrderAllocated.Where(x => x.TLORDA_POOrder_FK == POPrimaryKey).Delete();
                        }

                        context.TLCSV_StockOnHand.Where(x => x.TLSOH_POOrderDetail_FK == PODPrimaryKey).Delete();
                        context.TLCSV_RePackTransactions.Where(x => x.REPACT_PurchaseOrderDetail_FK == PODPrimaryKey).Delete();
                        context.TLCSV_RePackConfig.Where(x => x.PORConfig_PONumber_Fk == PODPrimaryKey).Delete();
                        context.TLCSV_MergePODetail.Where(x => x.TLMerge_PoDetail_FK == PODPrimaryKey).Delete();

                        context.TLCSV_OrderAllocated.Where(x => x.TLORDA_POOrder_FK == POPrimaryKey).Delete();

                        Expression<Func<TLCMT_CompletedWork, bool>> CWPredicate = PredicateBuilder.New<TLCMT_CompletedWork>();
                        Expression<Func<TLCSV_Movement, bool>> MovePredicate = PredicateBuilder.New<TLCSV_Movement>();
                        Expression<Func<TLCSV_BoxSplit, bool>> BoxSplitPredicate = PredicateBuilder.New<TLCSV_BoxSplit>();
                        Expression<Func<TLCSV_WhseTransferDetail, bool>> WhseTransfer = PredicateBuilder.New<TLCSV_WhseTransferDetail>();

                        foreach (var Box in BoxGroup)
                        {
                            CWPredicate = CWPredicate.Or(x => x.TLCMTWC_Pk == Box.TLSOH_CMT_FK);
                            BoxSplitPredicate = BoxSplitPredicate.Or(x => x.TLCMTBS_BoxNo == Box.TLSOH_BoxNumber);
                            MovePredicate = MovePredicate.Or(s => s.TLMV_BoxNumber == Box.TLSOH_BoxNumber);
                            WhseTransfer = WhseTransfer.Or(x => x.TLCSVWHTD_TLSOH_Fk == Box.TLSOH_Pk); 
                        }

                        context.TLCMT_CompletedWork.Where(CWPredicate).Update(x => new TLCMT_CompletedWork() { TLCMTWC_MarkedForDeletion = true });
                        context.TLCSV_BoxSplit.Where(BoxSplitPredicate).Delete();
                        context.TLCSV_Movement.Where(MovePredicate).Delete();
                        context.TLCSV_WhseTransferDetail.Where(WhseTransfer).Delete();
                        */
                    }
                    pBar1.PerformStep();

                 }

                // 
                //====================================================================

                var WhseTransfers = context.TLCSV_WhseTransfer.ToList();

                foreach (var Transfer in WhseTransfers)
                {
                    var Trns = context.TLCSV_WhseTransferDetail.Where(x => x.TLCSVWHTD_WhseTranfer_FK == Transfer.TLCSVWHT_Pk).FirstOrDefault();
                    if (Trns != null)
                        continue;

                    context.TLCSV_WhseTransfer.Where(x => x.TLCSVWHT_Pk == Transfer.TLCSVWHT_Pk).Delete();
                }
                

                //=====================================================================
                // End of Customer Service Clear Down 
                //=======================================================
               

                /*
                //================================================================
                // 2nd Step is to clear down the CMT Recrds 
                //========================================================================
                pBar1.Visible = true;
                // Display the ProgressBar control.
                pBar1.Visible = true;
                // Set Minimum to 1 
                pBar1.Minimum = 1;
                // Set the initial value of ProgessBar
                pBar1.Value = 1;
                // Set the Step property to a value of 1 to represent PPS Record processed
                pBar1.Step = 1;

                textBox1.Text = "Commencing with CMT ClearDown";

                var MarkDeletedGrouped = context.TLCMT_CompletedWork.Where(x => x.TLCMTWC_MarkedForDeletion && x.TLCMTWC_Despatched).GroupBy(x => x.TLCMTWC_LineIssue_FK);
                pBar1.Maximum = MarkDeletedGrouped.Count();

                foreach (var Grp in MarkDeletedGrouped)
                {
                    int NoRecords = Grp.Count();
                    int NoMarked = Grp.Where(x => x.TLCMTWC_MarkedForDeletion).Count();
                    if (NoRecords == NoMarked)
                    {
                        var LineIssue_Pk = Grp.FirstOrDefault().TLCMTWC_LineIssue_FK;
                        var LineIssueCS_Pk = Grp.FirstOrDefault().TLCMTWC_CutSheet_FK;

                        // Remove records from NonCompliance 
                        //=======================================================
                        context.TLCMT_NonCompliance.Where(x => x.CMTNCD_CutSheet_Fk == LineIssueCS_Pk).Delete();

                        // Remove Records from AuditMeasure 
                        //===========================================================
                        context.TLCMT_AuditMeasureRecorded.Where(x => x.TLBFAR_CutSheet_FK == LineIssueCS_Pk).Delete();

                        // Remove Record from Production Faults
                        //==========================================================
                        context.TLCMT_ProductionFaults.Where(x => x.TLCMTPF_PanelIssue_FK == LineIssueCS_Pk).Delete();


                        // Remove the records from the main completed work analysis
                        //===========================================================
                        context.TLCMT_CompletedWork.Where(x => x.TLCMTWC_MarkedForDeletion).Delete();


                        // Only Mark the Header record ready for deletion if all child records  
                        //==============================================================
                        if (context.TLCMT_CompletedWork.Where(x => x.TLCMTWC_CutSheet_FK == LineIssueCS_Pk).Count() == 0)
                        {
                            //Mark for Deletion the main  record
                            //=================================================
                            var LI = context.TLCMT_LineIssue.Find(LineIssue_Pk);
                            if (LI != null)
                                LI.TLCMTLI_MarkedForDeltion = true;
                        }
                    }
                    pBar1.PerformStep();
                }
                try
                {
                    context.SaveChanges();
                }
                catch (Exception ex)
                {

                }

                //========================================================================
                // 3rd Step Clear down the Cutting Information 
                //========================================================================
                pBar1.Visible = true;
                // Display the ProgressBar control.
                pBar1.Visible = true;
                // Set Minimum to 1 
                pBar1.Minimum = 1;
                // Set the initial value of ProgessBar
                pBar1.Value = 1;
                // Set the Step property to a value of 1 to represent PPS Record processed
                pBar1.Step = 1;
                textBox1.Text = "Commencing with Cutting ClearDown";

                var MarkedForDel = context.TLCMT_LineIssue.Where(x => x.TLCMTLI_MarkedForDeltion).ToList();
                pBar1.Maximum = MarkedForDel.Count;

                foreach (var Marked in MarkedForDel)
                {
                    // 1st Step find the CutSheet
                    var CutSheet = context.TLCUT_CutSheet.Find(Marked.TLCMTLI_CutSheet_FK);
                    if (CutSheet != null)
                    {
                        // Delete from TLCut Trims
                        //========================================
                        context.TLCUT_TrimsOnCut.Where(x => x.TLCUTTOC_CutSheet_FK == CutSheet.TLCutSH_Pk).Delete();

                        var CutSheetReceipt = context.TLCUT_CutSheetReceipt.Where(x => x.TLCUTSHR_CustSheet_FK == CutSheet.TLCutSH_Pk).FirstOrDefault();
                        if (CutSheetReceipt != null)
                        {
                            // Delete from TLCut Receipt boxes 
                            //=======================================
                            context.TLCUT_CutSheetReceiptBoxes.Where(x => x.TLCUTSHB_CutSheet_FK == CutSheetReceipt.TLCUTSHR_Pk).Delete();

                            // Delete from TLCut Expected Units 
                            //=======================================
                            context.TLCUT_ExpectedUnits.Where(x => x.TLCUTE_CutSheet_FK == CutSheetReceipt.TLCUTSHR_Pk).Delete();

                            // Delete from TLCut QC Berrie 
                            //=======================================
                            context.TLCUT_QCBerrie.Where(x => x.TLQCFB_CutSheetReceipt_FK == CutSheetReceipt.TLCUTSHR_Pk).Delete();

                            var CutSheetReceiptDetail = context.TLCUT_CutSheetReceiptDetail.Where(x => x.TLCUTSHRD_CutSheet_FK == CutSheetReceipt.TLCUTSHR_Pk).FirstOrDefault();
                            if (CutSheetReceiptDetail != null)
                            {
                                // QaA Results 
                                //===========================================
                                context.TLCUT_QAResults.Where(x => x.TLCUTQA_Bundle_FK == CutSheetReceiptDetail.TLCUTSHRD_Pk).Delete();
                            }


                            // Remove Receipt Detail Records
                            //=============================================
                            context.TLCUT_CutSheetReceiptDetail.Where(x => x.TLCUTSHRD_CutSheet_FK == CutSheetReceipt.TLCUTSHR_Pk).Delete();

                            // Need to remove the Panel Issue Records in Panel Issue
                            //======================================================
                            context.TLCMT_PanelIssueDetail.Where(x => x.CMTPID_CutSheet_FK == CutSheetReceipt.TLCUTSHR_Pk).Delete();

                            //Remove Receipt Master 
                            //===================================================
                            context.TLCUT_CutSheetReceipt.Remove(CutSheetReceipt);

                        }

                        context.TLCUT_CutSheetDetail.Where(x => x.TLCutSHD_CutSheet_FK == CutSheet.TLCutSH_Pk).Delete();
                        CutSheet.TLCUTSH_MarkedForDeletion = true;

                        pBar1.PerformStep();

                }



                //Remove all CMT Line Issues
                //==========================================================================
                context.TLCMT_LineIssue.Where(x => x.TLCMTLI_MarkedForDeltion).Delete();

                var PanelIssues = context.TLCMT_PanelIssue.ToList();
                foreach (var PanelIssue in PanelIssues)
                {
                        if (context.TLCMT_PanelIssueDetail.Where(x => x.CMTPID_PI_FK == PanelIssue.CMTPI_Pk).Count() == 0)
                        {
                            var PI = context.TLCMT_PanelIssue.Find(PanelIssue.CMTPI_Pk);
                            if (PI != null)
                                context.TLCMT_PanelIssue.Remove(PI);
                        }

                }

                try
                {
                     context.SaveChanges();
                }
                catch (Exception ex)
                {

                }

                //=============================================================================
                // 4th step Clear Down DyeHouse 
                //========================================================================
                    IList<TLDYE_DyeOrder> DOOrder = null;
                    pBar1.Visible = true;
                    // Display the ProgressBar control.
                    pBar1.Visible = true;
                    // Set Minimum to 1 
                    pBar1.Minimum = 1;
                    // Set the initial value of ProgessBar
                    pBar1.Value = 1;
                    // Set the Step property to a value of 1 to represent PPS Record processed
                    pBar1.Step = 1;

                    textBox1.Text = "Commencing with DyeHouse ClearDown";

                    var CSMarked = context.TLCUT_CutSheet.Where(x => x.TLCUTSH_MarkedForDeletion).ToList();
                    pBar1.Maximum = CSMarked.Count;

                    foreach (var CSMark in CSMarked)
                    {
                        bool BatchDelete = true;

                        //=======================================
                        // 1st find Dye Batch 
                        //=================================================
                        var DyeBatch = context.TLDYE_DyeBatch.Find(CSMark.TLCutSH_DyeBatch_FK);
                        if (DyeBatch != null)
                        {
                            // Now find the details
                            //===============================
                            var DyeBatchDetails = context.TLDYE_DyeBatchDetails.Where(x => x.DYEBD_DyeBatch_FK == DyeBatch.DYEB_Pk).ToList();
                            var Cnt = DyeBatchDetails.Count();
                            if (Cnt != 0)
                            {
                                var ToCSheet = DyeBatchDetails.Where(x => x.DYEBO_CutSheet).Count();
                                if (Cnt == ToCSheet)
                                {
                                    // All the pieces in this DyeBatch have been used and therefore be deleted
                                    //=========================================================================
                                    context.TLDYE_DyeBatchDetails.Where(x => x.DYEBD_DyeBatch_FK == DyeBatch.DYEB_Pk).Delete();
                                }
                                else
                                {
                                    //We only delete those pieces that have been sent to cutting
                                    //==========================================================
                                    BatchDelete = false;
                                    context.TLDYE_DyeBatchDetails.Where(x => x.DYEBD_DyeBatch_FK == DyeBatch.DYEB_Pk && x.DYEBO_CutSheet).Delete();

                                }
                            }

                            if (BatchDelete)
                            {
                                var index = DOOrder.Where(x => x.TLDYO_Pk == DyeBatch.DYEB_DyeOrder_FK).FirstOrDefault();
                                if (index == null)
                                {
                                    var Do = context.TLDYE_DyeOrder.Find(DyeBatch.DYEB_DyeOrder_FK);
                                    if (Do == null)
                                        DOOrder.Add(Do);
                                }
                                context.TLDYE_DyeBatch.Remove(DyeBatch);
                            }
                        }

                        // Now Delete from the Cut Sheet file 
                        //=======================================================
                        var CS = context.TLCUT_CutSheet.Find(CSMark.TLCutSH_Pk);
                        if (CS != null && BatchDelete)
                        {
                            //Before we do all the measurements and non-compliance
                            //=====================================================
                            context.TLDYE_DyeTransactions.RemoveRange(context.TLDYE_DyeTransactions.Where(x => x.TLDYET_Batch_FK == DyeBatch.DYEB_Pk));

                            context.TLDYE_NonCompliance.RemoveRange(context.TLDYE_NonCompliance.Where(x => x.TLDYE_NcrBatchNo_FK == DyeBatch.DYEB_Pk));
                            context.TLDYE_NonComplianceAnalysis.RemoveRange(context.TLDYE_NonComplianceAnalysis.Where(x => x.TLDYEDC_BatchNo == DyeBatch.DYEB_Pk));
                            context.TLDYE_NonComplianceDetail.RemoveRange(context.TLDYE_NonComplianceDetail.Where(x => x.DYENCRD_BatchNo_Fk == DyeBatch.DYEB_Pk));
                            context.TLDYE_NonComplianceConsDetail.RemoveRange(context.TLDYE_NonComplianceConsDetail.Where(x => x.DYENCCON_BatchNo_FK == DyeBatch.DYEB_Pk));

                            context.TLDye_QualityException.RemoveRange(context.TLDye_QualityException.Where(x => x.TLDyeIns_DyeBatch_Fk == DyeBatch.DYEB_Pk));

                            context.TLCUT_CutSheet.Remove(CS);
                        }

                    }

                    //======================================
                    // we need Delete Dye Orders of DyeBatches Deleted but only if there are no DyeBatch attached 
                    //====================================================
                    foreach (var Do in DOOrder)
                    {
                        var Cnt = context.TLDYE_DyeBatch.Where(x => x.DYEB_DyeOrder_FK == Do.TLDYO_Pk).Count();
                        if (Cnt == 0)
                        {
                            var DyeO = context.TLDYE_DyeOrder.Find(Do.TLDYO_Pk);
                            if (DyeO != null)
                            {
                                context.TLDYE_DyeOrderDetails.Where(x => x.TLDYOD_DyeOrder_Fk == Do.TLDYO_Pk).Delete();
                                context.TLDYE_DyeOrder.Remove(DyeO);
                            }
                        }
                    }

                    try
                    {
                        context.SaveChanges();
                    }
                    catch (Exception ex)
                    {

                    }
                    // 5th step Clear Knitting - We achieve this by looping throu DOrder
                    // Have decided to leave Bought in Fabric for the moment
                    //========================================================================
                    pBar1.Visible = true;
                    // Display the ProgressBar control.
                    pBar1.Visible = true;
                    // Set Minimum to 1 
                    pBar1.Minimum = 1;
                    // Set the initial value of ProgessBar
                    pBar1.Value = 1;
                    // Set the Step property to a value of 1 to represent PPS Record processed
                    pBar1.Step = 1;

                    textBox1.Text = "Commencing with Knitting ClearDown";

                    var KoOrders = context.TLKNI_Order.Where(x => x.KnitO_Closed && x.KnitO_ClosedDate <= DSelected).ToList();
                    foreach (var KoOrder in KoOrders)
                    {
                        var GProd = context.TLKNI_GreigeProduction.Where(x => x.GreigeP_KnitO_Fk == KoOrder.KnitO_Pk && !x.GreigeP_BoughtIn).ToList();
                        int NoDyed = GProd.Where(x => x.GreigeP_Dye).Count();
                        if (NoDyed == GProd.Count)
                        {
                            // We only want to delete batches where all the Production has been sent to dyeing
                            //================================================================================
                            // But before we do that 
                            //==============================================
                            var YarnAllocs = context.TLKNI_YarnAllocTransctions.Where(x => x.TLKYT_KnitOrder_FK == KoOrder.KnitO_Pk).ToList();
                            foreach (var YarnAlloc in YarnAllocs)
                            {
                                var Pallet = context.TLKNI_YarnOrderPallets.Find(YarnAlloc.TLKYT_YOP_FK);
                                if (Pallet != null && Pallet.TLKNIOP_PalletAllocated)
                                {
                                    context.TLKNI_YarnOrderPallets.Remove(Pallet);
                                }
                            }

                            context.TLKNI_GreigeProduction.Where(x => x.GreigeP_KnitO_Fk == KoOrder.KnitO_Pk).Delete();

                            var Ko = context.TLKNI_Order.Find(KoOrder.KnitO_Pk);
                            if (Ko != null)
                            {
                                context.TLKNI_Order.Remove(Ko);
                            }
                        }

                        context.TLKNI_GreigeTransactions.Where(x => x.GreigeT_KOrder_FK == KoOrder.KnitO_Pk).Delete();
                    }

                    //3rd Party 
                    //=========================================================
                    var YarnTS = context.TLKNI_YarnTransaction.Where(x => x.KnitY_TransactionDate <= DSelected).ToList();
                    foreach (var YarnT in YarnTS)
                    {
                        context.TLKNI_YarnTransactionDetails.Where(x => x.KnitYD_KnitY_FK == YarnT.KnitY_Pk).Delete();

                    }
                    context.TLKNI_GreigeCommissionTransctions.Where(x => x.GreigeCom_Transdate <= DSelected).Delete();
                }

                // 6th step Clear Spinning
                //========================================================================

                pBar1.Visible = true;
                // Display the ProgressBar control.
                pBar1.Visible = true;
                // Set Minimum to 1 
                pBar1.Minimum = 1;
                // Set the initial value of ProgessBar
                pBar1.Value = 1;
                // Set the Step property to a value of 1 to represent PPS Record processed
                pBar1.Step = 1;
                textBox1.Text = "Commencing with Spinning ClearDown";
                
                // 6th step 1st Step cotton received transactions
                //======================================================================

                var CottonTrans = context.TLSPN_CottonTransactions.Where(x => x.cotrx_TransDate <= DSelected).ToList();

                foreach (var CottonTran in CottonTrans)
                {
                    context.TLSPN_CottonReceivedBales.Where(x => x.CotBales_LotNo == CottonTran.cotrx_LotNo).Delete();
                    context.TLSPN_YarnOrderLayDown.Where(x => x.YarnLD_LotNo == CottonTran.cotrx_LotNo).Delete();

                    var CT = context.TLSPN_CottonTransactions.Find(CottonTran.cotrx_pk);
                    if (CT != null)
                        context.TLSPN_CottonTransactions.Remove(CT);
                }

                var YarnOrders = context.TLSPN_YarnOrder.Where(x => x.Yarno_Closed && x.YarnO_DelDate <= DSelected).ToList();
                foreach (var YarnOrder in YarnOrders)
                {
                    context.TLSPN_YarnOrderPallets.RemoveRange(context.TLSPN_YarnOrderPallets.Where(x => x.YarnOP_YarnOrder_FK == YarnOrder.YarnO_Pk));
                    context.TLSPN_YarnTransactions.RemoveRange(context.TLSPN_YarnTransactions.Where(x => x.YarnTrx_YarnOrder_FK == YarnOrder.YarnO_Pk));

                    var YO = context.TLSPN_YarnOrder.Find(YarnOrder.YarnO_Pk);
                    if (YO != null)
                        context.TLSPN_YarnOrder.Remove(YO);
                }
                 * */

            }               
            
        }

      
        
        
        // Use Context TTI2
    }
}
