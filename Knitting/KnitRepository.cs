using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;
using LinqKit;

namespace Knitting
{
    public class KnitRepository : IDisposable
    {
        protected readonly TTI2Entities _context;

        public KnitRepository()
        {
                _context = new TTI2Entities();
        }

        public TLADM_WhseStore LoadStore(int Pk)
        {
            return _context.TLADM_WhseStore.FirstOrDefault(s => s.WhStore_Id == Pk);
        }

        public TLADM_Yarn LoadYarn(int Pk)
        {
            return _context.TLADM_Yarn.FirstOrDefault(s => s.YA_Id == Pk);
        }

        public TLKNI_Order LoadOrder(int Pk)
        {
            return _context.TLKNI_Order.FirstOrDefault(s => s.KnitO_Pk == Pk);
        }

        public TLADM_MachineDefinitions LoadMachine(int Pk)
        {
            return _context.TLADM_MachineDefinitions.FirstOrDefault(s => s.MD_Pk == Pk);
        }

       public TLADM_Griege LoadGriege(int Pk)
        {
            return _context.TLADM_Griege.FirstOrDefault(s => s.TLGreige_Id == Pk); 
        }

       public TLADM_GreigeQuality LoadGreigeQuality(int Pk)
        {
            return _context.TLADM_GreigeQuality.FirstOrDefault(s => s.GQ_Pk== Pk); 
        }

        public TLADM_CustomerFile LoadCustomer(int Pk)
        {
            return _context.TLADM_CustomerFile.FirstOrDefault(s=>s.Cust_Pk == Pk);
        }

        public TLADM_MachineOperators LoadOperator(int Pk)
        {
            return _context.TLADM_MachineOperators.FirstOrDefault(s => s.MachOp_Pk == Pk);
        }

       public TLADM_StockTakeFreq LoadStockTake(int Pk)
       {
           return _context.TLADM_StockTakeFreq.FirstOrDefault(s => s.STF_Pk == Pk);
       }

       public TLSPN_YarnOrder LoadYarnOrder(int Pk)
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


       public IQueryable<TLKNI_GreigeProduction> GreigeProduction(KnitQueryParameters parameters)
       {
           IQueryable<TLKNI_GreigeProduction> GriegeProd;
           var YarnOrders = _context.TLSPN_YarnOrder.AsQueryable();
           var KnitOrders = _context.TLKNI_Order.AsQueryable();

           GriegeProd = _context.TLKNI_GreigeProduction.Where(x => x.GreigeP_PDate >= parameters.FromDate && x.GreigeP_PDate <= parameters.ToDate && x.GreigeP_Captured).AsQueryable();

           if (parameters.YarnTypes.Count != 0)
           {
               var YarnPredicate = PredicateBuilder.False<TLSPN_YarnOrder>();

               foreach (var Yt in parameters.YarnTypes)
               {
                   var temp = Yt;
                   YarnPredicate = YarnPredicate.Or(s => s.Yarno_YarnType_FK == temp.YA_Id);
               }

               YarnOrders = YarnOrders.AsExpandable().Where(YarnPredicate);

               var KnitOrderPredicate = PredicateBuilder.False<TLKNI_Order>();

               foreach (var YarnOrder in YarnOrders)
               {
                   var temp = YarnOrder;
                   KnitOrderPredicate = KnitOrderPredicate.Or(s => s.KnitO_YarnO_FK == temp.YarnO_Pk);
               }

               KnitOrders = KnitOrders.AsExpandable().Where(KnitOrderPredicate);

               var GPPredicate = PredicateBuilder.False<TLKNI_GreigeProduction>();

               foreach (var Ko in KnitOrders)
               {
                   var temp = Ko;
                   GPPredicate = GPPredicate.Or(s => s.GreigeP_KnitO_Fk == temp.KnitO_Pk);
               }
               GriegeProd = GriegeProd.AsExpandable().Where(GPPredicate);
           }

           if (parameters.Customers.Count != 0)
           {
               var CustomerPredicate = PredicateBuilder.False<TLKNI_GreigeProduction>();
               foreach (var Customer in parameters.Customers)
               {
                   var temp = Customer;
                   CustomerPredicate = CustomerPredicate.Or(s => s.GreigeP_CommissionCust_FK == temp.Cust_Pk);
               }

               GriegeProd = GriegeProd.AsExpandable().Where(CustomerPredicate);
           }

           if (parameters.Grade.Length != 0)
           {
               var GradePredicate = PredicateBuilder.False<TLKNI_GreigeProduction>();
               GriegeProd = GriegeProd.AsExpandable().Where(x=>x.GreigeP_Grade == parameters.Grade.ToUpper());
           }

           if (parameters.Greiges.Count != 0)
           {
               var GreigePredicate = PredicateBuilder.False<TLKNI_GreigeProduction>();
               foreach (var Greige in parameters.Greiges)
               {
                   var temp = Greige;
                   GreigePredicate = GreigePredicate.Or(s => s.GreigeP_Greige_Fk == temp.TLGreige_Id);
               }

               GriegeProd = GriegeProd.AsExpandable().Where(GreigePredicate);

           }

           if (parameters.Operators.Count != 0)
           {
               var OperatorPredicate = PredicateBuilder.False<TLKNI_GreigeProduction>();
               foreach (var Operator in parameters.Operators)
               {
                   var temp = Operator;
                   OperatorPredicate = OperatorPredicate.Or(s => s.GreigeP_Operator_FK == temp.MachOp_Pk);
               }

               GriegeProd = GriegeProd.AsExpandable().Where(OperatorPredicate);
           }

           if (parameters.Machines.Count != 0)
           {
               var MachinePredicate = PredicateBuilder.False<TLKNI_GreigeProduction>();
               foreach (var Machine in parameters.Machines)
               {
                   var temp = Machine;
                   MachinePredicate = MachinePredicate.Or(s => s.GreigeP_Machine_FK == temp.MD_Pk);
               }

               GriegeProd = GriegeProd.AsExpandable().Where(MachinePredicate);
           }

           return GriegeProd;
       }

       
       public IQueryable<TLKNI_GreigeProduction> GKMeasurements(KnitQueryParameters parameters)
       {
           var GreigeProduction = _context.TLKNI_GreigeProduction.Where(x => x.GreigeP_Inspected && x.GreigeP_InspDate >= parameters.FromDate && x.GreigeP_InspDate <= parameters.ToDate && x.GreigeP_Captured).AsQueryable();
           var YarnOrders = _context.TLSPN_YarnOrder.AsQueryable();
           var KnitOrders = _context.TLKNI_Order.AsQueryable();

           if (parameters.YarnTypes.Count != 0)
           {
               var YarnPredicate = PredicateBuilder.False<TLSPN_YarnOrder>();

               foreach (var Yt in parameters.YarnTypes)
               {
                   var temp = Yt;
                   YarnPredicate = YarnPredicate.Or(s => s.Yarno_YarnType_FK == int.Parse(temp.ToString()));
               }

               YarnOrders = YarnOrders.AsExpandable().Where(YarnPredicate);

               var KnitOrderPredicate = PredicateBuilder.False<TLKNI_Order>();

               foreach (var YarnOrder in YarnOrders)
               {
                   var temp = YarnOrder;
                   KnitOrderPredicate = KnitOrderPredicate.Or(s => s.KnitO_YarnO_FK == int.Parse(temp.ToString()));
               }

               KnitOrders = KnitOrders.AsExpandable().Where(KnitOrderPredicate);

               var GPPredicate = PredicateBuilder.False<TLKNI_GreigeProduction>();

               foreach (var Ko in KnitOrders)
               {
                   var temp = Ko;
                   GPPredicate = GPPredicate.Or(s => s.GreigeP_KnitO_Fk == temp.KnitO_Pk);
               }
               GreigeProduction = GreigeProduction.AsExpandable().Where(GPPredicate);
           }

           if (parameters.Greiges.Count > 0)
           {
               var GreigePredicate = PredicateBuilder.False<TLKNI_GreigeProduction>();
               foreach (var Greige in parameters.Greiges)
               {
                   var temp = Greige;
                   GreigePredicate = GreigePredicate.Or(s => s.GreigeP_Greige_Fk == temp.TLGreige_Id);
               }

               GreigeProduction = GreigeProduction.AsExpandable().Where(GreigePredicate);
           }

           if (parameters.Operators.Count > 0)
           {
               var OperatorPredicate = PredicateBuilder.False<TLKNI_GreigeProduction>();
               foreach (var Operator in parameters.Operators)
               {
                   var temp = Operator;
                   OperatorPredicate = OperatorPredicate.Or(s => s.GreigeP_Operator_FK == temp.MachOp_Pk);
               }

               GreigeProduction = GreigeProduction.AsExpandable().Where(OperatorPredicate);
           }

           if (parameters.Machines.Count > 0)
           {
               var MachinePredicate = PredicateBuilder.False<TLKNI_GreigeProduction>();
               foreach (var Machine in parameters.Machines)
               {
                   var temp = Machine;
                   MachinePredicate = MachinePredicate.Or(s => s.GreigeP_Machine_FK == temp.MD_Pk);
               }

               GreigeProduction = GreigeProduction.AsExpandable().Where(MachinePredicate);
           }

           return GreigeProduction;
       }
       

       public IQueryable<TLKNI_YarnOrderPallets> YarnOrderPallets(KnitQueryParameters parameters)
       {
           var YarnOP = _context.TLKNI_YarnOrderPallets.Where(x => x.TLKNIOP_ReservedDate >= parameters.FromDate && x.TLKNIOP_ReservedDate <= parameters.ToDate && x.TLKNIOP_ReservedBy != 9999 && x.TLKNIOP_YarnOrder_FK != null).AsQueryable();

           if (parameters.YarnTypes.Count != 0)
           {
               var YarnTypePredicate = PredicateBuilder.False<TLKNI_YarnOrderPallets>();
               foreach (var YarnType in parameters.YarnTypes)
               {
                   var temp = YarnType;
                   YarnTypePredicate = YarnTypePredicate.Or(s => s.TLKNIOP_YarnType_FK == temp.YA_Id);
               }
           }
           if (parameters.YarnOrders.Count > 0)
           {
               var YarnPredicate = PredicateBuilder.False<TLKNI_YarnOrderPallets>();
               foreach (var YarnOrder in parameters.YarnOrders)
               {
                   var temp = YarnOrder;
                   YarnPredicate = YarnPredicate.Or(s => s.TLKNIOP_YarnOrder_FK == temp.YarnO_Pk);
               }

               YarnOP = YarnOP.AsExpandable().Where(YarnPredicate); 
           }
           return YarnOP;
       }

       public IQueryable<TLKNI_GreigeProduction> SOHGreigeProduction(KnitQueryParameters parameters)
       {
           IQueryable<TLKNI_GreigeProduction> GreigeProduction;
           if (!parameters.BoughtInFabric)
           {
               GreigeProduction = _context.TLKNI_GreigeProduction.Where(x => x.GreigeP_Captured && !x.GreigeP_Dye && x.GreigeP_weightAvail > 0 && !x.GreigeP_BoughtIn).AsQueryable();
           }
           else
           {
               GreigeProduction = _context.TLKNI_GreigeProduction.Where(x => x.GreigeP_Captured && !x.GreigeP_Dye && x.GreigeP_weightAvail > 0 && x.GreigeP_BoughtIn).AsQueryable();
           }

           if (parameters.Grade.Length != 0)
           {
               var GradePredicate = PredicateBuilder.False<TLKNI_GreigeProduction>();
               GreigeProduction = GreigeProduction.AsExpandable().Where(x => x.GreigeP_Grade.Contains(parameters.Grade.ToUpper()));
           }
                     
           if (parameters.Greiges.Count > 0)
           {
               var GreigePredicate = PredicateBuilder.False<TLKNI_GreigeProduction>();
               foreach (var Greige in parameters.Greiges)
               {
                   var temp = Greige;
                   GreigePredicate = GreigePredicate.Or(s => s.GreigeP_Greige_Fk == temp.TLGreige_Id);
               }

               GreigeProduction = GreigeProduction.AsExpandable().Where(GreigePredicate);
           }

           if (parameters.WhseStores.Count > 0)
           {
               var WhsePredicate = PredicateBuilder.False<TLKNI_GreigeProduction>();
               foreach (var whse in parameters.WhseStores)
               {
                   var temp = whse;
                   WhsePredicate = WhsePredicate.Or(s => s.GreigeP_Store_FK == temp.WhStore_Id);
               }

               GreigeProduction = GreigeProduction.AsExpandable().Where(WhsePredicate);
           }

       
           return GreigeProduction;
       }

       public IQueryable<TLKNI_GreigeProduction> QAGreigeProduction(KnitQueryParameters parameters)
       {
           var GreigeProduction = _context.TLKNI_GreigeProduction.Where(x => x.GreigeP_InspDate >= parameters.FromDate && x.GreigeP_InspDate <= parameters.ToDate && x.GreigeP_Inspected && x.GreigeP_Captured && x.GreigeP_Machine_FK != null).AsQueryable();

           if (parameters.Greiges.Count > 0)
           {
               var GreigePredicate = PredicateBuilder.False<TLKNI_GreigeProduction>();
               foreach (var Greige in parameters.Greiges)
               {
                   var temp = Greige;
                   GreigePredicate = GreigePredicate.Or(s => s.GreigeP_Greige_Fk == temp.TLGreige_Id);
               }

               GreigeProduction = GreigeProduction.AsExpandable().Where(GreigePredicate);

           }

           if (parameters.Operators.Count > 0)
           {
               var OperatorPredicate = PredicateBuilder.False<TLKNI_GreigeProduction>();
               foreach (var Operator in parameters.Operators)
               {
                   var temp = Operator;
                   OperatorPredicate = OperatorPredicate.Or(s => s.GreigeP_Operator_FK == temp.MachOp_Pk);
               }

               GreigeProduction = GreigeProduction.AsExpandable().Where(OperatorPredicate);
           }

           if (parameters.Machines.Count > 0)
           {
               var MachinePredicate = PredicateBuilder.False<TLKNI_GreigeProduction>();
               foreach (var Machine in parameters.Machines)
               {
                   var temp = Machine;
                   MachinePredicate = MachinePredicate.Or(s => s.GreigeP_Machine_FK == temp.MD_Pk);
               }

               GreigeProduction = GreigeProduction.AsExpandable().Where(MachinePredicate);
           }

           return GreigeProduction;
       }

       public IQueryable<TLKNI_Order> KnitOrders(KnitQueryParameters parameters)
       {
           var KnitOrders = _context.TLKNI_Order.Where(x=>!(bool)x.KnitO_Closed).AsQueryable();
           var YarnOrders = _context.TLSPN_YarnOrder.AsQueryable();
           //--------------------------------------------------------------
           // Filter out any unwanted KnitOrders
           //------------------------------------------------------------
           if (parameters.YarnTypes.Count != 0)
           {
               var YarnPredicate = PredicateBuilder.False<TLSPN_YarnOrder>();

               foreach(var Yt in parameters.YarnTypes)
               {
                   var temp = Yt;
                   YarnPredicate =  YarnPredicate.Or(s=>s.Yarno_YarnType_FK == int.Parse(temp.ToString()));
               }

               YarnOrders = YarnOrders.AsExpandable().Where(YarnPredicate);

               var KnitOrderPredicate = PredicateBuilder.False<TLKNI_Order>();

               foreach (var YarnOrder in YarnOrders)
               {
                   var temp = YarnOrder;
                   KnitOrderPredicate = KnitOrderPredicate.Or(s => s.KnitO_YarnO_FK == int.Parse(temp.ToString()));
               }

               KnitOrders = KnitOrders.AsExpandable().Where(KnitOrderPredicate);
           }

           if (parameters.KnitOrderPk.Count > 0)
           {
               var PkPredicate = PredicateBuilder.False<TLKNI_Order>();
               foreach (var Pk in parameters.KnitOrderPk)
               {
                   var temp = Pk;
                   PkPredicate = PkPredicate.Or(s => s.KnitO_Pk == int.Parse(temp.ToString()));
               }

               KnitOrders = KnitOrders.AsExpandable().Where(PkPredicate);
           }

           //--------------------------------------------------------------
           // Filter out any Greiges
           //------------------------------------------------------------
           if (parameters.Greiges.Count > 0)
           {
               var GreigePredicate = PredicateBuilder.False<TLKNI_Order>();
               foreach (var Greige in parameters.Greiges)
               {
                   var temp = Greige;
                   GreigePredicate = GreigePredicate.Or(s => s.KnitO_Product_FK == temp.TLGreige_Id);
               }

               KnitOrders = KnitOrders.AsExpandable().Where(GreigePredicate);
           }
          
           //--------------------------------------------------------------
           // Filter out any particular Customers if neccessary
           //------------------------------------------------------------
           if (parameters.Customers.Count > 0)
           {
               var CustomerPredicate = PredicateBuilder.False<TLKNI_Order>();
               foreach (var Customer in parameters.Customers)
               {
                   var temp = Customer;
                   CustomerPredicate = CustomerPredicate.Or(s => s.KnitO_Customer_FK == temp.Cust_Pk);
               }

               KnitOrders = KnitOrders.AsExpandable().Where(CustomerPredicate);
           }

           //----------------------------------------------------------------------
           // Filter out any particular Machines if neccessary
           //------------------------------------------------------------
           if (parameters.Machines.Count > 0)
           {
               var MachinePredicate = PredicateBuilder.False<TLKNI_Order>();
               foreach (var Machine in parameters.Machines)
               {
                   var temp = Machine;
                   MachinePredicate = MachinePredicate.Or(s => s.KnitO_Machine_FK == temp.MD_Pk);
               }

               KnitOrders = KnitOrders.AsExpandable().Where(MachinePredicate);
           }

           
           return KnitOrders;
       }
     
    }

    public class KnitQueryParameters
    {
        public List<TLADM_CustomerFile> Customers;
        public List<TLADM_MachineDefinitions> Machines;
        public List<TLADM_Griege> Greiges;
        public List<TLADM_MachineOperators> Operators;
        public List<Int32> KnitOrderPk;
        public List<TLADM_StockTakeFreq> StockTakeFreq;
        public List<TLADM_WhseStore> WhseStores;
        public List<TLADM_GreigeQuality> ProductQualities;
        public List<TLADM_Yarn> YarnTypes;
        public DateTime FromDate;
        public DateTime ToDate;
        public String Grade;
        public List<TLSPN_YarnOrder> YarnOrders;
        public bool BoughtInFabric;
        public bool GradeAwithWarnings;


        public KnitQueryParameters()
        {
            YarnTypes = new List<TLADM_Yarn>();
            Customers = new List<TLADM_CustomerFile>();
            Machines = new List<TLADM_MachineDefinitions>();
            Greiges = new List<TLADM_Griege>();
            Operators = new List<TLADM_MachineOperators>();
            KnitOrderPk = new List<Int32>();
            FromDate = new DateTime();
            ToDate = new DateTime();
            Grade = String.Empty;
            StockTakeFreq = new List<TLADM_StockTakeFreq>();
            WhseStores = new List<TLADM_WhseStore>();
            ProductQualities = new List<TLADM_GreigeQuality>();
            YarnOrders = new List<TLSPN_YarnOrder>();
            BoughtInFabric = false;
            GradeAwithWarnings = false;
        }

    }
}
