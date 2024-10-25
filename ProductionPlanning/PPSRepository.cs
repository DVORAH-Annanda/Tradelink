﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;
using LinqKit;
using System.Data;
using System.Security.Cryptography;

namespace ProductionPlanning
{
   public class PPSRepository : IDisposable
   {
        protected readonly TTI2Entities _context;

        public PPSRepository()
        {
                _context = new TTI2Entities();
        }

        public TLADM_Departments LoadDepartment(int Pk)
        {
            return _context.TLADM_Departments.FirstOrDefault(s => s.Dep_Id == Pk);
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

          public TLADM_Griege LoadGreige(int Pk)
          {
              return _context.TLADM_Griege.FirstOrDefault(s => s.TLGreige_Id == Pk);
          }

          public TLADM_GreigeQuality LoadGreigeQual(int Pk)
          {
              return _context.TLADM_GreigeQuality.FirstOrDefault(x => x.GQ_Pk == Pk);
          }

          public TLADM_MachineDefinitions LoadMachine(int Pk)
          {
              return _context.TLADM_MachineDefinitions.FirstOrDefault(x => x.MD_Pk == Pk);
          }

         public void Dispose()
         {
               if (_context != null)
                {
                    _context.Dispose();
                }
         }

        public IQueryable<TLCUT_CutSheet> CSProcessLossAcrossProd(ProdQueryParameters parameters)
        {
            IQueryable<TLCUT_CutSheet> CutSheets = null;
            CutSheets = _context.TLCUT_CutSheet.Where(x => x.TLCUTSH_Completed_Date >= parameters.FromDate && x.TLCUTSH_Completed_Date <= parameters.ToDate && x.TLCutSH_WIPComplete).AsQueryable();

            if (parameters.Styles.Count() != 0)
            {
                var StylePredicate = PredicateBuilder.New<TLCUT_CutSheet>();
                foreach (var Style in parameters.Styles)
                {
                    var temp = Style;
                    StylePredicate = StylePredicate.Or(s => s.TLCutSH_Styles_FK == temp.Sty_Id);
                }
                CutSheets = CutSheets.AsExpandable().Where(StylePredicate);
            }

            if (parameters.Colours.Count() != 0)
            {
                var ColourPredicate = PredicateBuilder.New<TLCUT_CutSheet>();
                foreach (var Style in parameters.Colours)
                {
                    var temp = Style;
                    ColourPredicate = ColourPredicate.Or(s => s.TLCutSH_Colour_FK == temp.Col_Id);
                }
                CutSheets = CutSheets.AsExpandable().Where(ColourPredicate);
            }

            if (parameters.Sizes.Count() != 0)
            {
                var SizePredicate = PredicateBuilder.New<TLCUT_CutSheet>();
                foreach (var Style in parameters.Sizes)
                {
                    var temp = Style;
                    SizePredicate = SizePredicate.Or(s => s.TLCutSH_Size_FK == temp.SI_id);
                }
                CutSheets = CutSheets.AsExpandable().Where(SizePredicate);
            }

            if (parameters.Greiges.Count() != 0)
            {
                var QualityPredicate = PredicateBuilder.New<TLCUT_CutSheet>();
                foreach (var Style in parameters.Greiges)
                {
                    var temp = Style;
                    QualityPredicate = QualityPredicate.Or(s => s.TLCutSH_Quality_FK == temp.TLGreige_Id);
                }
                CutSheets = CutSheets.AsExpandable().Where(QualityPredicate);
            }

            return CutSheets;
        }
         public IQueryable<TLADM_Griege> GreigeQuery(ProdQueryParameters parameters)
         {
            IQueryable<TLADM_Griege> GreigeItems = null;

            GreigeItems = _context.TLADM_Griege.OrderBy(x => x.TLGreige_Description).AsQueryable();

            if (parameters.ExcludeDiscontinued)
            {
                GreigeItems = GreigeItems.Where(x => !(bool)x.TLGriege_Discontinued).AsQueryable();
            }
            if (parameters.Greiges.Count > 0)
             {
                 var GreigePredicate = PredicateBuilder.New<TLADM_Griege>();
                 foreach (var Greige in parameters.Greiges)
                 {
                     var temp = Greige;
                     GreigePredicate = GreigePredicate.Or(s => s.TLGreige_Id == temp.TLGreige_Id);
                 }

                 GreigeItems = GreigeItems.AsExpandable().Where(GreigePredicate);
             }

             if (parameters.GreigeQualities.Count > 0)
             {
                 var GreigeQualityPredicate = PredicateBuilder.New<TLADM_Griege>();
                 foreach (var Greige in parameters.GreigeQualities)
                 {
                     var temp = Greige;
                     GreigeQualityPredicate = GreigeQualityPredicate.Or(s => s.TLGreige_Quality_FK == temp.GQ_Pk);
                 }

                 GreigeItems = GreigeItems.AsExpandable().Where(GreigeQualityPredicate);
             }

             if (parameters.KnitMachines.Count > 0)
             {
                 var KnitMachinePredicate = PredicateBuilder.New<TLADM_Griege>();
                 foreach (var Machine in parameters.KnitMachines)
                 {
                     var temp = Machine;
                     KnitMachinePredicate = KnitMachinePredicate.Or(s => s.TLGreige_Machine_FK == temp.MD_Pk);
                 }

                 GreigeItems = GreigeItems.AsExpandable().Where(KnitMachinePredicate);
             }

             return GreigeItems;
         }

        // var DyeBatchDetail = context.TLDYE_DyeBatchDetails.Where(x => x.DYEBO_QAApproved && x.DYEBO_TransDate >= _ProdQParms.FromDate && x.DYEBO_TransDate <= _ProdQParms.ToDate && x.DYEBO_DiskWeight != 0).ToList();
        public IQueryable<TLDYE_DyeBatchDetails> SelectDskInfo(ProdQueryParameters QueryParms)
        {
            var DyeBatchDet = _context.TLDYE_DyeBatchDetails.Where(x => x.DYEBO_QAApproved && x.DYEBO_TransDate >= QueryParms.FromDate && x.DYEBO_TransDate <= QueryParms.ToDate && x.DYEBO_DiskWeight != 0).AsQueryable();

            if(QueryParms.Greiges.Count != 0)
            {
                var DyeBatchPredicate = PredicateBuilder.New<TLDYE_DyeBatchDetails>();
                foreach (var Qual in QueryParms.Greiges)
                {
                    var temp = Qual;
                    DyeBatchPredicate = DyeBatchPredicate.Or(s => s.DYEBD_QualityKey == temp.TLGreige_Id);
                }

                DyeBatchDet = DyeBatchDet.AsExpandable().Where(DyeBatchPredicate);
            }

            return DyeBatchDet;
        }
        public IQueryable<TLADM_CustomerFile> PPSCustomerOrders()
         {
             var Customers = (from CTS in _context.TLADM_CustomerFile
                             join PO in _context.TLCSV_PurchaseOrder on CTS.Cust_Pk equals PO.TLCSVPO_Customer_FK
                             where !PO.TLCSVPO_Closeed
                             select CTS).GroupBy(x=>x.Cust_Pk).FirstOrDefault().AsQueryable();

             return Customers;
         }

         public IQueryable<TLCSV_PurchaseOrder>PPSCustomers(ProdQueryParameters parameters)
         {
             var PO = _context.TLCSV_PurchaseOrder.Where(x => !x.TLCSVPO_Closeed).AsQueryable();
             if (parameters.Customers.Count() != 0)
             {
                 var CustomerPredicate = PredicateBuilder.New<TLCSV_PurchaseOrder>();
                 foreach (var Customer in parameters.Customers)
                 {
                     var temp = Customer;
                     CustomerPredicate = CustomerPredicate.Or(s => s.TLCSVPO_Customer_FK  == temp.Cust_Pk);
                 }
                 PO = PO.AsExpandable().Where(CustomerPredicate);
             }
             return PO;
         }

         public IQueryable<TLCSV_StockOnHand> PPSSales(ProdQueryParameters parameters)
         {
             var SOH = (from soh in _context.TLCSV_StockOnHand
                        join szs in _context.TLADM_Sizes on soh.TLSOH_Size_FK equals szs.SI_id
                        where soh.TLSOH_Sold && !soh.TLSOH_Returned && soh.TLSOH_SoldDate >= parameters.FromDate && soh.TLSOH_SoldDate <= parameters.ToDate
                        orderby szs.SI_DisplayOrder
                        select soh).AsQueryable();

             /*
             if (parameters.Styles.Count() != 0)
             {
                 var StylePredicate = PredicateBuilder.New<TLCSV_StockOnHand>();
                 foreach (var Style in parameters.Styles)
                 {
                     var temp = Style;
                     StylePredicate = StylePredicate.Or(s => s.TLSOH_Style_FK == temp.Sty_Id);
                 }
                 SOH = SOH.AsExpandable().Where(StylePredicate);
             }

             
             if (parameters.Colours.Count() != 0)
             {
                 var ColourPredicate = PredicateBuilder.New<TLCSV_StockOnHand>();
                 foreach (var Colour in parameters.Colours)
                 {
                     var temp = Colour;
                     ColourPredicate = ColourPredicate.Or(s => s.TYLSOH_Colour_FK == temp.Col_Id);
                 }
                 SOH = SOH.AsExpandable().Where(ColourPredicate);
             }

             if (parameters.Sizes.Count() != 0)
             {
                 var SizePredicate = PredicateBuilder.New<TLCSV_StockOnHand>();
                 foreach (var Size in parameters.Sizes)
                 {
                     var temp = Size;
                     SizePredicate = SizePredicate.Or(s => s.TLSOH_Size_FK == temp.SI_id);
                 }
                 SOH = SOH.AsExpandable().Where(SizePredicate);
             }
              */


             return SOH;
         }
         public IQueryable<TLPPS_Replenishment> PPSQItemStatus(ProdQueryParameters parameters)
         {
             var IS = _context.TLPPS_Replenishment.AsQueryable();
             if (parameters.Styles.Count() > 0)
             {
                 var stylePredicate = PredicateBuilder.New<TLPPS_Replenishment>();
                 foreach (var style in parameters.Styles)
                 {
                     var temp = style;
                     stylePredicate = stylePredicate.Or(s => s.TLREP_Style_FK == temp.Sty_Id);
                 }

                 IS = IS.AsExpandable().Where(stylePredicate);
             }

             if (parameters.Colours.Count() > 0)
             {
                 var colourPredicate = PredicateBuilder.New<TLPPS_Replenishment>();
                 foreach (var style in parameters.Colours)
                 {
                     var temp = style;
                     colourPredicate = colourPredicate.Or(s => s.TLREP_Colour_FK == temp.Col_Id);
                 }

                 IS = IS.AsExpandable().Where(colourPredicate);
             }

             if (parameters.Sizes.Count() > 0)
             {
                 var sizePredicate = PredicateBuilder.New<TLPPS_Replenishment>();
                 foreach (var style in parameters.Sizes)
                 {
                     var temp = style;
                     sizePredicate = sizePredicate.Or(s => s.TLREP_Size_FK == temp.SI_id);
                 }

                 IS = IS.AsExpandable().Where(sizePredicate);
             }

             return IS;
         }

        public IQueryable<TLCMT_LineIssue> CMTPanels(ProdQueryParameters parameters)
        {
            _context.Configuration.AutoDetectChangesEnabled = false;

            // This particular function has been changed To call for Panels in the CMT Panel Receipt Store  
            var LineIssues = _context.TLCMT_LineIssue.Where(x => !x.TLCMTLI_WorkCompleted && !x.TLCMTLI_IssuedToLine).AsQueryable();

            // Filter out any particular CMT if neccessary
            //------------------------------------------------------------
            if (parameters.Departments.Count > 0)
            {
                var DepartmentPredicate = PredicateBuilder.New<TLCMT_LineIssue>();
                foreach (var Dept in parameters.Departments)
                {
                    var temp = Dept;
                    DepartmentPredicate = DepartmentPredicate.Or(s => s.TLCMTLI_CmtFacility_FK == temp.Dep_Id);
                }

                LineIssues = LineIssues.AsExpandable().Where(DepartmentPredicate);
            }

            return LineIssues;
        }

        public IQueryable<TLCMT_LineIssue> CMTWIPLineIssues(ProdQueryParameters parameters)
        {
            _context.Configuration.AutoDetectChangesEnabled = false;

            // This particular function has been changed To call for Panels in the CMT Panel Receipt Store  
            var LineIssues = _context.TLCMT_LineIssue.Where(x => !x.TLCMTLI_WorkCompleted && x.TLCMTLI_IssuedToLine).AsQueryable();

            // Filter out any particular CMT if neccessary
            //------------------------------------------------------------
            if (parameters.Departments.Count > 0)
            {
                var DepartmentPredicate = PredicateBuilder.New<TLCMT_LineIssue>();
                foreach (var Dept in parameters.Departments)
                {
                    var temp = Dept;
                    DepartmentPredicate = DepartmentPredicate.Or(s => s.TLCMTLI_CmtFacility_FK == temp.Dep_Id);
                }

                LineIssues = LineIssues.AsExpandable().Where(DepartmentPredicate);
            }

            return LineIssues;
        }

        public IQueryable<TLCMT_LineIssue> CMTDespatchLineIssues(ProdQueryParameters parameters)
        {
            var LineIssuesCMTDespatch = (from T1 in _context.TLCMT_LineIssue
                                join T2 in _context.TLCMT_CompletedWork on T1.TLCMTLI_Pk equals T2.TLCMTWC_LineIssue_FK
                                where T1.TLCMTLI_WorkCompleted && !T2.TLCMTWC_Despatched
                                select T1).Distinct().AsQueryable();

            // Filter out any particular CMT if neccessary
            //------------------------------------------------------------
            if (parameters.Departments.Count > 0)
            {
                var DepartmentPredicate = PredicateBuilder.New<TLCMT_LineIssue>();
                foreach (var Dept in parameters.Departments)
                {
                    var temp = Dept;
                    DepartmentPredicate = DepartmentPredicate.Or(s => s.TLCMTLI_CmtFacility_FK == temp.Dep_Id);
                }

                LineIssuesCMTDespatch = LineIssuesCMTDespatch.AsExpandable().Where(DepartmentPredicate);
            }

            return LineIssuesCMTDespatch;
        }


        public IQueryable<TLPPS_Replenishment> PPSQuery(ProdQueryParameters parameters)
         {
             var PPSSortOrder = from T1 in _context.TLPPS_Replenishment
                            join T2 in _context.TLADM_Styles on T1.TLREP_Style_FK equals T2.Sty_Id
                            join T3 in _context.TLADM_Colours on T1.TLREP_Colour_FK equals T3.Col_Id
                            join T4 in _context.TLADM_Sizes on T1.TLREP_Size_FK equals T4.SI_id
                            orderby T2.Sty_Description , T3.Col_Display , T4.SI_DisplayOrder 
                            select T1;
                         
             var PPS = PPSSortOrder.Where(x=>(bool)x.TLREP_Discontinued != true).AsQueryable();

             if (parameters.Styles.Count() > 0)
             {
                 var stylePredicate = PredicateBuilder.New<TLPPS_Replenishment>();
                 foreach (var style in parameters.Styles)
                 {
                     var temp = style;
                     stylePredicate = stylePredicate.Or(s => s.TLREP_Style_FK == temp.Sty_Id);
                 }

                 PPS = PPS.AsExpandable().Where(stylePredicate);
             }

             if (parameters.Colours.Count() > 0)
             {
                var colourPredicate = PredicateBuilder.New<TLPPS_Replenishment>();
                foreach (var style in parameters.Colours)
                {
                        var temp = style;
                        colourPredicate = colourPredicate.Or(s => s.TLREP_Colour_FK == temp.Col_Id);
                }

                 PPS = PPS.AsExpandable().Where(colourPredicate);
             }

             if (parameters.Sizes.Count() > 0)
             {
                    var sizePredicate = PredicateBuilder.New<TLPPS_Replenishment>();
                    foreach (var style in parameters.Sizes)
                    {
                        var temp = style;
                        sizePredicate = sizePredicate.Or(s => s.TLREP_Size_FK == temp.SI_id);
                    }

                    PPS = PPS.AsExpandable().Where(sizePredicate);
             }

             return PPS;
         }
    }
   
    public class ProdQueryParameters
    {
        public List<TLADM_CustomerFile> Customers;
        public List<TLADM_Departments> Departments;
        public List<TLADM_Sizes> Sizes;
        public List<TLADM_Colours> Colours;
        public List<TLADM_Styles> Styles;        
        public List<TLADM_Griege> Greiges;
        public List<TLADM_GreigeQuality> GreigeQualities;
        public List<TLADM_MachineDefinitions> KnitMachines;
        public DateTime FromDate;
        public DateTime ToDate;
        public int TopSellers;
        public bool[] QAReportingDepts;
        public bool IncludeGradeAWithwarnings;
        public int GradeType;
        public bool ExcludeDiscontinued;
        public bool[] SelectedOptions;
        public int[] RecordKeys;
        public int InterDeptOption;

        public ProdQueryParameters()
        {
            Customers = new List<TLADM_CustomerFile>();
            Departments = new List<TLADM_Departments>();
            Sizes = new List<TLADM_Sizes>();
            Colours = new List<TLADM_Colours>();
            Styles = new List<TLADM_Styles>();            
            Greiges = new List<TLADM_Griege>();
            GreigeQualities = new List<TLADM_GreigeQuality>();
            KnitMachines = new List<TLADM_MachineDefinitions>();
            FromDate = new DateTime();
            ToDate = new DateTime();
            TopSellers = 0;
            QAReportingDepts = new bool[4] { false, false, false, false };
            IncludeGradeAWithwarnings = false;
            GradeType = 0;
            ExcludeDiscontinued = false;
            SelectedOptions = new bool[4] { false, false, false, false};
            RecordKeys = new int[4];
            InterDeptOption = 0;
        }

    }
}
