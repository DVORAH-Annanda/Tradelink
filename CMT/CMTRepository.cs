using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;
using LinqKit;
using System.Windows.Forms;
namespace CMT
{
   public class CMTRepository : IDisposable
   {
        protected readonly TTI2Entities _context;
       
        public CMTRepository()
        {
                _context = new TTI2Entities();
        }

        public TLCUT_CutSheet LoadCutSheet(int Pk)
        {
            return _context.TLCUT_CutSheet.FirstOrDefault(s => s.TLCutSH_Pk == Pk);
        }

        public TLADM_Departments LoadDepartment(int Pk)
        {
            return _context.TLADM_Departments.FirstOrDefault(s => s.Dep_Id == Pk);
        }

        public TLADM_CustomerFile LoadCustomer(int Pk)
        {
               return _context.TLADM_CustomerFile.FirstOrDefault(s => s.Cust_Pk == Pk);
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

          public TLADM_CMTMeasurementPoints LoadMeasurePoints(int Pk)
          {
              return _context.TLADM_CMTMeasurementPoints.FirstOrDefault(s => s.CMTMP_Pk == Pk);
          }

          public TLCMT_FactConfig LoadLine(int Pk)
          {
              return _context.TLCMT_FactConfig.FirstOrDefault(s => s.TLCMTCFG_Pk == Pk);
          }

          public TLCMT_PanelIssue LoadPanelIssue(int Pk)
          {
              return _context.TLCMT_PanelIssue.FirstOrDefault(s => s.CMTPI_Pk == Pk);
          }

          public TLDYE_BIFInTransit LoadBIFTransit(int Pk)
          {
              return _context.TLDYE_BIFInTransit.FirstOrDefault(s => s.BIFT_Pk == Pk);
          }

          public TLADM_WhseStore LoadWareHouse(int Pk)
          {
              return _context.TLADM_WhseStore.FirstOrDefault(s => s.WhStore_Id  == Pk);
          }

          public TLADM_CMTNonCompliance LoadNoneCompliance(int Pk)
          {
              return _context.TLADM_CMTNonCompliance.FirstOrDefault(s => s.CMTNC_Pk == Pk);
          }

          public TLSEC_UserAccess LoadUserAceess(int Pk)
          {
              return _context.TLSEC_UserAccess.FirstOrDefault(s => s.TLSECUA_Pk == Pk);
          }

          public void Dispose()
          {
               if (_context != null)
                {
                    _context.Dispose();
                }
          }

          public IQueryable<TLCMT_PanelIssue> CMTPanelIssue(CMTQueryParameters parameters)
          {
              // var PanelIssue = _context.TLCMT_PanelIssue.Where(x=>!x.CMTPI_Closed).AsQueryable();
              var PanelIssue = _context.TLCMT_PanelIssue.AsQueryable();
              if (parameters.PanelIssue.Count > 0)
              {
                  var PIPredicate = PredicateBuilder.False<TLCMT_PanelIssue>();
                  foreach (var PI in parameters.PanelIssue)
                  {
                      PIPredicate = PIPredicate.Or(s => s.CMTPI_Pk == PI.CMTPI_Pk);
                  }

                  PanelIssue = PanelIssue.AsExpandable().Where(PIPredicate);
              }

              return PanelIssue;
          }

          public IQueryable<TLCMT_NonCompliance> CMTNonCompliance(CMTQueryParameters parameters)
          {
              IQueryable<TLCMT_NonCompliance> NonComp = null;
              if (!parameters.UseDatePicker)
              {
                  NonComp = _context.TLCMT_NonCompliance.Where(x => x.CMTNCD_Year == parameters.Year).AsQueryable();
              }
              else
              {
                NonComp = _context.TLCMT_NonCompliance.Where(x => x.CMTNCD_TransDate >= parameters.FromDate && x.CMTNCD_TransDate <= parameters.ToDate).AsQueryable();
              }
              if (parameters.Styles.Count != 0)
              {
                  var NonCompliancePredicate = PredicateBuilder.False<TLCMT_NonCompliance>();
                  foreach (var Style in parameters.Styles)
                  {
                      NonCompliancePredicate = NonCompliancePredicate.Or(s => s.CMTNCD_Style_FK == Style.Sty_Id);
                  }

                  NonComp = NonComp.AsExpandable().Where(NonCompliancePredicate);
              }

              if (parameters.Lines.Count != 0)
              {
                  var LineCompliancePredicate = PredicateBuilder.False<TLCMT_NonCompliance>();
                  foreach (var Line in parameters.Lines)
                  {
                      LineCompliancePredicate = LineCompliancePredicate.Or(s => s.CMTNCD_Style_FK == Line.TLCMTCFG_Pk);
                  }

                  NonComp = NonComp.AsExpandable().Where(LineCompliancePredicate);
              }

              if(parameters.CutSheets.Count != 0)
              {
                var CutSheetPredicate = PredicateBuilder.False<TLCMT_NonCompliance>();
                foreach (var Line in parameters.CutSheets)
                {
                    CutSheetPredicate = CutSheetPredicate.Or(s => s.CMTNCD_CutSheet_Fk == Line.TLCutSH_Pk);
                }

                NonComp = NonComp.AsExpandable().Where(CutSheetPredicate);
            }
            return NonComp;
          }

         public IQueryable<TLADM_CMTMeasurementPoints> CMTMeasurementPoints(CMTQueryParameters parameters)
         {
             var MeasurePoints = _context.TLADM_CMTMeasurementPoints.AsQueryable();

             if (parameters.MeasurementPoints.Count > 0)
             {
                 var MeasurePredicate = PredicateBuilder.False<TLADM_CMTMeasurementPoints>();
                 foreach (var Measure in parameters.MeasurementPoints)
                 {
                     MeasurePredicate = MeasurePredicate.Or(s => s.CMTMP_Pk == Measure.CMTMP_Pk);
                 }

                 MeasurePoints = MeasurePoints.AsExpandable().Where(MeasurePredicate);
             }

             return MeasurePoints;
         }

         public IQueryable<TLCMT_AuditMeasureRecorded> SelectAMrecorded(CMTQueryParameters parameters)
         {
             var AMRecorded = _context.TLCMT_AuditMeasureRecorded.AsQueryable();

             if (parameters.MeasurementPoints.Count > 0)
             {
                 var MeasurePredicate = PredicateBuilder.False<TLCMT_AuditMeasureRecorded>();
                 foreach (var Measure in parameters.MeasurementPoints)
                 {
                     MeasurePredicate = MeasurePredicate.Or(s => s.TLBFAR_AuditMeasure_FK == Measure.CMTMP_Pk);
                 }

                 AMRecorded = AMRecorded.AsExpandable().Where(MeasurePredicate);
             }

             if (parameters.Depts.Count > 0)
             {
                 var DeptPredicate = PredicateBuilder.False<TLCMT_AuditMeasureRecorded>();
                 foreach (var Dept in parameters.Depts)
                 {
                     DeptPredicate = DeptPredicate.Or(s => s.TLBFAR_Department_FK == Dept.Dep_Id);
                 }

                 AMRecorded = AMRecorded.AsExpandable().Where(DeptPredicate);
             }
             return AMRecorded;
        }

        public IQueryable<TLCMT_ProductionCosts> CMTProductionsCosts(CMTQueryParameters parameters)
         {
             var ProdCosts = _context.TLCMT_ProductionCosts.AsQueryable();

             if (parameters.Styles.Count > 0)
             {
                 var StylePredicate = PredicateBuilder.False<TLCMT_ProductionCosts>();
                 foreach (var Style in parameters.Styles)
                 {
                     StylePredicate = StylePredicate.Or(s => s.CMTP_Style_FK == Style.Sty_Id);
                 }

                 ProdCosts = ProdCosts.AsExpandable().Where(StylePredicate);
             }

             if (parameters.Depts.Count > 0)
             {
                 var DeptPredicate = PredicateBuilder.False<TLCMT_ProductionCosts>();
                 foreach (var Dept in parameters.Depts)
                 {
                     DeptPredicate = DeptPredicate.Or(s => s.CMTP_CMTFacility_FK  == Dept.Dep_Id);
                 }

                 ProdCosts = ProdCosts.AsExpandable().Where(DeptPredicate);
             }

             //-------------------------------------------------------------
             // Filter out any particular Colour if neccessary
             //------------------------------------------------------------
             if (parameters.Colours.Count > 0)
             {
                 var ColourPredicate = PredicateBuilder.False<TLCMT_ProductionCosts>();
                 foreach (var Colour in parameters.Colours)
                 {
                     var temp = Colour;
                     ColourPredicate = ColourPredicate.Or(s => s.CMTP_Colour_FK == temp.Col_Id);
                 }

                 ProdCosts = ProdCosts.AsExpandable().Where(ColourPredicate);
             }

             //------------------------------------------------------------------
             // Filter out any particular Size if neccessary
             //------------------------------------------------------------
             if (parameters.Sizes.Count > 0)
             {
                 var SizePredicate = PredicateBuilder.False<TLCMT_ProductionCosts>();
                 foreach (var Size in parameters.Sizes)
                 {
                     var temp = Size;
                     SizePredicate = SizePredicate.Or(s => s.CMTP_Size_FK == temp.SI_id);
                 }

                 ProdCosts = ProdCosts.AsExpandable().Where(SizePredicate);
             }

             return ProdCosts;
         }


         public IQueryable<TLCMT_CompletedWork> CMTCompletedWork(CMTQueryParameters parameters)
         {

             var CompletedWork = _context.TLCMT_CompletedWork.AsQueryable();

             //--------------------------------------------------------------
             // Filter out any particular CMT if neccessary
             //------------------------------------------------------------
             if (parameters.Depts.Count > 0)
             {
                 var DepartmentPredicate = PredicateBuilder.False<TLCMT_CompletedWork>();
                 foreach (var Dept in parameters.Depts)
                 {
                     var temp = Dept;
                     DepartmentPredicate = DepartmentPredicate.Or(s => s.TLCMTWC_CMTFacility_FK == temp.Dep_Id);
                 }

                 CompletedWork = CompletedWork.AsExpandable().Where(DepartmentPredicate);
             }

             if (parameters.BillingRecords)
                 CompletedWork = CompletedWork.Where(x => !x.TLCMTWC_CMTBilled).AsQueryable();

             //----------------------------------------------------------------------
             // Filter out any particular Styles if neccessary
             //------------------------------------------------------------
             if (parameters.Styles.Count > 0)
             {
                 var StylePredicate = PredicateBuilder.False<TLCMT_CompletedWork>();
                 foreach (var Style in parameters.Styles)
                 {
                     var temp = Style;
                     StylePredicate = StylePredicate.Or(s => s.TLCMTWC_Style_FK == temp.Sty_Id);
                 }

                 CompletedWork = CompletedWork.AsExpandable().Where(StylePredicate);
             }

             //-------------------------------------------------------------
             // Filter out any particular Colour if neccessary
             //------------------------------------------------------------
             if (parameters.Colours.Count > 0)
             {
                 var ColourPredicate = PredicateBuilder.False<TLCMT_CompletedWork>();
                 foreach (var Colour in parameters.Colours)
                 {
                     var temp = Colour;
                     ColourPredicate = ColourPredicate.Or(s => s.TLCMTWC_Colour_FK == temp.Col_Id);
                 }

                 CompletedWork = CompletedWork.AsExpandable().Where(ColourPredicate);
             }
             //------------------------------------------------------------------
             // Filter out any particular Size if neccessary
             //------------------------------------------------------------
             if (parameters.Sizes.Count > 0)
             {
                 var SizePredicate = PredicateBuilder.False<TLCMT_CompletedWork>();
                 foreach (var Size in parameters.Sizes)
                 {
                     var temp = Size;
                     SizePredicate = SizePredicate.Or(s => s.TLCMTWC_Size_FK == temp.SI_id);
                 }

                 CompletedWork = CompletedWork.AsExpandable().Where(SizePredicate);
             }
             return CompletedWork;
         }
         
       public IQueryable<TLCMT_CompletedWork> CMTPlanedVsActual(CMTQueryParameters parameters)
         {

             var CompletedWork = (from LI in _context.TLCMT_LineIssue
                               join WC in _context.TLCMT_CompletedWork on LI.TLCMTLI_Pk equals WC.TLCMTWC_PanelIssue_FK
                               where LI.TLCMTLI_WorkCompleted && LI.TLCMTLI_WorkCompletedDate >= parameters.FromDate && LI.TLCMTLI_WorkCompletedDate <= parameters.ToDate
                               select WC).AsQueryable();

             //----------------------------------------------------------------------
             // Filter out any particular Styles if neccessary
             //------------------------------------------------------------
             if (parameters.Styles.Count > 0)
             {
                 var StylePredicate = PredicateBuilder.False<TLCMT_CompletedWork>();
                 foreach (var Style in parameters.Styles)
                 {
                     var temp = Style;
                     StylePredicate = StylePredicate.Or(s => s.TLCMTWC_Style_FK == temp.Sty_Id);
                 }

                 CompletedWork = CompletedWork.AsExpandable().Where(StylePredicate);
             }
             //----------------------------------------------------------------------
             // Filter out any particular Colours if neccessary
             //------------------------------------------------------------
             if (parameters.Colours.Count > 0)
             {
                 var ColourPredicate = PredicateBuilder.False<TLCMT_CompletedWork>();
                 foreach (var Colour in parameters.Colours)
                 {
                     var temp = Colour;
                     ColourPredicate = ColourPredicate.Or(s => s.TLCMTWC_Colour_FK == temp.Col_Id);
                 }

                 CompletedWork = CompletedWork.AsExpandable().Where(ColourPredicate);
             }

             //----------------------------------------------------------------------
             // Filter out any particular Sizes if neccessary
             //------------------------------------------------------------
             if (parameters.Sizes.Count > 0)
             {
                 var SizePredicate = PredicateBuilder.False<TLCMT_CompletedWork>();
                 foreach (var Size in parameters.Sizes)
                 {
                     var temp = Size;
                     SizePredicate = SizePredicate.Or(s => s.TLCMTWC_Size_FK == temp.SI_id);
                 }

                 CompletedWork = CompletedWork.AsExpandable().Where(SizePredicate);
             }
             return CompletedWork;
         }

       public IQueryable<TLCMT_LineIssue>CMTWIP(CMTQueryParameters parameters)
         {

             var WIP = _context.TLCMT_LineIssue.Where(x=>x.TLCMTLI_IssuedToLine && !x.TLCMTLI_WorkCompleted).AsQueryable();
             
             //--------------------------------------------------------------
             // Filter out any particular CMT if neccessary
             //------------------------------------------------------------
             if (parameters.Depts.Count > 0)
             {
                 var DepartmentPredicate = PredicateBuilder.False<TLCMT_LineIssue>();
                 foreach (var Dept in parameters.Depts)
                 {
                     var temp = Dept;
                     DepartmentPredicate = DepartmentPredicate.Or(s => s.TLCMTLI_CmtFacility_FK  == temp.Dep_Id);
                 }

                 WIP = WIP.AsExpandable().Where(DepartmentPredicate);
             }

             //----------------------------------------------------------------------
             // Filter out any particular Lines if neccessary
             //------------------------------------------------------------
             if (parameters.Lines.Count > 0)
             {
                 var StylePredicate = PredicateBuilder.False<TLCMT_LineIssue>();
                 foreach (var Style in parameters.Lines)
                 {
                     var temp = Style;
                     StylePredicate = StylePredicate.Or(s => s.TLCMTLI_Pk == temp.TLCMTCFG_Pk);
                 }

                 WIP = WIP.AsExpandable().Where(StylePredicate);     
             }

              return WIP;
         }

       public IQueryable<TLCMT_CompletedWork> CMTCompletedWorkExport(CMTQueryParameters parameters)
       {
           var CompletedWork = _context.TLCMT_CompletedWork.Where(x => !x.TLCMTWC_CMTBilled && x.TLCMTWC_TransactionDate <= parameters.ToDate).AsQueryable();
           //--------------------------------------------------------------
           // Filter out any particular CMT if neccessary
           //------------------------------------------------------------
           if (parameters.Depts.Count > 0)
           {
               var DepartmentPredicate = PredicateBuilder.False<TLCMT_CompletedWork>();
               foreach (var Dept in parameters.Depts)
               {
                   var temp = Dept;
                   DepartmentPredicate = DepartmentPredicate.Or(s => s.TLCMTWC_CMTFacility_FK == temp.Dep_Id);
               }

               CompletedWork = CompletedWork.AsExpandable().Where(DepartmentPredicate);
           }

           
           return CompletedWork;
       }
         public IQueryable<TLCMT_CompletedWork> CMTCompletedWorkNotDespatched(CMTQueryParameters parameters)
         {

             var CompletedWork = _context.TLCMT_CompletedWork.Where(x=>!x.TLCMTWC_Despatched).AsQueryable();
             //--------------------------------------------------------------
             // Filter out any particular CMT if neccessary
             //------------------------------------------------------------
             if (parameters.Depts.Count > 0)
             {
                 var DepartmentPredicate = PredicateBuilder.False<TLCMT_CompletedWork>();
                 foreach (var Dept in parameters.Depts)
                 {
                     var temp = Dept;
                     DepartmentPredicate = DepartmentPredicate.Or(s => s.TLCMTWC_CMTFacility_FK == temp.Dep_Id);
                 }

                 CompletedWork = CompletedWork.AsExpandable().Where(DepartmentPredicate);
             }

             //----------------------------------------------------------------------
             // Filter out any particular Styles if neccessary
             //------------------------------------------------------------
             if (parameters.Styles.Count > 0)
             {
                 var StylePredicate = PredicateBuilder.False<TLCMT_CompletedWork>();
                 foreach (var Style in parameters.Styles)
                 {
                     var temp = Style;
                     StylePredicate = StylePredicate.Or(s => s.TLCMTWC_Style_FK == temp.Sty_Id);
                 }

                 CompletedWork = CompletedWork.AsExpandable().Where(StylePredicate);
             }

             //-------------------------------------------------------------
             // Filter out any particular Colour if neccessary
             //------------------------------------------------------------
             if (parameters.Colours.Count > 0)
             {
                 var ColourPredicate = PredicateBuilder.False<TLCMT_CompletedWork>();
                 foreach (var Colour in parameters.Colours)
                 {
                     var temp = Colour;
                     ColourPredicate = ColourPredicate.Or(s => s.TLCMTWC_Colour_FK == temp.Col_Id);
                 }

                 CompletedWork = CompletedWork.AsExpandable().Where(ColourPredicate);
             }
             //------------------------------------------------------------------
             // Filter out any particular Size if neccessary
             //------------------------------------------------------------
             if (parameters.Sizes.Count > 0)
             {
                 var SizePredicate = PredicateBuilder.False<TLCMT_CompletedWork>();
                 foreach (var Size in parameters.Sizes)
                 {
                     var temp = Size;
                     SizePredicate = SizePredicate.Or(s => s.TLCMTWC_Size_FK == temp.SI_id);
                 }

                 CompletedWork = CompletedWork.AsExpandable().Where(SizePredicate);
             }
             return CompletedWork;
         }
         public IQueryable<TLCMT_LineIssue> CMTLineIssue(CMTQueryParameters parameters)
         {
             _context.Configuration.AutoDetectChangesEnabled = false;

             // This particular function has been changed To call for Panels in the CMT Panel Receipt Store  
             var LineIssues = _context.TLCMT_LineIssue.Where(x=>!x.TLCMTLI_WorkCompleted).AsQueryable();
      
             // Filter out any particular CMT if neccessary
             //------------------------------------------------------------
             if (parameters.Depts.Count > 0)
             {
                 var DepartmentPredicate = PredicateBuilder.False<TLCMT_LineIssue>();
                 foreach (var Dept in parameters.Depts)
                 {
                     var temp = Dept;
                     DepartmentPredicate = DepartmentPredicate.Or(s => s.TLCMTLI_CmtFacility_FK == temp.Dep_Id);
                 }

                LineIssues = LineIssues.AsExpandable().Where(DepartmentPredicate);
             }

             if (parameters.Styles.Count > 0)
             {
                 var StylesPredicate = PredicateBuilder.False<TLCMT_LineIssue>();
             }

             return LineIssues;
         }

         public IQueryable<TLCMT_LineIssue> CMTLineIssueCW(CMTQueryParameters parameters)
         {
             var LineIssuesCw = (from T1 in _context.TLCMT_LineIssue
                                join T2 in _context.TLCMT_CompletedWork on T1.TLCMTLI_Pk equals T2.TLCMTWC_LineIssue_FK
                                where T1.TLCMTLI_WorkCompleted && !T2.TLCMTWC_Despatched 
                                select T1).Distinct().AsQueryable();

             // Filter out any particular CMT if neccessary
             //------------------------------------------------------------
             if (parameters.Depts.Count > 0)
             {
                 var DepartmentPredicate = PredicateBuilder.False<TLCMT_LineIssue>();
                 foreach (var Dept in parameters.Depts)
                 {
                     var temp = Dept;
                     DepartmentPredicate = DepartmentPredicate.Or(s => s.TLCMTLI_CmtFacility_FK == temp.Dep_Id);
                 }

                 LineIssuesCw = LineIssuesCw.AsExpandable().Where(DepartmentPredicate);
             }

            return LineIssuesCw;
         }

         public IQueryable<TLCMT_LineIssue> CutsOnHold(CMTQueryParameters parameters)
         {
             var LineIssuesCw = _context.TLCMT_LineIssue.Where(x=>x.TLCMTLI_OnHold).AsQueryable();
             
             // Filter out any particular CMT if neccessary
             //------------------------------------------------------------
             if (parameters.Depts.Count > 0)
             {
                 var DepartmentPredicate = PredicateBuilder.False<TLCMT_LineIssue>();
                 foreach (var Dept in parameters.Depts)
                 {
                     var temp = Dept;
                     DepartmentPredicate = DepartmentPredicate.Or(s => s.TLCMTLI_CmtFacility_FK == temp.Dep_Id);
                 }

                 LineIssuesCw = LineIssuesCw.AsExpandable().Where(DepartmentPredicate);
             }

             /*
             if (parameters.Styles.Count > 0)
             {
                 var StylePredicate = PredicateBuilder.False<TLCMT_LineIssue>();
                 foreach (var Style in parameters.Styles)
                 {
                     var temp = Style;
                     StylePredicate = StylePredicate.Or(s => s.S.TLCUTSHR_Style_FK == temp.TLCUTSHR_Style_FK);
                 }

                 //CutSheets = CutSheets.AsExpandable().Where(StylePredicate);
             }
             
             if (parameters.Colours.Count > 0)
             {
                 var ColourPredicate = PredicateBuilder.False<TLCUT_CutSheetReceipt>();
                 foreach (var Colour in parameters.Colours)
                 {
                     var temp = Colour;
                     ColourPredicate = ColourPredicate.Or(s => s.TLCUTSHR_Colour_FK == temp.Col_Id);

                 }

                 // CutSheets = CutSheets.AsExpandable().Where(ColourPredicate);

             }
              */ 

             return LineIssuesCw;
         }
         public IQueryable<TLCUT_CutSheetReceipt> CMTCustSheetReceipt(CMTQueryParameters parameters)
         {
             var CutSheets = _context.TLCUT_CutSheetReceipt.Where(x => x.TLCUTSHR_Issued).AsQueryable();

             if (parameters.Styles.Count > 0)
             {
                 var StylePredicate = PredicateBuilder.False<TLCUT_CutSheetReceipt>();
                 foreach (var CutSheet in CutSheets)
                 {
                     var temp = CutSheet;
                     StylePredicate = StylePredicate.Or(s => s.TLCUTSHR_Style_FK == temp.TLCUTSHR_Style_FK); 
                 }

                 //CutSheets = CutSheets.AsExpandable().Where(StylePredicate);
             }

             if (parameters.Colours.Count > 0)
             {
                 var ColourPredicate = PredicateBuilder.False<TLCUT_CutSheetReceipt>();
                 foreach (var Colour in parameters.Colours)
                 {
                     var temp = Colour;
                     ColourPredicate = ColourPredicate.Or(s => s.TLCUTSHR_Colour_FK == temp.Col_Id);

                 }

                 // CutSheets = CutSheets.AsExpandable().Where(ColourPredicate);

             }

             return CutSheets;

         }

         public IQueryable<TLCUT_CutSheetReceiptDetail> CMTCutSheetReceiptDetail(CMTQueryParameters parameters)
         {
             var CutSheets = _context.TLCUT_CutSheetReceiptDetail.AsQueryable();

             if (parameters.Sizes.Count > 0)
             {
                 var SizePredicate = PredicateBuilder.False<TLCUT_CutSheetReceiptDetail>();
                 foreach (var Size in parameters.Sizes)
                 {
                     var temp = Size;
                     SizePredicate = SizePredicate.Or(s => s.TLCUTSHRD_Size_FK == temp.SI_id);
                 }

                 CutSheets = CutSheets.AsExpandable().Where(SizePredicate);
             }

             return CutSheets;
         }

       
         public IQueryable<TLADM_CustomerFile> CMTCustomer(CMTQueryParameters parameters)
         {
             var QueryCustomers = _context.TLADM_CustomerFile.AsQueryable();
             // Filter out any particular Customers if neccessary
             //------------------------------------------------------------
             if (parameters.Customers.Count > 0)
             {
                 var CustomerPredicate = PredicateBuilder.False<TLADM_CustomerFile>();
                 foreach (var Customer in parameters.Customers)
                 {
                     var temp = Customer;
                     CustomerPredicate = CustomerPredicate.Or(s => s.Cust_Pk == temp.Cust_Pk);
                 }

                 QueryCustomers = QueryCustomers.AsExpandable().Where(CustomerPredicate);
             }

             return QueryCustomers;
         }

         public IQueryable<TLADM_Styles> CMTStylesByCustomer(CMTQueryParameters parameters)
         {
             var QueryStyles = _context.TLADM_Styles.AsQueryable();
             
             // Filter out any particular Customers if neccessary
             //------------------------------------------------------------
             if (parameters.Customers.Count > 0)
             {
                 var CustomerPredicate = PredicateBuilder.False<TLADM_Styles>();
                 foreach (var Customer in parameters.Customers)
                 {
                     var temp = Customer;
                     CustomerPredicate = CustomerPredicate.Or(s => s.Sty_Label_FK == Customer.Cust_Pk);
                 }
                 QueryStyles = QueryStyles.AsExpandable().Where(CustomerPredicate);

             }
             if (parameters.Styles.Count != 0)
             {
                 var StylePredicate = PredicateBuilder.False<TLADM_Styles>();
                 foreach (var Style in parameters.Styles)
                 {
                     var temp = Style;
                     StylePredicate = StylePredicate.Or(s => s.Sty_Id == temp.Sty_Id);
                 }
                 QueryStyles = QueryStyles.AsExpandable().Where(StylePredicate);
             }

             return QueryStyles;
         }


         public IQueryable<TLADM_Styles> CMTStyles(CMTQueryParameters parameters)
         {
             var QueryStyles = _context.TLADM_Styles.AsQueryable();
             // Filter out any particular Customers if neccessary
             //------------------------------------------------------------
             if (parameters.Styles.Count > 0)
             {
                 var StylePredicate = PredicateBuilder.False<TLADM_Styles>();
                 foreach (var Style in parameters.Styles)
                 {
                     var temp = Style;
                     StylePredicate = StylePredicate.Or(s => s.Sty_Id == temp.Sty_Id);
                 }

                 QueryStyles = QueryStyles.AsExpandable().Where(StylePredicate);
             }

             return QueryStyles;
         }

         public IQueryable<TLCUT_CutSheetReceipt> SelCutSheetByWhse(CMTQueryParameters parameters)
         {
             var CutSheetReceipts = _context.TLCUT_CutSheetReceipt.Where(X=>!X.TLCUTSHR_Issued && X.TLCUTSHR_InPanelStore).AsQueryable();
             if (parameters.WhseStores.Count != 0)
             {
                 var WhsePredicate = PredicateBuilder.False<TLCUT_CutSheetReceipt>();

                 foreach (var Whse in parameters.WhseStores)
                 {
                     var temp = Whse;
                     WhsePredicate = WhsePredicate.Or(s => s.TLCUTSHR_WhsePanStore_FK == temp.WhStore_Id);
                 }

                 CutSheetReceipts = CutSheetReceipts.AsExpandable().Where(WhsePredicate);
             }

             return CutSheetReceipts;
         }

         public IQueryable<TLADM_Colours> CMTColours(CMTQueryParameters parameters)
         {
             var QueryColours = _context.TLADM_Colours.AsQueryable();
             // Filter out any particular Customers if neccessary
             //------------------------------------------------------------
             if (parameters.Colours.Count > 0)
             {
                 var ColourPredicate = PredicateBuilder.False<TLADM_Colours>();
                 foreach (var Colour in parameters.Colours)
                 {
                     var temp = Colour;
                     ColourPredicate = ColourPredicate.Or(s => s.Col_Id == temp.Col_Id);
                 }

                 QueryColours = QueryColours.AsExpandable().Where(ColourPredicate);
             }

             return QueryColours;
         }

         public IQueryable<TLADM_Sizes> CMTSizes(CMTQueryParameters parameters)
         {
             var QuerySizes = _context.TLADM_Sizes.AsQueryable();
             // Filter out any particular Customers if neccessary
             //------------------------------------------------------------
             if (parameters.Sizes.Count > 0)
             {
                 var SizePredicate = PredicateBuilder.False<TLADM_Sizes>();
                 foreach (var Size in parameters.Sizes)
                 {
                     var temp = Size;
                     SizePredicate = SizePredicate.Or(s => s.SI_id == temp.SI_id);
                 }

                 QuerySizes = QuerySizes.AsExpandable().Where(SizePredicate);
             }

             return QuerySizes;
         }
         
      

         
    }
   
    public class CMTQueryParameters
    {
        public List<TLADM_Sizes> Sizes;
        public List<TLADM_Colours> Colours;
        public List<TLADM_Styles> Styles;
        public List<TLCUT_CutSheet> CutSheets;
        public List<TLADM_CustomerFile> Customers;
        public List<TLADM_Departments> Depts;
        public List<TLCMT_FactConfig> Lines;
        public List<TLADM_CMTMeasurementPoints> MeasurementPoints;
        public List<TLCMT_PanelIssue> PanelIssue;
        public List<TLDYE_BIFInTransit> BIFInTransit;
        public List<TLADM_WhseStore> WhseStores;
        public List<TLSEC_UserAccess> UserAccesses;
        public bool BillingRecords;
        public bool ViewDataByStyle;
        public List<TLADM_CMTNonCompliance> NonADMCompliances;
        public DateTime FromDate;
        public DateTime ToDate;
        public int Year;
        public int FromStore_FK;
        public bool ProductionResults;
        public bool UseDatePicker;


        public CMTQueryParameters()
        {
            CutSheets = new List<TLCUT_CutSheet>();
            Sizes = new List<TLADM_Sizes>();
            Colours = new List<TLADM_Colours>();
            Styles = new List<TLADM_Styles>();
            Customers = new List<TLADM_CustomerFile>();
            Depts = new List<TLADM_Departments>();
            Lines = new List<TLCMT_FactConfig>();
            MeasurementPoints = new List<TLADM_CMTMeasurementPoints>();
            PanelIssue = new List<TLCMT_PanelIssue>();
            WhseStores = new List<TLADM_WhseStore>();
            BIFInTransit = new List<TLDYE_BIFInTransit>();
            UserAccesses = new List<TLSEC_UserAccess>();
            FromStore_FK = 0;
            ViewDataByStyle = true;
            BillingRecords = false;
            ToDate = DateTime.Now;
            FromDate = DateTime.Now;
            NonADMCompliances = new List<TLADM_CMTNonCompliance>();
            Year = DateTime.Now.Year;
            ProductionResults = false;
            UseDatePicker = false;

        }

    }
}
