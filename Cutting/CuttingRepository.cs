using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;
using LinqKit;
using System.Windows.Forms;
namespace Cutting
{
    public class CuttingRepository : IDisposable
    {
        protected readonly TTI2Entities _context;

        public CuttingRepository()
        {
            _context = new TTI2Entities();
        }

        public TLCUT_CutSheet LoadCutSheet(int Pk)
        {
            return _context.TLCUT_CutSheet.FirstOrDefault(s => s.TLCutSH_Pk == Pk);
        }

        public TLDYE_BIFInTransit LoadBIFInTransit(int Pk)
        {
            return _context.TLDYE_BIFInTransit.FirstOrDefault(s => s.BIFT_Pk == Pk);
        }

        public TLADM_Departments LoadDepartments(int Pk)
        {
            return _context.TLADM_Departments.FirstOrDefault(s => s.Dep_Id == Pk);
        }

        public TLADM_MachineOperators LoadOperators(int Pk)
        {
            return _context.TLADM_MachineOperators.FirstOrDefault(s => s.MachOp_Pk == Pk);
        }

        public TLCUT_CutSheetReceipt LoadCutSheetReceipt(int Pk)
        {
            return _context.TLCUT_CutSheetReceipt.FirstOrDefault(s => s.TLCUTSHR_Pk == Pk);
        }

        public TLADM_Sizes LoadSize(int Pk)
        {
            return _context.TLADM_Sizes.FirstOrDefault(s => s.SI_id == Pk);
        }

        public TLADM_Styles LoadStyle(int Pk)
        {
            return _context.TLADM_Styles.FirstOrDefault(s => s.Sty_Id == Pk);
        }

        public TLCUT_QAResults LoadQAResults(int Pk)
        {
            return _context.TLCUT_QAResults.FirstOrDefault(s => s.TLCUTQA_Bundle_FK == Pk);
        }

        public TLADM_Griege LoadQuality(int Pk)
        {
            return _context.TLADM_Griege.FirstOrDefault(s => s.TLGreige_Id == Pk);
        }

        public TLADM_Colours LoadColour(int Pk)
        {
            return _context.TLADM_Colours.FirstOrDefault(s => s.Col_Id == Pk);
        }

        public TLADM_MachineDefinitions LoadMachine(int Pk)
        {
            return _context.TLADM_MachineDefinitions.FirstOrDefault(s => s.MD_Pk == Pk);
        }

        public TLKNI_GreigeProduction LoadGreigeProduction(int Pk)
        {
            return _context.TLKNI_GreigeProduction.FirstOrDefault(s => s.GreigeP_Pk == Pk);
        }

        public TLADM_WhseStore LoadWareHouse(int Pk)
        {
            return _context.TLADM_WhseStore.FirstOrDefault(s => s.WhStore_Id == Pk);
        }

        public IQueryable<TLCSV_StockOnHand> FromWareHouse(CuttingQueryParameters parameters)
        {
            var FmWhse = _context.TLCSV_StockOnHand.Where(x => x.TLSOH_WareHouse_FK == parameters.FromWhse && !x.TLSOH_Picked).AsQueryable();

            if (parameters.Styles.Count() > 0)
            {
                var stylePredicate = PredicateBuilder.False<TLCSV_StockOnHand>();
                foreach (var style in parameters.Styles)
                {
                    var temp = style;
                    stylePredicate = stylePredicate.Or(s => s.TLSOH_Style_FK == temp.Sty_Id);
                }

                FmWhse = FmWhse.AsExpandable().Where(stylePredicate);
            }

            if (parameters.Colours.Count() > 0)
            {
                var colourPredicate = PredicateBuilder.False<TLCSV_StockOnHand>();
                foreach (var style in parameters.Colours)
                {
                    var temp = style;
                    colourPredicate = colourPredicate.Or(s => s.TLSOH_Colour_FK == temp.Col_Id);
                }

                FmWhse = FmWhse.AsExpandable().Where(colourPredicate);
            }

            if (parameters.Sizes.Count() > 0)
            {
                var sizePredicate = PredicateBuilder.False<TLCSV_StockOnHand>();
                foreach (var style in parameters.Sizes)
                {
                    var temp = style;
                    sizePredicate = sizePredicate.Or(s => s.TLSOH_Size_FK == temp.SI_id);
                }

                FmWhse = FmWhse.AsExpandable().Where(sizePredicate);
            }
            return FmWhse;
        }

        public IQueryable<TLCUT_CutSheet> SelectCustSheets(CuttingQueryParameters parameters)
        {
            var CS = _context.TLCUT_CutSheet.AsQueryable();
            if (parameters.Departments.Count != 0)
            {
                var DeptPredicate = PredicateBuilder.False<TLCUT_CutSheet>();
                foreach (var Dept in parameters.Departments)
                {
                    var temp = Dept;
                    DeptPredicate = DeptPredicate.Or(s => s.TLCutSH_Department_FK == temp.Dep_Id);
                }
                CS = CS.AsExpandable().Where(DeptPredicate);
            }
            return CS;
        }

        public IQueryable<TLCUT_CutSheet> SelectCustSheetsByLocation(CuttingQueryParameters parameters)
        {
            var CS = _context.TLCUT_CutSheet.Where(x=>x.TLCutSH_Date >= parameters.FromDate && x.TLCutSH_Date <= parameters.ToDate).AsQueryable();
            if (parameters.Departments.Count != 0)
            {
                var DeptPredicate = PredicateBuilder.False<TLCUT_CutSheet>();
                foreach (var Dept in parameters.Departments)
                {
                    var temp = Dept;
                    DeptPredicate = DeptPredicate.Or(s => s.TLCutSH_Department_FK == temp.Dep_Id); 
                }
                CS = CS.AsExpandable().Where(DeptPredicate);
            }
            return CS;
        }

        public IQueryable<TLCUT_CutSheetReceipt> SelCutReceiptByLoc(CuttingQueryParameters parameters)
        {
            var CutRec = _context.TLCUT_CutSheetReceipt.Where(x=>!x.TLCUTSHR_Issued && x.TLCUTSHR_InPanelStore).AsQueryable();
            if (parameters.WareHouses.Count != 0)
            {
                var WhsePredicate = PredicateBuilder.False<TLCUT_CutSheetReceipt>();

                foreach (var Whse in parameters.WareHouses)
                {
                    var temp = Whse;
                    WhsePredicate = WhsePredicate.Or(s => s.TLCUTSHR_WhsePanStore_FK == temp.WhStore_Id);

                }
                CutRec = CutRec.AsExpandable().Where(WhsePredicate);
            }
            return CutRec;
        }

        public IQueryable<TLCUT_QCBerrie> SelectQCBerrie(CuttingQueryParameters parameters)
         {
             var QCBerrie = _context.TLCUT_QCBerrie.Where(x => x.TLQCFB_Measure1 != 0 ||
                                                              x.TLQCFB_Measure10 != 0 ||
                                                              x.TLQCFB_Measure11 != 0 ||
                                                              x.TLQCFB_Measure2 != 0 ||
                                                              x.TLQCFB_Measure3 != 0 ||
                                                              x.TLQCFB_Measure4 != 0 ||
                                                              x.TLQCFB_Measure5 != 0 ||
                                                              x.TLQCFB_Measure6 != 0 ||
                                                              x.TLQCFB_Measure7 != 0 ||
                                                              x.TLQCFB_Measure8 != 0 ||
                                                              x.TLQCFB_Measure9 != 0).AsQueryable();
             if (parameters.CutSheetReceipts.Count != 0)
             {
                 var CutSheetPredicate = PredicateBuilder.False<TLCUT_QCBerrie>();
                 foreach (var Receipt in parameters.CutSheetReceipts)
                 {
                     var temp = Receipt;
                     CutSheetPredicate = CutSheetPredicate.Or(s => s.TLQCFB_CutSheetReceipt_FK == temp.TLCUTSHR_Pk);
                 }

                 QCBerrie = QCBerrie.AsExpandable().Where(CutSheetPredicate); 
             }
             
             if (parameters.Operators.Count != 0)
             {
                 var OperatorsPredicate = PredicateBuilder.False<TLCUT_QCBerrie>();
                 foreach (var Operator in parameters.Operators)
                 {
                     var temp = Operator;
                     OperatorsPredicate = OperatorsPredicate.Or(s => s.TLQCFB_Operator_FK == temp.MachOp_Pk);
                 }

                 QCBerrie = QCBerrie.AsExpandable().Where(OperatorsPredicate);
              }

             return QCBerrie;
         }

        public IQueryable<TLCUT_QAResults> SelectQaResults(CuttingQueryParameters parameters)
        {
            var QARes = _context.TLCUT_QAResults.AsQueryable() ;

            if (parameters.FromDate.Day != parameters.ToDate.Day)
                QARes = QARes.Where(x => x.TLCUTQA_Date >= parameters.FromDate && x.TLCUTQA_Date <= parameters.ToDate).AsQueryable();

            if (parameters.QAResults.Count != 0)
            {
                var CSPredicate = PredicateBuilder.False<TLCUT_QAResults>();
                foreach (var CSR in parameters.QAResults)
                {
                    var temp = CSR;
                    CSPredicate = CSPredicate.Or(s => s.TLCUTQA_Bundle_FK == temp.TLCUTQA_Bundle_FK);
                }

                QARes = QARes.AsExpandable().Where(CSPredicate);
                                
            }
            else
                QARes= _context.TLCUT_QAResults.Where(x=>x.TLCUTQA_Date >= parameters.FromDate && x.TLCUTQA_Date <= parameters.ToDate).AsQueryable();
           
            return QARes;
        }

        public IQueryable<TLKNI_GreigeProduction> SelectGreigeProduction(CuttingQueryParameters parameters)
        {
            var Entries = (from GP in _context.TLKNI_GreigeProduction
                           join DBD in _context.TLDYE_DyeBatchDetails on GP.GreigeP_Pk equals DBD.DYEBD_GreigeProduction_FK
                           where GP.GreigeP_BoughtIn && !DBD.DYEBO_CutSheet
                           select GP).AsQueryable();

            if (parameters.GreigeProd.Count != 0)
            {
                var GriegePredicate = PredicateBuilder.False<TLKNI_GreigeProduction>();
                foreach (var Prod in parameters.GreigeProd)
                {
                    var temp = Prod;
                    GriegePredicate = GriegePredicate.Or(s => s.GreigeP_Pk == temp.GreigeP_Pk);
                }
                Entries = Entries.AsExpandable().Where(GriegePredicate);
            }
            return Entries;
        }

        public IQueryable<TLCUT_CutSheet> SelectWIPCutSheets(CuttingQueryParameters parameters)
        {
            IQueryable<TLCUT_CutSheet> CS;

            if (parameters.AllWIP)
                CS = _context.TLCUT_CutSheet.Where(x => !x.TLCutSH_WIPComplete && x.TLCutSH_Accepted && !x.TLCutSH_Closed).AsQueryable();
            else
                CS = _context.TLCUT_CutSheet.Where(x => x.TLCutSH_Date >= parameters.FromDate && x.TLCutSH_Date <= parameters.ToDate && !x.TLCutSH_WIPComplete && x.TLCutSH_Accepted && !x.TLCutSH_Closed).AsQueryable();

            if (parameters.Departments.Count != 0)
            {
                var DeptPredicate = PredicateBuilder.False<TLCUT_CutSheet>();
                foreach (var Dept in parameters.Departments)
                {
                    var temp = Dept;
                    DeptPredicate = DeptPredicate.Or(s => s.TLCutSH_Department_FK == Dept.Dep_Id); 
                }

                CS = CS.AsExpandable().Where(DeptPredicate);
            }

            if (parameters.Qualities.Count != 0)
            {
                var CSPredicate = PredicateBuilder.False<TLCUT_CutSheet>();
                foreach (var CSR in parameters.Qualities)
                {
                    var temp = CSR;
                    CSPredicate = CSPredicate.Or(s => s.TLCutSH_Quality_FK == temp.TLGreige_Id);
                }

                CS = CS.AsExpandable().Where(CSPredicate);
            }

            if (parameters.Colours.Count != 0)
            {
                var CSColourPredicate = PredicateBuilder.False<TLCUT_CutSheet>();
                foreach (var Colour in parameters.Colours)
                {
                    var temp = Colour;
                    CSColourPredicate = CSColourPredicate.Or(s => s.TLCutSH_Colour_FK == temp.Col_Id);
                }

                CS = CS.AsExpandable().Where(CSColourPredicate);
            }
            return CS;
        }

        public IQueryable<TLCUT_CutSheetReceipt> SelectCSReceipts(CuttingQueryParameters parameters)
        {
            var CSRep = _context.TLCUT_CutSheetReceipt.Where(x=>x.TLCUTSHR_InBundleStore && !x.TLCUTSHR_Issued).AsQueryable();
     
            if (parameters.CutSheetReceipts.Count > 0)
            {
                var CSPredicate = PredicateBuilder.False<TLCUT_CutSheetReceipt>();
                foreach (var CSR in parameters.CutSheetReceipts)
                {
                    var temp = CSR;
                    CSPredicate = CSPredicate.Or(s => s.TLCUTSHR_Pk == temp.TLCUTSHR_Pk);
                }

                CSRep = CSRep.AsExpandable().Where(CSPredicate);
            }
            return CSRep;
        }

        public IQueryable<TLCUT_CutSheetReceipt> SelectCutProduction(CuttingQueryParameters parameters)
        {
            var CSRep = _context.TLCUT_CutSheetReceipt.Where(x => x.TLCUTSHR_DateIntoPanelStore >= parameters.FromDate && x.TLCUTSHR_DateIntoPanelStore <= parameters.ToDate).AsQueryable();

            if (parameters.Styles.Count != 0)
            {
                var CSPredicate = PredicateBuilder.False<TLCUT_CutSheetReceipt>();
                foreach (var Style in parameters.Styles)
                {
                    var temp = Style;
                    CSPredicate = CSPredicate.Or(s => s.TLCUTSHR_Style_FK == temp.Sty_Id);
                }

                CSRep = CSRep.AsExpandable().Where(CSPredicate);
            }
            if (parameters.Colours.Count != 0)
            {
                var ColourPredicate = PredicateBuilder.False<TLCUT_CutSheetReceipt>();
                foreach (var Colour in parameters.Colours)
                {
                    var temp = Colour;
                    ColourPredicate = ColourPredicate.Or(s => s.TLCUTSHR_Colour_FK == temp.Col_Id);
                }

                CSRep = CSRep.AsExpandable().Where(ColourPredicate);
            }
            return CSRep;
        }
        public void Dispose()
        {
            if (_context != null)
            {
                _context.Dispose();
            }
        }
    }

    public class CuttingQueryParameters
    {
        public List<TLADM_Styles> Styles;
        public List<TLCUT_CutSheet> CutSheets;
        public List<TLCUT_CutSheetReceipt> CutSheetReceipts;
        public List<TLADM_Griege> Qualities;
        public List<TLCUT_QAResults> QAResults;
        public List<TLADM_Colours> Colours;
        public List<TLADM_MachineDefinitions> Machines;
        public List<TLADM_Departments> Departments;
        public List<TLADM_MachineOperators> Operators;
        public List<TLADM_WhseStore> WareHouses;
        public List<TLKNI_GreigeProduction> GreigeProd;
        public List<TLDYE_BIFInTransit> BIFInTransit; 
        public DateTime FromDate;
        public DateTime ToDate;
        public bool AllWIP;
        public bool QAFullDetail;
        public int RepSortOption;
        public int FromWhse;
        public bool ProductionResults;
        public int BIFTransNumber;
        public List<TLADM_Sizes> Sizes;

        public CuttingQueryParameters()
        {
            CutSheets = new List<TLCUT_CutSheet>();
            Styles = new List<TLADM_Styles>();
            CutSheetReceipts = new List<TLCUT_CutSheetReceipt>();
            FromDate = new DateTime();
            ToDate = new DateTime();
            AllWIP = false;
            Qualities = new List<TLADM_Griege>();
            Colours = new List<TLADM_Colours>();
            QAResults = new List<TLCUT_QAResults>();
            Sizes = new List<TLADM_Sizes>();
            Departments = new List<TLADM_Departments>();
            Operators = new List<TLADM_MachineOperators>();
            Machines = new List<TLADM_MachineDefinitions>();
            WareHouses = new List<TLADM_WhseStore>();
            GreigeProd = new List<TLKNI_GreigeProduction>();
            BIFInTransit = new List<TLDYE_BIFInTransit>();
            FromWhse = 0;
            RepSortOption = 0;
            QAFullDetail = true;
            ProductionResults = false;
            BIFTransNumber = 0;

        }
    }
}
