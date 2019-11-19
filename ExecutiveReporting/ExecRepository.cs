using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;
using LinqKit;

namespace ExecutiveReporting
{
   public class ExecRepository : IDisposable
   {
        protected readonly TTI2Entities _context;

        public ExecRepository()
        {
                _context = new TTI2Entities();
        }

         public TLADM_Colours LoadColour(int Pk)
         {
                return _context.TLADM_Colours.FirstOrDefault(s => s.Col_Id == Pk);
         }

         public TLADM_Sizes LoadSize(int Pk)
         {
                return _context.TLADM_Sizes.FirstOrDefault(s => s.SI_id == Pk);
         }

          public TLADM_Styles LoadStyle(int Pk)
         {
                return _context.TLADM_Styles.FirstOrDefault(s => s.Sty_Id == Pk);
         }

         public TLADM_CustomerFile LoadCustomer(int Pk)
         {
              return _context.TLADM_CustomerFile.FirstOrDefault(s => s.Cust_Pk == Pk);
         }

         public TLADM_MachineDefinitions LoadMachine(int Pk)
         {
              return _context.TLADM_MachineDefinitions.FirstOrDefault(x => x.MD_Pk == Pk);
         }

         public IQueryable<TLSPN_YarnOrderPallets> ExecYarnProduction(ExecQueryParameters parameters)
         {
              return _context.TLSPN_YarnOrderPallets.Where(x=>x.YarnOP_DatePacked>= parameters.FromDate && x.YarnOP_DatePacked <= parameters.ToDate).AsQueryable();
         }

         public IQueryable<TLKNI_GreigeProduction> ExecGreigeProduction(ExecQueryParameters parameters)
         {
             return _context.TLKNI_GreigeProduction.Where(x=>x.GreigeP_PDate >= parameters.FromDate && x.GreigeP_PDate <= parameters.ToDate).AsQueryable();
         }

         public IQueryable<TLDYE_DyeTransactions> ExecDyeNotFinshed(ExecQueryParameters parameters)
         {
             // join with a query expression 
             //===========================================
             var result = (from t in _context.TLDYE_DyeBatch
                           join x in _context.TLDYE_DyeTransactions on t.DYEB_Pk equals x.TLDYET_Batch_FK
                           where x.TLDYET_Date >= parameters.FromDate && x.TLDYET_Date <= parameters.ToDate && x.TLDYET_Stage == 3
                           select x ).AsQueryable();

             return result;
         }

         public IQueryable<TLDYE_DyeBatchDetails> ExecDyeIntoQuarantine(ExecQueryParameters parameters)
         {
             var result = (from t in _context.TLDYE_DyeBatch
                          join x in _context.TLDYE_DyeBatchDetails on t.DYEB_Pk equals x.DYEBD_DyeBatch_FK
                          where x.DYEBO_QAApproved && x.DYEBO_ApprovalDate >= parameters.FromDate && x.DYEBO_ApprovalDate <= parameters.ToDate && t.DYEB_OutProcess
                          select x).AsQueryable();

             return result;
         }

         public IQueryable<TLCUT_CutSheetReceiptDetail> ExecIntoPanelStore(ExecQueryParameters parameters)
         {
             var Results = (from csr in _context.TLCUT_CutSheetReceipt
                            join csrd in _context.TLCUT_CutSheetReceiptDetail on csr.TLCUTSHR_Pk equals csrd.TLCUTSHRD_CutSheet_FK
                            where csr.TLCUTSHR_DateIntoPanelStore >= parameters.FromDate && csr.TLCUTSHR_DateIntoPanelStore <= parameters.ToDate
                            select csrd).AsQueryable();
             return Results;
         }

         public IQueryable<TLCMT_LineIssue> ExecWorkCompleted(ExecQueryParameters parameters)
         {
             return _context.TLCMT_LineIssue.Where(x => x.TLCMTLI_WorkCompleted && x.TLCMTLI_WorkCompletedDate >= parameters.FromDate && x.TLCMTLI_WorkCompletedDate <= parameters.ToDate).AsQueryable();

         }

         public void Dispose()
         {
               if (_context != null)
                {
                    _context.Dispose();
                }
         }
    }
   
    public class ExecQueryParameters
    {
        public DateTime FromDate;
        public DateTime ToDate;

        public ExecQueryParameters()
        {
            FromDate = new DateTime();
            ToDate = new DateTime();
        }
    }
}
