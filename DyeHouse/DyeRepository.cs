using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;
using LinqKit;
using System.Collections;

namespace DyeHouse
{
    public class DyeRepository : IDisposable
    {
        protected readonly TTI2Entities _context;
        
        public DyeRepository()
        {
            _context = new TTI2Entities();
        }

        public TLADM_CustomerFile LoadCustomer(int Pk)
        {
            return _context.TLADM_CustomerFile.FirstOrDefault(s => s.Cust_Pk == Pk);
        }

        public TLDYE_DyeTransactions LoadDyeTrans(int Pk)
        {
            return _context.TLDYE_DyeTransactions.FirstOrDefault(s => s.TLDYET_Pk == Pk);
        }

        public TLDYE_DyeBatch LoadDyeBatch(int Pk)
        {
            return _context.TLDYE_DyeBatch.FirstOrDefault(s => s.DYEB_Pk == Pk);
        }

        public TLDYE_DyeBatchDetails LoadDyeBatchDetails(int Pk)
        {
            return _context.TLDYE_DyeBatchDetails.FirstOrDefault(s => s.DYEBD_DyeBatch_FK == Pk);
        }

        public TLADM_FabricWeight LoadFabricWeight(int Pk)
        {
            return _context.TLADM_FabricWeight.FirstOrDefault(s => s.FWW_Id == Pk);
        }

        public TLADM_FabWidth LoadFabricWidth(int Pk)
        {
            return _context.TLADM_FabWidth.FirstOrDefault(s => s.FW_Id == Pk);
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

        public TLADM_Griege LoadQuality(int Pk)
        {
            return _context.TLADM_Griege.FirstOrDefault(s => s.TLGreige_Id == Pk);
        }

        public TLADM_GreigeQuality LoadFabricQuality(int Pk)
        {
            return _context.TLADM_GreigeQuality.FirstOrDefault(s => s.GQ_Pk == Pk);
        }

        public TLADM_DyeQDCodes LoadQDCodes(int Pk)
        {
            return _context.TLADM_DyeQDCodes.FirstOrDefault(s => s.QDF_Pk == Pk);
        }

        public TLADM_MachineOperators LoadOperators(int Pk)
        {
            return _context.TLADM_MachineOperators.FirstOrDefault(s => s.MachOp_Pk == Pk);
        }

        public TLADM_DyeRemendyCodes LoadRemedyCodes(int Pk)
        {
            return _context.TLADM_DyeRemendyCodes.FirstOrDefault(s => s.QRC_Pk == Pk);
        }

        public TLADM_MachineDefinitions LoadMachines(int Pk)
        {
            return _context.TLADM_MachineDefinitions.FirstOrDefault(s => s.MD_Pk == Pk);
        }

        public TLDYE_RecipeDefinition LoadDefinition(int Pk)
        {
            return _context.TLDYE_RecipeDefinition.FirstOrDefault(s => s.TLDYE_DefinePk == Pk);
        }

        public TLADM_ConsumablesDC LoadConsummable(int Pk)
        {
            return _context.TLADM_ConsumablesDC.FirstOrDefault(s => s.ConsDC_Pk == Pk);
        }

        public IQueryable<TLADM_ConsumablesDC> SelectConsumables(DyeQueryParameters parameters)
        {
            var Consumables = _context.TLADM_ConsumablesDC.Where(x => !(bool)x.ConsDC_Discontinued).AsQueryable();

            if (parameters.Consummables.Count > 0)
            {
                var ConsumablesPredicate = PredicateBuilder.False<TLADM_ConsumablesDC>();
                foreach (var Consumable in parameters.Consummables)
                {
                    var temp = Consumable;
                    ConsumablesPredicate = ConsumablesPredicate.Or(s => s.ConsDC_Pk  == temp.ConsDC_Pk);
                }

                Consumables = Consumables.AsExpandable().Where(ConsumablesPredicate);
            }
          
            return Consumables;
        }

        public IQueryable<TLDYE_DyeOrder> SelectDyeOrders(DyeQueryParameters parameters)
        {
            var DO = _context.TLDYE_DyeOrder.Where(x => x.TLDYO_OrderDate >= parameters.FromDate && x.TLDYO_OrderDate <= parameters.ToDate & !x.TLDYO_Closed).AsQueryable();

            if (parameters.Customers.Count > 0)
            {
                var CustomerPredicate = PredicateBuilder.False<TLDYE_DyeOrder>();
                foreach (var Customer in parameters.Customers)
                {
                    var temp = Customer;
                    CustomerPredicate = CustomerPredicate.Or(s => s.TLDYO_Customer_FK == temp.Cust_Pk);
                }

                DO = DO.AsExpandable().Where(CustomerPredicate);
            }
            if (parameters.Colours.Count > 0)
            {
                var ColourPredicate = PredicateBuilder.False<TLDYE_DyeOrder>();

                foreach (var colour in parameters.Colours)
                {
                    var temp = colour;
                    ColourPredicate = ColourPredicate.Or(s => s.TLDYO_Colour_FK == temp.Col_Id);
                }

                DO = DO.AsExpandable().Where(ColourPredicate);
            }
            if (parameters.Styles.Count > 0)
            {
                var StylePredicate = PredicateBuilder.False<TLDYE_DyeOrder>();

                foreach (var Style in parameters.Styles)
                {
                    var temp = Style;
                    StylePredicate = StylePredicate.Or(s => s.TLDYO_Style_FK == temp.Sty_Id);
                }

                DO = DO.AsExpandable().Where(StylePredicate);
            }
            if (parameters.Qualities.Count > 0)
            {
                var QualityPredicate = PredicateBuilder.False<TLDYE_DyeOrder>();

                foreach (var Quality in parameters.Qualities)
                {
                    var temp = Quality;
                    QualityPredicate = QualityPredicate.Or(s => s.TLDYO_Greige_FK == temp.TLGreige_Id);
                }

                DO = DO.AsExpandable().Where(QualityPredicate);
            }
            return DO;
        }

        public IQueryable<TLDYE_DyeOrder> SelectActiveDyeOrders(DyeQueryParameters parameters)
        {
            var DO = _context.TLDYE_DyeOrder.Where(x => !x.TLDYO_Closed).AsQueryable();
            if (parameters.Customers.Count > 0)
            {
                var CustomerPredicate = PredicateBuilder.False<TLDYE_DyeOrder>();
                foreach (var Customer in parameters.Customers)
                {
                    var temp = Customer;
                    CustomerPredicate = CustomerPredicate.Or(s => s.TLDYO_Customer_FK == temp.Cust_Pk);
                }

                DO = DO.AsExpandable().Where(CustomerPredicate);
            }
            if (parameters.Colours.Count > 0)
            {
                var ColourPredicate = PredicateBuilder.False<TLDYE_DyeOrder>();

                foreach (var colour in parameters.Colours)
                {
                    var temp = colour;
                    ColourPredicate = ColourPredicate.Or(s => s.TLDYO_Colour_FK == temp.Col_Id);
                }

                DO = DO.AsExpandable().Where(ColourPredicate);
            }
            if (parameters.Styles.Count > 0)
            {
                var StylePredicate = PredicateBuilder.False<TLDYE_DyeOrder>();

                foreach (var Style in parameters.Styles)
                {
                    var temp = Style;
                    StylePredicate = StylePredicate.Or(s => s.TLDYO_Style_FK == temp.Sty_Id);
                }

                DO = DO.AsExpandable().Where(StylePredicate);
            }
            if (parameters.Qualities.Count > 0)
            {
                var QualityPredicate = PredicateBuilder.False<TLDYE_DyeOrder>();

                foreach (var Quality in parameters.Qualities)
                {
                    var temp = Quality;
                    QualityPredicate = QualityPredicate.Or(s => s.TLDYO_Greige_FK == temp.TLGreige_Id);
                }

                DO = DO.AsExpandable().Where(QualityPredicate);
            }
            return DO;
        }

        public IQueryable<TLDYE_DyeBatch> SelectCommissionDyeBatches(DyeQueryParameters parameters)
        {
            var DB = _context.TLDYE_DyeBatch.Where(x =>x.DYEB_CommissinCust && x.DYEB_BatchDate >= parameters.FromDate && x.DYEB_BatchDate <= parameters.ToDate).AsQueryable();
            
            if (parameters.Customers.Count > 0)
            {
                var CustomerPredicate = PredicateBuilder.False<TLDYE_DyeBatch>();
                foreach (var Customer in parameters.Customers)
                {
                    var temp = Customer;
                    CustomerPredicate = CustomerPredicate.Or(s => s.DYEB_Customer_FK == temp.Cust_Pk);
                }

                DB = DB.AsExpandable().Where(CustomerPredicate);
            }

            if (parameters.Colours.Count > 0)
            {
                var ColourPredicate = PredicateBuilder.False<TLDYE_DyeBatch>();

                foreach (var colour in parameters.Colours)
                {
                    var temp = colour;
                    ColourPredicate = ColourPredicate.Or(s => s.DYEB_Colour_FK == temp.Col_Id);
                }

                DB = DB.AsExpandable().Where(ColourPredicate);
            }

            if (parameters.Qualities.Count > 0)
            {
                var QualityPredicate = PredicateBuilder.False<TLDYE_DyeBatch>();

                foreach (var Quality in parameters.Qualities)
                {
                    var temp = Quality;
                    QualityPredicate = QualityPredicate.Or(s => s.DYEB_Greige_FK == temp.TLGreige_Id);
                }

                DB = DB.AsExpandable().Where(QualityPredicate);
            }
            return DB;
        }
        public IQueryable<TLDYE_DyeBatch> SelectDyeBatches(DyeQueryParameters parameters)
        {
            var DB = _context.TLDYE_DyeBatch.Where(x=>!x.DYEB_Transfered).AsQueryable();
            if (parameters.DyeBatches.Count > 0)
            {
                var BatchesPredicate = PredicateBuilder.False<TLDYE_DyeBatch>();
                foreach (var DyeBatch in parameters.DyeBatches)
                {
                    var temp = DyeBatch;
                    BatchesPredicate = BatchesPredicate.Or(s => s.DYEB_Pk == temp.DYEB_Pk);
                }
                DB = DB.AsExpandable().Where(BatchesPredicate);
            }
            if (parameters.Customers.Count > 0)
            {
                var CustomerPredicate = PredicateBuilder.False<TLDYE_DyeBatch>();
                foreach (var Customer in parameters.Customers)
                {
                    var temp = Customer;
                    CustomerPredicate = CustomerPredicate.Or(s => s.DYEB_Customer_FK == temp.Cust_Pk);
                 }
                DB = DB.AsExpandable().Where(CustomerPredicate);
            }
            if(parameters.Qualities.Count != 0)
            {
                var QualitiesPredicate = PredicateBuilder.False<TLDYE_DyeBatch>();

                foreach(var Qual in parameters.Qualities)
                {
                    QualitiesPredicate = QualitiesPredicate.Or(s => s.DYEB_Greige_FK == Qual.TLGreige_Id);
                }

                DB.AsExpandable().Where(QualitiesPredicate);
            }
            if (parameters.Colours.Count > 0)
            {
                var ColourPredicate = PredicateBuilder.False<TLDYE_DyeBatch>();

                foreach (var colour in parameters.Colours)
                {
                    var temp = colour;
                    ColourPredicate = ColourPredicate.Or(s => s.DYEB_Colour_FK == temp.Col_Id);
                }

                DB = DB.AsExpandable().Where(ColourPredicate);
            }
            return DB;
        }
        
        public IQueryable<TLDYE_DyeBatch> SelectViewDyeBatches(DyeQueryParameters parameters)
        {
            var DB = _context.TLDYE_DyeBatch.Where(x => x.DYEB_BatchDate >= parameters.FromDate && x.DYEB_BatchDate <= parameters.ToDate && !x.DYEB_Closed && !x.DYEB_CommissinCust ).AsQueryable();
            if (parameters.FabricQualities.Count > 0)
            {
                var QualityPredicate = PredicateBuilder.False<TLDYE_DyeBatch>();

                foreach (var Quality in parameters.FabricQualities)
                {
                    var temp = Quality;
                    QualityPredicate = QualityPredicate.Or(s => s.DYEB_Pk == temp.GQ_Pk);
                }

                DB = DB.AsExpandable().Where(QualityPredicate);
            }

            if (parameters.Customers.Count > 0)
            {
                var CustomerPredicate = PredicateBuilder.False<TLDYE_DyeBatch>();
                foreach (var Customer in parameters.Customers)
                {
                    var temp = Customer;
                    CustomerPredicate = CustomerPredicate.Or(s => s.DYEB_Customer_FK == temp.Cust_Pk);
                }

                DB = DB.AsExpandable().Where(CustomerPredicate);
            }

            if (parameters.Colours.Count > 0)
            {
                var ColourPredicate = PredicateBuilder.False<TLDYE_DyeBatch>();

                foreach (var colour in parameters.Colours)
                {
                    var temp = colour;
                    ColourPredicate = ColourPredicate.Or(s => s.DYEB_Colour_FK == temp.Col_Id);
                }

                DB = DB.AsExpandable().Where(ColourPredicate);
            }
            return DB;
        }

        public IQueryable<TLDYE_DyeBatchDetails>SelectDyeBatchDetails(DyeQueryParameters parameters)
        {
            var DBDetails = _context.TLDYE_DyeBatchDetails.AsQueryable();
            if (parameters.DyeBatchDetails.Count > 0)
            {
                var DyeBatchDetPredicate = PredicateBuilder.False<TLDYE_DyeBatchDetails>();
                foreach (var BatchDetail in parameters.DyeBatchDetails)
                {
                    var Temp = BatchDetail;
                    DyeBatchDetPredicate = DyeBatchDetPredicate.Or(s => s.DYEBD_DyeBatch_FK == Temp.DYEBD_DyeBatch_FK);
                }
            }
            return DBDetails;
        }

        public IQueryable<TLADM_Griege> PPSGreige(DyeQueryParameters parameters)
        {
            var SOH = _context.TLADM_Griege.AsQueryable();

            if (parameters.Qualities.Count > 0)
            {
                var ReceipePredicate = PredicateBuilder.False<TLADM_Griege>();
                foreach (var Quality in parameters.Qualities)
                {
                    var temp = Quality;
                    ReceipePredicate = ReceipePredicate.Or(s => s.TLGreige_Id == temp.TLGreige_Id);
                }

                SOH = SOH.AsExpandable().Where(ReceipePredicate);
            }

            return SOH;
        }

        public IQueryable<TLADM_Colours> PPSColour(DyeQueryParameters parameters)
        {
            var SOH = _context.TLADM_Colours.AsQueryable();

            if (parameters.Colours.Count > 0)
            {
                var ReceipePredicate = PredicateBuilder.False<TLADM_Colours>();
                foreach (var Quality in parameters.Colours)
                {
                    var temp = Quality;
                    ReceipePredicate = ReceipePredicate.Or(s => s.Col_Id == temp.Col_Id);
                }

                SOH = SOH.AsExpandable().Where(ReceipePredicate);
            }

            return SOH;
        }

        public IQueryable<TLKNI_GreigeCommissionTransctions> CommissionTransactions(DyeQueryParameters parameters)
        {
            var Transactions = _context.TLKNI_GreigeCommissionTransctions.Where(x=>x.GreigeCom_Transdate >= parameters.FromDate && x.GreigeCom_Transdate <= parameters.ToDate).OrderBy(x => x.GreigeCom_GrnNo).AsQueryable();
            if (parameters.Customers.Count > 0)
            {
                var CustomerPredicate = PredicateBuilder.False<TLKNI_GreigeCommissionTransctions>();
                foreach (var Customer in parameters.Customers)
                {
                    var temp = Customer;
                    CustomerPredicate = CustomerPredicate.Or(s => s.GreigeCom_Customer_FK == temp.Cust_Pk);    
                }

                Transactions = Transactions.AsExpandable().Where(CustomerPredicate);
            }

            if (parameters.FabricQualities.Count > 0)
            {
                var QualityPredicate = PredicateBuilder.False<TLKNI_GreigeCommissionTransctions>();
                foreach (var Quality in parameters.FabricQualities)
                {
                    var temp = Quality;
                    QualityPredicate = QualityPredicate.Or(s => s.GreigeCom_ProductType_FK == Quality.GQ_Pk);
                }

                Transactions = Transactions.AsExpandable().Where(QualityPredicate);
            }

            return Transactions;
        }

        public IQueryable<TLDYE_RecipeDefinition> SOHQuery(DyeQueryParameters parameters)
        {
            var SOH = _context.TLDYE_RecipeDefinition.Where(x=>x.TLDYE_ColorChart_FK != null).AsQueryable();
            if (parameters.Colours.Count > 0)
            {
               
                var ReceipePredicate = PredicateBuilder.False<TLDYE_RecipeDefinition>();
                foreach (var Colour in parameters.Colours)
                {
                    var temp = Colour;
                    ReceipePredicate = ReceipePredicate.Or(s => s.TLDYE_ColorChart_FK == temp.Col_Id);
                }

                SOH = SOH.AsExpandable().Where(ReceipePredicate);
               
            }

            if (parameters.RecDefinitions.Count > 0)
            {
                var RecDefPredicate = PredicateBuilder.False<TLDYE_RecipeDefinition>();
                foreach (var Definition in parameters.RecDefinitions)
                {
                    var temp = Definition;
                    RecDefPredicate = RecDefPredicate.Or(s => s.TLDYE_DefinePk == temp.TLDYE_DefinePk);
                }

                SOH = SOH.AsExpandable().Where(RecDefPredicate);
            }

            if (parameters.Qualities.Count > 0)
            {
                var QualPredicate = PredicateBuilder.False<TLDYE_RecipeDefinition>();
                foreach (var Quality in parameters.Qualities)
                {
                    var temp = Quality;
                   // QualPredicate = QualPredicate.Or(s => s.TLDYE_DefineGreigeQual_Fk == temp.TLGreige_Id);
                }

                SOH = SOH.AsExpandable().Where(QualPredicate);
            }

            return SOH;
        }


        public void Dispose()
       {
              if (_context != null)
              {
                  _context.Dispose();
              }
       }

    }

    public class DyeQueryParameters
    {
        public List<TLADM_Sizes> Sizes;
        public List<TLADM_Colours> Colours;
        public List<TLADM_Styles> Styles;
        public List<TLDYE_RecipeDefinition> RecDefinitions;
        public List<TLADM_Griege> Qualities;
        public List<TLADM_CustomerFile> Customers;
        public List<TLDYE_DyeBatch> DyeBatches;
        public List<TLDYE_DyeBatchDetails> DyeBatchDetails;
        public List<TLADM_FabricWeight> FabricWeights;
        public List<TLADM_FabWidth> FabricWidths;
        public List<TLADM_GreigeQuality> FabricQualities;
        public List<TLKNI_GreigeCommissionTransctions> CommissionTransactions;
        public List<TLADM_DyeQDCodes> DyeQDCodes;
        public List<TLADM_MachineOperators> Operators;
        public List<TLADM_DyeRemendyCodes> RemedyCodes;
        public List<TLADM_MachineDefinitions> Machines;
        public List<TLADM_ConsumablesDC> Consummables;
        public List<TLDYE_DyeTransactions > DyeTransactions; 

        public int DO_OptionSelected; 
        public DateTime FromDate;
        public DateTime ToDate;
        public bool CommissionCustomers;
        public bool RejectedBatches;
        public int ops3_ComboSelected;
        public int ops3_ComboSelectedValue;
        public StringBuilder Notes;
        public bool CommDyeing;
        public bool CommDyeingReprint;

        public bool CalculateProdResults;
        public bool FabricSales;
        public bool ProdWIP;
        public bool ProdWIPCompleted;

        public DyeQueryParameters()
        {

            Sizes = new List<TLADM_Sizes>();
            Colours = new List<TLADM_Colours>();
            Styles = new List<TLADM_Styles>();
            RecDefinitions = new List<TLDYE_RecipeDefinition>();
            Qualities = new List<TLADM_Griege>();
            Customers = new List<TLADM_CustomerFile>();
            DyeBatches = new List<TLDYE_DyeBatch>();
            DyeBatchDetails = new List<TLDYE_DyeBatchDetails>();
            FabricWeights = new List<TLADM_FabricWeight>();
            FabricWidths = new List<TLADM_FabWidth>();
            FabricQualities = new List<TLADM_GreigeQuality>();
            CommissionTransactions = new List<TLKNI_GreigeCommissionTransctions>();
            DyeQDCodes = new List<TLADM_DyeQDCodes>();
            Operators = new List<TLADM_MachineOperators>();
            RemedyCodes = new List<TLADM_DyeRemendyCodes>();
            Machines = new List<TLADM_MachineDefinitions>();
            Consummables = new List<TLADM_ConsumablesDC>();
            DyeTransactions = new List<TLDYE_DyeTransactions>();
 
            FromDate = new DateTime();
            ToDate = new DateTime();
            CommissionCustomers = false;
            RejectedBatches = false;
            DO_OptionSelected = 0;
            ops3_ComboSelected = 1;
            ops3_ComboSelectedValue = 1;
            CommDyeing = true;
            CommDyeingReprint = false;

            ProdWIP = false;
            ProdWIPCompleted = false;

            CalculateProdResults = false;
            FabricSales = false;

            Notes = new StringBuilder();

        }


    }


    public class DyeProductionDetails 
    {
        public int GreigePk { get; set; }
        public int ColorPk { get; set; }
        public Decimal PlannedProd { get; set; }
        public int DyeBatchPk { get; set; } 

    }
        
}
