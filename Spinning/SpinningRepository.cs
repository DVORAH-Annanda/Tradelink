using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;
using System.Windows.Forms;
using LinqKit;

namespace Spinning
{
    public class SpinningRepository : IDisposable
    {
        protected readonly TTI2Entities _context;

        public SpinningRepository()
        {
            _context = new TTI2Entities();
        }

        public TLADM_MachineDefinitions LoadMachine(int Pk)
        {
            return _context.TLADM_MachineDefinitions.FirstOrDefault(s => s.MD_Pk == Pk);
        }

        public TLADM_Yarn LoadYarnTypes(int Pk)
        {
            return _context.TLADM_Yarn.FirstOrDefault(s => s.YA_Id == Pk);
        }

        public TLSPN_CottonReceivedBales LoadLotNo(int Pk)
        {
            return _context.TLSPN_CottonReceivedBales.FirstOrDefault(s => s.CotBales_LotNo == Pk);
        }
        public TLSPN_YarnOrder LoadYarnOrders(int Pk)
        {
            return _context.TLSPN_YarnOrder.FirstOrDefault(s => s.YarnO_Pk == Pk);
        }

        public void Dispose()
        {
            if (_context != null)
            {
                _context.Dispose();
            }
        }

        public IQueryable<TLSPN_CottonReceivedBales> CottonReceivedLots(SpinningQueryParameters parameters)
        {
            var CottonReceived = _context.TLSPN_CottonReceivedBales.Where(x=>x.CotBales_ConfirmedByQA && !x.CoBales_IssuedToProd).AsQueryable();

            if (parameters.CotReceivedBales.Count > 0)
            {
                var CottonReceivedPredicate = PredicateBuilder.New<TLSPN_CottonReceivedBales>();
                foreach (var CottonR in parameters.CotReceivedBales)
                {
                    var temp = CottonR;
                    CottonReceivedPredicate = CottonReceivedPredicate.Or(s => s.CotBales_LotNo == temp.CotBales_LotNo);
                }

                CottonReceived = CottonReceived.AsExpandable().Where(CottonReceivedPredicate);

            }
              

            return CottonReceived;
        }
        public IQueryable<TLSPN_YarnOrder> YarnOrderQuery(SpinningQueryParameters parameters)
        {
            var yarnOrders = _context.TLSPN_YarnOrder.Where(x => !(bool)x.Yarno_Closed && x.YarnO_Date >= parameters.FromDate && x.YarnO_Date <= parameters.ToDate).AsQueryable();

            if (parameters.YarnOrders.Count > 0)
            {
                var yarnOrderPredicate = PredicateBuilder.New<TLSPN_YarnOrder>();
                foreach (var yarnOrder in parameters.YarnOrders)
                {
                    var temp = yarnOrder;
                    yarnOrderPredicate = yarnOrderPredicate.Or(s => s.YarnO_Pk == temp.YarnO_Pk);
                }

                yarnOrders = yarnOrders.AsExpandable().Where(yarnOrderPredicate);

            }

            if (parameters.YarnTypes.Count > 0)
            {
                var yarnTypePredicate = PredicateBuilder.New<TLSPN_YarnOrder>();
                foreach (var yarnType in parameters.YarnTypes)
                {
                    var temp = yarnType;
                    yarnTypePredicate = yarnTypePredicate.Or(s => s.Yarno_YarnType_FK == temp.YA_Id);
                }

                yarnOrders = yarnOrders.AsExpandable().Where(yarnTypePredicate);

            }

            return yarnOrders;
        }


        public IQueryable<TLSPN_QAMeasurements> QAMeasurementQuery(SpinningQueryParameters parameters)
        {
            IQueryable<TLSPN_QAMeasurements> QAItems = null;

            QAItems = _context.TLSPN_QAMeasurements.Where(x => x.YarnQA_Date >= parameters.FromDate && x.YarnQA_Date <= parameters.ToDate && x.YarnQA_MeasureNo == parameters.MeasurementOpt).AsQueryable();

            if (parameters.MeasurementOpt == 3)
            {
                 if (!parameters.OutSideTolerance)
                 {
                        QAItems = QAItems.Where(x => x.YarnQA_12H00 > parameters.LowerTolerance && x.YarnQA_12H00 <= parameters.UpperTolerance).AsQueryable();
                 }
                 else
                 {
                        QAItems = QAItems.Where(x => x.YarnQA_12H00 < parameters.LowerTolerance || x.YarnQA_12H00 >= parameters.UpperTolerance).AsQueryable();
                 }
            }
            
            if (parameters.Machines.Count > 0)
            {
                var MeasurementPredicate = PredicateBuilder.New<TLSPN_QAMeasurements>();
                foreach (var Machine in parameters.Machines)
                {
                     var temp = Machine;
                     MeasurementPredicate = MeasurementPredicate.Or(s => s.YarnQA_MachineNo_FK == temp.MD_Pk);
                }

                QAItems = QAItems.AsExpandable().Where(MeasurementPredicate);
            }
            

            return QAItems;
        }
    }

    public class SpinningQueryParameters
    {
        public List<TLADM_MachineDefinitions> Machines;
        public List<TLADM_Yarn> YarnTypes;
        public List<TLSPN_YarnOrder> YarnOrders;
        public List<TLSPN_CottonReceivedBales> CotReceivedBales;
        public DateTime FromDate;
        public DateTime ToDate;
        public int MeasurementOpt;

        public int UpperTolerance;
        public int LowerTolerance;

        public bool CottonRecSummarised;
        public bool OutSideTolerance;
        public SpinningQueryParameters()
        {
            Machines = new List<TLADM_MachineDefinitions>();
            YarnTypes = new List<TLADM_Yarn>();
            YarnOrders = new List<TLSPN_YarnOrder>();
            CotReceivedBales = new List<TLSPN_CottonReceivedBales>();
            FromDate = new DateTime();
            ToDate = new DateTime();
            MeasurementOpt = 1;

            OutSideTolerance = false;
            CottonRecSummarised = false;
            UpperTolerance = 0;
            LowerTolerance = 0;
        }
    }

    public class SpinningMachineProductionTotal
    {
        public List<TLADM_MachineDefinitions> Machines;
        public DateTime ProductionTotalDate;
        public decimal Card1Total;
        public decimal Card2Total;
        public decimal Card3Total;
        public decimal Card4Total;
        public decimal RSB1Total;
        public decimal RSB2Total;


        public SpinningMachineProductionTotal()
        {
            Machines = new List<TLADM_MachineDefinitions>();
            ProductionTotalDate = new DateTime();

            Card1Total = 0;
            Card2Total = 0;
            Card3Total = 0;
            Card4Total = 0;
            RSB1Total = 0;
            RSB2Total = 0;
        }
    }    
}
